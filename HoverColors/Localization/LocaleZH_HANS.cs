// File: Localization/LocaleZH_HANS.cs
// Purpose: Simplified Chinese (zh-HANS) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/zh-HANS.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
    public sealed class LocaleZH_HANS : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleZH_HANS(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "å…³äºŽ" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "å·¥å…·é¢œè‰²è¡Œä¸º" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "é¢æ¿" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "æŒ‰é”®ç»‘å®š" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "è¾…åŠ©çº¿" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "çŒ®è¯" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "æŽ¨åœŸæœº + é“è·¯" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "æŽ§åˆ¶æŽ¨åœŸæœºæˆ–é“è·¯å·¥å…·å¯ç”¨æ—¶çš„ä¸´æ—¶è½®å»“é¢œè‰²ã€‚\n\n**1. æŽ¨è**ï¼šæ‹†é™¤æ—¶ä½¿ç”¨æ¸¸æˆçš„è­¦å‘Šè‰²ï¼ˆé»„è‰²ï¼‰ï¼Œé“è·¯ä½¿ç”¨æ›´æŸ”å’Œçš„åŽŸç‰ˆè“è‰²ã€‚\n**2. åŽŸç‰ˆå·¥å…·é¢œè‰²**ï¼šæŽ¨åœŸæœºæˆ–é“è·¯å·¥å…·å¯ç”¨æ—¶æ¢å¤æ¸¸æˆæ­£å¸¸çš„åŽŸç‰ˆè“è‰²ã€‚\n**3. ä¿æŒæˆ‘çš„è‡ªå®šä¹‰é¢œè‰²**ï¼šæ‰€æœ‰åœ°æ–¹éƒ½ä½¿ç”¨ä½ é€‰æ‹©çš„é¢œè‰²ã€‚\n\nç”¨é€”ï¼šæœ‰äº›ç”¨æˆ·/æµ‹è¯•è€…è§‰å¾—æ‹†é™¤æ—¶è‡ªå®šä¹‰é¢œè‰²ä¸å®¹æ˜“çœ‹æ¸…ã€‚\nè¿™äº›é€‰é¡¹å¯åœ¨ä½¿ç”¨å·¥å…·æ—¶æä¾›é«˜å¯è§åº¦é¢œè‰²ã€‚\nä¸ä¼šè¦†ç›–é¢œè‰²é€‰æ‹©å™¨ä¸­è‡ªåŠ¨ä¿å­˜çš„è‡ªå®šä¹‰é¢œè‰²ã€‚" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. æŽ¨è" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. åŽŸç‰ˆå·¥å…·é¢œè‰²" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ä¿æŒæˆ‘çš„è‡ªå®šä¹‰é¢œè‰²" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "å¯ç”¨é‡å ç‰©å“è½®å»“" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<å»ºè®®å¯ç”¨>\nå½“å¯¹è±¡æˆ–ç½‘ç»œæ”¾ç½®è¢«é‡å ç‰©å“é˜»æŒ¡æ—¶ï¼Œä¿ç•™æ¸¸æˆåŽŸç‰ˆçš„é²‘çº¢è‰²è½®å»“ã€‚\nåŒºåŸŸé™åˆ¶ï¼ˆä¾‹å¦‚ä¸“ä¸šå·¥ä¸šå†œåœºåŠå¾„å¼•å¯¼çº¿ï¼‰ä¿æŒä¸å˜ã€‚\n\né€‚ç”¨äºŽæ‰€æœ‰æŽ¨åœŸæœº + é“è·¯æ¨¡å¼ï¼Œå¹¶ä¸”ä¸ä¼šè¦†ç›–å·²ä¿å­˜çš„è‡ªå®šä¹‰é¢œè‰²ã€‚" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "å…è®¸ NetLanes ä½¿ç”¨è‡ªå®šä¹‰é¢œè‰²" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<å»ºè®®å¯ç”¨>\næ”¾ç½® NetLane ç»†èŠ‚ç‰©å“æ—¶ä½¿ç”¨å·²ä¿å­˜çš„ HC é¢œè‰²/é€æ˜Žåº¦ï¼Œä¾‹å¦‚å›´æ ã€æ ‘ç¯±ã€æ ‡çº¿ä»¥åŠç±»ä¼¼çš„åŸºäºŽè½¦é“çš„å·¥å…·ã€‚\n\n- æ™®é€šé“è·¯ä»ä¼šéµå¾ªä½ åœ¨ä¸‹æ‹‰åˆ—è¡¨ä¸­é€‰æ‹©çš„æŽ¨åœŸæœº + é“è·¯è®¾ç½®ã€‚\n- å¦‚æžœå¸Œæœ›è¿™äº›å·¥å…·æ”¹ç”¨æ¸¸æˆåŽŸç‰ˆè“è‰²è½®å»“ï¼Œè¯·å…³é—­æ­¤é¡¹ã€‚\n- å¯ç”¨æ—¶ï¼Œé‡å é”™è¯¯é¢œè‰²ä»ç„¶ä¼˜å…ˆï¼ˆåŽŸç‰ˆé”™è¯¯é¢œè‰² = é²‘çº¢è‰²ï¼‰ã€‚" },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "æ›´æ·±è‰²é¢æ¿" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "å¯ç”¨æ—¶ä½¿ç”¨ <æ·±è‰²é¢æ¿>ï¼šä¸ºæ—§ç‰ˆ UI çŽ©å®¶åˆ¶ä½œï¼›å¦‚æžœä½ å–œæ¬¢æ›´æ·±çš„é¢æ¿ï¼Œä¹Ÿå¯åœ¨ Modern UI ä¸­ä½¿ç”¨ã€‚\nå…³é—­æ—¶ä½¿ç”¨ <æ ‡å‡†é¢æ¿>ï¼šHover Colors è‡ªå®šä¹‰åŠé€æ˜Žæ ·å¼ã€‚\n- æ›´æ˜Žäº®ã€æ›´çŽ°ä»£çš„å¤–è§‚ã€‚\n- é€‚åˆå¤§å¤šæ•°ä½¿ç”¨æ–°ç‰ˆ Modern æ¸¸æˆ UI çš„çŽ©å®¶ã€‚\n\nä¸¤ç§éƒ½è¯•è¯•ï¼Œé€‰æ‹©ä½ å–œæ¬¢çš„ï¼è¿™åªä¼šæ›´æ”¹æ­¤æ¨¡ç»„é¢æ¿çš„èƒŒæ™¯ï¼Œä¸ä¼šæ›´æ”¹æ¸¸æˆ UIã€‚" },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "è¾…åŠ©çº¿ä¸é€æ˜Žåº¦ï¼ˆAlphaï¼‰" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "æŽ§åˆ¶è™šçº¿å¯¹é½è¾…åŠ©çº¿çš„ä¸é€æ˜Žåº¦ï¼Œæ”¾ç½®é“è·¯ã€å›´æ ã€é“å…·ç­‰æ—¶å¾ˆæœ‰ç”¨ã€‚\n\n**100%** ä¿æŒåŽŸç‰ˆé»˜è®¤å¤–è§‚ã€‚\n**é™ä½Ž** ä¼šè®©è¾…åŠ©çº¿æ›´é€æ˜Žã€‚\n**0%** ä¼šå®Œå…¨éšè— - <ä¸æŽ¨è>ã€‚\nå»ºè®®ä¿æŒåœ¨ 15% ä»¥ä¸Šï¼Œå¦åˆ™å¾ˆéš¾çœ‹æ¸…å‘ç”Ÿäº†ä»€ä¹ˆã€‚\nåŸŽå¸‚å†…æ¨¡ç»„é¢æ¿ä¹Ÿæœ‰ç›¸åŒæ»‘å—ã€‚ä¸¤è€…ä¼šåŒæ­¥ï¼›\næ›´æ”¹è¿™ä¸ªæ»‘å—æ—¶ï¼ŒåŸŽå¸‚å†…çš„æ»‘å—ä¹Ÿä¼šéšä¹‹æ›´æ”¹ã€‚" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "æ‰“å¼€/å…³é—­ä¸»é¢æ¿" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "ç”¨äºŽæ‰“å¼€ / å…³é—­åŸŽå¸‚å†… Hover å¯¹è±¡é¢œè‰²é¢æ¿çš„å¿«æ·é”®ã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "åˆ‡æ¢ Hover Colors é¢æ¿" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "å¼€å…³ Surface å·¥å…·é¢„è§ˆ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "æ”¾ç½® Surface æ—¶ï¼Œç”¨å¿«æ·é”®éšè—æˆ–æ¢å¤å½“å‰ Surface å·¥å…·è¾¹ç•Œé¢„è§ˆçº¿ã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface å·¥å…·é¢„è§ˆå±‚ On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "åˆ‡æ¢é¢„è®¾ 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "ç”¨äºŽåœ¨é¢„è®¾æ§½ 1 å’Œæ§½ 2 ä¹‹é—´åˆ‡æ¢çš„å¿«æ·é”®ã€‚" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "åœ¨é¢„è®¾ 1 å’Œ 2 ä¹‹é—´åˆ‡æ¢" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "æ‰“å¼€ä½œè€…çš„ Paradox Mods é¡µé¢ã€‚" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "è°¨ä»¥æ­¤çºªå¿µ Mochiã€‚" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "æ­¤æ¨¡ç»„çŒ®ç»™ Mochiã€‚å¥¹æ˜¯ä¸€åªæ·±å—å–œçˆ±çš„ç‹—ç‹—ï¼Œ7 å²æ—¶è¢«é¢†å…»ï¼Œ\nå¸¦æ¥äº† 13 å¹´çš„çˆ±ä¸Žå¿«ä¹ã€‚æ²¡æœ‰ Mochiï¼Œå°±ä¸ä¼šæœ‰è¿™ä¸ªæ¨¡ç»„ã€‚" },
            };
        }

        public void Unload()
        {
        }
    }
}
