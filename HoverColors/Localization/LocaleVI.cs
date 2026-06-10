// File: Localization/LocaleVI.cs
// Purpose: Vietnamese (vi-VN) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/vi-VN.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
    public sealed class LocaleVI : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleVI(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "HÃ nh Ä‘á»™ng" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Giá»›i thiá»‡u" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "CÃ¡ch hoáº¡t Ä‘á»™ng mÃ u cÃ´ng cá»¥" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Báº£ng Ä‘iá»u khiá»ƒn" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "PhÃ­m táº¯t" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "ÄÆ°á»ng dáº«n hÆ°á»›ng" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "TÆ°á»Ÿng nhá»›" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + ÄÆ°á»ng" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Äiá»u khiá»ƒn mÃ u viá»n táº¡m thá»i khi cÃ´ng cá»¥ bulldozer hoáº·c cÃ´ng cá»¥ Ä‘Æ°á»ng Ä‘ang hoáº¡t Ä‘á»™ng.\n\n**1. KhuyÃªn dÃ¹ng** dÃ¹ng mÃ u Cáº£nh bÃ¡o cá»§a game (vÃ ng) cho phÃ¡ dá»¡ vÃ  mÃ u xanh vanilla dá»‹u hÆ¡n cho Ä‘Æ°á»ng.\n**2. MÃ u cÃ´ng cá»¥ vanilla** khÃ´i phá»¥c mÃ u xanh vanilla bÃ¬nh thÆ°á»ng cá»§a game khi dÃ¹ng bulldozer hoáº·c cÃ´ng cá»¥ Ä‘Æ°á»ng.\n**3. Giá»¯ mÃ u tÃ¹y chá»‰nh cá»§a tÃ´i** dÃ¹ng mÃ u báº¡n Ä‘Ã£ chá»n á»Ÿ má»i nÆ¡i.\n\nMá»¥c Ä‘Ã­ch: má»™t sá»‘ ngÆ°á»i dÃ¹ng/ngÆ°á»i thá»­ nghiá»‡m tháº¥y mÃ u tÃ¹y chá»‰nh khÃ³ nhÃ¬n khi phÃ¡ dá»¡.\nTÃ¹y chá»n nÃ y cung cáº¥p mÃ u dá»… nhÃ¬n hÆ¡n khi dÃ¹ng cÃ´ng cá»¥.\nKhÃ´ng ghi Ä‘Ã¨ mÃ u tÃ¹y chá»‰nh Ä‘Ã£ tá»± Ä‘á»™ng lÆ°u trong bá»™ chá»n mÃ u." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. KhuyÃªn dÃ¹ng" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. MÃ u cÃ´ng cá»¥ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Giá»¯ mÃ u tÃ¹y chá»‰nh cá»§a tÃ´i" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Báº­t viá»n cho má»¥c chá»“ng láº¥n" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<NÃªn báº­t>\nGiá»¯ viá»n Ä‘á» cÃ¡ há»“i vanilla cá»§a game khi viá»‡c Ä‘áº·t váº­t thá»ƒ hoáº·c máº¡ng bá»‹ cháº·n do cÃ¡c má»¥c chá»“ng láº¥n.\nCÃ¡c giá»›i háº¡n khu vá»±c, nhÆ° vÃ²ng bÃ¡n kÃ­nh trang tráº¡i CÃ´ng nghiá»‡p chuyÃªn biá»‡t, Ä‘Æ°á»£c giá»¯ nguyÃªn.\n\nHoáº¡t Ä‘á»™ng vá»›i má»i cháº¿ Ä‘á»™ Bulldozer + ÄÆ°á»ng vÃ  khÃ´ng ghi Ä‘Ã¨ mÃ u tÃ¹y chá»‰nh Ä‘Ã£ lÆ°u." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Cho phÃ©p mÃ u tÃ¹y chá»‰nh cho NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<NÃªn báº­t>\nDÃ¹ng mÃ u/Ä‘á»™ trong suá»‘t HC Ä‘Ã£ lÆ°u khi Ä‘áº·t cÃ¡c chi tiáº¿t NetLane nhÆ° hÃ ng rÃ o, hÃ ng cÃ¢y, váº¡ch káº» vÃ  cÃ¡c cÃ´ng cá»¥ theo lÃ n tÆ°Æ¡ng tá»±.\n\n- ÄÆ°á»ng bÃ¬nh thÆ°á»ng váº«n theo cÃ i Ä‘áº·t Bulldozer + ÄÆ°á»ng báº¡n chá»n tá»« danh sÃ¡ch tháº£ xuá»‘ng.\n- Táº¯t náº¿u báº¡n muá»‘n cÃ¡c cÃ´ng cá»¥ Ä‘Ã³ dÃ¹ng mÃ u viá»n xanh vanilla cá»§a game.\n- MÃ u lá»—i chá»“ng láº¥n váº«n Ä‘Æ°á»£c Æ°u tiÃªn khi báº­t (mÃ u lá»—i vanilla = Ä‘á» cÃ¡ há»“i)." },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Báº£ng tá»‘i hÆ¡n" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Báº­t dÃ¹ng <Báº£ng tá»‘i>: dÃ nh cho ngÆ°á»i chÆ¡i dÃ¹ng UI cÅ©; cÅ©ng cÃ³ thá»ƒ dÃ¹ng trong Modern UI náº¿u báº¡n thÃ­ch báº£ng tá»‘i hÆ¡n.\nTáº¯t dÃ¹ng <Báº£ng chuáº©n>: phong cÃ¡ch Hover Colors trong má» tÃ¹y chá»‰nh.\n- Giao diá»‡n sÃ¡ng hÆ¡n, hiá»‡n Ä‘áº¡i hÆ¡n.\n- Tá»‘t nháº¥t cho Ä‘a sá»‘ ngÆ°á»i chÆ¡i dÃ¹ng UI game Modern má»›i.\n\nHÃ£y thá»­ cáº£ hai vÃ  chá»n cÃ¡i báº¡n thÃ­ch! CÃ i Ä‘áº·t nÃ y chá»‰ Ä‘á»•i ná»n cá»§a báº£ng mod nÃ y, khÃ´ng Ä‘á»•i UI cá»§a game." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Äá»™ má» Ä‘Æ°á»ng dáº«n hÆ°á»›ng (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Äiá»u khiá»ƒn Ä‘á»™ má» cá»§a Ä‘Æ°á»ng dáº«n hÆ°á»›ng cÄƒn chá»‰nh nÃ©t Ä‘á»©t, há»¯u Ã­ch khi Ä‘áº·t Ä‘Æ°á»ng, hÃ ng rÃ o, props, v.v.\n\n**100%** giá»¯ giao diá»‡n vanilla máº·c Ä‘á»‹nh.\n**Tháº¥p hÆ¡n** lÃ m Ä‘Æ°á»ng dáº«n hÆ°á»›ng trong suá»‘t hÆ¡n.\n**0%** áº©n hoÃ n toÃ n - <KhÃ´ng khuyÃªn dÃ¹ng>.\nNÃªn Ä‘á»ƒ trÃªn 15%, náº¿u khÃ´ng sáº½ khÃ³ nhÃ¬n chuyá»‡n gÃ¬ Ä‘ang xáº£y ra.\nThanh trÆ°á»£t tÆ°Æ¡ng tá»± cÅ©ng náº±m trong báº£ng mod trong thÃ nh phá»‘. Hai thanh Ä‘Æ°á»£c Ä‘á»“ng bá»™;\nnáº¿u Ä‘á»•i thanh nÃ y, thanh trong thÃ nh phá»‘ cÅ©ng Ä‘á»•i theo." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Má»Ÿ/Ä‘Ã³ng báº£ng chÃ­nh" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "PhÃ­m táº¯t Ä‘á»ƒ má»Ÿ / Ä‘Ã³ng Báº£ng mÃ u Ä‘á»‘i tÆ°á»£ng Hover trong thÃ nh phá»‘." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Báº­t/táº¯t báº£ng Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Báº­t/táº¯t xem trÆ°á»›c cÃ´ng cá»¥ Surface" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "PhÃ­m táº¯t Ä‘á»ƒ áº©n hoáº·c khÃ´i phá»¥c cÃ¡c Ä‘Æ°á»ng biÃªn xem trÆ°á»›c Ä‘ang hoáº¡t Ä‘á»™ng cá»§a cÃ´ng cá»¥ Surface khi Ä‘áº·t bá» máº·t." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Lá»›p xem trÆ°á»›c cÃ´ng cá»¥ Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Chuyá»ƒn preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "PhÃ­m táº¯t Ä‘á»ƒ chuyá»ƒn giá»¯a Ã´ preset 1 vÃ  Ã´ 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Chuyá»ƒn giá»¯a preset 1 vÃ  2" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Má»Ÿ trang Paradox Mods cá»§a tÃ¡c giáº£." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "TÆ°á»Ÿng nhá»› Mochi báº±ng táº¥t cáº£ yÃªu thÆ°Æ¡ng." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mod nÃ y Ä‘Æ°á»£c dÃ nh táº·ng cho Mochi. CÃ´ bÃ© lÃ  má»™t chÃº chÃ³ Ä‘Æ°á»£c yÃªu thÆ°Æ¡ng, Ä‘Æ°á»£c nháº­n nuÃ´i khi 7 tuá»•i,\nvÃ  Ä‘Ã£ mang Ä‘áº¿n 13 nÄƒm tÃ¬nh yÃªu cÃ¹ng niá»m vui. Mod nÃ y sáº½ khÃ´ng thá»ƒ cÃ³ náº¿u thiáº¿u Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
