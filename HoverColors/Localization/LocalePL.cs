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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "O modzie" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Kolory narzędzi" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Skróty klawiszowe" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Linie pomocnicze" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedykacja" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldożer + drogi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Ustawia tymczasowe kolory obrysu, gdy aktywny jest buldożer lub narzędzia dróg.\n\n**1. Zalecane**: żółty kolor ostrzegawczy do wyburzania i łagodniejszy vanilla niebieski dla dróg.\n**2. Kolory vanilla**: zwykły niebieski z gry dla buldożera/dróg.\n**3. Zostaw mój kolor**: używa twojego koloru wszędzie.\n\nPrzydatne, gdy własny kolor słabo widać przy wyburzaniu. Zapisany kolor nie zostaje nadpisany." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Zalecane" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Kolory vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Zostaw mój kolor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Obrys przy nakładaniu" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Zalecane: włączone>\nZostawia widoczny łososiowoczerwony obrys z gry, gdy obiekt lub sieć nie może zostać postawiona przez nakładanie.\nLimity obszaru, np. promienie farm przemysłu specjalistycznego, pozostają bez zmian.\n\nDziała ze wszystkimi trybami Buldożer + drogi i nie nadpisuje twojego koloru." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Własne kolory dla NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Zalecane: włączone>\nUżywa zapisanego koloru/przezroczystości HC dla detali NetLane, np. płotów, żywopłotów, oznaczeń i podobnych narzędzi.\n\n- Zwykłe drogi nadal używają ustawienia Buldożer + drogi.\n- Wyłącz, jeśli te narzędzia mają używać vanilla niebieskiego.\n- Kolor błędu nakładania nadal ma pierwszeństwo, jeśli jest włączony (vanilla łososiowa czerwień)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Podpowiedzi moda" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Włączone> = pokazuje podpowiedzi moda (zalecane [x]).\n<Wyłączone> = ukrywa podpowiedzi tego moda.\nWyłączyć je można tylko w tym menu Opcji.\nW mieście możesz włączyć je ponownie przyciskiem Info (i) na pasku tytułu." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ciemniejszy panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Włączone = <Ciemny panel>: dla starego UI albo gdy wolisz ciemniej.\nWyłączone = <Standardowy panel>: półprzezroczysty styl moda.\n- Lżejszy i nowocześniejszy wygląd.\n- Najlepszy dla nowego Modern UI.\n\nSprawdź oba. Zmienia tylko tło panelu moda, nie UI gry." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Przezroczystość linii (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Reguluje widoczność przerywanych linii wyrównania przy stawianiu dróg, płotów, propsów itd.\n\n**100%** domyślnie jak w grze.\n**Niżej** bardziej przezroczyste.\n**0%** ukrywa wszystko.\nZostań powyżej 15%, bo linia robi się trudna do zobaczenia.\nTen sam suwak jest w panelu miasta; oba są zsynchronizowane." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Otwórz/zamknij panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Skrót do <otwarcia / zamknięcia> panelu kolorów w mieście." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Przełącz panel" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Podgląd Surface wł./wył." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Skrót do <ukrycia lub pokazania> linii granic narzędzia Surface podczas stawiania." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Warstwa podglądu Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Przełącz presety 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Skrót do przełączania między\n<slotem presetu 1 i 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Przełącz między presetami 1 i 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Wersja" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Otwiera stronę autora w Paradox Mods.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Ku pamięci Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Ten mod jest dedykowany Mochi.\nByła ukochanym pieskiem, adoptowanym w wieku 7 lat,\ni dała 13 lat miłości oraz radości.\nBez Mochi ten mod by nie powstał." },
            };
        }

        public void Unload()
        {
        }
    }
}
