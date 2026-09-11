// <copyright file="LocaleEN.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleEN.cs
// Purpose: English (en-US) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/en-US.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleEN : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleEN(HoverColorsSettings settings)
        {
            m_Settings = settings;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            string title = Mod.ModName;
            if (!string.IsNullOrEmpty(Mod.ModVersion))
            {
                title += " (" + Mod.ModVersion + ")";
            }

            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), title },

                // Tabs
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Actions" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Key Bindings" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "About" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Tool Color Behavior" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Reset" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Key bindings" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedication" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + Roads" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Controls temporary outline colors while bulldozer or road tools are active.\n" +
                    "\n" +
                    "**1. Recommended** uses the game's Warning color (yellow) for demolition and a softer vanilla blue for roads.\n" +
                    "**2. Vanilla tool colors** restores the game's normal vanilla blue while bulldoze or road tools are active.\n" +
                    "**3. Keep my custom color** uses your chosen color everywhere.\n" +
                    "\n" +
                    "Purpose: some users/testers find their custom color hard to see while bulldozing.\n" +
                    "This offers options for high visibility colors during tool usage.\n" +
                    "This does not overwrite your automatically saved custom color in the color picker."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recommended" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla tool colors" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Keep my custom color" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Enable Overlapping items outline" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Enabled is recommended>\n" +
                    "Keeps the game's vanilla salmon red outline visible when object or network placement is blocked by overlapping items.\n" +
                    "Area limits, like Specialized Industry farm radius guides, are left alone.\n" +
                    "\n" +
                    "Works with all Bulldozer + Roads modes and does not overwrite your saved custom color."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Allow custom colors for NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Enabled is recommended>\n" +
                    "Use your saved HC color/transparency while placing NetLane detail items such as fences, hedges, markings, and similar lane-based tools.\n" +
                    "\n" +
                    "- Normal roads still follow the Bulldozer + Roads setting you picked from the drop-down list.\n" +
                    "- Disable this if you want those tools to use the game's vanilla blue outline color instead.\n" +
                    "- Overlapping error color still wins when enabled (vanilla error color = salmon red)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Darker panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Enabled = <Dark panel>: made for Legacy UI players; can also be used in Modern UI if you like a darker panel.\n" +
                    "Disabled = <Standard panel>: custom translucent Hover Colors style.\n" +
                    "- Lighter, more modern look.\n" +
                    "- Best for most players using the new Modern game UI.\n" +
                    "\n" +
                    "Try both and see which you prefer! This only changes the background of this mod panel and not the game's UI."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Panel opacity" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "How solid the in-city panel background is.\n" +
                    "\n" +
                    "**30% Glass** is the most see-through and clear look.\n" +
                    "**100%** is more solid with slight transparency.\n" +
                    "\n" +
                    "Only the background changes. Text, icons and swatches stay fully readable at every setting.\n" +
                    "\n" +
                    "**Slider works for Standard panel only.** Dark panel uses the game's own panel surface, so it follows the game's Interface Opacity setting instead.\n" +
                    "Dark works well in Legacy or Modern UI. Standard works best in Modern UI."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Show Tooltips (recommended)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Keep this ON> is recommended for most players.\n" +
                    "Shows short help when hovering Hover Colors buttons.\n" +
                    "If disabled, click the title-bar Info (i) button to turn them back on. Or this checkbox at any time for tooltips on.\n" +
                    "To prevent mishaps: tooltips can only be disabled inside this Options menu."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Reset to Mod Defaults" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Puts every Hover Colors setting back to a fresh install: colors, outline thickness, tool color behavior, guidelines, panel options and tooltips.\n" +
                    "\n" +
                    "**This also wipes your saved presets (Set A and Set B).**\n" +
                    "\n" +
                    "Keybindings are not affected.\n" +
                    "This is like installing Hover Colors for the first time."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Reset every Hover Colors setting to a fresh install?\n\nYour saved presets (Set A and Set B) will be erased." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Reset Colors to Vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Hands the game its own look back: outline, owner highlight, fill, outline thickness, guidelines and districts.\n" +
                    "\n" +
                    "Everything else stays as you set it - Bulldozer/Roads behavior, tool preview areas, saved presets, panel options and keybindings.\n" +
                    "\n" +
                    "Note: this mod can be removed without any reset. Highlights go back to game defaults on their own.\n"+
                    "This is just a Quick Reset button to game color defaults."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Reset the colors this mod controls back to the game's own look?\n\nPresets, tool behavior and panel options are kept." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Main panel open/close" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Hotkey shortcut to <open / close> the in-city Color Panel." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Toggle Hover Colors panel" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Quick Eyeball On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Optional hotkey for the title-bar Eyeball button: instant Highlight + Fill color Off/On.\n" +
                    "Ships unbound to avoid key conflicts." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Quick Eyeball On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Toggle Surface tool previews on/off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Hotkey shortcut to <hide or show> active Surface tool boundary preview lines while placing surfaces." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface tool preview layer On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Toggle presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Hotkey shortcut to flip between\n" +
                    "<preset slot 1 and slot 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Toggle between presets 1 and 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochi's Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Open the author's Paradox Mods page.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "In loving memory of Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "This mod is dedicated to Mochi.\n" +
                    "She was a beloved doggie, adopted at age 7,\n" +
                    "and gave 13 years of love and joy.\n" +
                    "This mod would not be possible without Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
