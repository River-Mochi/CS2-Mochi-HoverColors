// <copyright file="LocaleVI.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleVI.cs
// Purpose: Vietnamese (vi-VN) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/vi-VN.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleVI : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleVI(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Thao tác" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Giới thiệu" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Màu công cụ" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Bảng" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Phím tắt" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Đường gióng" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Tưởng nhớ" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Ủi phá + đường" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Điều khiển màu viền tạm thời khi dùng công cụ ủi phá hoặc làm đường.\n\n**1. Khuyên dùng**: vàng cảnh báo khi phá, xanh vanilla nhẹ hơn cho đường.\n**2. Màu vanilla**: dùng xanh mặc định của game khi dùng ủi phá/đường.\n**3. Giữ màu của tôi**: dùng màu bạn chọn ở mọi nơi.\n\nHữu ích nếu màu tùy chỉnh khó nhìn khi phá dỡ. Màu đã lưu sẽ không bị ghi đè." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Khuyên dùng" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Màu vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Giữ màu của tôi" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Viền khi chồng lấn" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Khuyên bật>\nGiữ viền đỏ cá hồi vanilla của game khi vật thể hoặc mạng lưới bị chặn do chồng lấn.\nGiới hạn vùng, như bán kính nông trại công nghiệp chuyên biệt, không bị đổi.\n\nHoạt động với mọi chế độ Ủi phá + đường và không ghi đè màu đã lưu." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Màu riêng cho NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Khuyên bật>\nDùng màu/độ trong suốt HC đã lưu cho chi tiết NetLane như hàng rào, bụi cây, vạch kẻ và công cụ tương tự.\n\n- Đường bình thường vẫn theo cài đặt Ủi phá + đường.\n- Tắt nếu muốn dùng xanh vanilla của game.\n- Màu lỗi chồng lấn vẫn ưu tiên nếu bật (đỏ cá hồi vanilla)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Mẹo của mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Bật> = hiện mẹo trợ giúp của mod (khuyên dùng [x]).\n<Tắt> = ẩn mẹo của mod này.\nChỉ có thể tắt trong menu Tùy chọn này.\nTrong thành phố, bấm nút Info (i) trên thanh tiêu đề để bật lại." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Bảng tối hơn" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Bật = <Bảng tối>: dành cho UI cũ, hoặc nếu bạn thích tối hơn.\nTắt = <Bảng chuẩn>: kiểu trong mờ của mod.\n- Nhẹ và hiện đại hơn.\n- Hợp nhất với UI Modern mới.\n\nThử cả hai. Chỉ đổi nền bảng của mod, không đổi UI của game." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Độ mờ đường gióng (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Điều chỉnh độ mờ của đường gióng chấm khi đặt đường, hàng rào, props, v.v.\n\n**100%** mặc định game.\n**Thấp hơn** trong suốt hơn.\n**0%** ẩn hết.\nNên trên 15%, nếu không đường rất khó thấy.\nCùng thanh trượt có trong bảng thành phố; cả hai được đồng bộ." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Mở/đóng bảng chính" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Phím tắt để <mở / đóng> bảng màu trong thành phố." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Bật/tắt bảng" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Xem trước Surface bật/tắt" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Phím tắt để <ẩn hoặc hiện> đường biên xem trước của công cụ Surface khi đặt bề mặt." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Lớp xem trước Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Đổi preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Phím tắt để đổi giữa\n<ô preset 1 và ô 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Đổi giữa preset 1 và 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Phiên bản" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Mở trang Paradox Mods của tác giả.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Tưởng nhớ Mochi yêu dấu." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mod này dành tặng Mochi.\nCô bé là chú chó được yêu thương, nhận nuôi lúc 7 tuổi,\nvà đã mang đến 13 năm yêu thương, niềm vui.\nKhông có Mochi thì mod này sẽ không thể có." },
            };
        }

        public void Unload()
        {
        }
    }
}
