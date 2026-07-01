// <copyright file="LocaleTR.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Localization/LocaleTR.cs
// Purpose: Turkish (tr-TR) strings for the Options Menu.
// Strings for the in-city cohtml panel live separately in L10n/lang/tr-TR.json.

namespace HoverColors.Localization
{
    using System.Collections.Generic;

    using Colossal;

    using HoverColors.Settings;

    public sealed class LocaleTR : IDictionarySource
    {
        private readonly HoverColorsSettings m_Settings;

        public LocaleTR(HoverColorsSettings settings)
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "İşlemler" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Hakkında" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Araç renkleri" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Kısayollar" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Kılavuzlar" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "İthaf" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldozer + yollar" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldozer veya yol araçları açıkken geçici dış çizgi renklerini ayarlar.\n\n**1. Önerilen**: yıkımda uyarı sarısı, yollarda daha yumuşak vanilla mavi.\n**2. Vanilla renkler**: buldozer/yol araçlarında oyunun normal mavisine döner.\n**3. Kendi rengimi koru**: seçtiğin rengi her yerde kullanır.\n\nKendi rengin yıkım sırasında zor görünüyorsa işe yarar. Kayıtlı rengin üzerine yazılmaz." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Önerilen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla renkler" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Kendi rengimi koru" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Çakışma dış çizgisi" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Önerilen: açık>\nBir nesne veya ağ çakışma yüzünden yerleştirilemediğinde oyunun vanilla somon kırmızısı dış çizgisini korur.\nÖzel sanayi çiftlik yarıçapı gibi alan sınırları değiştirilmez.\n\nTüm Buldozer + yollar modlarında çalışır ve kayıtlı rengini değiştirmez." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes için özel renk" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Önerilen: açık>\nÇit, çalı, işaretleme ve benzeri NetLane araçlarında kayıtlı HC renk/şeffaflığını kullanır.\n\n- Normal yollar yine Buldozer + yollar ayarını izler.\n- Bu araçların vanilla mavi kullanmasını istiyorsan kapat.\n- Çakışma hata rengi açıksa yine önceliklidir (vanilla somon kırmızısı)." },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Mod ipuçları" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Açık> = mod yardım ipuçlarını gösterir (önerilen [x]).\n<Kapalı> = bu modun ipuçlarını gizler.\nİpuçları sadece bu Seçenekler menüsünde kapatılabilir.\nŞehirde başlık çubuğundaki Info (i) düğmesiyle yeniden açabilirsin." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Daha koyu panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Açık = <Koyu panel>: eski UI için, ya da daha koyu seviyorsan.\nKapalı = <Standart panel>: modun yarı saydam stili.\n- Daha hafif ve modern görünür.\n- Yeni Modern UI kullanan çoğu oyuncu için daha iyi.\n\nİkisini de dene. Sadece mod panelinin arka planını değiştirir, oyunun UI’sini değil." },

                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Kılavuz opaklığı (alpha)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Yol, çit, props vb. yerleştirirken noktalı hizalama kılavuzlarının opaklığını ayarlar.\n\n**100%** oyun varsayılanı.\n**Daha düşük** daha şeffaf.\n**0%** hepsini gizler.\n15% üstünde kal, yoksa çizgiyi görmek zorlaşır.\nAynı kaydırıcı şehir panelinde de var; ikisi senkron kalır." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ana paneli aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Şehirdeki renk panelini <açmak / kapatmak> için kısayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Paneli değiştir" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface önizleme aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface yerleştirirken sınır önizleme çizgilerini <gizlemek veya göstermek> için kısayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface önizleme katmanı On/Off" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Preset 1+2 değiştir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "<Preset yuvası 1 ve 2> arasında\ngeçmek için kısayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Preset 1 ve 2 arasında geç" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Sürüm" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Yazarın Paradox Mods sayfasını açar.**" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mochi’nin sevgi dolu anısına." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Bu mod Mochi’ye adandı.\n7 yaşında sahiplenilen çok sevilen bir köpekti,\n13 yıl sevgi ve neşe verdi.\nMochi olmasaydı bu mod olmazdı." },
            };
        }

        public void Unload()
        {
        }
    }
}
