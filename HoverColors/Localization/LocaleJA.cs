// File: Localization/LocaleJA.cs
// Purpose: Japanese (ja-JP) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/ja-JP.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
    public sealed class LocaleJA : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleJA(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "æ“ä½œ" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "æƒ…å ±" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "ãƒ„ãƒ¼ãƒ«è‰²ã®å‹•ä½œ" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "ãƒ‘ãƒãƒ«" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "ã‚­ãƒ¼å‰²ã‚Šå½“ã¦" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "ã‚¬ã‚¤ãƒ‰ãƒ©ã‚¤ãƒ³" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "æ§ã’ã‚‹è¨€è‘‰" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ãƒ–ãƒ«ãƒ‰ãƒ¼ã‚¶ãƒ¼ + é“è·¯" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ãƒ–ãƒ«ãƒ‰ãƒ¼ã‚¶ãƒ¼ã¾ãŸã¯é“è·¯ãƒ„ãƒ¼ãƒ«ãŒæœ‰åŠ¹ãªé–“ã®ä¸€æ™‚çš„ãªã‚¢ã‚¦ãƒˆãƒ©ã‚¤ãƒ³è‰²ã‚’åˆ¶å¾¡ã—ã¾ã™ã€‚\n\n**1. ãŠã™ã™ã‚** ã¯ã€è§£ä½“ã«ã¯ã‚²ãƒ¼ãƒ ã®è­¦å‘Šè‰²ï¼ˆé»„è‰²ï¼‰ã€é“è·¯ã«ã¯å°‘ã—æŸ”ã‚‰ã‹ã„ãƒãƒ‹ãƒ©ãƒ–ãƒ«ãƒ¼ã‚’ä½¿ã„ã¾ã™ã€‚\n**2. ãƒãƒ‹ãƒ©ã®ãƒ„ãƒ¼ãƒ«è‰²** ã¯ã€ãƒ–ãƒ«ãƒ‰ãƒ¼ã‚¶ãƒ¼ã¾ãŸã¯é“è·¯ãƒ„ãƒ¼ãƒ«ãŒæœ‰åŠ¹ãªé–“ã€ã‚²ãƒ¼ãƒ æ¨™æº–ã®ãƒãƒ‹ãƒ©ãƒ–ãƒ«ãƒ¼ã«æˆ»ã—ã¾ã™ã€‚\n**3. ã‚«ã‚¹ã‚¿ãƒ è‰²ã‚’ç¶­æŒ** ã¯ã€é¸æŠžã—ãŸè‰²ã‚’ã™ã¹ã¦ã®å ´é¢ã§ä½¿ã„ã¾ã™ã€‚\n\nç›®çš„: ä¸€éƒ¨ã®ãƒ¦ãƒ¼ã‚¶ãƒ¼/ãƒ†ã‚¹ã‚¿ãƒ¼ã‹ã‚‰ã€è§£ä½“ä¸­ã«ã‚«ã‚¹ã‚¿ãƒ è‰²ãŒè¦‹ã¥ã‚‰ã„ã¨ã„ã†å£°ãŒã‚ã‚Šã¾ã—ãŸã€‚\nãƒ„ãƒ¼ãƒ«ä½¿ç”¨ä¸­ã«è¦‹ã‚„ã™ã„é«˜è¦–èªæ€§ã®è‰²ã‚’é¸ã¹ã‚‹ã‚ˆã†ã«ã—ã¾ã™ã€‚\nã‚«ãƒ©ãƒ¼ãƒ”ãƒƒã‚«ãƒ¼ã«è‡ªå‹•ä¿å­˜ã•ã‚ŒãŸã‚«ã‚¹ã‚¿ãƒ è‰²ã¯ä¸Šæ›¸ãã•ã‚Œã¾ã›ã‚“ã€‚" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. ãŠã™ã™ã‚" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. ãƒãƒ‹ãƒ©ã®ãƒ„ãƒ¼ãƒ«è‰²" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ã‚«ã‚¹ã‚¿ãƒ è‰²ã‚’ç¶­æŒ" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "é‡ãªã‚Šé …ç›®ã®ã‚¢ã‚¦ãƒˆãƒ©ã‚¤ãƒ³ã‚’æœ‰åŠ¹åŒ–" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<æœ‰åŠ¹åŒ–ã‚’ãŠã™ã™ã‚ã—ã¾ã™>\nã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã¾ãŸã¯ãƒãƒƒãƒˆãƒ¯ãƒ¼ã‚¯ã®é…ç½®ãŒé‡ãªã‚Šã«ã‚ˆã£ã¦ãƒ–ãƒ­ãƒƒã‚¯ã•ã‚ŒãŸã¨ãã€ã‚²ãƒ¼ãƒ æ¨™æº–ã®ã‚µãƒ¼ãƒ¢ãƒ³ãƒ¬ãƒƒãƒ‰ã®ã‚¢ã‚¦ãƒˆãƒ©ã‚¤ãƒ³ã‚’è¡¨ç¤ºã—ãŸã¾ã¾ã«ã—ã¾ã™ã€‚\nå°‚é–€ç”£æ¥­ã®è¾²å ´åŠå¾„ã‚¬ã‚¤ãƒ‰ãªã©ã®ã‚¨ãƒªã‚¢åˆ¶é™ã¯å¤‰æ›´ã—ã¾ã›ã‚“ã€‚\n\nã™ã¹ã¦ã®ãƒ–ãƒ«ãƒ‰ãƒ¼ã‚¶ãƒ¼ + é“è·¯ãƒ¢ãƒ¼ãƒ‰ã§æ©Ÿèƒ½ã—ã€ä¿å­˜æ¸ˆã¿ã®ã‚«ã‚¹ã‚¿ãƒ è‰²ã¯ä¸Šæ›¸ãã—ã¾ã›ã‚“ã€‚" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes ã«ã‚«ã‚¹ã‚¿ãƒ è‰²ã‚’ä½¿ã†" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<æœ‰åŠ¹åŒ–ã‚’ãŠã™ã™ã‚ã—ã¾ã™>\nãƒ•ã‚§ãƒ³ã‚¹ã€åž£æ ¹ã€ãƒžãƒ¼ã‚­ãƒ³ã‚°ãªã©ã®ãƒ¬ãƒ¼ãƒ³ç³» NetLane è©³ç´°ã‚¢ã‚¤ãƒ†ãƒ ã‚’é…ç½®ã™ã‚‹ã¨ãã€ä¿å­˜æ¸ˆã¿ã® HC è‰²/é€æ˜Žåº¦ã‚’ä½¿ã„ã¾ã™ã€‚\n\n- é€šå¸¸ã®é“è·¯ã¯ã€ãƒ‰ãƒ­ãƒƒãƒ—ãƒ€ã‚¦ãƒ³ã§é¸ã‚“ã ãƒ–ãƒ«ãƒ‰ãƒ¼ã‚¶ãƒ¼ + é“è·¯è¨­å®šã«å¾“ã„ã¾ã™ã€‚\n- ã“ã‚Œã‚‰ã®ãƒ„ãƒ¼ãƒ«ã«ã‚²ãƒ¼ãƒ æ¨™æº–ã®ãƒãƒ‹ãƒ©ãƒ–ãƒ«ãƒ¼ã‚’ä½¿ã‚ã›ãŸã„å ´åˆã¯ç„¡åŠ¹ã«ã—ã¦ãã ã•ã„ã€‚\n- æœ‰åŠ¹ãªå ´åˆã€é‡ãªã‚Šã‚¨ãƒ©ãƒ¼è‰²ãŒå¼•ãç¶šãå„ªå…ˆã•ã‚Œã¾ã™ï¼ˆãƒãƒ‹ãƒ©ã®ã‚¨ãƒ©ãƒ¼è‰² = ã‚µãƒ¼ãƒ¢ãƒ³ãƒ¬ãƒƒãƒ‰ï¼‰ã€‚" },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "æš—ã‚ã®ãƒ‘ãƒãƒ«" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "æœ‰åŠ¹ã«ã™ã‚‹ã¨ <æš—ã„ãƒ‘ãƒãƒ«> ã‚’ä½¿ã„ã¾ã™: ãƒ¬ã‚¬ã‚·ãƒ¼ UI ãƒ—ãƒ¬ã‚¤ãƒ¤ãƒ¼å‘ã‘ã§ã™ãŒã€æš—ã„ãƒ‘ãƒãƒ«ãŒå¥½ã¿ãªã‚‰ Modern UI ã§ã‚‚ä½¿ãˆã¾ã™ã€‚\nç„¡åŠ¹ã«ã™ã‚‹ã¨ <æ¨™æº–ãƒ‘ãƒãƒ«> ã‚’ä½¿ã„ã¾ã™: Hover Colors ç‹¬è‡ªã®åŠé€æ˜Žã‚¹ã‚¿ã‚¤ãƒ«ã§ã™ã€‚\n- ã‚ˆã‚Šæ˜Žã‚‹ãã€ã‚ˆã‚Šãƒ¢ãƒ€ãƒ³ãªè¦‹ãŸç›®ã€‚\n- æ–°ã—ã„ Modern UI ã‚’ä½¿ã†å¤šãã®ãƒ—ãƒ¬ã‚¤ãƒ¤ãƒ¼ã«ãŠã™ã™ã‚ã§ã™ã€‚\n\nä¸¡æ–¹è©¦ã—ã¦å¥½ã¿ã®æ–¹ã‚’é¸ã‚“ã§ãã ã•ã„ï¼å¤‰æ›´ã•ã‚Œã‚‹ã®ã¯ã“ã® Mod ãƒ‘ãƒãƒ«ã®èƒŒæ™¯ã ã‘ã§ã€ã‚²ãƒ¼ãƒ ã® UI ã¯å¤‰ã‚ã‚Šã¾ã›ã‚“ã€‚" },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ã‚¬ã‚¤ãƒ‰ãƒ©ã‚¤ãƒ³ä¸é€æ˜Žåº¦ï¼ˆã‚¢ãƒ«ãƒ•ã‚¡ï¼‰" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ç ´ç·šã®ä½ç½®åˆã‚ã›ã‚¬ã‚¤ãƒ‰ã®ä¸é€æ˜Žåº¦ã‚’åˆ¶å¾¡ã—ã¾ã™ã€‚é“è·¯ã€ãƒ•ã‚§ãƒ³ã‚¹ã€ãƒ—ãƒ­ãƒƒãƒ—ãªã©ã®é…ç½®æ™‚ã«ä¾¿åˆ©ã§ã™ã€‚\n\n**100%** ã¯ãƒãƒ‹ãƒ©ã®æ—¢å®šè¡¨ç¤ºã‚’ç¶­æŒã—ã¾ã™ã€‚\n**ä½Žãã™ã‚‹** ã¨ã‚¬ã‚¤ãƒ‰ãŒã‚ˆã‚Šé€æ˜Žã«ãªã‚Šã¾ã™ã€‚\n**0%** ã¯å®Œå…¨ã«éžè¡¨ç¤ºã«ã—ã¾ã™ - <éžæŽ¨å¥¨>ã€‚\n15%ä»¥ä¸Šã‚’æŽ¨å¥¨ã—ã¾ã™ã€‚ãã‚Œä»¥ä¸‹ã§ã¯çŠ¶æ³ãŒè¦‹ãˆã«ãããªã‚Šã¾ã™ã€‚\nåŒã˜ã‚¹ãƒ©ã‚¤ãƒ€ãƒ¼ãŒè¡—ä¸­ã® Mod ãƒ‘ãƒãƒ«ã«ã‚‚ã‚ã‚Šã¾ã™ã€‚ä¸¡æ–¹ã¯åŒæœŸã•ã‚Œã¦ã„ã¾ã™ã€‚\nã“ã¡ã‚‰ã‚’å¤‰æ›´ã™ã‚‹ã¨ã€è¡—ä¸­ã®ã‚¹ãƒ©ã‚¤ãƒ€ãƒ¼ã‚‚åŒæ™‚ã«å¤‰æ›´ã•ã‚Œã¾ã™ã€‚" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "ãƒ¡ã‚¤ãƒ³ãƒ‘ãƒãƒ«ã‚’é–‹ã/é–‰ã˜ã‚‹" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "è¡—ä¸­ã® Hover ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆè‰²ãƒ‘ãƒãƒ«ã‚’é–‹ã / é–‰ã˜ã‚‹ãƒ›ãƒƒãƒˆã‚­ãƒ¼ã§ã™ã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors ãƒ‘ãƒãƒ«ã‚’åˆ‡ã‚Šæ›¿ãˆ" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface ãƒ„ãƒ¼ãƒ«ã®ãƒ—ãƒ¬ãƒ“ãƒ¥ãƒ¼ã‚’ã‚ªãƒ³/ã‚ªãƒ•" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface ã‚’é…ç½®ä¸­ã€ã‚¢ã‚¯ãƒ†ã‚£ãƒ–ãª Surface ãƒ„ãƒ¼ãƒ«å¢ƒç•Œãƒ—ãƒ¬ãƒ“ãƒ¥ãƒ¼ç·šã‚’éš ã™ã€ã¾ãŸã¯æˆ»ã™ãƒ›ãƒƒãƒˆã‚­ãƒ¼ã§ã™ã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface ãƒ„ãƒ¼ãƒ« ãƒ—ãƒ¬ãƒ“ãƒ¥ãƒ¼ãƒ¬ã‚¤ãƒ¤ãƒ¼ On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "ãƒ—ãƒªã‚»ãƒƒãƒˆ 1+2 ã‚’åˆ‡ã‚Šæ›¿ãˆ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "ãƒ—ãƒªã‚»ãƒƒãƒˆã‚¹ãƒ­ãƒƒãƒˆ 1 ã¨ã‚¹ãƒ­ãƒƒãƒˆ 2 ã‚’åˆ‡ã‚Šæ›¿ãˆã‚‹ãƒ›ãƒƒãƒˆã‚­ãƒ¼ã§ã™ã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "ãƒ—ãƒªã‚»ãƒƒãƒˆ 1 ã¨ 2 ã‚’åˆ‡ã‚Šæ›¿ãˆ" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "ä½œè€…ã® Paradox Mods ãƒšãƒ¼ã‚¸ã‚’é–‹ãã¾ã™ã€‚" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "æ„›ã™ã‚‹ Mochi ã®æ€ã„å‡ºã«ã€‚" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "ã“ã® Mod ã¯ Mochi ã«æ§ã’ã‚‰ã‚Œã¦ã„ã¾ã™ã€‚Mochi ã¯7æ­³ã§è¿Žãˆã‚‰ã‚ŒãŸå¤§åˆ‡ãªãƒ¯ãƒ³ã¡ã‚ƒã‚“ã§ã€\n13å¹´é–“ãŸãã•ã‚“ã®æ„›ã¨å–œã³ã‚’ãã‚Œã¾ã—ãŸã€‚ã“ã® Mod ã¯ Mochi ãªã—ã«ã¯ç”Ÿã¾ã‚Œã¾ã›ã‚“ã§ã—ãŸã€‚" },
            };
        }

        public void Unload()
        {
        }
    }
}
