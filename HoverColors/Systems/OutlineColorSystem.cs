// <copyright file="OutlineColorSystem.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/OutlineColorSystem.cs
// Purpose: Apply user-chosen outline color + fill/outline alpha to the game's selection highlight.
// Can temporarily override colors while the player is using Bulldoze / Better Bulldozer,
// road tools, or detail-style NetTool lanes so invisible-alpha settings don't get in the way.
//
// Surfaces written (one color choice covers all of them, two alpha sliders control opacity):
//   - RenderingSettingsData.m_HoveredColor.RGB   ← Outline RGB (lot-pattern tint on hovered building)
//   - RenderingSettingsData.m_OwnerColor.RGBA    ← Owner color (parent/owned objects while placing)
//   - Material _OuterColor.RGB                   ← Outline RGB (the visible halo edge color)
//   - Material _OuterColor.a                     ← OutlineA   (halo edge opacity)
//   - Material _InnerColor.RGB                   ← Fill RGB (tint of fill overlay inside silhouette;
//                                                   white = fill takes the per-object hover color)
//   - Material _InnerColor.a                     ← FillA      (fill overlay opacity)
//   - Material _OutlineWidth                     ← captured vanilla width x OutlineThicknessScale
//                                                  (optional: skipped if the shader lacks it)
//
// Tool override: controlled by HoverColorsSettings.ToolColorMode.
//   - Object/network placement errors: optional vanilla ErrorColor wins first.
//   - Recommended: WarningColor for bulldozer, softer vanilla blue for roads.
//   - Detail NetTools: optional custom color for fences/hedges/lanes from detailing mods.
//   - Vanilla: captured vanilla hover profile while those tools are active.
//   - Custom: player color everywhere.
// The dirty-flag tracks the *effective* values so an idle tool session is still ~free per frame.
//
// Performance (matters because this system runs every Rendering tick):
//   - The HDRP CustomPassVolume / OutlinesWorldUIPass / Material refs are found ONCE and cached.
//     The fallback scene scan is throttled while loading so we never call
//     Object.FindObjectsOfType<CustomPassVolume>() every frame.
//   - Last-applied effective color snapshot is kept, so OnUpdate early-returns when
//     neither the sliders nor the active-tool override flag changed.
//   - Cache invalidates only when the Material reference goes destroyed-null (e.g. scene reload).

namespace HoverColors.Systems
{
    using System;
    using System.Reflection;
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Prefabs;
    using Game.Rendering;
    using Game.Tools;
    using HoverColors.Settings;
    using Unity.Entities;
    using UnityEngine;
    using UnityEngine.Rendering.HighDefinition;

    public partial class OutlineColorSystem : GameSystemBase
    {
        // Vanilla cyan defaults applied during Bulldoze / Net tool override.
        // Keep in sync with HoverColorsSettings.SetDefaults().

        // Vanilla cyan fallbacks used by captured/override paths.
        // New-install live HC color may intentionally be different.
        private const float kVanillaR = 0.502f;
        private const float kVanillaG = 0.869f;
        private const float kVanillaB = 1f;
        private const float kVanillaOutlineA = 0.855f;
        private const float kVanillaFillA = 0f;
        private const float kVanillaOwnerR = 0.247f;
        private const float kVanillaOwnerG = 0.981f;
        private const float kVanillaOwnerB = 0.247f;
        private const float kVanillaOwnerA = 0.702f;
        private const float kRoadRecommendedOutlineA = 0.75f;
        private const float kMaterialResolveRetrySeconds = 0.5f;

        // Cached so the per-frame path never re-hashes the property name.
        private static readonly int s_OutlineWidthProperty = Shader.PropertyToID("_OutlineWidth");

        public static Color CapturedHoveredColor { get; private set; } = new Color(kVanillaR, kVanillaG, kVanillaB, kVanillaOutlineA);
        public static Color CapturedOwnerColor { get; private set; } = new Color(kVanillaOwnerR, kVanillaOwnerG, kVanillaOwnerB, kVanillaOwnerA);
        public static Color CapturedOuterColor { get; private set; } = new Color(1f, 1f, 1f, kVanillaOutlineA);
        public static Color CapturedInnerColor { get; private set; } = new Color(1f, 1f, 1f, kVanillaFillA);
        public static Color CapturedWarningColor { get; private set; } = new Color(1f, 1f, 0.5f, 0.447058827f);
        public static Color CapturedErrorColor { get; private set; } = new Color(1f, 0.5f, 0.5f, 0.447058827f);
        // Placeholder until the real runtime width is read; nothing is written before that happens.
        public static float CapturedOutlineWidth { get; private set; } = 1f;
        public static float CapturedOutlineA { get; private set; } = kVanillaOutlineA;
        public static float CapturedFillA { get; private set; } = kVanillaFillA;
        public static bool HasCapturedVanillaDefaults { get; private set; }

