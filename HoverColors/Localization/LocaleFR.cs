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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportement des couleurs d'outil" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panneau" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Réinitialiser" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Raccourcis" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dédicace" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + routes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Contrôle les couleurs temporaires de contour quand le bulldozer ou les outils route sont actifs.\n" +
                    "\n" +
                    "**1. Recommandé** utilise la couleur d'avertissement du jeu (jaune) pour démolir et un bleu vanilla plus doux pour les routes.\n" +
                    "**2. Couleurs vanilla** restaure le bleu vanilla normal du jeu avec bulldozer ou routes.\n" +
                    "**3. Garder ma couleur** utilise votre couleur choisie partout.\n" +
                    "\n" +
                    "But: certains joueurs/testeurs voient mal leur couleur en bulldozer.\n" +
                    "Donne des couleurs très visibles pendant l'usage des outils.\n" +
                    "N'écrase pas la couleur auto-sauvegardée dans le sélecteur."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recommandé" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Couleurs vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Garder ma couleur" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Activer le contour des objets en chevauchement" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Activé recommandé>\n" +
                    "Garde visible le contour rouge saumon vanilla quand le placement d'objet ou réseau est bloqué.\n" +
                    "Les limites d'aire, comme les rayons de ferme Industrie spécialisée, restent inchangées.\n" +
                    "\n" +
                    "Fonctionne avec tous les modes Bulldozer + routes et n'écrase pas votre couleur sauvée."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Autoriser les couleurs perso pour NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Activé recommandé>\n" +
                    "Utilise votre couleur/transparence HC sauvée pour placer clôtures, haies, marquages et autres détails NetLane.\n" +
                    "\n" +
                    "- Les routes normales suivent toujours le réglage Bulldozer + routes choisi dans la liste.\n" +
                    "- Désactivez si vous voulez que ces outils utilisent plutôt le bleu vanilla du jeu.\n" +
                    "- La couleur d'erreur de chevauchement gagne toujours si activée (vanilla = rouge saumon)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Infobulles de couleurs au survol" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Activé> = afficher l'aide des couleurs au survol (recommandé [x]).\n" +
                    "<Désactivé> = masquer les infobulles de ce mod.\n" +
                    "Les infobulles ne se désactivent que dans ce menu Options.\n" +
                    "Vous pouvez les réactiver en ville: cliquez sur Info (i) dans la barre de titre."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Panneau plus sombre" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Activé = <Panneau sombre>: pour LegacyUI; aussi utile en Modern UI si vous voulez plus de contraste.\n" +
                    "Désactivé = <Panneau standard>: style translucide personnalisé pour les couleurs au survol.\n" +
                    "- Aspect plus clair et moderne.\n" +
                    "- Idéal pour la plupart des joueurs avec la nouvelle UI moderne.\n" +
                    "\n" +
                    "Essayez les deux! Cela ne change que le fond de ce panneau du mod, pas l'UI du jeu."
                },


                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ouvrir/fermer le panneau principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Raccourci pour <ouvrir / fermer> le panneau Couleurs en ville." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Basculer le panneau couleurs au survol" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Aperçus outil Surface On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Raccourci pour <masquer ou afficher> les lignes de limite Surface actives pendant le placement." },
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
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
