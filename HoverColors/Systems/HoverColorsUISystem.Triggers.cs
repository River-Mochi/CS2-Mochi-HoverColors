// <copyright file="HoverColorsUISystem.Triggers.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/HoverColorsUISystem.Triggers.cs
// Purpose: TriggerBinding registration and C#-side guards for in-city panel actions.

namespace HoverColors.UI
{
    using System;
    using Colossal.UI.Binding;
    using HoverColors.Settings;
    using HoverColors.Systems;

    public partial class HoverColorsUISystem
    {
        private void RegisterTriggerBindings()
        {
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetOutlineColor", SetOutlineColor));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetOwnerColor", SetOwnerColor));
            AddBinding(new TriggerBinding<float>(Mod.ModId, "SetFillAlpha", SetFillAlpha));
            AddBinding(new TriggerBinding<int>(Mod.ModId, "SetGuidelineOpacity", SetGuidelineOpacity));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetGuidelineLinesColor", SetGuidelineLinesColor));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetGuidelinePreviewColor", SetGuidelinePreviewColor));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetGuidelineDashedColor", SetGuidelineDashedColor));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetDistrictColor", SetDistrictColor));
            AddBinding(new TriggerBinding(Mod.ModId, "ResetDistrictToVanilla", ResetDistrictToVanilla));
            AddBinding(new TriggerBinding(Mod.ModId, "ResetToVanilla", ResetToVanilla));
            AddBinding(new TriggerBinding(Mod.ModId, "ResetOutlineToVanilla", ResetOutlineToVanilla));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetPanelOpen", SetPanelOpen));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetPanelTooltipsEnabled", SetPanelTooltipsEnabled));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetPanelCollapsed", SetPanelCollapsed));
            AddBinding(new TriggerBinding(Mod.ModId, "ToggleSurfaceToolAreas", ToggleSurfaceToolAreas));
            AddBinding(new TriggerBinding(Mod.ModId, "ToggleSpecializedIndustryAreas", ToggleSpecializedIndustryAreas));
            AddBinding(new TriggerBinding<int>(Mod.ModId, "ApplyPreset", ApplyPreset));
            AddBinding(new TriggerBinding<int>(Mod.ModId, "SavePreset", SavePreset));

            AddBinding(new TriggerBinding(Mod.ModId, "TogglePresetDefaults", TogglePresetDefaults));
            AddBinding(new TriggerBinding(Mod.ModId, "RestorePresetDefaults", RestorePresetDefaults));
            AddBinding(new TriggerBinding(Mod.ModId, "ResetGuidelines", ResetGuidelines));
        }

        private void SetOutlineColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            if (SameColor(settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA, r, g, b, a))
            {
                return;
            }

            settings.OutlineR = r;
            settings.OutlineG = g;
            settings.OutlineB = b;
            settings.OutlineA = a;
            ApplySaveAndSync(settings);
        }

        private void SetOwnerColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            if (SameColor(settings.OwnerR, settings.OwnerG, settings.OwnerB, settings.OwnerA, r, g, b, a))
            {
                return;
            }

            settings.OwnerR = r;
            settings.OwnerG = g;
            settings.OwnerB = b;
            settings.OwnerA = a;
            ApplySaveAndSync(settings);
        }

        private void SetFillAlpha(float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            a = Clamp01(a);
            if (ApproxEqual(settings.FillA, a))
            {
                return;
            }

            settings.FillA = a;
            ApplySaveAndSync(settings);
        }

        private void SetGuidelineOpacity(int percent)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            percent = Math.Max(0, Math.Min(100, percent));
            if (settings.GuidelineOpacityPercent == percent)
            {
                return;
            }

            settings.GuidelineOpacityPercent = percent;
            ApplySaveAndSync(settings);
        }

        private void SetGuidelineLinesColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            r = Clamp01(r);
            g = Clamp01(g);
            b = Clamp01(b);
            a = Clamp01(a);

            bool changed = settings.GuidelineLinesColorPreset != HoverColorsSettings.kGuidelineColorPresetCustom
                || settings.GuidelineVanillaToggleActive
                || !SameColor(settings.GuidelineLinesR, settings.GuidelineLinesG, settings.GuidelineLinesB, settings.GuidelineLinesA, r, g, b, a);

            if (!changed)
            {
                return;
            }

            settings.GuidelineLinesColorPreset = HoverColorsSettings.kGuidelineColorPresetCustom;
            settings.GuidelineLinesR = r;
            settings.GuidelineLinesG = g;
            settings.GuidelineLinesB = b;
            settings.GuidelineLinesA = a;
            settings.GuidelineVanillaToggleActive = false;
            ApplySaveAndSync(settings);
        }

        private void SetGuidelinePreviewColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            r = Clamp01(r);
            g = Clamp01(g);
            b = Clamp01(b);
            a = Clamp01(a);

            bool changed = settings.GuidelinePreviewColorPreset != HoverColorsSettings.kGuidelineColorPresetCustom
                || settings.GuidelineVanillaToggleActive
                || !SameColor(settings.GuidelinePreviewR, settings.GuidelinePreviewG, settings.GuidelinePreviewB, settings.GuidelinePreviewA, r, g, b, a);

            if (!changed)
            {
                return;
            }

            settings.GuidelinePreviewColorPreset = HoverColorsSettings.kGuidelineColorPresetCustom;
            settings.GuidelinePreviewR = r;
            settings.GuidelinePreviewG = g;
            settings.GuidelinePreviewB = b;
            settings.GuidelinePreviewA = a;
            settings.GuidelineVanillaToggleActive = false;
            ApplySaveAndSync(settings);
        }


        private void SetGuidelineDashedColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            r = Clamp01(r);
            g = Clamp01(g);
            b = Clamp01(b);
            a = Clamp01(a);
            int percent = Math.Max(0, Math.Min(100, (int)(Math.Round(a * 20f) * 5f)));

            bool changed = settings.GuidelineDashedColorPreset != HoverColorsSettings.kGuidelineDashedColorPresetCustom
                || settings.GuidelineVanillaToggleActive
                || !ApproxEqual(settings.GuidelineDashedR, r)
                || !ApproxEqual(settings.GuidelineDashedG, g)
                || !ApproxEqual(settings.GuidelineDashedB, b)
                || settings.GuidelineOpacityPercent != percent;

            if (!changed)
            {
                return;
            }

            // Dashed swatch alpha and the guideline opacity slider represent the same value.
            settings.GuidelineDashedColorPreset = HoverColorsSettings.kGuidelineDashedColorPresetCustom;
            settings.GuidelineDashedR = r;
            settings.GuidelineDashedG = g;
            settings.GuidelineDashedB = b;
            settings.GuidelineOpacityPercent = percent;
            settings.GuidelineVanillaToggleActive = false;
            ApplySaveAndSync(settings);
        }
        private void SetDistrictColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            r = Clamp01(r);
            g = Clamp01(g);
            b = Clamp01(b);
            a = Clamp01(a);

            bool changed = !settings.DistrictColorEnabled
                || !SameColor(settings.DistrictR, settings.DistrictG, settings.DistrictB, settings.DistrictA, r, g, b, a);

            if (!changed)
            {
                return;
            }

            settings.DistrictColorEnabled = true;
            settings.DistrictR = r;
            settings.DistrictG = g;
            settings.DistrictB = b;
            settings.DistrictA = a;
            ApplySaveAndSync(settings);
        }

        private void ResetDistrictToVanilla()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            UnityEngine.Color district = DistrictColorSystem.CapturedDistrictFillColor;
            bool changed = settings.DistrictColorEnabled
                || !SameColor(settings.DistrictR, settings.DistrictG, settings.DistrictB, settings.DistrictA,
                    district.r, district.g, district.b, district.a);

            if (!changed)
            {
                return;
            }

            settings.DistrictColorEnabled = false;
            settings.DistrictR = district.r;
            settings.DistrictG = district.g;
            settings.DistrictB = district.b;
            settings.DistrictA = district.a;
            ApplySaveAndSync(settings);
        }

        private void ResetToVanilla()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            UnityEngine.Color hovered = OutlineColorSystem.CapturedHoveredColor;
            UnityEngine.Color owner = OutlineColorSystem.CapturedOwnerColor;
            float fillA = OutlineColorSystem.CapturedFillA;

            bool changed = !SameColor(settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA,
                    hovered.r, hovered.g, hovered.b, OutlineColorSystem.CapturedOutlineA)
                || !SameColor(settings.OwnerR, settings.OwnerG, settings.OwnerB, settings.OwnerA,
                    owner.r, owner.g, owner.b, owner.a)
                || !ApproxEqual(settings.FillA, fillA);

            if (!changed)
            {
                return;
            }

            settings.OutlineR = hovered.r;
            settings.OutlineG = hovered.g;
            settings.OutlineB = hovered.b;
            settings.OutlineA = OutlineColorSystem.CapturedOutlineA;
            settings.OwnerR = owner.r;
            settings.OwnerG = owner.g;
            settings.OwnerB = owner.b;
            settings.OwnerA = owner.a;
            settings.FillA = fillA;
            ApplySaveAndSync(settings);
        }

        private void ResetOutlineToVanilla()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            UnityEngine.Color hovered = OutlineColorSystem.CapturedHoveredColor;
            UnityEngine.Color owner = OutlineColorSystem.CapturedOwnerColor;

            bool changed = !SameColor(settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA,
                    hovered.r, hovered.g, hovered.b, OutlineColorSystem.CapturedOutlineA)
                || !SameColor(settings.OwnerR, settings.OwnerG, settings.OwnerB, settings.OwnerA,
                    owner.r, owner.g, owner.b, owner.a);

            if (!changed)
            {
                return;
            }

            settings.OutlineR = hovered.r;
            settings.OutlineG = hovered.g;
            settings.OutlineB = hovered.b;
            settings.OutlineA = OutlineColorSystem.CapturedOutlineA;
            settings.OwnerR = owner.r;
            settings.OwnerG = owner.g;
            settings.OwnerB = owner.b;
            settings.OwnerA = owner.a;
            ApplySaveAndSync(settings);
        }

        private void SetPanelTooltipsEnabled(bool enabled)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || settings.PanelTooltipsEnabled == enabled)
            {
                return;
            }

            settings.PanelTooltipsEnabled = enabled;
            ApplySaveAndSync(settings);
        }

        private void SetPanelCollapsed(bool collapsed)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || settings.PanelCollapsed == collapsed)
            {
                return;
            }

            settings.PanelCollapsed = collapsed;
            ApplySaveAndSync(settings);
        }

        private void ToggleSurfaceToolAreas()
        {
            bool enabled = !AreaToolOverlaySystem.SuppressSurfaceToolAreas;
            AreaToolOverlaySystem.SetSurfaceSuppression(enabled);

            HoverColorsSettings? settings = Mod.Settings;
            if (settings != null && settings.SurfaceToolAreasSuppressed != enabled)
            {
                settings.SurfaceToolAreasSuppressed = enabled;
                ApplySaveAndSync(settings);
            }
            else
            {
                UpdateIfChanged(m_SurfaceToolAreasSuppressedBinding, AreaToolOverlaySystem.SuppressSurfaceToolAreas);
            }
        }

        private void ToggleSpecializedIndustryAreas()
        {
            bool enabled = !AreaToolOverlaySystem.SuppressSpecializedIndustryToolAreas;
            AreaToolOverlaySystem.SetSpecializedIndustrySuppression(enabled);

            HoverColorsSettings? settings = Mod.Settings;
            if (settings != null
                && (settings.SpecializedIndustryAreasSuppressed != enabled
                    || !settings.SpecializedIndustryAreasSuppressionInitialized))
            {
                settings.SpecializedIndustryAreasSuppressed = enabled;
                settings.SpecializedIndustryAreasSuppressionInitialized = true;
                ApplySaveAndSync(settings);
            }
            else
            {
                UpdateIfChanged(m_SpecializedIndustryAreasSuppressedBinding, AreaToolOverlaySystem.SuppressSpecializedIndustryToolAreas);
            }
        }

        private void ApplyPreset(int slot)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            if (!TryGetPresetTargets(settings, slot,
                    out float targetR,
                    out float targetG,
                    out float targetB,
                    out float targetA,
                    out float targetFillA,
                    out int targetGuidelinePercent))
            {
                return;
            }

            bool changed = !SameColor(settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA,
                    targetR, targetG, targetB, targetA)
                || !ApproxEqual(settings.FillA, targetFillA)
                || settings.GuidelineOpacityPercent != targetGuidelinePercent;

            if (!changed)
            {
                return;
            }

            settings.OutlineR = targetR;
            settings.OutlineG = targetG;
            settings.OutlineB = targetB;
            settings.OutlineA = targetA;
            settings.FillA = targetFillA;
            settings.GuidelineOpacityPercent = targetGuidelinePercent;
            ApplySaveAndSync(settings);
        }


        private void SavePreset(int slot)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            // If defaults are being previewed, save into the player's real preset bank.
            // This keeps the mod default colors as a temporary view instead of letting
            // players accidentally overwrite the default-preview copy.
            if (settings.PresetDefaultsToggleActive)
            {
                if (settings.PresetDefaultsToggleHasBackup)
                {
                    RestorePresetToggleBackup(settings);
                }

                settings.PresetDefaultsToggleActive = false;
                settings.PresetDefaultsToggleHasBackup = false;
            }

            bool useSetB = settings.ActivePresetSet == HoverColorsSettings.kPresetSetB;

            if (slot == 1 && useSetB)
            {
                bool changed = !SameColor(settings.PresetAlt1R, settings.PresetAlt1G, settings.PresetAlt1B, settings.PresetAlt1A,
                        settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA)
                    || !ApproxEqual(settings.PresetAlt1FillA, settings.FillA)
                    || settings.PresetAlt1GuidelinePercent != settings.GuidelineOpacityPercent;

                if (!changed)
                {
                    return;
                }

                settings.PresetAlt1R = settings.OutlineR;
                settings.PresetAlt1G = settings.OutlineG;
                settings.PresetAlt1B = settings.OutlineB;
                settings.PresetAlt1A = settings.OutlineA;
                settings.PresetAlt1FillA = settings.FillA;
                settings.PresetAlt1GuidelinePercent = settings.GuidelineOpacityPercent;
            }
            else if (slot == 1)
            {
                bool changed = !SameColor(settings.Preset1R, settings.Preset1G, settings.Preset1B, settings.Preset1A,
                        settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA)
                    || !ApproxEqual(settings.Preset1FillA, settings.FillA)
                    || settings.Preset1GuidelinePercent != settings.GuidelineOpacityPercent;

                if (!changed)
                {
                    return;
                }

                settings.Preset1R = settings.OutlineR;
                settings.Preset1G = settings.OutlineG;
                settings.Preset1B = settings.OutlineB;
                settings.Preset1A = settings.OutlineA;
                settings.Preset1FillA = settings.FillA;
                settings.Preset1GuidelinePercent = settings.GuidelineOpacityPercent;
            }
            else if (slot == 2 && useSetB)
            {
                bool changed = !SameColor(settings.PresetAlt2R, settings.PresetAlt2G, settings.PresetAlt2B, settings.PresetAlt2A,
                        settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA)
                    || !ApproxEqual(settings.PresetAlt2FillA, settings.FillA)
                    || settings.PresetAlt2GuidelinePercent != settings.GuidelineOpacityPercent;

                if (!changed)
                {
                    return;
                }

                settings.PresetAlt2R = settings.OutlineR;
                settings.PresetAlt2G = settings.OutlineG;
                settings.PresetAlt2B = settings.OutlineB;
                settings.PresetAlt2A = settings.OutlineA;
                settings.PresetAlt2FillA = settings.FillA;
                settings.PresetAlt2GuidelinePercent = settings.GuidelineOpacityPercent;
            }
            else if (slot == 2)
            {
                bool changed = !SameColor(settings.Preset2R, settings.Preset2G, settings.Preset2B, settings.Preset2A,
                        settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA)
                    || !ApproxEqual(settings.Preset2FillA, settings.FillA)
                    || settings.Preset2GuidelinePercent != settings.GuidelineOpacityPercent;

                if (!changed)
                {
                    return;
                }

                settings.Preset2R = settings.OutlineR;
                settings.Preset2G = settings.OutlineG;
                settings.Preset2B = settings.OutlineB;
                settings.Preset2A = settings.OutlineA;
                settings.Preset2FillA = settings.FillA;
                settings.Preset2GuidelinePercent = settings.GuidelineOpacityPercent;
            }
            else
            {
                return;
            }

            ApplySaveAndSync(settings);
        }

        private void TogglePresetDefaults()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            int nextSet = settings.ActivePresetSet == HoverColorsSettings.kPresetSetB
                ? HoverColorsSettings.kPresetSetA
                : HoverColorsSettings.kPresetSetB;

            if (settings.ActivePresetSet == nextSet)
            {
                return;
            }

            settings.ActivePresetSet = nextSet;
            ApplySaveAndSync(settings);
        }

        private void RestorePresetDefaults()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            if (settings.PresetDefaultsToggleActive && settings.PresetDefaultsToggleHasBackup)
            {
                RestorePresetToggleBackup(settings);
                settings.PresetDefaultsToggleActive = false;
                settings.PresetDefaultsToggleHasBackup = false;
                ApplySaveAndSync(settings);
                return;
            }

            SavePresetToggleBackup(settings);
            ApplyPresetDefaults(settings);
            settings.PresetDefaultsToggleActive = true;
            settings.PresetDefaultsToggleHasBackup = true;
            ApplySaveAndSync(settings);
        }

        private static void SavePresetToggleBackup(HoverColorsSettings settings)
        {
            settings.PresetDefaultsBackupActiveSet = settings.ActivePresetSet;

            settings.PresetDefaultsBackup1R = settings.Preset1R;
            settings.PresetDefaultsBackup1G = settings.Preset1G;
            settings.PresetDefaultsBackup1B = settings.Preset1B;
            settings.PresetDefaultsBackup1A = settings.Preset1A;
            settings.PresetDefaultsBackup1FillA = settings.Preset1FillA;
            settings.PresetDefaultsBackup1GuidelinePercent = settings.Preset1GuidelinePercent;

            settings.PresetDefaultsBackup2R = settings.Preset2R;
            settings.PresetDefaultsBackup2G = settings.Preset2G;
            settings.PresetDefaultsBackup2B = settings.Preset2B;
            settings.PresetDefaultsBackup2A = settings.Preset2A;
            settings.PresetDefaultsBackup2FillA = settings.Preset2FillA;
            settings.PresetDefaultsBackup2GuidelinePercent = settings.Preset2GuidelinePercent;

            settings.PresetDefaultsBackupAlt1R = settings.PresetAlt1R;
            settings.PresetDefaultsBackupAlt1G = settings.PresetAlt1G;
            settings.PresetDefaultsBackupAlt1B = settings.PresetAlt1B;
            settings.PresetDefaultsBackupAlt1A = settings.PresetAlt1A;
            settings.PresetDefaultsBackupAlt1FillA = settings.PresetAlt1FillA;
            settings.PresetDefaultsBackupAlt1GuidelinePercent = settings.PresetAlt1GuidelinePercent;

            settings.PresetDefaultsBackupAlt2R = settings.PresetAlt2R;
            settings.PresetDefaultsBackupAlt2G = settings.PresetAlt2G;
            settings.PresetDefaultsBackupAlt2B = settings.PresetAlt2B;
            settings.PresetDefaultsBackupAlt2A = settings.PresetAlt2A;
            settings.PresetDefaultsBackupAlt2FillA = settings.PresetAlt2FillA;
            settings.PresetDefaultsBackupAlt2GuidelinePercent = settings.PresetAlt2GuidelinePercent;
        }

        private static void RestorePresetToggleBackup(HoverColorsSettings settings)
        {
            settings.ActivePresetSet = settings.PresetDefaultsBackupActiveSet == HoverColorsSettings.kPresetSetB
                ? HoverColorsSettings.kPresetSetB
                : HoverColorsSettings.kPresetSetA;

            settings.Preset1R = settings.PresetDefaultsBackup1R;
            settings.Preset1G = settings.PresetDefaultsBackup1G;
            settings.Preset1B = settings.PresetDefaultsBackup1B;
            settings.Preset1A = settings.PresetDefaultsBackup1A;
            settings.Preset1FillA = settings.PresetDefaultsBackup1FillA;
            settings.Preset1GuidelinePercent = settings.PresetDefaultsBackup1GuidelinePercent;

            settings.Preset2R = settings.PresetDefaultsBackup2R;
            settings.Preset2G = settings.PresetDefaultsBackup2G;
            settings.Preset2B = settings.PresetDefaultsBackup2B;
            settings.Preset2A = settings.PresetDefaultsBackup2A;
            settings.Preset2FillA = settings.PresetDefaultsBackup2FillA;
            settings.Preset2GuidelinePercent = settings.PresetDefaultsBackup2GuidelinePercent;

            settings.PresetAlt1R = settings.PresetDefaultsBackupAlt1R;
            settings.PresetAlt1G = settings.PresetDefaultsBackupAlt1G;
            settings.PresetAlt1B = settings.PresetDefaultsBackupAlt1B;
            settings.PresetAlt1A = settings.PresetDefaultsBackupAlt1A;
            settings.PresetAlt1FillA = settings.PresetDefaultsBackupAlt1FillA;
            settings.PresetAlt1GuidelinePercent = settings.PresetDefaultsBackupAlt1GuidelinePercent;

            settings.PresetAlt2R = settings.PresetDefaultsBackupAlt2R;
            settings.PresetAlt2G = settings.PresetDefaultsBackupAlt2G;
            settings.PresetAlt2B = settings.PresetDefaultsBackupAlt2B;
            settings.PresetAlt2A = settings.PresetDefaultsBackupAlt2A;
            settings.PresetAlt2FillA = settings.PresetDefaultsBackupAlt2FillA;
            settings.PresetAlt2GuidelinePercent = settings.PresetDefaultsBackupAlt2GuidelinePercent;
        }

        private static void ApplyPresetDefaults(HoverColorsSettings settings)
        {
            settings.Preset1R = HoverColorsSettings.kPresetA1R;
            settings.Preset1G = HoverColorsSettings.kPresetA1G;
            settings.Preset1B = HoverColorsSettings.kPresetA1B;
            settings.Preset1A = HoverColorsSettings.kPresetA1A;
            settings.Preset1FillA = HoverColorsSettings.kPresetA1FillA;
            settings.Preset1GuidelinePercent = HoverColorsSettings.kDefaultGuidelineOpacityPercent;

            settings.Preset2R = HoverColorsSettings.kPresetA2R;
            settings.Preset2G = HoverColorsSettings.kPresetA2G;
            settings.Preset2B = HoverColorsSettings.kPresetA2B;
            settings.Preset2A = HoverColorsSettings.kPresetA2A;
            settings.Preset2FillA = HoverColorsSettings.kPresetA2FillA;
            settings.Preset2GuidelinePercent = HoverColorsSettings.kDefaultGuidelineOpacityPercent;

            settings.PresetAlt1R = HoverColorsSettings.kPresetB1R;
            settings.PresetAlt1G = HoverColorsSettings.kPresetB1G;
            settings.PresetAlt1B = HoverColorsSettings.kPresetB1B;
            settings.PresetAlt1A = HoverColorsSettings.kPresetB1A;
            settings.PresetAlt1FillA = HoverColorsSettings.kPresetB1FillA;
            settings.PresetAlt1GuidelinePercent = HoverColorsSettings.kDefaultGuidelineOpacityPercent;

            settings.PresetAlt2R = HoverColorsSettings.kPresetB2R;
            settings.PresetAlt2G = HoverColorsSettings.kPresetB2G;
            settings.PresetAlt2B = HoverColorsSettings.kPresetB2B;
            settings.PresetAlt2A = HoverColorsSettings.kPresetB2A;
            settings.PresetAlt2FillA = HoverColorsSettings.kPresetB2FillA;
            settings.PresetAlt2GuidelinePercent = HoverColorsSettings.kDefaultGuidelineOpacityPercent;
        }

        private void ResetGuidelines()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;

            if (settings.GuidelineVanillaToggleActive && settings.GuidelineVanillaToggleHasBackup)
            {
                RestoreGuidelineToggleBackup(settings);
                settings.GuidelineVanillaToggleActive = false;
            }
            else
            {
                SaveGuidelineToggleBackup(settings);
                ApplyVanillaGuidelineSwatches(settings);
                settings.GuidelineVanillaToggleActive = true;
            }

            ApplySaveAndSync(settings);
        }

        private void ApplyPresetSlot(int slot)
        {
            ApplyPreset(slot);
        }
    }
}
