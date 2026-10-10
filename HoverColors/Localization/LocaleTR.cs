// <copyright file="LocaleTR.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
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
            return new Dictionary<string, string>
            {
                // Mod title in the left rail of the Options menu.
                { m_Settings.GetSettingsLocaleID(), Mod.ModName },

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
                    "**1. Önerilen** yıkımda sarı Uyarı rengini kullanır. Yol/patika aracı açıkken yeni önizlemelerde ve araç altındaki segmentlerde vanilla maviyi yumuşatır. Mevcut yollara normal şekilde gelince yine kendi Dış Çizgi rengin kullanılır. Yeni yolun geniş dolgusu Kılavuzlar önizleme rengini izler.\n" +
                    "**2. Vanilla araç renkleri** bulldozer veya yol araçlarında normal vanilla maviyi geri getirir.\n" +
                    "**3. Özel rengimi koru** seçtiğin rengi her yerde kullanır.\n" +
                    "\n" +
                    "Rengin yıkım sırasında zor görünüyorsa işe yarar.\n" +
                    "Ayrı yol izi kılavuzu da Kılavuzlar önizleme rengini izler.\n" +
                    "Renk seçicide kayıtlı özel rengin değiştirilmez."
                },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Önerilen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla araç renkleri" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Özel rengimi koru" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "▪ Çakışan öğe dış çizgisini etkinleştir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)),
                    "<Etkin önerilir>\n" +
                    "Nesne veya ağ yerleşimi çakışma yüzünden engellenince vanilla somon kırmızı dış çizgiyi gösterir.\n" +
                    "Özel Sanayi çiftlik yarıçapı gibi alan sınırlarına dokunmaz.\n" +
                    "\n" +
                    "Tüm Bulldozer + yollar modlarında çalışır ve kayıtlı rengini değiştirmez."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "▪ NetLanes için özel renklere izin ver" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)),
                    "<Etkin önerilir>\n" +
                    "Çit, çalı, işaretleme gibi NetLane detaylarında kayıtlı HC renk/şeffaflığını kullanır.\n" +
                    "\n" +
                    "- Normal yollar Bulldozer + yollar ayarını izlemeye devam eder.\n" +
                    "- Bu araçlarda vanilla mavi istiyorsan kapat.\n" +
                    "- Çakışma hata rengi yine önceliklidir (vanilla = somon kırmızı)."
                },

                // Panel style
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelStyle)), "▪ Koyu veya cam panel seç" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelStyle)),
                    "**Koyu (Vanilla)** oyunun kendi panelini kullanır.\n" +
                    "- Legacy UI veya Modern UI görünümüne otomatik uyar.\n" +
                    "- Oyunun Arayüz Opaklığı ayarını izler.\n" +
                    "\n" +
                    "**Cam (Özel)** daha açık Hover Colors cam panelini kullanır.\n" +
                    "- %100’de bile şehir biraz görünür.\n" +
                    "- Panel opaklığı kaydırıcısı ekler.\n" +
                    "\n" +
                    "İkisini de dene! Yalnızca bu mod panelinin arka planı değişir.\n" +
                    "\n" +
                    "İpucu: oyun panellerin arkasını bulanıklaştırır. Arayüz Şeffaflığı %0 ise bulanıklık kapanır; %1 veya üstünde kalır."
                },
                { m_Settings.GetPanelStyleLocaleID("Dark"), "Koyu (Vanilla)" },
                { m_Settings.GetPanelStyleLocaleID("Glass"), "Cam (Özel)" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.LauncherLocation)), "▪ Panel düğmesinin konumu" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.LauncherLocation)),
                    "Sol üst, sağ üst veya Universal Mod Menu seç. Bu ayarı değiştirdiğinde düğme hemen yer değiştirir."
                },
                { m_Settings.GetLauncherLocationLocaleID("TopLeft"), "Sol üst" },
                { m_Settings.GetLauncherLocationLocaleID("TopRight"), "Sağ üst" },
                { m_Settings.GetLauncherLocationLocaleID("UniversalMenu"), "Universal Mod Menu" },

                // Panel opacity
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)), "▪ Panel opaklığı" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelOpacityPercent)),
                    "**Cam (Özel)** panelinin arka plan opaklığı.\n" +
                    "\n" +
                    "**%30** = en şeffaf.\n" +
                    "**%100** = neredeyse opak, şehir biraz görünür.\n" +
                    "\n" +
                    "Yalnızca arka plan değişir; metin, simgeler ve renk örnekleri okunaklı kalır.\n" +
                    "\n" +
                    "**Koyu (Vanilla)** oyunun Arayüz Opaklığını izler, bu yüzden bu kaydırıcı gizlenir."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "▪ İpuçlarını göster (önerilen)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)),
                    "Çoğu oyuncu için <açık bırakmak> önerilir.\n" +
                    "Hover Colors düğmelerinin üstüne gelince kısa yardım gösterir.\n" +
                    "Kapattıysan başlık çubuğundaki Info (i) veya bu seçenekle tekrar açabilirsin.\n" +
                    "İpuçları yalnızca bu Seçenekler menüsünden kapatılabilir."
                },

                // Reset buttons
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetModDefaults)), "Mod varsayılanlarına sıfırla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Tüm Hover Colors ayarlarını yeni kurulum durumuna döndürür: renkler, dış çizgi kalınlığı, araçlar, kılavuzlar, panel ve ipuçları.\n" +
                    "\n" +
                    "**Kayıtlı presetler (Set A ve Set B) de silinir.**\n" +
                    "\n" +
                    "Kısayollar değişmez.\n" +
                    "Hover Colors ilk kez kurulmuş gibi olur."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetModDefaults)),
                    "Tüm Hover Colors ayarları sıfırlansın mı?\n" +
                    "\n" +
                    "Kayıtlı presetler (Set A ve Set B) silinecek."
                },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)), "Renkleri vanilla olarak sıfırla" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Oyunun kendi görünümünü geri getirir: dış çizgi, sahip vurgusu, dolgu, kalınlık, kılavuzlar ve bölgeler.\n" +
                    "\n" +
                    "Diğer her şey aynı kalır: Bulldozer/yollar, araç önizlemeleri, presetler, panel ve kısayollar.\n" +
                    "\n" +
                    "Modu sıfırlamadan kaldırabilirsin; vurgular kendiliğinden oyun varsayılanlarına döner.\n" +
                    "Bu sadece hızlı renk sıfırlamasıdır."
                },
                { m_Settings.GetOptionWarningLocaleID(nameof(HoverColorsSettings.ResetColorsToVanilla)),
                    "Modun kontrol ettiği renkler oyun görünümüne dönsün mü?\n" +
                    "\n" +
                    "Presetler, araç davranışı ve panel seçenekleri korunur."
                },

                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ana paneli aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)),
                    "Şehir içi renk panelini <açmak / kapatmak> için kısayol."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Hover Colors panelini aç/kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Hızlı göz Aç/Kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),
                    "Göz düğmesi için isteğe bağlı kısayol: Vurgu + Dolguyu anında Aç/Kapat.\n" +
                    "Tuş çakışmalarını önlemek için varsayılan olarak atanmamıştır."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHighlightsActionName), "Hızlı göz Aç/Kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)), "Açı ipuçları Aç/Kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleRoadAngleTooltipsBinding)),
                    "Açı ve Yerleştir/Geri Al kutuları için kısayol.\n" +
                    "Yeni yol veya patika çizerken çalışır.\n" +
                    "Maliyet, uzunluk ve eğim görünür kalır."
                },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleRoadAngleTooltipsActionName), "Açı ve fare ipuçları Aç/Kapat" },

                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Preset 1+2 değiştir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)),
                    "Kısayol şunlar arasında geçer:\n" +
                    "<preset 1 ve preset 2>."
                },
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
                    "Bu mod Mochi’ye adanmıştır.\n" +
                    "7 yaşında sahiplenilen, çok sevilen bir köpekti\n" +
                    "ve 13 yıl sevgiyle mutluluk verdi.\n" +
                    "Mochi olmasaydı bu mod da olmazdı."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
