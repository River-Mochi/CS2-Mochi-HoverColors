// <copyright file="HoverColorsUISystem.HoverToggle.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
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
