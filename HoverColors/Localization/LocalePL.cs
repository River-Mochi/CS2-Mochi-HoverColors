// File: Localization/LocalePL.cs
// Purpose: Polish (pl-PL) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/pl-PL.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
    public sealed class LocalePL : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocalePL(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Akcje" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "O modzie" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Zachowanie kolorÃ³w narzÄ™dzi" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "SkrÃ³ty klawiszowe" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Linie pomocnicze" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedykacja" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "BuldoÅ¼er + drogi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Steruje tymczasowymi kolorami obrysu, gdy aktywny jest buldoÅ¼er lub narzÄ™dzia drogowe.\n\n**1. Zalecane** uÅ¼ywa koloru ostrzeÅ¼enia gry (Å¼Ã³Å‚tego) dla wyburzania oraz Å‚agodniejszego vanilla niebieskiego dla drÃ³g.\n**2. Vanilla kolory narzÄ™dzi** przywraca normalny vanilla niebieski gry, gdy aktywny jest buldoÅ¼er lub narzÄ™dzia drogowe.\n**3. Zachowaj mÃ³j kolor** uÅ¼ywa wszÄ™dzie wybranego koloru.\n\nCel: niektÃ³rzy uÅ¼ytkownicy/testerzy uznali, Å¼e ich wÅ‚asny kolor jest sÅ‚abo widoczny podczas wyburzania.\nTe opcje dajÄ… kolory o wysokiej widocznoÅ›ci podczas uÅ¼ywania narzÄ™dzi.\nNie nadpisuje to automatycznie zapisanego koloru w prÃ³bniku kolorÃ³w." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Zalecane" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla kolory narzÄ™dzi" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Zachowaj mÃ³j kolor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "WÅ‚Ä…cz obrys nakÅ‚adajÄ…cych siÄ™ elementÃ³w" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Zalecane wÅ‚Ä…czenie>\nUtrzymuje widoczny vanilla Å‚ososiowoczerwony obrys gry, gdy umieszczanie obiektu lub sieci jest blokowane przez nakÅ‚adajÄ…ce siÄ™ elementy.\nLimity obszarÃ³w, takie jak przewodniki promienia farm wyspecjalizowanego przemysÅ‚u, pozostajÄ… bez zmian.\n\nDziaÅ‚a ze wszystkimi trybami BuldoÅ¼er + drogi i nie nadpisuje zapisanego koloru niestandardowego." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Zezwalaj na wÅ‚asne kolory dla NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Zalecane wÅ‚Ä…czenie>\nUÅ¼ywa zapisanego koloru/przezroczystoÅ›ci HC podczas umieszczania elementÃ³w szczegÃ³Å‚owych NetLane, takich jak pÅ‚oty, Å¼ywopÅ‚oty, oznaczenia i podobne narzÄ™dzia oparte na pasach.\n\n- ZwykÅ‚e drogi nadal uÅ¼ywajÄ… ustawienia BuldoÅ¼er + drogi wybranego z listy.\n- WyÅ‚Ä…cz, jeÅ›li te narzÄ™dzia majÄ… uÅ¼ywaÄ‡ vanilla niebieskiego obrysu gry.\n- Kolor bÅ‚Ä™du nakÅ‚adania nadal ma pierwszeÅ„stwo, gdy jest wÅ‚Ä…czony (vanilla kolor bÅ‚Ä™du = Å‚ososiowoczerwony)." },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ciemniejszy panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "WÅ‚Ä…czone uÅ¼ywa <Ciemny panel>: stworzone dla graczy ze starszÄ… UI; moÅ¼na teÅ¼ uÅ¼ywaÄ‡ w Modern UI, jeÅ›li wolisz ciemniejszy panel.\nWyÅ‚Ä…czone uÅ¼ywa <Panel standardowy>: wÅ‚asny pÃ³Å‚przezroczysty styl Hover Colors.\n- JaÅ›niejszy, bardziej nowoczesny wyglÄ…d.\n- Najlepszy dla wiÄ™kszoÅ›ci graczy uÅ¼ywajÄ…cych nowej Modern UI gry.\n\nSprawdÅº oba i wybierz, ktÃ³ry wolisz! Zmienia to tylko tÅ‚o panelu tego moda, a nie UI gry." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Krycie linii pomocniczych (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Steruje kryciem przerywanych linii wyrÃ³wnania, przydatne przy stawianiu drÃ³g, pÅ‚otÃ³w, propÃ³w itd.\n\n**100%** zachowuje domyÅ›lny vanilla wyglÄ…d.\n**NiÅ¼ej** sprawia, Å¼e linie sÄ… bardziej przezroczyste.\n**0%** ukrywa je caÅ‚kowicie - <Niezalecane>.\nZalecane jest pozostanie powyÅ¼ej 15%, inaczej trudno zobaczyÄ‡, co siÄ™ dzieje.\nTen sam suwak jest w panelu moda w mieÅ›cie. Oba sÄ… zsynchronizowane;\njeÅ›li zmienisz ten, miejski teÅ¼ siÄ™ zmieni." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "OtwÃ³rz/zamknij panel gÅ‚Ã³wny" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "SkrÃ³t klawiszowy do otwierania / zamykania miejskiego Panelu kolorÃ³w obiektÃ³w Hover." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "PrzeÅ‚Ä…cz panel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "PrzeÅ‚Ä…cz podglÄ…dy narzÄ™dzia Powierzchnia" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "SkrÃ³t klawiszowy do ukrycia lub przywrÃ³cenia aktywnych linii granic narzÄ™dzia Powierzchnia podczas umieszczania powierzchni." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Warstwa podglÄ…du Powierzchni On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "PrzeÅ‚Ä…cz presety 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "SkrÃ³t klawiszowy do przeÅ‚Ä…czania miÄ™dzy slotem presetu 1 i 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "PrzeÅ‚Ä…cz miÄ™dzy presetami 1 i 2" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "OtwÃ³rz stronÄ™ autora w Paradox Mods." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "PamiÄ™ci kochanej Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Ten mod jest dedykowany Mochi. ByÅ‚a ukochanÄ… suczkÄ…, adoptowanÄ… w wieku 7 lat,\ni daÅ‚a 13 lat miÅ‚oÅ›ci oraz radoÅ›ci. Ten mod nie byÅ‚by moÅ¼liwy bez Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
