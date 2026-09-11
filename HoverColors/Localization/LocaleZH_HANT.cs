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

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleZH_HANT : IDictionarySource
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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "重設" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "快捷鍵" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "紀念" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ 推土機 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "控制推土機或道路工具啟用時的臨時輪廓顏色。\n" +
                    "\n" +
                    "**1. 推薦** 拆除用遊戲警告色（黃），道路用更柔和的原版藍。\n" +
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

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ 啟用重疊物件輪廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<建議啟用>\n" +
                    "當物件或網路放置被重疊阻止時，保持遊戲原版鮭紅輪廓可見。\n" +
                    "區域限制，例如專業工業農場半徑輔助線，不會被更動。\n" +
                    "\n" +
                    "適用於所有推土機 + 道路模式，不會覆蓋你儲存的自訂色。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ 允許 NetLanes 使用自訂色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<建議啟用>\n" +
                    "放置 NetLane 細節物，如圍欄、樹籬、標線等時，使用已儲存的 HC 顏色/透明度。\n" +
                    "\n" +
                    "- 一般道路仍遵循你在清單選的推土機 + 道路設定。\n" +
                    "- 如果想讓這些工具使用遊戲原版藍色輪廓，請關閉此項。\n" +
                    "- 啟用時，重疊錯誤顏色仍優先（原版錯誤色 = 鮭紅）。"
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ 更暗面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "啟用 = <暗色面板>：使用遊戲自己的面板顏色，與原版面板風格一致。\n" +
                    "- 100% 時完全不透明。\n" +
                    "停用 = <標準面板>：更明亮、半透明的 Hover Colors 風格。\n" +
                    "- 玻璃效果；即使在 100% 時仍能稍微看到後面的城市。\n" +
                    "\n" +
                    "兩種面板都使用下方的不透明度滑桿，並且在 Modern UI 和 Legacy UI 中顯示相同。\n" +
                    "\n" +
                    "兩種都試試，選擇你喜歡的！這只會改變本模組面板的背景，不影響遊戲 UI。"
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ 面板不透明度" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "調整城市內面板背景的實心程度。\n" +
                    "\n" +
                    "**30% 玻璃** 最透明、最清爽。\n" +
                    "**100%** 時暗色面板完全不透明，標準面板接近完全不透明。\n" +
                    "\n" +
                    "只改變背景。文字、圖示和色塊在任何設定下都保持清楚可讀。\n" +
                    "\n" +
                    "**兩種面板樣式都有效。** 遊戲的介面不透明度設定不再影響此面板。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ 顯示提示（推薦）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "建議多數玩家<保持開啟>。\n" +
                    "滑鼠停在 Hover Colors 按鈕上時顯示簡短說明。\n" +
                    "如果關閉，可點擊標題列 Info (i) 或重新勾選此項來開啟。\n" +
                    "為避免誤關，提示只能在此選項選單中關閉。"
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "重設為 Mod 預設值" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "將所有 Hover Colors 設定恢復到全新安裝狀態：顏色、輪廓粗細、工具顏色、輔助線、面板和提示。\n" +
                    "\n" +
                    "**這也會刪除你儲存的預設（Set A 和 Set B）。**\n" +
                    "\n" +
                    "快捷鍵不會改變。\n" +
                    "就像第一次安裝 Hover Colors。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "將所有 Hover Colors 設定重設為全新安裝狀態？\n\n你儲存的預設（Set A 和 Set B）將被刪除。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "重設為原版顏色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "恢復遊戲自己的外觀：輪廓、擁有者高亮、填充、輪廓粗細、輔助線和區域。\n" +
                    "\n" +
                    "其他內容保持不變：推土機/道路、工具預覽、預設、面板選項和快捷鍵。\n" +
                    "\n" +
                    "注意：無需重設即可移除此 mod。高亮會自動恢復遊戲預設值。\n"+
                    "這只是快速恢復遊戲預設顏色的按鈕。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "將此 mod 控制的顏色恢復為遊戲原版外觀？\n\n預設、工具行為和面板選項會保留。" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "開啟/關閉主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "用於<開啟 / 關閉>城市內顏色面板的快捷鍵。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切換 Hover Colors 面板" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "快速眼睛 開/關" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "標題列眼睛按鈕的可選快捷鍵：立即開/關 高亮 + 填充。\n" +
                    "預設未綁定，避免快捷鍵衝突。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "快速眼睛 開/關" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 工具預覽開/關" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "快捷鍵：放置地表時<隱藏或顯示>活動 Surface 邊界預覽線。" },
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochi 的 Paradox Mods" },
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
