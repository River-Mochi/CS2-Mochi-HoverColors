// <copyright file="Setting.Defaults.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Settings/Setting.Defaults.cs
// Purpose: Defaults and one-time migration helpers for HoverColorsSettings.

namespace HoverColors.Settings
{
    public partial class HoverColorsSettings
    {
        public override void SetDefaults()
        {
            // Vanilla cyan-blue from the OutlinesWorldUIPass material defaults.
            OutlineR = 0.502f;
            OutlineG = 0.869f;
            OutlineB = 1f;
            OutlineA = 0.855f;

            // Vanilla parent/owner green used for sub-building placement and owned objects.
            OwnerR = 0.247f;
            OwnerG = 0.981f;
            OwnerB = 0.247f;
            OwnerA = 0.702f;

            // FillA=0 matches vanilla CS2: no extra silhouette overlay until the player turns it up.
            FillA = 0f;

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

            Preset2R = kPresetA2R;
            Preset2G = kPresetA2G;
            Preset2B = kPresetA2B;
            Preset2A = kPresetA2A;
            Preset2FillA = kPresetA2FillA;

            // Set B: P1 = soft white, P2 = original dark purple-gray.
            PresetAlt1R = kPresetB1R;
            PresetAlt1G = kPresetB1G;
            PresetAlt1B = kPresetB1B;
            PresetAlt1A = kPresetB1A;
            PresetAlt1FillA = kPresetB1FillA;

            PresetAlt2R = kPresetB2R;
            PresetAlt2G = kPresetB2G;
            PresetAlt2B = kPresetB2B;
            PresetAlt2A = kPresetB2A;
            PresetAlt2FillA = kPresetB2FillA;

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
            UseDarkerPanel = false;
            PanelCollapsed = false;

            // 100 = vanilla default. Lower = more transparent guidelines.
            GuidelineOpacityPercent = kDefaultGuidelineOpacityPercent;
        }

        public void MigrateAfterLoad()
        {
            bool changed = false;

            if (!SpecializedIndustryAreasSuppressionInitialized)
            {
                SpecializedIndustryAreasSuppressed = true;
                SpecializedIndustryAreasSuppressionInitialized = true;
                changed = true;
            }

            if (!PresetSetsInitialized)
            {
                // Preserve existing player P1/P2 as Set A. Only initialize Set B.
                ActivePresetSet = kPresetSetA;

                PresetAlt1R = kPresetB1R;
                PresetAlt1G = kPresetB1G;
                PresetAlt1B = kPresetB1B;
                PresetAlt1A = kPresetB1A;
                PresetAlt1FillA = kPresetB1FillA;
                PresetAlt1GuidelinePercent = kDefaultGuidelineOpacityPercent;

                PresetAlt2R = kPresetB2R;
                PresetAlt2G = kPresetB2G;
                PresetAlt2B = kPresetB2B;
                PresetAlt2A = kPresetB2A;
                PresetAlt2FillA = kPresetB2FillA;
                PresetAlt2GuidelinePercent = kDefaultGuidelineOpacityPercent;

                PresetSetsInitialized = true;
                changed = true;
            }

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
