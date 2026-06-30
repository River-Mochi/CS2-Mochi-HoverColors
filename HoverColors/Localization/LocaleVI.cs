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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Hành động" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Giới thiệu" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Cách màu của công cụ hoạt động" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Bảng" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Phím tắt" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Đường dẫn hướng" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Tưởng nhớ" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Ủi phá + Đường" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Điều khiển màu viền tạm thời khi công cụ ủi phá hoặc đường đang hoạt động.\n\n**1. Khuyến nghị** dùng màu cảnh báo của game (vàng) cho phá dỡ và màu xanh vanilla dịu hơn cho đường.\n**2. Màu công cụ vanilla** khôi phục màu xanh vanilla bình thường của game khi dùng ủi phá hoặc đường.\n**3. Giữ màu tùy chỉnh của tôi** dùng màu bạn chọn ở mọi nơi.\n\nMục đích: một số người chơi/tester thấy màu tùy chỉnh khó nhìn khi phá dỡ.\nCác tùy chọn này giúp màu dễ thấy hơn khi dùng công cụ.\nKhông ghi đè màu tùy chỉnh đã tự động lưu trong bộ chọn màu." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Khuyến nghị" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Màu công cụ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Giữ màu tùy chỉnh của tôi" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Bật viền cho mục bị chồng lấn" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Khuyến nghị bật>\nGiữ viền đỏ cá hồi vanilla của game hiển thị khi đặt vật thể hoặc mạng bị chặn do chồng lấn.\nCác giới hạn vùng, như hướng dẫn bán kính nông trại của công nghiệp chuyên biệt, không bị thay đổi.\n\nHoạt động với mọi chế độ Ủi phá + Đường và không ghi đè màu đã lưu của bạn." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Cho phép màu tùy chỉnh cho NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Khuyến nghị bật>\nDùng màu/độ trong suốt đã lưu của Màu khi rê chuột khi đặt chi tiết NetLane như hàng rào, bụi cây, vạch kẻ và công cụ theo làn tương tự.\n\n- Đường bình thường vẫn theo thiết lập Ủi phá + Đường bạn chọn trong danh sách.\n- Tắt nếu bạn muốn các công cụ đó dùng viền xanh vanilla của game.\n- Màu lỗi chồng lấn vẫn ưu tiên khi bật (màu lỗi vanilla = đỏ cá hồi)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Chú giải của Màu khi rê chuột" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Bật> = hiển thị chú giải trợ giúp của Màu khi rê chuột (khuyến nghị [x]).\n<Tắt> = ẩn chú giải của mod này.\nChỉ có thể tắt chú giải trong menu Tùy chọn này.\nNhưng bạn có thể bật lại trong thành phố: nhấp nút Info (i) trên thanh tiêu đề." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Bảng tối hơn" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Bật = <Bảng tối>: dành cho người chơi UI cũ; cũng dùng được trong UI Hiện đại nếu bạn thích bảng tối hơn.\nTắt = <Bảng chuẩn>: phong cách trong mờ tùy chỉnh của Màu khi rê chuột.\n- Nhẹ và hiện đại hơn.\n- Tốt nhất cho đa số người chơi dùng UI Hiện đại mới.\n\nHãy thử cả hai và chọn cái bạn thích. Chỉ đổi nền của bảng mod này, không đổi UI của game." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Độ mờ đường dẫn hướng (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Điều khiển độ mờ của đường dẫn hướng căn chỉnh dạng gạch, hữu ích khi đặt đường, hàng rào, prop, v.v.\n\n**100%** giữ giao diện vanilla mặc định.\n**Thấp hơn** làm đường dẫn hướng trong suốt hơn.\n**0%** ẩn hoàn toàn - <Không khuyến nghị>.\nNên để trên 15% nếu không sẽ khó nhìn chuyện gì đang xảy ra.\nThanh trượt này cũng có trên bảng mod trong thành phố. Cả hai được đồng bộ;\nnếu đổi thanh này, thanh trong thành phố cũng đổi." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Mở/đóng bảng chính" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Phím tắt để mở/đóng bảng màu đối tượng khi rê chuột trong thành phố." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Bật/tắt bảng Màu khi rê chuột" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Bật/tắt xem trước công cụ Bề mặt" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Phím tắt để ẩn hoặc khôi phục đường xem trước ranh giới công cụ Bề mặt khi đặt bề mặt." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Lớp xem trước công cụ Bề mặt On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Chuyển preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Phím tắt để chuyển giữa ô preset 1 và ô 2." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Chuyển giữa preset 1 và 2" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Phiên bản" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mở trang Paradox Mods của tác giả." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Tưởng nhớ Mochi." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mod này dành tặng Mochi. Cô bé là một chú chó được yêu thương, được nhận nuôi lúc 7 tuổi,\nvà đã mang đến 13 năm yêu thương và niềm vui. Mod này sẽ không thể có nếu thiếu Mochi." },
            };
        }

        public void Unload()
        {
        }
    }
}
