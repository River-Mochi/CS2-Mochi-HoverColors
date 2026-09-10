// <copyright file="LocaleTH.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleTH.cs
// Purpose: Thai (th-TH) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/th-TH.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleTH : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleTH(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "การทำงาน" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "ปุ่มลัด" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "เกี่ยวกับ" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "สีของเครื่องมือ" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "แผง" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "ปุ่มลัด" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "อุทิศให้" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + ถนน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "ควบคุมสีเส้นขอบชั่วคราวเมื่อใช้ bulldozer หรือเครื่องมือถนน\n" +
                    "\n" +
                    "**1. แนะนำ** ใช้สีเตือนของเกม (เหลือง) สำหรับรื้อ และสีน้ำเงิน vanilla ที่นุ่มลงสำหรับถนน\n" +
                    "**2. สีเครื่องมือ vanilla** คืนสีน้ำเงิน vanilla ปกติเมื่อใช้ bulldozer หรือถนน\n" +
                    "**3. ใช้สีของฉัน** ใช้สีที่คุณเลือกทุกที่\n" +
                    "\n" +
                    "เหตุผล: ผู้ใช้/ผู้ทดสอบบางคนมองสีของตนยากตอนรื้อ\n" +
                    "ตัวเลือกนี้ให้สีที่มองเห็นชัดขณะใช้เครื่องมือ\n" +
                    "ไม่เขียนทับสีที่บันทึกอัตโนมัติในตัวเลือกสี"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. แนะนำ" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. สีเครื่องมือ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ใช้สีของฉัน" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "เปิดเส้นขอบของวัตถุที่ซ้อนทับ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<แนะนำให้เปิด>\n" +
                    "คงเส้นขอบสีแดงแซลมอนของเกมไว้เมื่อวางวัตถุหรือเน็ตเวิร์กไม่ได้เพราะซ้อนทับ\n" +
                    "ขอบเขตพื้นที่ เช่น รัศมีฟาร์ม Specialized Industry จะไม่ถูกเปลี่ยน\n" +
                    "\n" +
                    "ใช้ได้กับทุกโหมด Bulldozer + ถนน และไม่เขียนทับสีที่คุณบันทึกไว้"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "อนุญาตสีเองสำหรับ NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<แนะนำให้เปิด>\n" +
                    "ใช้สี/ความโปร่งใส HC ที่บันทึกไว้เมื่อวางรายละเอียด NetLane เช่น รั้ว พุ่มไม้ เส้นมาร์ก และอื่น ๆ\n" +
                    "\n" +
                    "- ถนนปกติยังใช้ค่าที่เลือกในรายการ Bulldozer + ถนน\n" +
                    "- ปิดถ้าต้องการให้เครื่องมือเหล่านี้ใช้สีน้ำเงิน vanilla ของเกม\n" +
                    "- สีข้อผิดพลาดการซ้อนทับยังมีผลก่อนเสมอเมื่อเปิด (สี vanilla = แดงแซลมอน)"
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "ทูลทิปสีเมื่อชี้เมาส์" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<เปิด> = แสดงทูลทิปช่วยสีเมื่อชี้เมาส์ (แนะนำ [x])\n" +
                    "<ปิด> = ซ่อนทูลทิปของม็อดนี้\n" +
                    "ปิดทูลทิปได้เฉพาะในเมนู Options นี้\n" +
                    "แต่เปิดกลับในเมืองได้: คลิกปุ่ม Info (i) บนแถบหัวเรื่อง"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "แผงมืดขึ้น" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "เปิด = <แผงมืด>: ทำเพื่อผู้ใช้ LegacyUI; ใช้กับ Modern UI ได้ถ้าต้องการคอนทราสต์สูงขึ้น\n" +
                    "ปิด = <แผงมาตรฐาน>: สไตล์โปร่งแสงของสีเมื่อชี้เมาส์\n" +
                    "- ดูสว่างและทันสมัยขึ้น\n" +
                    "- เหมาะกับผู้เล่นส่วนใหญ่ที่ใช้ Modern UI ใหม่\n" +
                    "\n" +
                    "ลองทั้งสองแบบแล้วเลือกที่ชอบ การตั้งค่านี้เปลี่ยนเฉพาะพื้นหลังแผงม็อด ไม่ใช่ UI เกม"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "เปิด/ปิดแผงหลัก" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "ปุ่มลัดเพื่อ<เปิด / ปิด>แผงสีในเมือง" },

                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "สลับแผงสีเมื่อชี้เมาส์" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "พรีวิวเครื่องมือ Surface เปิด/ปิด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "ปุ่มลัดเพื่อ <ซ่อนหรือแสดง> เส้นขอบ Surface ที่ใช้งานอยู่ตอนวางพื้นผิว" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "เลเยอร์พรีวิว Surface เปิด/ปิด" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "สลับพรีเซ็ต 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "ปุ่มลัดเพื่อสลับระหว่าง\n" +
                    "<ช่องพรีเซ็ต 1 และ 2>" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "สลับระหว่างพรีเซ็ต 1 และ 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "ม็อด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "เวอร์ชัน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**เปิดหน้า Paradox Mods ของผู้สร้าง**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "แด่ความทรงจำของ Mochi"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "ม็อดนี้อุทิศให้ Mochi\n" +
                    "เธอเป็นน้องหมาที่รักมาก รับมาเลี้ยงตอนอายุ 7 ปี\n" +
                    "และมอบความรักกับความสุขให้ 13 ปี\n" +
                    "ม็อดนี้คงเกิดขึ้นไม่ได้ถ้าไม่มี Mochi"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
