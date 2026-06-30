// <copyright file="LocalePT_BR.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocalePT_BR.cs
// Purpose: Portuguese Brazil (pt-BR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/pt-BR.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocalePT_BR : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocalePT_BR(HoverColorsSettings settings)
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Trator + Estradas" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Controla as cores temporárias do contorno enquanto as ferramentas de demolição ou estrada estão ativas.\n\n**1. Recomendado** usa a cor de aviso do jogo (amarelo) para demolição e um azul vanilla mais suave para estradas.\n**2. Cores vanilla das ferramentas** restaura o azul vanilla normal do jogo ao usar demolição ou estradas.\n**3. Manter minha cor personalizada** usa a cor escolhida em tudo.\n\nObjetivo: alguns usuários/testadores acham a cor personalizada difícil de ver ao demolir.\nEssas opções dão cores de alta visibilidade durante o uso das ferramentas.\nIsso não sobrescreve sua cor personalizada salva automaticamente no seletor." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla das ferramentas" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter minha cor personalizada" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Ativar contorno de itens sobrepostos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Ativado recomendado>\nMantém visível o contorno vermelho-salmão vanilla do jogo quando a colocação de objetos ou redes é bloqueada por itens sobrepostos.\nLimites de área, como guias de raio de fazendas da indústria especializada, não são alterados.\n\nFunciona com todos os modos Trator + Estradas e não sobrescreve sua cor salva." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Permitir cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Ativado recomendado>\nUsa sua cor/transparência salva de Cores ao passar o mouse ao colocar detalhes NetLane como cercas, sebes, marcações e ferramentas similares baseadas em faixas.\n\n- Estradas normais ainda seguem a opção Trator + Estradas escolhida na lista.\n- Desative isto se quiser que essas ferramentas usem o contorno azul vanilla do jogo.\n- A cor de erro por sobreposição ainda tem prioridade quando ativada (vanilla = vermelho-salmão)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Dicas de Cores ao passar o mouse" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Ativado> = mostra dicas de ajuda de Cores ao passar o mouse (recomendado [x]).\n<Desativado> = oculta as dicas deste mod.\nAs dicas só podem ser desativadas neste menu de Opções.\nMas você pode reativá-las na cidade: clique no botão Info (i) na barra de título." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Painel mais escuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ativado = <Painel escuro>: feito para jogadores da UI antiga; também pode ser usado na UI Moderna se você preferir um painel mais escuro.\nDesativado = <Painel padrão>: estilo translúcido personalizado de Cores ao passar o mouse.\n- Visual mais leve e moderno.\n- Melhor para a maioria dos jogadores usando a nova UI Moderna.\n\nTeste os dois e veja qual prefere. Isto muda apenas o fundo deste painel do mod, não a UI do jogo." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacidade das guias (alfa)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Controla a opacidade das guias de alinhamento tracejadas, útil ao colocar estradas, cercas, props etc.\n\n**100%** mantém o visual vanilla padrão.\n**Menor** deixa as guias mais transparentes.\n**0%** oculta tudo - <Não recomendado>.\nRecomendado ficar acima de 15% ou fica difícil ver o que acontece.\nO mesmo controle existe no painel da cidade. Os dois são sincronizados;\nse você mudar este, o da cidade também muda." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/fechar painel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Atalho para abrir/fechar o painel na cidade de cores dos objetos ao passar o mouse." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar painel Cores ao passar o mouse" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Alternar prévias da ferramenta Superfície" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Atalho para ocultar ou restaurar as linhas de limite da ferramenta Superfície ao colocar superfícies." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Camada de prévia da ferramenta Superfície On/Off" },
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Em memória amorosa de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Este mod é dedicado à Mochi. Ela era uma cachorrinha amada, adotada aos 7 anos,\ne deu 13 anos de amor e alegria. Este mod não seria possível sem Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
