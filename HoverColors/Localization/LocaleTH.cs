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

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ เปิดเส้นขอบของวัตถุที่ซ้อนทับ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<แนะนำให้เปิด>\n" +
                    "คงเส้นขอบสีแดงแซลมอนของเกมไว้เมื่อวางวัตถุหรือเน็ตเวิร์กไม่ได้เพราะซ้อนทับ\n" +
                    "ขอบเขตพื้นที่ เช่น รัศมีฟาร์ม Specialized Industry จะไม่ถูกเปลี่ยน\n" +
                    "\n" +
                    "ใช้ได้กับทุกโหมด Bulldozer + ถนน และไม่เขียนทับสีที่คุณบันทึกไว้"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ อนุญาตสีเองสำหรับ NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<แนะนำให้เปิด>\n" +
                    "ใช้สี/ความโปร่งใส HC ที่บันทึกไว้เมื่อวางรายละเอียด NetLane เช่น รั้ว พุ่มไม้ เส้นมาร์ก และอื่น ๆ\n" +
                    "\n" +
                    "- ถนนปกติยังใช้ค่าที่เลือกในรายการ Bulldozer + ถนน\n" +
                    "- ปิดถ้าต้องการให้เครื่องมือเหล่านี้ใช้สีน้ำเงิน vanilla ของเกม\n" +
                    "- สีข้อผิดพลาดการซ้อนทับยังมีผลก่อนเสมอเมื่อเปิด (สี vanilla = แดงแซลมอน)"
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ แผงมืดขึ้น" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "เปิด = <แผงมืด>: ใช้สีแผงของเกมเอง จึงเข้ากับแผงแบบ vanilla ของเกม\n" +
                    "- ทึบเต็มที่ที่ 100%\n" +
                    "ปิด = <แผงมาตรฐาน>: สไตล์ Hover Colors ที่สว่างและโปร่งใสกว่า\n" +
                    "- ลุคแบบกระจก แม้ที่ 100% ก็ยังเห็นเมืองด้านหลังเล็กน้อย\n" +
                    "\n" +
                    "แผงทั้งสองแบบใช้สไลเดอร์ความทึบด้านล่าง และดูเหมือนกันทั้ง Modern UI และ Legacy UI\n" +
                    "\n" +
                    "ลองทั้งสองแบบแล้วเลือกที่ชอบ! เปลี่ยนเฉพาะพื้นหลังของแผงม็อดนี้ ไม่เปลี่ยน UI ของเกม"
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ ความทึบของแผง" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "ปรับความทึบของพื้นหลังแผงในเมือง\n" +
                    "\n" +
                    "**30% กระจก** โปร่งใสและดูโล่งที่สุด\n" +
                    "**100%** ทึบเต็มที่สำหรับแผงมืด และเกือบทึบเต็มที่สำหรับแผงมาตรฐาน\n" +
                    "\n" +
                    "เปลี่ยนเฉพาะพื้นหลัง ข้อความ ไอคอน และช่องสียังอ่านได้ชัดเจนทุกระดับ\n" +
                    "\n" +
                    "**ใช้ได้กับแผงทั้งสองแบบ** การตั้งค่าความทึบของ Interface ในเกมจะไม่ส่งผลกับแผงนี้อีกต่อไป"
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ แสดงทูลทิป (แนะนำ)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "แนะนำให้ผู้เล่นส่วนใหญ่<เปิดไว้>\n" +
                    "แสดงคำอธิบายสั้น ๆ เมื่อชี้เมาส์ที่ปุ่ม Hover Colors\n" +
                    "ถ้าปิดไว้ ให้คลิก Info (i) บนแถบหัวเรื่อง หรือเปิดจากช่องนี้อีกครั้ง\n" +
                    "เพื่อป้องกันการปิดโดยไม่ตั้งใจ ทูลทิปจะปิดได้เฉพาะในเมนู Options นี้"
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "รีเซ็ตเป็นค่าเริ่มต้นของม็อด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "คืนค่าทั้งหมดของ Hover Colors เหมือนติดตั้งใหม่: สี ความหนาเส้นขอบ สีเครื่องมือ ไกด์ แผง และทูลทิป\n" +
                    "\n" +
                    "**พรีเซ็ตที่บันทึกไว้ (Set A และ Set B) จะถูกลบด้วย**\n" +
                    "\n" +
                    "ปุ่มลัดจะไม่ถูกเปลี่ยน\n" +
                    "เหมือนติดตั้ง Hover Colors เป็นครั้งแรก"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "รีเซ็ตค่า Hover Colors ทั้งหมดเหมือนติดตั้งใหม่หรือไม่?\n\nพรีเซ็ตที่บันทึกไว้ (Set A และ Set B) จะถูกลบ" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "รีเซ็ตสีเป็น vanilla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "คืนหน้าตาเดิมของเกม: เส้นขอบ ไฮไลต์เจ้าของ สีเติม ความหนา ไกด์ และเขต\n" +
                    "\n" +
                    "ค่าอื่นคงเดิม: Bulldozer/ถนน พรีวิวเครื่องมือ พรีเซ็ต แผง และปุ่มลัด\n" +
                    "\n" +
                    "หมายเหตุ: ลบม็อดได้โดยไม่ต้องรีเซ็ต ไฮไลต์จะกลับเป็นค่าเกมเอง\n"+
                    "ปุ่มนี้เป็นเพียงรีเซ็ตด่วนกลับสู่สีเริ่มต้นของเกม"
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "รีเซ็ตสีที่ม็อดควบคุมกลับเป็นหน้าตาเดิมของเกมหรือไม่?\n\nพรีเซ็ต การทำงานของเครื่องมือ และตัวเลือกแผงจะยังอยู่" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "เปิด/ปิดแผงหลัก" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "ปุ่มลัดเพื่อ<เปิด / ปิด>แผงสีในเมือง" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "สลับแผง Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "ตาแบบด่วน เปิด/ปิด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "ปุ่มลัดเสริมสำหรับปุ่มรูปตา: เปิด/ปิด Highlight + Fill ทันที\n" +
                    "ค่าเริ่มต้นไม่ผูกปุ่ม เพื่อเลี่ยงปุ่มชนกัน" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "ตาแบบด่วน เปิด/ปิด" },

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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods ของ Mochi" },
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
