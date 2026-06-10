// File: Localization/LocaleFR.cs
// Purpose: French (fr-FR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/fr-FR.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Ã€ propos" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportement des couleurs dâ€™outil" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panneau" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Raccourcis clavier" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "RepÃ¨res" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "DÃ©dicace" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + routes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ContrÃ´le les couleurs temporaires du contour lorsque le bulldozer ou les outils de route sont actifs.\n\n**1. RecommandÃ©** utilise la couleur dâ€™avertissement du jeu (jaune) pour la dÃ©molition et un bleu vanilla plus doux pour les routes.\n**2. Couleurs vanilla des outils** restaure le bleu vanilla normal du jeu lorsque le bulldozer ou les outils de route sont actifs.\n**3. Garder ma couleur personnalisÃ©e** utilise partout la couleur que vous avez choisie.\n\nObjectif : certains utilisateurs/testeurs trouvent leur couleur personnalisÃ©e difficile Ã  voir pendant la dÃ©molition.\nCes options offrent des couleurs trÃ¨s visibles pendant lâ€™utilisation des outils.\nCela nâ€™Ã©crase pas la couleur personnalisÃ©e enregistrÃ©e automatiquement dans le sÃ©lecteur de couleur." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. RecommandÃ©" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Couleurs vanilla des outils" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Garder ma couleur personnalisÃ©e" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Activer le contour des Ã©lÃ©ments qui se chevauchent" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<ActivÃ© est recommandÃ©>\nConserve le contour vanilla saumon rouge du jeu lorsque le placement dâ€™un objet ou dâ€™un rÃ©seau est bloquÃ© par des Ã©lÃ©ments qui se chevauchent.\nLes limites de zone, comme les guides de rayon des fermes dâ€™industrie spÃ©cialisÃ©e, ne sont pas modifiÃ©es.\n\nFonctionne avec tous les modes Bulldozer + routes et nâ€™Ã©crase pas votre couleur personnalisÃ©e enregistrÃ©e." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Autoriser les couleurs personnalisÃ©es pour NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<ActivÃ© est recommandÃ©>\nUtilise votre couleur/transparence HC enregistrÃ©e lors du placement dâ€™Ã©lÃ©ments de dÃ©tail NetLane comme les clÃ´tures, haies, marquages et autres outils similaires basÃ©s sur les voies.\n\n- Les routes normales suivent toujours le rÃ©glage Bulldozer + routes choisi dans la liste dÃ©roulante.\n- DÃ©sactivez cette option si vous voulez que ces outils utilisent le bleu vanilla du jeu Ã  la place.\n- La couleur dâ€™erreur de chevauchement reste prioritaire quand elle est activÃ©e (couleur dâ€™erreur vanilla = saumon rouge)." },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Panneau plus sombre" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "ActivÃ© utilise <Panneau sombre> : conÃ§u pour les joueurs avec lâ€™ancienne UI ; peut aussi Ãªtre utilisÃ© avec lâ€™UI moderne si vous prÃ©fÃ©rez un panneau plus sombre.\nDÃ©sactivÃ© utilise <Panneau standard> : style Hover Colors translucide personnalisÃ©.\n- Aspect plus clair et plus moderne.\n- IdÃ©al pour la plupart des joueurs utilisant la nouvelle UI moderne du jeu.\n\nEssayez les deux et gardez celui que vous prÃ©fÃ©rez ! Cela change seulement lâ€™arriÃ¨re-plan de ce panneau du mod, pas lâ€™UI du jeu." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "OpacitÃ© des repÃ¨res (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ContrÃ´le lâ€™opacitÃ© des guides dâ€™alignement en pointillÃ©s, utile pendant le placement de routes, clÃ´tures, props, etc.\n\n**100%** conserve lâ€™apparence vanilla par dÃ©faut.\n**Plus bas** rend les guides plus transparents.\n**0%** les masque complÃ¨tement - <Non recommandÃ©>.\nIl est conseillÃ© de rester au-dessus de 15%, sinon il devient difficile de voir ce qui se passe.\nLe mÃªme curseur existe dans le panneau du mod en ville. Les deux sont synchronisÃ©s ;\nsi vous changez celui-ci, celui en ville change aussi automatiquement." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ouvrir/fermer le panneau principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Raccourci clavier pour ouvrir / fermer le panneau de couleur des objets survolÃ©s en ville." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Afficher/masquer le panneau Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Activer/dÃ©sactiver les aperÃ§us de lâ€™outil Surface" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Raccourci clavier pour masquer ou rÃ©tablir les lignes de limite actives de lâ€™outil Surface pendant le placement des surfaces." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Couche dâ€™aperÃ§u Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Basculer les prÃ©rÃ©glages 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Raccourci clavier pour passer du slot de prÃ©rÃ©glage 1 au slot 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Basculer entre les prÃ©rÃ©glages 1 et 2" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Ouvrir la page Paradox Mods de lâ€™auteur." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "En tendre mÃ©moire de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Ce mod est dÃ©diÃ© Ã  Mochi. Câ€™Ã©tait une chienne adorÃ©e, adoptÃ©e Ã  lâ€™Ã¢ge de 7 ans,\net elle a donnÃ© 13 ans dâ€™amour et de joie. Ce mod nâ€™aurait pas Ã©tÃ© possible sans Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
