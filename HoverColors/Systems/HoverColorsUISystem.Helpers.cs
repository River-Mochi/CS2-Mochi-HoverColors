// <copyright file="HoverColorsUISystem.Helpers.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/HoverColorsUISystem.Helpers.cs
// Purpose: Shared comparison and preset/guideline helper methods for HoverColorsUISystem.

namespace HoverColors.UI
{
    using System;
    using HoverColors.Localization;
    using HoverColors.Settings;
    using HoverColors.Systems;

    public partial class HoverColorsUISystem
    {
        private void ApplySaveAndSync(HoverColorsSettings settings)
        {
            settings.ApplyAndSave();
            SyncValueBindings();
        }

        private static bool IsVanillaOutlineActive()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null)
            {
                return false;
            }

            UnityEngine.Color hovered = OutlineColorSystem.CapturedHoveredColor;
            UnityEngine.Color owner = OutlineColorSystem.CapturedOwnerColor;
            return SameColor(settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA,
                    hovered.r, hovered.g, hovered.b, OutlineColorSystem.CapturedOutlineA)
                && SameColor(settings.OwnerR, settings.OwnerG, settings.OwnerB, settings.OwnerA,
                    owner.r, owner.g, owner.b, owner.a);
        }

        // True when the live swatch exactly matches what's stored in that slot.
        private static bool IsPresetActive(int slot)
        {
            HoverColorsSettings? s = Mod.Settings;
            if (s == null) return false;

            if (!TryGetPresetTargets(
                    s,
                    slot,
                    out float targetR,
                    out float targetG,
                    out float targetB,
                    out float targetA,
                    out float targetFillA,
                    out int _))
            {
                return false;
            }

            return SameColor(s.OutlineR, s.OutlineG, s.OutlineB, s.OutlineA, targetR, targetG, targetB, targetA)
                && ApproxEqual(s.FillA, targetFillA);
        }

        private static bool TryGetPresetTargets(
            HoverColorsSettings settings,
            int slot,
            out float targetR,
            out float targetG,
            out float targetB,
            out float targetA,
            out float targetFillA,
            out int targetGuidelinePercent)
        {
            bool useSetB = settings.ActivePresetSet == HoverColorsSettings.kPresetSetB;

            if (slot == 1 && useSetB)
            {
                targetR = settings.PresetAlt1R;
                targetG = settings.PresetAlt1G;
                targetB = settings.PresetAlt1B;
                targetA = settings.PresetAlt1A;
                targetFillA = settings.PresetAlt1FillA;
                targetGuidelinePercent = settings.PresetAlt1GuidelinePercent;
                return true;
            }

            if (slot == 1)
            {
                targetR = settings.Preset1R;
                targetG = settings.Preset1G;
                targetB = settings.Preset1B;
                targetA = settings.Preset1A;
                targetFillA = settings.Preset1FillA;
                targetGuidelinePercent = settings.Preset1GuidelinePercent;
                return true;
            }

            if (slot == 2 && useSetB)
            {
                targetR = settings.PresetAlt2R;
                targetG = settings.PresetAlt2G;
                targetB = settings.PresetAlt2B;
                targetA = settings.PresetAlt2A;
                targetFillA = settings.PresetAlt2FillA;
                targetGuidelinePercent = settings.PresetAlt2GuidelinePercent;
                return true;
            }

            if (slot == 2)
            {
                targetR = settings.Preset2R;
                targetG = settings.Preset2G;
                targetB = settings.Preset2B;
                targetA = settings.Preset2A;
                targetFillA = settings.Preset2FillA;
                targetGuidelinePercent = settings.Preset2GuidelinePercent;
                return true;
            }

            targetR = 0f;
            targetG = 0f;
            targetB = 0f;
            targetA = 0f;
            targetFillA = 0f;
            targetGuidelinePercent = HoverColorsSettings.kDefaultGuidelineOpacityPercent;
            return false;
        }

        private static void GetPresetBindingValues(
            HoverColorsSettings? settings,
            int slot,
            out float targetR,
            out float targetG,
            out float targetB,
            out float targetA,
            out float targetFillA)
        {
            if (settings != null
                && TryGetPresetTargets(
                    settings,
                    slot,
                    out targetR,
                    out targetG,
                    out targetB,
                    out targetA,
                    out targetFillA,
                    out int _))
            {
                return;
            }

            if (slot == 2)
            {
                targetR = HoverColorsSettings.kPresetA2R;
                targetG = HoverColorsSettings.kPresetA2G;
                targetB = HoverColorsSettings.kPresetA2B;
                targetA = HoverColorsSettings.kPresetA2A;
                targetFillA = HoverColorsSettings.kPresetA2FillA;
                return;
            }

            targetR = HoverColorsSettings.kPresetA1R;
            targetG = HoverColorsSettings.kPresetA1G;
            targetB = HoverColorsSettings.kPresetA1B;
            targetA = HoverColorsSettings.kPresetA1A;
            targetFillA = HoverColorsSettings.kPresetA1FillA;
        }


        private static bool ApproxEqual(float a, float b) => Math.Abs(a - b) < 0.0005f;

        private static bool SameColor(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2)
        {
            return ApproxEqual(r1, r2)
                && ApproxEqual(g1, g2)
                && ApproxEqual(b1, b2)
                && ApproxEqual(a1, a2);
        }

        private static float Clamp01(float value) => Math.Max(0f, Math.Min(1f, value));


        private static void SaveGuidelineToggleBackup(HoverColorsSettings settings)
        {
            settings.GuidelineBackupLinesColorPreset = settings.GuidelineLinesColorPreset;
            settings.GuidelineBackupLinesR = settings.GuidelineLinesR;
            settings.GuidelineBackupLinesG = settings.GuidelineLinesG;
            settings.GuidelineBackupLinesB = settings.GuidelineLinesB;
            settings.GuidelineBackupLinesA = settings.GuidelineLinesA;
            settings.GuidelineBackupPreviewColorPreset = settings.GuidelinePreviewColorPreset;
            settings.GuidelineBackupPreviewR = settings.GuidelinePreviewR;
            settings.GuidelineBackupPreviewG = settings.GuidelinePreviewG;
            settings.GuidelineBackupPreviewB = settings.GuidelinePreviewB;
            settings.GuidelineBackupPreviewA = settings.GuidelinePreviewA;
            settings.GuidelineBackupDashedColorPreset = settings.GuidelineDashedColorPreset;
            settings.GuidelineBackupDashedR = settings.GuidelineDashedR;
            settings.GuidelineBackupDashedG = settings.GuidelineDashedG;
            settings.GuidelineBackupDashedB = settings.GuidelineDashedB;
            settings.GuidelineBackupOpacityPercent = settings.GuidelineOpacityPercent;
            settings.GuidelineVanillaToggleHasBackup = true;
        }

        private static void RestoreGuidelineToggleBackup(HoverColorsSettings settings)
        {
            settings.GuidelineLinesColorPreset = settings.GuidelineBackupLinesColorPreset;
            settings.GuidelineLinesR = settings.GuidelineBackupLinesR;
            settings.GuidelineLinesG = settings.GuidelineBackupLinesG;
            settings.GuidelineLinesB = settings.GuidelineBackupLinesB;
            settings.GuidelineLinesA = settings.GuidelineBackupLinesA;
            settings.GuidelinePreviewColorPreset = settings.GuidelineBackupPreviewColorPreset;
            settings.GuidelinePreviewR = settings.GuidelineBackupPreviewR;
            settings.GuidelinePreviewG = settings.GuidelineBackupPreviewG;
            settings.GuidelinePreviewB = settings.GuidelineBackupPreviewB;
            settings.GuidelinePreviewA = settings.GuidelineBackupPreviewA;
            settings.GuidelineDashedColorPreset = settings.GuidelineBackupDashedColorPreset;
            settings.GuidelineDashedR = settings.GuidelineBackupDashedR;
            settings.GuidelineDashedG = settings.GuidelineBackupDashedG;
            settings.GuidelineDashedB = settings.GuidelineBackupDashedB;
            settings.GuidelineOpacityPercent = settings.GuidelineBackupOpacityPercent;
        }

        private static void ApplyVanillaGuidelineSwatches(HoverColorsSettings settings)
        {
            UnityEngine.Color lines = GuidelineColorSystem.CapturedVanillaGuidelineLinesColor;
            UnityEngine.Color preview = GuidelineColorSystem.CapturedVanillaGuidelinePreviewColor;
            UnityEngine.Color dashed = GuidelineColorSystem.CapturedVanillaGuidelineDashedColor;
            settings.GuidelineLinesColorPreset = HoverColorsSettings.kGuidelineColorPresetVanilla;
            settings.GuidelineLinesR = lines.r;
            settings.GuidelineLinesG = lines.g;
            settings.GuidelineLinesB = lines.b;
            settings.GuidelineLinesA = 1f;
            settings.GuidelinePreviewColorPreset = HoverColorsSettings.kGuidelineColorPresetVanilla;
            settings.GuidelinePreviewR = preview.r;
            settings.GuidelinePreviewG = preview.g;
            settings.GuidelinePreviewB = preview.b;
            settings.GuidelinePreviewA = 1f;
            settings.GuidelineDashedColorPreset = HoverColorsSettings.kGuidelineDashedColorPresetVanilla;
            settings.GuidelineDashedR = dashed.r;
            settings.GuidelineDashedG = dashed.g;
            settings.GuidelineDashedB = dashed.b;
            settings.GuidelineOpacityPercent = 100;
        }
    }
}
