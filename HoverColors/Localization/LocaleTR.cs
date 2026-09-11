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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "▪ Bulldozer + yollar" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)),
                    "Bulldozer veya yol araçları aktifken geçici dış çizgi renklerini kontrol eder.\n" +
                    "\n" +
                    "**1. Önerilen** yıkımda oyunun Uyarı rengini (sarı), yollarda daha yumuşak vanilla maviyi kullanır.\n" +
                    "**2. Vanilla araç renkleri** bulldozer veya yol araçlarında oyunun normal vanilla mavisini geri yükler.\n" +
                    "**3. Özel rengimi koru** seçtiğin rengi her yerde kullanır.\n" +
                    "\n" +
                    "Amaç: bazı kullanıcı/testçiler yıkımda özel renklerini zor görüyor.\n" +
                    "Araç kullanırken daha görünür renkler sunar.\n" +
                    "Renk seçicide otomatik kaydedilen özel renginin üzerine yazmaz."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Önerilen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla araç renkleri" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Özel rengimi koru" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Çakışan öğe dış çizgisini etkinleştir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Etkin önerilir>\n" +
                    "Nesne veya ağ yerleşimi çakışma yüzünden engellenince oyunun vanilla somon kırmızı dış çizgisini görünür tutar.\n" +
                    "Özel Sanayi çiftlik yarıçapı kılavuzları gibi alan sınırlarına dokunmaz.\n" +
                    "\n" +
                    "Tüm Bulldozer + yollar modlarıyla çalışır ve kayıtlı özel renginin üzerine yazmaz."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ NetLanes için özel renklere izin ver" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Etkin önerilir>\n" +
                    "Çit, çalı, işaretleme ve benzeri NetLane detaylarını yerleştirirken kayıtlı HC renk/şeffaflığını kullanır.\n" +
                    "\n" +
                    "- Normal yollar listeden seçtiğin Bulldozer + yollar ayarını izlemeye devam eder.\n" +
                    "- Bu araçların oyunun vanilla mavi dış çizgisini kullanmasını istiyorsan kapat.\n" +
                    "- Etkinse çakışma hata rengi yine önceliklidir (vanilla hata rengi = somon kırmızı)."
                },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Panel opaklığı" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "Şehir içindeki panel arka planının ne kadar opak olduğunu ayarlar.\n" +
                    "\n" +
                    "**Daha düşük** değerler şehrin daha fazla görünmesini sağlar.\n" +
                    "**100%** tamamen opaktır.\n" +
                    "\n" +
                    "Yalnızca arka plan değişir. Metin, simgeler ve renk örnekleri her ayarda tamamen okunabilir kalır.\n" +
                    "\n" +
                    "**Kaydırıcı yalnızca Standart panelde çalışır.** Koyu panel oyunun kendi panel yüzeyini kullanır, bu yüzden oyunun Arayüz Opaklığı ayarını takip eder.\n" +
                    "Koyu panel Legacy veya Modern UI ile iyi çalışır. Standart panel Modern UI ile en iyi çalışır."
                },

                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "▪ Daha koyu panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)),
                    "Etkin = <Koyu panel>: Legacy UI için; daha koyu panel seviyorsan Modern UI'da da kullanılabilir.\n" +
                    "Kapalı = <Standart panel>: Hover Colors için özel yarı saydam stil.\n" +
                    "- Daha açık, daha modern görünüm.\n" +
                    "- Yeni Modern UI kullanan çoğu oyuncu için en iyisi.\n" +
                    "\n" +
                    "İkisini de dene. Bu yalnızca bu mod panelinin arka planını değiştirir, oyun UI'sını değil."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ İpuçlarını göster (önerilen)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "Çoğu oyuncu için <açık bırakmak> önerilir.\n" +
                    "Hover Colors düğmelerinin üstüne gelince kısa yardım gösterir.\n" +
                    "Kapattıysan başlık çubuğundaki Info (i) düğmesiyle veya bu seçenekle tekrar açabilirsin.\n" +
                    "Yanlışlıkla kapanmaması için ipuçları yalnızca bu Seçenekler menüsünden kapatılabilir."
                },


                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Mod varsayılanlarına sıfırla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Tüm Hover Colors ayarlarını yeni kurulum durumuna döndürür: renkler, dış çizgi kalınlığı, araç renkleri, kılavuzlar, panel ve ipuçları.\n" +
                    "\n" +
                    "**Kayıtlı presetlerin de (Set A ve Set B) silinir.**\n" +
                    "\n" +
                    "Kısayollar etkilenmez.\n" +
                    "Hover Colors ilk kez kurulmuş gibi olur."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Tüm Hover Colors ayarları yeni kurulum durumuna sıfırlansın mı?\n\nKayıtlı presetler (Set A ve Set B) silinecek." },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Renkleri vanilla olarak sıfırla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Oyunun kendi görünümünü geri getirir: dış çizgi, sahip vurgusu, dolgu, kalınlık, kılavuzlar ve bölgeler.\n" +
                    "\n" +
                    "Diğer her şey ayarladığın gibi kalır: Bulldozer/yollar, araç önizlemeleri, presetler, panel ve kısayollar.\n" +
                    "\n" +
                    "Not: mod sıfırlama yapmadan kaldırılabilir. Vurgular kendiliğinden oyun varsayılanlarına döner.\n"+
                    "Bu yalnızca oyun renk varsayılanlarına hızlı sıfırlama düğmesidir."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Modun kontrol ettiği renkler oyunun kendi görünümüne dönsün mü?\n\nPresetler, araç davranışı ve panel seçenekleri korunur." },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ana paneli aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Şehir içi renk panelini <açmak / kapatmak> için kısayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors panelini aç/kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Hızlı göz Aç/Kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Başlık çubuğundaki göz düğmesi için isteğe bağlı kısayol: Vurgu + Dolguyu anında Aç/Kapat.\n" +
                    "Tuş çakışmalarını önlemek için varsayılan olarak atanmamıştır." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Hızlı göz Aç/Kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Surface aracı önizlemeleri aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)),
                    "Yüzey yerleştirirken aktif Surface sınır önizleme çizgilerini <gizle veya göster> kısayolu." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Surface önizleme katmanı aç/kapat" },

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
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Mochi'nin Paradox Mods'u" },
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
