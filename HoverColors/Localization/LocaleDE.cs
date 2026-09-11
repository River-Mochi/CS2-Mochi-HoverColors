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

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleDE : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Tastenkürzel" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Info" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Tool-Farbverhalten" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Zurücksetzen" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Tastenkürzel" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Widmung" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + Straßen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Steuert temporäre Umrissfarben, wenn Bulldozer- oder Straßen-Tools aktiv sind.\n" +
                    "\n" +
                    "**1. Empfohlen** nutzt die Warnfarbe des Spiels (Gelb) für Abriss und ein weicheres Vanilla-Blau für Straßen.\n" +
                    "**2. Vanilla-Toolfarben** stellt das normale Vanilla-Blau des Spiels bei Bulldozer- oder Straßen-Tools wieder her.\n" +
                    "**3. Eigene Farbe behalten** nutzt deine gewählte Farbe überall.\n" +
                    "\n" +
                    "Zweck: Manche Nutzer/Tester sehen ihre eigene Farbe beim Bulldozern schlecht.\n" +
                    "Das bietet gut sichtbare Farben während der Tool-Nutzung.\n" +
                    "Deine automatisch gespeicherte Farbe im Farbwähler wird nicht überschrieben."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Empfohlen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla-Toolfarben" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Eigene Farbe behalten" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Umriss für überlappende Objekte aktivieren" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Aktiviert empfohlen>\n" +
                    "Behält den Vanilla-Lachsrot-Umriss sichtbar, wenn Objekt- oder Netzwerkplatzierung durch Überlappungen blockiert ist.\n" +
                    "Bereichsgrenzen, z. B. Farmradius-Guides der Spezialindustrie, bleiben unberührt.\n" +
                    "\n" +
                    "Funktioniert mit allen Bulldozer + Straßen-Modi und überschreibt deine gespeicherte Farbe nicht."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Eigene Farben für NetLanes erlauben" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Aktiviert empfohlen>\n" +
                    "Nutzt deine gespeicherte HC-Farbe/Transparenz beim Platzieren von NetLane-Details wie Zäunen, Hecken, Markierungen und ähnlichen lane-basierten Tools.\n" +
                    "\n" +
                    "- Normale Straßen folgen weiter der Bulldozer + Straßen-Auswahl aus dem Dropdown.\n" +
                    "- Deaktivieren, wenn diese Tools stattdessen das Vanilla-Blau des Spiels nutzen sollen.\n" +
                    "- Die Überlappungs-Fehlerfarbe hat bei Aktivierung weiter Vorrang (Vanilla-Fehlerfarbe = Lachsrot)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Dunkleres Panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Aktiviert = <Dunkles Panel>: nutzt die eigenen Panel-Farben des Spiels und passt damit zu Vanilla-Panels.\n" +
                    "- Bei 100% vollständig deckend.\n" +
                    "Deaktiviert = <Standard-Panel>: hellerer, transparenter Hover-Colors-Stil.\n" +
                    "- Glas-Look; selbst bei 100% bleibt etwas von der Stadt sichtbar.\n" +
                    "\n" +
                    "Beide Panel-Stile nutzen den Deckkraft-Regler unten und sehen in Modern UI und Legacy UI gleich aus.\n" +
                    "\n" +
                    "Probier beide aus! Dies ändert nur den Hintergrund dieses Mod-Panels, nicht die Spiel-UI."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Panel-Deckkraft" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Bestimmt, wie deckend der Hintergrund des Panels in der Stadt ist.\n" +
                    "\n" +
                    "**30% Glas** ist am transparentesten und klarsten.\n" +
                    "**100%** ist beim dunklen Panel vollständig und beim Standard-Panel fast vollständig deckend.\n" +
                    "\n" +
                    "Nur der Hintergrund ändert sich. Text, Symbole und Farbfelder bleiben bei jeder Einstellung gut lesbar.\n" +
                    "\n" +
                    "**Funktioniert mit beiden Panel-Stilen.** Die Interface-Deckkraft des Spiels beeinflusst dieses Panel nicht mehr."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Tooltips anzeigen (empfohlen)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Eingeschaltet lassen> wird für die meisten Spieler empfohlen.\n" +
                    "Zeigt kurze Hilfe beim Überfahren von Hover-Colors-Schaltflächen.\n" +
                    "Wenn deaktiviert, klicke auf Info (i) in der Titelleiste oder aktiviere diese Option wieder.\n" +
                    "Damit nichts versehentlich verschwindet, können Tooltips nur in diesem Optionsmenü deaktiviert werden."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Auf Mod-Standard zurücksetzen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Setzt alle Hover-Colors-Einstellungen wie bei einer Neuinstallation zurück: Farben, Umrissdicke, Tool-Farben, Guidelines, Panel und Tooltips.\n" +
                    "\n" +
                    "**Dabei werden auch deine gespeicherten Presets (Set A und Set B) gelöscht.**\n" +
                    "\n" +
                    "Tastenkürzel bleiben unverändert.\n" +
                    "Das entspricht einer frischen Hover-Colors-Installation."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Alle Hover-Colors-Einstellungen wie bei einer Neuinstallation zurücksetzen?\n\nDeine gespeicherten Presets (Set A und Set B) werden gelöscht." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Farben auf Vanilla zurücksetzen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Stellt den Spiel-Look wieder her: Umriss, Besitzer-Hervorhebung, Füllung, Umrissdicke, Guidelines und Bezirke.\n" +
                    "\n" +
                    "Alles andere bleibt wie eingestellt – Bulldozer/Straßen-Verhalten, Tool-Vorschauen, Presets, Panel-Optionen und Tastenkürzel.\n" +
                    "\n" +
                    "Hinweis: Der Mod kann ohne Reset entfernt werden. Hervorhebungen kehren automatisch zu den Spielstandards zurück.\n"+
                    "Dies ist nur ein Schnell-Reset auf die Standardfarben des Spiels."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Die vom Mod gesteuerten Farben auf den Spiel-Look zurücksetzen?\n\nPresets, Tool-Verhalten und Panel-Optionen bleiben erhalten." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Hauptpanel öffnen/schließen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Hotkey zum <Öffnen / Schließen> des Farbpanels in der Stadt." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover-Colors-Panel umschalten" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Schnelles Auge Ein/Aus" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Optionaler Hotkey für das Auge in der Titelleiste: Hervorhebung + Füllfarbe sofort Aus/Ein.\n" +
                    "Standardmäßig unbelegt, um Tastenkonflikte zu vermeiden." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Schnelles Auge Ein/Aus" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface-Tool-Vorschau ein/aus" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Hotkey zum <Ausblenden oder Anzeigen> aktiver Surface-Grenzvorschauen beim Platzieren von Flächen." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface-Vorschauebene Ein/Aus" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Presets 1+2 umschalten" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Hotkey zum Wechseln zwischen\n" +
                    "<Preset-Slot 1 und Slot 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Zwischen Preset 1 und 2 wechseln" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochis Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Paradox-Mods-Seite des Autors öffnen.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "In liebevoller Erinnerung an Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Dieser Mod ist Mochi gewidmet.\n" +
                    "Sie war ein geliebtes Hündchen, adoptiert mit 7 Jahren,\n" +
                    "und schenkte 13 Jahre Liebe und Freude.\n" +
                    "Ohne Mochi wäre dieser Mod nicht möglich."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
