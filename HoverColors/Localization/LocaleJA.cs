// <copyright file="LocaleJA.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleJA.cs
// Purpose: Japanese (ja-JP) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/ja-JP.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleJA : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleJA(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "情報" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "ツール色" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "パネル" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "キー設定" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "ガイド" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "献辞" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ブルドーザー + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ブルドーザーや道路ツール使用中の一時的なアウトライン色を設定します。\n\n**1. おすすめ**：解体は警告色の黄色、道路は少しやわらかいバニラ青。\n**2. バニラ色**：ブルドーザー/道路中はゲーム標準の青に戻します。\n**3. 自分の色を使う**：選んだ色を常に使います。\n\n解体中に自分の色が見づらい時に便利です。保存済みの色は上書きされません。" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. おすすめ" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. バニラ色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 自分の色を使う" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "重なり時のアウトライン" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<オン推奨>\nオブジェクトやネットワークが重なって置けない時、ゲーム標準のサーモン赤アウトラインを表示します。\n特化産業の農場範囲など、エリア制限ガイドは変更しません。\n\nBulldozer + Roads の全モードで動作し、保存した色は上書きしません。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes にカスタム色" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<オン推奨>\nフェンス、生け垣、マーキングなどの NetLane 系ツールで、保存した HC 色/透明度を使います。\n\n- 通常の道路は Bulldozer + Roads 設定に従います。\n- ゲーム標準の青を使いたい場合はオフにしてください。\n- 重なりエラー色がオンなら、そちらが優先されます（バニラのサーモン赤）。" },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Mod のツールチップ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<オン> = Mod のヘルプを表示（推奨 [x]）。\n<オフ> = この Mod のツールチップを非表示。\nオフにできるのはこのオプション画面だけです。\n街の画面では、タイトルバーの Info (i) でオンに戻せます。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "暗めのパネル" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "オン = <暗いパネル>：旧 UI 向け。暗い見た目が好きなら Modern UI でも使えます。\nオフ = <標準パネル>：この Mod の半透明スタイル。\n- 軽くてモダンな見た目。\n- 新しい Modern UI の多くのプレイヤーにおすすめ。\n\n両方試してみてください。変わるのは Mod パネルの背景だけで、ゲーム UI は変わりません。" },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ガイドの不透明度 (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "道路、フェンス、props などを置く時の点線ガイドの濃さを調整します。\n\n**100%** ゲーム標準。\n**低い値** より透明。\n**0%** すべて非表示。\n15% 未満は線が見づらいので注意。\n街の Mod パネルにも同じスライダーがあり、両方同期します。" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "メインパネルを開く/閉じる" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "街の色パネルを<開く / 閉じる>ショートカット。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "パネル表示切替" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface プレビュー切替" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 配置中の境界プレビュー線を<隠す/表示する>ショートカット。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface プレビュー On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "プリセット 1+2 切替" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "<プリセットスロット 1 と 2>を\n切り替えるショートカット。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "プリセット 1 と 2 を切替" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "バージョン" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**作者の Paradox Mods ページを開きます。**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mochi へ、愛をこめて。" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "この Mod は Mochi に捧げます。\n7歳で迎えた大切なわんこで、\n13年もの愛と喜びをくれました。\nMochi なしでは、この Mod はありませんでした。" },
            };
        }

        public void Unload()
        {
        }
    }
}
