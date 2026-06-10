// File: Localization/LocaleKO.cs
// Purpose: Korean (ko-KR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/ko-KR.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
    public sealed class LocaleKO : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleKO(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "ë™ìž‘" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "ì •ë³´" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "ë„êµ¬ ìƒ‰ìƒ ë™ìž‘" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "íŒ¨ë„" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "í‚¤ ë°”ì¸ë”©" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "ê°€ì´ë“œë¼ì¸" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "í—Œì •" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ë¶ˆë„ì € + ë„ë¡œ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ë¶ˆë„ì € ë˜ëŠ” ë„ë¡œ ë„êµ¬ê°€ í™œì„±í™”ë˜ì–´ ìžˆì„ ë•Œ ìž„ì‹œ ìœ¤ê³½ì„  ìƒ‰ìƒì„ ì œì–´í•©ë‹ˆë‹¤.\n\n**1. ê¶Œìž¥**ì€ ì² ê±°ì—ëŠ” ê²Œìž„ì˜ ê²½ê³  ìƒ‰ìƒ(ë…¸ëž€ìƒ‰)ì„, ë„ë¡œì—ëŠ” ë” ë¶€ë“œëŸ¬ìš´ ë°”ë‹ë¼ íŒŒëž€ìƒ‰ì„ ì‚¬ìš©í•©ë‹ˆë‹¤.\n**2. ë°”ë‹ë¼ ë„êµ¬ ìƒ‰ìƒ**ì€ ë¶ˆë„ì € ë˜ëŠ” ë„ë¡œ ë„êµ¬ê°€ í™œì„±í™”ë˜ì–´ ìžˆì„ ë•Œ ê²Œìž„ì˜ ê¸°ë³¸ ë°”ë‹ë¼ íŒŒëž€ìƒ‰ìœ¼ë¡œ ë˜ëŒë¦½ë‹ˆë‹¤.\n**3. ë‚´ ì‚¬ìš©ìž ìƒ‰ìƒ ìœ ì§€**ëŠ” ì„ íƒí•œ ìƒ‰ìƒì„ ëª¨ë“  ê³³ì— ì‚¬ìš©í•©ë‹ˆë‹¤.\n\nëª©ì : ì¼ë¶€ ì‚¬ìš©ìž/í…ŒìŠ¤í„°ëŠ” ì² ê±° ì¤‘ ì‚¬ìš©ìž ìƒ‰ìƒì´ ìž˜ ë³´ì´ì§€ ì•ŠëŠ”ë‹¤ê³  ëŠê¼ˆìŠµë‹ˆë‹¤.\në„êµ¬ ì‚¬ìš© ì¤‘ ìž˜ ë³´ì´ëŠ” ê³ ì‹œì¸ì„± ìƒ‰ìƒ ì˜µì…˜ì„ ì œê³µí•©ë‹ˆë‹¤.\nì»¬ëŸ¬ í”¼ì»¤ì— ìžë™ ì €ìž¥ëœ ì‚¬ìš©ìž ìƒ‰ìƒì€ ë®ì–´ì“°ì§€ ì•ŠìŠµë‹ˆë‹¤." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. ê¶Œìž¥" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. ë°”ë‹ë¼ ë„êµ¬ ìƒ‰ìƒ" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ë‚´ ì‚¬ìš©ìž ìƒ‰ìƒ ìœ ì§€" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "ê²¹ì¹˜ëŠ” í•­ëª© ìœ¤ê³½ì„  í™œì„±í™”" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<í™œì„±í™” ê¶Œìž¥>\nì˜¤ë¸Œì íŠ¸ ë˜ëŠ” ë„¤íŠ¸ì›Œí¬ ë°°ì¹˜ê°€ ê²¹ì¹˜ëŠ” í•­ëª© ë•Œë¬¸ì— ë§‰íž ë•Œ ê²Œìž„ì˜ ë°”ë‹ë¼ ì—°ì–´ìƒ‰ ë¹¨ê°„ ìœ¤ê³½ì„ ì„ ê³„ì† í‘œì‹œí•©ë‹ˆë‹¤.\nì „ë¬¸í™” ì‚°ì—… ë†ìž¥ ë°˜ê²½ ê°€ì´ë“œ ê°™ì€ ì˜ì—­ ì œí•œì€ ê·¸ëŒ€ë¡œ ë‘¡ë‹ˆë‹¤.\n\nëª¨ë“  ë¶ˆë„ì € + ë„ë¡œ ëª¨ë“œì—ì„œ ìž‘ë™í•˜ë©° ì €ìž¥ëœ ì‚¬ìš©ìž ìƒ‰ìƒì„ ë®ì–´ì“°ì§€ ì•ŠìŠµë‹ˆë‹¤." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanesì— ì‚¬ìš©ìž ìƒ‰ìƒ í—ˆìš©" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<í™œì„±í™” ê¶Œìž¥>\nìš¸íƒ€ë¦¬, ìƒìš¸íƒ€ë¦¬, í‘œì‹œ ë“± ë ˆì¸ ê¸°ë°˜ NetLane ì„¸ë¶€ í•­ëª©ì„ ë°°ì¹˜í•  ë•Œ ì €ìž¥ëœ HC ìƒ‰ìƒ/íˆ¬ëª…ë„ë¥¼ ì‚¬ìš©í•©ë‹ˆë‹¤.\n\n- ì¼ë°˜ ë„ë¡œëŠ” ë“œë¡­ë‹¤ìš´ì—ì„œ ì„ íƒí•œ ë¶ˆë„ì € + ë„ë¡œ ì„¤ì •ì„ ê³„ì† ë”°ë¦…ë‹ˆë‹¤.\n- í•´ë‹¹ ë„êµ¬ì— ê²Œìž„ì˜ ë°”ë‹ë¼ íŒŒëž€ ìœ¤ê³½ì„ ì„ ì‚¬ìš©í•˜ê³  ì‹¶ìœ¼ë©´ ë¹„í™œì„±í™”í•˜ì„¸ìš”.\n- í™œì„±í™”ëœ ê²½ìš° ê²¹ì¹¨ ì˜¤ë¥˜ ìƒ‰ìƒì´ ê³„ì† ìš°ì„ í•©ë‹ˆë‹¤(ë°”ë‹ë¼ ì˜¤ë¥˜ ìƒ‰ìƒ = ì—°ì–´ìƒ‰ ë¹¨ê°•)." },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "ë” ì–´ë‘ìš´ íŒ¨ë„" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "í™œì„±í™”í•˜ë©´ <ì–´ë‘ìš´ íŒ¨ë„>ì„ ì‚¬ìš©í•©ë‹ˆë‹¤: ë ˆê±°ì‹œ UI í”Œë ˆì´ì–´ìš©ìœ¼ë¡œ ë§Œë“¤ì—ˆì§€ë§Œ, ë” ì–´ë‘ìš´ íŒ¨ë„ì„ ì„ í˜¸í•œë‹¤ë©´ Modern UIì—ì„œë„ ì‚¬ìš©í•  ìˆ˜ ìžˆìŠµë‹ˆë‹¤.\në¹„í™œì„±í™”í•˜ë©´ <í‘œì¤€ íŒ¨ë„>ì„ ì‚¬ìš©í•©ë‹ˆë‹¤: Hover Colorsì˜ ì‚¬ìš©ìž ì§€ì • ë°˜íˆ¬ëª… ìŠ¤íƒ€ì¼ìž…ë‹ˆë‹¤.\n- ë” ë°ê³  í˜„ëŒ€ì ì¸ ëŠë‚Œ.\n- ìƒˆ Modern ê²Œìž„ UIë¥¼ ì‚¬ìš©í•˜ëŠ” ëŒ€ë¶€ë¶„ì˜ í”Œë ˆì´ì–´ì—ê²Œ ì í•©í•©ë‹ˆë‹¤.\n\në‘˜ ë‹¤ ì‚¬ìš©í•´ ë³´ê³  ë” ë§ˆìŒì— ë“œëŠ” ê²ƒì„ ì„ íƒí•˜ì„¸ìš”! ì´ ì„¤ì •ì€ ì´ ëª¨ë“œ íŒ¨ë„ì˜ ë°°ê²½ë§Œ ë³€ê²½í•˜ë©° ê²Œìž„ UIëŠ” ë³€ê²½í•˜ì§€ ì•ŠìŠµë‹ˆë‹¤." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ê°€ì´ë“œë¼ì¸ ë¶ˆíˆ¬ëª…ë„(ì•ŒíŒŒ)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ë„ë¡œ, ìš¸íƒ€ë¦¬, ì†Œí’ˆ ë“±ì„ ë°°ì¹˜í•  ë•Œ ìœ ìš©í•œ ì ì„  ì •ë ¬ ê°€ì´ë“œì˜ ë¶ˆíˆ¬ëª…ë„ë¥¼ ì œì–´í•©ë‹ˆë‹¤.\n\n**100%**ëŠ” ë°”ë‹ë¼ ê¸°ë³¸ ëª¨ìŠµì„ ìœ ì§€í•©ë‹ˆë‹¤.\n**ë‚®ê²Œ** ì„¤ì •í•˜ë©´ ê°€ì´ë“œë¼ì¸ì´ ë” íˆ¬ëª…í•´ì§‘ë‹ˆë‹¤.\n**0%**ëŠ” ì™„ì „ížˆ ìˆ¨ê¹ë‹ˆë‹¤ - <ê¶Œìž¥í•˜ì§€ ì•ŠìŒ>.\në¬´ìŠ¨ ì¼ì´ ì¼ì–´ë‚˜ëŠ”ì§€ ë³´ê¸° ì–´ë µê¸° ë•Œë¬¸ì— 15% ì´ìƒì„ ê¶Œìž¥í•©ë‹ˆë‹¤.\nê°™ì€ ìŠ¬ë¼ì´ë”ê°€ ë„ì‹œ ëª¨ë“œ íŒ¨ë„ì—ë„ ìžˆìŠµë‹ˆë‹¤. ë‘˜ì€ ë™ê¸°í™”ë©ë‹ˆë‹¤.\nì´ ê°’ì„ ë°”ê¾¸ë©´ ë„ì‹œ ì•ˆì˜ ìŠ¬ë¼ì´ë”ë„ í•¨ê»˜ ë°”ë€ë‹ˆë‹¤." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "ë©”ì¸ íŒ¨ë„ ì—´ê¸°/ë‹«ê¸°" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "ë„ì‹œ ì•ˆ Hover ì˜¤ë¸Œì íŠ¸ ìƒ‰ìƒ íŒ¨ë„ì„ ì—´ê±°ë‚˜ ë‹«ëŠ” ë‹¨ì¶•í‚¤ìž…ë‹ˆë‹¤." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors íŒ¨ë„ ì „í™˜" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface ë„êµ¬ ë¯¸ë¦¬ë³´ê¸° ì¼œê¸°/ë„ê¸°" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface ë°°ì¹˜ ì¤‘ í™œì„± Surface ë„êµ¬ ê²½ê³„ ë¯¸ë¦¬ë³´ê¸° ì„ ì„ ìˆ¨ê¸°ê±°ë‚˜ ë³µì›í•˜ëŠ” ë‹¨ì¶•í‚¤ìž…ë‹ˆë‹¤." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface ë„êµ¬ ë¯¸ë¦¬ë³´ê¸° ë ˆì´ì–´ On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "í”„ë¦¬ì…‹ 1+2 ì „í™˜" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "í”„ë¦¬ì…‹ ìŠ¬ë¡¯ 1ê³¼ ìŠ¬ë¡¯ 2 ì‚¬ì´ë¥¼ ì „í™˜í•˜ëŠ” ë‹¨ì¶•í‚¤ìž…ë‹ˆë‹¤." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "í”„ë¦¬ì…‹ 1ê³¼ 2 ì „í™˜" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "ìž‘ì„±ìžì˜ Paradox Mods íŽ˜ì´ì§€ë¥¼ ì—½ë‹ˆë‹¤." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "ì‚¬ëž‘í•˜ëŠ” Mochië¥¼ ê¸°ë¦¬ë©°." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "ì´ ëª¨ë“œëŠ” Mochiì—ê²Œ ë°”ì¹©ë‹ˆë‹¤. MochiëŠ” 7ì‚´ì— ìž…ì–‘ëœ ì‚¬ëž‘ìŠ¤ëŸ¬ìš´ ê°•ì•„ì§€ì˜€ê³ ,\n13ë…„ ë™ì•ˆ ì‚¬ëž‘ê³¼ ê¸°ì¨ì„ ì£¼ì—ˆìŠµë‹ˆë‹¤. ì´ ëª¨ë“œëŠ” Mochi ì—†ì´ëŠ” ê°€ëŠ¥í•˜ì§€ ì•Šì•˜ìŠµë‹ˆë‹¤." },
            };
        }

        public void Unload()
        {
        }
    }
}
