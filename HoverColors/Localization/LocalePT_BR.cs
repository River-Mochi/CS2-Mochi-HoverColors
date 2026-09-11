// <copyright file="LocalePT_BR.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocalePT_BR.cs
// Purpose: Brazilian Portuguese (pt-BR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/pt-BR.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocalePT_BR : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Atalhos" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Sobre" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportamento das cores" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Painel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Redefinir" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atalhos" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedicação" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + estradas" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Controla cores temporárias de contorno enquanto bulldozer ou ferramentas de estrada estão ativos.\n" +
                    "\n" +
                    "**1. Recomendado** usa a cor de Aviso do jogo (amarelo) para demolição e um azul vanilla mais suave para estradas.\n" +
                    "**2. Cores vanilla** restaura o azul vanilla normal do jogo com bulldozer ou estradas ativos.\n" +
                    "**3. Manter minha cor** usa a cor escolhida em tudo.\n" +
                    "\n" +
                    "Objetivo: alguns usuários/testadores acham a cor personalizada difícil de ver ao demolir.\n" +
                    "Oferece cores de alta visibilidade durante o uso de ferramentas.\n" +
                    "Não substitui sua cor salva automaticamente no seletor."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter minha cor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Ativar contorno de itens sobrepostos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Ativado recomendado>\n" +
                    "Mantém visível o contorno vermelho salmão vanilla quando a colocação é bloqueada por sobreposição.\n" +
                    "Limites de área, como raios de fazenda da Indústria Especializada, ficam sem mudanças.\n" +
                    "\n" +
                    "Funciona com todos os modos Bulldozer + estradas e não substitui sua cor salva."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permitir cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Ativado recomendado>\n" +
                    "Usa sua cor/transparência HC salva ao colocar detalhes NetLane como cercas, arbustos, marcações e similares.\n" +
                    "\n" +
                    "- Estradas normais ainda seguem o ajuste Bulldozer + estradas escolhido na lista.\n" +
                    "- Desative se quiser que essas ferramentas usem o azul vanilla do jogo.\n" +
                    "- A cor de erro por sobreposição ainda tem prioridade quando ativada (vanilla = vermelho salmão)."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacidade do painel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Ajusta o quanto o fundo do painel na cidade é sólido.\n" +
                    "\n" +
                    "**100%** = totalmente sólido.\n" +
                    "**Menor** = deixa mais da cidade aparecer.\n" +
                    "\n" +
                    "Só o fundo muda. Texto, ícones e amostras continuam totalmente legíveis.\n" +
                    "\n" +
                    "**Somente painel padrão.** O painel escuro usa a superfície do próprio jogo e segue a Opacidade da Interface do jogo."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Painel mais escuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Ativado = <Painel escuro>: feito para Legacy UI; também serve na Modern UI se você preferir um painel mais escuro.\n" +
                    "Desativado = <Painel padrão>: estilo translúcido personalizado do Hover Colors.\n" +
                    "- Visual mais claro e moderno.\n" +
                    "- Melhor para a maioria dos jogadores usando a nova Modern UI.\n" +
                    "\n" +
                    "Teste os dois e escolha. Isso só muda o fundo deste painel do mod, não a UI do jogo."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostrar dicas (recomendado)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Deixe ligado> é recomendado para a maioria dos jogadores.\n" +
                    "Mostra ajuda curta ao passar o mouse nos botões do Hover Colors.\n" +
                    "Se desativar, clique em Info (i) na barra de título ou marque esta opção novamente.\n" +
                    "Para evitar enganos, as dicas só podem ser desativadas neste menu Opções."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Redefinir para padrões do mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Volta todas as configurações do Hover Colors para uma instalação nova: cores, espessura do contorno, ferramentas, guias, painel e dicas.\n" +
                    "\n" +
                    "**Isso também apaga seus presets salvos (Set A e Set B).**\n" +
                    "\n" +
                    "Os atalhos não são alterados.\n" +
                    "É como instalar o Hover Colors pela primeira vez."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Redefinir todas as configurações do Hover Colors como uma instalação nova?\n\nSeus presets salvos (Set A e Set B) serão apagados." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Redefinir cores para vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Devolve o visual original do jogo: contorno, destaque do proprietário, preenchimento, espessura, guias e distritos.\n" +
                    "\n" +
                    "Todo o resto fica como você configurou: Bulldozer/estradas, prévias, presets, painel e atalhos.\n" +
                    "\n" +
                    "Nota: o mod pode ser removido sem reset. Os destaques voltam sozinhos aos padrões do jogo.\n"+
                    "Este botão só faz um reset rápido para as cores padrão do jogo."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Redefinir as cores controladas pelo mod para o visual do jogo?\n\nPresets, comportamento das ferramentas e painel são mantidos." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/fechar painel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Atalho para <abrir / fechar> o painel de cores na cidade." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar painel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Olho rápido On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Atalho opcional para o botão Olho: liga/desliga na hora Destaque + Preenchimento.\n" +
                    "Vem sem tecla definida para evitar conflitos." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Olho rápido On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Prévia da ferramenta Surface On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Atalho para <ocultar ou mostrar> linhas de limite ativas da Surface ao colocar superfícies." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Camada de prévia Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Atalho para alternar entre\n" +
                    "<slot de preset 1 e slot 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Alternar entre presets 1 e 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versão" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods da Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Abrir a página do autor no Paradox Mods.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Em memória de Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Este mod é dedicado à Mochi.\n" +
                    "Ela foi uma cachorrinha amada, adotada aos 7 anos,\n" +
                    "e deu 13 anos de amor e alegria.\n" +
                    "Este mod não seria possível sem Mochi."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
