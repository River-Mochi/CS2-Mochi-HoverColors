// <copyright file="Setting.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Settings/Setting.cs
// Purpose: Defines Hover Colors settings, persistent storage, and the Options UI surface.
// Note: the in-city panel color settings are intentionally NOT decorated for Options UI — they are
// persisted here, read by cs2/api bindings, and applied by Systems/HoverColorsUISystem.cs.

namespace HoverColors.Settings
{
    using Colossal.IO.AssetDatabase; // FileLocation
    using Game.Input;       // BindingKeyboard
    using Game.Modding;     // IMod
    using Game.Settings;    // ModSetting, attributes

    [FileLocation("ModsSettings/HoverColors/HoverColors")]
    [SettingsUITabOrder(Actions, KeyBindings, About)]
    [SettingsUIGroupOrder(kToolColors, kPanel, kReset, kKeyBindings, kAboutInfo, kAboutLinks, kAboutDedication)]
    [SettingsUIShowGroupName(kToolColors, kPanel, kReset, kKeyBindings, kAboutDedication)]
    public partial class HoverColorsSettings : ModSetting
    {
        // Tab IDs
        internal const string Actions = nameof(Actions);
        internal const string KeyBindings = nameof(KeyBindings);
        internal const string About = nameof(About);

        // Group IDs
        internal const string kToolColors = nameof(kToolColors);
        internal const string kPanel = nameof(kPanel);
        internal const string kReset = nameof(kReset);
        internal const string kKeyBindings = nameof(kKeyBindings);
        internal const string kAboutInfo = nameof(kAboutInfo);
        internal const string kAboutLinks = nameof(kAboutLinks);
        internal const string kAboutDedication = nameof(kAboutDedication);

        public const int kToolColorModeRecommended = 0;
        public const int kToolColorModeVanilla = 1;
        public const int kToolColorModeCustom = 2;

        public const int kGuidelineColorPresetVanilla = 0;
        public const int kGuidelineColorPresetCustom = 4;

        public const int kGuidelineDashedColorPresetVanilla = 0;
        public const int kGuidelineDashedColorPresetYellow = 1;
        public const int kGuidelineDashedColorPresetGreen = 2;
        // Value 3 used to be an old test color; now it is Mochi Blue.
        public const int kGuidelineDashedColorPresetMochiBlue = 3;
        public const int kGuidelineDashedColorPresetCyanBlue = 4;
        public const int kGuidelineDashedColorPresetCustom = 5;

        // Outline thickness is stored as a multiplier on the vanilla shader width, not as a raw
        // width, so 1.0 always means "whatever this game build's _OutlineWidth actually is".
        internal const float kMinOutlineThicknessScale = 0f;
        internal const float kDefaultOutlineThicknessScale = 1f;
        internal const float kMaxOutlineThicknessScale = 2f;

        // Centralized default for the guideline opacity slider.
        // Vanilla CS2 is 100; lower = more transparent. Keep TSX fallback bindings in sync.
        public const int kDefaultGuidelineOpacityPercent = 30;

        // Kept for old local test .coc files. The city icon now resets guidelines to mod defaults.
        public int GuidelineDefaultPercent { get; set; }

        private const string kAboutLinksRow = nameof(kAboutLinksRow);

        // Same Paradox URL pattern as CityWatchdog — lands on River-Mochi's author page filtered to CS2.
        private const string kUrlParadox =
            "https://mods.paradoxplaza.com/authors/River-mochi/cities_skylines_2?games=cities_skylines_2&orderBy=desc&sortBy=best&time=alltime";

        // -----------------------------------------------------------------------
        // In-city color-picker bindings (driven by Systems/HoverColorsUISystem)
        // These are data fields the cs2/api bindings read/write
        // and the OutlineColorSystem applies. Field layout after the post-alpha redesign:
        //   - OutlineR/G/B → regular hover outline/fill color
        //   - OwnerR/G/B   → parent/owner highlight color, e.g. main building while placing sub-buildings
        //   - OutlineA     → outline halo edge opacity  (material _OuterColor.a)
        //   - FillA        → fill overlay opacity inside the silhouette (material _InnerColor.a)
        //   - FillR/G/B    → fill tint inside the silhouette             (material _InnerColor.RGB)
        //   - OutlineThicknessScale → multiplier on the captured vanilla material _OutlineWidth
        // -----------------------------------------------------------------------

        [SettingsUIHidden]
        public float OutlineR { get; set; }

        [SettingsUIHidden]
        public float OutlineG { get; set; }

        [SettingsUIHidden]
        public float OutlineB { get; set; }

