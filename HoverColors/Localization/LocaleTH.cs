// <copyright file="LocaleTH.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Localization/LocaleTH.cs
// Purpose: Thai (th-TH) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/th-TH.json.

namespace HoverColors
{
    using System.Collections.Generic;
    using Colossal;

    public class LocaleTH : IDictionarySource
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

                // Tabs
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "การทำงาน" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "ปุ่มลัด" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "เกี่ยวกับ" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "สีของเครื่องมือ" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "แผง" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "รีเซ็ต" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "ปุ่มลัด" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "อุทิศให้" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + ถนน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "ควบคุมสีเส้นขอบชั่วคราวเมื่อใช้ bulldozer หรือเครื่องมือถนน\n" +
                    "\n" +
                    "**1. แนะนำ** ใช้สีเตือนเหลืองตอนรื้อ เมื่อใช้เครื่องมือถนน/ทางเดิน จะลดความสดของสีน้ำเงิน vanilla ในพรีวิวใหม่และช่วงถนนใต้เครื่องมือ การชี้ถนนเดิมตามปกติยังใช้สีเส้นขอบของคุณ ส่วนพื้นที่เติมกว้างของถนนใหม่ใช้สีพรีวิวจาก Guidelines\n" +
                    "**2. สีเครื่องมือ vanilla** คืนสีน้ำเงิน vanilla ปกติเมื่อใช้ bulldozer หรือถนน\n" +
                    "**3. ใช้สีของฉัน** ใช้สีที่คุณเลือกทุกที่\n" +
                    "\n" +
                    "เหมาะเมื่อสีของคุณมองยากตอนรื้อ\n" +
                    "ไกด์พื้นที่ถนนแยกก็ใช้สีพรีวิวจาก Guidelines เช่นกัน\n" +
                    "ไม่เขียนทับสีที่บันทึกไว้ในตัวเลือกสี"
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. แนะนำ" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. สีเครื่องมือ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ใช้สีของฉัน" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ เปิดเส้นขอบของวัตถุที่ซ้อนทับ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<แนะนำให้เปิด>\n" +
                    "แสดงเส้นขอบแดงแซลมอน vanilla เมื่อวางวัตถุหรือเน็ตเวิร์กไม่ได้เพราะซ้อนทับ\n" +
                    "ขอบเขตพื้นที่ เช่น รัศมีฟาร์ม Specialized Industry จะไม่เปลี่ยน\n" +
                    "\n" +
                    "ใช้ได้กับทุกโหมด Bulldozer + ถนน และไม่เขียนทับสีที่บันทึกไว้"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ อนุญาตสีเองสำหรับ NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<แนะนำให้เปิด>\n" +
                    "ใช้สี/ความโปร่งใส HC ที่บันทึกไว้กับรายละเอียด NetLane เช่น รั้ว พุ่มไม้ และเส้นมาร์ก\n" +
                    "\n" +
                    "- ถนนปกติยังใช้ค่า Bulldozer + ถนน\n" +
                    "- ปิดถ้าต้องการสีน้ำเงิน vanilla ของเกม\n" +
                    "- สีข้อผิดพลาดการซ้อนทับยังมาก่อนเสมอ (vanilla = แดงแซลมอน)"
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ เลือกแผงแบบมืดหรือกระจก" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**มืด (แบบเกม)** ใช้แผงของเกมโดยตรง\n" +
                    "- เข้ากับ Legacy UI หรือ Modern UI อัตโนมัติ\n" +
                    "- ใช้ค่าความทึบของ Interface ในเกม\n" +
                    "\n" +
                    "**กระจก (กำหนดเอง)** ใช้แผงกระจกสีอ่อนของ Hover Colors\n" +
                    "- แม้ 100% ก็ยังเห็นเมืองด้านหลังเล็กน้อย\n" +
                    "- เพิ่มสไลเดอร์ความทึบของแผง\n" +
                    "\n" +
                    "ลองทั้งสองแบบแล้วเลือกที่ชอบ! เปลี่ยนเฉพาะพื้นหลังของแผงม็อดนี้\n" +
                    "\n" +
                    "เคล็ดลับ: เกมจะเบลอภาพหลังแผง ตั้งค่าความโปร่งใส Interface เป็น 0% จะปิดเบลอ ส่วน 1% ขึ้นไปจะยังเปิดอยู่"
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "มืด (แบบเกม)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "กระจก (กำหนดเอง)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ ตำแหน่งปุ่มแผง" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "เลือกซ้ายบน ขวาบน หรือ Universal Mod Menu\n" +
                    "**รีสตาร์ตเกม** เพื่อให้ตำแหน่งใหม่มีผล"
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "ซ้ายบน" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "ขวาบน" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ ความทึบของแผง" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "ความทึบพื้นหลังของแผง **กระจก (กำหนดเอง)**\n" +
                    "\n" +
                    "**30%** = โปร่งใสที่สุด\n" +
                    "**100%** = เกือบทึบ แต่ยังเห็นเมืองเล็กน้อย\n" +
                    "\n" +
                    "เปลี่ยนเฉพาะพื้นหลัง ข้อความ ไอคอน และสียังอ่านง่าย\n" +
                    "\n" +
                    "**มืด (แบบเกม)** ใช้ค่าความทึบ Interface ของเกม จึงซ่อนสไลเดอร์นี้"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ แสดงทูลทิป (แนะนำ)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "ผู้เล่นส่วนใหญ่ควร<เปิดไว้>\n" +
                    "แสดงคำอธิบายสั้น ๆ เมื่อชี้เมาส์ที่ปุ่ม Hover Colors\n" +
                    "ถ้าปิด ให้คลิก Info (i) บนแถบหัวเรื่องหรือเปิดตัวเลือกนี้อีกครั้ง\n" +
                    "ทูลทิปจะปิดได้เฉพาะในเมนู Options นี้"
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "รีเซ็ตเป็นค่าเริ่มต้นของม็อด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "คืนค่า Hover Colors ทั้งหมดเหมือนติดตั้งใหม่: สี ความหนาเส้นขอบ สีเครื่องมือ ไกด์ แผง และทูลทิป\n" +
                    "\n" +
                    "**พรีเซ็ตที่บันทึกไว้ (Set A และ Set B) จะถูกลบด้วย**\n" +
                    "\n" +
                    "ปุ่มลัดไม่เปลี่ยน\n" +
                    "เหมือนติดตั้ง Hover Colors ครั้งแรก"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "รีเซ็ตค่า Hover Colors ทั้งหมดหรือไม่?\n" +
                    "\n" +
                    "พรีเซ็ตที่บันทึกไว้ (Set A และ Set B) จะถูกลบ"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "รีเซ็ตสีเป็น vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "คืนหน้าตาเดิมของเกม: เส้นขอบ ไฮไลต์เจ้าของ สีเติม ความหนา ไกด์ และเขต\n" +
                    "\n" +
                    "ค่าอื่นคงเดิม: Bulldozer/ถนน พรีวิว พรีเซ็ต แผง และปุ่มลัด\n" +
                    "\n" +
                    "ลบม็อดได้โดยไม่ต้องรีเซ็ต ไฮไลต์จะกลับเป็นค่าเกมเอง\n" +
                    "ปุ่มนี้เป็นแค่รีเซ็ตสีแบบเร็ว"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "คืนสีที่ม็อดควบคุมเป็นหน้าตาเดิมของเกมหรือไม่?\n" +
                    "\n" +
                    "พรีเซ็ต การทำงานของเครื่องมือ และตัวเลือกแผงจะยังอยู่"
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "เปิด/ปิดแผงหลัก" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "ปุ่มลัดสำหรับ<เปิด / ปิด>แผงสีในเมือง"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "สลับแผง Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "ตาด่วน เปิด/ปิด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "ปุ่มลัดเสริมสำหรับปุ่มรูปตา: เปิด/ปิด Highlight + Fill ทันที\n" +
                    "ไม่ได้กำหนดปุ่มไว้เพื่อเลี่ยงปุ่มชนกัน"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "ตาด่วน เปิด/ปิด" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "ทูลทิปมุม เปิด/ปิด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "ปุ่มลัดสำหรับกล่องมุมและกล่อง วาง/เลิกทำ\n" +
                    "ใช้ขณะวาดถนนหรือทางเดินใหม่\n" +
                    "ราคา ความยาว และความชันยังแสดงอยู่"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "มุมและคำแนะนำเมาส์ เปิด/ปิด" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "สลับพรีเซ็ต 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "ปุ่มลัดเพื่อสลับระหว่าง\n" +
                    "<พรีเซ็ต 1 และพรีเซ็ต 2>"
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "สลับระหว่างพรีเซ็ต 1 และ 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "ม็อด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "เวอร์ชัน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods ของ Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**เปิดหน้า Paradox Mods ของผู้สร้าง**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "ด้วยรักและคิดถึง Mochi"
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "ม็อดนี้อุทิศให้ Mochi\n" +
                    "เธอเป็นน้องหมาที่รักมาก รับมาเลี้ยงตอนอายุ 7 ปี\n" +
                    "และมอบความรักกับความสุขให้ 13 ปี\n" +
                    "ถ้าไม่มี Mochi ม็อดนี้ก็คงไม่มีวันนี้"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
