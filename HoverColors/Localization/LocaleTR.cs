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
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.Actions), "Eylemler" },
                { m_Settings.GetOptionTabLocaleID(HoverColorsSettings.About), "Hakkında" },
                // Groups
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kToolColors), "Araç renk davranışı" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kPanel), "Panel" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kKeyBindings), "Kısayol tuşları" },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kGuidelines), "Kılavuzlar" },
                // AboutInfo + AboutLinks intentionally have empty group headers.
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutInfo), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutLinks), string.Empty },
                { m_Settings.GetOptionGroupLocaleID(HoverColorsSettings.kAboutDedication), "İthaf" },
                // Tool color behavior
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldozer + Yollar" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToolColorMode)), "Buldozer veya yol araçları etkinken geçici dış çizgi renklerini kontrol eder.\n\n**1. Önerilen** yıkım için oyunun Uyarı rengini (sarı), yollar için daha yumuşak vanilla maviyi kullanır.\n**2. Vanilla araç renkleri** buldozer veya yol araçları etkinken oyunun normal vanilla mavisini geri getirir.\n**3. Özel rengimi koru** seçtiğin rengi her yerde kullanır.\n\nAmaç: bazı kullanıcılar/test edenler yıkım sırasında özel renklerini zor görüyor.\nBu seçenekler araç kullanımı sırasında daha görünür renkler sunar.\nRenk seçicide otomatik kaydedilen özel renginin üzerine yazmaz." },
                { m_Settings.GetToolColorModeLocaleID("Recommended"), "1. Önerilen" },
                { m_Settings.GetToolColorModeLocaleID("Vanilla"), "2. Vanilla araç renkleri" },
                { m_Settings.GetToolColorModeLocaleID("Custom"), "3. Özel rengimi koru" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "Çakışan öğe dış çizgisini etkinleştir" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseOverlapWarningColor)), "<Etkin önerilir>\nNesne veya ağ yerleşimi çakışan öğeler yüzünden engellendiğinde oyunun vanilla somon kırmızısı dış çizgisini görünür tutar.\nUzmanlaşmış Sanayi çiftlik yarıçapı kılavuzları gibi alan sınırlarına dokunulmaz.\n\nTüm Buldozer + Yollar modlarıyla çalışır ve kaydedilmiş özel renginin üzerine yazmaz." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "NetLanes için özel renklere izin ver" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseCustomColorsForNetLanes)), "<Etkin önerilir>\nÇit, çalı, işaretleme ve benzeri şerit tabanlı NetLane ayrıntılarını yerleştirirken kaydedilmiş Üzerine Gelme Renkleri rengini/şeffaflığını kullanır.\n\n- Normal yollar, açılır listeden seçtiğin Buldozer + Yollar ayarını izlemeye devam eder.\n- Bu araçların oyunun vanilla mavi dış çizgisini kullanmasını istiyorsan bunu kapat.\n- Çakışma hata rengi etkinse yine önceliklidir (vanilla hata rengi = somon kırmızısı)." },
                // Panel
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "Üzerine Gelme Renkleri ipuçları" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.PanelTooltipsEnabled)), "<Etkin> = Üzerine Gelme Renkleri yardım ipuçlarını gösterir (önerilen [x]).\n<Devre dışı> = bu modun ipuçlarını gizler.\nİpuçları yalnızca bu Seçenekler menüsünden kapatılabilir.\nAma şehirde tekrar açabilirsin: başlık çubuğundaki Info (i) düğmesine tıkla." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Daha koyu panel" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.UseDarkerPanel)), "Etkin = <Koyu panel>: eski UI oyuncuları için yapıldı; daha koyu panel seviyorsan Modern UI ile de kullanılabilir.\nDevre dışı = <Standart panel>: özel yarı saydam Üzerine Gelme Renkleri stili.\n- Daha hafif ve modern görünüm.\n- Yeni Modern oyun UI kullanan çoğu oyuncu için en iyisi.\n\nİkisini de dene ve hangisini sevdiğini seç. Bu sadece mod panelinin arka planını değiştirir, oyunun UI’sını değil." },
                // Guidelines opacity slider
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Kılavuz opaklığı (alfa)" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.GuidelineOpacityPercent)), "Kesikli hizalama kılavuzlarının opaklığını kontrol eder; yol, çit, prop vb. yerleştirirken kullanışlıdır.\n\n**100%** vanilla varsayılan görünümü korur.\n**Daha düşük** kılavuzları daha şeffaf yapar.\n**0%** tamamen gizler - <Önerilmez>.\n15% üzerinde kalman önerilir, yoksa ne olduğunu görmek zorlaşır.\nAynı kaydırıcı şehir mod panelinde de var. İkisi senkronize;\nbunu değiştirirsen şehirdeki de değişir." },
                // Keybinds
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Ana paneli aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePanelBinding)), "Şehir içi üzerine gelinen nesne renk panelini açıp/kapatmak için kısayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Üzerine Gelme Renkleri panelini aç/kapat" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Yüzey aracı önizlemelerini aç/kapat" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Yüzey yerleştirirken etkin Yüzey aracı sınır önizleme çizgilerini gizlemek veya geri getirmek için kısayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleSurfaceToolAreasActionName), "Yüzey aracı önizleme katmanı On/Off" },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Preset 1+2 arasında geç" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.TogglePresetBinding)), "Preset slot 1 ile slot 2 arasında geçmek için kısayol." },
                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePresetActionName), "Preset 1 ve 2 arasında geçiş yap" },
                // About name + version
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.NameText)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.NameText)), string.Empty },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.VersionText)), "Sürüm" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.VersionText)), string.Empty },
                // About Paradox Mods link button
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Paradox Mods" },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.OpenParadox)), "Yazarın Paradox Mods sayfasını aç." },
                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Mochi’nin sevgi dolu anısına." },
                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.MochiDedicationText)), "Bu mod Mochi’ye adanmıştır. 7 yaşında sahiplenilen çok sevilen bir köpekti,\nve 13 yıl sevgi ve neşe verdi. Bu mod Mochi olmadan mümkün olmazdı." },
            };
        }

        public void Unload()
        {
        }
    }
}
