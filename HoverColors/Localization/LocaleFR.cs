// <copyright file="LocaleFR.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleFR.cs
// Purpose: French (fr-FR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/fr-FR.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleFR : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleFR(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "À propos" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportement des couleurs d’outils" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panneau" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Raccourcis clavier" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guides" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dédicace" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + Routes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Contrôle les couleurs temporaires du contour quand les outils Bulldozer ou Route sont actifs.\n\n**1. Recommandé** utilise la couleur d’avertissement du jeu (jaune) pour la démolition et un bleu vanilla plus doux pour les routes.\n**2. Couleurs vanilla des outils** restaure le bleu vanilla normal du jeu avec Bulldozer ou Route.\n**3. Garder ma couleur personnalisée** utilise partout la couleur choisie.\n\nBut : certains joueurs trouvent leur couleur personnalisée difficile à voir pendant la démolition.\nCes options donnent des couleurs plus visibles pendant l’utilisation des outils.\nCela n’écrase pas la couleur personnalisée enregistrée automatiquement dans le sélecteur." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recommandé" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Couleurs vanilla des outils" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Garder ma couleur personnalisée" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Activer le contour des éléments en chevauchement" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Activé recommandé>\nGarde visible le contour rouge saumon vanilla du jeu quand le placement d’un objet ou réseau est bloqué par un chevauchement.\nLes limites de zone, comme les rayons de ferme de l’industrie spécialisée, ne sont pas modifiées.\n\nFonctionne avec tous les modes Bulldozer + Routes et n’écrase pas votre couleur enregistrée." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Autoriser les couleurs personnalisées pour les NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Activé recommandé>\nUtilise votre couleur/transparence de Couleurs de survol enregistrée pour placer les détails NetLane comme clôtures, haies, marquages et outils similaires.\n\n- Les routes normales suivent toujours le réglage Bulldozer + Routes choisi dans la liste.\n- Désactivez ceci si vous voulez que ces outils utilisent plutôt le contour bleu vanilla du jeu.\n- La couleur d’erreur de chevauchement reste prioritaire si elle est activée (rouge saumon vanilla)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Infobulles de Couleurs de survol" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Activé> = affiche les infobulles d’aide de Couleurs de survol (recommandé [x]).\n<Désactivé> = masque les infobulles de ce mod.\nLes infobulles ne peuvent être désactivées que dans ce menu Options.\nMais vous pouvez les réactiver en ville : cliquez sur le bouton Info (i) de la barre de titre." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Panneau plus sombre" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Activé = <Panneau sombre> : pensé pour l’interface héritée; utilisable aussi avec l’interface moderne si vous préférez un panneau plus sombre.\nDésactivé = <Panneau standard> : style translucide personnalisé de Couleurs de survol.\n- Aspect plus léger et moderne.\n- Idéal pour la plupart des joueurs avec la nouvelle interface moderne.\n\nEssayez les deux et choisissez votre préférence. Cela ne change que l’arrière-plan du panneau du mod, pas l’interface du jeu." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacité des guides (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Contrôle l’opacité des guides d’alignement en pointillés, utile pour placer routes, clôtures, props, etc.\n\n**100%** garde l’apparence vanilla.\n**Plus bas** rend les guides plus transparents.\n**0%** les masque complètement - <Non recommandé>.\nIl est conseillé de rester au-dessus de 15 %, sinon il devient difficile de voir ce qui se passe.\nLe même curseur existe dans le panneau en ville. Les deux sont synchronisés;\nsi vous changez celui-ci, celui en ville change aussi." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ouvrir/fermer le panneau principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Raccourci pour ouvrir/fermer le panneau en ville des couleurs de survol des objets." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Basculer le panneau Couleurs de survol" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Activer/désactiver les aperçus de l’outil Surface" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Raccourci pour masquer ou restaurer les lignes d’aperçu de limite de l’outil Surface pendant le placement." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Couche d’aperçu Surface On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Basculer presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Raccourci pour passer du preset 1 au preset 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Basculer entre les presets 1 et 2" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Ouvre la page Paradox Mods de l’auteur." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "À la mémoire de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Ce mod est dédié à Mochi. Cette adorable chienne, adoptée à 7 ans,\na donné 13 ans d’amour et de joie. Ce mod n’aurait pas été possible sans Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