        [SettingsUIHidden]
        public float OutlineA { get; set; }

        [SettingsUIHidden]
        public float OwnerR { get; set; }

        [SettingsUIHidden]
        public float OwnerG { get; set; }

        [SettingsUIHidden]
        public float OwnerB { get; set; }

        [SettingsUIHidden]
        public float OwnerA { get; set; }

        [SettingsUIHidden]
        public float FillA { get; set; }

        // Fill tint. The outline material carries this as _InnerColor.RGB, which the fullscreen
        // outline pass combines with the per-object hover color, so white reproduces the old
        // behavior of the fill simply taking the Outline color.
        [SettingsUIHidden]
        public float FillR { get; set; }

        [SettingsUIHidden]
        public float FillG { get; set; }

        [SettingsUIHidden]
        public float FillB { get; set; }

        // Saves written before the fill tint existed have no FillR/G/B, so they deserialize as
        // black. This flag lets MigrateAfterLoad hand those saves white instead.
        [SettingsUIHidden]
        public bool FillColorInitialized { get; set; }

        [SettingsUIHidden]
        public float OutlineThicknessScale { get; set; }

        // 0 is a legitimate player choice here, so the migration needs its own marker rather than
        // treating an unset value as zero.
        [SettingsUIHidden]
        public bool OutlineThicknessInitialized { get; set; }

        // District overlay color. Disabled by default so we do not touch vanilla/other-mod
        // district prefabs until the player picks a color from the in-game District picker.
        [SettingsUIHidden]
        public bool DistrictColorEnabled { get; set; }

        [SettingsUIHidden]
        public float DistrictR { get; set; }

        [SettingsUIHidden]
        public float DistrictG { get; set; }

        [SettingsUIHidden]
        public float DistrictB { get; set; }

        [SettingsUIHidden]
        public float DistrictA { get; set; }

        // -----------------------------------------------------------------------
        // Player-editable preset slots (slots 1 + 2 on the in-city panel)
        // Not decorated for Options UI. Each slot stores the same values as the
        // live swatch (Outline RGBA + Fill RGBA). The panel's Save button overwrites a slot
        // with the current live color; the slot button applies it back. Persisted in the
        // .coc like the live values, so the in-city panel restores them after a reboot.
        // -----------------------------------------------------------------------

        [SettingsUIHidden]
        public float Preset1R { get; set; }

        [SettingsUIHidden]
        public float Preset1G { get; set; }

        [SettingsUIHidden]
        public float Preset1B { get; set; }

        [SettingsUIHidden]
        public float Preset1A { get; set; }

        [SettingsUIHidden]
        public float Preset1FillA { get; set; }

        [SettingsUIHidden]
        public float Preset1FillR { get; set; }

        [SettingsUIHidden]
        public float Preset1FillG { get; set; }

        [SettingsUIHidden]
        public float Preset1FillB { get; set; }

        [SettingsUIHidden]
        public float Preset2R { get; set; }

        [SettingsUIHidden]
        public float Preset2G { get; set; }

        [SettingsUIHidden]
        public float Preset2B { get; set; }

        [SettingsUIHidden]
        public float Preset2A { get; set; }

        [SettingsUIHidden]
        public float Preset2FillA { get; set; }

        [SettingsUIHidden]
        public float Preset2FillR { get; set; }

        [SettingsUIHidden]
        public float Preset2FillG { get; set; }

        [SettingsUIHidden]
        public float Preset2FillB { get; set; }

        // Four persisted preset slots shown as two buttons in the in-city panel.
        // ActivePresetSet 0 = Set A, 1 = Set B. The panel still shows P1/P2 only.
        // Save P1/P2 writes to the currently active set, so players can keep four colors.
        internal const int kPresetSetA = 0;
        internal const int kPresetSetB = 1;

        internal const float kPresetA1R = 215f / 255f, kPresetA1G = 226f / 255f, kPresetA1B = 194f / 255f;
        internal const float kPresetA1A = 0.67f, kPresetA1FillA = 0f;

        internal const float kPresetA2R = 140f / 255f, kPresetA2G = 140f / 255f, kPresetA2B = 171f / 255f;
        internal const float kPresetA2A = 0.5f, kPresetA2FillA = 0f;

        internal const float kPresetB1R = 121f / 255f, kPresetB1G = 155f / 255f, kPresetB1B = 156f / 255f;
        internal const float kPresetB1A = 0.70f, kPresetB1FillA = 0f;


        internal const float kPresetB2R = 0.25f, kPresetB2G = 0.15f, kPresetB2B = 0.25f;
        internal const float kPresetB2A = 0.5f, kPresetB2FillA = 0f;

