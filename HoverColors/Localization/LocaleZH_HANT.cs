// File: Localization/LocaleZH_HANT.cs
// Purpose: Traditional Chinese (zh-HANT) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/zh-HANT.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
    public sealed class LocaleZH_HANT : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleZH_HANT(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "é—œæ–¼" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "å·¥å…·é¡è‰²è¡Œç‚º" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "é¢æ¿" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "æŒ‰éµç¶å®š" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "è¼”åŠ©ç·š" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "ç»è©ž" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "æŽ¨åœŸæ©Ÿ + é“è·¯" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "æŽ§åˆ¶æŽ¨åœŸæ©Ÿæˆ–é“è·¯å·¥å…·å•Ÿç”¨æ™‚çš„è‡¨æ™‚è¼ªå»“é¡è‰²ã€‚\n\n" +
                    "**1. æŽ¨è–¦** æ‹†é™¤ç”¨éŠæˆ²è­¦å‘Šè‰²ï¼ˆé»ƒè‰²ï¼‰ï¼Œé“è·¯ç”¨è¼ƒæŸ”å’Œçš„åŽŸç‰ˆè—è‰²ã€‚\n" +
                    "**2. åŽŸç‰ˆå·¥å…·é¡è‰²** ä½¿ç”¨æŽ¨åœŸæ©Ÿæˆ–é“è·¯å·¥å…·æ™‚ï¼Œæ¢å¾©éŠæˆ²åŽŸç‰ˆè—è‰²ã€‚\n" +
                    "**3. ä¿æŒæˆ‘çš„è‡ªè¨‚é¡è‰²** åˆ°è™•éƒ½ç”¨ä½ é¸çš„é¡è‰²ã€‚\n\n" +

                    "ç”¨é€”ï¼šæœ‰äº›çŽ©å®¶æ‹†é™¤æ™‚è¦ºå¾—è‡ªè¨‚é¡è‰²ä¸å¥½çœ‹æ¸…ã€‚\n" +
                    "é€™è£¡æä¾›å·¥å…·ä½¿ç”¨æ™‚æ›´é†’ç›®çš„é¡è‰²ã€‚\n" +
                    "ä¸æœƒè¦†è“‹è‰²å½©é¸æ“‡å™¨ä¸­è‡ªå‹•å„²å­˜çš„è‡ªè¨‚é¡è‰²ã€‚"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. æŽ¨è–¦" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. åŽŸç‰ˆå·¥å…·é¡è‰²" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ä¿æŒæˆ‘çš„è‡ªè¨‚é¡è‰²" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "å•Ÿç”¨é‡ç–Šç‰©å“è¼ªå»“" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<å»ºè­°å•Ÿç”¨>\n" +
                    "ç‰©ä»¶æˆ–ç¶²è·¯æ”¾ç½®è¢«é‡ç–Šç‰©å“æ“‹ä½æ™‚ï¼Œä¿ç•™éŠæˆ²åŽŸç‰ˆé®­ç´…è‰²è¼ªå»“ã€‚\n" +
                    "å€åŸŸé™åˆ¶ï¼Œä¾‹å¦‚å°ˆæ¥­å·¥æ¥­è¾²å ´åŠå¾‘è¼”åŠ©ç·šï¼Œæœƒä¿æŒåŽŸæ¨£ã€‚\n\n" +
                    "é©ç”¨æ‰€æœ‰æŽ¨åœŸæ©Ÿ + é“è·¯æ¨¡å¼ï¼Œä¹Ÿä¸æœƒè¦†è“‹å·²å„²å­˜çš„è‡ªè¨‚é¡è‰²ã€‚"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "å…è¨± NetLanes ä½¿ç”¨è‡ªè¨‚é¡è‰²" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<å»ºè­°å•Ÿç”¨>\n" +
                    "æ”¾ç½® NetLane ç´°ç¯€ç‰©ä»¶æ™‚ï¼Œä½¿ç”¨ä½ å„²å­˜çš„ HC é¡è‰²/é€æ˜Žåº¦ï¼Œä¾‹å¦‚åœæ¬„ã€æ¨¹ç±¬ã€æ¨™ç·šå’Œé¡žä¼¼çš„è»Šé“å·¥å…·ã€‚\n\n" +
                    "- ä¸€èˆ¬é“è·¯ä»ä¾ç…§ä½ åœ¨ä¸‹æ‹‰é¸å–®é¸çš„æŽ¨åœŸæ©Ÿ + é“è·¯è¨­å®šã€‚\n" +
                    "- è‹¥æƒ³è®“é€™äº›å·¥å…·ä½¿ç”¨éŠæˆ²åŽŸç‰ˆè—è‰²è¼ªå»“ï¼Œè«‹é—œé–‰æ­¤é¸é …ã€‚\n" +
                    "- å•Ÿç”¨æ™‚ï¼Œé‡ç–ŠéŒ¯èª¤é¡è‰²ä»æœƒå„ªå…ˆï¼ˆåŽŸç‰ˆéŒ¯èª¤è‰² = é®­ç´…è‰²ï¼‰ã€‚"
                },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "è¼ƒæ·±è‰²é¢æ¿" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "å•Ÿç”¨ä½¿ç”¨ <æ·±è‰²é¢æ¿>ï¼šçµ¦èˆŠç‰ˆ UI çŽ©å®¶ç”¨ï¼›å¦‚æžœä½ å–œæ­¡æ·±è‰²é¢æ¿ï¼ŒModern UI ä¹Ÿå¯ç”¨ã€‚\n" +
                    "é—œé–‰ä½¿ç”¨ <æ¨™æº–é¢æ¿>ï¼šHover Colors è‡ªè¨‚åŠé€æ˜Žæ¨£å¼ã€‚\n" +
                    "- è¼ƒæ˜Žäº®ã€è¼ƒç¾ä»£ã€‚\n" +
                    "- é©åˆå¤§å¤šæ•¸ä½¿ç”¨æ–°ç‰ˆ Modern UI çš„çŽ©å®¶ã€‚\n\n" +
                    "å…©ç¨®éƒ½è©¦è©¦çœ‹ï¼é€™åªæ”¹æœ¬æ¨¡çµ„é¢æ¿èƒŒæ™¯ï¼Œä¸æœƒæ”¹éŠæˆ² UIã€‚"
                },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "è¼”åŠ©ç·šé€æ˜Žåº¦ï¼ˆAlphaï¼‰" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)),
                    "æŽ§åˆ¶è™›ç·šå°é½Šè¼”åŠ©ç·šé€æ˜Žåº¦ï¼Œæ”¾ç½®é“è·¯ã€åœæ¬„ã€é“å…·ç­‰æ™‚å¾ˆæœ‰ç”¨ã€‚\n\n" +
                    "**100%** ä¿æŒåŽŸç‰ˆé è¨­å¤–è§€ã€‚\n" +
                    "**é™ä½Ž** è®“è¼”åŠ©ç·šæ›´é€æ˜Žã€‚\n" +
                    "**0%** å®Œå…¨éš±è— - <ä¸å»ºè­°>ã€‚\n" +
                    "å»ºè­°ä¿æŒ 15% ä»¥ä¸Šï¼Œä¸ç„¶å¾ˆé›£çœ‹æ¸…æ¥šã€‚\n" +
                    "åŸŽå¸‚æ¨¡çµ„é¢æ¿ä¹Ÿæœ‰åŒä¸€å€‹æ»‘æ¡¿ã€‚å…©é‚ŠæœƒåŒæ­¥ï¼›\n" +
                    "æ”¹é€™å€‹ï¼ŒåŸŽå¸‚è£¡çš„ä¹Ÿæœƒä¸€èµ·æ”¹ã€‚"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "é–‹å•Ÿ/é—œé–‰ä¸»é¢æ¿" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "é–‹å•Ÿ / é—œé–‰åŸŽå¸‚å…§ Hover ç‰©ä»¶é¡è‰²é¢æ¿çš„å¿«æ·éµã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "åˆ‡æ› Hover Colors é¢æ¿" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "åˆ‡æ› Surface å·¥å…·é è¦½" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "æ”¾ç½® Surface æ™‚ï¼Œç”¨å¿«æ·éµéš±è—æˆ–æ¢å¾©ç›®å‰çš„ Surface å·¥å…·é‚Šç•Œé è¦½ç·šã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface å·¥å…·é è¦½å±¤ On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "åˆ‡æ›é è¨­ 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "åœ¨é è¨­æ§½ 1 å’Œæ§½ 2 ä¹‹é–“åˆ‡æ›çš„å¿«æ·éµã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName),
                    "åœ¨é è¨­ 1 å’Œ 2 ä¹‹é–“åˆ‡æ›" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "é–‹å•Ÿä½œè€…çš„ Paradox Mods é é¢ã€‚" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "è¬¹ä»¥æ­¤ç´€å¿µ Mochiã€‚"
                    },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "æ­¤æ¨¡çµ„ç»çµ¦ Mochiã€‚å¥¹æ˜¯ä¸€éš»æ·±å—å–œæ„›çš„ç‹—ç‹—ï¼Œ7 æ­²æ™‚è¢«é ˜é¤Šï¼Œ\n" +
                    "å¸¶ä¾†äº† 13 å¹´çš„æ„›èˆ‡å¿«æ¨‚ã€‚æ²’æœ‰ Mochiï¼Œå°±ä¸æœƒæœ‰é€™å€‹æ¨¡çµ„ã€‚"
                    },
            };
        }

        public void Unload()
        {
        }
    }
}
