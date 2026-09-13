// <copyright file="Setting.Defaults.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Settings/Setting.Defaults.cs
// Purpose: Defaults and one-time migration helpers for HoverColorsSettings.

namespace HoverColors
{
    public partial class HoverColorsSettings
    {
        public override void SetDefaults()
        {
            // New installs start on HC's Set A / P1 instead of vanilla cyan-blue.
            // Existing users keep their saved live color when their .coc loads.
            OutlineR = kPresetA1R;
            OutlineG = kPresetA1G;
            OutlineB = kPresetA1B;
            OutlineA = kPresetA1A;

            // Vanilla parent/owner green used for sub-building placement and owned objects.
            OwnerR = 0.247f;
            OwnerG = 0.981f;
            OwnerB = 0.247f;
            OwnerA = 0.702f;

            // FillA=0 matches vanilla CS2: no extra silhouette overlay until the player turns it up.
            FillA = 0f;

            // White is the neutral tint: the fill renders in the Outline color, same as before the
            // fill had its own swatch. Matches the vanilla material _InnerColor RGB.
            FillR = 1f;
            FillG = 1f;
            FillB = 1f;
            FillColorInitialized = true;

            // 1.0 = the vanilla shader width captured at runtime, whatever that build's value is.
            OutlineThicknessScale = kModDefaultOutlineThicknessScale;
            OutlineThicknessInitialized = true;

            PanelOpacityPercent = kDefaultPanelOpacityPercent;

            // Safe fallback for the District picker until DistrictColorSystem captures the authored
            // default district prefab colors. Not applied unless DistrictColorEnabled is true.
            DistrictColorEnabled = false;
            DistrictR = 128f / 255f;
            DistrictG = 128f / 255f;
            DistrictB = 128f / 255f;
            DistrictA = 64f / 255f;

            // Starter presets. Players can overwrite P1/P2 with the panel's Save button.
            // Set A is visible first. Reset switches the panel to Set B.
            // Existing users keep their old P1/P2 as Set A during migration.
            ActivePresetSet = kPresetSetA;
            PresetSetsInitialized = true;

            // Set A: P1 = off-white D7E2C2, P2 = original light gray-purple.
            Preset1R = kPresetA1R;
            Preset1G = kPresetA1G;
            Preset1B = kPresetA1B;
            Preset1A = kPresetA1A;
            Preset1FillA = kPresetA1FillA;
            Preset1FillR = 1f;
            Preset1FillG = 1f;
            Preset1FillB = 1f;

            Preset2R = kPresetA2R;
            Preset2G = kPresetA2G;
            Preset2B = kPresetA2B;
            Preset2A = kPresetA2A;
            Preset2FillA = kPresetA2FillA;
            Preset2FillR = 1f;
            Preset2FillG = 1f;
            Preset2FillB = 1f;

            // Set B: P1 = soft white, P2 = original dark purple-gray.
            PresetAlt1R = kPresetB1R;
            PresetAlt1G = kPresetB1G;
            PresetAlt1B = kPresetB1B;
            PresetAlt1A = kPresetB1A;
            PresetAlt1FillA = kPresetB1FillA;
            PresetAlt1FillR = 1f;
            PresetAlt1FillG = 1f;
            PresetAlt1FillB = 1f;

            PresetAlt2R = kPresetB2R;
            PresetAlt2G = kPresetB2G;
            PresetAlt2B = kPresetB2B;
            PresetAlt2A = kPresetB2A;
            PresetAlt2FillA = kPresetB2FillA;
            PresetAlt2FillR = 1f;
            PresetAlt2FillG = 1f;
            PresetAlt2FillB = 1f;

            Preset1GuidelinePercent = kDefaultGuidelineOpacityPercent;
            Preset2GuidelinePercent = kDefaultGuidelineOpacityPercent;
            PresetAlt1GuidelinePercent = kDefaultGuidelineOpacityPercent;
            PresetAlt2GuidelinePercent = kDefaultGuidelineOpacityPercent;


            PresetDefaultsToggleActive = false;
            PresetDefaultsToggleHasBackup = false;
            PresetDefaultsBackupActiveSet = kPresetSetA;

            GuidelineDefaultPercent = kDefaultGuidelineOpacityPercent;

            GuidelineLinesColorPreset = kGuidelineColorPresetVanilla;
            GuidelineLinesR = 0.7f;
            GuidelineLinesG = 0.7f;
            GuidelineLinesB = 1f;
            GuidelineLinesA = 1f;

            GuidelinePreviewColorPreset = kGuidelineColorPresetVanilla;
            GuidelinePreviewR = 0.7f;
            GuidelinePreviewG = 0.7f;
            GuidelinePreviewB = 1f;
            GuidelinePreviewA = 1f;

            GuidelineDashedColorPreset = kGuidelineDashedColorPresetVanilla;
            GuidelineDashedR = 0.7f;
            GuidelineDashedG = 0.7f;
            GuidelineDashedB = 1f;

            GuidelineVanillaToggleActive = false;
            GuidelineVanillaToggleHasBackup = false;
            GuidelineBackupLinesColorPreset = kGuidelineColorPresetVanilla;
            GuidelineBackupLinesR = GuidelineLinesR;
            GuidelineBackupLinesG = GuidelineLinesG;
            GuidelineBackupLinesB = GuidelineLinesB;
            GuidelineBackupLinesA = GuidelineLinesA;
            GuidelineBackupPreviewColorPreset = kGuidelineColorPresetVanilla;
            GuidelineBackupPreviewR = GuidelinePreviewR;
            GuidelineBackupPreviewG = GuidelinePreviewG;
            GuidelineBackupPreviewB = GuidelinePreviewB;
            GuidelineBackupPreviewA = GuidelinePreviewA;

            PanelTooltipsEnabled = true;
            SurfaceToolAreasSuppressed = true;
            SpecializedIndustryAreasSuppressed = true;
            SpecializedIndustryAreasSuppressionInitialized = true;

            // Release default: help players see demolition/road targets even if their custom
            // alpha is very low, without changing their saved custom color.
            ToolColorMode = kToolColorModeRecommended;
            UseOverlapWarningColor = true;
            UseCustomColorsForNetLanes = true;
            // Dark for fresh installs and for Reset to Mod Defaults: it is the game's own panel
            // surface, so it matches whatever UI skin the player already runs. The setter keeps the
            // legacy UseDarkerPanel field in step.
            PanelStyle = kPanelStyleDark;
            PanelStyleInitialized = true;
            PanelCollapsed = false;

            // 100 = vanilla default. Lower = more transparent guidelines.
            GuidelineOpacityPercent = kDefaultGuidelineOpacityPercent;
        }

