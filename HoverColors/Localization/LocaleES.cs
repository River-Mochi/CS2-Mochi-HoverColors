// <copyright file="LocaleES.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "Controla los colores temporales del contorno con el bulldozer o las herramientas de carretera.\n" +
                    "\n" +
                    "**1. Recomendado** usa el amarillo de aviso para demoler. Con una herramienta de carretera/camino, suaviza el azul vanilla en las nuevas vistas previas y los segmentos bajo la herramienta. Al pasar por una carretera existente normalmente, sigue usando tu color de Contorno. El relleno grande de una carretera nueva sigue el color de vista previa de Guías.\n" +
                    "**2. Colores vanilla** restaura el azul vanilla normal con bulldozer o carreteras.\n" +
                    "**3. Mantener mi color** usa tu color en todo.\n" +
                    "\n" +
                    "Útil si tu color cuesta ver al demoler.\n" +
                    "La guía separada de la huella de la carretera también sigue el color de vista previa de Guías.\n" +
                    "No cambia el color personalizado guardado en el selector."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Colores vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Mantener mi color" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Activar contorno de objetos superpuestos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Activado recomendado>\n" +
                    "Mantiene el contorno rojo salmón vanilla cuando no se puede colocar un objeto o red por solape.\n" +
                    "Los límites de área, como el radio de granjas de Industria especializada, no cambian.\n" +
                    "\n" +
                    "Funciona con todos los modos Bulldozer + carreteras y no sobrescribe tu color guardado."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permitir colores propios para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Activado recomendado>\n" +
                    "Usa tu color/transparencia HC guardado al colocar detalles NetLane: vallas, setos, marcas, etc.\n" +
                    "\n" +
                    "- Las carreteras normales siguen el ajuste Bulldozer + carreteras.\n" +
                    "- Desactívalo para usar el azul vanilla del juego en esas herramientas.\n" +
                    "- El color de error por solape sigue teniendo prioridad (vanilla = rojo salmón)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Elige panel oscuro o de cristal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Oscuro (Vanilla)** usa el panel del propio juego.\n" +
                    "- Se adapta a Legacy UI o Modern UI.\n" +
                    "- Sigue la Opacidad de interfaz del juego.\n" +
                    "\n" +
                    "**Cristal (Personalizado)** usa el panel de cristal más claro de Hover Colors.\n" +
                    "- Incluso al 100 % deja ver un poco la ciudad.\n" +
                    "- Añade un deslizador de opacidad del panel.\n" +
                    "\n" +
                    "¡Prueba ambos! Solo cambia el fondo de este panel, no la interfaz del juego.\n" +
                    "\n" +
                    "Consejo: el juego desenfoca lo que hay detrás de los paneles. Transparencia de interfaz al 0 % desactiva ese desenfoque; 1 % o más lo mantiene."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Oscuro (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Cristal (Personalizado)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Ubicación del botón del panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Elige arriba a la izquierda, arriba a la derecha o el Universal Mod Menu. El botón cambia de ubicación al cambiar esta opción."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "Arriba a la izquierda" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "Arriba a la derecha" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacidad del panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Opacidad del fondo del panel **Cristal (Personalizado)**.\n" +
                    "\n" +
                    "**30 %** = lo más transparente.\n" +
                    "**100 %** = casi sólido, aún dejando ver un poco la ciudad.\n" +
                    "\n" +
                    "Solo cambia el fondo; texto, iconos y muestras siguen legibles.\n" +
                    "\n" +
                    "**Oscuro (Vanilla)** usa la Opacidad de interfaz del juego, así que este deslizador se oculta."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostrar ayudas (recomendado)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Déjalo activado> es lo recomendado.\n" +
                    "Muestra ayuda breve al pasar por los botones de Hover Colors.\n" +
                    "Si lo desactivas, pulsa Info (i) en la barra de título o vuelve a marcar esta opción.\n" +
                    "Para evitar accidentes, solo se puede desactivar desde este menú de Opciones."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Restablecer valores del mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Devuelve todos los ajustes de Hover Colors a una instalación nueva: colores, grosor del contorno, herramientas, guías, panel y ayudas.\n" +
                    "\n" +
                    "**También borra tus presets guardados (Set A y Set B).**\n" +
                    "\n" +
                    "Los atajos no cambian.\n" +
                    "Es como instalar Hover Colors por primera vez."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "¿Restablecer todos los ajustes de Hover Colors?\n" +
                    "\n" +
                    "Se borrarán tus presets guardados (Set A y Set B)."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Restablecer colores vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Devuelve al juego su aspecto original: contorno, resaltado del propietario, relleno, grosor, guías y distritos.\n" +
                    "\n" +
                    "Todo lo demás queda igual: Bulldozer/carreteras, vistas previas, presets, panel y atajos.\n" +
                    "\n" +
                    "Puedes quitar el mod sin restablecer nada; los resaltados vuelven solos a los valores del juego.\n" +
                    "Este botón solo hace un reset rápido de colores."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "¿Restablecer los colores que controla el mod al aspecto del juego?\n" +
                    "\n" +
                    "Se conservan presets, herramientas y opciones del panel."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/cerrar panel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Atajo para <abrir / cerrar> el panel de color en la ciudad."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar panel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Ojo rápido On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Atajo opcional para el botón Ojo: activa/desactiva al instante Resaltado + Relleno.\n" +
                    "Viene sin tecla asignada para evitar conflictos."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Ojo rápido On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Ayudas de ángulo On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Atajo para los cuadros de ángulo y Colocar/Deshacer.\n" +
                    "Funciona al dibujar carreteras o caminos nuevos.\n" +
                    "Coste, longitud y pendiente siguen visibles."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Ángulos y avisos del ratón On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Atajo para cambiar entre\n" +
                    "<preset 1 y preset 2>."
                },
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
                    "Con cariño, en memoria de Mochi."
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
