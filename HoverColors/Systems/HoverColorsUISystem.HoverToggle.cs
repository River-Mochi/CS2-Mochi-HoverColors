// <copyright file="HoverColorsUISystem.HoverToggle.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/HoverColorsUISystem.HoverToggle.cs
// Eye-button binding + optional unbound hotkey. The OFF state persists in HC settings.

namespace HoverColors.UI
{
    using Colossal.UI.Binding;
    using Game.Input;
    using HoverColors.Settings;

    public partial class HoverColorsUISystem
    {
        private ProxyAction? m_ToggleHoverHighlightsAction;
        private ValueBinding<bool> m_HoverHighlightsSuppressedBinding = null!;

        private void RegisterHoverToggleBindings()
        {
            HoverColorsSettings? settings = Mod.Settings;

            m_HoverHighlightsSuppressedBinding = AddValueBinding(
                "HoverHighlightsSuppressed",
                settings?.HoverHighlightsSuppressed ?? false);

            AddBinding(new TriggerBinding(Mod.ModId, "ToggleHoverHighlights", ToggleHoverHighlights));
            m_ToggleHoverHighlightsAction = EnableAction(Mod.kToggleHoverHighlightsActionName);
        }

        private void UpdateHoverToggleHotkey()
        {
            if (m_ToggleHoverHighlightsAction == null)
            {
                m_ToggleHoverHighlightsAction = EnableAction(Mod.kToggleHoverHighlightsActionName);
            }

            if (m_ToggleHoverHighlightsAction?.WasReleasedThisFrame() == true)
            {
                ToggleHoverHighlights();
            }
        }

        private void ToggleHoverHighlights()
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
