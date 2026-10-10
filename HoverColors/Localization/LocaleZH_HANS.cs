// <copyright file="LocaleZH_HANS.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Localization/LocaleZH_HANS.cs
// Purpose: Simplified Chinese (zh-HANS) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/zh-HANS.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleZH_HANS : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleZH_HANS(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "操作" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "快捷键" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "关于" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "工具颜色行为" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "面板" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "重置" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "快捷键" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "纪念" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ 推土机 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "控制推土机或道路工具启用时的临时轮廓颜色。\n" +
                    "\n" +
                    "**1. 推荐** 拆除时使用黄色警告色。使用道路/步道工具时，会柔化新预览和工具下现有路段的原版蓝色。普通悬停已有道路仍使用你的“轮廓”颜色。新道路的大面积填充跟随“辅助线”的预览色。\n" +
                    "**2. 原版工具颜色** 使用推土机或道路工具时恢复正常原版蓝色。\n" +
                    "**3. 保留我的自定义色** 到处使用你选的颜色。\n" +
                    "\n" +
                    "如果拆除时自定义色不够清楚，这个模式会更好看清。\n" +
                    "单独的道路占地辅助线也跟随“辅助线”预览色。\n" +
                    "不会覆盖颜色选择器里已保存的自定义色。"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推荐" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 原版工具颜色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自定义色" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ 启用重叠物体轮廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<建议启用>\n" +
                    "物体或网络因重叠无法放置时，保留原版鲑红色错误轮廓。\n" +
                    "专业工业农场半径等区域限制不会改变。\n" +
                    "\n" +
                    "适用于所有推土机 + 道路模式，不会覆盖已保存的颜色。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ 允许 NetLanes 使用自定义色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<建议启用>\n" +
                    "放置围栏、树篱、标线等 NetLane 细节时，使用已保存的 HC 颜色/透明度。\n" +
                    "\n" +
                    "- 普通道路仍遵循推土机 + 道路设置。\n" +
                    "- 想让这些工具用原版蓝色时请关闭。\n" +
                    "- 启用重叠错误色时，错误色仍优先（原版 = 鲑红）。"
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ 选择深色或玻璃面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**暗色（原版）**使用游戏自己的面板。\n" +
                    "- 自动匹配 Legacy UI 或 Modern UI。\n" +
                    "- 跟随游戏的界面不透明度设置。\n" +
                    "\n" +
                    "**玻璃（自定义）**使用较亮的 Hover Colors 玻璃面板。\n" +
                    "- 即使 100% 仍能稍微看到城市。\n" +
                    "- 增加面板不透明度滑块。\n" +
                    "\n" +
                    "两种都试试，选喜欢的！只改变本模组面板背景，不影响游戏 UI。\n" +
                    "\n" +
                    "提示：游戏会模糊面板后方画面。界面透明度设为 0% 会关闭模糊；1% 或更高则保留。"
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "暗色（原版）" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "玻璃（自定义）" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ 面板按钮位置" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "选择左上角、右上角或 Universal Mod Menu。更改此设置后，按钮会立即移动到所选位置。"
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "左上角" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "右上角" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ 面板不透明度" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "**玻璃（自定义）**面板的背景不透明度。\n" +
                    "\n" +
                    "**30%** = 最透明。\n" +
                    "**100%** = 接近完全不透明，但仍能看到一点城市。\n" +
                    "\n" +
                    "只改变背景；文字、图标和色块一直清晰。\n" +
                    "\n" +
                    "**暗色（原版）**跟随游戏的界面不透明度，所以会隐藏此滑块。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ 显示提示（推荐）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "建议大多数玩家<保持开启>。\n" +
                    "鼠标停在 Hover Colors 按钮上时显示简短帮助。\n" +
                    "如果关闭，可点标题栏 Info (i) 或重新勾选此项开启。\n" +
                    "提示只能在此选项菜单中关闭。"
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "重置为 Mod 默认值" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "将所有 Hover Colors 设置恢复到全新安装状态：颜色、轮廓粗细、工具颜色、辅助线、面板和提示。\n" +
                    "\n" +
                    "**保存的预设（Set A 和 Set B）也会被删除。**\n" +
                    "\n" +
                    "快捷键不会改变。\n" +
                    "就像第一次安装 Hover Colors。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "重置所有 Hover Colors 设置？\n" +
                    "\n" +
                    "保存的预设（Set A 和 Set B）将被删除。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "重置为原版颜色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "恢复游戏自己的外观：轮廓、所有者高亮、填充、轮廓粗细、辅助线和区域。\n" +
                    "\n" +
                    "其他设置保持不变：推土机/道路、工具预览、预设、面板和快捷键。\n" +
                    "\n" +
                    "无需重置也可以移除 mod；高亮会自动恢复游戏默认值。\n" +
                    "这只是快速恢复颜色的按钮。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "将本 mod 控制的颜色恢复为游戏原版外观？\n" +
                    "\n" +
                    "预设、工具行为和面板选项会保留。"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "打开/关闭主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "用于<打开 / 关闭>城市内颜色面板的快捷键。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切换 Hover Colors 面板" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "快速眼睛 开/关" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "眼睛按钮的可选快捷键：立即开/关 高亮 + 填充。\n" +
                    "默认未绑定，避免快捷键冲突。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "快速眼睛 开/关" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "角度提示 开/关" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "角度框和放置/撤销框的快捷键。\n" +
                    "绘制新道路或路径时可用。\n" +
                    "费用、长度和坡度仍会显示。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "角度和鼠标提示 开/关" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切换预设 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "快捷键用于切换\n" +
                    "<预设 1 和预设 2>。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "在预设 1 和 2 间切换" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "版本" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochi 的 Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**打开作者的 Paradox Mods 页面。**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "深情怀念 Mochi。"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "此 mod 献给 Mochi。\n" +
                    "她是一只深爱的小狗，7 岁时被收养，\n" +
                    "带来了 13 年的爱与快乐。\n" +
                    "没有 Mochi，就不会有这个 mod。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
