// <copyright file="HoverColorsUISystem.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/HoverColorsUISystem.cs
// Bridges Mod.Settings to cs2/api bindings, owns the shared panel-open flag,
// and checks cached keybind actions for panel/tool toggles.

namespace HoverColors.UI
{
    using System;
    using System.Collections.Generic;

    using Colossal.UI.Binding;

    using CS2Shared.RiverMochi;

    using Game;
    using Game.Input;
    using Game.SceneFlow;
    using Game.UI;

    using HoverColors.Settings;
    using HoverColors.Systems;

    public partial class HoverColorsUISystem : UISystemBase
    {
        private static bool s_PanelOpen;

        // Toggle target for both the GTL button (via SetPanelOpen trigger) and the J hotkey poll below.
        public static void TogglePanel() => s_PanelOpen = !s_PanelOpen;

        // ProxyActions registered by Setting.RegisterKeyBindings() in Mod.OnLoad.
        // Cached actions are checked with WasReleasedThisFrame(); lookup is retried only if an action is missing.
        private ProxyAction? m_TogglePanelAction;
        private ProxyAction? m_ToggleSurfaceToolAreasAction;

        // Live color bindings
        private ValueBinding<float> m_OutlineRBinding = null!;
        private ValueBinding<float> m_OutlineGBinding = null!;
        private ValueBinding<float> m_OutlineBBinding = null!;
        private ValueBinding<float> m_OutlineABinding = null!;
        private ValueBinding<float> m_OwnerRBinding = null!;
        private ValueBinding<float> m_OwnerGBinding = null!;
        private ValueBinding<float> m_OwnerBBinding = null!;
        private ValueBinding<float> m_OwnerABinding = null!;
        private ValueBinding<float> m_FillABinding = null!;
        private ValueBinding<float> m_DistrictRBinding = null!;
        private ValueBinding<float> m_DistrictGBinding = null!;
        private ValueBinding<float> m_DistrictBBinding = null!;
        private ValueBinding<float> m_DistrictABinding = null!;
        private ValueBinding<float> m_GuidelineLinesColorRBinding = null!;
        private ValueBinding<float> m_GuidelineLinesColorGBinding = null!;
        private ValueBinding<float> m_GuidelineLinesColorBBinding = null!;
        private ValueBinding<float> m_GuidelineLinesColorABinding = null!;
        private ValueBinding<float> m_GuidelinePreviewColorRBinding = null!;
        private ValueBinding<float> m_GuidelinePreviewColorGBinding = null!;
        private ValueBinding<float> m_GuidelinePreviewColorBBinding = null!;
        private ValueBinding<float> m_GuidelinePreviewColorABinding = null!;
        private ValueBinding<float> m_GuidelineDashedColorRBinding = null!;
        private ValueBinding<float> m_GuidelineDashedColorGBinding = null!;
        private ValueBinding<float> m_GuidelineDashedColorBBinding = null!;
        private ValueBinding<int> m_GuidelineOpacityBinding = null!;
        private ValueBinding<int> m_GuidelineDefaultBinding = null!;
        private ValueBinding<bool> m_PanelOpenBinding = null!;
        private ValueBinding<bool> m_PanelTooltipsEnabledBinding = null!;
        private ValueBinding<bool> m_PanelCollapsedBinding = null!;
        private ValueBinding<bool> m_UseDarkerPanelBinding = null!;
        private ValueBinding<bool> m_SurfaceToolAreasSuppressedBinding = null!;
        private ValueBinding<bool> m_SpecializedIndustryAreasSuppressedBinding = null!;
        private ValueBinding<bool> m_VanillaOutlineActiveBinding = null!;

        // Preset slot bindings — panel reads stored colors for swatch previews + active-state dots.
        private ValueBinding<float> m_Preset1RBinding = null!;
        private ValueBinding<float> m_Preset1GBinding = null!;
        private ValueBinding<float> m_Preset1BBinding = null!;
        private ValueBinding<float> m_Preset1ABinding = null!;
        private ValueBinding<float> m_Preset1FillABinding = null!;
        private ValueBinding<float> m_Preset2RBinding = null!;
        private ValueBinding<float> m_Preset2GBinding = null!;
        private ValueBinding<float> m_Preset2BBinding = null!;
        private ValueBinding<float> m_Preset2ABinding = null!;
        private ValueBinding<float> m_Preset2FillABinding = null!;
        private ValueBinding<bool> m_Preset1ActiveBinding = null!;
        private ValueBinding<bool> m_Preset2ActiveBinding = null!;

        // Factory defaults — keep in sync with HoverColorsSettings.SetDefaults().
        private const float DefaultPreset1R = 215f / 255f, DefaultPreset1G = 226f / 255f, DefaultPreset1B = 194f / 255f;
        private const float DefaultPreset1A = 0.67f, DefaultPreset1FillA = 0f;
        private const float DefaultPreset2R = 0.25f, DefaultPreset2G = 0.15f, DefaultPreset2B = 0.25f;
        private const float DefaultPreset2A = 0.5f, DefaultPreset2FillA = 0f;

        // In-memory backup for TogglePresetDefaults (session-only, not persisted to .coc).
        private float m_BkP1R, m_BkP1G, m_BkP1B, m_BkP1A, m_BkP1FillA;
        private int m_BkP1Guideline;
        private float m_BkP2R, m_BkP2G, m_BkP2B, m_BkP2A, m_BkP2FillA;
        private int m_BkP2Guideline;
        private bool m_PresetBackupExists;

