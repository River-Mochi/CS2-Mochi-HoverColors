// <copyright file="LocaleKO.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleKO.cs
// Purpose: Korean (ko-KR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/ko-KR.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleKO : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleKO(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "동작" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "키 설정" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "정보" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "도구 색상 동작" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "패널" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "초기화" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "키 설정" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "헌정" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ 불도저 + 도로" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "불도저나 도로 도구가 활성일 때 임시 외곽선 색을 제어합니다.\n" +
                    "\n" +
                    "**1. 추천** 철거에는 게임 경고색(노랑), 도로에는 부드러운 바닐라 파랑을 씁니다.\n" +
                    "**2. 바닐라 도구 색** 불도저/도로 사용 중 게임 기본 파랑을 복원합니다.\n" +
                    "**3. 내 색 유지** 선택한 색을 모든 곳에 사용합니다.\n" +
                    "\n" +
                    "목적: 일부 사용자/테스터는 철거 중 사용자 색이 잘 안 보입니다.\n" +
                    "도구 사용 중 잘 보이는 색을 제공합니다.\n" +
                    "색상 선택기에 자동 저장된 사용자 색은 덮어쓰지 않습니다."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 추천" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 바닐라 도구 색" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 내 색 유지" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ 겹친 항목 외곽선 켜기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<켜기 추천>\n" +
                    "오브젝트/네트워크 배치가 겹침으로 막힐 때 게임 기본 연어색 외곽선을 표시합니다.\n" +
                    "특수 산업 농장 반경 가이드 같은 영역 제한은 그대로 둡니다.\n" +
                    "\n" +
                    "모든 불도저 + 도로 모드에서 작동하며 저장한 사용자 색을 덮어쓰지 않습니다."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ NetLanes에 사용자 색 허용" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<켜기 추천>\n" +
                    "울타리, 생울타리, 표시 등 NetLane 디테일 배치에 저장한 HC 색/투명도를 사용합니다.\n" +
                    "\n" +
                    "- 일반 도로는 선택한 불도저 + 도로 설정을 계속 따릅니다.\n" +
                    "- 해당 도구가 게임 기본 파랑을 쓰게 하려면 끄세요.\n" +
                    "- 켜져 있으면 겹침 오류색이 항상 우선합니다 (기본 오류색 = 연어색)."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ 패널 불투명도" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "도시 내 패널 배경의 불투명도를 조절합니다.\n" +
                    "\n" +
                    "**낮을수록** 도시가 더 많이 비칩니다.\n" +
                    "**100%** 완전 불투명입니다.\n" +
                    "\n" +
                    "배경만 바뀝니다. 텍스트, 아이콘, 색상 견본은 모든 설정에서 선명하게 유지됩니다.\n" +
                    "\n" +
                    "**슬라이더는 표준 패널에서만 작동합니다.** 어두운 패널은 게임 자체 패널 표면을 사용하므로 게임의 인터페이스 불투명도 설정을 따릅니다.\n" +
                    "어두운 패널은 Legacy 또는 Modern UI 모두에서 잘 작동합니다. 표준 패널은 Modern UI에 가장 적합합니다."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ 더 어두운 패널" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "켜기 = <어두운 패널>: Legacy UI용. Modern UI에서도 더 어두운 배경을 원하면 사용할 수 있습니다.\n" +
                    "끄기 = <표준 패널>: Hover Colors의 반투명 스타일.\n" +
                    "- 더 밝고 현대적인 느낌.\n" +
                    "- 새 Modern UI를 쓰는 대부분의 플레이어에게 좋습니다.\n" +
                    "\n" +
                    "둘 다 써보고 고르세요. 이 모드 패널 배경만 바뀌며 게임 UI는 바뀌지 않습니다."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ 툴팁 표시 (추천)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "대부분의 플레이어는 <켜두는 것>을 추천합니다.\n" +
                    "Hover Colors 버튼에 마우스를 올리면 짧은 도움말을 표시합니다.\n" +
                    "끄면 제목 표시줄의 정보 (i) 또는 이 체크박스로 다시 켤 수 있습니다.\n" +
                    "실수 방지를 위해 툴팁은 이 옵션 메뉴에서만 끌 수 있습니다."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "모드 기본값으로 초기화" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "모든 Hover Colors 설정을 새 설치 상태로 되돌립니다: 색상, 외곽선 두께, 도구 색, 가이드, 패널, 툴팁.\n" +
                    "\n" +
                    "**저장한 프리셋(Set A, Set B)도 삭제됩니다.**\n" +
                    "\n" +
                    "키 설정은 바뀌지 않습니다.\n" +
                    "Hover Colors를 처음 설치한 상태와 같습니다."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "모든 Hover Colors 설정을 새 설치 상태로 초기화할까요?\n\n저장한 프리셋(Set A, Set B)이 삭제됩니다." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "색상을 바닐라로 초기화" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "게임 기본 모습으로 되돌립니다: 외곽선, 소유자 강조, 채움, 두께, 가이드, 구역.\n" +
                    "\n" +
                    "그 외 설정은 유지됩니다: 불도저/도로, 도구 미리보기, 프리셋, 패널, 키 설정.\n" +
                    "\n" +
                    "참고: 초기화 없이 모드를 삭제해도 됩니다. 강조 표시는 자동으로 게임 기본값으로 돌아갑니다.\n"+
                    "게임 기본 색상으로 빠르게 되돌리는 버튼입니다."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "이 모드가 바꾸는 색상을 게임 기본 모습으로 되돌릴까요?\n\n프리셋, 도구 동작, 패널 설정은 유지됩니다." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "메인 패널 열기/닫기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "도시 내 색상 패널을 <열기 / 닫기> 단축키." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors 패널 전환" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "빠른 눈 아이콘 On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "제목 표시줄 눈 아이콘용 선택 단축키: 강조 + 채움 색을 즉시 Off/On.\n" +
                    "키 충돌 방지를 위해 기본 미지정입니다." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "빠른 눈 아이콘 On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 도구 미리보기 켜기/끄기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "표면 배치 중 활성 Surface 경계 미리보기 선을 <숨기거나 표시>하는 단축키." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 미리보기 레이어 켜기/끄기" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "프리셋 1+2 전환" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "단축키로\n" +
                    "<프리셋 슬롯 1과 2>를 전환합니다." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "프리셋 1과 2 전환" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "모드" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "버전" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochi의 Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**작성자의 Paradox Mods 페이지를 엽니다.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Mochi를 사랑으로 기억하며."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "이 모드는 Mochi에게 바칩니다.\n" +
                    "7살에 입양한 사랑스러운 강아지였고,\n" +
                    "13년 동안 사랑과 기쁨을 주었습니다.\n" +
                    "Mochi 없이는 이 모드도 없었습니다."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
