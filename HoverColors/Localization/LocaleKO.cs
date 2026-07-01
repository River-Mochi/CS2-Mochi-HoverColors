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

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleKO : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "작업" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "정보" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "도구 색상" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "패널" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "키 설정" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "가이드" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "헌정" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "불도저 + 도로" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "불도저나 도로 도구 사용 중 임시 윤곽선 색을 설정합니다.\n\n**1. 추천**: 철거는 경고색 노랑, 도로는 부드러운 바닐라 파랑을 사용합니다.\n**2. 바닐라 색상**: 불도저/도로 도구 중 게임 기본 파랑으로 되돌립니다.\n**3. 내 색 유지**: 선택한 색을 모든 곳에 사용합니다.\n\n철거할 때 내 색이 잘 안 보이는 경우에 좋습니다. 저장된 색은 덮어쓰지 않습니다." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 추천" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. 바닐라 색상" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 내 색 유지" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "겹침 윤곽선" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<켜기 추천>\n오브젝트나 네트워크가 겹쳐서 배치가 막힐 때, 게임 기본 연어색 빨강 윤곽선을 유지합니다.\n특수 산업 농장 반경 같은 영역 제한 가이드는 건드리지 않습니다.\n\n모든 불도저 + 도로 모드에서 작동하며 저장된 색을 덮어쓰지 않습니다." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes에 커스텀 색상" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<켜기 추천>\n울타리, 생울타리, 표시선 같은 NetLane 계열 도구에 저장된 HC 색/투명도를 사용합니다.\n\n- 일반 도로는 불도저 + 도로 설정을 따릅니다.\n- 게임 기본 파랑을 쓰려면 끄세요.\n- 겹침 오류 색상이 켜져 있으면 그 색이 우선입니다(바닐라 연어색 빨강)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "모드 툴팁" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<켜짐> = 모드 도움말 툴팁 표시(추천 [x]).\n<꺼짐> = 이 모드 툴팁 숨김.\n이 옵션 메뉴에서만 끌 수 있습니다.\n도시 화면에서는 제목 표시줄의 Info (i) 버튼으로 다시 켤 수 있습니다." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "더 어두운 패널" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "켜짐 = <어두운 패널>: 레거시 UI용, 또는 더 어두운 패널을 원할 때.\n꺼짐 = <기본 패널>: 이 모드의 반투명 스타일.\n- 더 가볍고 현대적인 느낌.\n- 새 Modern UI에 가장 잘 맞습니다.\n\n둘 다 써 보세요. 모드 패널 배경만 바뀌며 게임 UI는 바뀌지 않습니다." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "가이드 불투명도 (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "도로, 울타리, props 등을 배치할 때 점선 정렬 가이드의 불투명도를 조절합니다.\n\n**100%** 게임 기본값.\n**낮게** 더 투명.\n**0%** 모두 숨김.\n15% 아래는 선이 잘 안 보입니다.\n도시 패널에도 같은 슬라이더가 있으며 서로 동기화됩니다." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "메인 패널 열기/닫기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "도시 안 색상 패널을 <열기 / 닫기> 하는 단축키." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "패널 표시 전환" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 미리보기 켜기/끄기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 배치 중 경계 미리보기 선을 <숨기기/보이기> 하는 단축키." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 미리보기 On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "프리셋 1+2 전환" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "<프리셋 슬롯 1과 2> 사이를\n전환하는 단축키." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "프리셋 1과 2 전환" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "모드" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "버전" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**작성자의 Paradox Mods 페이지를 엽니다.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "사랑하는 Mochi를 기억하며." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "이 모드는 Mochi에게 바칩니다.\n7살에 입양된 사랑스러운 강아지였고,\n13년 동안 사랑과 기쁨을 주었습니다.\nMochi가 없었다면 이 모드는 없었을 것입니다." },
            };
        }

        public void Unload()
        {
        }
    }
}