        // K hotkey toggle: alternates between applying preset 1 and preset 2.
        private ProxyAction? m_TogglePresetAction;
        private int m_LastAppliedPreset = 1;

        protected override void OnCreate()
        {
            base.OnCreate();

            InitializeKeybindActions();
            RegisterValueBindings();
            RegisterTriggerBindings();
        }

        protected override void OnUpdate()
        {
            // CWD-style push bindings: do not call base.OnUpdate() because there are no
            // GetterValueBindings to poll. This keeps the panel idle unless a value actually changes.
            SyncValueBindings();

            // Re-fetch if the action wasn't ready at OnCreate (RegisterKeyBindings race) or got dropped.
            RefreshKeybindActions();

            // Don't fire hotkeys in main menu / editor.
            if (!IsInGame())
            {
                return;
            }

            // Read current shared state and flip it — works whether button or previous hotkey set it.
            if (m_TogglePanelAction?.WasReleasedThisFrame() == true)
            {
                TogglePanel();
                SyncValueBindings();
            }

            if (m_ToggleSurfaceToolAreasAction?.WasReleasedThisFrame() == true)
            {
                ToggleSurfaceToolAreas();
            }

            // K toggles between preset slot 1 and slot 2.
            if (m_TogglePresetAction?.WasReleasedThisFrame() == true)
            {
                m_LastAppliedPreset = (m_LastAppliedPreset == 1) ? 2 : 1;
                ApplyPresetSlot(m_LastAppliedPreset);
            }
        }

        private void RegisterValueBindings()
        {
            HoverColorsSettings? settings = Mod.Settings;
            bool suppressSurfaceToolAreas = settings?.SurfaceToolAreasSuppressed ?? true;
            bool suppressSpecializedIndustryAreas = settings?.SpecializedIndustryAreasSuppressed ?? true;
            AreaToolOverlaySystem.SetSurfaceSuppression(suppressSurfaceToolAreas);
            AreaToolOverlaySystem.SetSpecializedIndustrySuppression(suppressSpecializedIndustryAreas);

            m_OutlineRBinding = AddValueBinding("OutlineR", settings?.OutlineR ?? 0.502f);
            m_OutlineGBinding = AddValueBinding("OutlineG", settings?.OutlineG ?? 0.869f);
            m_OutlineBBinding = AddValueBinding("OutlineB", settings?.OutlineB ?? 1f);
            m_OutlineABinding = AddValueBinding("OutlineA", settings?.OutlineA ?? 0.855f);
            m_OwnerRBinding = AddValueBinding("OwnerR", settings?.OwnerR ?? 0.247f);
            m_OwnerGBinding = AddValueBinding("OwnerG", settings?.OwnerG ?? 0.981f);
            m_OwnerBBinding = AddValueBinding("OwnerB", settings?.OwnerB ?? 0.247f);
            m_OwnerABinding = AddValueBinding("OwnerA", settings?.OwnerA ?? 0.702f);
            m_FillABinding = AddValueBinding("FillA", settings?.FillA ?? 0f);
            m_DistrictRBinding = AddValueBinding("DistrictR", settings?.DistrictR ?? 128f / 255f);
            m_DistrictGBinding = AddValueBinding("DistrictG", settings?.DistrictG ?? 128f / 255f);
            m_DistrictBBinding = AddValueBinding("DistrictB", settings?.DistrictB ?? 128f / 255f);
            m_DistrictABinding = AddValueBinding("DistrictA", settings?.DistrictA ?? 64f / 255f);
            m_GuidelineLinesColorRBinding = AddValueBinding("GuidelineLinesColorR", settings?.GuidelineLinesR ?? 0.7f);
            m_GuidelineLinesColorGBinding = AddValueBinding("GuidelineLinesColorG", settings?.GuidelineLinesG ?? 0.7f);
            m_GuidelineLinesColorBBinding = AddValueBinding("GuidelineLinesColorB", settings?.GuidelineLinesB ?? 1f);
            m_GuidelineLinesColorABinding = AddValueBinding("GuidelineLinesColorA", settings?.GuidelineLinesA ?? 1f);
            m_GuidelinePreviewColorRBinding = AddValueBinding("GuidelinePreviewColorR", settings?.GuidelinePreviewR ?? 0.7f);
            m_GuidelinePreviewColorGBinding = AddValueBinding("GuidelinePreviewColorG", settings?.GuidelinePreviewG ?? 0.7f);
            m_GuidelinePreviewColorBBinding = AddValueBinding("GuidelinePreviewColorB", settings?.GuidelinePreviewB ?? 1f);
            m_GuidelinePreviewColorABinding = AddValueBinding("GuidelinePreviewColorA", settings?.GuidelinePreviewA ?? 1f);
            m_GuidelineDashedColorRBinding = AddValueBinding("GuidelineDashedColorR", settings?.GuidelineDashedR ?? 0.7f);
            m_GuidelineDashedColorGBinding = AddValueBinding("GuidelineDashedColorG", settings?.GuidelineDashedG ?? 0.7f);
            m_GuidelineDashedColorBBinding = AddValueBinding("GuidelineDashedColorB", settings?.GuidelineDashedB ?? 1f);
            m_GuidelineOpacityBinding = AddValueBinding("GuidelineOpacityPercent", settings?.GuidelineOpacityPercent ?? HoverColorsSettings.kDefaultGuidelineOpacityPercent);
