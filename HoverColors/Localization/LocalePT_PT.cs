// <copyright file="LocalePT_PT.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Repor" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atalhos" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedicação" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + estradas" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Controla cores temporárias de contorno enquanto o bulldozer ou ferramentas de estrada estão ativos.\n" +
                    "\n" +
                    "**1. Recomendado** usa a cor de Aviso do jogo (amarelo) para demolição e um azul vanilla mais suave para estradas.\n" +
                    "**2. Cores vanilla** repõe o azul vanilla normal do jogo com bulldozer ou estradas ativos.\n" +
                    "**3. Manter a minha cor** usa a cor escolhida em todo o lado.\n" +
                    "\n" +
                    "Objetivo: alguns utilizadores/testers acham a cor personalizada difícil de ver ao demolir.\n" +
                    "Oferece cores bem visíveis durante o uso de ferramentas.\n" +
                    "Não substitui a cor guardada automaticamente no seletor."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter a minha cor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Ativar contorno de itens sobrepostos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Ativado recomendado>\n" +
                    "Mantém visível o contorno vermelho salmão vanilla quando a colocação é bloqueada por sobreposição.\n" +
                    "Limites de área, como raios de quinta da Indústria Especializada, ficam sem alterações.\n" +
                    "\n" +
                    "Funciona com todos os modos Bulldozer + estradas e não substitui a cor guardada."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Permitir cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Ativado recomendado>\n" +
                    "Usa a cor/transparência HC guardada ao colocar detalhes NetLane como vedações, sebes, marcações e semelhantes.\n" +
                    "\n" +
                    "- As estradas normais continuam a seguir a opção Bulldozer + estradas escolhida na lista.\n" +
                    "- Desativa se quiseres que essas ferramentas usem o azul vanilla do jogo.\n" +
                    "- A cor de erro por sobreposição continua a ter prioridade quando ativada (vanilla = vermelho salmão)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Painel mais escuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Ativado = <Painel escuro>: usa as cores de painel do próprio jogo para combinar com os painéis vanilla.\n" +
                    "- Fica totalmente sólido a 100%.\n" +
                    "Desativado = <Painel padrão>: estilo Hover Colors mais claro e translúcido.\n" +
                    "- Aspeto de vidro; mesmo a 100% ainda deixa ver um pouco da cidade.\n" +
                    "\n" +
                    "Os dois estilos usam o controlo de opacidade abaixo e têm o mesmo aspeto em Modern UI e Legacy UI.\n" +
                    "\n" +
                    "Experimenta os dois! Isto muda apenas o fundo deste painel do mod, não a interface do jogo."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Opacidade do painel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Controla o quão sólido é o fundo do painel na cidade.\n" +
                    "\n" +
                    "**30% Vidro** é o aspeto mais transparente e limpo.\n" +
                    "**100%** fica totalmente sólido no painel escuro e quase sólido no painel padrão.\n" +
                    "\n" +
                    "Só o fundo muda. Texto, ícones e amostras de cor continuam totalmente legíveis em qualquer definição.\n" +
                    "\n" +
                    "**Funciona nos dois estilos de painel.** A definição de Opacidade da Interface do jogo já não afeta este painel."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Mostrar dicas (recomendado)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Deixa isto ligado> é recomendado para a maioria dos jogadores.\n" +
                    "Mostra ajuda breve ao passar o rato nos botões do Hover Colors.\n" +
                    "Se desativares, clica em Info (i) na barra de título ou volta a marcar esta opção.\n" +
                    "Para evitar enganos, as dicas só podem ser desativadas neste menu Opções."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Repor padrões do mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Repõe todas as definições do Hover Colors como numa instalação nova: cores, espessura do contorno, ferramentas, guias, painel e dicas.\n" +
                    "\n" +
                    "**Isto também apaga os presets guardados (Set A e Set B).**\n" +
                    "\n" +
                    "Os atalhos não são alterados.\n" +
                    "É como instalar o Hover Colors pela primeira vez."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Repor todas as definições do Hover Colors como numa instalação nova?\n\nOs presets guardados (Set A e Set B) serão apagados." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Repor cores vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Devolve o aspeto original do jogo: contorno, destaque do proprietário, preenchimento, espessura, guias e distritos.\n" +
                    "\n" +
                    "Tudo o resto fica como definiste: Bulldozer/estradas, prévias, presets, painel e atalhos.\n" +
                    "\n" +
                    "Nota: o mod pode ser removido sem qualquer reset. Os destaques voltam sozinhos aos padrões do jogo.\n"+
                    "Este botão apenas faz um reset rápido para as cores padrão do jogo."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Repor as cores controladas pelo mod para o aspeto do jogo?\n\nPresets, comportamento das ferramentas e painel são mantidos." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/fechar painel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Atalho para <abrir / fechar> o painel de cores na cidade." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar painel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Olho rápido On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Atalho opcional para o botão Olho: liga/desliga imediatamente Destaque + Preenchimento.\n" +
                    "Vem sem tecla atribuída para evitar conflitos." },
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
