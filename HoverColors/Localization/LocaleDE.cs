// <copyright file="LocaleDE.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleDE.cs
// Purpose: German (de-DE) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/de-DE.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleDE : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleDE(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Aktionen" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Info" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Werkzeug-Farbverhalten" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Tastenkürzel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Hilfslinien" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Widmung" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + Straßen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Steuert temporäre Konturfarben, wenn Bulldozer- oder Straßenwerkzeuge aktiv sind.\n\n**1. Empfohlen** nutzt die Warnfarbe des Spiels (Gelb) für Abriss und ein weicheres Vanilla-Blau für Straßen.\n**2. Vanilla-Werkzeugfarben** stellt das normale Vanilla-Blau des Spiels wieder her, solange Bulldozer- oder Straßenwerkzeuge aktiv sind.\n**3. Eigene Farbe behalten** nutzt überall deine gewählte Farbe.\n\nZweck: Einige Nutzer/Tester finden ihre eigene Farbe beim Bulldozern schwer zu sehen.\nDiese Optionen bieten gut sichtbare Farben während der Werkzeugnutzung.\nDeine automatisch gespeicherte Farbe im Farbwähler wird nicht überschrieben." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Empfohlen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla-Werkzeugfarben" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Eigene Farbe behalten" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Kontur für überlappende Elemente aktivieren" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Aktiviert empfohlen>\nLässt die Vanilla-lachsrote Kontur des Spiels sichtbar, wenn Objekt- oder Netzwerkplatzierung durch überlappende Elemente blockiert ist.\nBereichsgrenzen, etwa Farm-Radius-Hilfen der Spezialindustrie, bleiben unverändert.\n\nFunktioniert mit allen Bulldozer + Straßen-Modi und überschreibt deine gespeicherte Farbe nicht." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Eigene Farben für NetLanes erlauben" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Aktiviert empfohlen>\nNutzt deine gespeicherte Hover-Farben-Farbe/Transparenz beim Platzieren von NetLane-Details wie Zäunen, Hecken, Markierungen und ähnlichen spurbezogenen Werkzeugen.\n\n- Normale Straßen folgen weiterhin der Einstellung Bulldozer + Straßen aus der Dropdown-Liste.\n- Deaktiviere dies, wenn diese Werkzeuge stattdessen die Vanilla-blaue Kontur des Spiels nutzen sollen.\n- Die Überlappungs-Fehlerfarbe hat weiterhin Vorrang, wenn aktiviert (Vanilla-Fehlerfarbe = Lachsrot)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Tooltips für Hover-Farben" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Aktiviert> = zeigt Hilfe-Tooltips für Hover-Farben (empfohlen [x]).\n<Deaktiviert> = blendet Tooltips für diesen Mod aus.\nTooltips können nur in diesem Optionsmenü deaktiviert werden.\nDu kannst sie in der Stadt wieder einschalten: Klicke auf die Info-Schaltfläche (i) in der Titelleiste." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Dunkleres Panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Aktiviert = <Dunkles Panel>: für Spieler mit Legacy-UI; auch in Modern UI nutzbar, wenn du ein dunkleres Panel magst.\nDeaktiviert = <Standardpanel>: eigener transparenter Hover-Farben-Stil.\n- Hellerer, modernerer Look.\n- Am besten für die meisten Spieler mit der neuen Modern-UI.\n\nProbiere beides aus und nimm, was dir besser gefällt! Dies ändert nur den Hintergrund dieses Mod-Panels, nicht die Spiel-UI." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Hilfslinien-Deckkraft (Alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Steuert die Deckkraft der gestrichelten Ausrichtungshilfen, nützlich beim Platzieren von Straßen, Zäunen, Props usw.\n\n**100%** behält den Vanilla-Standardlook.\n**Niedriger** macht Hilfslinien transparenter.\n**0%** blendet sie komplett aus - <Nicht empfohlen>.\nBleibe am besten über 15%, sonst ist schwer zu erkennen, was passiert.\nDerselbe Schieberegler ist im Stadt-Panel. Beide sind synchronisiert;\nwenn du diesen änderst, ändert sich der in der Stadt ebenfalls." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Hauptpanel öffnen/schließen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Tastenkürzel zum Öffnen/Schließen des Hover-Objekt-Farbpanels in der Stadt." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover-Farben-Panel umschalten" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Vorschau des Oberflächenwerkzeugs ein/aus" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Tastenkürzel zum Ausblenden oder Wiederherstellen aktiver Grenzlinien des Oberflächenwerkzeugs beim Platzieren von Oberflächen." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Oberflächenwerkzeug-Vorschau Ebene Ein/Aus" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Presets 1+2 umschalten" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Tastenkürzel zum Wechseln zwischen Preset-Slot 1 und Slot 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Zwischen Preset 1 und 2 wechseln" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Öffnet die Paradox-Mods-Seite des Autors." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "In liebevoller Erinnerung an Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Dieser Mod ist Mochi gewidmet. Sie war eine geliebte Hündin, adoptiert im Alter von 7 Jahren,\nund schenkte 13 Jahre Liebe und Freude. Ohne Mochi wäre dieser Mod nicht möglich." },
            };
        }

        public void Unload()
        {
        }
    }
}
