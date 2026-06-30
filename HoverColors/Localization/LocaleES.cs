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

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleES : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Acerca de" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportamiento de color de herramientas" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atajos de teclado" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guías" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedicatoria" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + Carreteras" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Controla los colores temporales del contorno mientras están activas las herramientas de bulldozer o carretera.\n\n**1. Recomendado** usa el color de advertencia del juego (amarillo) para demoler y un azul vanilla más suave para carreteras.\n**2. Colores vanilla de herramientas** restaura el azul vanilla normal del juego al usar bulldozer o carreteras.\n**3. Mantener mi color personalizado** usa tu color elegido en todas partes.\n\nObjetivo: algunos usuarios/probadores ven mal su color personalizado al demoler.\nEstas opciones ofrecen colores muy visibles durante el uso de herramientas.\nNo sobrescribe el color personalizado guardado automáticamente en el selector." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Colores vanilla de herramientas" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Mantener mi color personalizado" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Activar contorno de elementos superpuestos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Activado recomendado>\nMantiene visible el contorno rojo salmón vanilla del juego cuando la colocación de objetos o redes se bloquea por elementos superpuestos.\nLos límites de área, como las guías de radio de granjas de industria especializada, no se modifican.\n\nFunciona con todos los modos Bulldozer + Carreteras y no sobrescribe tu color guardado." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Permitir colores personalizados para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Activado recomendado>\nUsa tu color/transparencia guardados de Colores al pasar el cursor al colocar detalles NetLane como vallas, setos, marcas y herramientas similares basadas en carriles.\n\n- Las carreteras normales siguen usando la opción Bulldozer + Carreteras elegida en la lista.\n- Desactiva esto si quieres que esas herramientas usen el contorno azul vanilla del juego.\n- El color de error por superposición sigue teniendo prioridad si está activado (color vanilla = rojo salmón)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Tooltips de Colores al pasar el cursor" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Activado> = muestra tooltips de ayuda de Colores al pasar el cursor (recomendado [x]).\n<Desactivado> = oculta los tooltips de este mod.\nLos tooltips solo se pueden desactivar dentro de este menú de Opciones.\nPero puedes volver a activarlos en la ciudad: haz clic en el botón Info (i) de la barra de título." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Panel más oscuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Activado = <Panel oscuro>: hecho para jugadores con UI clásica; también sirve en UI moderna si prefieres un panel más oscuro.\nDesactivado = <Panel estándar>: estilo translúcido personalizado de Colores al pasar el cursor.\n- Aspecto más claro y moderno.\n- Mejor para la mayoría de jugadores con la nueva UI moderna.\n\nPrueba ambos y quédate con el que prefieras. Esto solo cambia el fondo del panel de este mod, no la UI del juego." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacidad de guías (alfa)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Controla la opacidad de las guías de alineación discontinuas, útil al colocar carreteras, vallas, props, etc.\n\n**100%** mantiene el aspecto vanilla predeterminado.\n**Menor** hace las guías más transparentes.\n**0%** las oculta por completo - <No recomendado>.\nSe recomienda mantenerse por encima de 15% o será difícil ver qué pasa.\nEl mismo deslizador está en el panel de la ciudad. Ambos están sincronizados;\nsi cambias este, también cambia el de la ciudad." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/cerrar panel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Atajo para abrir/cerrar el panel de color de objetos al pasar el cursor en la ciudad." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar panel de Colores al pasar el cursor" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Alternar vistas previas de herramienta Superficie" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Atajo para ocultar o restaurar las líneas de límite de la herramienta Superficie mientras colocas superficies." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Capa de vista previa de Superficie On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Atajo para cambiar entre el preset 1 y el preset 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Alternar entre presets 1 y 2" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versión" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Abre la página de Paradox Mods del autor." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "En memoria de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Este mod está dedicado a Mochi. Fue una perrita muy querida, adoptada a los 7 años,\ny dio 13 años de amor y alegría. Este mod no sería posible sin Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
