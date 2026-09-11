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

namespace HoverColors
{
    using System.Collections.Generic;

    using Colossal;

    public class LocaleTR : IDictionarySource
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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Eylemler" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.KeyBindings), "Kısayollar" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Hakkında" },

                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Araç rengi davranışı" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kReset), "Sıfırla" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Kısayollar" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "İthaf" },

                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Bulldozer + yollar" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Bulldozer veya yol araçları aktifken geçici dış çizgi renklerini kontrol eder.\n" +
                    "\n" +
                    "**1. Önerilen** yıkımda oyunun Uyarı rengini (sarı), yollarda daha yumuşak vanilla maviyi kullanır.\n" +
                    "**2. Vanilla araç renkleri** bulldozer veya yol araçlarında oyunun normal vanilla mavisini geri yükler.\n" +
                    "**3. Özel rengimi koru** seçtiğin rengi her yerde kullanır.\n" +
                    "\n" +
                    "Amaç: bazı kullanıcı/testçiler yıkımda özel renklerini zor görüyor.\n" +
                    "Araç kullanırken yüksek görünürlük renkleri sunar.\n" +
                    "Renk seçicide otomatik kaydedilen özel renginin üzerine yazmaz."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Önerilen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla araç renkleri" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Özel rengimi koru" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Çakışan öğe dış çizgisini etkinleştir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Etkin önerilir>\n" +
                    "Nesne veya ağ yerleşimi çakışma yüzünden engellenince oyunun vanilla somon kırmızı dış çizgisini görünür tutar.\n" +
                    "Özel Sanayi çiftlik yarıçapı kılavuzları gibi alan sınırlarına dokunmaz.\n" +
                    "\n" +
                    "Tüm Bulldozer + yollar modlarıyla çalışır ve kayıtlı özel renginin üzerine yazmaz."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes için özel renklere izin ver" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Etkin önerilir>\n" +
                    "Çit, çalı, işaretleme ve benzeri NetLane detaylarını yerleştirirken kayıtlı HC renk/şeffaflığını kullanır.\n" +
                    "\n" +
                    "- Normal yollar listeden seçtiğin Bulldozer + yollar ayarını izlemeye devam eder.\n" +
                    "- Bu araçların oyunun vanilla mavi dış çizgisini kullanmasını istiyorsan kapat.\n" +
                    "- Etkinse çakışma hata rengi yine önceliklidir (vanilla hata rengi = somon kırmızı)."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Üstüne gelme renk ipuçları" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "<Etkin> = üstüne gelme renk yardım ipuçlarını göster (önerilen [x]).\n" +
                    "<Kapalı> = bu modun ipuçlarını gizle.\n" +
                    "İpuçları yalnızca bu Seçenekler menüsünden kapatılabilir.\n" +
                    "Şehirde tekrar açabilirsin: başlık çubuğundaki Info (i) düğmesine tıkla."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Daha koyu panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Etkin = <Koyu panel>: LegacyUI oyuncuları için; Modern UI'da daha güçlü kontrast istersen de kullanılabilir.\n" +
                    "Kapalı = <Standart panel>: üstüne gelme renkleri için özel yarı saydam stil.\n" +
                    "- Daha açık, daha modern görünüm.\n" +
                    "- Yeni Modern UI kullanan çoğu oyuncu için en iyisi.\n" +
                    "\n" +
                    "İkisini de dene. Bu yalnızca bu mod panelinin arka planını değiştirir, oyun UI'sını değil."
                },

                // Reset button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)), "Oyun görsellerini varsayılana döndür" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)),
                    "Bu modun kontrol ettiği her görseli oyunun kendi haline döndürür: dış çizgi, sahip vurgusu, dolgu, dış çizgi kalınlığı, kılavuzlar, bölgeler ve araç önizleme alanları.\n" +
                    "\n" +
                    "Kayıtlı ön ayarların, kısayolların ve panel tercihlerin korunur.\n" +
                    "\n" +
                    "Modu kaldırmak için gerekli değildir. Denemelerden sonra sıfırdan başlamak içindir."
                },

                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetGameVisuals)),
                    "Bu modun kontrol ettiği her görsel oyunun kendi haline döndürülsün mü?\n\nÖn ayarlar, kısayollar ve panel ayarları korunur." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ana paneli aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Şehir içi renk panelini <açmak / kapatmak> için kısayol." },

                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Üstüne gelme renk panelini aç/kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface aracı önizlemeleri aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Yüzey yerleştirirken aktif Surface araç sınır önizleme çizgilerini <gizle veya göster> kısayolu." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface araç önizleme katmanı aç/kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Preset 1+2 değiştir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Kısayol şunlar arasında geçer:\n" +
                    "<preset yuvası 1 ve yuva 2>." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Preset 1 ve 2 arasında geç" },

                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Sürüm" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },

                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "**Yazarın Paradox Mods sayfasını aç.**" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Mochi'nin sevgi dolu anısına."
                },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)),
                    "Bu mod Mochi'ye adanmıştır.\n" +
                    "7 yaşında sahiplenilmiş, çok sevilen bir köpekti,\n" +
                    "ve 13 yıl sevgi ve mutluluk verdi.\n" +
                    "Mochi olmasaydı bu mod olmazdı."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
