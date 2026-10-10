// <copyright file="LocaleFR.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "Contrôle les couleurs temporaires du contour avec le bulldozer ou les outils route.\n" +
                    "\n" +
                    "**1. Recommandé** utilise le jaune d’avertissement pour démolir. Avec un outil route/chemin, il adoucit le bleu vanilla sur les nouveaux aperçus et les segments sous l’outil. Le survol normal des routes existantes garde votre couleur de contour. Le grand remplissage des nouvelles routes suit la couleur d’aperçu des Guides.\n" +
                    "**2. Couleurs vanilla** restaure le bleu vanilla normal avec le bulldozer ou les outils route.\n" +
                    "**3. Garder ma couleur** utilise votre couleur partout.\n" +
                    "\n" +
                    "Utile si votre couleur est difficile à voir pendant la démolition.\n" +
                    "Le guide séparé de l’emprise de la route suit aussi la couleur d’aperçu des Guides.\n" +
                    "Votre couleur sauvegardée dans le sélecteur n’est jamais écrasée."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recommandé" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Couleurs vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Garder ma couleur" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Activer le contour des objets en chevauchement" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Activé recommandé>\n" +
                    "Garde le contour rouge saumon vanilla quand un objet ou réseau ne peut pas être placé à cause d’un chevauchement.\n" +
                    "Les limites de zone, comme les rayons des fermes spécialisées, ne changent pas.\n" +
                    "\n" +
                    "Fonctionne avec tous les modes Bulldozer + routes sans écraser votre couleur sauvegardée."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Autoriser les couleurs perso pour NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Activé recommandé>\n" +
                    "Utilise votre couleur/transparence HC pour les détails NetLane : clôtures, haies, marquages, etc.\n" +
                    "\n" +
                    "- Les routes normales suivent toujours le réglage Bulldozer + routes.\n" +
                    "- Désactivez pour utiliser le bleu vanilla du jeu sur ces outils.\n" +
                    "- La couleur d’erreur de chevauchement reste prioritaire (vanilla = rouge saumon)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Choisir panneau sombre ou vitré" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Sombre (Vanilla)** utilise le panneau du jeu.\n" +
                    "- S’adapte automatiquement à Legacy UI ou Modern UI.\n" +
                    "- Suit l’opacité de l’interface du jeu.\n" +
                    "\n" +
                    "**Verre (Personnalisé)** utilise le panneau vitré plus clair de Hover Colors.\n" +
                    "- Même à 100 %, un peu de la ville reste visible.\n" +
                    "- Ajoute un curseur d’opacité du panneau.\n" +
                    "\n" +
                    "Essayez les deux ! Seul le fond de ce panneau change, pas l’interface du jeu.\n" +
                    "\n" +
                    "Astuce : le jeu floute l’arrière-plan des panneaux. Une Transparence d’interface à 0 % coupe ce flou partout ; 1 % ou plus le garde."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Sombre (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Verre (Personnalisé)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Emplacement du bouton du panneau" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Choisissez en haut à gauche, en haut à droite ou dans l’Universal Mod Menu. Le bouton change de place dès que vous modifiez ce réglage."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "En haut à gauche + Universal" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "En haut à droite + Universal" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacité du panneau" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Opacité du fond du panneau **Verre (Personnalisé)**.\n" +
                    "\n" +
                    "**30 %** = le plus transparent.\n" +
                    "**100 %** = presque opaque, avec un peu de ville visible.\n" +
                    "\n" +
                    "Seul le fond change ; texte, icônes et couleurs restent lisibles.\n" +
                    "\n" +
                    "**Sombre (Vanilla)** suit l’opacité de l’interface du jeu, donc ce curseur est masqué."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Afficher les infobulles (recommandé)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Laisser activé> est recommandé.\n" +
                    "Affiche une aide courte au survol des boutons Hover Colors.\n" +
                    "Si vous les désactivez, cliquez sur Info (i) dans la barre de titre ou cochez à nouveau cette option.\n" +
                    "Pour éviter les erreurs, elles ne peuvent être désactivées que dans ce menu Options."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Réinitialiser les valeurs du mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Remet tous les réglages Hover Colors comme après une nouvelle installation : couleurs, épaisseur du contour, outils, guides, panneau et infobulles.\n" +
                    "\n" +
                    "**Efface aussi les presets sauvegardés (Set A et Set B).**\n" +
                    "\n" +
                    "Les raccourcis clavier ne changent pas.\n" +
                    "C’est comme installer Hover Colors pour la première fois."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Réinitialiser tous les réglages Hover Colors ?\n" +
                    "\n" +
                    "Les presets sauvegardés (Set A et Set B) seront effacés."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Réinitialiser les couleurs vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Rend au jeu son apparence d’origine : contour, surbrillance propriétaire, remplissage, épaisseur, guides et districts.\n" +
                    "\n" +
                    "Tout le reste reste comme réglé : Bulldozer/routes, aperçus, presets, panneau et raccourcis.\n" +
                    "\n" +
                    "Vous pouvez supprimer le mod sans reset : les surbrillances reviennent seules aux valeurs du jeu.\n" +
                    "Ce bouton sert juste de reset rapide des couleurs."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Remettre les couleurs contrôlées par le mod à l’apparence du jeu ?\n" +
                    "\n" +
                    "Presets, outils et options du panneau sont conservés."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ouvrir/fermer le panneau principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Raccourci pour <ouvrir / fermer> le panneau Couleurs en ville."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Afficher/masquer le panneau Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Œil rapide On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Raccourci optionnel du bouton Œil : active/désactive instantanément Surbrillance + Remplissage.\n" +
                    "Non attribué par défaut pour éviter les conflits."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Œil rapide On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Infobulles d’angle On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Raccourci pour les cases d’angle et Placer/Annuler.\n" +
                    "Fonctionne pendant le tracé de nouvelles routes ou chemins.\n" +
                    "Coût, longueur et pente restent visibles."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Angles et indications souris On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Basculer presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Raccourci pour passer entre\n" +
                    "<preset 1 et preset 2>."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Passer entre presets 1 et 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods de Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Ouvrir la page Paradox Mods de l’auteur.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "En tendre mémoire de Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Ce mod est dédié à Mochi.\n" +
                    "Adoptée à 7 ans, cette petite chienne adorée\n" +
                    "a offert 13 ans d’amour et de joie.\n" +
                    "Ce mod n’existerait pas sans Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
