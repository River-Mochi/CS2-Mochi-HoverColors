// <copyright file="LocaleZH_HANS.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "推土机 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "控制推土机或道路工具启用时的临时轮廓颜色。\n" +
                    "\n" +
                    "**1. 推荐** 拆除用游戏警告色(黄)，道路用更柔和的原版蓝。\n" +
                    "**2. 原版工具颜色** 在推土机或道路工具启用时恢复游戏正常原版蓝。\n" +
                    "**3. 保留我的自定义色** 到处使用你选的颜色。\n" +
                    "\n" +
                    "用途：一些用户/测试者在拆除时觉得自定义色不够清楚。\n" +
                    "这里提供工具使用时更醒目的颜色。\n" +
                    "不会覆盖颜色选择器中自动保存的自定义色。"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推荐" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 原版工具颜色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自定义色" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "启用重叠物体轮廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<建议启用>\n" +
                    "当物体或网络放置被重叠阻止时，保持游戏原版鲑红轮廓可见。\n" +
                    "区域限制，比如专业工业农场半径辅助线，不会被改动。\n" +
                    "\n" +
                    "适用于所有推土机 + 道路模式，不会覆盖你保存的自定义色。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "允许 NetLanes 使用自定义色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<建议启用>\n" +
                    "放置 NetLane 细节物，如围栏、树篱、标线等时，使用已保存的 HC 颜色/透明度。\n" +
                    "\n" +
                    "- 普通道路仍遵循你在下拉列表选择的推土机 + 道路设置。\n" +
                    "- 如果想让这些工具使用游戏原版蓝色轮廓，请关闭此项。\n" +
                    "- 启用时，重叠错误颜色仍优先（原版错误色 = 鲑红）。"
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "悬停颜色提示" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<启用> = 显示悬停颜色帮助提示（推荐 [x]）。\n" +
                    "<禁用> = 隐藏此 mod 的提示。\n" +
                    "提示只能在此选项菜单中关闭。\n" +
                    "但可在城市里重新开启：点击标题栏上的 Info (i) 按钮。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "更暗面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "启用 = <暗色面板>：为 LegacyUI 玩家制作；如果喜欢更高对比，Modern UI 也可用。\n" +
                    "禁用 = <标准面板>：悬停颜色的自定义半透明风格。\n" +
                    "- 更亮、更现代的外观。\n" +
                    "- 最适合使用新版 Modern UI 的大多数玩家。\n" +
                    "\n" +
                    "两个都试试。这里只改变此 mod 面板背景，不改变游戏 UI。"
                },


                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "打开/关闭主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                     "用于<打开 / 关闭>城市内颜色面板的快捷键。" },
     
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切换悬停颜色面板" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 工具预览开/关" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "快捷键：放置地表时<隐藏或显示>活动 Surface 工具边界预览线。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 工具预览层开/关" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切换预设 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "快捷键用于切换\n" +
                    "<预设槽 1 和槽 2>。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "在预设 1 和 2 间切换" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "版本" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**打开作者的 Paradox Mods 页面。**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "纪念 Mochi。"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "此 mod 献给 Mochi。\n" +
                    "她是一只被深爱的小狗，7岁时被收养，\n" +
                    "带来了13年的爱与快乐。\n" +
                    "没有 Mochi，就不会有这个 mod。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