        private EntityQuery m_RenderSettingsQuery;
        private ToolSystem? m_ToolSystem;
        private PrefabSystem? m_PrefabSystem;
        private readonly PrefabID m_RenderingSettingsPrefab = new(nameof(m_RenderingSettingsPrefab), "RenderingSettings");

        // Cached HDRP outline material. UnityEngine.Object operator!= detects destroyed-but-not-null.
        private Material? m_OutlineMaterial;
        private float m_NextMaterialResolveTime;
        private bool m_PrefabDefaultsCaptured;
        private bool m_RenderingDefaultsCaptured;
        private bool m_MaterialDefaultsCaptured;
        private bool m_CaptureLogged;

        // Tracks that m_OutlineMaterial once held a live reference, so a later null can be told
        // apart from "never resolved" - Unity reports a destroyed Material as null.
        private bool m_OutlineMaterialResolved;

        // Thickness capture is deliberately independent of m_MaterialDefaultsCaptured: if a future
        // game build drops _OutlineWidth, colors and alpha must keep working untouched.
        private bool m_OutlineWidthCaptured;
        private bool m_OutlineWidthMissing;
        private bool m_OutlineThicknessApplied;
        private bool m_HasWrittenOutlineWidth;
        private float m_LastWrittenOutlineWidth;

        // Last-applied EFFECTIVE values (after tool-override decision).
        private float m_LastR, m_LastG, m_LastB, m_LastOutlineA, m_LastFillA;
        private float m_LastFillR, m_LastFillG, m_LastFillB;
        private float m_LastOutlineThicknessScale;
        private float m_LastOwnerR, m_LastOwnerG, m_LastOwnerB, m_LastOwnerA;
        private EffectivePalette m_LastPalette;
        private bool m_Applied;
        private ToolBaseSystem? m_CachedErrorTool;
        private EntityQuery m_CachedErrorQuery;
        private bool m_HasCachedErrorQuery;

        private static readonly FieldInfo? s_ToolErrorQueryField =
            typeof(ToolBaseSystem).GetField("m_ErrorQuery", BindingFlags.Instance | BindingFlags.NonPublic);

        private enum ToolKind
        {
            None,
            Bulldoze,
            NetRoad,
            NetDetailing,
        }

        private enum EffectivePalette
        {
            Custom,
            CapturedVanilla,
            VanillaToolError,
            RecommendedBulldoze,
            RecommendedNet,
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            m_RenderSettingsQuery = GetEntityQuery(ComponentType.ReadWrite<RenderingSettingsData>());
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            InitializeHoverToggle();
        }

        protected override void OnDestroy()
        {
            RestoreOutlineWidth();
            base.OnDestroy();
        }

        protected override void OnUpdate()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null)
            {
                return;
            }

            InvalidateCacheIfMaterialDestroyed();
            TryCaptureVanillaDefaults();

