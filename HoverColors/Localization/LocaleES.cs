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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + carreteras" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Controla colores temporales de contorno con bulldozer o herramientas de carretera activas.\n" +
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

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Activar contorno de objetos superpuestos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Activado recomendado>\n" +
                    "Mantiene visible el contorno rojo salmón vanilla cuando una colocación está bloqueada por solapes.\n" +
                    "Los límites de área, como radios de granjas de Industria especializada, no se cambian.\n" +
                    "\n" +
                    "Funciona con todos los modos Bulldozer + carreteras y no sobrescribe tu color guardado."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Permitir colores propios para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Activado recomendado>\n" +
                    "Usa tu color/transparencia HC guardado al colocar detalles NetLane como vallas, setos, marcas y similares.\n" +
                    "\n" +
                    "- Las carreteras normales siguen el ajuste Bulldozer + carreteras elegido en la lista.\n" +
                    "- Desactívalo si quieres que esas herramientas usen el azul vanilla del juego.\n" +
                    "- El color de error por solape sigue ganando si está activo (color vanilla = rojo salmón)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Ayudas para colores al pasar" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Activado> = mostrar ayudas de colores al pasar (recomendado [x]).\n" +
                    "<Desactivado> = ocultar ayudas de este mod.\n" +
                    "Las ayudas solo se desactivan en este menú de Opciones.\n" +
                    "Pero puedes volver a activarlas en la ciudad: pulsa el botón Info (i) de la barra de título."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Panel más oscuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Activado = <Panel oscuro>: para jugadores de LegacyUI; también vale en Modern UI si quieres más contraste.\n" +
                    "Desactivado = <Panel estándar>: estilo translúcido propio de colores al pasar.\n" +
                    "- Aspecto más claro y moderno.\n" +
                    "- Mejor para la mayoría con la nueva UI moderna.\n" +
                    "\n" +
                    "Prueba ambos y elige. Solo cambia el fondo de este panel del mod, no la interfaz del juego."
                },

                // Reset button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)), "Restablecer los visuales del juego" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)),
                    "Devuelve al aspecto original todo lo que controla este mod: contorno, resaltado del propietario, relleno, grosor del contorno, guías, distritos y áreas de vista previa de herramientas.\n" +
                    "\n" +
                    "Tus ajustes preestablecidos, atajos y preferencias del panel se conservan.\n" +
                    "\n" +
                    "No hace falta para desinstalar el mod. Sirve para empezar de cero tras experimentar."
                },

                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)),
                    "¿Devolver al aspecto original todo lo que controla este mod?\n\nSe conservan los ajustes preestablecidos, los atajos y la configuración del panel." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/cerrar panel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Atajo para <abrir / cerrar> el panel de color en la ciudad." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar panel de colores al pasar" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Vista previa de herramienta Surface On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Atajo para <ocultar o mostrar> líneas de límite activas de Surface al colocar superficies." },
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
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
