// <copyright file="HoverColorsUISystem.HoverToggle.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/HoverColorsUISystem.HoverToggle.cs
// Eye-button binding + optional unbound hotkey. OFF state persists in HC settings.

namespace HoverColors.UI
{
    using Colossal.UI.Binding;
    using Game.Input;

    public partial class HoverColorsUISystem
    {
        private ProxyAction? m_ToggleHighlightsAction;
        private ValueBinding<bool> m_HoverHighlightsSuppressedBinding = null!;

        private void RegisterHoverToggleBindings()
        {
            HoverColorsSettings? settings = Mod.Settings;

            m_HoverHighlightsSuppressedBinding = AddValueBinding(
                "HoverHighlightsSuppressed",
                settings?.HoverHighlightsSuppressed ?? false);

            AddBinding(new TriggerBinding(Mod.ModId, "ToggleHighlights", ToggleHighlights));
            m_ToggleHighlightsAction = EnableAction(Mod.kToggleHighlightsActionName);
        }

        private void UpdateHoverToggleHotkey()
        {
            if (m_ToggleHighlightsAction == null)
            {
                m_ToggleHighlightsAction = EnableAction(Mod.kToggleHighlightsActionName);
            }

            if (m_ToggleHighlightsAction?.WasReleasedThisFrame() == true)
            {
                ToggleHighlights();
            }
        }

        // The Options reset button writes HoverHighlightsSuppressed directly, so the panel needs a
        // sync path that does not go through ToggleHighlights().
        private void SyncHoverToggleBinding()
        {
            UpdateIfChanged(m_HoverHighlightsSuppressedBinding, Mod.Settings?.HoverHighlightsSuppressed ?? false);
        }

        private void ToggleHighlights()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null)
            {
                return;
            }

            settings.HoverHighlightsSuppressed = !settings.HoverHighlightsSuppressed;
            settings.ApplyAndSave();
            UpdateIfChanged(m_HoverHighlightsSuppressedBinding, settings.HoverHighlightsSuppressed);
        }
    }
}
