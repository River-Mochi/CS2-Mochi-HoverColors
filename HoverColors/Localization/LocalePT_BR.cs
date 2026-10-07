// <copyright file="LocalePT_BR.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "Controla as cores temporárias do contorno com bulldozer ou ferramentas de estrada.\n" +
                    "\n" +
                    "**1. Recomendado** usa o amarelo de aviso para demolição. Com ferramenta de estrada/caminho, suaviza o azul vanilla nas novas prévias e nos segmentos sob a ferramenta. Ao passar normalmente sobre uma estrada existente, continua usando sua cor de Contorno. O preenchimento grande de estrada nova segue a cor de prévia das Guias.\n" +
                    "**2. Cores vanilla** restaura o azul vanilla normal com bulldozer ou estradas.\n" +
                    "**3. Manter minha cor** usa sua cor em tudo.\n" +
                    "\n" +
                    "Útil se sua cor fica difícil de ver ao demolir.\n" +
                    "A guia separada da área da estrada também segue a cor de prévia das Guias.\n" +
                    "Não altera sua cor salva no seletor."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter minha cor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Ativar contorno de itens sobrepostos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Ativado recomendado>\n" +
                    "Mantém o contorno vermelho salmão vanilla quando um objeto ou rede não pode ser colocado por sobreposição.\n" +
                    "Limites de área, como raios de fazenda da Indústria Especializada, não mudam.\n" +
                    "\n" +
                    "Funciona com todos os modos Bulldozer + estradas sem substituir sua cor salva."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permitir cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Ativado recomendado>\n" +
                    "Usa sua cor/transparência HC salva em detalhes NetLane como cercas, arbustos e marcações.\n" +
                    "\n" +
                    "- Estradas normais continuam seguindo Bulldozer + estradas.\n" +
                    "- Desative para usar o azul vanilla do jogo nessas ferramentas.\n" +
                    "- A cor de erro por sobreposição continua tendo prioridade (vanilla = vermelho salmão)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Escolha painel escuro ou de vidro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Escuro (Vanilla)** usa o painel do próprio jogo.\n" +
                    "- Combina automaticamente com Legacy UI ou Modern UI.\n" +
                    "- Segue a Opacidade da Interface do jogo.\n" +
                    "\n" +
                    "**Vidro (Personalizado)** usa o painel de vidro mais claro do Hover Colors.\n" +
                    "- Mesmo em 100%, ainda deixa um pouco da cidade aparecer.\n" +
                    "- Adiciona um controle de opacidade do painel.\n" +
                    "\n" +
                    "Experimente os dois! Só muda o fundo deste painel, não a interface do jogo.\n" +
                    "\n" +
                    "Dica: o jogo desfoca o que fica atrás dos painéis. Transparência da Interface em 0% desliga o desfoque; 1% ou mais mantém."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Escuro (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Vidro (Personalizado)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Local do botão do painel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Escolha o canto superior esquerdo, superior direito ou Universal Mod Menu.\n" +
                    "**Reinicie o jogo** para aplicar a nova posição."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "Superior esquerdo" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "Superior direito" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacidade do painel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Opacidade do fundo do painel **Vidro (Personalizado)**.\n" +
                    "\n" +
                    "**30%** = mais transparente.\n" +
                    "**100%** = quase sólido, ainda mostrando um pouco da cidade.\n" +
                    "\n" +
                    "Só o fundo muda; texto, ícones e amostras continuam legíveis.\n" +
                    "\n" +
                    "**Escuro (Vanilla)** segue a Opacidade da Interface do jogo, então este controle fica oculto."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostrar dicas (recomendado)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Deixe ligado> é recomendado.\n" +
                    "Mostra ajuda curta ao passar o mouse nos botões do Hover Colors.\n" +
                    "Se desativar, clique em Info (i) na barra de título ou marque esta opção novamente.\n" +
                    "As dicas só podem ser desativadas neste menu Opções."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Redefinir para padrões do mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Volta todas as configurações do Hover Colors para uma instalação nova: cores, espessura do contorno, ferramentas, guias, painel e dicas.\n" +
                    "\n" +
                    "**Também apaga seus presets salvos (Set A e Set B).**\n" +
                    "\n" +
                    "Os atalhos não mudam.\n" +
                    "É como instalar o Hover Colors pela primeira vez."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Redefinir todas as configurações do Hover Colors?\n" +
                    "\n" +
                    "Seus presets salvos (Set A e Set B) serão apagados."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Redefinir cores para vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Devolve o visual original do jogo: contorno, destaque do proprietário, preenchimento, espessura, guias e distritos.\n" +
                    "\n" +
                    "Todo o resto fica igual: Bulldozer/estradas, prévias, presets, painel e atalhos.\n" +
                    "\n" +
                    "Você pode remover o mod sem reset; os destaques voltam sozinhos aos padrões do jogo.\n" +
                    "Este botão só faz um reset rápido das cores."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Redefinir as cores controladas pelo mod para o visual do jogo?\n" +
                    "\n" +
                    "Presets, ferramentas e opções do painel são mantidos."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/fechar painel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Atalho para <abrir / fechar> o painel de cores na cidade."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar painel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Olho rápido On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Atalho opcional para o botão Olho: liga/desliga na hora Destaque + Preenchimento.\n" +
                    "Vem sem tecla definida para evitar conflitos."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Olho rápido On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Dicas de ângulo On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Atalho para os quadros de ângulo e Colocar/Desfazer.\n" +
                    "Funciona ao desenhar estradas ou caminhos novos.\n" +
                    "Custo, comprimento e inclinação continuam visíveis."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Ângulos e dicas do mouse On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Atalho para alternar entre\n" +
                    "<preset 1 e preset 2>."
                },
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
                    "Em memória de Mochi, com amor."
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
