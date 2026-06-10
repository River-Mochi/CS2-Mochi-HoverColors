// File: Localization/LocalePT_BR.cs
// Purpose: Brazilian Portuguese (pt-BR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/pt-BR.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Actions" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Sobre" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Comportamento das cores das ferramentas" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Painel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Atalhos de teclado" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Guias" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "DedicaÃ§Ã£o" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + estradas" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Controla temporariamente as cores do contorno enquanto o bulldozer ou as ferramentas de estrada estÃ£o ativos.\n\n**1. Recomendado** usa a cor de Aviso do jogo (amarelo) para demoliÃ§Ã£o e um azul vanilla mais suave para estradas.\n**2. Cores vanilla das ferramentas** restaura o azul vanilla normal do jogo enquanto o bulldozer ou as ferramentas de estrada estÃ£o ativos.\n**3. Manter minha cor personalizada** usa a cor escolhida em todos os lugares.\n\nObjetivo: alguns usuÃ¡rios/testadores acham difÃ­cil ver a cor personalizada ao demolir.\nIsto oferece opÃ§Ãµes de cores de alta visibilidade durante o uso das ferramentas.\nIsto nÃ£o sobrescreve a cor personalizada salva automaticamente no seletor de cores." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Recomendado" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Cores vanilla das ferramentas" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Manter minha cor personalizada" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Ativar contorno de itens sobrepostos" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Ativado Ã© recomendado>\nMantÃ©m visÃ­vel o contorno vanilla vermelho-salmÃ£o do jogo quando o posicionamento de objetos ou redes Ã© bloqueado por itens sobrepostos.\nLimites de Ã¡rea, como guias de raio de fazendas da IndÃºstria Especializada, ficam intactos.\n\nFunciona com todos os modos Bulldozer + estradas e nÃ£o sobrescreve sua cor personalizada salva." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Permitir cores personalizadas para NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Ativado Ã© recomendado>\nUsa sua cor/transparÃªncia HC salva ao colocar itens de detalhe NetLane, como cercas, sebes, marcaÃ§Ãµes e ferramentas semelhantes baseadas em faixas.\n\n- Estradas normais ainda seguem a opÃ§Ã£o Bulldozer + estradas escolhida na lista.\n- Desative se quiser que essas ferramentas usem o azul vanilla do jogo.\n- A cor de erro de sobreposiÃ§Ã£o ainda vence quando ativada (cor de erro vanilla = vermelho-salmÃ£o)." },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Painel mais escuro" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Ativado usa <Painel escuro>: feito para jogadores da UI antiga; tambÃ©m pode ser usado na UI Moderna se vocÃª gostar de um painel mais escuro.\nDesativado usa <Painel padrÃ£o>: estilo translÃºcido personalizado do Hover Colors.\n- Visual mais claro e moderno.\n- Melhor para a maioria dos jogadores usando a nova UI Moderna do jogo.\n\nTeste os dois e veja qual prefere! Isto muda apenas o fundo deste painel do mod, nÃ£o a UI do jogo." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Opacidade das guias (alfa)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Controla a opacidade das guias de alinhamento tracejadas, Ãºtil ao colocar estradas, cercas, props etc.\n\n**100%** mantÃ©m o visual vanilla padrÃ£o.\n**Mais baixo** deixa as guias mais transparentes.\n**0%** esconde tudo - <NÃ£o recomendado>.\nRecomenda-se ficar acima de 15%, ou fica difÃ­cil ver o que estÃ¡ acontecendo.\nO mesmo controle existe no painel do mod na cidade. Eles sÃ£o sincronizados;\nse vocÃª mudar este, o da cidade muda tambÃ©m." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Abrir/fechar painel principal" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Atalho de teclado para abrir / fechar o Painel de Cores dos objetos em hover na cidade." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Alternar painel Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Ativar/desativar prÃ©vias da ferramenta SuperfÃ­cie" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Atalho de teclado para ocultar ou restaurar as linhas de limite ativas da ferramenta SuperfÃ­cie enquanto coloca superfÃ­cies." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Camada de prÃ©via da SuperfÃ­cie On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Alternar presets 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Atalho de teclado para alternar entre o slot de preset 1 e o slot 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Alternar entre presets 1 e 2" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Abrir a pÃ¡gina do autor no Paradox Mods." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Em memÃ³ria amorosa de Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Este mod Ã© dedicado Ã  Mochi. Ela foi uma doguinha muito amada, adotada aos 7 anos,\ne deu 13 anos de amor e alegria. Este mod nÃ£o seria possÃ­vel sem a Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
