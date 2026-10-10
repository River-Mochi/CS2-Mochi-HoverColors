// <copyright file="LocaleVI.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Localization/LocaleVI.cs
// Purpose: Vietnamese (vi-VN) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/vi-VN.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleVI : IDictionarySource
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

                // Tabs
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Thao tác" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Phím tắt" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Giới thiệu" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Màu công cụ" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Bảng" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Đặt lại" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Phím tắt" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "Tưởng nhớ" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + đường" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Điều khiển màu viền tạm thời khi dùng bulldozer hoặc công cụ đường.\n" +
                    "\n" +
                    "**1. Khuyên dùng** dùng màu Cảnh báo vàng khi phá dỡ. Khi dùng công cụ đường/lối đi, màu xanh vanilla trên phần xem trước mới và đoạn đường dưới công cụ sẽ dịu hơn. Rê chuột bình thường trên đường có sẵn vẫn dùng màu Viền của bạn. Phần tô lớn của đường mới theo màu xem trước của Hướng dẫn.\n" +
                    "**2. Màu công cụ vanilla** trả lại xanh vanilla bình thường khi dùng bulldozer hoặc công cụ đường.\n" +
                    "**3. Giữ màu của tôi** dùng màu bạn chọn ở mọi nơi.\n" +
                    "\n" +
                    "Hữu ích nếu màu của bạn khó nhìn khi phá dỡ.\n" +
                    "Hướng dẫn dấu chân đường riêng cũng theo màu xem trước của Hướng dẫn.\n" +
                    "Không ghi đè màu đã lưu trong bộ chọn màu."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Khuyên dùng" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Màu công cụ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Giữ màu của tôi" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Bật viền vật thể bị chồng lấn" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Nên bật>\n" +
                    "Giữ viền đỏ cá hồi vanilla khi không thể đặt vật thể hoặc mạng vì chồng lấn.\n" +
                    "Giới hạn vùng, như bán kính trang trại Công nghiệp chuyên biệt, không bị đổi.\n" +
                    "\n" +
                    "Hoạt động với mọi chế độ Bulldozer + đường và không ghi đè màu đã lưu."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ Cho phép màu riêng cho NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Nên bật>\n" +
                    "Dùng màu/độ trong suốt HC đã lưu cho chi tiết NetLane như hàng rào, bụi cây, vạch kẻ.\n" +
                    "\n" +
                    "- Đường thường vẫn theo cài đặt Bulldozer + đường.\n" +
                    "- Tắt nếu muốn các công cụ này dùng xanh vanilla của game.\n" +
                    "- Màu lỗi chồng lấn vẫn ưu tiên (vanilla = đỏ cá hồi)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Chọn bảng tối hoặc kính" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Tối (Vanilla)** dùng bảng của game.\n" +
                    "- Tự khớp với Legacy UI hoặc Modern UI.\n" +
                    "- Theo cài đặt Độ đục Giao diện của game.\n" +
                    "\n" +
                    "**Kính (Tùy chỉnh)** dùng bảng kính sáng hơn của Hover Colors.\n" +
                    "- Ngay cả 100% vẫn thấy một chút thành phố.\n" +
                    "- Thêm thanh Độ đục bảng.\n" +
                    "\n" +
                    "Thử cả hai và chọn kiểu bạn thích! Chỉ nền bảng mod thay đổi, không ảnh hưởng UI game.\n" +
                    "\n" +
                    "Mẹo: game làm mờ phía sau các bảng. Độ trong suốt Giao diện 0% sẽ tắt hiệu ứng mờ; 1% trở lên sẽ giữ nó."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Tối (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Kính (Tùy chỉnh)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Vị trí nút bảng" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Chọn góc trên trái, trên phải hoặc Universal Mod Menu. Nút chuyển vị trí ngay khi bạn thay đổi cài đặt này."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "Trên trái + Universal" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "Trên phải + Universal" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Độ đục bảng" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Độ đục nền của bảng **Kính (Tùy chỉnh)**.\n" +
                    "\n" +
                    "**30%** = trong suốt nhất.\n" +
                    "**100%** = gần như đặc, vẫn thấy chút thành phố.\n" +
                    "\n" +
                    "Chỉ nền thay đổi; chữ, biểu tượng và ô màu luôn dễ đọc.\n" +
                    "\n" +
                    "**Tối (Vanilla)** theo Độ đục Giao diện của game nên thanh này sẽ bị ẩn."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ Hiện mẹo (khuyên dùng)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "Đa số người chơi nên <để bật>.\n" +
                    "Hiện trợ giúp ngắn khi rê chuột lên nút Hover Colors.\n" +
                    "Nếu tắt, bấm Info (i) trên thanh tiêu đề hoặc bật lại tùy chọn này.\n" +
                    "Mẹo chỉ có thể tắt trong menu Tùy chọn này."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Đặt lại mặc định mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Đưa mọi cài đặt Hover Colors về như mới cài: màu, độ dày viền, công cụ, hướng dẫn, bảng và mẹo.\n" +
                    "\n" +
                    "**Preset đã lưu (Set A và Set B) cũng sẽ bị xóa.**\n" +
                    "\n" +
                    "Phím tắt không đổi.\n" +
                    "Giống như cài Hover Colors lần đầu."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Đặt lại mọi cài đặt Hover Colors?\n" +
                    "\n" +
                    "Preset đã lưu (Set A và Set B) sẽ bị xóa."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Đặt lại màu vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Trả lại giao diện gốc của game: viền, tô sáng chủ sở hữu, fill, độ dày, hướng dẫn và quận.\n" +
                    "\n" +
                    "Mọi thứ khác giữ nguyên: Bulldozer/đường, xem trước, preset, bảng và phím tắt.\n" +
                    "\n" +
                    "Có thể gỡ mod mà không cần reset; highlight tự trở về mặc định game.\n" +
                    "Đây chỉ là nút reset nhanh màu sắc."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Đặt lại các màu do mod kiểm soát về giao diện gốc của game?\n" +
                    "\n" +
                    "Preset, hành vi công cụ và tùy chọn bảng vẫn được giữ."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Mở/đóng bảng chính" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Phím tắt để <mở / đóng> bảng màu trong thành phố."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Bật/tắt bảng Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Mắt nhanh Bật/Tắt" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Phím tắt tùy chọn cho nút Mắt: bật/tắt ngay Highlight + Fill.\n" +
                    "Mặc định chưa gán phím để tránh xung đột."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Mắt nhanh Bật/Tắt" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Mẹo góc Bật/Tắt" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Phím tắt cho ô góc và Đặt/Hoàn tác.\n" +
                    "Dùng khi vẽ đường hoặc lối đi mới.\n" +
                    "Chi phí, chiều dài và độ dốc vẫn hiển thị."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Góc và gợi ý chuột Bật/Tắt" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Đổi preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Phím tắt để đổi giữa\n" +
                    "<preset 1 và preset 2>."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Đổi giữa preset 1 và 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Phiên bản" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods của Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Mở trang Paradox Mods của tác giả.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Thương nhớ Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Mod này dành tặng Mochi.\n" +
                    "Cô bé là một chú chó được yêu thương, được nhận nuôi lúc 7 tuổi,\n" +
                    "và mang đến 13 năm yêu thương, niềm vui.\n" +
                    "Không có Mochi thì mod này cũng không thể có."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
