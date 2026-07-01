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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Werkzeugfarben" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Tastenbelegung" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Hilfslinien" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Widmung" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + Straßen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Steuert temporäre Umrissfarben, solange Bulldozer oder Straßenwerkzeuge aktiv sind.\n\n**1. Empfohlen**: Warnfarbe Gelb fürs Abreißen, sanfteres Vanilla-Blau für Straßen.\n**2. Vanilla-Farben**: normales Blau des Spiels bei Bulldozer/Straßen.\n**3. Meine Farbe behalten**: deine Farbe überall.\n\nHilft, wenn die eigene Farbe beim Abreißen schlecht sichtbar ist. Deine gespeicherte Farbe wird nicht überschrieben." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Empfohlen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla-Farben" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Meine Farbe behalten" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Umriss bei Überschneidung" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Empfohlen: Ein>\nBehält den lachsroten Vanilla-Umriss bei, wenn ein Objekt oder Netzwerk wegen Überschneidung blockiert ist.\nFlächenlimits, z. B. Farmradien der Spezialindustrie, bleiben unverändert.\n\nFunktioniert mit allen Bulldozer + Straßen-Modi und überschreibt deine Farbe nicht." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Eigene Farben für NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Empfohlen: Ein>\nNutzt deine HC-Farbe/Transparenz für NetLane-Details wie Zäune, Hecken, Markierungen und ähnliche Werkzeuge.\n\n- Normale Straßen folgen weiter der Bulldozer + Straßen-Einstellung.\n- Ausschalten, wenn diese Werkzeuge Vanilla-Blau nutzen sollen.\n- Fehlerfarbe bei Überschneidung gewinnt weiterhin, wenn aktiv (Vanilla-Lachsrot)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Tooltips für den Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Ein> = zeigt Hilfetooltips des Mods (empfohlen [x]).\n<Aus> = blendet Tooltips für diesen Mod aus.\nAusschalten geht nur in diesem Optionsmenü.\nIn der Stadt kannst du sie über Info (i) in der Titelleiste wieder einschalten." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Dunkleres Panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ein = <Dunkles Panel>: für Legacy-UI, oder wenn du es dunkler magst.\nAus = <Standardpanel>: transparenter Mod-Stil.\n- Heller und moderner.\n- Am besten für die neue moderne UI.\n\nProbier beides aus. Es ändert nur den Hintergrund dieses Mod-Panels, nicht die Spiel-UI." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Deckkraft der Hilfslinien (Alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Regelt die Deckkraft gestrichelter Ausrichtungshilfen für Straßen, Zäune, Props usw.\n\n**100%** Spielstandard.\n**Niedriger** transparenter.\n**0%** blendet alles aus.\nBleib über 15 %, sonst ist die Linie schwer zu sehen.\nDerselbe Regler ist im Stadtpanel; beide bleiben synchron." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Hauptpanel öffnen/schließen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Hotkey zum <Öffnen / Schließen> des Farbpanels in der Stadt." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Panel ein-/ausblenden" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface-Vorschau ein/aus" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Hotkey zum <Ausblenden oder Anzeigen> aktiver Surface-Grenzlinien beim Platzieren." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface-Vorschau On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Presets 1+2 umschalten" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Hotkey zum Wechsel zwischen\n<Preset-Slot 1 und Slot 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Zwischen Preset 1 und 2 wechseln" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Öffnet die Paradox Mods-Seite des Autors.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "In liebevoller Erinnerung an Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Dieser Mod ist Mochi gewidmet.\nSie war eine geliebte Hündin, mit 7 Jahren adoptiert,\nund schenkte 13 Jahre Liebe und Freude.\nOhne Mochi wäre dieser Mod nicht möglich." },
            };
        }

        public void Unload()
        {
        }
    }
}
