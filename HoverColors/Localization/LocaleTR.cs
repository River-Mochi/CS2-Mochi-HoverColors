// File: Localization/LocaleTR.cs
// Purpose: Turkish (tr-TR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/tr-TR.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
    public sealed class LocaleTR : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleTR(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Eylemler" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "HakkÄ±nda" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "AraÃ§ renk davranÄ±ÅŸÄ±" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "KÄ±sayol tuÅŸlarÄ±" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "KÄ±lavuzlar" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Ä°thaf" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldozer + Yollar" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldozer veya yol araÃ§larÄ± aktifken geÃ§ici dÄ±ÅŸ Ã§izgi renklerini kontrol eder.\n\n**1. Ã–nerilen** yÄ±kÄ±m iÃ§in oyunun UyarÄ± rengini (sarÄ±), yollar iÃ§in daha yumuÅŸak bir vanilla mavisini kullanÄ±r.\n**2. Vanilla araÃ§ renkleri** buldozer veya yol araÃ§larÄ± aktifken oyunun normal vanilla mavisini geri getirir.\n**3. Ã–zel rengimi koru** seÃ§tiÄŸiniz rengi her yerde kullanÄ±r.\n\nAmaÃ§: bazÄ± kullanÄ±cÄ±lar/test edenler yÄ±kÄ±m sÄ±rasÄ±nda Ã¶zel renklerini gÃ¶rmekte zorlanÄ±yor.\nBu seÃ§enekler araÃ§ kullanÄ±mÄ± sÄ±rasÄ±nda yÃ¼ksek gÃ¶rÃ¼nÃ¼rlÃ¼klÃ¼ renkler sunar.\nRenk seÃ§icide otomatik kaydedilmiÅŸ Ã¶zel renginizin Ã¼zerine yazmaz." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Ã–nerilen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla araÃ§ renkleri" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Ã–zel rengimi koru" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Ã‡akÄ±ÅŸan Ã¶ÄŸe dÄ±ÅŸ Ã§izgisini etkinleÅŸtir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Etkin olmasÄ± Ã¶nerilir>\nNesne veya aÄŸ yerleÅŸimi Ã§akÄ±ÅŸan Ã¶ÄŸeler tarafÄ±ndan engellendiÄŸinde oyunun vanilla somon kÄ±rmÄ±zÄ±sÄ± dÄ±ÅŸ Ã§izgisini gÃ¶rÃ¼nÃ¼r tutar.\nUzmanlaÅŸmÄ±ÅŸ EndÃ¼stri Ã§iftlik yarÄ±Ã§apÄ± kÄ±lavuzlarÄ± gibi alan sÄ±nÄ±rlarÄ±na dokunulmaz.\n\nTÃ¼m Buldozer + Yollar modlarÄ±yla Ã§alÄ±ÅŸÄ±r ve kaydedilmiÅŸ Ã¶zel renginizin Ã¼zerine yazmaz." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes iÃ§in Ã¶zel renklere izin ver" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Etkin olmasÄ± Ã¶nerilir>\nÃ‡itler, Ã§alÄ±lar, iÅŸaretlemeler ve benzer ÅŸerit tabanlÄ± araÃ§lar gibi NetLane detay Ã¶ÄŸelerini yerleÅŸtirirken kayÄ±tlÄ± HC renginizi/saydamlÄ±ÄŸÄ±nÄ±zÄ± kullanÄ±r.\n\n- Normal yollar hÃ¢lÃ¢ aÃ§Ä±lÄ±r listeden seÃ§tiÄŸiniz Buldozer + Yollar ayarÄ±nÄ± takip eder.\n- Bu araÃ§larÄ±n oyunun vanilla mavi dÄ±ÅŸ Ã§izgisini kullanmasÄ±nÄ± istiyorsanÄ±z devre dÄ±ÅŸÄ± bÄ±rakÄ±n.\n- Etkinken Ã§akÄ±ÅŸma hata rengi hÃ¢lÃ¢ Ã¶nceliklidir (vanilla hata rengi = somon kÄ±rmÄ±zÄ±sÄ±)." },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Daha koyu panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Etkinse <Koyu panel> kullanÄ±r: eski UI oyuncularÄ± iÃ§in yapÄ±lmÄ±ÅŸtÄ±r; daha koyu panel seviyorsanÄ±z Modern UIâ€™da da kullanÄ±labilir.\nDevre dÄ±ÅŸÄ±ysa <Standart panel> kullanÄ±r: Ã¶zel yarÄ± saydam Hover Colors stili.\n- Daha aÃ§Ä±k, daha modern gÃ¶rÃ¼nÃ¼m.\n- Yeni Modern oyun UIâ€™sini kullanan Ã§oÄŸu oyuncu iÃ§in en iyisi.\n\nÄ°kisini de deneyin ve hangisini sevdiÄŸinizi gÃ¶rÃ¼n! Bu yalnÄ±zca bu mod panelinin arka planÄ±nÄ± deÄŸiÅŸtirir, oyunun UIâ€™sini deÄŸil." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "KÄ±lavuz opaklÄ±ÄŸÄ± (alfa)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Yol, Ã§it, prop vb. yerleÅŸtirirken yararlÄ± olan kesikli hizalama kÄ±lavuzlarÄ±nÄ±n opaklÄ±ÄŸÄ±nÄ± kontrol eder.\n\n**100%** vanilla varsayÄ±lan gÃ¶rÃ¼nÃ¼mÃ¼ korur.\n**Daha dÃ¼ÅŸÃ¼k** kÄ±lavuzlarÄ± daha saydam yapar.\n**0%** tamamen gizler - <Ã–nerilmez>.\nNe olduÄŸunu gÃ¶rmek zorlaÅŸacaÄŸÄ± iÃ§in 15% Ã¼zerinde kalmanÄ±z Ã¶nerilir.\nAynÄ± kaydÄ±rÄ±cÄ± ÅŸehir mod panelinde de bulunur. Ä°kisi eÅŸitlenir;\nbunu deÄŸiÅŸtirirseniz ÅŸehirdeki de deÄŸiÅŸir." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ana paneli aÃ§/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Åžehir iÃ§i Hover nesneleri Renk Panelini aÃ§mak / kapatmak iÃ§in kÄ±sayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors panelini aÃ§/kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface aracÄ± Ã¶nizlemelerini aÃ§/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "YÃ¼zey yerleÅŸtirirken aktif Surface aracÄ± sÄ±nÄ±r Ã¶nizleme Ã§izgilerini gizlemek veya geri getirmek iÃ§in kÄ±sayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface aracÄ± Ã¶nizleme katmanÄ± On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Preset 1+2 deÄŸiÅŸtir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Preset yuvasÄ± 1 ile yuva 2 arasÄ±nda geÃ§iÅŸ yapmak iÃ§in kÄ±sayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Preset 1 ve 2 arasÄ±nda geÃ§iÅŸ yap" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "YazarÄ±n Paradox Mods sayfasÄ±nÄ± aÃ§." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mochiâ€™nin sevgi dolu anÄ±sÄ±na." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Bu mod Mochiâ€™ye adanmÄ±ÅŸtÄ±r. O, 7 yaÅŸÄ±nda sahiplenilen Ã§ok sevilen bir kÃ¶pekti\nve 13 yÄ±l boyunca sevgi ve neÅŸe verdi. Bu mod Mochi olmadan mÃ¼mkÃ¼n olmazdÄ±." },
            };
        }

        public void Unload()
        {
        }
    }
}
