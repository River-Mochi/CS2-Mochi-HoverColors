// <copyright file="Setting.Defaults.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
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
            OutlineThicknessScale = kDefaultOutlineThicknessScale;
            OutlineThicknessInitialized = true;

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

            // Without this an upgrading player's fill would go black the moment they raise the slider.
            if (!FillColorInitialized)
            {
                FillR = 1f;
                FillG = 1f;
                FillB = 1f;

                Preset1FillR = 1f; Preset1FillG = 1f; Preset1FillB = 1f;
                Preset2FillR = 1f; Preset2FillG = 1f; Preset2FillB = 1f;
                PresetAlt1FillR = 1f; PresetAlt1FillG = 1f; PresetAlt1FillB = 1f;
                PresetAlt2FillR = 1f; PresetAlt2FillG = 1f; PresetAlt2FillB = 1f;

                PresetDefaultsBackup1FillR = 1f; PresetDefaultsBackup1FillG = 1f; PresetDefaultsBackup1FillB = 1f;
                PresetDefaultsBackup2FillR = 1f; PresetDefaultsBackup2FillG = 1f; PresetDefaultsBackup2FillB = 1f;
                PresetDefaultsBackupAlt1FillR = 1f; PresetDefaultsBackupAlt1FillG = 1f; PresetDefaultsBackupAlt1FillB = 1f;
                PresetDefaultsBackupAlt2FillR = 1f; PresetDefaultsBackupAlt2FillG = 1f; PresetDefaultsBackupAlt2FillB = 1f;

                FillColorInitialized = true;
                changed = true;
            }

            if (!OutlineThicknessInitialized)
            {
                OutlineThicknessScale = kDefaultOutlineThicknessScale;
                OutlineThicknessInitialized = true;
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
                PresetAlt1FillR = 1f;
                PresetAlt1FillG = 1f;
                PresetAlt1FillB = 1f;
                PresetAlt1GuidelinePercent = kDefaultGuidelineOpacityPercent;

                PresetAlt2R = kPresetB2R;
                PresetAlt2G = kPresetB2G;
                PresetAlt2B = kPresetB2B;
                PresetAlt2A = kPresetB2A;
                PresetAlt2FillA = kPresetB2FillA;
                PresetAlt2FillR = 1f;
                PresetAlt2FillG = 1f;
                PresetAlt2FillB = 1f;
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
