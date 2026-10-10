// <copyright file="LocalePT_PT.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Localization/LocalePT_PT.cs
// Purpose: European Portuguese (pt-PT) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/pt-PT.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocalePT_PT : IDictionarySource
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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Repor" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atalhos" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedicação" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + estradas" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Controla as cores temporárias do contorno com o bulldozer ou ferramentas de estrada.\n" +
                    "\n" +
                    "**1. Recomendado** usa o amarelo de aviso para demolição. Com uma ferramenta de estrada/caminho, suaviza o azul vanilla nas novas prévias e nos segmentos sob a ferramenta. Ao passar normalmente sobre uma estrada existente, continua a usar a tua cor de Contorno. O grande preenchimento de estrada nova segue a cor de prévia das Guias.\n" +
                    "**2. Cores vanilla** repõe o azul vanilla normal com bulldozer ou estradas.\n" +
                    "**3. Manter a minha cor** usa a tua cor em todo o lado.\n" +
                    "\n" +
                    "Útil se a tua cor for difícil de ver ao demolir.\n" +
                    "A guia separada da área da estrada também segue a cor de prévia das Guias.\n" +
                    "Não altera a cor guardada no seletor."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter a minha cor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Ativar contorno de itens sobrepostos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Ativado recomendado>\n" +
                    "Mantém o contorno vermelho salmão vanilla quando um objeto ou rede não pode ser colocado por sobreposição.\n" +
                    "Limites de área, como raios de quinta da Indústria Especializada, não mudam.\n" +
                    "\n" +
                    "Funciona com todos os modos Bulldozer + estradas sem substituir a cor guardada."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permitir cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Ativado recomendado>\n" +
                    "Usa a cor/transparência HC guardada em detalhes NetLane como vedações, sebes e marcações.\n" +
                    "\n" +
                    "- As estradas normais continuam a seguir Bulldozer + estradas.\n" +
                    "- Desativa para usar o azul vanilla do jogo nestas ferramentas.\n" +
                    "- A cor de erro por sobreposição continua a ter prioridade (vanilla = vermelho salmão)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Escolhe painel escuro ou de vidro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Escuro (Vanilla)** usa o painel do próprio jogo.\n" +
                    "- Adapta-se automaticamente à Legacy UI ou Modern UI.\n" +
                    "- Segue a Opacidade da Interface do jogo.\n" +
                    "\n" +
                    "**Vidro (Personalizado)** usa o painel de vidro mais claro do Hover Colors.\n" +
                    "- Mesmo a 100%, ainda deixa ver um pouco da cidade.\n" +
                    "- Adiciona um cursor de opacidade do painel.\n" +
                    "\n" +
                    "Experimenta os dois! Só muda o fundo deste painel, não a interface do jogo.\n" +
                    "\n" +
                    "Dica: o jogo desfoca o que fica atrás dos painéis. Transparência da Interface a 0% desliga o desfoque; 1% ou mais mantém."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Escuro (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Vidro (Personalizado)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Localização do botão do painel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Escolhe o canto superior esquerdo, superior direito ou Universal Mod Menu. O botão muda de posição assim que alteras esta opção."
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
                    "**100%** = quase sólido, ainda deixando ver um pouco da cidade.\n" +
                    "\n" +
                    "Só o fundo muda; texto, ícones e amostras continuam legíveis.\n" +
                    "\n" +
                    "**Escuro (Vanilla)** segue a Opacidade da Interface do jogo, por isso este cursor fica oculto."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostrar dicas (recomendado)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Deixa ligado> é recomendado.\n" +
                    "Mostra ajuda breve ao passar o rato nos botões do Hover Colors.\n" +
                    "Se desativares, clica em Info (i) na barra de título ou volta a marcar esta opção.\n" +
                    "As dicas só podem ser desativadas neste menu Opções."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Repor padrões do mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Repõe todas as definições do Hover Colors como numa instalação nova: cores, espessura do contorno, ferramentas, guias, painel e dicas.\n" +
                    "\n" +
                    "**Também apaga os presets guardados (Set A e Set B).**\n" +
                    "\n" +
                    "Os atalhos não mudam.\n" +
                    "É como instalar o Hover Colors pela primeira vez."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Repor todas as definições do Hover Colors?\n" +
                    "\n" +
                    "Os presets guardados (Set A e Set B) serão apagados."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Repor cores vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Devolve o aspeto original do jogo: contorno, destaque do proprietário, preenchimento, espessura, guias e distritos.\n" +
                    "\n" +
                    "Tudo o resto fica igual: Bulldozer/estradas, prévias, presets, painel e atalhos.\n" +
                    "\n" +
                    "Podes remover o mod sem reset; os destaques voltam sozinhos aos padrões do jogo.\n" +
                    "Este botão só faz um reset rápido das cores."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Repor as cores controladas pelo mod para o aspeto do jogo?\n" +
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
                    "Atalho opcional para o botão Olho: liga/desliga imediatamente Destaque + Preenchimento.\n" +
                    "Vem sem tecla atribuída para evitar conflitos."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Olho rápido On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Dicas de ângulo On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Atalho para as caixas de ângulo e Colocar/Anular.\n" +
                    "Funciona ao desenhar estradas ou caminhos novos.\n" +
                    "Custo, comprimento e inclinação continuam visíveis."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Ângulos e dicas do rato On/Off" },

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
                    "Ela foi uma cadelinha adorada, adotada aos 7 anos,\n" +
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
