// <copyright file="LocaleZH_HANT.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "控制推土機或道路工具啟用時的暫時輪廓顏色。\n" +
                    "\n" +
                    "**1. 推薦** 拆除時使用黃色警告色。使用道路/步道工具時，會柔化新預覽和工具下既有路段的原版藍色。一般滑過既有道路時仍使用你的「輪廓」顏色。新道路的大面積填充跟隨「輔助線」的預覽色。\n" +
                    "**2. 原版工具顏色** 使用推土機或道路工具時恢復正常原版藍色。\n" +
                    "**3. 保留我的自訂色** 到處使用你選的顏色。\n" +
                    "\n" +
                    "如果拆除時自訂色不夠清楚，這個模式會更容易看。\n" +
                    "獨立的道路占地輔助線也跟隨「輔助線」預覽色。\n" +
                    "不會覆蓋顏色選擇器裡已儲存的自訂色。"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推薦" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 原版工具顏色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自訂色" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ 啟用重疊物件輪廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<建議啟用>\n" +
                    "物件或網路因重疊無法放置時，保留原版鮭紅色錯誤輪廓。\n" +
                    "專業工業農場半徑等區域限制不會改變。\n" +
                    "\n" +
                    "適用於所有推土機 + 道路模式，不會覆蓋已儲存的顏色。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ 允許 NetLanes 使用自訂色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<建議啟用>\n" +
                    "放置圍欄、樹籬、標線等 NetLane 細節時，使用已儲存的 HC 顏色/透明度。\n" +
                    "\n" +
                    "- 一般道路仍遵循推土機 + 道路設定。\n" +
                    "- 想讓這些工具用原版藍色時請關閉。\n" +
                    "- 啟用重疊錯誤色時，錯誤色仍優先（原版 = 鮭紅）。"
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ 選擇深色或玻璃面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**暗色（原版）**使用遊戲自己的面板。\n" +
                    "- 自動配合 Legacy UI 或 Modern UI。\n" +
                    "- 跟隨遊戲的介面不透明度設定。\n" +
                    "\n" +
                    "**玻璃（自訂）**使用較亮的 Hover Colors 玻璃面板。\n" +
                    "- 即使 100% 仍能稍微看到城市。\n" +
                    "- 加入面板不透明度滑桿。\n" +
                    "\n" +
                    "兩種都試試，選喜歡的！只改變本模組面板背景，不影響遊戲 UI。\n" +
                    "\n" +
                    "提示：遊戲會模糊面板後方畫面。介面透明度設為 0% 會關閉模糊；1% 或更高則保留。"
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "暗色（原版）" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "玻璃（自訂）" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ 面板按鈕位置" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "選擇左上角、右上角或 Universal Mod Menu。\n" +
                    "**重新啟動遊戲**後按鈕位置才會改變。"
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "左上角" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "右上角" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ 面板不透明度" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "**玻璃（自訂）**面板的背景不透明度。\n" +
                    "\n" +
                    "**30%** = 最透明。\n" +
                    "**100%** = 接近完全不透明，但仍能看到一點城市。\n" +
                    "\n" +
                    "只改變背景；文字、圖示和色塊一直清楚。\n" +
                    "\n" +
                    "**暗色（原版）**跟隨遊戲的介面不透明度，所以會隱藏此滑桿。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ 顯示提示（推薦）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "建議多數玩家<保持開啟>。\n" +
                    "滑鼠停在 Hover Colors 按鈕上時顯示簡短說明。\n" +
                    "如果關閉，可點標題列 Info (i) 或重新勾選此項開啟。\n" +
                    "提示只能在此選項選單中關閉。"
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "重設為 Mod 預設值" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "將所有 Hover Colors 設定恢復到全新安裝狀態：顏色、輪廓粗細、工具顏色、輔助線、面板和提示。\n" +
                    "\n" +
                    "**儲存的預設（Set A 和 Set B）也會被刪除。**\n" +
                    "\n" +
                    "快捷鍵不會改變。\n" +
                    "就像第一次安裝 Hover Colors。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "重設所有 Hover Colors 設定？\n" +
                    "\n" +
                    "儲存的預設（Set A 和 Set B）將被刪除。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "重設為原版顏色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "恢復遊戲自己的外觀：輪廓、擁有者高亮、填充、輪廓粗細、輔助線和區域。\n" +
                    "\n" +
                    "其他設定保持不變：推土機/道路、工具預覽、預設、面板和快捷鍵。\n" +
                    "\n" +
                    "無需重設也可以移除 mod；高亮會自動恢復遊戲預設值。\n" +
                    "這只是快速恢復顏色的按鈕。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "將本 mod 控制的顏色恢復為遊戲原版外觀？\n" +
                    "\n" +
                    "預設、工具行為和面板選項會保留。"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "開啟/關閉主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "用於<開啟 / 關閉>城市內顏色面板的快捷鍵。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切換 Hover Colors 面板" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "快速眼睛 開/關" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "眼睛按鈕的可選快捷鍵：立即開/關 高亮 + 填充。\n" +
                    "預設未綁定，避免快捷鍵衝突。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "快速眼睛 開/關" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "角度提示 開/關" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "角度框和放置/復原框的快捷鍵。\n" +
                    "繪製新道路或路徑時可用。\n" +
                    "費用、長度和坡度仍會顯示。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "角度與滑鼠提示 開/關" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切換預設 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "快捷鍵用於切換\n" +
                    "<預設 1 和預設 2>。"
                },
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
                    "深情懷念 Mochi。"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "此 mod 獻給 Mochi。\n" +
                    "她是一隻深受疼愛的小狗，7 歲時被收養，\n" +
                    "帶來了 13 年的愛與快樂。\n" +
                    "沒有 Mochi，就不會有這個 mod。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
