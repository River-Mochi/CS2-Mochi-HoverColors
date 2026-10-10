// <copyright file="LocaleJA.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Localization/LocaleJA.cs
// Purpose: Japanese (ja-JP) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/ja-JP.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleJA : IDictionarySource
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

                // Tabs
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "操作" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "キー設定" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "情報" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "ツール色の動作" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "パネル" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "リセット" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "キー設定" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "献辞" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ ブルドーザー + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "ブルドーザーや道路ツール使用中の一時的なアウトライン色を調整します。\n" +
                    "\n" +
                    "**1. 推奨** 解体では警告色（黄）を使用。道路/歩道ツールでは、新しいプレビューとツール下の既存区間のバニラ青をやわらげます。通常の既存道路ホバーは、選んだアウトライン色のままです。新しい道路の広い塗りは「ガイド」のプレビュー色に従います。\n" +
                    "**2. バニラのツール色** ブルドーザー/道路ツール中は通常のバニラ青に戻します。\n" +
                    "**3. カスタム色を維持** 選んだ色をすべてで使います。\n" +
                    "\n" +
                    "解体中にカスタム色が見づらい場合に便利です。\n" +
                    "道路の接地範囲ガイドも「ガイド」のプレビュー色に従います。\n" +
                    "カラーピッカーに保存した色は上書きしません。"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推奨" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. バニラのツール色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. カスタム色を維持" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ 重なりオブジェクトのアウトラインを有効化" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<有効推奨>\n" +
                    "重なりで配置できない時、ゲーム標準のサーモン赤アウトラインを表示します。\n" +
                    "特殊産業の農場半径など、エリア制限は変更しません。\n" +
                    "\n" +
                    "すべてのブルドーザー + 道路モードで動作し、保存色は上書きしません。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ NetLanesでカスタム色を許可" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<有効推奨>\n" +
                    "フェンス、生け垣、マーキングなどのNetLane詳細で、保存したHC色/透明度を使います。\n" +
                    "\n" +
                    "- 通常の道路はブルドーザー + 道路設定に従います。\n" +
                    "- バニラ青を使いたい場合は無効にします。\n" +
                    "- 重なりエラー色が有効なら、そちらが優先されます（バニラ = サーモン赤）。"
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ ダークまたはガラスパネルを選択" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**ダーク (バニラ)** はゲーム標準パネルを使います。\n" +
                    "- Legacy UI / Modern UI に自動で合います。\n" +
                    "- ゲームのUI不透明度設定に従います。\n" +
                    "\n" +
                    "**ガラス (カスタム)** はHover Colorsの明るいガラス調パネルです。\n" +
                    "- 100%でも街が少し透けます。\n" +
                    "- パネル不透明度スライダーを追加します。\n" +
                    "\n" +
                    "両方試して好みの方をどうぞ。このModパネルの背景だけが変わります。\n" +
                    "\n" +
                    "ヒント: ゲームはパネルの背後をぼかします。UI透明度を0%にするとぼかしがOFF、1%以上ならONのままです。"
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "ダーク (バニラ)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "ガラス (カスタム)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ パネルボタンの位置" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "左上、右上、または Universal Mod Menu を選びます。変更するとボタンの位置がすぐに切り替わります。"
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "左上 + Universal" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "右上 + Universal" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ パネル不透明度" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "**ガラス (カスタム)** パネルの背景不透明度です。\n" +
                    "\n" +
                    "**30%** = 最も透明。\n" +
                    "**100%** = ほぼ不透明ですが、街が少し透けます。\n" +
                    "\n" +
                    "変わるのは背景だけ。文字、アイコン、色は常に読みやすいままです。\n" +
                    "\n" +
                    "**ダーク (バニラ)** はゲームのUI不透明度に従うため、このスライダーは非表示になります。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ ツールチップ表示（推奨）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "ほとんどのプレイヤーは<ONのまま>がおすすめです。\n" +
                    "Hover Colorsのボタンにカーソルを合わせると短い説明を表示します。\n" +
                    "OFFにした場合は、タイトルバーのInfo (i)かこの設定で再びONにできます。\n" +
                    "OFFにできるのはこのオプション画面だけです。"
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "MOD初期設定に戻す" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Hover Colorsの全設定を初回インストール状態に戻します：色、アウトライン太さ、ツール色、ガイド、パネル、ツールチップ。\n" +
                    "\n" +
                    "**保存したプリセット（Set A / Set B）も消去されます。**\n" +
                    "\n" +
                    "キー設定は変更しません。\n" +
                    "Hover Colorsを初めて入れた状態と同じです。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Hover Colorsの全設定を初回状態に戻しますか？\n" +
                    "\n" +
                    "保存したプリセット（Set A / Set B）は消去されます。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "色をバニラに戻す" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "ゲーム標準の見た目に戻します：アウトライン、所有者ハイライト、塗り、太さ、ガイド、地区。\n" +
                    "\n" +
                    "その他はそのまま：ブルドーザー/道路、ツールプレビュー、プリセット、パネル、キー設定。\n" +
                    "\n" +
                    "リセットせずにMODを削除しても、ハイライトは自動でゲーム標準に戻ります。\n" +
                    "これは色だけを素早く戻すボタンです。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "このMODが変更する色をゲーム標準に戻しますか？\n" +
                    "\n" +
                    "プリセット、ツール動作、パネル設定は保持されます。"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "メインパネルを開く/閉じる" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "街中のカラーパネルを<開く / 閉じる>ショートカット。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colorsパネルを切替" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "クイック目アイコン On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "目アイコン用の任意ショートカット：ハイライト + 塗りをすぐにOff/On。\n" +
                    "キー競合を避けるため初期状態では未設定です。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "クイック目アイコン On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "角度ツールチップ On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "角度ボックスと配置/元に戻すボックス用のショートカット。\n" +
                    "新しい道路や歩道を描画中に使えます。\n" +
                    "費用、長さ、勾配は表示されたままです。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "角度とマウスヒント On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "プリセット1+2を切替" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "ショートカットで\n" +
                    "<プリセット1と2>を切り替えます。"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "プリセット1と2を切替" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "MOD" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "バージョン" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochi の Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**作者のParadox Modsページを開きます。**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "愛するMochiを偲んで。"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "このModはMochiに捧げます。\n" +
                    "7歳で迎えた大切なわんこで、\n" +
                    "13年間たくさんの愛と喜びをくれました。\n" +
                    "Mochiがいなければ、このModもありませんでした。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