            float r, g, b, outlineA, fillA, ownerR, ownerG, ownerB, ownerA;
            float fillR, fillG, fillB;
            float thicknessScale = Mathf.Clamp(
                settings.OutlineThicknessScale,
                HoverColorsSettings.kMinOutlineThicknessScale,
                HoverColorsSettings.kMaxOutlineThicknessScale);
            EffectivePalette palette;
            ToolBaseSystem? activeToolSystem = m_ToolSystem?.activeTool;
            ToolKind activeTool = GetActiveToolKind(activeToolSystem);
            if (settings.UseOverlapWarningColor && HasSupportedPlacementError(activeToolSystem))
            {
                Color error = CapturedErrorColor;
                r = error.r;
                g = error.g;
                b = error.b;
                outlineA = error.a;
                fillA = CapturedFillA;
                fillR = CapturedInnerColor.r;
                fillG = CapturedInnerColor.g;
                fillB = CapturedInnerColor.b;
                ownerR = CapturedOwnerColor.r;
                ownerG = CapturedOwnerColor.g;
                ownerB = CapturedOwnerColor.b;
                ownerA = CapturedOwnerColor.a;
                palette = EffectivePalette.VanillaToolError;
            }
            else if (settings.ToolColorMode == HoverColorsSettings.kToolColorModeRecommended
                && activeTool == ToolKind.Bulldoze)
            {
                Color warning = CapturedWarningColor;
                r = warning.r;
                g = warning.g;
                b = warning.b;
                outlineA = warning.a;
                fillA = CapturedFillA;
                fillR = CapturedInnerColor.r;
                fillG = CapturedInnerColor.g;
                fillB = CapturedInnerColor.b;
                ownerR = warning.r;
                ownerG = warning.g;
                ownerB = warning.b;
                ownerA = warning.a;
                palette = EffectivePalette.RecommendedBulldoze;
            }
            else if (activeTool == ToolKind.NetDetailing)
            {
                if (settings.UseCustomColorsForNetLanes)
                {
                    r = settings.OutlineR;
                    g = settings.OutlineG;
                    b = settings.OutlineB;
                    outlineA = settings.OutlineA;
                    fillA = settings.FillA;
                    fillR = settings.FillR;
                    fillG = settings.FillG;
                    fillB = settings.FillB;
                    ownerR = settings.OwnerR;
                    ownerG = settings.OwnerG;
                    ownerB = settings.OwnerB;
                    ownerA = settings.OwnerA;
                    palette = MatchesCapturedVanillaProfile(r, g, b, outlineA, fillA, ownerR, ownerG, ownerB, ownerA)
                        ? EffectivePalette.CapturedVanilla
                        : EffectivePalette.Custom;
                }
                else
                {
                    Color hovered = CapturedHoveredColor;
                    r = hovered.r;
                    g = hovered.g;
                    b = hovered.b;
                    outlineA = CapturedOutlineA;
                    fillA = CapturedFillA;
                    fillR = CapturedInnerColor.r;
                    fillG = CapturedInnerColor.g;
                    fillB = CapturedInnerColor.b;
                    ownerR = CapturedOwnerColor.r;
                    ownerG = CapturedOwnerColor.g;
                    ownerB = CapturedOwnerColor.b;
                    ownerA = CapturedOwnerColor.a;
                    palette = EffectivePalette.CapturedVanilla;
                }
            }
            else if (settings.ToolColorMode == HoverColorsSettings.kToolColorModeRecommended
                && activeTool == ToolKind.NetRoad)
            {
                Color hovered = CapturedHoveredColor;
                r = hovered.r;
                g = hovered.g;
                b = hovered.b;
                outlineA = Mathf.Min(CapturedOutlineA, kRoadRecommendedOutlineA);
                fillA = CapturedFillA;
                fillR = CapturedInnerColor.r;
                fillG = CapturedInnerColor.g;
                fillB = CapturedInnerColor.b;
                ownerR = CapturedOwnerColor.r;
                ownerG = CapturedOwnerColor.g;
                ownerB = CapturedOwnerColor.b;
                ownerA = Mathf.Min(CapturedOwnerColor.a, outlineA);
                palette = EffectivePalette.RecommendedNet;
            }
            else if (settings.ToolColorMode == HoverColorsSettings.kToolColorModeVanilla
                && (activeTool == ToolKind.Bulldoze || activeTool == ToolKind.NetRoad))
            {
                Color hovered = CapturedHoveredColor;
                r = hovered.r;
                g = hovered.g;
                b = hovered.b;
                outlineA = CapturedOutlineA;
                fillA = CapturedFillA;
                fillR = CapturedInnerColor.r;
                fillG = CapturedInnerColor.g;
                fillB = CapturedInnerColor.b;
                ownerR = CapturedOwnerColor.r;
                ownerG = CapturedOwnerColor.g;
                ownerB = CapturedOwnerColor.b;
                ownerA = CapturedOwnerColor.a;
                palette = EffectivePalette.CapturedVanilla;
            }
            else
            {
                r = settings.OutlineR;
                g = settings.OutlineG;
                b = settings.OutlineB;
                outlineA = settings.OutlineA;
                fillA = settings.FillA;
                fillR = settings.FillR;
                fillG = settings.FillG;
                fillB = settings.FillB;
                ownerR = settings.OwnerR;
                ownerG = settings.OwnerG;
                ownerB = settings.OwnerB;
                ownerA = settings.OwnerA;
                palette = MatchesCapturedVanillaProfile(r, g, b, outlineA, fillA, ownerR, ownerG, ownerB, ownerA)
                    ? EffectivePalette.CapturedVanilla
                    : EffectivePalette.Custom;
            }


