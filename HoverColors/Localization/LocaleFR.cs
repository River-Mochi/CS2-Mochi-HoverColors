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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Couleurs des outils" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panneau" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Raccourcis clavier" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guides" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dédicace" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + routes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Règle les couleurs temporaires des contours avec le bulldozer ou les outils route.\n\n**1. Recommandé** : jaune d’avertissement pour démolir, bleu vanilla plus doux pour les routes.\n**2. Couleurs vanilla** : bleu vanilla du jeu avec bulldozer/routes.\n**3. Ma couleur perso** : garde votre couleur partout.\n\nUtile si votre couleur perso est dure à voir pendant la démolition. Votre couleur enregistrée n’est pas écrasée." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recommandé" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Couleurs vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Ma couleur perso" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Contour des chevauchements" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Recommandé : activé>\nGarde le contour rouge saumon du jeu quand un objet ou réseau ne peut pas être placé à cause d’un chevauchement.\nLes limites de zone, comme les rayons de ferme d’industrie spécialisée, ne changent pas.\n\nFonctionne avec tous les modes Bulldozer + routes et n’écrase pas votre couleur." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Couleurs perso pour NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Recommandé : activé>\nUtilise votre couleur/transparence HC pour les détails NetLane : clôtures, haies, marquages et outils similaires.\n\n- Les routes normales suivent toujours le réglage Bulldozer + routes.\n- Désactivez pour utiliser le bleu vanilla du jeu.\n- La couleur d’erreur de chevauchement reste prioritaire si activée (rouge saumon vanilla)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Infobulles du mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Activé> = affiche l’aide du mod (recommandé [x]).\n<Désactivé> = masque les infobulles du mod.\nElles se désactivent seulement dans ce menu Options.\nEn ville, cliquez sur Info (i) dans la barre de titre pour les réactiver." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Panneau plus sombre" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Activé = <Panneau sombre> : pour l’ancienne interface, ou si vous aimez plus sombre.\nDésactivé = <Panneau standard> : style translucide du mod.\n- Plus léger et moderne.\n- Idéal avec la nouvelle interface moderne.\n\nEssayez les deux. Cela change seulement le fond du panneau du mod, pas l’interface du jeu." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacité des guides (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Règle l’opacité des guides d’alignement en pointillés pour routes, clôtures, props, etc.\n\n**100%** défaut du jeu.\n**Plus bas** plus transparent.\n**0%** masque tout.\nRestez au-dessus de 15 %, sinon la ligne devient dure à voir.\nMême curseur sur le panneau en ville : les deux restent synchronisés." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ouvrir/fermer le panneau" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Raccourci pour <ouvrir / fermer> le panneau de couleurs en ville." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Afficher/masquer le panneau" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Aperçus Surface on/off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Raccourci pour <masquer ou afficher> les limites de l’outil Surface pendant le placement." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Aperçu Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Basculer préréglages 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Raccourci pour basculer entre\n<l’emplacement 1 et l’emplacement 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Basculer entre préréglages 1 et 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Ouvre la page Paradox Mods de l’auteur.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "À la douce mémoire de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Ce mod est dédié à Mochi.\nC’était une petite chienne adorée, adoptée à 7 ans,\net elle a donné 13 ans d’amour et de joie.\nCe mod n’existerait pas sans Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
