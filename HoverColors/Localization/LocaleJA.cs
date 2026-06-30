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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "アクション" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "情報" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "ツール色の動作" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "パネル" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "キー割り当て" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "ガイドライン" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "献辞" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ブルドーザー + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ブルドーザーまたは道路ツールが有効な間の一時的なアウトライン色を制御します。\n\n**1. 推奨** は、解体時にゲームの警告色（黄色）、道路では少し柔らかい vanilla ブルーを使います。\n**2. vanilla ツール色** は、ブルドーザーまたは道路ツール中にゲーム通常の vanilla ブルーへ戻します。\n**3. カスタム色を維持** は、選んだ色を常に使います。\n\n目的: 一部のユーザー/テスターは、解体中にカスタム色が見えにくいと感じます。\nこの設定で、ツール使用中の視認性を高められます。\nカラーピッカーに自動保存されたカスタム色は上書きしません。" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推奨" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. vanilla ツール色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. カスタム色を維持" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "重なっている項目のアウトラインを有効化" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<有効を推奨>\nオブジェクトやネットワークの配置が重なりでブロックされたとき、ゲーム標準のサーモンレッドのアウトラインを表示したままにします。\n特殊産業の農場半径ガイドなどのエリア制限は変更しません。\n\nすべての ブルドーザー + 道路 モードで動作し、保存済みのカスタム色は上書きしません。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes でカスタム色を許可" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<有効を推奨>\nフェンス、生け垣、マーキングなどの NetLane 詳細アイテムや類似のレーン系ツールを配置するとき、保存済みのホバー色/透明度を使います。\n\n- 通常の道路は、ドロップダウンで選んだ ブルドーザー + 道路 設定に従います。\n- これらのツールにゲームの vanilla ブルーのアウトラインを使わせたい場合は無効にしてください。\n- 重なりエラー色が有効な場合はそちらが優先されます（vanilla エラー色 = サーモンレッド）。" },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "ホバー色のツールチップ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<有効> = ホバー色のヘルプツールチップを表示します（推奨 [x]）。\n<無効> = この Mod のツールチップを非表示にします。\nツールチップはこのオプションメニュー内でのみ無効化できます。\nただし、都市内ではタイトルバーの Info (i) ボタンをクリックして再度有効にできます。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "暗いパネル" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "有効 = <暗いパネル>: レガシー UI プレイヤー向け。暗いパネルが好みなら Modern UI でも使えます。\n無効 = <標準パネル>: ホバー色のカスタム半透明スタイル。\n- より軽く、モダンな見た目。\n- 新しい Modern UI を使うほとんどのプレイヤーに最適。\n\n両方試して好みを選んでください。変更されるのはこの Mod パネルの背景だけで、ゲーム UI は変わりません。" },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ガイドラインの不透明度（アルファ）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "道路、フェンス、プロップなどを配置するときに便利な、破線の位置合わせガイドの不透明度を制御します。\n\n**100%** は vanilla の既定表示を保ちます。\n**低く** するとガイドラインがより透明になります。\n**0%** は完全に非表示 - <非推奨>。\n15%以上を推奨します。低すぎると状況が見えにくくなります。\n同じスライダーは都市内の Mod パネルにもあります。両方は同期しています;\nこちらを変えると都市内のスライダーも変わります。" },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "メインパネルを開く/閉じる" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "都市内のホバー対象オブジェクト色パネルを開閉するホットキーです。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "ホバー色パネルの切り替え" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface ツールのプレビューをオン/オフ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "サーフェス配置中に、Surface ツールの境界プレビュー線を非表示/復元するホットキーです。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface ツールプレビューレイヤー On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "プリセット 1+2 の切り替え" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "プリセットスロット 1 と 2 を切り替えるホットキーです。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "プリセット 1 と 2 を切り替え" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "バージョン" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "作者の Paradox Mods ページを開きます。" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mochi への愛をこめて。" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "この Mod は Mochi に捧げます。7歳で迎えた大切なわんこで、\n13年間たくさんの愛と喜びをくれました。Mochi なしではこの Mod は生まれませんでした。" },
            };
        }

        public void Unload()
        {
        }
    }
}
