// <copyright file="Setting.RoadEngineer.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

namespace HoverColors
{
    using Game.Settings;
    using Game.UI;

    public partial class HoverColorsSettings
    {
        internal const float kDefaultRoadBadgeR = 8f / 255f;
        internal const float kDefaultRoadBadgeG = 41f / 255f;
        internal const float kDefaultRoadBadgeB = 2f / 255f;

        [SettingsUIHidden]
        public float RoadBadgeR { get; set; } = kDefaultRoadBadgeR;
        [SettingsUIHidden]
        public float RoadBadgeG { get; set; } = kDefaultRoadBadgeG;
        [SettingsUIHidden]
        public float RoadBadgeB { get; set; } = kDefaultRoadBadgeB;
        [SettingsUIHidden]
        public float RoadBadgeA { get; set; } = 1f;
        [SettingsUIHidden]
        public bool RoadBadgeNativeBackground { get; set; }
        [SettingsUIHidden]
        public bool RoadBadgeCustomText { get; set; } = true;
        [SettingsUIHidden]
        public float RoadBadgeTextR { get; set; } = 1f;
        [SettingsUIHidden]
        public float RoadBadgeTextG { get; set; } = 235f / 255f;
        [SettingsUIHidden]
        public float RoadBadgeTextB { get; set; } = 128f / 255f;
        [SettingsUIHidden]
        public bool RoadEngineerUseUnits { get; set; }
        [SettingsUIHidden]
        public int RoadDistanceDecimals { get; set; } = 2;
        [SettingsUIHidden]
        public int RoadAngleDecimals { get; set; } = 2;
        [SettingsUIHidden]
        public bool RoadPreciseFloatingLabels { get; set; }
        [SettingsUIHidden]
        public int RoadLabelFontPercent { get; set; } = 100;

        internal static int NormalizeRoadLabelFontPercent(int value)
        {
            return value >= 120 ? 120 : value >= 110 ? 110 : 100;
        }

        [SettingsUISection(Actions, kPanel)]
        public bool RoadEngineerEnabled { get; set; } = true;

        [SettingsUISection(Actions, kPanel)]
        public bool RoadEngineerMatchHC { get; set; }

        [SettingsUISection(Actions, kPanel)]
        [SettingsUISlider(min = 60, max = 100, step = 5, scalarMultiplier = 1, unit = Unit.kPercentage)]
        public int RoadEngineerPanelOpacity { get; set; } = 100;

        [SettingsUIHidden]
        public bool RoadEngineerPositionSet { get; set; }

        [SettingsUIHidden]
        public int RoadEngineerOffsetX { get; set; }

        [SettingsUIHidden]
        public int RoadEngineerOffsetY { get; set; }

        internal void ResetRoadEngineerAppearance()
        {
            RoadBadgeR = kDefaultRoadBadgeR;
            RoadBadgeG = kDefaultRoadBadgeG;
            RoadBadgeB = kDefaultRoadBadgeB;
            RoadBadgeA = 1f;
            RoadBadgeNativeBackground = false;
            RoadBadgeCustomText = true;
            RoadBadgeTextR = 1f;
            RoadBadgeTextG = 235f / 255f;
            RoadBadgeTextB = 128f / 255f;
            RoadEngineerUseUnits = false;
            RoadDistanceDecimals = RoadAngleDecimals = 2;
            RoadPreciseFloatingLabels = false;
            RoadLabelFontPercent = 100;
            RoadEngineerEnabled = true;
            RoadEngineerMatchHC = false;
            RoadEngineerPanelOpacity = 100;
            RoadEngineerPositionSet = false;
            RoadEngineerOffsetX = 0;
            RoadEngineerOffsetY = 0;
        }
    }
}
