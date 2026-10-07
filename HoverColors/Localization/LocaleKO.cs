// <copyright file="LocaleKO.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "불도저나 도로 도구를 사용할 때 임시 외곽선 색을 조절합니다.\n" +
                    "\n" +
                    "**1. 추천** 철거에는 노란 경고색을 사용합니다. 도로/보행로 도구 사용 중에는 새 미리보기와 도구 아래 기존 구간의 바닐라 파랑을 부드럽게 합니다. 기존 도로에 평소처럼 마우스를 올리면 내 외곽선 색을 계속 사용합니다. 새 도로의 넓은 채움은 가이드 미리보기 색을 따릅니다.\n" +
                    "**2. 바닐라 도구 색** 불도저/도로 도구 사용 중 기본 바닐라 파랑으로 돌아갑니다.\n" +
                    "**3. 내 색 유지** 선택한 색을 모든 곳에 사용합니다.\n" +
                    "\n" +
                    "철거 중 내 색이 잘 안 보일 때 유용합니다.\n" +
                    "별도 도로 영역 가이드도 가이드 미리보기 색을 따릅니다.\n" +
                    "색상 선택기에 저장한 색은 덮어쓰지 않습니다."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 추천" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 바닐라 도구 색" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 내 색 유지" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ 겹친 항목 외곽선 켜기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<켜기 추천>\n" +
                    "겹침 때문에 오브젝트/네트워크를 배치할 수 없을 때 기본 연어색 외곽선을 표시합니다.\n" +
                    "특수 산업 농장 반경 같은 영역 제한은 그대로 둡니다.\n" +
                    "\n" +
                    "모든 불도저 + 도로 모드에서 작동하며 저장한 색을 덮어쓰지 않습니다."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ NetLanes에 사용자 색 허용" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<켜기 추천>\n" +
                    "울타리, 생울타리, 표시 등 NetLane 디테일에 저장한 HC 색/투명도를 사용합니다.\n" +
                    "\n" +
                    "- 일반 도로는 불도저 + 도로 설정을 계속 따릅니다.\n" +
                    "- 바닐라 파랑을 쓰려면 끄세요.\n" +
                    "- 겹침 오류색이 켜져 있으면 항상 우선합니다(바닐라 = 연어색)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ 다크 또는 글래스 패널 선택" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**어두움 (바닐라)** 은 게임 기본 패널을 사용합니다.\n" +
                    "- Legacy UI / Modern UI에 자동으로 맞습니다.\n" +
                    "- 게임의 인터페이스 불투명도 설정을 따릅니다.\n" +
                    "\n" +
                    "**유리 (사용자 지정)** 는 더 밝은 Hover Colors 유리 패널입니다.\n" +
                    "- 100%에서도 도시가 조금 비칩니다.\n" +
                    "- 패널 불투명도 슬라이더를 추가합니다.\n" +
                    "\n" +
                    "둘 다 써 보고 마음에 드는 쪽을 고르세요. 이 모드 패널 배경만 바뀝니다.\n" +
                    "\n" +
                    "팁: 게임은 패널 뒤를 흐리게 합니다. 인터페이스 투명도 0%면 흐림 효과가 꺼지고, 1% 이상이면 유지됩니다."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "어두움 (바닐라)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "유리 (사용자 지정)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ 패널 버튼 위치" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "왼쪽 위, 오른쪽 위 또는 Universal Mod Menu를 선택합니다.\n" +
                    "위치 변경은 **게임 재시작 후** 적용됩니다."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "왼쪽 위" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "오른쪽 위" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ 패널 불투명도" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "**유리 (사용자 지정)** 패널의 배경 불투명도입니다.\n" +
                    "\n" +
                    "**30%** = 가장 투명.\n" +
                    "**100%** = 거의 불투명하지만 도시가 조금 비칩니다.\n" +
                    "\n" +
                    "배경만 바뀌며 텍스트, 아이콘, 색상은 항상 잘 보입니다.\n" +
                    "\n" +
                    "**어두움 (바닐라)** 은 게임의 인터페이스 불투명도를 따르므로 이 슬라이더가 숨겨집니다."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ 툴팁 표시 (추천)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "대부분의 플레이어는 <켜두는 것>을 추천합니다.\n" +
                    "Hover Colors 버튼에 마우스를 올리면 짧은 도움말을 표시합니다.\n" +
                    "끄면 제목 표시줄의 Info (i) 또는 이 옵션으로 다시 켤 수 있습니다.\n" +
                    "툴팁은 이 옵션 메뉴에서만 끌 수 있습니다."
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
                    "모든 Hover Colors 설정을 초기화할까요?\n" +
                    "\n" +
                    "저장한 프리셋(Set A, Set B)이 삭제됩니다."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "색상을 바닐라로 초기화" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "게임 기본 모습으로 되돌립니다: 외곽선, 소유자 강조, 채움, 두께, 가이드, 구역.\n" +
                    "\n" +
                    "그 외 설정은 유지됩니다: 불도저/도로, 도구 미리보기, 프리셋, 패널, 키 설정.\n" +
                    "\n" +
                    "초기화 없이 모드를 삭제해도 강조 표시는 자동으로 게임 기본값으로 돌아갑니다.\n" +
                    "색상만 빠르게 되돌리는 버튼입니다."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "이 모드가 바꾸는 색상을 게임 기본 모습으로 되돌릴까요?\n" +
                    "\n" +
                    "프리셋, 도구 동작, 패널 설정은 유지됩니다."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "메인 패널 열기/닫기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "도시 내 색상 패널을 <열기 / 닫기> 단축키."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors 패널 전환" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "빠른 눈 아이콘 On/Off" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "눈 아이콘용 선택 단축키: 강조 + 채움 색을 즉시 Off/On.\n" +
                    "키 충돌 방지를 위해 기본 미지정입니다."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "빠른 눈 아이콘 On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "각도 툴팁 켜기/끄기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "각도 및 배치/실행 취소 상자용 단축키입니다.\n" +
                    "새 도로나 길을 그릴 때 작동합니다.\n" +
                    "비용, 길이, 경사는 계속 표시됩니다."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "각도 및 마우스 안내 켜기/끄기" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "프리셋 1+2 전환" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "단축키로\n" +
                    "<프리셋 1과 2>를 전환합니다."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "프리셋 1과 2 전환" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "모드" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "버전" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochi의 Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**제작자의 Paradox Mods 페이지를 엽니다.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "사랑하는 Mochi를 기억하며."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "이 모드는 Mochi에게 바칩니다.\n" +
                    "7살에 입양한 사랑스러운 강아지였고,\n" +
                    "13년 동안 사랑과 기쁨을 주었습니다.\n" +
                    "Mochi가 없었다면 이 모드도 없었을 것입니다."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
