// <copyright file="LocalePT_PT.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocalePT_PT.cs
// Purpose: Portuguese Portugal (pt-PT) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/pt-PT.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocalePT_PT : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocalePT_PT(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Ações" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Sobre" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportamento das cores das ferramentas" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Painel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atalhos de teclado" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guias" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedicação" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + Estradas" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Controla as cores temporárias do contorno enquanto as ferramentas de bulldozer ou estrada estão activas.\n\n**1. Recomendado** usa a cor de aviso do jogo (amarelo) para demolição e um azul vanilla mais suave para estradas.\n**2. Cores vanilla das ferramentas** restaura o azul vanilla normal do jogo ao usar bulldozer ou estradas.\n**3. Manter a minha cor personalizada** usa a cor escolhida em todo o lado.\n\nObjectivo: alguns utilizadores/testadores acham a cor personalizada difícil de ver ao demolir.\nEstas opções dão cores de alta visibilidade durante a utilização das ferramentas.\nIsto não substitui a cor personalizada guardada automaticamente no selector." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla das ferramentas" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter a minha cor personalizada" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Activar contorno de itens sobrepostos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Activado recomendado>\nMantém visível o contorno vermelho-salmão vanilla do jogo quando a colocação de objectos ou redes é bloqueada por itens sobrepostos.\nLimites de área, como guias de raio de quintas da indústria especializada, não são alterados.\n\nFunciona com todos os modos Bulldozer + Estradas e não substitui a tua cor guardada." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Permitir cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Activado recomendado>\nUsa a tua cor/transparência guardada de Cores ao passar o cursor ao colocar detalhes NetLane como vedações, sebes, marcações e ferramentas semelhantes baseadas em faixas.\n\n- Estradas normais continuam a seguir a opção Bulldozer + Estradas escolhida na lista.\n- Desactiva isto se quiseres que essas ferramentas usem o contorno azul vanilla do jogo.\n- A cor de erro por sobreposição continua a ter prioridade quando activada (vanilla = vermelho-salmão)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Dicas de Cores ao passar o cursor" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Activado> = mostra dicas de ajuda de Cores ao passar o cursor (recomendado [x]).\n<Desactivado> = oculta as dicas deste mod.\nAs dicas só podem ser desactivadas neste menu de Opções.\nMas podes voltar a activá-las na cidade: clica no botão Info (i) na barra de título." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Painel mais escuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Activado = <Painel escuro>: feito para jogadores da UI antiga; também pode ser usado na UI Moderna se preferires um painel mais escuro.\nDesactivado = <Painel padrão>: estilo translúcido personalizado de Cores ao passar o cursor.\n- Visual mais leve e moderno.\n- Melhor para a maioria dos jogadores com a nova UI Moderna.\n\nExperimenta ambos e escolhe o que preferires. Isto muda apenas o fundo deste painel do mod, não a UI do jogo." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacidade das guias (alfa)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Controla a opacidade das guias de alinhamento tracejadas, útil ao colocar estradas, vedações, adereços, etc.\n\n**100%** mantém o aspecto vanilla padrão.\n**Menor** torna as guias mais transparentes.\n**0%** oculta-as por completo - <Não recomendado>.\nRecomenda-se ficar acima de 15% ou torna-se difícil ver o que acontece.\nO mesmo controlo existe no painel da cidade. Ambos estão sincronizados;\nse alterares este, o da cidade também muda." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/fechar painel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Atalho para abrir/fechar o painel na cidade das cores dos objectos ao passar o cursor." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar painel Cores ao passar o cursor" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Alternar pré-visualizações da ferramenta Superfície" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Atalho para ocultar ou restaurar as linhas de limite da ferramenta Superfície ao colocar superfícies." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Camada de pré-visualização da ferramenta Superfície On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Atalho para alternar entre o preset 1 e o preset 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Alternar entre presets 1 e 2" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versão" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Abrir a página do autor no Paradox Mods." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Em memória carinhosa de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Este mod é dedicado à Mochi. Ela era uma cadelinha adorada, adoptada aos 7 anos,\ne deu 13 anos de amor e alegria. Este mod não seria possível sem a Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
