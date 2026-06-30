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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportamento colore strumenti" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Pannello" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Tasti rapidi" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guide" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedica" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + Strade" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Controlla i colori temporanei del contorno mentre sono attivi gli strumenti bulldozer o strada.\n\n**1. Consigliato** usa il colore di avviso del gioco (giallo) per la demolizione e un blu vanilla più morbido per le strade.\n**2. Colori vanilla strumenti** ripristina il normale blu vanilla del gioco mentre usi bulldozer o strade.\n**3. Mantieni il mio colore personalizzato** usa ovunque il colore scelto.\n\nScopo: alcuni utenti/tester trovano il proprio colore difficile da vedere durante la demolizione.\nQueste opzioni offrono colori più visibili durante l’uso degli strumenti.\nNon sovrascrive il colore personalizzato salvato automaticamente nel selettore." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Consigliato" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Colori vanilla strumenti" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Mantieni il mio colore personalizzato" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Attiva contorno elementi sovrapposti" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Attivato consigliato>\nMantiene visibile il contorno rosso salmone vanilla del gioco quando il posizionamento di oggetti o reti è bloccato da elementi sovrapposti.\nI limiti d’area, come le guide del raggio delle fattorie di industria specializzata, non vengono modificati.\n\nFunziona con tutti i modi Bulldozer + Strade e non sovrascrive il tuo colore salvato." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Consenti colori personalizzati per NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Attivato consigliato>\nUsa il colore/trasparenza salvato di Colori al passaggio per posizionare dettagli NetLane come recinzioni, siepi, segnaletica e strumenti simili basati sulle corsie.\n\n- Le strade normali seguono ancora l’impostazione Bulldozer + Strade scelta dal menu.\n- Disattiva questa opzione se vuoi che quegli strumenti usino il contorno blu vanilla del gioco.\n- Il colore di errore per sovrapposizione ha comunque la priorità se attivo (colore vanilla = rosso salmone)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Tooltip di Colori al passaggio" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Attivato> = mostra i tooltip di aiuto di Colori al passaggio (consigliato [x]).\n<Disattivato> = nasconde i tooltip di questo mod.\nI tooltip possono essere disattivati solo in questo menu Opzioni.\nPuoi però riattivarli in città: fai clic sul pulsante Info (i) nella barra del titolo." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Pannello più scuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Attivato = <Pannello scuro>: pensato per chi usa la UI legacy; utilizzabile anche con la UI moderna se preferisci un pannello più scuro.\nDisattivato = <Pannello standard>: stile traslucido personalizzato di Colori al passaggio.\n- Aspetto più leggero e moderno.\n- Ideale per la maggior parte dei giocatori con la nuova UI moderna.\n\nProvali entrambi e scegli quello che preferisci. Cambia solo lo sfondo del pannello del mod, non la UI del gioco." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacità guide (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Controlla l’opacità delle guide di allineamento tratteggiate, utile mentre posizioni strade, recinzioni, props, ecc.\n\n**100%** mantiene l’aspetto vanilla predefinito.\n**Più basso** rende le guide più trasparenti.\n**0%** le nasconde completamente - <Non consigliato>.\nMeglio restare sopra il 15% o diventa difficile capire cosa succede.\nLo stesso cursore è nel pannello in città. Sono sincronizzati;\nse cambi questo, cambia anche quello in città." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Apri/chiudi pannello principale" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Tasto rapido per aprire/chiudere il pannello in città dei colori degli oggetti al passaggio." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Attiva/disattiva pannello Colori al passaggio" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Attiva/disattiva anteprime strumento Superficie" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Tasto rapido per nascondere o ripristinare le linee di anteprima dei confini dello strumento Superficie mentre posizioni superfici." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Livello anteprima strumento Superficie On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alterna preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Tasto rapido per passare dal preset 1 al preset 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Alterna tra preset 1 e 2" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versione" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Apre la pagina Paradox Mods dell’autore." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "In amorevole memoria di Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Questo mod è dedicato a Mochi. Era una cagnolina amata, adottata a 7 anni,\ne ha donato 13 anni di amore e gioia. Questo mod non sarebbe possibile senza Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
