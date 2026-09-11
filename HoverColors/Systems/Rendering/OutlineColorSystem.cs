// <copyright file="OutlineColorSystem.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Rendering/OutlineColorSystem.cs
// Purpose: Coordinates effective hover/owner/fill colors and outline thickness.
// Material capture/writes are in OutlineColorSystem.Material.cs.
// Tool classification/error handling is in OutlineColorSystem.Tools.cs.
// Hover-hide behavior is in OutlineColorSystem.HoverToggle.cs.

namespace HoverColors.Systems
{
    using System.Reflection;
    using Game;
    using Game.Prefabs;
    using Game.Rendering;
    using Game.Tools;
    using Unity.Entities;
    using UnityEngine;

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
                bool matOk = ApplyOutlineMaterialColors(outlineA, fillA, fillR, fillG, fillB, palette);

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

    }
}
