// <copyright file="LocalePL.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Localization/LocalePL.cs
// Purpose: Polish (pl-PL) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/pl-PL.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocalePL : IDictionarySource
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

                // Tabs
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Akcje" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Skróty" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "O modzie" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Kolory narzędzi" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Resetowanie" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Skróty" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedykacja" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Buldożer + drogi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Steruje tymczasowymi kolorami obrysu przy buldożerze i narzędziach dróg.\n" +
                    "\n" +
                    "**1. Zalecane** używa żółtego koloru ostrzeżenia przy burzeniu. Przy narzędziu drogi/ścieżki łagodzi błękit vanilla na nowych podglądach i segmentach pod narzędziem. Zwykłe wskazanie istniejącej drogi nadal używa twojego koloru obrysu. Duże wypełnienie nowej drogi korzysta z koloru podglądu Prowadnic.\n" +
                    "**2. Kolory vanilla** przywraca normalny błękit vanilla przy buldożerze i drogach.\n" +
                    "**3. Zachowaj mój kolor** używa wybranego koloru wszędzie.\n" +
                    "\n" +
                    "Przydatne, gdy twój kolor jest słabo widoczny podczas burzenia.\n" +
                    "Osobna prowadnica obrysu drogi też korzysta z koloru podglądu Prowadnic.\n" +
                    "Nie nadpisuje koloru zapisanego w próbniku."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Zalecane" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Kolory vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Zachowaj mój kolor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Włącz obrys nakładających się elementów" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Włączenie zalecane>\n" +
                    "Pokazuje łososiowy obrys vanilla, gdy obiektu lub sieci nie można postawić przez nakładanie.\n" +
                    "Limity obszaru, np. promienie farm przemysłu specjalnego, pozostają bez zmian.\n" +
                    "\n" +
                    "Działa ze wszystkimi trybami Buldożer + drogi i nie nadpisuje zapisanego koloru."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Pozwól na własne kolory dla NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Włączenie zalecane>\n" +
                    "Używa zapisanego koloru/przezroczystości HC dla detali NetLane, np. płotów, żywopłotów i oznaczeń.\n" +
                    "\n" +
                    "- Zwykłe drogi nadal używają ustawienia Buldożer + drogi.\n" +
                    "- Wyłącz, aby te narzędzia używały błękitu vanilla.\n" +
                    "- Kolor błędu nakładania nadal ma pierwszeństwo (vanilla = łososiowy)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Wybierz ciemny lub szklany panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Ciemny (Vanilla)** używa panelu z gry.\n" +
                    "- Automatycznie pasuje do Legacy UI lub Modern UI.\n" +
                    "- Korzysta z ustawienia krycia interfejsu gry.\n" +
                    "\n" +
                    "**Szkło (Własny)** używa jaśniejszego szklanego panelu Hover Colors.\n" +
                    "- Nawet przy 100% trochę miasta prześwituje.\n" +
                    "- Dodaje suwak krycia panelu.\n" +
                    "\n" +
                    "Wypróbuj oba! Zmienia się tylko tło tego panelu moda, nie interfejs gry.\n" +
                    "\n" +
                    "Wskazówka: gra rozmywa obraz za panelami. Przezroczystość interfejsu 0% wyłącza rozmycie; 1% lub więcej je zachowuje."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Ciemny (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Szkło (Własny)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Położenie przycisku panelu" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Wybierz lewy górny róg, prawy górny róg lub Universal Mod Menu. Zmiana tego ustawienia natychmiast przenosi przycisk."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "Lewy górny róg" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "Prawy górny róg" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Krycie panelu" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Krycie tła panelu **Szkło (Własny)**.\n" +
                    "\n" +
                    "**30%** = najbardziej przezroczyste.\n" +
                    "**100%** = prawie pełne, ale trochę miasta nadal widać.\n" +
                    "\n" +
                    "Zmienia się tylko tło; tekst, ikony i próbki kolorów pozostają czytelne.\n" +
                    "\n" +
                    "**Ciemny (Vanilla)** korzysta z krycia interfejsu gry, więc ten suwak jest ukryty."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Pokaż dymki (zalecane)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "Dla większości graczy zalecamy <zostawić włączone>.\n" +
                    "Pokazuje krótką pomoc po najechaniu na przyciski Hover Colors.\n" +
                    "Po wyłączeniu kliknij Info (i) na pasku tytułu albo włącz tę opcję ponownie.\n" +
                    "Dymki można wyłączyć tylko w tym menu Opcje."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Przywróć domyślne moda" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Przywraca wszystkie ustawienia Hover Colors jak po nowej instalacji: kolory, grubość obrysu, narzędzia, prowadnice, panel i dymki.\n" +
                    "\n" +
                    "**Usuwa też zapisane presety (Set A i Set B).**\n" +
                    "\n" +
                    "Skróty klawiszowe pozostają bez zmian.\n" +
                    "To jak pierwsza instalacja Hover Colors."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Przywrócić wszystkie ustawienia Hover Colors?\n" +
                    "\n" +
                    "Zapisane presety (Set A i Set B) zostaną usunięte."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Przywróć kolory vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Przywraca wygląd gry: obrys, podświetlenie właściciela, wypełnienie, grubość, prowadnice i dzielnice.\n" +
                    "\n" +
                    "Wszystko inne zostaje: Buldożer/drogi, podglądy, presety, panel i skróty.\n" +
                    "\n" +
                    "Mod można usunąć bez resetu; podświetlenia same wrócą do ustawień gry.\n" +
                    "To tylko szybki reset kolorów."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Przywrócić kolory sterowane przez mod do wyglądu gry?\n" +
                    "\n" +
                    "Presety, narzędzia i opcje panelu zostają."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Otwórz/zamknij panel główny" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Skrót do <otwarcia / zamknięcia> panelu kolorów w mieście."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Przełącz panel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Szybkie oko wł./wył." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Opcjonalny skrót dla przycisku oka: natychmiast włącza/wyłącza podświetlenie + wypełnienie.\n" +
                    "Domyślnie bez klawisza, aby uniknąć konfliktów."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Szybkie oko wł./wył." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Dymki kątów wł./wył." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Skrót dla dymków kątów oraz pól Umieść/Cofnij.\n" +
                    "Działa podczas rysowania nowych dróg lub ścieżek.\n" +
                    "Koszt, długość i nachylenie pozostają widoczne."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Kąty i wskazówki myszy wł./wył." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Przełącz presety 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Skrót do przełączania między\n" +
                    "<presetem 1 i presetem 2>."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Przełącz między presetami 1 i 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Wersja" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Otwórz stronę autora w Paradox Mods.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Pamięci ukochanej Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Ten mod jest dedykowany Mochi.\n" +
                    "Była ukochaną suczką, adoptowaną w wieku 7 lat,\n" +
                    "i dała 13 lat miłości oraz radości.\n" +
                    "Ten mod nie powstałby bez Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
