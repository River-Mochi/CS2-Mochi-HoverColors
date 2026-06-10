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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Actions" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Ãœber" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Werkzeug-Farbverhalten" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Tastenbelegungen" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Hilfslinien" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Widmung" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + StraÃŸen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Steuert temporÃ¤re Umrissfarben, solange der Bulldozer oder StraÃŸenwerkzeuge aktiv sind.\n\n**1. Empfohlen** nutzt die Warnfarbe des Spiels (Gelb) fÃ¼r Abriss und ein weicheres Vanilla-Blau fÃ¼r StraÃŸen.\n**2. Vanilla-Werkzeugfarben** stellt das normale Vanilla-Blau des Spiels wieder her, solange Bulldozer oder StraÃŸenwerkzeuge aktiv sind.\n**3. Meine eigene Farbe behalten** nutzt Ã¼berall deine gewÃ¤hlte Farbe.\n\nZweck: Einige Nutzer/Tester finden ihre eigene Farbe beim AbreiÃŸen schwer zu sehen.\nDiese Optionen bieten gut sichtbare Farben wÃ¤hrend der Werkzeugnutzung.\nDie automatisch gespeicherte eigene Farbe im FarbwÃ¤hler wird nicht Ã¼berschrieben." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Empfohlen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla-Werkzeugfarben" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Meine eigene Farbe behalten" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Umriss fÃ¼r Ã¼berlappende Elemente aktivieren" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Aktiviert wird empfohlen>\nHÃ¤lt den Vanilla-lachsroten Umriss des Spiels sichtbar, wenn Objekt- oder Netzwerkplatzierung durch Ã¼berlappende Elemente blockiert ist.\nFlÃ¤chenbegrenzungen, wie Radius-Hilfen fÃ¼r spezialisierte Industrie-Farmen, bleiben unverÃ¤ndert.\n\nFunktioniert mit allen Bulldozer + StraÃŸen-Modi und Ã¼berschreibt deine gespeicherte eigene Farbe nicht." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Eigene Farben fÃ¼r NetLanes erlauben" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Aktiviert wird empfohlen>\nVerwendet deine gespeicherte HC-Farbe/Transparenz beim Platzieren von NetLane-Details wie ZÃ¤unen, Hecken, Markierungen und Ã¤hnlichen spur-basierten Werkzeugen.\n\n- Normale StraÃŸen folgen weiterhin der Bulldozer + StraÃŸen-Einstellung aus der Dropdown-Liste.\n- Deaktiviere dies, wenn diese Werkzeuge stattdessen das Vanilla-Blau des Spiels verwenden sollen.\n- Die Ãœberlappungs-Fehlerfarbe hat bei Aktivierung weiterhin Vorrang (Vanilla-Fehlerfarbe = Lachsrot)." },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Dunkleres Panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Aktiviert nutzt <Dunkles Panel>: fÃ¼r Spieler mit Legacy UI gemacht; kann auch in der modernen UI genutzt werden, wenn du ein dunkleres Panel magst.\nDeaktiviert nutzt <Standard-Panel>: eigener transparenter Hover Colors-Stil.\n- Hellerer, modernerer Look.\n- Am besten fÃ¼r die meisten Spieler mit der neuen modernen Spiel-UI.\n\nProbier beides aus und nimm, was dir besser gefÃ¤llt. Das Ã¤ndert nur den Hintergrund dieses Mod-Panels, nicht die Spiel-UI." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Deckkraft der Hilfslinien (Alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Steuert die Deckkraft gestrichelter Ausrichtungshilfen, nÃ¼tzlich beim Platzieren von StraÃŸen, ZÃ¤unen, Props usw.\n\n**100%** behÃ¤lt den Vanilla-Standard-Look.\n**Niedriger** macht Hilfslinien transparenter.\n**0%** blendet sie vollstÃ¤ndig aus - <Nicht empfohlen>.\nBleib mÃ¶glichst Ã¼ber 15%, sonst ist kaum zu erkennen, was passiert.\nDerselbe Regler befindet sich im Stadt-Mod-Panel. Beide sind synchronisiert;\nwenn du diesen Ã¤nderst, Ã¤ndert sich der in der Stadt ebenfalls." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Hauptpanel Ã¶ffnen/schlieÃŸen" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "TastenkÃ¼rzel zum Ã–ffnen / SchlieÃŸen des Hover-Objekt-Farbpanels in der Stadt." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors-Panel umschalten" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Vorschauen des OberflÃ¤chenwerkzeugs ein/aus" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "TastenkÃ¼rzel zum Ausblenden oder Wiederherstellen aktiver Begrenzungsvorschau-Linien des OberflÃ¤chenwerkzeugs beim Platzieren von FlÃ¤chen." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "OberflÃ¤chen-Vorschau-Ebene Ein/Aus" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Voreinstellungen 1+2 umschalten" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "TastenkÃ¼rzel zum Wechseln zwischen Voreinstellungsplatz 1 und 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Zwischen Voreinstellungen 1 und 2 umschalten" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Ã–ffnet die Paradox Mods-Seite des Autors." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "In liebevoller Erinnerung an Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Dieser Mod ist Mochi gewidmet. Sie war eine geliebte HÃ¼ndin, adoptiert im Alter von 7 Jahren,\nund schenkte 13 Jahre Liebe und Freude. Ohne Mochi wÃ¤re dieser Mod nicht mÃ¶glich gewesen." },
            };
        }

        public void Unload()
        {
        }
    }
}
