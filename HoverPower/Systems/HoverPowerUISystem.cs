// File: Systems/HoverPowerUISystem.cs
// Bridges Mod.Settings to cs2/api bindings, owns the shared panel-open flag,
// and checks cached keybind actions for panel/tool toggles.

namespace HoverPower.UI
{
    using Colossal.UI.Binding;
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Input;
    using Game.SceneFlow;
    using Game.UI;
    using HoverPower.Settings;
    using HoverPower.Systems;
    using System;
    using System.Collections.Generic;

    public partial class HoverPowerUISystem : UISystemBase
    {
        private static bool s_PanelOpen;

        // Toggle target for both the GTL button (via SetPanelOpen trigger) and the H hotkey poll below.
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
        private ValueBinding<float> m_FillABinding = null!;
        private ValueBinding<int> m_GuidelineOpacityBinding = null!;
        private ValueBinding<bool> m_PanelOpenBinding = null!;
        private ValueBinding<bool> m_SurfaceToolAreasSuppressedBinding = null!;
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

        protected override void OnCreate()
        {
            base.OnCreate();
            LogUtils.Info(() => $"{Mod.ModTag} HoverPowerUISystem created");

            InitializeKeybindActions();

            RegisterValueBindings();

            AddBinding(new TriggerBinding<float, float, float, float>(
                Mod.ModId,
                "SetOutlineColor",
                (r, g, b, a) =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    settings.OutlineR = r;
                    settings.OutlineG = g;
                    settings.OutlineB = b;
                    settings.OutlineA = a;
                    settings.ApplyAndSave();
                    SyncValueBindings();
                }));

            AddBinding(new TriggerBinding<float>(
                Mod.ModId,
                "SetFillAlpha",
                a =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    settings.FillA = a;
                    settings.ApplyAndSave();
                    SyncValueBindings();
                }));

