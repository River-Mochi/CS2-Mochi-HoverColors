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
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "สีเครื่องมือ" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "แผง" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "ปุ่มลัด" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "เส้นไกด์" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "คำอุทิศ" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "รถปราบ + ถนน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "ตั้งค่าสีขอบชั่วคราวตอนใช้รถปราบหรือเครื่องมือถนน\n\n**1. แนะนำ**: ใช้สีเตือนสีเหลืองตอนรื้อ และสีน้ำเงิน vanilla ที่นุ่มขึ้นสำหรับถนน\n**2. สี vanilla**: ใช้สีน้ำเงินปกติของเกมตอนใช้รถปราบ/ถนน\n**3. ใช้สีของฉัน**: ใช้สีที่คุณเลือกทุกที่\n\nเหมาะเมื่อสีที่ตั้งเองมองยากตอนรื้อถอน สีที่บันทึกไว้จะไม่ถูกเขียนทับ" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. แนะนำ" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. สี vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. ใช้สีของฉัน" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "ขอบตอนวางทับกัน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<แนะนำให้เปิด>\nคงขอบสีแดงแซลมอนของเกมไว้ เมื่อวางวัตถุหรือเน็ตเวิร์กไม่ได้เพราะซ้อนทับกัน\nเส้นจำกัดพื้นที่ เช่น รัศมีฟาร์มอุตสาหกรรมเฉพาะทาง จะไม่ถูกเปลี่ยน\n\nใช้ได้กับทุกโหมด รถปราบ + ถนน และไม่เขียนทับสีที่บันทึกไว้" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "สีเองสำหรับ NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<แนะนำให้เปิด>\nใช้สี/ความโปร่งใส HC ที่บันทึกไว้กับรายละเอียด NetLane เช่น รั้ว พุ่มไม้ เส้นเครื่องหมาย และเครื่องมือคล้ายกัน\n\n- ถนนปกติยังใช้ค่ารถปราบ + ถนนที่เลือกไว้\n- ปิดถ้าต้องการใช้สีน้ำเงิน vanilla ของเกม\n- สี error จากการวางทับยังมีสิทธิ์ก่อนถ้าเปิดอยู่ (แดงแซลมอน vanilla)" },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "ทูลทิปของม็อด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<เปิด> = แสดงคำช่วยเหลือของม็อด (แนะนำ [x])\n<ปิด> = ซ่อนทูลทิปของม็อดนี้\nปิดได้เฉพาะในเมนู Options นี้\nในเมือง เปิดกลับได้ด้วยปุ่ม Info (i) บนแถบหัวข้อ" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "แผงสีเข้มขึ้น" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "เปิด = <แผงมืด> : สำหรับผู้เล่น UI เก่า หรือถ้าชอบแผงมืด\nปิด = <แผงมาตรฐาน> : สไตล์โปร่งแสงของม็อด\n- ดูเบาและทันสมัยกว่า\n- เหมาะกับ UI Modern ใหม่ส่วนใหญ่\n\nลองทั้งสองแบบได้ เปลี่ยนเฉพาะพื้นหลังแผงม็อด ไม่เปลี่ยน UI ของเกม" },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ความทึบเส้นไกด์ (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "ปรับความทึบของเส้นไกด์ประแบบประ ใช้ตอนวางถนน รั้ว props ฯลฯ\n\n**100%** ค่าเริ่มต้นเกม\n**ต่ำลง** โปร่งใสขึ้น\n**0%** ซ่อนทั้งหมด\nควรอยู่เหนือ 15% ไม่งั้นเส้นจะมองยาก\nสไลเดอร์เดียวกันอยู่ในแผงเมืองด้วย และซิงก์กัน" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "เปิด/ปิดแผงหลัก" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "ปุ่มลัดเพื่อ <เปิด / ปิด> แผงสีในเมือง" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "สลับแผง" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "พรีวิว Surface เปิด/ปิด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "ปุ่มลัดเพื่อ <ซ่อนหรือแสดง> เส้นขอบพรีวิวของเครื่องมือ Surface ตอนวางพื้นผิว" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "ชั้นพรีวิว Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "สลับพรีเซ็ต 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "ปุ่มลัดเพื่อสลับระหว่าง\n<ช่องพรีเซ็ต 1 และช่อง 2>" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "สลับระหว่างพรีเซ็ต 1 และ 2" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "ม็อด" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "เวอร์ชัน" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**เปิดหน้า Paradox Mods ของผู้สร้าง**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "แด่ความทรงจำของ Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "ม็อดนี้อุทิศให้ Mochi\nเธอเป็นน้องหมาที่รักมาก รับมาเลี้ยงตอนอายุ 7 ปี\nและมอบความรักกับความสุขให้ 13 ปี\nม็อดนี้คงเป็นไปไม่ได้ถ้าไม่มี Mochi" },
            };
        }

        public void Unload()
        {
        }
    }
}
