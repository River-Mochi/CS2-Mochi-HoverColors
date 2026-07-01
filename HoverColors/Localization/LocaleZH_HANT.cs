// <copyright file="LocaleZH_HANT.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleZH_HANT.cs
// Purpose: Traditional Chinese (zh-HANT) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/zh-HANT.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleZH_HANT : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleZH_HANT(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "關於" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "工具顏色" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "面板" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "快捷鍵" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "輔助線" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "紀念" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "推土機 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "控制推土機或道路工具啟用時的暫時輪廓顏色。\n\n**1. 推薦**：拆除用遊戲警告黃，道路用更柔和的原版藍。\n**2. 原版工具顏色**：推土機/道路工具使用遊戲預設藍。\n**3. 保留我的自訂色**：所有地方都用你選的顏色。\n\n如果拆除時自訂色不夠醒目，這個選項會更清楚。不會覆蓋你儲存的顏色。" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推薦" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 原版工具顏色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自訂色" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "重疊物品輪廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<建議開啟>\n當物件或網路因重疊無法放置時，保留遊戲原版的鮭紅色輪廓。\n區域限制線，例如特色工業農場半徑，不會被改動。\n\n適用於所有「推土機 + 道路」模式，也不會覆蓋你儲存的顏色。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes 使用自訂色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<建議開啟>\n放置圍欄、樹籬、標線等 NetLane 細節工具時，使用儲存的 HC 顏色/透明度。\n\n- 一般道路仍依照「推土機 + 道路」設定。\n- 如果想用遊戲原版藍色，請關閉此項。\n- 重疊錯誤顏色開啟時仍會優先顯示（原版鮭紅色）。" },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "模組提示" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<開啟> = 顯示模組說明提示（建議 [x]）。\n<關閉> = 隱藏本模組提示。\n只能在這個選項選單裡關閉。\n在城市中可點標題列的 Info (i) 按鈕重新開啟。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "更暗面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "開啟 = <暗色面板>：適合舊 UI 玩家；喜歡更暗也可在 Modern UI 使用。\n關閉 = <標準面板>：本模組的半透明風格。\n- 更輕、更現代。\n- 多數使用新 Modern UI 的玩家更適合。\n\n兩個都試試。只改變本模組面板背景，不改變遊戲 UI。" },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "輔助線透明度 (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "控制虛線對齊輔助線的透明度，適合放置道路、圍欄、props 等。\n\n**100%** 遊戲預設。\n**更低** 更透明。\n**0%** 全部隱藏。\n建議保持 15% 以上，否則線條很難看清。\n城市面板裡也有同一個滑桿，兩個會同步。" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "開啟/關閉主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "快捷鍵：<開啟 / 關閉>城市中的顏色面板。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切換面板" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 預覽開/關" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "快捷鍵：放置 Surface 時<隱藏或顯示>邊界預覽線。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 預覽層 On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切換預設 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "快捷鍵：在\n<預設槽 1 和槽 2>之間切換。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "在預設 1 和 2 間切換" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "模組" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "版本" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**開啟作者的 Paradox Mods 頁面。**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "獻給 Mochi 的美好回憶。" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "本模組獻給 Mochi。\n她是被深深愛著的小狗，7 歲時被收養，\n帶來了 13 年的愛與快樂。\n沒有 Mochi，就沒有這個模組。" },
            };
        }

        public void Unload()
        {
        }
    }
}
