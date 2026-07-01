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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "工具颜色" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "面板" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "快捷键" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "引导线" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "纪念" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "推土机 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "控制推土机或道路工具启用时的临时轮廓颜色。\n\n**1. 推荐**：拆除用游戏警告黄，道路用更柔和的原版蓝。\n**2. 原版工具颜色**：推土机/道路工具使用游戏默认蓝。\n**3. 保留我的自定义色**：所有地方都用你选的颜色。\n\n如果拆除时自定义色不够醒目，这个选项会更清楚。不会覆盖你保存的颜色。" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推荐" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 原版工具颜色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自定义色" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "重叠物品轮廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<推荐开启>\n当物品或网络因重叠无法放置时，保留游戏原版的鲑红色轮廓。\n区域限制线，例如特色工业农场半径，不会被改动。\n\n适用于所有“推土机 + 道路”模式，也不会覆盖你保存的颜色。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes 使用自定义色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<推荐开启>\n放置围栏、树篱、标线等 NetLane 细节工具时，使用保存的 HC 颜色/透明度。\n\n- 普通道路仍按“推土机 + 道路”设置。\n- 如果想用游戏原版蓝色，请关闭此项。\n- 重叠错误颜色开启时仍优先显示（原版鲑红色）。" },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "模组提示" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<开启> = 显示模组帮助提示（推荐 [x]）。\n<关闭> = 隐藏本模组提示。\n只能在这个选项菜单里关闭。\n在城市中可点击标题栏的 Info (i) 按钮重新开启。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "更暗面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "开启 = <暗色面板>：适合旧 UI 玩家；喜欢更暗也可在 Modern UI 使用。\n关闭 = <标准面板>：本模组的半透明风格。\n- 更轻、更现代。\n- 多数使用新 Modern UI 的玩家更适合。\n\n两个都试试。只改变本模组面板背景，不改变游戏 UI。" },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "引导线透明度 (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "控制虚线对齐引导线的透明度，适合放置道路、围栏、props 等。\n\n**100%** 游戏默认。\n**更低** 更透明。\n**0%** 全部隐藏。\n建议保持 15% 以上，否则线条很难看清。\n城市面板里也有同一个滑块，两个会同步。" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "打开/关闭主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "快捷键：<打开 / 关闭>城市中的颜色面板。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切换面板" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 预览开/关" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "快捷键：放置 Surface 时<隐藏或显示>边界预览线。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 预览层 On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切换预设 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "快捷键：在\n<预设槽 1 和槽 2>之间切换。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "在预设 1 和 2 间切换" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "模组" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "版本" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**打开作者的 Paradox Mods 页面。**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "献给 Mochi 的美好回忆。" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "本模组献给 Mochi。\n她是被深深爱着的小狗，7 岁时被收养，\n带来了 13 年的爱与快乐。\n没有 Mochi，就没有这个模组。" },
            };
        }

        public void Unload()
        {
        }
    }
}
