// <copyright file="LocaleES.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleES.cs
// Purpose: Spanish (es-ES) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/es-ES.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleES : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleES(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Acciones" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Atajos" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Acerca de" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Color de herramientas" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Restablecer" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atajos" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedicatoria" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + carreteras" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Controla colores temporales de contorno cuando están activos el bulldozer o las herramientas de carretera.\n" +
                    "\n" +
                    "**1. Recomendado** usa el color de aviso del juego (amarillo) para demoler y un azul vanilla más suave para carreteras.\n" +
                    "**2. Colores vanilla** restaura el azul vanilla normal del juego con bulldozer o carreteras.\n" +
                    "**3. Mantener mi color** usa tu color elegido en todo.\n" +
                    "\n" +
                    "Objetivo: algunos usuarios/testers ven mal su color al demoler.\n" +
                    "Ofrece colores muy visibles mientras usas herramientas.\n" +
                    "No sobrescribe tu color guardado automáticamente en el selector."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Colores vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Mantener mi color" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Activar contorno de objetos superpuestos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Activado recomendado>\n" +
                    "Mantiene visible el contorno rojo salmón vanilla cuando una colocación está bloqueada por solapes.\n" +
                    "Los límites de área, como los radios de granjas de Industria especializada, no se cambian.\n" +
                    "\n" +
                    "Funciona con todos los modos Bulldozer + carreteras y no sobrescribe tu color guardado."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permitir colores propios para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Activado recomendado>\n" +
                    "Usa tu color/transparencia HC guardado al colocar detalles NetLane como vallas, setos, marcas y similares.\n" +
                    "\n" +
                    "- Las carreteras normales siguen el ajuste Bulldozer + carreteras elegido en la lista.\n" +
                    "- Desactívalo si quieres que esas herramientas usen el azul vanilla del juego.\n" +
                    "- El color de error por solape sigue teniendo prioridad si está activo (vanilla = rojo salmón)."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacidad del panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Controla qué tan sólido es el fondo del panel en la ciudad.\n" +
                    "\n" +
                    "**Más bajo** deja ver más de la ciudad.\n" +
                    "**100%** totalmente sólido.\n" +
                    "\n" +
                    "Solo cambia el fondo. El texto, los iconos y las muestras de color siguen siendo totalmente legibles con cualquier ajuste.\n" +
                    "\n" +
                    "**El deslizador solo funciona con el panel estándar.** El panel oscuro usa la superficie de panel del juego y sigue la opción de opacidad de la interfaz del juego.\n" +
                    "El panel oscuro funciona bien con Legacy o Modern UI. El estándar funciona mejor con Modern UI."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Panel más oscuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Activado = <Panel oscuro>: pensado para Legacy UI; también sirve en Modern UI si prefieres un panel más oscuro.\n" +
                    "Desactivado = <Panel estándar>: estilo translúcido propio de Hover Colors.\n" +
                    "- Aspecto más claro y moderno.\n" +
                    "- Mejor para la mayoría con la nueva Modern UI.\n" +
                    "\n" +
                    "Prueba ambos y elige. Solo cambia el fondo de este panel del mod, no la interfaz del juego."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostrar ayudas (recomendado)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Déjalo activado> para la mayoría de jugadores.\n" +
                    "Muestra ayuda breve al pasar por los botones de Hover Colors.\n" +
                    "Si lo desactivas, pulsa Info (i) en la barra de título o vuelve a marcar esta opción.\n" +
                    "Para evitar accidentes, las ayudas solo se pueden desactivar en este menú de Opciones."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Restablecer valores del mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Devuelve todos los ajustes de Hover Colors a una instalación nueva: colores, grosor del contorno, herramientas, guías, panel y ayudas.\n" +
                    "\n" +
                    "**También borra tus presets guardados (Set A y Set B).**\n" +
                    "\n" +
                    "Los atajos no se modifican.\n" +
                    "Es como instalar Hover Colors por primera vez."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "¿Restablecer todos los ajustes de Hover Colors como una instalación nueva?\n\nSe borrarán tus presets guardados (Set A y Set B)." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Restablecer colores vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Devuelve al juego su aspecto original: contorno, resaltado del propietario, relleno, grosor, guías y distritos.\n" +
                    "\n" +
                    "Todo lo demás queda como lo configuraste: Bulldozer/carreteras, vistas previas, presets, panel y atajos.\n" +
                    "\n" +
                    "Nota: puedes quitar el mod sin restablecer nada. Los resaltados vuelven solos a los valores del juego.\n"+
                    "Este botón solo devuelve rápidamente los colores a los valores del juego."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "¿Restablecer los colores que controla el mod al aspecto del juego?\n\nSe conservan presets, herramientas y opciones del panel." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/cerrar panel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Atajo para <abrir / cerrar> el panel de color en la ciudad." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar panel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Ojo rápido On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Atajo opcional para el botón Ojo de la barra de título: activa/desactiva al instante Resaltado + Relleno.\n" +
                    "Viene sin tecla asignada para evitar conflictos." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Ojo rápido On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Vista previa de Surface On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Atajo para <ocultar o mostrar> las líneas de límite de Surface mientras colocas superficies." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Capa de vista previa Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Atajo para cambiar entre\n" +
                    "<preset 1 y preset 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Cambiar entre presets 1 y 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versión" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods de Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Abrir la página de Paradox Mods del autor.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "En memoria de Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Este mod está dedicado a Mochi.\n" +
                    "Fue una perrita muy querida, adoptada con 7 años,\n" +
                    "y dio 13 años de amor y alegría.\n" +
                    "Este mod no existiría sin Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