            ApplyHoverToggle(
            settings,
            activeToolSystem,
            ref outlineA,
            ref fillA,
            ref ownerA,
            ref palette);

            // Two independent dirty states. m_Applied predates thickness and means only that the
            // effective color/palette landed; a shader with no _OutlineWidth must never leave the
            // color path looking dirty forever, and vice versa.
            bool colorsNeedApply = !m_Applied
                || r != m_LastR
                || g != m_LastG
                || b != m_LastB
                || outlineA != m_LastOutlineA
                || fillA != m_LastFillA
                || fillR != m_LastFillR
                || fillG != m_LastFillG
                || fillB != m_LastFillB
                || ownerR != m_LastOwnerR
                || ownerG != m_LastOwnerG
                || ownerB != m_LastOwnerB
                || ownerA != m_LastOwnerA
                || palette != m_LastPalette;

            bool thicknessNeedsApply = !m_OutlineThicknessApplied
                || !ApproximatelyEqual(thicknessScale, m_LastOutlineThicknessScale);

            if (!colorsNeedApply && !thicknessNeedsApply)
            {
                return;
            }

            if (colorsNeedApply)
            {
                bool ecsOk = ApplyRenderingSettingsColors(r, g, b, outlineA, ownerR, ownerG, ownerB, ownerA, palette);
                bool matOk = ApplyOutlineMaterialColors(r, g, b, outlineA, fillA, fillR, fillG, fillB, palette);

                // Only cache the snapshot when BOTH writes land - otherwise retry next frame.
                if (ecsOk && matOk)
                {
                    m_LastR = r;
                    m_LastG = g;
                    m_LastB = b;
                    m_LastOutlineA = outlineA;
                    m_LastFillA = fillA;
                    m_LastFillR = fillR;
                    m_LastFillG = fillG;
                    m_LastFillB = fillB;
                    m_LastOwnerR = ownerR;
                    m_LastOwnerG = ownerG;
                    m_LastOwnerB = ownerB;
                    m_LastOwnerA = ownerA;
                    m_LastPalette = palette;
                    m_Applied = true;
                }
            }

