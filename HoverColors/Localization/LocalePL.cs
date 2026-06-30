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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Zachowanie kolorów narzędzi" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Skróty klawiszowe" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Linie pomocnicze" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedykacja" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldożer + Drogi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Kontroluje tymczasowe kolory obrysu, gdy aktywne są narzędzia buldożera lub dróg.\n\n**1. Zalecane** używa koloru ostrzeżenia gry (żółty) do wyburzania i łagodniejszego vanilla niebieskiego dla dróg.\n**2. Kolory narzędzi vanilla** przywraca normalny vanilla niebieski gry podczas używania buldożera lub dróg.\n**3. Zachowaj mój kolor** używa wszędzie wybranego koloru.\n\nCel: niektórzy gracze/testerzy słabo widzą własny kolor podczas wyburzania.\nTe opcje dają bardziej widoczne kolory podczas używania narzędzi.\nNie nadpisuje to automatycznie zapisanego koloru w selektorze." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Zalecane" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Kolory narzędzi vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Zachowaj mój kolor" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Włącz obrys nakładających się elementów" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Zalecane włączone>\nPozostawia widoczny vanilla łososiowoczerwony obrys gry, gdy umieszczenie obiektu lub sieci jest blokowane przez nakładające się elementy.\nLimity obszarów, np. promienie farm przemysłu specjalistycznego, pozostają bez zmian.\n\nDziała ze wszystkimi trybami Buldożer + Drogi i nie nadpisuje zapisanego koloru." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Zezwól na własne kolory dla NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Zalecane włączone>\nUżywa zapisanego koloru/przezroczystości Kolorów pod kursorem podczas umieszczania detali NetLane, takich jak płoty, żywopłoty, oznaczenia i podobne narzędzia pasów.\n\n- Normalne drogi nadal używają ustawienia Buldożer + Drogi wybranego z listy.\n- Wyłącz to, jeśli te narzędzia mają używać vanilla niebieskiego obrysu gry.\n- Kolor błędu nakładania nadal ma pierwszeństwo, gdy jest włączony (vanilla = łososiowy czerwony)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Podpowiedzi Kolorów pod kursorem" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Włączone> = pokazuje podpowiedzi pomocy Kolorów pod kursorem (zalecane [x]).\n<Wyłączone> = ukrywa podpowiedzi tego moda.\nPodpowiedzi można wyłączyć tylko w tym menu Opcje.\nMożesz je jednak ponownie włączyć w mieście: kliknij przycisk Info (i) na pasku tytułu." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ciemniejszy panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Włączone = <Ciemny panel>: dla graczy używających starszego UI; można też używać w Modern UI, jeśli wolisz ciemniejszy panel.\nWyłączone = <Panel standardowy>: własny półprzezroczysty styl Kolorów pod kursorem.\n- Lżejszy, nowocześniejszy wygląd.\n- Najlepszy dla większości graczy używających nowego Modern UI.\n\nWypróbuj oba i wybierz, co wolisz. Zmienia tylko tło panelu tego moda, nie UI gry." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Przezroczystość linii pomocniczych (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Kontroluje przezroczystość przerywanych linii wyrównania, przydatne przy stawianiu dróg, płotów, rekwizytów itd.\n\n**100%** zachowuje wygląd vanilla.\n**Niżej** zwiększa przezroczystość linii.\n**0%** całkowicie je ukrywa - <Niezalecane>.\nZaleca się pozostać powyżej 15%, bo inaczej trudno zobaczyć, co się dzieje.\nTen sam suwak jest w panelu miasta. Oba są zsynchronizowane;\njeśli zmienisz ten, miejski też się zmieni." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Otwórz/zamknij panel główny" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Skrót do otwierania/zamykania miejskiego panelu kolorów obiektów pod kursorem." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Przełącz panel Kolorów pod kursorem" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Włącz/wyłącz podglądy narzędzia Powierzchnia" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Skrót do ukrywania lub przywracania aktywnych linii granic narzędzia Powierzchnia podczas umieszczania powierzchni." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Warstwa podglądu narzędzia Powierzchnia On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Przełącz presety 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Skrót do przełączania między slotem 1 i 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Przełącz między presetami 1 i 2" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Wersja" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Otwórz stronę autora w Paradox Mods." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Pamięci Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Ten mod jest dedykowany Mochi. Była ukochaną psinką, adoptowaną w wieku 7 lat,\ni dała 13 lat miłości oraz radości. Ten mod nie byłby możliwy bez Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
