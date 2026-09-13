// <copyright file="Setting.Helpers.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// This notice MUST be kept with copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Settings/Setting.Helpers.cs
// Purpose: Small Options UI and external-link helpers for HoverColorsSettings.

namespace HoverColors
{
    using System;               // Exception handling
    using CS2Shared.RiverMochi; // LogUtils
    using Game.UI.Widgets;      // DropdownItem for Options UI
    using UnityEngine;          // Application.OpenURL

    public partial class HoverColorsSettings
    {
        public DropdownItem<int>[] GetToolColorModeItems()
        {
            return new[]
            {
                new DropdownItem<int>
                {
                    value = kToolColorModeRecommended,
                    displayName = GetToolColorModeLocaleID("Recommended"),
                },
                new DropdownItem<int>
                {
                    value = kToolColorModeVanilla,
                    displayName = GetToolColorModeLocaleID("Vanilla"),
                },
                new DropdownItem<int>
                {
                    value = kToolColorModeCustom,
                    displayName = GetToolColorModeLocaleID("Custom"),
                },
            };
        }

        public DropdownItem<int>[] GetPanelStyleItems()
        {
            return new[]
            {
                new DropdownItem<int>
                {
                    value = kPanelStyleDark,
                    displayName = GetPanelStyleLocaleID("Dark"),
                },
                new DropdownItem<int>
                {
                    value = kPanelStyleStandard,
                    displayName = GetPanelStyleLocaleID("Glass"),
                },
            };
        }

        public string GetPanelStyleLocaleID(string valueName)
        {
            return "Options[" + id + ".PanelStyle." + valueName + "]";
        }

        public string GetToolColorModeLocaleID(string valueName)
        {
            return "Options[" + id + ".ToolColorMode." + valueName + "]";
        }

        private static void TryOpenUrl(string url)
        {
            try
            {
                Application.OpenURL(url);
            }
            catch (Exception ex)
            {
                LogUtils.WarnOnce(
                    "open-url-" + url,
                    () => $"Failed to open URL '{url}': {ex.GetType().Name}: {ex.Message}",
                    ex);
            }
        }
    }
}
