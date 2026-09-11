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
                    "ブルドーザーや道路ツール使用中の一時的なアウトライン色を制御します。\n" +
                    "\n" +
                    "**1. 推奨** 解体はゲームの警告色（黄）、道路はやわらかいバニラ青を使います。\n" +
                    "**2. バニラのツール色** ブルドーザー/道路ツール中は通常のバニラ青に戻します。\n" +
                    "**3. カスタム色を維持** 選んだ色をすべてで使います。\n" +
                    "\n" +
                    "目的: 解体中にカスタム色が見づらいユーザー/テスター向けです。\n" +
                    "ツール使用中に見やすい色を選べます。\n" +
                    "カラーピッカーに自動保存された色は上書きしません。"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推奨" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. バニラのツール色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. カスタム色を維持" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ 重なりオブジェクトのアウトラインを有効化" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<有効推奨>\n" +
                    "配置が重なりでブロックされた時、ゲーム標準のサーモン赤アウトラインを表示します。\n" +
                    "特殊産業の農場半径ガイドなど、エリア制限は変更しません。\n" +
                    "\n" +
                    "すべてのブルドーザー + 道路モードで動作し、保存色は上書きしません。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ NetLanesでカスタム色を許可" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<有効推奨>\n" +
                    "フェンス、生け垣、マーキングなどのNetLane詳細配置で、保存したHC色/透明度を使います。\n" +
                    "\n" +
                    "- 通常の道路は、選んだブルドーザー + 道路設定に従います。\n" +
                    "- これらのツールでゲームのバニラ青を使いたい場合は無効にします。\n" +
                    "- 有効時は重なりエラー色が優先されます（バニラのエラー色 = サーモン赤）。"
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ パネル不透明度" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "街中パネルの背景の濃さを調整します。\n" +
                    "\n" +
                    "**100%** = 完全に不透明。\n" +
                    "**低い値** = 背景の街がより見えます。\n" +
                    "\n" +
                    "変わるのは背景だけです。文字、アイコン、色見本は常に見やすいままです。\n" +
                    "\n" +
                    "**標準パネルのみ。** 暗いパネルはゲーム標準のパネルを使うため、ゲームのUI不透明度設定に従います。"
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ 暗いパネル" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "有効 = <暗いパネル>: Legacy UI向け。Modern UIでも暗めが好みなら使えます。\n" +
                    "無効 = <標準パネル>: Hover Colors独自の半透明スタイル。\n" +
                    "- 明るくモダンな見た目。\n" +
                    "- 新しいModern UIを使う多くのプレイヤー向け。\n" +
                    "\n" +
                    "両方試して好みを選んでください。変わるのはこのMODパネルの背景だけです。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ ツールチップ表示（推奨）" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "ほとんどのプレイヤーは<ONのまま>がおすすめです。\n" +
                    "Hover Colorsのボタンにカーソルを合わせると短い説明を表示します。\n" +
                    "無効にした場合は、タイトルバーのInfo (i)かこのチェックで再度ONにできます。\n" +
                    "誤操作防止のため、ツールチップをOFFにできるのはこのオプション画面だけです。"
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "MOD初期設定に戻す" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Hover Colorsの全設定を初回インストール時に戻します: 色、アウトライン太さ、ツール色、ガイド、パネル、ツールチップ。\n" +
                    "\n" +
                    "**保存したプリセット（Set A / Set B）も消去されます。**\n" +
                    "\n" +
                    "キー設定は変更しません。\n" +
                    "Hover Colorsを初めて入れた状態と同じです。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Hover Colorsの全設定を初回状態に戻しますか？\n\n保存したプリセット（Set A / Set B）は消去されます。" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "色をバニラに戻す" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "ゲーム標準の見た目に戻します: アウトライン、所有者ハイライト、塗り、太さ、ガイド、地区。\n" +
                    "\n" +
                    "その他はそのままです: ブルドーザー/道路、ツールプレビュー、プリセット、パネル、キー設定。\n" +
                    "\n" +
                    "注意: リセットせずにMODを削除しても問題ありません。ハイライトは自動でゲーム標準に戻ります。\n"+
                    "これはゲーム標準色へすぐ戻すためのボタンです。"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "このMODが変更する色をゲーム標準に戻しますか？\n\nプリセット、ツール動作、パネル設定は保持されます。" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "メインパネルを開く/閉じる" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "街中のカラーパネルを<開く / 閉じる>ショートカット。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colorsパネルを切替" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "クイック目アイコン On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "タイトルバーの目アイコン用オプションキー: ハイライト + 塗りを即座にOff/On。\n" +
                    "キー競合を避けるため初期状態では未設定です。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "クイック目アイコン On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surfaceツールプレビュー On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Surface配置中の境界プレビュー線を<非表示 / 表示>にするショートカット。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surfaceプレビューレイヤー On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "プリセット1+2を切替" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "ショートカットで\n" +
                    "<プリセットスロット1と2>を切替。" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "プリセット1と2を切替" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "MOD" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "バージョン" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "MochiのParadox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**作者のParadox Modsページを開きます。**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Mochiへ、愛を込めて。"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "このMODはMochiに捧げます。\n" +
                    "7歳で迎えた、愛しいワンちゃんでした。\n" +
                    "13年間、愛と喜びをくれました。\n" +
                    "Mochiなしでは、このMODはありませんでした。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
