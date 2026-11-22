// <copyright file="Setting.MapBoundaries.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Settings/Setting.MapBoundaries.cs
// Persistent map-border preferences are independent of District colors and hover presets.
// A non-custom color always means the exact vanilla color captured from the live prefab.

namespace HoverColors
{
    using Game.Settings;
    using HoverColors.Systems;
    using UnityEngine;

    public partial class HoverColorsSettings
    {
        [SettingsUIHidden]
        public bool MapBoundaryTileVisible { get; set; } = true;

        [SettingsUIHidden]
        public bool MapBoundaryMapVisible { get; set; } = true;

        [SettingsUIHidden]
        public bool MapBoundaryTileCustom { get; set; }

        [SettingsUIHidden]
        public bool MapBoundaryMapCustom { get; set; }

        [SettingsUIHidden]
        public float MapBoundaryTileR { get; set; } = 1f;

        [SettingsUIHidden]
        public float MapBoundaryTileG { get; set; } = 1f;

        [SettingsUIHidden]
        public float MapBoundaryTileB { get; set; } = 1f;

        [SettingsUIHidden]
        public float MapBoundaryTileA { get; set; } = 0.75f;

        [SettingsUIHidden]
        public float MapBoundaryMapR { get; set; } = 1f;

        [SettingsUIHidden]
        public float MapBoundaryMapG { get; set; } = 1f;

        [SettingsUIHidden]
        public float MapBoundaryMapB { get; set; } = 1f;

        [SettingsUIHidden]
        public float MapBoundaryMapA { get; set; } = 0.25f;

        // Multiplier on the actual prefab width/tiling length; 1 = exact game default.
        [SettingsUIHidden]
        public float MapBoundaryWidthScale { get; set; } = 1f;

        [SettingsUIHidden]
        public float MapBoundaryDashScale { get; set; } = 1f;

        public void ResetMapBoundarySettings()
        {
            MapBoundaryTileVisible = true;
            MapBoundaryMapVisible = true;
            MapBoundaryTileCustom = false;
            MapBoundaryMapCustom = false;
            Color tile = MapBoundaryColorSystem.CapturedTileColor;
            Color map = MapBoundaryColorSystem.CapturedMapColor;
            MapBoundaryTileR = tile.r;
            MapBoundaryTileG = tile.g;
            MapBoundaryTileB = tile.b;
            MapBoundaryTileA = tile.a;
            MapBoundaryMapR = map.r;
            MapBoundaryMapG = map.g;
            MapBoundaryMapB = map.b;
            MapBoundaryMapA = map.a;
            MapBoundaryWidthScale = 1f;
            MapBoundaryDashScale = 1f;
        }
    }
}