        /// <summary>
        /// Repairs saved preset values that fall outside the range the code expects, so a corrupt or
        /// hand-edited .coc cannot leave the preset state inconsistent. Runs once per load.
        /// </summary>
        public void SanitizeAfterLoad()
        {
            bool changed = false;

            // Belt and braces, not a safety net. Every consumer of these values already fails safe:
            // ActivePresetSet and PresetDefaultsBackupActiveSet are only ever read as binary
            // "== kPresetSetB" tests, never as indexes, so an out-of-range value already behaves as
            // Set A; and the restore path already requires ToggleActive && HasBackup together.
            //
            // What they add is correcting the saved file itself, which a player can edit by hand.
            // Removal candidate.
            if (ActivePresetSet != kPresetSetA && ActivePresetSet != kPresetSetB)
            {
                ActivePresetSet = kPresetSetA;
                changed = true;
            }


            if (PresetDefaultsToggleActive && !PresetDefaultsToggleHasBackup)
            {
                PresetDefaultsToggleActive = false;
                changed = true;
            }

            if (PresetDefaultsBackupActiveSet != kPresetSetA && PresetDefaultsBackupActiveSet != kPresetSetB)
            {
                PresetDefaultsBackupActiveSet = kPresetSetA;
                changed = true;
            }

            if (changed)
            {
                ApplyAndSave();
            }
        }

    }
}
