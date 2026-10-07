// <copyright file="LocaleIT.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "Controlla i colori temporanei del contorno con bulldozer o strumenti strada.\n" +
                    "\n" +
                    "**1. Consigliato** usa il giallo di avviso per demolire. Con uno strumento strada/sentiero, attenua il blu vanilla sulle nuove anteprime e sui segmenti sotto lo strumento. Il normale passaggio su strade esistenti usa ancora il tuo colore Contorno. Il grande riempimento delle nuove strade segue il campione anteprima delle Guide.\n" +
                    "**2. Colori vanilla** ripristina il normale blu vanilla con bulldozer o strade.\n" +
                    "**3. Tieni il mio colore** usa il tuo colore ovunque.\n" +
                    "\n" +
                    "Utile se il tuo colore si vede male durante la demolizione.\n" +
                    "Anche la guida separata dell’ingombro strada segue il campione anteprima delle Guide.\n" +
                    "Il colore salvato nel selettore non viene sovrascritto."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Consigliato" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Colori vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Tieni il mio colore" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Abilita contorno oggetti sovrapposti" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Abilitato consigliato>\n" +
                    "Mantiene il contorno vanilla rosso salmone quando un oggetto o rete non può essere piazzato per sovrapposizione.\n" +
                    "I limiti area, come i raggi delle fattorie specializzate, restano invariati.\n" +
                    "\n" +
                    "Funziona con tutti i modi Bulldozer + strade senza sovrascrivere il colore salvato."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permetti colori personalizzati per NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Abilitato consigliato>\n" +
                    "Usa colore/trasparenza HC salvati per dettagli NetLane come recinzioni, siepi e segnaletica.\n" +
                    "\n" +
                    "- Le strade normali seguono ancora Bulldozer + strade.\n" +
                    "- Disattiva per usare il blu vanilla del gioco su questi strumenti.\n" +
                    "- Il colore errore sovrapposizione resta prioritario (vanilla = rosso salmone)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Scegli pannello scuro o vetro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Scuro (Vanilla)** usa il pannello del gioco.\n" +
                    "- Si adatta a Legacy UI o Modern UI.\n" +
                    "- Segue l’Opacità interfaccia del gioco.\n" +
                    "\n" +
                    "**Vetro (Personalizzato)** usa il pannello vetro più chiaro di Hover Colors.\n" +
                    "- Anche al 100% lascia intravedere un po’ la città.\n" +
                    "- Aggiunge uno slider per l’opacità del pannello.\n" +
                    "\n" +
                    "Provali entrambi! Cambia solo lo sfondo di questo pannello, non l’interfaccia del gioco.\n" +
                    "\n" +
                    "Suggerimento: il gioco sfoca ciò che c’è dietro i pannelli. Trasparenza interfaccia 0% disattiva lo sfocato; 1% o più lo mantiene."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Scuro (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Vetro (Personalizzato)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Posizione del pulsante del pannello" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Scegli in alto a sinistra, in alto a destra o Universal Mod Menu.\n" +
                    "**Riavvia il gioco** per applicare la nuova posizione."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "In alto a sinistra" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "In alto a destra" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacità pannello" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Opacità dello sfondo del pannello **Vetro (Personalizzato)**.\n" +
                    "\n" +
                    "**30%** = più trasparente.\n" +
                    "**100%** = quasi opaco, ma lascia intravedere la città.\n" +
                    "\n" +
                    "Cambia solo lo sfondo; testo, icone e colori restano leggibili.\n" +
                    "\n" +
                    "**Scuro (Vanilla)** segue l’Opacità interfaccia del gioco, quindi questo slider viene nascosto."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostra tooltip (consigliato)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Lascialo attivo> è consigliato.\n" +
                    "Mostra un breve aiuto passando sui pulsanti Hover Colors.\n" +
                    "Se lo disattivi, clicca Info (i) nella barra del titolo o riattiva questa opzione.\n" +
                    "I tooltip possono essere disattivati solo in questo menu Opzioni."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Ripristina valori del mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Riporta tutte le impostazioni Hover Colors a una nuova installazione: colori, spessore contorno, strumenti, guide, pannello e tooltip.\n" +
                    "\n" +
                    "**Cancella anche i preset salvati (Set A e Set B).**\n" +
                    "\n" +
                    "I tasti rapidi non cambiano.\n" +
                    "È come installare Hover Colors per la prima volta."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Ripristinare tutte le impostazioni Hover Colors?\n" +
                    "\n" +
                    "I preset salvati (Set A e Set B) verranno cancellati."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Ripristina colori vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Ripristina l’aspetto del gioco: contorno, evidenziazione proprietario, riempimento, spessore, guide e distretti.\n" +
                    "\n" +
                    "Tutto il resto resta uguale: Bulldozer/strade, anteprime, preset, pannello e tasti rapidi.\n" +
                    "\n" +
                    "Puoi rimuovere il mod senza reset; le evidenziazioni tornano da sole ai valori del gioco.\n" +
                    "Questo è solo un reset rapido dei colori."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Ripristinare i colori controllati dal mod all’aspetto del gioco?\n" +
                    "\n" +
                    "Preset, strumenti e opzioni pannello restano invariati."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Apri/chiudi pannello principale" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Scorciatoia per <aprire / chiudere> il pannello Colori in città."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Attiva/disattiva pannello Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Occhio rapido On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Tasto opzionale per il pulsante Occhio: attiva/disattiva subito Evidenziazione + Riempimento.\n" +
                    "Nessun tasto assegnato di default per evitare conflitti."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Occhio rapido On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Tooltip angolo On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Scorciatoia per i riquadri angolo e Posiziona/Annulla.\n" +
                    "Funziona mentre disegni nuove strade o sentieri.\n" +
                    "Costo, lunghezza e pendenza restano visibili."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Angoli e suggerimenti mouse On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alterna preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Tasto rapido per passare tra\n" +
                    "<slot preset 1 e slot 2>."
                },
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
                    "In ricordo di Mochi, con amore."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Questo mod è dedicato a Mochi.\n" +
                    "Era una cagnolina amata, adottata a 7 anni,\n" +
                    "e ha regalato 13 anni di amore e gioia.\n" +
                    "Questo mod non sarebbe possibile senza Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