            // Snapshot the scale only when a write actually reached the shader, so an unresolved
            // material can never be remembered as "thickness applied".
            if (thicknessNeedsApply && ApplyOutlineWidth(thicknessScale))
            {
                m_LastOutlineThicknessScale = thicknessScale;
                m_OutlineThicknessApplied = true;
            }
        }

        // Unity's operator== reports a destroyed Material as null while the reference is still set.
        // Scene or render-pipeline reload therefore hands us a brand new material, and every
        // captured vanilla value belongs to the old one.
        private void InvalidateCacheIfMaterialDestroyed()
        {
            if (!m_OutlineMaterialResolved || m_OutlineMaterial != null)
            {
                return;
            }

            m_OutlineMaterialResolved = false;
            m_MaterialDefaultsCaptured = false;
            m_OutlineWidthCaptured = false;
            m_OutlineWidthMissing = false;
            m_OutlineThicknessApplied = false;

            // The instance HC wrote to is gone, so there is nothing left to restore on it.
            m_HasWrittenOutlineWidth = false;

            // Force the color path to rewrite once the replacement material has been captured.
            m_Applied = false;

            LogUtils.Info(() => $"{Mod.ModTag} Outline material was destroyed; re-acquiring and re-capturing vanilla values.");
        }

        private void TryCaptureVanillaDefaults()
        {
            if (!m_PrefabDefaultsCaptured && m_PrefabSystem != null
                && m_PrefabSystem.TryGetPrefab(m_RenderingSettingsPrefab, out PrefabBase prefab)
                && m_PrefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                && EntityManager.HasComponent<RenderingSettingsData>(prefabEntity))
            {
                RenderingSettingsData prefabData = EntityManager.GetComponentData<RenderingSettingsData>(prefabEntity);
                if (!m_RenderingDefaultsCaptured)
                {
                    CapturedHoveredColor = prefabData.m_HoveredColor;
                    CapturedOwnerColor = prefabData.m_OwnerColor;
                    CapturedWarningColor = prefabData.m_WarningColor;
                    CapturedErrorColor = prefabData.m_ErrorColor;
                    m_RenderingDefaultsCaptured = true;
                }

                m_PrefabDefaultsCaptured = true;
            }

            if (!m_RenderingDefaultsCaptured && !m_RenderSettingsQuery.IsEmptyIgnoreFilter)
            {
                Entity entity = m_RenderSettingsQuery.GetSingletonEntity();
                RenderingSettingsData data = EntityManager.GetComponentData<RenderingSettingsData>(entity);
                CapturedHoveredColor = data.m_HoveredColor;
                CapturedOwnerColor = data.m_OwnerColor;
                CapturedWarningColor = data.m_WarningColor;
                CapturedErrorColor = data.m_ErrorColor;
                m_RenderingDefaultsCaptured = true;
            }

            if (!m_MaterialDefaultsCaptured && TryResolveOutlineMaterial())
            {
                Color outer = m_OutlineMaterial!.GetColor("_OuterColor");
                Color inner = m_OutlineMaterial.GetColor("_InnerColor");
                CapturedOuterColor = outer;
                CapturedInnerColor = inner;
                CapturedOutlineA = outer.a;
                CapturedFillA = inner.a;
                m_MaterialDefaultsCaptured = true;
            }

            if (!m_OutlineWidthCaptured && !m_OutlineWidthMissing && TryResolveOutlineMaterial())
            {
                if (m_OutlineMaterial!.HasProperty(s_OutlineWidthProperty))
                {
                    CapturedOutlineWidth = m_OutlineMaterial.GetFloat(s_OutlineWidthProperty);
                    m_OutlineWidthCaptured = true;
                    LogUtils.Info(() => $"{Mod.ModTag} Captured vanilla outline width: {CapturedOutlineWidth:F3}");
                }
                else
                {
                    // Scoped to this material instance, not the session: a replacement material is
                    // checked again once InvalidateCacheIfMaterialDestroyed clears this.
                    m_OutlineWidthMissing = true;
                    LogUtils.WarnOnce(
                        "outline-width-property-missing",
                        () => $"{Mod.ModTag} Outline material has no _OutlineWidth; thickness control is disabled. Colors and opacity are unaffected.");
                }
            }

            if (!HasCapturedVanillaDefaults && m_RenderingDefaultsCaptured && m_MaterialDefaultsCaptured)
            {
                HasCapturedVanillaDefaults = true;
            }

            if (!m_CaptureLogged && HasCapturedVanillaDefaults)
            {
                m_CaptureLogged = true;
                LogUtils.Info(() =>
                    $"{Mod.ModTag} Captured vanilla render colors:\n" +
                    $"  Hovered RGBA = {FormatColor(CapturedHoveredColor)}\n" +
                    $"  Owner   RGBA = {FormatColor(CapturedOwnerColor)}\n" +
                    $"  Warning RGBA = {FormatColor(CapturedWarningColor)}\n" +
                    $"  Error   RGBA = {FormatColor(CapturedErrorColor)}\n" +
                    $"  Outer   RGBA = {FormatColor(CapturedOuterColor)}\n" +
                    $"  Inner   RGBA = {FormatColor(CapturedInnerColor)}");
            }
        }

        private static string FormatColor(Color color)
        {
            return $"({color.r:F3}, {color.g:F3}, {color.b:F3}, {color.a:F3})";
        }

        // ECS singleton: hovered + owner overlay color used by several vanilla render paths.
        // Building lots clamp this alpha internally, but area/surface borders read it directly,
        // so we forward OutlineA here to make extractor and painted-area borders respect the
        // same outline-opacity control as the main hover highlight.
        private bool ApplyRenderingSettingsColors(
            float r,
            float g,
            float b,
            float outlineA,
            float ownerR,
            float ownerG,
            float ownerB,
            float ownerA,
            EffectivePalette palette)
        {
            if (m_RenderSettingsQuery.IsEmptyIgnoreFilter)
            {
                return false;
            }

            Entity entity = m_RenderSettingsQuery.GetSingletonEntity();
            RenderingSettingsData data = EntityManager.GetComponentData<RenderingSettingsData>(entity);

            switch (palette)
            {
                case EffectivePalette.CapturedVanilla:
                    data.m_HoveredColor = CapturedHoveredColor;
                    data.m_OwnerColor = CapturedOwnerColor;
                    data.m_WarningColor = CapturedWarningColor;
                    data.m_ErrorColor = CapturedErrorColor;
                    break;
                case EffectivePalette.VanillaToolError:
                    // Blocking placement errors already carry Game.Tools.Error; vanilla render
                    // paths color those objects from m_ErrorColor. Keep the rest of the hover
                    // profile vanilla so the final salmon matches the game's own colors.
                    data.m_HoveredColor = CapturedHoveredColor;
                    data.m_OwnerColor = CapturedOwnerColor;
                    data.m_WarningColor = CapturedWarningColor;
                    data.m_ErrorColor = CapturedErrorColor;
                    break;
                case EffectivePalette.RecommendedBulldoze:
                    data.m_HoveredColor = CapturedWarningColor;
                    data.m_OwnerColor = CapturedWarningColor;
                    data.m_WarningColor = CapturedWarningColor;
                    data.m_ErrorColor = CapturedErrorColor;
                    break;
                case EffectivePalette.RecommendedNet:
                    Color hovered = CapturedHoveredColor;
                    hovered.a = outlineA;
                    Color owner = CapturedOwnerColor;
                    owner.a = Mathf.Min(owner.a, outlineA);
                    data.m_HoveredColor = hovered;
                    data.m_OwnerColor = owner;
                    break;
                default:
                    data.m_HoveredColor = new Color(r, g, b, outlineA);
                    data.m_OwnerColor = new Color(ownerR, ownerG, ownerB, ownerA);
                    break;
            }

            EntityManager.SetComponentData(entity, data);
            return true;
        }

        // HDRP material: keep vanilla material RGB as the neutral carrier.
        // Per-object hover/owner colors come from RenderingSettingsData.m_HoveredColor
        // and m_OwnerColor. Only the material alpha values are controlled here:
        //   _OuterColor.a = outlineA (halo edge opacity)
        //   _InnerColor.a = fillA    (fill overlay opacity inside the silhouette)
        private bool ApplyOutlineMaterialColors(
            float outlineA,
            float fillA,
            float fillR,
            float fillG,
            float fillB,
            EffectivePalette palette)
        {
            if (!TryResolveOutlineMaterial())
            {
                return false;
            }

            Color outer;
            Color inner;
            switch (palette)
            {
                case EffectivePalette.CapturedVanilla:
                    outer = CapturedOuterColor;
                    inner = CapturedInnerColor;
                    break;
                case EffectivePalette.VanillaToolError:
                    // Do not paint the material red here. Vanilla salmon comes from
                    // RenderingSettingsData.m_ErrorColor plus the normal outline material.
                    outer = CapturedOuterColor;
                    inner = CapturedInnerColor;
                    break;
                case EffectivePalette.RecommendedBulldoze:
                    outer = CapturedWarningColor;
                    inner = new Color(CapturedWarningColor.r, CapturedWarningColor.g, CapturedWarningColor.b, CapturedFillA);
                    break;
                case EffectivePalette.RecommendedNet:
                    outer = CapturedOuterColor;
                    outer.a = outlineA;
                    inner = CapturedInnerColor;
                    break;

                default:
                    outer = CapturedOuterColor;
                    outer.a = outlineA;

                    // White here reproduces the pre-tint behavior, so an untouched save is unchanged.
                    inner = new Color(fillR, fillG, fillB, fillA);
                    break;

            }

            m_OutlineMaterial!.SetColor("_OuterColor", outer);
            m_OutlineMaterial.SetColor("_InnerColor", inner);
            return true;
        }

        // Optional extra on the same cached material. Runs only when OnUpdate already decided
        // something changed, so this is not a per-frame reassert and does not fight other mods.
        private bool ApplyOutlineWidth(float thicknessScale)
        {
            // CapturedOutlineWidth is only trustworthy once it has been read off the material that
            // is live right now, so no width is written before that happens.
            if (!m_OutlineWidthCaptured || m_OutlineMaterial == null)
            {
                return false;
            }

            float desiredWidth = CapturedOutlineWidth * thicknessScale;
            m_OutlineMaterial.SetFloat(s_OutlineWidthProperty, desiredWidth);
            m_LastWrittenOutlineWidth = desiredWidth;
            m_HasWrittenOutlineWidth = true;
            return true;
        }

        // Only give the width back if it still holds the value HC last wrote. If another mod has
        // set it since, that value is newer and stays.
        private void RestoreOutlineWidth()
        {
            if (!m_HasWrittenOutlineWidth
                || !m_OutlineWidthCaptured
                || m_OutlineMaterial == null
                || !m_OutlineMaterial.HasProperty(s_OutlineWidthProperty))
            {
                return;
            }

            float current = m_OutlineMaterial.GetFloat(s_OutlineWidthProperty);
            if (ApproximatelyEqual(current, m_LastWrittenOutlineWidth))
            {
                m_OutlineMaterial.SetFloat(s_OutlineWidthProperty, CapturedOutlineWidth);
            }

            m_HasWrittenOutlineWidth = false;
        }

        private ToolKind GetActiveToolKind(ToolBaseSystem? tool)
        {
            if (tool == null)
            {
                return ToolKind.None;
            }

            if (tool is BulldozeToolSystem)
            {
                return ToolKind.Bulldoze;
            }

            if (tool is NetToolSystem netTool)
            {
                return GetNetToolKind(netTool);
            }

            // Better Bulldozer may still drive vanilla BulldozeToolSystem, but this keeps the
            // feature resilient if a tool wrapper becomes active instead.
            string typeName = tool.GetType().Name;
            if (typeName.IndexOf("Bulldoze", StringComparison.OrdinalIgnoreCase) >= 0
                || typeName.IndexOf("Bulldozer", StringComparison.OrdinalIgnoreCase) >= 0
                || SafeToolId(tool).IndexOf("Bulldoze", StringComparison.OrdinalIgnoreCase) >= 0
                || SafeToolId(tool).IndexOf("Bulldozer", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return ToolKind.Bulldoze;
            }

            // Mod Selection tools (e.g. AllSpeedLimits) own their own
            // multi-segment selection and should stay as high-visibility as the "road tools", so a
            // low-visibility player hover preset can't hide what is currently selected. Matched by
            // tool id, with the tool's namespace as backup so there is no hard reference to the other mod.
            // Mapping to NetRoad means: options menu, Recommended/Vanilla mode -> high-vis vanilla
            // cyan (the default), Custom mode -> the player's chosen color (power-user override).
            string toolId = SafeToolId(tool);

            // Parking Control mod's No Parking selector behaves like a road upgrade tool for colors.
            if (string.Equals(
                    toolId,
                    "ParkingControl.NoParking",
                    StringComparison.Ordinal))
            {
                return ToolKind.NetRoad;
            }

            // For compatibility with AllSpeedLimits mod
            if (string.Equals(toolId, "SpeedLimitTool", StringComparison.Ordinal)
                || (tool.GetType().Namespace?.StartsWith("RoadRailSpeeds", StringComparison.Ordinal) ?? false))
            {
                return ToolKind.NetRoad;
            }

            return ToolKind.None;
        }

        private ToolKind GetNetToolKind(NetToolSystem netTool)
        {
            if (m_PrefabSystem == null)
            {
                return ToolKind.NetRoad;
            }

            PrefabBase? selectedPrefab = netTool.GetPrefab();
            if (selectedPrefab == null || !m_PrefabSystem.TryGetEntity(selectedPrefab, out Entity prefabEntity))
            {
                return ToolKind.NetRoad;
            }

            if (EntityManager.HasComponent<RoadData>(prefabEntity))
            {
                return ToolKind.NetRoad;
            }

            // EDT fences/hedges/markings and similar detail tools enter through NetTool
            // as lanes or fence prefabs. To keep this check cheap: selected prefab only.
            if (selectedPrefab is NetLanePrefab
                || EntityManager.HasComponent<NetLaneData>(prefabEntity)
                || EntityManager.HasComponent<FenceData>(prefabEntity))
            {
                return ToolKind.NetDetailing;
            }

            return ToolKind.NetRoad;
        }

        private bool HasSupportedPlacementError(ToolBaseSystem? tool)
        {
            if (tool == null)
            {
                return false;
            }

            // Keep this option scoped to item/network placement. AreaTool uses Error for
            // extractor/district/surface limits too, and those should not become salmon.
            if (tool is not ObjectToolSystem && tool is not NetToolSystem)
            {
                return false;
            }

            if (s_ToolErrorQueryField == null)
            {
                LogUtils.WarnOnce(
                    "tool-error-query-field-missing",
                    () => $"{Mod.ModTag} Cannot preserve placement error color: ToolBaseSystem.m_ErrorQuery not found.");
                return false;
            }

            try
            {
                // Vanilla ToolBaseSystem.GetAllowApply() checks this same query. In these
                // tools it covers blocking placement errors, including Overlapping items.
                if (!ReferenceEquals(m_CachedErrorTool, tool))
                {
                    object? value = s_ToolErrorQueryField.GetValue(tool);
                    m_CachedErrorTool = tool;
                    m_HasCachedErrorQuery = value is EntityQuery;
                    if (m_HasCachedErrorQuery)
                    {
                        m_CachedErrorQuery = (EntityQuery)value!;
                    }
                }

                return m_HasCachedErrorQuery && !m_CachedErrorQuery.IsEmptyIgnoreFilter;
            }
            catch (Exception ex)
            {
                LogUtils.WarnOnce(
                    "tool-error-query-read-failed",
                    () => $"{Mod.ModTag} Cannot preserve placement error color: {ex.GetType().Name}: {ex.Message}",
                    ex);
                return false;
            }
        }

        private static string SafeToolId(ToolBaseSystem tool)
        {
            try
            {
                return tool.toolID ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool MatchesCapturedVanillaProfile(float r, float g, float b, float outlineA, float fillA)
        {
            return MatchesCapturedVanillaProfile(
                r,
                g,
                b,
                outlineA,
                fillA,
                CapturedOwnerColor.r,
                CapturedOwnerColor.g,
                CapturedOwnerColor.b,
                CapturedOwnerColor.a);
        }

        public static bool MatchesCapturedVanillaProfile(
            float r,
            float g,
            float b,
            float outlineA,
            float fillA,
            float ownerR,
            float ownerG,
            float ownerB,
            float ownerA)
        {
            return ApproximatelyEqual(r, CapturedHoveredColor.r)
                && ApproximatelyEqual(g, CapturedHoveredColor.g)
                && ApproximatelyEqual(b, CapturedHoveredColor.b)
                && ApproximatelyEqual(outlineA, CapturedOutlineA)
                && ApproximatelyEqual(fillA, CapturedFillA)
                && ApproximatelyEqual(ownerR, CapturedOwnerColor.r)
                && ApproximatelyEqual(ownerG, CapturedOwnerColor.g)
                && ApproximatelyEqual(ownerB, CapturedOwnerColor.b)
                && ApproximatelyEqual(ownerA, CapturedOwnerColor.a);
        }

        private static bool ApproximatelyEqual(float a, float b)
        {
            return Mathf.Abs(a - b) < 0.0005f;
        }

        // Locates the OutlinesWorldUIPass material once per scene and caches it.
        // Re-scans only when the cached Material is destroyed (Unity operator!= detects that).
        private bool TryResolveOutlineMaterial()
        {
            if (m_OutlineMaterial != null)
            {
                return true;
            }

            // Scene load can briefly run before outline pass exists. Throttle expensive
            // Unity object scan; once the material is found, a cached reference handles all
            // future frames until Unity destroys it on scene reload.
            float now = UnityEngine.Time.realtimeSinceStartup;
            if (now < m_NextMaterialResolveTime)
            {
                return false;
            }

            m_NextMaterialResolveTime = now + kMaterialResolveRetrySeconds;

            CustomPassVolume[] volumes = UnityEngine.Object.FindObjectsOfType<CustomPassVolume>();
            for (int i = 0; i < volumes.Length; i++)
            {
                CustomPassVolume volume = volumes[i];
                if (volume == null || volume.customPasses == null)
                {
                    continue;
                }

                for (int j = 0; j < volume.customPasses.Count; j++)
                {
                    if (volume.customPasses[j] is OutlinesWorldUIPass pass && pass.m_FullscreenOutline != null)
                    {
                        m_OutlineMaterial = pass.m_FullscreenOutline;
                        m_OutlineMaterialResolved = true;
                        LogUtils.Info(() => $"{Mod.ModTag} OutlinesWorldUIPass material cached");
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
