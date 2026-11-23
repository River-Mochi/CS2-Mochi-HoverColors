// <copyright file="HoverColorsUISystem.MapBoundaries.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/UI/HoverColorsUISystem.MapBoundaries.cs
// In-city boundary bindings and actions; completely separate from District colors.

namespace HoverColors.UI
{
    using Colossal.UI.Binding;
    using HoverColors.Systems;
    using UnityEngine;

    public partial class HoverColorsUISystem
    {
        private ValueBinding<float> m_MapBoundaryTileRBinding = null!;
        private ValueBinding<float> m_MapBoundaryTileGBinding = null!;
        private ValueBinding<float> m_MapBoundaryTileBBinding = null!;
        private ValueBinding<float> m_MapBoundaryTileABinding = null!;
        private ValueBinding<float> m_MapBoundaryMapRBinding = null!;
        private ValueBinding<float> m_MapBoundaryMapGBinding = null!;
        private ValueBinding<float> m_MapBoundaryMapBBinding = null!;
        private ValueBinding<float> m_MapBoundaryMapABinding = null!;
        private ValueBinding<bool> m_MapBoundaryTileVisibleBinding = null!;
        private ValueBinding<bool> m_MapBoundaryMapVisibleBinding = null!;
        private ValueBinding<float> m_MapBoundaryWidthBinding = null!;
        private ValueBinding<float> m_MapBoundaryDashScaleBinding = null!;

        private void RegisterMapBoundaryValueBindings(HoverColorsSettings? settings)
        {
            Color tile = MapBoundaryColorSystem.GetTileColor(settings);
            Color map = MapBoundaryColorSystem.GetMapColor(settings);
            m_MapBoundaryTileRBinding = AddValueBinding("MapBoundaryTileR", tile.r);
            m_MapBoundaryTileGBinding = AddValueBinding("MapBoundaryTileG", tile.g);
            m_MapBoundaryTileBBinding = AddValueBinding("MapBoundaryTileB", tile.b);
            m_MapBoundaryTileABinding = AddValueBinding("MapBoundaryTileA", tile.a);
            m_MapBoundaryMapRBinding = AddValueBinding("MapBoundaryMapR", map.r);
            m_MapBoundaryMapGBinding = AddValueBinding("MapBoundaryMapG", map.g);
            m_MapBoundaryMapBBinding = AddValueBinding("MapBoundaryMapB", map.b);
            m_MapBoundaryMapABinding = AddValueBinding("MapBoundaryMapA", map.a);
            m_MapBoundaryTileVisibleBinding = AddValueBinding("MapBoundaryTileVisible", settings?.MapBoundaryTileVisible ?? true);
            m_MapBoundaryMapVisibleBinding = AddValueBinding("MapBoundaryMapVisible", settings?.MapBoundaryMapVisible ?? true);
            m_MapBoundaryWidthBinding = AddValueBinding("MapBoundaryWidth", MapBoundaryColorSystem.GetWidth(settings));
            m_MapBoundaryDashScaleBinding = AddValueBinding("MapBoundaryDashScale", settings?.MapBoundaryDashScale ?? 1f);
        }

        private void SyncMapBoundaryValueBindings(HoverColorsSettings? settings)
        {
            Color tile = MapBoundaryColorSystem.GetTileColor(settings);
            Color map = MapBoundaryColorSystem.GetMapColor(settings);
            UpdateIfChanged(m_MapBoundaryTileRBinding, tile.r);
            UpdateIfChanged(m_MapBoundaryTileGBinding, tile.g);
            UpdateIfChanged(m_MapBoundaryTileBBinding, tile.b);
            UpdateIfChanged(m_MapBoundaryTileABinding, tile.a);
            UpdateIfChanged(m_MapBoundaryMapRBinding, map.r);
            UpdateIfChanged(m_MapBoundaryMapGBinding, map.g);
            UpdateIfChanged(m_MapBoundaryMapBBinding, map.b);
            UpdateIfChanged(m_MapBoundaryMapABinding, map.a);
            UpdateIfChanged(m_MapBoundaryTileVisibleBinding, settings?.MapBoundaryTileVisible ?? true);
            UpdateIfChanged(m_MapBoundaryMapVisibleBinding, settings?.MapBoundaryMapVisible ?? true);
            UpdateIfChanged(m_MapBoundaryWidthBinding, MapBoundaryColorSystem.GetWidth(settings));
            UpdateIfChanged(m_MapBoundaryDashScaleBinding, settings?.MapBoundaryDashScale ?? 1f);
        }

