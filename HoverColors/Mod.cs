// <copyright file="Mod.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Mod.cs
// Purpose: Mod entrypoint; registers settings, localization, systems, keybindings, and the dedicated mod log.

namespace HoverColors
{
    using System;
    using System.Reflection;
    using Colossal.IO.AssetDatabase;
    using Colossal.Localization;
    using Colossal.Logging;
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Modding;
    using Game.SceneFlow;
    using HoverColors.Systems;
    using HoverColors.UI;
    using Unity.Entities;

    public sealed class Mod : IMod
    {
        public const string ModName = "Hover Colors";
        public const string ModId = "HoverColors";
        public const string ModTag = "[HC]";

        internal const string kTogglePanelActionName = "TogglePanel";
        internal const string kToggleSurfaceToolAreasActionName = "ToggleSurfaceToolAreas";
        internal const string kTogglePresetActionName = "TogglePreset";

        internal const string kToggleHighlightsActionName = "ToggleHoverHighlights";

        public static readonly string ModVersion =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";

        private static bool s_BannerLogged;

        public static readonly ILog s_Log =
            LogManager.GetLogger(ModId).SetShowsErrorsInUI(false);

        public static HoverColorsSettings? Settings { get; private set; }

        public void OnLoad(UpdateSystem updateSystem)
        {
            LogUtils.Configure(ModId, s_Log);

            if (!s_BannerLogged)
            {
                s_BannerLogged = true;

#if DEBUG
                LogUtils.Info($"{ModName} v{ModVersion} DEBUG loaded");
#else
                LogUtils.Info($"{ModName} v{ModVersion} loaded");
#endif
            }

            if (GameManager.instance == null)
            {
                LogUtils.Warn($"{ModTag} GameManager.instance is null; {ModName} cannot initialize.");
                return;
            }

            HoverColorsSettings setting = new(this);
            Settings = setting;

            try
            {
                LocalizationManager? localizationManager = GameManager.instance.localizationManager;
                if (localizationManager == null)
                {
                    LogUtils.Warn($"{ModTag} LocalizationManager is null; locale sources were not registered.");
                }
                else
                {
                    // Register localization before Options UI reads setting labels.
                    localizationManager.AddSource("en-US", new LocaleEN(setting));
                    localizationManager.AddSource("fr-FR", new LocaleFR(setting));
                    localizationManager.AddSource("es-ES", new LocaleES(setting));
                    localizationManager.AddSource("de-DE", new LocaleDE(setting));
                    localizationManager.AddSource("it-IT", new LocaleIT(setting));
                    localizationManager.AddSource("ja-JP", new LocaleJA(setting));
                    localizationManager.AddSource("ko-KR", new LocaleKO(setting));
                    localizationManager.AddSource("pl-PL", new LocalePL(setting));
                    localizationManager.AddSource("pt-BR", new LocalePT_BR(setting));
                    localizationManager.AddSource("pt-PT", new LocalePT_PT(setting));
                    localizationManager.AddSource("zh-HANS", new LocaleZH_HANS(setting));
                    localizationManager.AddSource("zh-HANT", new LocaleZH_HANT(setting));
                    localizationManager.AddSource("th-TH", new LocaleTH(setting));
                    localizationManager.AddSource("vi-VN", new LocaleVI(setting));
                    localizationManager.AddSource("tr-TR", new LocaleTR(setting));
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ModTag} Localization registration failed: {ex.GetType().Name}: {ex.Message}", ex);
            }

            try
            {
                AssetDatabase.global.LoadSettings(ModId, setting, new HoverColorsSettings(this));
                setting.SanitizeAfterLoad();
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ModTag} Settings load failed: {ex.GetType().Name}: {ex.Message}", ex);
            }

            try
            {
                setting.RegisterInOptionsUI();
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ModTag} Options UI registration failed: {ex.GetType().Name}: {ex.Message}", ex);
            }

            try
            {
                setting.RegisterKeyBindings();
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ModTag} Keybinding registration failed: {ex.GetType().Name}: {ex.Message}", ex);
            }

            try
            {
                World world = World.DefaultGameObjectInjectionWorld;

                world.GetOrCreateSystemManaged<OutlineColorSystem>();
                updateSystem.UpdateAt<OutlineColorSystem>(SystemUpdatePhase.Rendering);

                world.GetOrCreateSystemManaged<GuidelineColorSystem>();
                updateSystem.UpdateAt<GuidelineColorSystem>(SystemUpdatePhase.Rendering);

                world.GetOrCreateSystemManaged<AreaToolOverlaySystem>();
                updateSystem.UpdateAt<AreaToolOverlaySystem>(SystemUpdatePhase.Rendering);

                world.GetOrCreateSystemManaged<DistrictColorSystem>();
                updateSystem.UpdateAt<DistrictColorSystem>(SystemUpdatePhase.Rendering);

                updateSystem.UpdateAt<HoverColorsUISystem>(SystemUpdatePhase.UIUpdate);
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ModTag} System scheduling failed: {ex.GetType().Name}: {ex.Message}", ex);
            }
        }

        public void OnDispose()
        {
            Settings?.UnregisterInOptionsUI();
            Settings = null;
        }
    }
}
