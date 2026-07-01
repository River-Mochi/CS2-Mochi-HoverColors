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

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleIT : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Info" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Colori strumenti" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Pannello" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Scorciatoie" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guide" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedica" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + strade" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Gestisce i colori temporanei del contorno quando sono attivi bulldozer o strumenti strada.\n\n**1. Consigliato**: giallo di avviso per demolire e blu vanilla più morbido per le strade.\n**2. Colori vanilla**: torna al blu normale del gioco con bulldozer/strade.\n**3. Tieni il mio colore**: usa il tuo colore ovunque.\n\nUtile se il colore scelto è difficile da vedere durante la demolizione. Il colore salvato non viene sovrascritto." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Consigliato" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Colori vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Tieni il mio colore" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Contorno sovrapposizioni" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Consigliato: attivo>\nMantiene visibile il contorno rosso salmone vanilla quando un oggetto o una rete è bloccato da una sovrapposizione.\nI limiti di area, come i raggi delle fattorie dell’industria specializzata, non cambiano.\n\nFunziona con tutti i modi Bulldozer + strade e non sovrascrive il tuo colore." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Colori personalizzati per NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Consigliato: attivo>\nUsa il colore/trasparenza HC salvato per dettagli NetLane come recinzioni, siepi, segnaletica e strumenti simili.\n\n- Le strade normali seguono sempre l’impostazione Bulldozer + strade.\n- Disattiva se vuoi il blu vanilla del gioco.\n- Il colore errore per sovrapposizione ha sempre priorità se attivo (rosso salmone vanilla)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Tooltip del mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Attivo> = mostra gli aiuti del mod (consigliato [x]).\n<Disattivo> = nasconde i tooltip del mod.\nSi possono disattivare solo da questo menu Opzioni.\nIn città puoi riattivarli con Info (i) nella barra del titolo." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Pannello più scuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Attivo = <Pannello scuro>: per UI classica, o se preferisci più scuro.\nDisattivo = <Pannello standard>: stile traslucido del mod.\n- Più leggero e moderno.\n- Ideale con la nuova UI moderna.\n\nProvali entrambi. Cambia solo lo sfondo del pannello del mod, non la UI del gioco." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacità guide (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Regola l’opacità delle guide tratteggiate di allineamento per strade, recinzioni, props, ecc.\n\n**100%** valore del gioco.\n**Più basso** più trasparente.\n**0%** nasconde tutto.\nResta sopra il 15%, altrimenti la linea è difficile da vedere.\nLo stesso slider è nel pannello in città; i due restano sincronizzati." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Apri/chiudi pannello" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Scorciatoia per <aprire / chiudere> il pannello colori in città." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Mostra/nascondi pannello" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Anteprima Surface on/off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Scorciatoia per <nascondere o mostrare> i bordi dell’area Surface durante il posizionamento." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Anteprima Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alterna preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Scorciatoia per passare tra\n<slot preset 1 e slot 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Passa tra preset 1 e 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versione" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Apre la pagina Paradox Mods dell’autore.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "In dolce memoria di Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Questo mod è dedicato a Mochi.\nEra una cagnolina amatissima, adottata a 7 anni,\ne ha regalato 13 anni di amore e gioia.\nQuesto mod non sarebbe possibile senza Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