            AddBinding(new TriggerBinding<int>(
                Mod.ModId,
                "SetGuidelineOpacity",
                percent =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    if (percent < 0) percent = 0;
                    if (percent > 100) percent = 100;

                    settings.GuidelineOpacityPercent = percent;
                    settings.ApplyAndSave();
                    SyncValueBindings();
                }));

            AddBinding(new TriggerBinding(
                Mod.ModId,
                "ResetToVanilla",
                () =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    UnityEngine.Color hovered = OutlineColorSystem.CapturedHoveredColor;
                    settings.OutlineR = hovered.r;
                    settings.OutlineG = hovered.g;
                    settings.OutlineB = hovered.b;
                    settings.OutlineA = OutlineColorSystem.CapturedOutlineA;
                    settings.FillA = OutlineColorSystem.CapturedFillA;
                    settings.ApplyAndSave();
                    SyncValueBindings();
                }));

            AddBinding(new TriggerBinding(
                Mod.ModId,
                "ResetOutlineToVanilla",
                () =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    UnityEngine.Color hovered = OutlineColorSystem.CapturedHoveredColor;
                    settings.OutlineR = hovered.r;
                    settings.OutlineG = hovered.g;
                    settings.OutlineB = hovered.b;
                    settings.OutlineA = OutlineColorSystem.CapturedOutlineA;
                    settings.ApplyAndSave();
                    SyncValueBindings();
                }));

            AddBinding(new TriggerBinding<bool>(
                Mod.ModId,
                "SetPanelOpen",
                SetPanelOpen));

            AddBinding(new TriggerBinding(
                Mod.ModId,
                "ToggleSurfaceToolAreas",
                ToggleSurfaceToolAreas));

            // Apply a stored preset slot (slot = 1 or 2) — pushes that slot's color to live swatch.
            AddBinding(new TriggerBinding<int>(
                Mod.ModId,
                "ApplyPreset",
                slot =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    if (slot == 1)
                    {
                        settings.OutlineR = settings.Preset1R;
                        settings.OutlineG = settings.Preset1G;
                        settings.OutlineB = settings.Preset1B;
                        settings.OutlineA = settings.Preset1A;
                        settings.FillA = settings.Preset1FillA;
                    }
                    else if (slot == 2)
                    {
                        settings.OutlineR = settings.Preset2R;
                        settings.OutlineG = settings.Preset2G;
                        settings.OutlineB = settings.Preset2B;
                        settings.OutlineA = settings.Preset2A;
                        settings.FillA = settings.Preset2FillA;
                    }

                    settings.ApplyAndSave();
                    SyncValueBindings();
                }));

            // Save the current live color into a preset slot. Persisted to .coc automatically.
            AddBinding(new TriggerBinding<int>(
                Mod.ModId,
                "SavePreset",
                slot =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    if (slot == 1)
                    {
                        settings.Preset1R = settings.OutlineR;
                        settings.Preset1G = settings.OutlineG;
                        settings.Preset1B = settings.OutlineB;
                        settings.Preset1A = settings.OutlineA;
                        settings.Preset1FillA = settings.FillA;
                    }
                    else if (slot == 2)
                    {
                        settings.Preset2R = settings.OutlineR;
                        settings.Preset2G = settings.OutlineG;
                        settings.Preset2B = settings.OutlineB;
                        settings.Preset2A = settings.OutlineA;
                        settings.Preset2FillA = settings.FillA;
                    }

                    settings.ApplyAndSave();
                    SyncValueBindings(); // updates Preset*Active + stored color bindings
                }));

            // Restore both preset slots to the original mod factory defaults.
            // Does not change the live swatch color.
            AddBinding(new TriggerBinding(
                Mod.ModId,
                "ResetPresetsToDefault",
                () =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    settings.Preset1R = 140f / 255f;
                    settings.Preset1G = 140f / 255f;
                    settings.Preset1B = 171f / 255f;
                    settings.Preset1A = 0.5f;
                    settings.Preset1FillA = 0f;
                    settings.Preset2R = 0.25f;
                    settings.Preset2G = 0.15f;
                    settings.Preset2B = 0.25f;
                    settings.Preset2A = 0.5f;
                    settings.Preset2FillA = 0f;
                    settings.ApplyAndSave();
                    SyncValueBindings();
                }));
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
        }

        private void RegisterValueBindings()
        {
            HoverPowerSettings? settings = Mod.Settings;
            m_OutlineRBinding = AddValueBinding("OutlineR", settings?.OutlineR ?? 0.502f);
            m_OutlineGBinding = AddValueBinding("OutlineG", settings?.OutlineG ?? 0.869f);
            m_OutlineBBinding = AddValueBinding("OutlineB", settings?.OutlineB ?? 1f);
            m_OutlineABinding = AddValueBinding("OutlineA", settings?.OutlineA ?? 0.855f);
            m_FillABinding = AddValueBinding("FillA", settings?.FillA ?? 0f);
            m_GuidelineOpacityBinding = AddValueBinding("GuidelineOpacityPercent", settings?.GuidelineOpacityPercent ?? 40);
            m_PanelOpenBinding = AddValueBinding("PanelOpen", s_PanelOpen);
            m_SurfaceToolAreasSuppressedBinding = AddValueBinding("SurfaceToolAreasSuppressed", SurfaceToolOverlaySystem.SuppressSurfaceToolAreas);
            m_VanillaOutlineActiveBinding = AddValueBinding("VanillaOutlineActive", IsVanillaOutlineActive());

            // Preset slot stored-color bindings (swatch previews + active-state).
            m_Preset1RBinding = AddValueBinding("Preset1R", settings?.Preset1R ?? 140f / 255f);
            m_Preset1GBinding = AddValueBinding("Preset1G", settings?.Preset1G ?? 140f / 255f);
            m_Preset1BBinding = AddValueBinding("Preset1B", settings?.Preset1B ?? 171f / 255f);
            m_Preset1ABinding = AddValueBinding("Preset1A", settings?.Preset1A ?? 0.5f);
            m_Preset1FillABinding = AddValueBinding("Preset1FillA", settings?.Preset1FillA ?? 0f);
            m_Preset2RBinding = AddValueBinding("Preset2R", settings?.Preset2R ?? 0.25f);
            m_Preset2GBinding = AddValueBinding("Preset2G", settings?.Preset2G ?? 0.15f);
            m_Preset2BBinding = AddValueBinding("Preset2B", settings?.Preset2B ?? 0.25f);
            m_Preset2ABinding = AddValueBinding("Preset2A", settings?.Preset2A ?? 0.5f);
            m_Preset2FillABinding = AddValueBinding("Preset2FillA", settings?.Preset2FillA ?? 0f);
            m_Preset1ActiveBinding = AddValueBinding("Preset1Active", IsPresetActive(1));
            m_Preset2ActiveBinding = AddValueBinding("Preset2Active", IsPresetActive(2));
        }

        private ValueBinding<T> AddValueBinding<T>(string name, T initialValue)
        {
            ValueBinding<T> binding = new ValueBinding<T>(Mod.ModId, name, initialValue);
            AddBinding(binding);
            return binding;
        }

        private void SyncValueBindings()
        {
            HoverPowerSettings? settings = Mod.Settings;
            UpdateIfChanged(m_OutlineRBinding, settings?.OutlineR ?? 0.502f);
            UpdateIfChanged(m_OutlineGBinding, settings?.OutlineG ?? 0.869f);
            UpdateIfChanged(m_OutlineBBinding, settings?.OutlineB ?? 1f);
            UpdateIfChanged(m_OutlineABinding, settings?.OutlineA ?? 0.855f);
            UpdateIfChanged(m_FillABinding, settings?.FillA ?? 0f);
            UpdateIfChanged(m_GuidelineOpacityBinding, settings?.GuidelineOpacityPercent ?? 40);
            UpdateIfChanged(m_PanelOpenBinding, s_PanelOpen);
            UpdateIfChanged(m_SurfaceToolAreasSuppressedBinding, SurfaceToolOverlaySystem.SuppressSurfaceToolAreas);
            UpdateIfChanged(m_VanillaOutlineActiveBinding, IsVanillaOutlineActive());

            // Preset stored colors + active flags
            UpdateIfChanged(m_Preset1RBinding, settings?.Preset1R ?? 140f / 255f);
            UpdateIfChanged(m_Preset1GBinding, settings?.Preset1G ?? 140f / 255f);
            UpdateIfChanged(m_Preset1BBinding, settings?.Preset1B ?? 171f / 255f);
            UpdateIfChanged(m_Preset1ABinding, settings?.Preset1A ?? 0.5f);
            UpdateIfChanged(m_Preset1FillABinding, settings?.Preset1FillA ?? 0f);
            UpdateIfChanged(m_Preset2RBinding, settings?.Preset2R ?? 0.25f);
            UpdateIfChanged(m_Preset2GBinding, settings?.Preset2G ?? 0.15f);
            UpdateIfChanged(m_Preset2BBinding, settings?.Preset2B ?? 0.25f);
            UpdateIfChanged(m_Preset2ABinding, settings?.Preset2A ?? 0.5f);
            UpdateIfChanged(m_Preset2FillABinding, settings?.Preset2FillA ?? 0f);
            UpdateIfChanged(m_Preset1ActiveBinding, IsPresetActive(1));
            UpdateIfChanged(m_Preset2ActiveBinding, IsPresetActive(2));
        }

        private static void UpdateIfChanged<T>(ValueBinding<T> binding, T value)
        {
            if (EqualityComparer<T>.Default.Equals(binding.value, value))
            {
                return;
            }

            binding.Update(value);
        }

        private void SetPanelOpen(bool open)
        {
            s_PanelOpen = open;
            UpdateIfChanged(m_PanelOpenBinding, s_PanelOpen);
        }

        private void ToggleSurfaceToolAreas()
        {
            SurfaceToolOverlaySystem.ToggleSuppression();
            UpdateIfChanged(m_SurfaceToolAreasSuppressedBinding, SurfaceToolOverlaySystem.SuppressSurfaceToolAreas);
        }

        private static bool IsVanillaOutlineActive()
        {
            HoverPowerSettings? settings = Mod.Settings;
            return settings != null
                && OutlineColorSystem.MatchesCapturedVanillaProfile(
                    settings.OutlineR,
                    settings.OutlineG,
                    settings.OutlineB,
                    settings.OutlineA,
                    settings.FillA);
        }

        // True when the live swatch exactly matches what's stored in that slot.
        private static bool IsPresetActive(int slot)
        {
            HoverPowerSettings? s = Mod.Settings;
            if (s == null) return false;

            if (slot == 1)
            {
                return ApproxEqual(s.OutlineR, s.Preset1R)
                    && ApproxEqual(s.OutlineG, s.Preset1G)
                    && ApproxEqual(s.OutlineB, s.Preset1B)
                    && ApproxEqual(s.OutlineA, s.Preset1A)
                    && ApproxEqual(s.FillA, s.Preset1FillA);
            }

            if (slot == 2)
            {
                return ApproxEqual(s.OutlineR, s.Preset2R)
                    && ApproxEqual(s.OutlineG, s.Preset2G)
                    && ApproxEqual(s.OutlineB, s.Preset2B)
                    && ApproxEqual(s.OutlineA, s.Preset2A)
                    && ApproxEqual(s.FillA, s.Preset2FillA);
            }

            return false;
        }

        private static bool ApproxEqual(float a, float b) => Math.Abs(a - b) < 0.0005f;

        private void InitializeKeybindActions()
        {
            m_TogglePanelAction = EnableAction(Mod.kTogglePanelActionName);
            m_ToggleSurfaceToolAreasAction = EnableAction(Mod.kToggleSurfaceToolAreasActionName);
        }

        private void RefreshKeybindActions()
        {
            if (m_TogglePanelAction == null)
            {
                m_TogglePanelAction = EnableAction(Mod.kTogglePanelActionName);
            }

            if (m_ToggleSurfaceToolAreasAction == null)
            {
                m_ToggleSurfaceToolAreasAction = EnableAction(Mod.kToggleSurfaceToolAreasActionName);
            }
        }

        // CWD-style: fetch the ProxyAction registered by the [SettingsUIKeyboardBinding] attribute
        // and flip shouldBeEnabled so it actually receives input. Returns null on miss.
        private static ProxyAction? EnableAction(string actionName)
        {
            try
            {
                ProxyAction? action = Mod.Settings?.GetAction(actionName);
                if (action != null)
                {
                    action.shouldBeEnabled = true;
                }
                return action;
            }
            catch (Exception ex)
            {
                LogUtils.WarnOnce(
                    "missing-keybind-" + actionName,
                    () => $"{Mod.ModTag} Keybinding '{actionName}' unavailable: {ex.GetType().Name}: {ex.Message}",
                    ex);
                return null;
            }
        }

        private static bool IsInGame()
        {
            return GameManager.instance != null && GameManager.instance.gameMode == GameMode.Game;
        }
    }
}