        [SettingsUIHidden]
        public bool PresetSetsInitialized { get; set; }

        [SettingsUIHidden]
        public int ActivePresetSet { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1R { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1G { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1B { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1A { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1FillA { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1FillR { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1FillG { get; set; }

        [SettingsUIHidden]
        public float PresetAlt1FillB { get; set; }

        [SettingsUIHidden]
        public int PresetAlt1GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2R { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2G { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2B { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2A { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2FillA { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2FillR { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2FillG { get; set; }

        [SettingsUIHidden]
        public float PresetAlt2FillB { get; set; }

        [SettingsUIHidden]
        public int PresetAlt2GuidelinePercent { get; set; }


        // Non-destructive preset defaults toggle.
        // Reset temporarily shows the four mod default colors; second click restores this backup.
        [SettingsUIHidden]
        public bool PresetDefaultsToggleActive { get; set; }

        [SettingsUIHidden]
        public bool PresetDefaultsToggleHasBackup { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackupActiveSet { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1FillA { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1FillR { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1FillG { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1FillB { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackup1GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2FillA { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2FillR { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2FillG { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2FillB { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackup2GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1FillA { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1FillR { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1FillG { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1FillB { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackupAlt1GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2FillA { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2FillR { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2FillG { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2FillB { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackupAlt2GuidelinePercent { get; set; }



        // Guideline opacity saved per outline preset; guideline colors stay independent.
        [SettingsUIHidden]
        public int Preset1GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public int Preset2GuidelinePercent { get; set; }


        // Large guide circles/spacing lines (GuideLineSettingsData Low + VeryLow).
        // Alpha is independent from the dashed alignment guideline opacity slider.
        [SettingsUIHidden]
        public float GuidelineLinesR { get; set; }

        [SettingsUIHidden]
        public float GuidelineLinesG { get; set; }

        [SettingsUIHidden]
        public float GuidelineLinesB { get; set; }

        [SettingsUIHidden]
        public float GuidelineLinesA { get; set; }

        // Road/tool preview overlay body (GuideLineSettingsData Medium).
        [SettingsUIHidden]
        public float GuidelinePreviewR { get; set; }

        [SettingsUIHidden]
        public float GuidelinePreviewG { get; set; }

        [SettingsUIHidden]
        public float GuidelinePreviewB { get; set; }

        [SettingsUIHidden]
        public float GuidelinePreviewA { get; set; }

        // Dashed alignment guide line color (GuideLineSettingsData High).
        // Opacity stays controlled by GuidelineOpacityPercent.
        [SettingsUIHidden]
        public int GuidelineDashedColorPreset { get; set; }

        [SettingsUIHidden]
        public float GuidelineDashedR { get; set; }

        [SettingsUIHidden]
        public float GuidelineDashedG { get; set; }

        [SettingsUIHidden]
        public float GuidelineDashedB { get; set; }

        // Guidelines icon toggle backup. First click saves S1/S2/S3 + opacity and applies vanilla;
        // next click restores those values.
        [SettingsUIHidden]
        public bool GuidelineVanillaToggleActive { get; set; }

        [SettingsUIHidden]
        public bool GuidelineVanillaToggleHasBackup { get; set; }

        [SettingsUIHidden]
        public int GuidelineBackupLinesColorPreset { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupLinesR { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupLinesG { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupLinesB { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupLinesA { get; set; }

        [SettingsUIHidden]
        public int GuidelineBackupPreviewColorPreset { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupPreviewR { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupPreviewG { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupPreviewB { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupPreviewA { get; set; }

        [SettingsUIHidden]
        public int GuidelineBackupDashedColorPreset { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupDashedR { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupDashedG { get; set; }

        [SettingsUIHidden]
        public float GuidelineBackupDashedB { get; set; }

        [SettingsUIHidden]
        public int GuidelineBackupOpacityPercent { get; set; }

        // -----------------------------------------------------------------------
        // Actions tab — Tool color behavior
        // -----------------------------------------------------------------------
        // This controls temporary effective colors only. It never overwrites the
        // player's saved swatch color in ModsSettings/HoverColors/HoverColors.coc.

        [SettingsUIDropdown(typeof(HoverColorsSettings), nameof(GetToolColorModeItems))]
        [SettingsUISection(Actions, kToolColors)]
        public int ToolColorMode { get; set; }

        [SettingsUISection(Actions, kToolColors)]
        public bool UseOverlapWarningColor { get; set; }

        // NetLanes cover EDT-style fences/hedges/markings through NetTool.
        // ON lets those detail tools use custom HC colors while normal roads keep road overrides.
        [SettingsUISection(Actions, kToolColors)]
        public bool UseCustomColorsForNetLanes { get; set; }

        // -----------------------------------------------------------------------
        // Actions tab — Panel readability and help
        // -----------------------------------------------------------------------
        // Options UI lists a group in declaration order, so Darker panel is declared first to
        // put it above the tooltips toggle.

        // User-facing label is "Darker panel". LegacyUI's extra transparency exposed
        // the need for this, but Modern UI players can use it too if they prefer
        // stronger panel contrast.
        [SettingsUISection(Actions, kPanel)]
        public bool UseDarkerPanel { get; set; }

        // PanelTooltipsEnabled is player-facing now so new players do not accidentally
        // lose tooltip help from a title-bar button. The city info icon can only turn it back ON.
        [SettingsUISection(Actions, kPanel)]
        public bool PanelTooltipsEnabled { get; set; }

        // Hidden in-city preference: collapses panel to outline row only.
        // Title-bar arrow toggle (no Options UI setting for it)
        [SettingsUIHidden]
        public bool PanelCollapsed { get; set; }

        // Eye button state. Keeps player's saved color/alpha untouched while normal hover is hidden.
        [SettingsUIHidden]
        public bool HoverHighlightsSuppressed { get; set; }

        // Hidden in-city preference for Surface tool button/hotkey.
        // Default ON because players mainly use this to see layered surfaces clearly.
        [SettingsUIHidden]
        public bool SurfaceToolAreasSuppressed { get; set; }

        // Hidden in-city preference for Specialized Industry area fill previews.
        // This is AreaTypeMask.Lots, must be handled with Surface in one system.
        [SettingsUIHidden]
        public bool SpecializedIndustryAreasSuppressed { get; set; }

        [SettingsUIHidden]
        public bool SpecializedIndustryAreasSuppressionInitialized { get; set; }

        // -----------------------------------------------------------------------
        // Guidelines (in-city panel only)
        // -----------------------------------------------------------------------
        // All guideline controls live on the city panel now, so none of these appear in Options.
        // They are still persisted here and still read by the cs2/api bindings.

        [SettingsUIHidden]
        public int GuidelineLinesColorPreset { get; set; }

        [SettingsUIHidden]
        public int GuidelinePreviewColorPreset { get; set; }

        [SettingsUIHidden]
        public int GuidelineOpacityPercent { get; set; }

        // -----------------------------------------------------------------------
        // Key Bindings tab
        // -----------------------------------------------------------------------

        [SettingsUISection(KeyBindings, kKeyBindings)]
        [SettingsUIKeyboardBinding(BindingKeyboard.J, Mod.kTogglePanelActionName)]
        public ProxyBinding TogglePanelBinding { get; set; }

        // Unbound keybind by default so HC cannot collide with other mod/game shortcut.
        [SettingsUISection(KeyBindings, kKeyBindings)]
        [SettingsUIKeyboardBinding(BindingKeyboard.None, Mod.kToggleHighlightsActionName)]
        public ProxyBinding ToggleHoverHighlightsBinding { get; set; }

        [SettingsUISection(KeyBindings, kKeyBindings)]
        [SettingsUIKeyboardBinding(BindingKeyboard.K, Mod.kTogglePresetActionName)]
        public ProxyBinding TogglePresetBinding { get; set; }

        [SettingsUISection(KeyBindings, kKeyBindings)]
        [SettingsUIKeyboardBinding(BindingKeyboard.L, Mod.kToggleSurfaceToolAreasActionName)]
        public ProxyBinding ToggleSurfaceToolAreasBinding { get; set; }

        // -----------------------------------------------------------------------
        // About tab
        // -----------------------------------------------------------------------

        [SettingsUISection(About, kAboutInfo)]
        public string NameText => Mod.ModName;

        [SettingsUISection(About, kAboutInfo)]
        public string VersionText =>
#if DEBUG
            Mod.ModVersion + " (DEBUG)";
#else
            Mod.ModVersion;
#endif

        [SettingsUIButtonGroup(kAboutLinksRow)]
        [SettingsUIButton]
        [SettingsUISection(About, kAboutLinks)]
        public bool OpenParadox
        {
            set
            {
                if (value)
                {
                    TryOpenUrl(kUrlParadox);
                }
            }
        }

        [SettingsUISection(About, kAboutDedication)]
        public string MochiDedicationText => string.Empty;

        // -----------------------------------------------------------------------
        // Construction
        // -----------------------------------------------------------------------

        public HoverColorsSettings(IMod mod) : base(mod)
        {
            SetDefaults();
        }
    }
}
