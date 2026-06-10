// File: Localization/LocaleTH.cs
// Purpose: Thai (th-TH) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/th-TH.json.

namespace HoverColors.Localization
{
    using Colossal;
    using HoverColors.Settings;
    using System.Collections.Generic;
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "à¸à¸²à¸£à¸—à¸³à¸‡à¸²à¸™" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "à¹€à¸à¸µà¹ˆà¸¢à¸§à¸à¸±à¸š" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "à¸žà¸¤à¸•à¸´à¸à¸£à¸£à¸¡à¸ªà¸µà¸‚à¸­à¸‡à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "à¹à¸œà¸‡" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "à¸›à¸¸à¹ˆà¸¡à¸¥à¸±à¸”" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "à¹€à¸ªà¹‰à¸™à¹„à¸à¸”à¹Œ" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "à¸„à¸³à¸­à¸¸à¸—à¸´à¸¨" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "à¸£à¸–à¸›à¸£à¸±à¸šà¸”à¸´à¸™ + à¸–à¸™à¸™" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "à¸„à¸§à¸šà¸„à¸¸à¸¡à¸ªà¸µà¹€à¸ªà¹‰à¸™à¸‚à¸­à¸šà¸Šà¸±à¹ˆà¸§à¸„à¸£à¸²à¸§à¸‚à¸“à¸°à¹ƒà¸Šà¹‰à¸£à¸–à¸›à¸£à¸±à¸šà¸”à¸´à¸™à¸«à¸£à¸·à¸­à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­à¸–à¸™à¸™\n\n**1. à¹à¸™à¸°à¸™à¸³** à¹ƒà¸Šà¹‰à¸ªà¸µà¹€à¸•à¸·à¸­à¸™à¸‚à¸­à¸‡à¹€à¸à¸¡ (à¸ªà¸µà¹€à¸«à¸¥à¸·à¸­à¸‡) à¸ªà¸³à¸«à¸£à¸±à¸šà¸à¸²à¸£à¸£à¸·à¹‰à¸­à¸–à¸­à¸™ à¹à¸¥à¸°à¹ƒà¸Šà¹‰à¸ªà¸µà¸™à¹‰à¸³à¹€à¸‡à¸´à¸™ vanilla à¸—à¸µà¹ˆà¸™à¸¸à¹ˆà¸¡à¸¥à¸‡à¸ªà¸³à¸«à¸£à¸±à¸šà¸–à¸™à¸™\n**2. à¸ªà¸µà¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­ vanilla** à¸„à¸·à¸™à¸„à¹ˆà¸²à¸ªà¸µà¸™à¹‰à¸³à¹€à¸‡à¸´à¸™ vanilla à¸›à¸à¸•à¸´à¸‚à¸­à¸‡à¹€à¸à¸¡à¹€à¸¡à¸·à¹ˆà¸­à¹ƒà¸Šà¹‰à¸£à¸–à¸›à¸£à¸±à¸šà¸”à¸´à¸™à¸«à¸£à¸·à¸­à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­à¸–à¸™à¸™\n**3. à¹ƒà¸Šà¹‰à¸ªà¸µà¸—à¸µà¹ˆà¸‰à¸±à¸™à¸à¸³à¸«à¸™à¸”à¹€à¸­à¸‡à¸•à¹ˆà¸­à¹„à¸›** à¹ƒà¸Šà¹‰à¸ªà¸µà¸—à¸µà¹ˆà¸„à¸¸à¸“à¹€à¸¥à¸·à¸­à¸à¹ƒà¸™à¸—à¸¸à¸à¸à¸£à¸“à¸µ\n\nà¸ˆà¸¸à¸”à¸›à¸£à¸°à¸ªà¸‡à¸„à¹Œ: à¸œà¸¹à¹‰à¹ƒà¸Šà¹‰/à¸œà¸¹à¹‰à¸—à¸”à¸ªà¸­à¸šà¸šà¸²à¸‡à¸„à¸™à¸¡à¸­à¸‡à¹€à¸«à¹‡à¸™à¸ªà¸µà¸—à¸µà¹ˆà¸•à¸±à¹‰à¸‡à¹€à¸­à¸‡à¹„à¸”à¹‰à¸¢à¸²à¸à¸‚à¸“à¸°à¸£à¸·à¹‰à¸­à¸–à¸­à¸™\nà¸•à¸±à¸§à¹€à¸¥à¸·à¸­à¸à¸™à¸µà¹‰à¹ƒà¸«à¹‰à¸ªà¸µà¸—à¸µà¹ˆà¸¡à¸­à¸‡à¹€à¸«à¹‡à¸™à¸Šà¸±à¸”à¸‚à¸¶à¹‰à¸™à¸‚à¸“à¸°à¹ƒà¸Šà¹‰à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­\nà¹„à¸¡à¹ˆà¹€à¸‚à¸µà¸¢à¸™à¸—à¸±à¸šà¸ªà¸µà¸—à¸µà¹ˆà¸à¸³à¸«à¸™à¸”à¹€à¸­à¸‡à¸‹à¸¶à¹ˆà¸‡à¸šà¸±à¸™à¸—à¸¶à¸à¸­à¸±à¸•à¹‚à¸™à¸¡à¸±à¸•à¸´à¹ƒà¸™à¸•à¸±à¸§à¹€à¸¥à¸·à¸­à¸à¸ªà¸µ" },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. à¹à¸™à¸°à¸™à¸³" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. à¸ªà¸µà¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­ vanilla" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. à¹ƒà¸Šà¹‰à¸ªà¸µà¸—à¸µà¹ˆà¸‰à¸±à¸™à¸à¸³à¸«à¸™à¸”à¹€à¸­à¸‡à¸•à¹ˆà¸­à¹„à¸›" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "à¹€à¸›à¸´à¸”à¹€à¸ªà¹‰à¸™à¸‚à¸­à¸šà¸£à¸²à¸¢à¸à¸²à¸£à¸—à¸µà¹ˆà¸—à¸±à¸šà¸‹à¹‰à¸­à¸™à¸à¸±à¸™" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<à¹à¸™à¸°à¸™à¸³à¹ƒà¸«à¹‰à¹€à¸›à¸´à¸”>\nà¸„à¸‡à¹€à¸ªà¹‰à¸™à¸‚à¸­à¸šà¸ªà¸µà¹à¸”à¸‡à¹à¸‹à¸¥à¸¡à¸­à¸™à¹à¸šà¸š vanilla à¸‚à¸­à¸‡à¹€à¸à¸¡à¹„à¸§à¹‰ à¹€à¸¡à¸·à¹ˆà¸­à¸à¸²à¸£à¸§à¸²à¸‡à¸§à¸±à¸•à¸–à¸¸à¸«à¸£à¸·à¸­à¹€à¸„à¸£à¸·à¸­à¸‚à¹ˆà¸²à¸¢à¸–à¸¹à¸à¸šà¸¥à¹‡à¸­à¸à¹‚à¸”à¸¢à¸£à¸²à¸¢à¸à¸²à¸£à¸—à¸µà¹ˆà¸—à¸±à¸šà¸‹à¹‰à¸­à¸™à¸à¸±à¸™\nà¸‚à¸­à¸šà¹€à¸‚à¸•à¸žà¸·à¹‰à¸™à¸—à¸µà¹ˆ à¹€à¸Šà¹ˆà¸™ à¹„à¸à¸”à¹Œà¸£à¸±à¸¨à¸¡à¸µà¸Ÿà¸²à¸£à¹Œà¸¡à¸­à¸¸à¸•à¸ªà¸²à¸«à¸à¸£à¸£à¸¡à¹€à¸‰à¸žà¸²à¸°à¸—à¸²à¸‡ à¸ˆà¸°à¹„à¸¡à¹ˆà¸–à¸¹à¸à¹€à¸›à¸¥à¸µà¹ˆà¸¢à¸™\n\nà¹ƒà¸Šà¹‰à¹„à¸”à¹‰à¸à¸±à¸šà¸—à¸¸à¸à¹‚à¸«à¸¡à¸”à¸£à¸–à¸›à¸£à¸±à¸šà¸”à¸´à¸™ + à¸–à¸™à¸™ à¹à¸¥à¸°à¹„à¸¡à¹ˆà¹€à¸‚à¸µà¸¢à¸™à¸—à¸±à¸šà¸ªà¸µà¸—à¸µà¹ˆà¸à¸³à¸«à¸™à¸”à¹€à¸­à¸‡à¸—à¸µà¹ˆà¸šà¸±à¸™à¸—à¸¶à¸à¹„à¸§à¹‰" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "à¸­à¸™à¸¸à¸à¸²à¸•à¸ªà¸µà¸—à¸µà¹ˆà¸à¸³à¸«à¸™à¸”à¹€à¸­à¸‡à¸ªà¸³à¸«à¸£à¸±à¸š NetLanes" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<à¹à¸™à¸°à¸™à¸³à¹ƒà¸«à¹‰à¹€à¸›à¸´à¸”>\nà¹ƒà¸Šà¹‰à¸ªà¸µ/à¸„à¸§à¸²à¸¡à¹‚à¸›à¸£à¹ˆà¸‡à¹ƒà¸ª HC à¸—à¸µà¹ˆà¸šà¸±à¸™à¸—à¸¶à¸à¹„à¸§à¹‰à¹€à¸¡à¸·à¹ˆà¸­à¸§à¸²à¸‡à¸£à¸²à¸¢à¸¥à¸°à¹€à¸­à¸µà¸¢à¸” NetLane à¹€à¸Šà¹ˆà¸™ à¸£à¸±à¹‰à¸§ à¹à¸™à¸§à¸žà¸¸à¹ˆà¸¡à¹„à¸¡à¹‰ à¹€à¸ªà¹‰à¸™à¸¡à¸²à¸£à¹Œà¸ à¹à¸¥à¸°à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­à¹à¸šà¸šà¹€à¸¥à¸™à¸—à¸µà¹ˆà¸„à¸¥à¹‰à¸²à¸¢à¸à¸±à¸™\n\n- à¸–à¸™à¸™à¸›à¸à¸•à¸´à¸¢à¸±à¸‡à¸„à¸‡à¸—à¸³à¸•à¸²à¸¡à¸à¸²à¸£à¸•à¸±à¹‰à¸‡à¸„à¹ˆà¸² à¸£à¸–à¸›à¸£à¸±à¸šà¸”à¸´à¸™ + à¸–à¸™à¸™ à¸—à¸µà¹ˆà¸„à¸¸à¸“à¹€à¸¥à¸·à¸­à¸à¸ˆà¸²à¸à¸£à¸²à¸¢à¸à¸²à¸£à¸”à¸£à¸­à¸›à¸”à¸²à¸§à¸™à¹Œ\n- à¸›à¸´à¸”à¸•à¸±à¸§à¹€à¸¥à¸·à¸­à¸à¸™à¸µà¹‰à¸«à¸²à¸à¸•à¹‰à¸­à¸‡à¸à¸²à¸£à¹ƒà¸«à¹‰à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­à¹€à¸«à¸¥à¹ˆà¸²à¸™à¸±à¹‰à¸™à¹ƒà¸Šà¹‰à¹€à¸ªà¹‰à¸™à¸‚à¸­à¸šà¸ªà¸µà¸™à¹‰à¸³à¹€à¸‡à¸´à¸™ vanilla à¸‚à¸­à¸‡à¹€à¸à¸¡à¹à¸—à¸™\n- à¸ªà¸µà¸‚à¹‰à¸­à¸œà¸´à¸”à¸žà¸¥à¸²à¸”à¸ˆà¸²à¸à¸à¸²à¸£à¸—à¸±à¸šà¸‹à¹‰à¸­à¸™à¸¢à¸±à¸‡à¸„à¸‡à¸¡à¸µà¸¥à¸³à¸”à¸±à¸šà¸„à¸§à¸²à¸¡à¸ªà¸³à¸„à¸±à¸à¹€à¸¡à¸·à¹ˆà¸­à¹€à¸›à¸´à¸”à¸­à¸¢à¸¹à¹ˆ (à¸ªà¸µà¸‚à¹‰à¸­à¸œà¸´à¸”à¸žà¸¥à¸²à¸” vanilla = à¸ªà¸µà¹à¸”à¸‡à¹à¸‹à¸¥à¸¡à¸­à¸™)" },

                // Darker panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "à¹à¸œà¸‡à¸ªà¸µà¹€à¸‚à¹‰à¸¡à¸‚à¸¶à¹‰à¸™" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "à¹€à¸¡à¸·à¹ˆà¸­à¹€à¸›à¸´à¸” à¸ˆà¸°à¹ƒà¸Šà¹‰ <à¹à¸œà¸‡à¹€à¸‚à¹‰à¸¡>: à¸—à¸³à¸¡à¸²à¸ªà¸³à¸«à¸£à¸±à¸šà¸œà¸¹à¹‰à¹€à¸¥à¹ˆà¸™ UI à¹à¸šà¸šà¹€à¸à¹ˆà¸² à¹à¸¥à¸°à¹ƒà¸Šà¹‰à¹ƒà¸™ Modern UI à¹„à¸”à¹‰à¸«à¸²à¸à¸„à¸¸à¸“à¸Šà¸­à¸šà¹à¸œà¸‡à¸—à¸µà¹ˆà¹€à¸‚à¹‰à¸¡à¸à¸§à¹ˆà¸²\nà¹€à¸¡à¸·à¹ˆà¸­à¸›à¸´à¸” à¸ˆà¸°à¹ƒà¸Šà¹‰ <à¹à¸œà¸‡à¸¡à¸²à¸•à¸£à¸à¸²à¸™>: à¸ªà¹„à¸•à¸¥à¹Œ Hover Colors à¹à¸šà¸šà¹‚à¸›à¸£à¹ˆà¸‡à¹à¸ªà¸‡à¸—à¸µà¹ˆà¸à¸³à¸«à¸™à¸”à¹€à¸­à¸‡\n- à¸”à¸¹à¸ªà¸§à¹ˆà¸²à¸‡à¹à¸¥à¸°à¸—à¸±à¸™à¸ªà¸¡à¸±à¸¢à¸à¸§à¹ˆà¸²\n- à¹€à¸«à¸¡à¸²à¸°à¸à¸±à¸šà¸œà¸¹à¹‰à¹€à¸¥à¹ˆà¸™à¸ªà¹ˆà¸§à¸™à¹ƒà¸«à¸à¹ˆà¸—à¸µà¹ˆà¹ƒà¸Šà¹‰ UI à¹€à¸à¸¡à¹à¸šà¸š Modern à¹ƒà¸«à¸¡à¹ˆ\n\nà¸¥à¸­à¸‡à¸—à¸±à¹‰à¸‡à¸ªà¸­à¸‡à¹à¸šà¸šà¹à¸¥à¹‰à¸§à¹€à¸¥à¸·à¸­à¸à¹à¸šà¸šà¸—à¸µà¹ˆà¸Šà¸­à¸š! à¸•à¸±à¸§à¹€à¸¥à¸·à¸­à¸à¸™à¸µà¹‰à¹€à¸›à¸¥à¸µà¹ˆà¸¢à¸™à¹€à¸‰à¸žà¸²à¸°à¸žà¸·à¹‰à¸™à¸«à¸¥à¸±à¸‡à¸‚à¸­à¸‡à¹à¸œà¸‡à¸¡à¸­à¸”à¸™à¸µà¹‰ à¹„à¸¡à¹ˆà¹ƒà¸Šà¹ˆ UI à¸‚à¸­à¸‡à¹€à¸à¸¡" },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "à¸„à¸§à¸²à¸¡à¸—à¸¶à¸šà¸‚à¸­à¸‡à¹€à¸ªà¹‰à¸™à¹„à¸à¸”à¹Œ (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "à¸„à¸§à¸šà¸„à¸¸à¸¡à¸„à¸§à¸²à¸¡à¸—à¸¶à¸šà¸‚à¸­à¸‡à¹„à¸à¸”à¹Œà¸ˆà¸±à¸”à¹à¸™à¸§à¹à¸šà¸šà¸›à¸£à¸° à¸¡à¸µà¸›à¸£à¸°à¹‚à¸¢à¸Šà¸™à¹Œà¸‚à¸“à¸°à¸§à¸²à¸‡à¸–à¸™à¸™ à¸£à¸±à¹‰à¸§ à¸žà¸£à¹‡à¸­à¸› à¸¯à¸¥à¸¯\n\n**100%** à¸„à¸‡à¸£à¸¹à¸›à¸¥à¸±à¸à¸©à¸“à¹Œ vanilla à¹€à¸£à¸´à¹ˆà¸¡à¸•à¹‰à¸™\n**à¸•à¹ˆà¸³à¸¥à¸‡** à¸—à¸³à¹ƒà¸«à¹‰à¹€à¸ªà¹‰à¸™à¹„à¸à¸”à¹Œà¹‚à¸›à¸£à¹ˆà¸‡à¹ƒà¸ªà¸¡à¸²à¸à¸‚à¸¶à¹‰à¸™\n**0%** à¸‹à¹ˆà¸­à¸™à¸—à¸±à¹‰à¸‡à¸«à¸¡à¸” - <à¹„à¸¡à¹ˆà¹à¸™à¸°à¸™à¸³>\nà¹à¸™à¸°à¸™à¸³à¹ƒà¸«à¹‰à¸­à¸¢à¸¹à¹ˆà¹€à¸«à¸™à¸·à¸­ 15% à¸¡à¸´à¸‰à¸°à¸™à¸±à¹‰à¸™à¸ˆà¸°à¸”à¸¹à¸¢à¸²à¸à¸§à¹ˆà¸²à¹€à¸à¸´à¸”à¸­à¸°à¹„à¸£à¸‚à¸¶à¹‰à¸™\nà¹à¸–à¸šà¹€à¸¥à¸·à¹ˆà¸­à¸™à¹€à¸”à¸µà¸¢à¸§à¸à¸±à¸™à¸­à¸¢à¸¹à¹ˆà¹ƒà¸™à¹à¸œà¸‡à¸¡à¸­à¸”à¹ƒà¸™à¹€à¸¡à¸·à¸­à¸‡ à¸—à¸±à¹‰à¸‡à¸ªà¸­à¸‡à¸‹à¸´à¸‡à¸„à¹Œà¸à¸±à¸™;\nà¸–à¹‰à¸²à¹€à¸›à¸¥à¸µà¹ˆà¸¢à¸™à¸­à¸±à¸™à¸™à¸µà¹‰ à¸­à¸±à¸™à¹ƒà¸™à¹€à¸¡à¸·à¸­à¸‡à¸à¹‡à¸ˆà¸°à¹€à¸›à¸¥à¸µà¹ˆà¸¢à¸™à¸•à¸²à¸¡" },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "à¹€à¸›à¸´à¸”/à¸›à¸´à¸”à¹à¸œà¸‡à¸«à¸¥à¸±à¸" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "à¸›à¸¸à¹ˆà¸¡à¸¥à¸±à¸”à¸ªà¸³à¸«à¸£à¸±à¸šà¹€à¸›à¸´à¸” / à¸›à¸´à¸”à¹à¸œà¸‡à¸ªà¸µà¸§à¸±à¸•à¸–à¸¸ Hover à¹ƒà¸™à¹€à¸¡à¸·à¸­à¸‡" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "à¸ªà¸¥à¸±à¸šà¹à¸œà¸‡ Hover Colors" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "à¸ªà¸¥à¸±à¸šà¸žà¸£à¸µà¸§à¸´à¸§à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­ Surface à¹€à¸›à¸´à¸”/à¸›à¸´à¸”" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "à¸›à¸¸à¹ˆà¸¡à¸¥à¸±à¸”à¸ªà¸³à¸«à¸£à¸±à¸šà¸‹à¹ˆà¸­à¸™à¸«à¸£à¸·à¸­à¸„à¸·à¸™à¹€à¸ªà¹‰à¸™à¸žà¸£à¸µà¸§à¸´à¸§à¸‚à¸­à¸šà¹€à¸‚à¸•à¸‚à¸­à¸‡à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­ Surface à¸‚à¸“à¸°à¸§à¸²à¸‡à¸žà¸·à¹‰à¸™à¸œà¸´à¸§" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "à¹€à¸¥à¹€à¸¢à¸­à¸£à¹Œà¸žà¸£à¸µà¸§à¸´à¸§à¹€à¸„à¸£à¸·à¹ˆà¸­à¸‡à¸¡à¸·à¸­ Surface On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "à¸ªà¸¥à¸±à¸šà¸žà¸£à¸µà¹€à¸‹à¹‡à¸• 1+2" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "à¸›à¸¸à¹ˆà¸¡à¸¥à¸±à¸”à¸ªà¸³à¸«à¸£à¸±à¸šà¸ªà¸¥à¸±à¸šà¸£à¸°à¸«à¸§à¹ˆà¸²à¸‡à¸Šà¹ˆà¸­à¸‡à¸žà¸£à¸µà¹€à¸‹à¹‡à¸• 1 à¹à¸¥à¸°à¸Šà¹ˆà¸­à¸‡ 2" },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "à¸ªà¸¥à¸±à¸šà¸£à¸°à¸«à¸§à¹ˆà¸²à¸‡à¸žà¸£à¸µà¹€à¸‹à¹‡à¸• 1 à¹à¸¥à¸° 2" },

                // About â€” name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About â€” Paradox Mods link button (matches CityWatchdog phrasing)
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "à¹€à¸›à¸´à¸”à¸«à¸™à¹‰à¸² Paradox Mods à¸‚à¸­à¸‡à¸œà¸¹à¹‰à¸ªà¸£à¹‰à¸²à¸‡" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "à¹à¸”à¹ˆà¸„à¸§à¸²à¸¡à¸—à¸£à¸‡à¸ˆà¸³à¸­à¸±à¸™à¹€à¸›à¸µà¹ˆà¸¢à¸¡à¸£à¸±à¸à¸‚à¸­à¸‡ Mochi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "à¸¡à¸­à¸”à¸™à¸µà¹‰à¸­à¸¸à¸—à¸´à¸¨à¹ƒà¸«à¹‰ Mochi à¹€à¸˜à¸­à¹€à¸›à¹‡à¸™à¸™à¹‰à¸­à¸‡à¸«à¸¡à¸²à¸—à¸µà¹ˆà¸£à¸±à¸à¸¡à¸²à¸ à¸£à¸±à¸šà¸¡à¸²à¹€à¸¥à¸µà¹‰à¸¢à¸‡à¸•à¸­à¸™à¸­à¸²à¸¢à¸¸ 7 à¸›à¸µ\nà¹à¸¥à¸°à¸¡à¸­à¸šà¸„à¸§à¸²à¸¡à¸£à¸±à¸à¸à¸±à¸šà¸„à¸§à¸²à¸¡à¸ªà¸¸à¸‚à¹ƒà¸«à¹‰à¸–à¸¶à¸‡ 13 à¸›à¸µ à¸¡à¸­à¸”à¸™à¸µà¹‰à¸„à¸‡à¹€à¸à¸´à¸”à¸‚à¸¶à¹‰à¸™à¹„à¸¡à¹ˆà¹„à¸”à¹‰à¸«à¸²à¸à¹„à¸¡à¹ˆà¸¡à¸µ Mochi" },
            };
        }

        public void Unload()
        {
        }
    }
}
