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

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleFR : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Raccourcis" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "À propos" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Couleurs d'outil" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panneau" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Réinitialiser" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Raccourcis" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dédicace" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + routes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Contrôle les couleurs temporaires du contour quand le bulldozer ou les outils route sont actifs.\n" +
                    "\n" +
                    "**1. Recommandé** utilise la couleur d'avertissement du jeu (jaune) pour démolir et un bleu vanilla plus doux pour les routes.\n" +
                    "**2. Couleurs vanilla** restaure le bleu vanilla normal du jeu avec bulldozer ou routes.\n" +
                    "**3. Garder ma couleur** utilise votre couleur choisie partout.\n" +
                    "\n" +
                    "But : certains joueurs/testeurs voient mal leur couleur avec le bulldozer.\n" +
                    "Offre des couleurs très visibles pendant l'utilisation des outils.\n" +
                    "N'écrase pas la couleur personnalisée sauvegardée dans le sélecteur."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recommandé" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Couleurs vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Garder ma couleur" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Activer le contour des objets en chevauchement" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Activé recommandé>\n" +
                    "Garde visible le contour rouge saumon vanilla quand le placement est bloqué par un chevauchement.\n" +
                    "Les limites de zone, comme les rayons de ferme de l'Industrie spécialisée, restent inchangées.\n" +
                    "\n" +
                    "Fonctionne avec tous les modes Bulldozer + routes et n'écrase pas votre couleur sauvegardée."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Autoriser les couleurs perso pour NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Activé recommandé>\n" +
                    "Utilise votre couleur/transparence HC sauvegardée pour les détails NetLane : clôtures, haies, marquages, etc.\n" +
                    "\n" +
                    "- Les routes normales suivent toujours le réglage Bulldozer + routes choisi dans la liste.\n" +
                    "- Désactivez pour que ces outils utilisent plutôt le bleu vanilla du jeu.\n" +
                    "- La couleur d'erreur de chevauchement reste prioritaire si activée (vanilla = rouge saumon)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Panneau plus sombre" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Activé = <Panneau sombre> : utilise les couleurs de panneau du jeu pour correspondre aux panneaux vanilla.\n" +
                    "- Totalement opaque à 100%.\n" +
                    "Désactivé = <Panneau standard> : style Hover Colors plus clair et translucide.\n" +
                    "- Effet verre ; même à 100%, la ville reste légèrement visible.\n" +
                    "\n" +
                    "Les deux styles utilisent le curseur d'opacité ci-dessous et ont le même rendu en Modern UI et Legacy UI.\n" +
                    "\n" +
                    "Essayez les deux ! Cela change uniquement le fond de ce panneau du mod, pas l'interface du jeu."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacité du panneau" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Règle l'opacité du fond du panneau en ville.\n" +
                    "\n" +
                    "**30% Verre** est le rendu le plus transparent et clair.\n" +
                    "**100%** est totalement opaque sur le panneau sombre et presque opaque sur le panneau standard.\n" +
                    "\n" +
                    "Seul le fond change. Le texte, les icônes et les échantillons restent parfaitement lisibles.\n" +
                    "\n" +
                    "**Fonctionne avec les deux styles de panneau.** Le réglage d'opacité de l'interface du jeu n'affecte plus ce panneau."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Afficher les infobulles (recommandé)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Laisser activé> est recommandé pour la plupart des joueurs.\n" +
                    "Affiche une aide courte au survol des boutons Hover Colors.\n" +
                    "Si désactivé, cliquez sur Info (i) dans la barre de titre ou réactivez cette case.\n" +
                    "Pour éviter les erreurs, les infobulles ne peuvent être désactivées que dans ce menu Options."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Réinitialiser les valeurs du mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Remet tous les réglages Hover Colors comme après une nouvelle installation : couleurs, épaisseur, outils, guides, panneau et infobulles.\n" +
                    "\n" +
                    "**Cela efface aussi vos presets sauvegardés (Set A et Set B).**\n" +
                    "\n" +
                    "Les raccourcis clavier ne changent pas.\n" +
                    "Équivaut à une première installation de Hover Colors."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Réinitialiser tous les réglages Hover Colors comme après une nouvelle installation ?\n\nVos presets sauvegardés (Set A et Set B) seront effacés." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Réinitialiser les couleurs vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Rend au jeu son apparence d’origine : contour, surbrillance propriétaire, remplissage, épaisseur, guides et districts.\n" +
                    "\n" +
                    "Tout le reste reste comme réglé : Bulldozer/routes, aperçus, presets, panneau et raccourcis.\n" +
                    "\n" +
                    "Note : le mod peut être supprimé sans reset. Les surbrillances reviennent seules aux valeurs du jeu.\n"+
                    "Ce bouton remet simplement rapidement les couleurs du jeu."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Réinitialiser les couleurs contrôlées par le mod à l’apparence du jeu ?\n\nPresets, outils et options du panneau sont conservés." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ouvrir/fermer le panneau principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Raccourci pour <ouvrir / fermer> le panneau Couleurs en ville." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Afficher/masquer le panneau Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Œil rapide On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Raccourci optionnel du bouton Œil : active/désactive instantanément Surbrillance + Remplissage.\n" +
                    "Non attribué par défaut pour éviter les conflits de touches." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Œil rapide On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Aperçus Surface On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Raccourci pour <masquer ou afficher> les lignes de limite Surface pendant le placement." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Calque aperçu Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Basculer presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Raccourci pour passer entre\n" +
                    "<preset 1 et preset 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Passer entre presets 1 et 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods de Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Ouvrir la page Paradox Mods de l'auteur.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "En mémoire de Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Ce mod est dédié à Mochi.\n" +
                    "Elle était une chienne adorée, adoptée à 7 ans,\n" +
                    "et a donné 13 ans d'amour et de joie.\n" +
                    "Ce mod n'existerait pas sans Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
