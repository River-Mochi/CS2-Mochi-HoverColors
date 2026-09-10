// <copyright file="Setting.Reset.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Settings/Setting.Reset.cs
// Purpose: Options UI action for restoring HC-controlled game visuals to vanilla.

namespace HoverColors.Settings
{
    using Game.Settings;

    using HoverColors.Systems;

    public partial class HoverColorsSettings
    {
        [SettingsUIButton]
        [SettingsUISection(Actions, kReset)]
        public bool ResetGameVisuals
        {
            set
            {
                if (!value)
                {
                    return;
                }

                ResetGameVisualsToVanilla();
            }
        }

        private void ResetGameVisualsToVanilla()
        {
            // Hover outline, owner highlight, fill, and thickness.
            UnityEngine.Color hovered = OutlineColorSystem.CapturedHoveredColor;
            UnityEngine.Color owner = OutlineColorSystem.CapturedOwnerColor;
            UnityEngine.Color inner = OutlineColorSystem.CapturedInnerColor;

            OutlineR = hovered.r;
            OutlineG = hovered.g;
            OutlineB = hovered.b;
            OutlineA = OutlineColorSystem.CapturedOutlineA;

            OwnerR = owner.r;
            OwnerG = owner.g;
            OwnerB = owner.b;
            OwnerA = owner.a;

            FillR = inner.r;
            FillG = inner.g;
            FillB = inner.b;
            FillA = OutlineColorSystem.CapturedFillA;
            FillColorInitialized = true;

            OutlineThicknessScale = kDefaultOutlineThicknessScale;
            OutlineThicknessInitialized = true;

            // Global Eye toggle: vanilla behavior is normal hover highlights visible.
            HoverHighlightsSuppressed = false;

            // Tool-specific colors back to normal game behavior.
            ToolColorMode = kToolColorModeVanilla;
            UseOverlapWarningColor = true;
            UseCustomColorsForNetLanes = false;

            // Guidelines: vanilla colors and full vanilla opacity.
            UnityEngine.Color guidelineLines =
                GuidelineColorSystem.CapturedVanillaGuidelineLinesColor;

            UnityEngine.Color guidelinePreview =
                GuidelineColorSystem.CapturedVanillaGuidelinePreviewColor;

            UnityEngine.Color guidelineDashed =
                GuidelineColorSystem.CapturedVanillaGuidelineDashedColor;

            GuidelineLinesColorPreset = kGuidelineColorPresetVanilla;
            GuidelineLinesR = guidelineLines.r;
            GuidelineLinesG = guidelineLines.g;
            GuidelineLinesB = guidelineLines.b;
            GuidelineLinesA = 1f;

            GuidelinePreviewColorPreset = kGuidelineColorPresetVanilla;
            GuidelinePreviewR = guidelinePreview.r;
            GuidelinePreviewG = guidelinePreview.g;
            GuidelinePreviewB = guidelinePreview.b;
            GuidelinePreviewA = 1f;

            GuidelineDashedColorPreset = kGuidelineDashedColorPresetVanilla;
            GuidelineDashedR = guidelineDashed.r;
            GuidelineDashedG = guidelineDashed.g;
            GuidelineDashedB = guidelineDashed.b;

            // GuidelineColorSystem multiplies the captured vanilla alpha by this percentage,
            // so 100 restores the actual vanilla alpha rather than HC's 30% default.
            GuidelineOpacityPercent = 100;

            // A full reset intentionally discards the temporary guideline-toggle backup.
            GuidelineVanillaToggleActive = false;
            GuidelineVanillaToggleHasBackup = false;

            // District override off. DistrictColorSystem restores its captured vanilla colors.
            UnityEngine.Color district = DistrictColorSystem.CapturedDistrictFillColor;

            DistrictColorEnabled = false;
            DistrictR = district.r;
            DistrictG = district.g;
            DistrictB = district.b;
            DistrictA = district.a;

            // Vanilla Area-tool behavior shows these preview areas.
            SurfaceToolAreasSuppressed = false;
            SpecializedIndustryAreasSuppressed = false;
            SpecializedIndustryAreasSuppressionInitialized = true;

            // These two switches live outside the settings values, so update them immediately too.
            AreaToolOverlaySystem.SetSurfaceSuppression(false);
            AreaToolOverlaySystem.SetSpecializedIndustrySuppression(false);

            ApplyAndSave();
        }
    }
}
