// <copyright file="OutlineColorSystem.Material.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Rendering/OutlineColorSystem.Material.cs
// Purpose: Captures vanilla rendering defaults and applies ECS/material color, alpha, and width values.

namespace HoverColors.Systems
{
    using CS2Shared.RiverMochi;
    using Game.Prefabs;
    using Game.Rendering;
    using Unity.Entities;
    using UnityEngine;
    using UnityEngine.Rendering.HighDefinition;

    public partial class OutlineColorSystem
    {
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
