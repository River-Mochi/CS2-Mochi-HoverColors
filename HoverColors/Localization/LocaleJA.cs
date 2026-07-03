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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "ツール色の動作" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "パネル" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "キー設定" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "ガイド" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "献辞" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ブルドーザー + 道路" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "ブルドーザーや道路ツール使用中の一時的なアウトライン色を制御します。\n" +
                    "\n" +
                    "**1. 推奨** 解体はゲームの警告色(黄)、道路はやわらかいバニラ青を使います。\n" +
                    "**2. バニラのツール色** ブルドーザー/道路ツール中は通常のバニラ青に戻します。\n" +
                    "**3. カスタム色を維持** 選んだ色をすべてで使います。\n" +
                    "\n" +
                    "目的: 解体中にカスタム色が見づらいユーザー/テスターがいます。\n" +
                    "ツール使用中に見やすい色を選べます。\n" +
                    "カラーピッカーに自動保存されたカスタム色は上書きしません。"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 推奨" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. バニラのツール色" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. カスタム色を維持" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "重なりオブジェクトのアウトラインを有効化" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<有効推奨>\n" +
                    "配置が重なりでブロックされた時、ゲーム標準のサーモン赤アウトラインを表示します。\n" +
                    "特殊産業の農場半径ガイドなど、エリア制限は変更しません。\n" +
                    "\n" +
                    "すべてのブルドーザー + 道路モードで動作し、保存色は上書きしません。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanesでカスタム色を許可" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<有効推奨>\n" +
                    "フェンス、生け垣、マーキングなどのNetLane詳細配置で、保存したHC色/透明度を使います。\n" +
                    "\n" +
                    "- 通常の道路は、ドロップダウンで選んだブルドーザー + 道路設定に従います。\n" +
                    "- それらのツールでゲームのバニラ青を使いたい場合は無効にします。\n" +
                    "- 有効時は重なりエラー色が優先されます (バニラのエラー色 = サーモン赤)。"
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "ホバー色のヘルプ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<有効> = ホバー色のヘルプツールチップを表示 (推奨 [x])。\n" +
                    "<無効> = このMODのツールチップを非表示。\n" +
                    "ツールチップはこのオプションメニューでのみ無効化できます。\n" +
                    "街ではタイトルバーのInfo (i)をクリックして再度ONにできます。"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "暗いパネル" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "有効 = <暗いパネル>: LegacyUI向け。Modern UIでも強いコントラストが欲しい時に使えます。\n" +
                    "無効 = <標準パネル>: ホバー色用の半透明カスタムスタイル。\n" +
                    "- 明るくモダンな見た目。\n" +
                    "- 新しいModern UIを使う多くのプレイヤー向け。\n" +
                    "\n" +
                    "両方試して好みを選んでください。変更されるのはこのMODパネルの背景だけです。"
                },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ガイドの不透明度 (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)),
                    "道路、フェンス、プロップ配置に便利な、破線の整列ガイドの不透明度を制御します。\n" +
                    "\n" +
                    "**100%** ゲーム標準。\n" +
                    "**低いほど** 透明。\n" +
                    "**0%** すべて非表示。\n" +
                    "15%以上がおすすめです。低すぎると線が見えません。\n" +
                    "同じスライダーが街のMODパネルにもあります。両方同期します。\n" +
                    "こちらを変えると、街のパネル側も変わります。"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "メインパネルを開く/閉じる" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "街中のカラーパネルを<開く / 閉じる>ショートカット。" },

                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "ホバー色パネルを切替" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surfaceツールのプレビュー On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "サーフェス配置中のアクティブなSurface境界プレビュー線を<表示/非表示>にします。" },
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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
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
