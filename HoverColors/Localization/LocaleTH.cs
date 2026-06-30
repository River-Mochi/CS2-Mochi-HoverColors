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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "เกี่ยวกับ" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "พฤติกรรมสีของเครื่องมือ" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "แผง" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "ปุ่มลัด" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "เส้นไกด์" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "อุทิศให้" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "บูลโดเซอร์ + ถนน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ควบคุมสีเส้นขอบชั่วคราวเมื่อใช้เครื่องมือบูลโดเซอร์หรือเครื่องมือถนน\n\n**1. แนะนำ** ใช้สีเตือนของเกม (เหลือง) สำหรับการรื้อถอน และใช้สีน้ำเงิน vanilla ที่นุ่มลงสำหรับถนน\n**2. สีเครื่องมือ vanilla** คืนค่าสีน้ำเงิน vanilla ปกติของเกมเมื่อใช้บูลโดเซอร์หรือถนน\n**3. ใช้สีที่ฉันกำหนดเอง** ใช้สีที่เลือกไว้ทุกที่\n\nจุดประสงค์: ผู้เล่น/ผู้ทดสอบบางคนมองสีที่กำหนดเองได้ยากตอนรื้อถอน\nตัวเลือกนี้ให้สีที่มองเห็นชัดขึ้นระหว่างใช้เครื่องมือ\nไม่เขียนทับสีที่บันทึกอัตโนมัติไว้ในตัวเลือกสี" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. แนะนำ" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. สีเครื่องมือ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ใช้สีที่ฉันกำหนดเอง" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "เปิดเส้นขอบของรายการที่ซ้อนทับ" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<แนะนำให้เปิด>\nคงเส้นขอบสีแดงแซลมอน vanilla ของเกมไว้ เมื่อการวางวัตถุหรือเครือข่ายถูกบล็อกเพราะมีของซ้อนทับ\nขอบเขตพื้นที่ เช่น ไกด์รัศมีฟาร์มของอุตสาหกรรมเฉพาะทาง จะไม่ถูกเปลี่ยน\n\nใช้ได้กับทุกโหมด บูลโดเซอร์ + ถนน และไม่เขียนทับสีที่บันทึกไว้" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "อนุญาตสีกำหนดเองสำหรับ NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<แนะนำให้เปิด>\nใช้สี/ความโปร่งใสที่บันทึกไว้ของสีเมื่อชี้เมาส์ขณะวางรายละเอียด NetLane เช่น รั้ว พุ่มไม้ เครื่องหมาย และเครื่องมือแบบเลนอื่น ๆ\n\n- ถนนปกติยังใช้การตั้งค่า บูลโดเซอร์ + ถนน ที่คุณเลือกจากรายการ\n- ปิดตัวเลือกนี้ถ้าต้องการให้เครื่องมือเหล่านั้นใช้เส้นขอบสีน้ำเงิน vanilla ของเกม\n- สีข้อผิดพลาดจากการซ้อนทับยังมีลำดับความสำคัญเมื่อเปิดอยู่ (สี vanilla = แดงแซลมอน)" },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "คำแนะนำของสีเมื่อชี้เมาส์" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<เปิด> = แสดงคำแนะนำช่วยเหลือของสีเมื่อชี้เมาส์ (แนะนำ [x])\n<ปิด> = ซ่อนคำแนะนำของม็อดนี้\nสามารถปิดคำแนะนำได้เฉพาะในเมนูตัวเลือกนี้เท่านั้น\nแต่คุณเปิดกลับในเมืองได้: คลิกปุ่ม Info (i) บนแถบชื่อ" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "แผงสีเข้มขึ้น" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "เปิด = <แผงสีเข้ม>: ทำมาสำหรับผู้เล่น UI แบบเก่า; ใช้กับ Modern UI ได้ถ้าชอบแผงเข้มกว่า\nปิด = <แผงมาตรฐาน>: สไตล์โปร่งแสงของสีเมื่อชี้เมาส์\n- ดูเบาและทันสมัยกว่า\n- เหมาะกับผู้เล่นส่วนใหญ่ที่ใช้ Modern UI ใหม่\n\nลองทั้งสองแบบแล้วเลือกที่ชอบ ตัวเลือกนี้เปลี่ยนเฉพาะพื้นหลังของแผงม็อด ไม่เปลี่ยน UI ของเกม" },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ความทึบของเส้นไกด์ (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ควบคุมความทึบของเส้นไกด์จัดแนวแบบเส้นประ มีประโยชน์ตอนวางถนน รั้ว พร็อพ ฯลฯ\n\n**100%** คงลุค vanilla เดิม\n**ต่ำลง** ทำให้เส้นไกด์โปร่งใสขึ้น\n**0%** ซ่อนทั้งหมด - <ไม่แนะนำ>\nแนะนำให้อยู่เหนือ 15% ไม่งั้นจะมองสิ่งที่เกิดขึ้นได้ยาก\nแถบเลื่อนเดียวกันอยู่ในแผงม็อดในเมืองด้วย ทั้งสองซิงก์กัน;\nถ้าเปลี่ยนอันนี้ อันในเมืองจะเปลี่ยนด้วย" },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "เปิด/ปิดแผงหลัก" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "ปุ่มลัดเพื่อเปิด/ปิดแผงสีวัตถุเมื่อชี้เมาส์ในเมือง" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "สลับแผงสีเมื่อชี้เมาส์" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "สลับตัวอย่างเครื่องมือ Surface เปิด/ปิด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "ปุ่มลัดเพื่อซ่อนหรือคืนเส้นตัวอย่างขอบเขตของเครื่องมือ Surface ระหว่างวางพื้นผิว" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "เลเยอร์ตัวอย่างเครื่องมือ Surface On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "สลับพรีเซ็ต 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "ปุ่มลัดเพื่อสลับระหว่างช่องพรีเซ็ต 1 และ 2" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "สลับระหว่างพรีเซ็ต 1 และ 2" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "ม็อด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "เวอร์ชัน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "เปิดหน้า Paradox Mods ของผู้สร้าง" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "ด้วยความรักและระลึกถึง Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "ม็อดนี้อุทิศให้ Mochi เธอเป็นสุนัขที่รักมาก รับเลี้ยงตอนอายุ 7 ปี\nและมอบความรักกับความสุขให้ 13 ปี ม็อดนี้คงเป็นไปไม่ได้หากไม่มี Mochi" },
            };
        }

        public void Unload()
        {
        }
    }
}
