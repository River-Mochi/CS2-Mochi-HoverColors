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

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldożer + drogi" },
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

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Włącz obrys nakładających się elementów" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Włączenie zalecane>\n" +
                    "Zostawia widoczny łososiowy obrys vanilla, gdy obiekt lub sieć jest blokowana przez kolizję.\n" +
                    "Limity obszaru, np. promienie farm przemysłu specjalnego, są zostawione bez zmian.\n" +
                    "\n" +
                    "Działa ze wszystkimi trybami Buldożer + drogi i nie nadpisuje zapisanego koloru."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Pozwól na własne kolory dla NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Włączenie zalecane>\n" +
                    "Używa zapisanego koloru/przezroczystości HC przy stawianiu detali NetLane, jak płoty, żywopłoty, oznaczenia itp.\n" +
                    "\n" +
                    "- Zwykłe drogi nadal używają ustawienia Buldożer + drogi z listy.\n" +
                    "- Wyłącz, jeśli te narzędzia mają używać błękitu vanilla z gry.\n" +
                    "- Kolor błędu nakładania nadal ma pierwszeństwo, gdy jest włączony (vanilla = łososiowy)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Dymki kolorów po najechaniu" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Włączone> = pokaż dymki pomocy kolorów po najechaniu (zalecane [x]).\n" +
                    "<Wyłączone> = ukryj dymki tego moda.\n" +
                    "Dymki można wyłączyć tylko w tym menu Opcje.\n" +
                    "W mieście możesz je włączyć z powrotem: kliknij Info (i) na pasku tytułu."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ciemniejszy panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Włączone = <Ciemny panel>: dla graczy LegacyUI; działa też w Modern UI, jeśli wolisz mocniejszy kontrast.\n" +
                    "Wyłączone = <Panel standardowy>: własny półprzezroczysty styl kolorów po najechaniu.\n" +
                    "- Jaśniejszy, nowocześniejszy wygląd.\n" +
                    "- Najlepsze dla większości graczy z nowym Modern UI.\n" +
                    "\n" +
                    "Wypróbuj oba. Zmienia tylko tło panelu moda, nie interfejs gry."
                },

                // Reset button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)), "Przywróć domyślną oprawę gry" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)),
                    "Przywraca domyślny wygląd wszystkiego, czym steruje ten mod: obrys, podświetlenie właściciela, wypełnienie, grubość obrysu, prowadnice, dzielnice i podglądy obszarów narzędzi.\n" +
                    "\n" +
                    "Zapisane zestawy, skróty klawiszowe i ustawienia panelu pozostają bez zmian.\n" +
                    "\n" +
                    "Nie jest potrzebne do odinstalowania moda. Służy do zaczęcia od nowa po eksperymentach."
                },

                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)),
                    "Przywrócić domyślny wygląd wszystkiego, czym steruje ten mod?\n\nZestawy, skróty klawiszowe i ustawienia panelu zostaną zachowane." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Otwórz/zamknij panel główny" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Atalho para <abrir / fechar> o painel de cores na cidade." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Przełącz panel kolorów po najechaniu" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Podglądy narzędzia Surface wł./wył." },
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
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
