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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Cores das ferramentas" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Painel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atalhos" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guias" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Dedicação" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + vias" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Controla as cores temporárias do contorno com o bulldozer ou ferramentas de vias ativas.\n\n**1. Recomendado**: amarelo de aviso para demolir e azul vanilla mais suave para vias.\n**2. Cores vanilla**: usa o azul normal do jogo com bulldozer/vias.\n**3. Manter minha cor**: usa sua cor em tudo.\n\nAjuda quando sua cor personalizada fica difícil de ver ao demolir. Sua cor salva não é substituída." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter minha cor" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Contorno de sobreposição" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Recomendado: ligado>\nMantém o contorno vermelho-salmão do jogo quando um objeto ou rede não pode ser colocado por sobreposição.\nLimites de área, como raio de fazendas da indústria especializada, ficam intactos.\n\nFunciona com todos os modos Bulldozer + vias e não substitui sua cor." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Recomendado: ligado>\nUsa sua cor/transparência HC salva em detalhes NetLane, como cercas, sebes, marcações e ferramentas parecidas.\n\n- Vias normais ainda seguem a opção Bulldozer + vias.\n- Desligue se quiser o azul vanilla do jogo.\n- A cor de erro por sobreposição continua tendo prioridade se estiver ligada (vermelho-salmão vanilla)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Dicas do mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Ligado> = mostra dicas de ajuda do mod (recomendado [x]).\n<Desligado> = oculta as dicas deste mod.\nSó dá para desligar neste menu de Opções.\nNa cidade, use o botão Info (i) na barra de título para ligar de novo." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Painel mais escuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ligado = <Painel escuro>: feito para UI antiga, ou se você preferir mais escuro.\nDesligado = <Painel padrão>: estilo translúcido do mod.\n- Visual mais leve e moderno.\n- Melhor para a nova UI Moderna.\n\nTeste os dois. Isso muda só o fundo do painel do mod, não a UI do jogo." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacidade das guias (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Controla a opacidade das guias pontilhadas de alinhamento ao colocar vias, cercas, props etc.\n\n**100%** padrão do jogo.\n**Menor** mais transparente.\n**0%** oculta tudo.\nFique acima de 15%, senão a linha fica difícil de ver.\nO mesmo slider está no painel da cidade; os dois ficam sincronizados." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/fechar painel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Atalho para <abrir / fechar> o painel de cores na cidade." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar painel" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Prévia Surface liga/desliga" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Atalho para <ocultar ou mostrar> as linhas de limite da ferramenta Surface ao colocar superfícies." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Camada de prévia Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Atalho para alternar entre\n<slot de preset 1 e slot 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Alternar entre presets 1 e 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Versão" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Abre a página do autor no Paradox Mods.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Em memória de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Este mod é dedicado à Mochi.\nEla foi uma cachorrinha muito amada, adotada aos 7 anos,\ne deu 13 anos de amor e alegria.\nEste mod não existiria sem Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
