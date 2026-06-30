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

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleZH_HANS : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "关于" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "工具颜色行为" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "面板" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "按键绑定" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "辅助线" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "献词" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "推土机 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "控制推土机或道路工具启用时的临时轮廓颜色。\n\n**1. 推荐** 拆除时使用游戏警告色（黄色），道路使用更柔和的 vanilla 蓝色。\n**2. vanilla 工具颜色** 在使用推土机或道路工具时恢复游戏正常的 vanilla 蓝色。\n**3. 保留我的自定义颜色** 在所有地方使用你选择的颜色。\n\n用途：一些用户/测试者在拆除时觉得自己的自定义颜色不容易看清。\n这些选项在使用工具时提供更高可见度的颜色。\n不会覆盖颜色选择器中自动保存的自定义颜色。" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推荐" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. vanilla 工具颜色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自定义颜色" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "启用重叠项目轮廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<建议启用>\n当对象或网络放置因重叠项目被阻挡时，保留游戏 vanilla 的鲑红色轮廓。\n区域限制，例如专业工业农场半径辅助线，不会被更改。\n\n适用于所有 推土机 + 道路 模式，并且不会覆盖你保存的自定义颜色。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "允许 NetLanes 使用自定义颜色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<建议启用>\n放置围栏、绿篱、标线等 NetLane 细节项目和类似车道工具时，使用已保存的悬停颜色/透明度。\n\n- 普通道路仍会遵循你在下拉列表中选择的 推土机 + 道路 设置。\n- 如果希望这些工具改用游戏 vanilla 蓝色轮廓，请关闭此项。\n- 重叠错误颜色启用时仍会优先生效（vanilla 错误色 = 鲑红色）。" },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "悬停颜色工具提示" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<启用> = 显示悬停颜色帮助提示（建议 [x]）。\n<禁用> = 隐藏此模组的提示。\n只能在此选项菜单中禁用提示。\n不过你可以在城市中重新启用：点击标题栏上的 Info (i) 按钮。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "更深色面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "启用 = <深色面板>：为旧版 UI 玩家设计；如果你喜欢更深色的面板，也可用于 Modern UI。\n禁用 = <标准面板>：悬停颜色的自定义半透明样式。\n- 更轻、更现代的外观。\n- 最适合使用新 Modern UI 的大多数玩家。\n\n两个都试试，看看你更喜欢哪个！这只会改变此模组面板的背景，不会改变游戏 UI。" },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "辅助线不透明度（alpha）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "控制虚线对齐辅助线的不透明度，放置道路、围栏、道具等时很有用。\n\n**100%** 保持 vanilla 默认外观。\n**更低** 会让辅助线更透明。\n**0%** 会完全隐藏 - <不推荐>。\n建议保持在 15% 以上，否则很难看清发生了什么。\n城市模组面板中也有同一个滑块。两者会同步；\n如果更改这里，城市中的滑块也会跟着更改。" },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "打开/关闭主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "用于打开/关闭城市内悬停对象颜色面板的快捷键。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切换悬停颜色面板" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "切换 Surface 工具预览开/关" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "放置表面时，用快捷键隐藏或恢复 Surface 工具的边界预览线。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 工具预览图层 On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切换预设 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "在预设槽 1 和槽 2 之间切换的快捷键。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "在预设 1 和 2 之间切换" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "模组" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "版本" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "打开作者的 Paradox Mods 页面。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "深切怀念 Mochi。" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "此模组献给 Mochi。她是一只被深爱的小狗，在 7 岁时被收养，\n带来了 13 年的爱与快乐。没有 Mochi，就不会有这个模组。" },
            };
        }

        public void Unload()
        {
        }
    }
}
