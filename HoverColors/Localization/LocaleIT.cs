// <copyright file="LocaleIT.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleIT.cs
// Purpose: Italian (it-IT) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/it-IT.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleIT : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleIT(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Azioni" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Tasti rapidi" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Info" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Colori strumenti" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Pannello" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Ripristina" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Tasti rapidi" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedica" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + strade" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Controlla i colori temporanei del contorno quando bulldozer o strumenti strada sono attivi.\n" +
                    "\n" +
                    "**1. Consigliato** usa il colore Avviso del gioco (giallo) per demolire e un blu vanilla più tenue per le strade.\n" +
                    "**2. Colori vanilla** ripristina il normale blu vanilla con bulldozer o strade attivi.\n" +
                    "**3. Tieni il mio colore** usa ovunque il colore scelto.\n" +
                    "\n" +
                    "Scopo: alcuni utenti/tester vedono male il colore personalizzato durante la demolizione.\n" +
                    "Offre colori ben visibili durante l’uso degli strumenti.\n" +
                    "Non sovrascrive il colore salvato automaticamente nel selettore."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Consigliato" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Colori vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Tieni il mio colore" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Abilita contorno oggetti sovrapposti" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Abilitato consigliato>\n" +
                    "Mantiene visibile il contorno vanilla rosso salmone quando un oggetto o rete è bloccato da sovrapposizioni.\n" +
                    "I limiti area, come i raggi fattoria di Industria specializzata, restano invariati.\n" +
                    "\n" +
                    "Funziona con tutti i modi Bulldozer + strade e non sovrascrive il colore salvato."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permetti colori personalizzati per NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Abilitato consigliato>\n" +
                    "Usa colore/trasparenza HC salvati quando piazzi dettagli NetLane come recinzioni, siepi, segni e simili.\n" +
                    "\n" +
                    "- Le strade normali seguono ancora l’impostazione Bulldozer + strade scelta dal menu.\n" +
                    "- Disattiva se vuoi che quegli strumenti usino il blu vanilla del gioco.\n" +
                    "- Il colore errore sovrapposizione resta prioritario se attivo (vanilla = rosso salmone)."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacità pannello" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Regola quanto è solido lo sfondo del pannello in città.\n" +
                    "\n" +
                    "**Più basso** lascia vedere di più la città.\n" +
                    "**100%** completamente solido.\n" +
                    "\n" +
                    "Cambia solo lo sfondo. Testo, icone e campioni di colore restano sempre ben leggibili.\n" +
                    "\n" +
                    "**Il cursore funziona solo con il pannello standard.** Il pannello scuro usa la superficie del pannello del gioco e segue invece l'impostazione Opacità interfaccia del gioco.\n" +
                    "Il pannello scuro funziona bene con Legacy o Modern UI. Quello standard funziona meglio con Modern UI."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Pannello più scuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Abilitato = <Pannello scuro>: pensato per Legacy UI; funziona anche in Modern UI se preferisci più contrasto.\n" +
                    "Disabilitato = <Pannello standard>: stile traslucido personalizzato Hover Colors.\n" +
                    "- Aspetto più chiaro e moderno.\n" +
                    "- Migliore per la maggior parte dei giocatori con la nuova Modern UI.\n" +
                    "\n" +
                    "Prova entrambi! Cambia solo lo sfondo di questo pannello del mod, non l’UI del gioco."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostra tooltip (consigliato)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Lascialo attivo> è consigliato per la maggior parte dei giocatori.\n" +
                    "Mostra un breve aiuto passando sui pulsanti Hover Colors.\n" +
                    "Se disattivato, clicca Info (i) nella barra del titolo o riattiva questa casella.\n" +
                    "Per evitare errori, i tooltip possono essere disattivati solo in questo menu Opzioni."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Ripristina valori del mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Riporta tutte le impostazioni Hover Colors a una nuova installazione: colori, spessore, strumenti, guide, pannello e tooltip.\n" +
                    "\n" +
                    "**Cancella anche i preset salvati (Set A e Set B).**\n" +
                    "\n" +
                    "I tasti rapidi non vengono modificati.\n" +
                    "È come installare Hover Colors per la prima volta."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Ripristinare tutte le impostazioni Hover Colors come una nuova installazione?\n\nI preset salvati (Set A e Set B) verranno cancellati." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Ripristina colori vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Ripristina l’aspetto del gioco: contorno, evidenziazione proprietario, riempimento, spessore, guide e distretti.\n" +
                    "\n" +
                    "Tutto il resto resta come impostato: Bulldozer/strade, anteprime strumenti, preset, pannello e tasti rapidi.\n" +
                    "\n" +
                    "Nota: puoi rimuovere il mod senza alcun reset. Le evidenziazioni tornano da sole ai valori del gioco.\n"+
                    "Questo pulsante ripristina rapidamente i colori predefiniti del gioco."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Ripristinare i colori controllati dal mod all’aspetto del gioco?\n\nPreset, strumenti e opzioni pannello restano invariati." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Apri/chiudi pannello principale" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Scorciatoia per <aprire / chiudere> il pannello Colori in città." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Attiva/disattiva pannello Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Occhio rapido On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Tasto opzionale per il pulsante Occhio: attiva/disattiva subito Evidenziazione + Riempimento.\n" +
                    "Nessun tasto assegnato di default per evitare conflitti." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Occhio rapido On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Anteprime Surface On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Tasto rapido per <nascondere o mostrare> le linee limite Surface durante il piazzamento." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Livello anteprima Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alterna preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Tasto rapido per passare tra\n" +
                    "<slot preset 1 e slot 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Passa tra preset 1 e 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versione" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods di Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Apri la pagina Paradox Mods dell’autore.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "In memoria di Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Questo mod è dedicato a Mochi.\n" +
                    "Era una cagnolina amata, adottata a 7 anni,\n" +
                    "e ha dato 13 anni di amore e gioia.\n" +
                    "Questo mod non sarebbe possibile senza Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
