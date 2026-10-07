// <copyright file="LocaleDE.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "Steuert temporäre Umrissfarben bei Bulldozer- oder Straßen-Tools.\n" +
                    "\n" +
                    "**1. Empfohlen** nutzt beim Abriss die Warnfarbe Gelb. Mit Straßen-/Weg-Tool wird das Vanilla-Blau an neuen Vorschauen und Segmenten unter dem Tool weicher. Normales Hover über bestehende Straßen nutzt weiter deine Umrissfarbe. Die große Füllung neuer Straßen folgt dem Vorschau-Farbfeld der Guidelines.\n" +
                    "**2. Vanilla-Toolfarben** stellt bei Bulldozer- oder Straßen-Tools das normale Vanilla-Blau wieder her.\n" +
                    "**3. Eigene Farbe behalten** nutzt deine Farbe überall.\n" +
                    "\n" +
                    "Hilfreich, wenn deine Farbe beim Abriss schlecht sichtbar ist.\n" +
                    "Auch der separate Straßen-Footprint-Guide folgt dem Vorschau-Farbfeld der Guidelines.\n" +
                    "Deine gespeicherte Farbe im Farbwähler wird nicht überschrieben."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Empfohlen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla-Toolfarben" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Eigene Farbe behalten" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Umriss für überlappende Objekte aktivieren" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Aktiviert empfohlen>\n" +
                    "Zeigt den Vanilla-lachsroten Umriss, wenn Objekt- oder Netzwerkplatzierung wegen Überlappung blockiert ist.\n" +
                    "Bereichsgrenzen wie Farmradius-Guides der Spezialindustrie bleiben unverändert.\n" +
                    "\n" +
                    "Funktioniert mit allen Bulldozer + Straßen-Modi und überschreibt deine gespeicherte Farbe nicht."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Eigene Farben für NetLanes erlauben" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Aktiviert empfohlen>\n" +
                    "Nutzt deine gespeicherte HC-Farbe/Transparenz für NetLane-Details wie Zäune, Hecken und Markierungen.\n" +
                    "\n" +
                    "- Normale Straßen folgen weiter deiner Bulldozer + Straßen-Auswahl.\n" +
                    "- Deaktivieren, wenn diese Tools Vanilla-Blau nutzen sollen.\n" +
                    "- Die Überlappungs-Fehlerfarbe hat weiter Vorrang (Vanilla = Lachsrot)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Dunkles oder Glas-Panel wählen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Dunkel (Vanilla)** nutzt das Panel des Spiels.\n" +
                    "- Passt automatisch zu Legacy UI oder Modern UI.\n" +
                    "- Folgt der Interface-Deckkraft des Spiels.\n" +
                    "\n" +
                    "**Glas (Benutzerdefiniert)** nutzt das hellere Hover-Colors-Glaspanel.\n" +
                    "- Selbst bei 100 % bleibt etwas Stadt sichtbar.\n" +
                    "- Fügt einen Regler für die Panel-Deckkraft hinzu.\n" +
                    "\n" +
                    "Probier beide aus! Nur der Hintergrund dieses Mod-Panels ändert sich, nicht die Spiel-UI.\n" +
                    "\n" +
                    "Tipp: Das Spiel verwischt den Bereich hinter Panels. Interface-Transparenz 0 % schaltet die Unschärfe überall aus; ab 1 % bleibt sie aktiv."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Dunkel (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Glas (Benutzerdefiniert)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Position der Panel-Schaltfläche" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Wähle oben links, oben rechts oder das Universal Mod Menu.\n" +
                    "**Spiel neu starten**, damit die Position geändert wird."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "Oben links" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "Oben rechts" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Panel-Deckkraft" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Hintergrund-Deckkraft für **Glas (Benutzerdefiniert)**.\n" +
                    "\n" +
                    "**30 %** = am transparentesten.\n" +
                    "**100 %** = fast deckend, etwas Stadt bleibt sichtbar.\n" +
                    "\n" +
                    "Nur der Hintergrund ändert sich; Text, Symbole und Farbfelder bleiben lesbar.\n" +
                    "\n" +
                    "**Dunkel (Vanilla)** folgt der Interface-Deckkraft des Spiels, daher ist dieser Regler ausgeblendet."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Tooltips anzeigen (empfohlen)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Eingeschaltet lassen> wird empfohlen.\n" +
                    "Zeigt kurze Hilfe beim Überfahren von Hover-Colors-Schaltflächen.\n" +
                    "Wenn deaktiviert, klicke auf Info (i) in der Titelleiste oder aktiviere diese Option wieder.\n" +
                    "Tooltips können nur in diesem Optionsmenü ausgeschaltet werden."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Auf Mod-Standard zurücksetzen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Setzt alle Hover-Colors-Einstellungen auf Neuinstallation zurück: Farben, Umrissdicke, Tool-Farben, Guidelines, Panel und Tooltips.\n" +
                    "\n" +
                    "**Gespeicherte Presets (Set A und Set B) werden ebenfalls gelöscht.**\n" +
                    "\n" +
                    "Tastenkürzel bleiben unverändert.\n" +
                    "Wie eine frische Hover-Colors-Installation."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Alle Hover-Colors-Einstellungen zurücksetzen?\n" +
                    "\n" +
                    "Gespeicherte Presets (Set A und Set B) werden gelöscht."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Farben auf Vanilla zurücksetzen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Stellt den Spiel-Look wieder her: Umriss, Besitzer-Hervorhebung, Füllung, Umrissdicke, Guidelines und Bezirke.\n" +
                    "\n" +
                    "Alles andere bleibt: Bulldozer/Straßen-Verhalten, Tool-Vorschauen, Presets, Panel und Tastenkürzel.\n" +
                    "\n" +
                    "Der Mod kann ohne Reset entfernt werden; Hervorhebungen kehren automatisch zu den Spielstandards zurück.\n" +
                    "Das ist nur ein schneller Farb-Reset."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Die vom Mod gesteuerten Farben auf den Spiel-Look zurücksetzen?\n" +
                    "\n" +
                    "Presets, Tool-Verhalten und Panel-Optionen bleiben erhalten."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Hauptpanel öffnen/schließen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Hotkey zum <Öffnen / Schließen> des Farbpanels in der Stadt."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover-Colors-Panel umschalten" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Schnelles Auge Ein/Aus" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Optionaler Hotkey fürs Auge: Hervorhebung + Füllfarbe sofort Ein/Aus.\n" +
                    "Standardmäßig unbelegt, um Tastenkonflikte zu vermeiden."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Schnelles Auge Ein/Aus" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Winkel-Tooltips Ein/Aus" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Hotkey für Winkel- sowie Platzieren/Rückgängig-Hinweise.\n" +
                    "Funktioniert beim Zeichnen neuer Straßen oder Wege.\n" +
                    "Kosten, Länge und Steigung bleiben sichtbar."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Winkel- und Maushinweise Ein/Aus" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Presets 1+2 umschalten" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Hotkey zum Wechseln zwischen\n" +
                    "<Preset-Slot 1 und Slot 2>."
                },
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
                    "Sie war ein geliebtes Hündchen, mit 7 Jahren adoptiert,\n" +
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