        private void RegisterMapBoundaryTriggerBindings()
        {
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetMapBoundaryTileVisible", SetMapBoundaryTileVisible));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetMapBoundaryMapVisible", SetMapBoundaryMapVisible));
            AddBinding(new TriggerBinding(Mod.ModId, "ToggleMapBoundaryVisibility", ToggleMapBoundaryVisibility));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetMapBoundaryTileColor", SetMapBoundaryTileColor));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetMapBoundaryMapColor", SetMapBoundaryMapColor));
            AddBinding(new TriggerBinding<float>(Mod.ModId, "SetMapBoundaryWidth", SetMapBoundaryWidth));
            AddBinding(new TriggerBinding<float>(Mod.ModId, "SetMapBoundaryDashScale", SetMapBoundaryDashScale));
            AddBinding(new TriggerBinding(Mod.ModId, "ResetMapBoundary", ResetMapBoundary));
        }

        private void SetMapBoundaryTileVisible(bool visible)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || settings.MapBoundaryTileVisible == visible) return;
            settings.MapBoundaryTileVisible = visible;
            ApplySaveAndSync(settings);
        }

        private void SetMapBoundaryMapVisible(bool visible)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || settings.MapBoundaryMapVisible == visible) return;
            settings.MapBoundaryMapVisible = visible;
            ApplySaveAndSync(settings);
        }

        private void ToggleMapBoundaryVisibility()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            bool visible = !settings.MapBoundaryTileVisible || !settings.MapBoundaryMapVisible;
            settings.MapBoundaryTileVisible = visible;
            settings.MapBoundaryMapVisible = visible;
            ApplySaveAndSync(settings);
        }

        private void SetMapBoundaryTileColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            r = Clamp01(r); g = Clamp01(g); b = Clamp01(b); a = Clamp01(a);
            if (settings.MapBoundaryTileCustom &&
                SameColor(settings.MapBoundaryTileR, settings.MapBoundaryTileG, settings.MapBoundaryTileB,
                    settings.MapBoundaryTileA, r, g, b, a)) return;
            settings.MapBoundaryTileCustom = true;
            settings.MapBoundaryTileR = r;
            settings.MapBoundaryTileG = g;
            settings.MapBoundaryTileB = b;
            settings.MapBoundaryTileA = a;
            ApplySaveAndSync(settings);
        }

        private void SetMapBoundaryMapColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            r = Clamp01(r); g = Clamp01(g); b = Clamp01(b); a = Clamp01(a);
            if (settings.MapBoundaryMapCustom &&
                SameColor(settings.MapBoundaryMapR, settings.MapBoundaryMapG, settings.MapBoundaryMapB,
                    settings.MapBoundaryMapA, r, g, b, a)) return;
            settings.MapBoundaryMapCustom = true;
            settings.MapBoundaryMapR = r;
            settings.MapBoundaryMapG = g;
            settings.MapBoundaryMapB = b;
            settings.MapBoundaryMapA = a;
            ApplySaveAndSync(settings);
        }

        private void SetMapBoundaryWidth(float width)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || float.IsNaN(width) || float.IsInfinity(width)) return;

            // Width is in actual game units (1–200), not a multiple of vanilla.
            float desiredWidth = Mathf.Round(Mathf.Clamp(width, 1f, MapBoundaryColorSystem.MaxBoundaryWidth));
            float baseWidth = Mathf.Max(0.01f, MapBoundaryColorSystem.CapturedWidth);
            float scale = desiredWidth / baseWidth;
            if (Mathf.Approximately(settings.MapBoundaryWidthScale, scale)) return;
            settings.MapBoundaryWidthScale = scale;
            ApplySaveAndSync(settings);
        }

        private void SetMapBoundaryDashScale(float scale)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            scale = Mathf.Round(Mathf.Clamp(scale, 0.5f, 2f) * 10f) / 10f;
            if (Mathf.Approximately(settings.MapBoundaryDashScale, scale)) return;
            settings.MapBoundaryDashScale = scale;
            ApplySaveAndSync(settings);
        }

        private void ResetMapBoundary()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            settings.ResetMapBoundarySettings();
            ApplySaveAndSync(settings);
        }
    }
}
