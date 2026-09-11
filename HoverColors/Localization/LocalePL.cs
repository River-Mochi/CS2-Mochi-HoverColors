// <copyright file="LocalePL.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
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
                    "Steruje tymczasowymi kolorami obrysu, gdy aktywny jest buldożer lub narzędzia dróg.\n" +
                    "\n" +
                    "**1. Zalecane** używa koloru ostrzeżenia gry (żółty) do rozbiórki i łagodniejszego błękitu vanilla dla dróg.\n" +
                    "**2. Kolory vanilla** przywraca normalny błękit gry przy buldożerze lub drogach.\n" +
                    "**3. Zachowaj mój kolor** używa wybranego koloru wszędzie.\n" +
                    "\n" +
                    "Cel: niektórzy użytkownicy/testerzy słabo widzą własny kolor przy burzeniu.\n" +
                    "Daje dobrze widoczne kolory podczas używania narzędzi.\n" +
                    "Nie nadpisuje koloru zapisanego automatycznie w próbniku."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Zalecane" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Kolory vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Zachowaj mój kolor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Włącz obrys nakładających się elementów" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Włączenie zalecane>\n" +
                    "Zostawia widoczny łososiowy obrys vanilla, gdy obiekt lub sieć jest blokowana przez kolizję.\n" +
                    "Limity obszaru, np. promienie farm przemysłu specjalnego, są zostawione bez zmian.\n" +
                    "\n" +
                    "Działa ze wszystkimi trybami Buldożer + drogi i nie nadpisuje zapisanego koloru."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Pozwól na własne kolory dla NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Włączenie zalecane>\n" +
                    "Używa zapisanego koloru/przezroczystości HC przy stawianiu detali NetLane, jak płoty, żywopłoty i oznaczenia.\n" +
                    "\n" +
                    "- Zwykłe drogi nadal używają ustawienia Buldożer + drogi z listy.\n" +
                    "- Wyłącz, jeśli te narzędzia mają używać błękitu vanilla z gry.\n" +
                    "- Kolor błędu nakładania nadal ma pierwszeństwo, gdy jest włączony (vanilla = łososiowy)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Ciemniejszy panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Włączone = <Ciemny panel>: używa kolorów panelu gry, dzięki czemu pasuje do paneli vanilla.\n" +
                    "- Przy 100% jest całkowicie nieprzezroczysty.\n" +
                    "Wyłączone = <Standardowy panel>: jaśniejszy, półprzezroczysty styl Hover Colors.\n" +
                    "- Efekt szkła; nawet przy 100% trochę miasta pozostaje widoczne.\n" +
                    "\n" +
                    "Oba style używają suwaka krycia poniżej i wyglądają tak samo w Modern UI i Legacy UI.\n" +
                    "\n" +
                    "Wypróbuj oba! Zmienia się tylko tło tego panelu moda, a nie interfejs gry."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Krycie panelu" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Określa krycie tła panelu w mieście.\n" +
                    "\n" +
                    "**30% Szkło** daje najbardziej przezroczysty i czysty wygląd.\n" +
                    "**100%** oznacza pełne krycie ciemnego panelu i prawie pełne krycie standardowego.\n" +
                    "\n" +
                    "Zmienia się tylko tło. Tekst, ikony i próbki kolorów pozostają zawsze czytelne.\n" +
                    "\n" +
                    "**Działa z oboma stylami panelu.** Ustawienie krycia interfejsu gry nie wpływa już na ten panel."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Pokaż dymki (zalecane)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "Dla większości graczy zalecamy <zostawić włączone>.\n" +
                    "Pokazuje krótką pomoc po najechaniu na przyciski Hover Colors.\n" +
                    "Po wyłączeniu kliknij Info (i) na pasku tytułu albo ponownie zaznacz tę opcję.\n" +
                    "Aby uniknąć przypadków, dymki można wyłączyć tylko w tym menu Opcje."
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
                    "Przywrócić wszystkie ustawienia Hover Colors jak po nowej instalacji?\n\nZapisane presety (Set A i Set B) zostaną usunięte." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Przywróć kolory vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Przywraca wygląd gry: obrys, podświetlenie właściciela, wypełnienie, grubość, prowadnice i dzielnice.\n" +
                    "\n" +
                    "Wszystko inne zostaje bez zmian: Buldożer/drogi, podglądy narzędzi, presety, panel i skróty.\n" +
                    "\n" +
                    "Uwaga: mod można usunąć bez resetu. Podświetlenia same wrócą do ustawień gry.\n"+
                    "To tylko szybki reset kolorów do ustawień gry."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Przywrócić kolory sterowane przez mod do wyglądu gry?\n\nPresety, zachowanie narzędzi i opcje panelu zostają." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Otwórz/zamknij panel główny" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Skrót do <otwarcia / zamknięcia> panelu kolorów w mieście." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Przełącz panel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Szybkie oko wł./wył." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Opcjonalny skrót dla przycisku oka: natychmiast włącza/wyłącza podświetlenie + wypełnienie.\n" +
                    "Domyślnie bez klawisza, aby uniknąć konfliktów." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Szybkie oko wł./wył." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Podglądy Surface wł./wył." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Skrót do <ukrycia lub pokazania> aktywnych linii granicy Surface podczas stawiania powierzchni." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Warstwa podglądu Surface wł./wył." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Przełącz presety 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Skrót do przełączania między\n" +
                    "<slotem presetu 1 i slotem 2>." },
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
                    "Ku pamięci Mochi."
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
