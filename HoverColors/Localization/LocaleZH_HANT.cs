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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "工具顏色行為" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "面板" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "按鍵綁定" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "輔助線" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "獻詞" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "推土機 + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "控制推土機或道路工具啟用時的臨時輪廓顏色。\n\n**1. 建議** 拆除時使用遊戲警告色（黃色），道路使用較柔和的 vanilla 藍色。\n**2. vanilla 工具顏色** 在使用推土機或道路工具時恢復遊戲正常的 vanilla 藍色。\n**3. 保留我的自訂顏色** 在所有地方使用你選擇的顏色。\n\n用途：有些使用者/測試者在拆除時覺得自訂顏色不容易看清楚。\n這些選項在使用工具時提供更高可見度的顏色。\n不會覆蓋色彩選擇器中自動儲存的自訂顏色。" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 建議" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. vanilla 工具顏色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 保留我的自訂顏色" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "啟用重疊項目輪廓" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<建議啟用>\n當物件或網路放置因重疊項目而被阻擋時，保留遊戲 vanilla 的鮭紅色輪廓。\n區域限制，例如專業工業農場半徑輔助線，不會被變更。\n\n適用於所有 推土機 + 道路 模式，且不會覆蓋你儲存的自訂顏色。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "允許 NetLanes 使用自訂顏色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<建議啟用>\n放置圍欄、樹籬、標線等 NetLane 細節項目與類似車道工具時，使用已儲存的懸停顏色/透明度。\n\n- 一般道路仍會依照你在下拉清單中選擇的 推土機 + 道路 設定。\n- 如果希望這些工具改用遊戲 vanilla 藍色輪廓，請停用此項。\n- 重疊錯誤顏色啟用時仍會優先生效（vanilla 錯誤色 = 鮭紅色）。" },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "懸停顏色工具提示" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<啟用> = 顯示懸停顏色說明提示（建議 [x]）。\n<停用> = 隱藏此模組的提示。\n只能在此選項選單中停用提示。\n不過你可以在城市中重新啟用：點擊標題列上的 Info (i) 按鈕。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "較深色面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "啟用 = <深色面板>：為舊版 UI 玩家設計；如果你喜歡較深色面板，也可用於 Modern UI。\n停用 = <標準面板>：懸停顏色的自訂半透明樣式。\n- 較輕、更現代的外觀。\n- 最適合使用新 Modern UI 的大多數玩家。\n\n兩個都試試，看看你比較喜歡哪個！這只會改變此模組面板背景，不會改變遊戲 UI。" },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "輔助線不透明度（alpha）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "控制虛線對齊輔助線的不透明度，放置道路、圍欄、道具等時很有用。\n\n**100%** 保持 vanilla 預設外觀。\n**較低** 會讓輔助線更透明。\n**0%** 會完全隱藏 - <不建議>。\n建議保持在 15% 以上，否則很難看清發生了什麼。\n城市模組面板中也有同一個滑桿。兩者會同步；\n如果更改這裡，城市中的滑桿也會跟著更改。" },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "開啟/關閉主面板" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "用於開啟/關閉城市內懸停物件顏色面板的快捷鍵。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "切換懸停顏色面板" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "切換 Surface 工具預覽開/關" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "放置表面時，用快捷鍵隱藏或恢復 Surface 工具的邊界預覽線。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 工具預覽圖層 On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "切換預設 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "在預設槽 1 與槽 2 之間切換的快捷鍵。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "在預設 1 與 2 之間切換" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "模組" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "版本" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "開啟作者的 Paradox Mods 頁面。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "深切懷念 Mochi。" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "此模組獻給 Mochi。她是一隻深受喜愛的小狗，在 7 歲時被收養，\n帶來了 13 年的愛與快樂。沒有 Mochi，就不會有這個模組。" },
            };
        }

        public void Unload()
        {
        }
    }
}
