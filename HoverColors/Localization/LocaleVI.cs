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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + đường" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Điều khiển màu viền tạm thời khi dùng bulldozer hoặc công cụ đường.\n" +
                    "\n" +
                    "**1. Khuyên dùng** dùng màu Cảnh báo của game (vàng) khi phá dỡ và xanh vanilla nhẹ hơn cho đường.\n" +
                    "**2. Màu công cụ vanilla** trả lại xanh vanilla bình thường khi dùng bulldozer hoặc công cụ đường.\n" +
                    "**3. Giữ màu của tôi** dùng màu bạn chọn ở mọi nơi.\n" +
                    "\n" +
                    "Mục đích: vài người dùng/tester thấy màu riêng khó nhìn khi phá dỡ.\n" +
                    "Tùy chọn này cho màu dễ thấy hơn khi dùng công cụ.\n" +
                    "Không ghi đè màu riêng tự lưu trong bộ chọn màu."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Khuyên dùng" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Màu công cụ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Giữ màu của tôi" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Bật viền vật thể bị chồng lấn" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Nên bật>\n" +
                    "Giữ viền đỏ cá hồi vanilla của game khi đặt vật thể hoặc mạng bị chặn do chồng lấn.\n" +
                    "Giới hạn vùng, như bán kính trang trại Công nghiệp chuyên biệt, không bị đổi.\n" +
                    "\n" +
                    "Hoạt động với mọi chế độ Bulldozer + đường và không ghi đè màu đã lưu."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "Cho phép màu riêng cho NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Nên bật>\n" +
                    "Dùng màu/độ trong suốt HC đã lưu khi đặt chi tiết NetLane như hàng rào, bụi cây, vạch kẻ và tương tự.\n" +
                    "\n" +
                    "- Đường thường vẫn theo cài đặt Bulldozer + đường bạn chọn trong danh sách.\n" +
                    "- Tắt nếu muốn các công cụ đó dùng viền xanh vanilla của game.\n" +
                    "- Màu lỗi chồng lấn vẫn ưu tiên khi bật (màu lỗi vanilla = đỏ cá hồi)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Mẹo màu khi rê chuột" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Bật> = hiện mẹo trợ giúp màu khi rê chuột (khuyên dùng [x]).\n" +
                    "<Tắt> = ẩn mẹo của mod này.\n" +
                    "Chỉ có thể tắt mẹo trong menu Tùy chọn này.\n" +
                    "Bạn có thể bật lại trong thành phố: bấm nút Info (i) trên thanh tiêu đề."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Bảng tối hơn" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Bật = <Bảng tối>: dành cho người dùng LegacyUI; cũng dùng được trong Modern UI nếu bạn thích tương phản mạnh hơn.\n" +
                    "Tắt = <Bảng chuẩn>: kiểu trong mờ riêng cho màu khi rê chuột.\n" +
                    "- Sáng hơn, hiện đại hơn.\n" +
                    "- Hợp với đa số người chơi dùng Modern UI mới.\n" +
                    "\n" +
                    "Thử cả hai để chọn. Chỉ đổi nền bảng mod này, không đổi UI của game."
                },


                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Mở/đóng bảng chính" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Phím tắt để <mở / đóng> bảng màu trong thành phố." },
               
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Bật/tắt bảng màu khi rê chuột" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Xem trước công cụ Surface bật/tắt" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Phím tắt để <ẩn hoặc hiện> đường ranh Surface đang hoạt động khi đặt bề mặt." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Lớp xem trước Surface bật/tắt" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Đổi preset 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Phím tắt để đổi giữa\n" +
                    "<ô preset 1 và ô 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Đổi giữa preset 1 và 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Phiên bản" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Mở trang Paradox Mods của tác giả.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Tưởng nhớ Mochi."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Mod này dành tặng Mochi.\n" +
                    "Cô bé là chú chó được yêu thương, được nhận nuôi lúc 7 tuổi,\n" +
                    "và đã cho 13 năm yêu thương, niềm vui.\n" +
                    "Không có Mochi thì mod này đã không thể ra đời."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
