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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "快捷鍵" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "關於" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "工具顏色行為" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "面板" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "快捷鍵" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "紀念" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "推土機 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "控制推土機或道路工具啟用時的臨時輪廓顏色。\n" +
                    "\n" +
                    "**1. 推薦** 拆除用遊戲警告色(黃)，道路用更柔和的原版藍。\n" +
                    "**2. 原版工具顏色** 在推土機或道路工具啟用時恢復遊戲正常原版藍。\n" +
                    "**3. 保留我的自訂色** 到處使用你選的顏色。\n" +
                    "\n" +
                    "用途：一些使用者/測試者在拆除時覺得自訂色不夠清楚。\n" +
                    "這裡提供工具使用時更醒目的顏色。\n" +
                    "不會覆蓋顏色選擇器中自動儲存的自訂色。"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推薦" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 原版工具顏色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自訂色" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "啟用重疊物件輪廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<建議啟用>\n" +
                    "當物件或網路放置被重疊阻止時，保持遊戲原版鮭紅輪廓可見。\n" +
                    "區域限制，例如專業工業農場半徑輔助線，不會被更動。\n" +
                    "\n" +
                    "適用於所有推土機 + 道路模式，不會覆蓋你儲存的自訂色。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "允許 NetLanes 使用自訂色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<建議啟用>\n" +
                    "放置 NetLane 細節物，如圍欄、樹籬、標線等時，使用已儲存的 HC 顏色/透明度。\n" +
                    "\n" +
                    "- 一般道路仍遵循你在下拉清單選的推土機 + 道路設定。\n" +
                    "- 如果想讓這些工具使用遊戲原版藍色輪廓，請關閉此項。\n" +
                    "- 啟用時，重疊錯誤顏色仍優先（原版錯誤色 = 鮭紅）。"
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "懸停顏色提示" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<啟用> = 顯示懸停顏色說明提示（推薦 [x]）。\n" +
                    "<停用> = 隱藏此 mod 的提示。\n" +
                    "提示只能在此選項選單中關閉。\n" +
                    "但可在城市裡重新開啟：點擊標題列上的 Info (i) 按鈕。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "更暗面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "啟用 = <暗色面板>：為 LegacyUI 玩家製作；如果喜歡更高對比，Modern UI 也可用。\n" +
                    "停用 = <標準面板>：懸停顏色的自訂半透明風格。\n" +
                    "- 更亮、更現代的外觀。\n" +
                    "- 最適合使用新版 Modern UI 的多數玩家。\n" +
                    "\n" +
                    "兩個都試試。這裡只改變此 mod 面板背景，不改變遊戲 UI。"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "開啟/關閉主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "用於<開啟 / 關閉>城市內顏色面板的快捷鍵。" },

                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切換懸停顏色面板" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 工具預覽開/關" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "快捷鍵：放置地表時<隱藏或顯示>活動 Surface 工具邊界預覽線。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 工具預覽層開/關" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切換預設 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "快捷鍵用於切換\n" +
                    "<預設槽 1 和槽 2>。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "在預設 1 和 2 間切換" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "版本" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**開啟作者的 Paradox Mods 頁面。**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "紀念 Mochi。"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "此 mod 獻給 Mochi。\n" +
                    "她是一隻被深愛的小狗，7歲時被收養，\n" +
                    "帶來了13年的愛與快樂。\n" +
                    "沒有 Mochi，就不會有這個 mod。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
