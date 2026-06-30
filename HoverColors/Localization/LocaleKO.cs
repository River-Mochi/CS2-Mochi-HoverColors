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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "동작" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "정보" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "도구 색상 동작" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "패널" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "키 바인딩" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "가이드라인" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "헌정" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "불도저 + 도로" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "불도저 또는 도로 도구가 활성화되어 있을 때 임시 외곽선 색상을 제어합니다.\n\n**1. 권장**은 철거에는 게임의 경고색(노랑)을, 도로에는 더 부드러운 vanilla 파랑을 사용합니다.\n**2. vanilla 도구 색상**은 불도저 또는 도로 도구 사용 중 게임의 기본 vanilla 파랑으로 되돌립니다.\n**3. 내 사용자 지정 색 유지**는 선택한 색을 모든 곳에 사용합니다.\n\n목적: 일부 사용자/테스터는 철거 중 사용자 지정 색이 잘 보이지 않는다고 느낍니다.\n이 옵션은 도구 사용 중 더 잘 보이는 색을 제공합니다.\n색상 선택기에 자동 저장된 사용자 지정 색을 덮어쓰지 않습니다." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. 권장" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. vanilla 도구 색상" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. 내 사용자 지정 색 유지" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "겹치는 항목 외곽선 활성화" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<활성화 권장>\n오브젝트 또는 네트워크 배치가 겹치는 항목 때문에 막힐 때, 게임의 vanilla 연어색 빨간 외곽선을 계속 표시합니다.\n전문 산업 농장 반경 가이드 같은 영역 제한은 변경하지 않습니다.\n\n모든 불도저 + 도로 모드에서 작동하며 저장된 사용자 지정 색을 덮어쓰지 않습니다." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes에 사용자 지정 색 허용" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<활성화 권장>\n울타리, 생울타리, 표시선 등 NetLane 세부 항목과 비슷한 차선 기반 도구를 배치할 때 저장된 호버 색상/투명도를 사용합니다.\n\n- 일반 도로는 드롭다운에서 선택한 불도저 + 도로 설정을 계속 따릅니다.\n- 해당 도구들이 게임의 vanilla 파란 외곽선을 사용하길 원하면 이 옵션을 끄세요.\n- 겹침 오류 색이 활성화되면 여전히 우선 적용됩니다(vanilla 오류 색 = 연어색 빨강)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "호버 색상 툴팁" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<활성화> = 호버 색상 도움말 툴팁을 표시합니다(권장 [x]).\n<비활성화> = 이 모드의 툴팁을 숨깁니다.\n툴팁은 이 옵션 메뉴 안에서만 비활성화할 수 있습니다.\n하지만 도시 안에서는 제목 표시줄의 Info (i) 버튼을 클릭해 다시 켤 수 있습니다." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "더 어두운 패널" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "활성화 = <어두운 패널>: 레거시 UI 사용자를 위해 만들었으며, 더 어두운 패널을 원하면 Modern UI에서도 사용할 수 있습니다.\n비활성화 = <표준 패널>: 호버 색상의 사용자 지정 반투명 스타일.\n- 더 가볍고 현대적인 느낌.\n- 새 Modern UI를 쓰는 대부분의 플레이어에게 가장 좋습니다.\n\n둘 다 써 보고 선호하는 쪽을 선택하세요. 이 설정은 이 모드 패널 배경만 바꾸며 게임 UI는 바꾸지 않습니다." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "가이드라인 불투명도(알파)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "도로, 울타리, 프롭 등을 배치할 때 유용한 점선 정렬 가이드의 불투명도를 제어합니다.\n\n**100%**는 vanilla 기본 모양을 유지합니다.\n**낮게** 하면 가이드라인이 더 투명해집니다.\n**0%**는 완전히 숨깁니다 - <권장하지 않음>.\n15% 이상을 권장합니다. 너무 낮으면 상황을 보기 어렵습니다.\n같은 슬라이더가 도시 모드 패널에도 있습니다. 둘은 동기화되어 있습니다;\n이 값을 바꾸면 도시 안의 슬라이더도 함께 바뀝니다." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "메인 패널 열기/닫기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "도시 안 호버 오브젝트 색상 패널을 열거나 닫는 단축키입니다." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "호버 색상 패널 전환" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface 도구 미리보기 켜기/끄기" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "표면을 배치하는 동안 활성 Surface 도구 경계 미리보기 선을 숨기거나 복원하는 단축키입니다." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface 도구 미리보기 레이어 On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "프리셋 1+2 전환" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "프리셋 슬롯 1과 슬롯 2를 바꾸는 단축키입니다." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "프리셋 1과 2 사이 전환" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "모드" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "버전" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "제작자의 Paradox Mods 페이지를 엽니다." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mochi를 사랑으로 기억하며." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "이 모드는 Mochi에게 바칩니다. 7살에 입양된 사랑스러운 강아지였고,\n13년 동안 사랑과 기쁨을 주었습니다. Mochi 없이는 이 모드도 없었을 것입니다." },
            };
        }

        public void Unload()
        {
        }
    }
}
