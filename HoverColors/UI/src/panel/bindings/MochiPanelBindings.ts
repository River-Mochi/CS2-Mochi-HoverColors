// File: UI/src/panel/bindings/MochiPanelBindings.ts
// Purpose: Central binding names/defaults for the compact Hover Colors panel.

import { bindValue } from "cs2/api";

export const CHANNEL = "HoverColors";

// Hold time for saving preset slots. Increase if quick taps save too often.
export const PRESET_HOLD_MS = 700;

// Hold time for District reset. Normal click still opens the picker.
export const DISTRICT_RESET_HOLD_MS = 800;

// Body class used by MochiColorPickerPanel.global.scss while compact vanilla pickers are open.
export const COMPACT_PICKER_BODY_CLASS = "mochiCompactColorPickerOpen";

// Set while ANY panel picker is open. Used to lift the vanilla balloon layer above the panel.
export const PICKER_OPEN_BODY_CLASS = "mochiColorPickerOpen";

// Live color bindings. New-install fallback matches Set A / P1.
export const outlineR$ = bindValue<number>(CHANNEL, "OutlineR", 215 / 255);
export const outlineG$ = bindValue<number>(CHANNEL, "OutlineG", 226 / 255);
export const outlineB$ = bindValue<number>(CHANNEL, "OutlineB", 194 / 255);
export const outlineA$ = bindValue<number>(CHANNEL, "OutlineA", 0.67);

export const ownerR$ = bindValue<number>(CHANNEL, "OwnerR", 0.247);
export const ownerG$ = bindValue<number>(CHANNEL, "OwnerG", 0.981);
export const ownerB$ = bindValue<number>(CHANNEL, "OwnerB", 0.247);
export const ownerA$ = bindValue<number>(CHANNEL, "OwnerA", 0.702);

export const fillA$ = bindValue<number>(CHANNEL, "FillA", 0);

// Fill tint. White is neutral: the fill renders in the Outline color, as it did before the swatch.
export const fillR$ = bindValue<number>(CHANNEL, "FillR", 1);
export const fillG$ = bindValue<number>(CHANNEL, "FillG", 1);
export const fillB$ = bindValue<number>(CHANNEL, "FillB", 1);

// Multiplier on the game's captured vanilla outline width. 1 = vanilla on any build.
export const outlineThicknessScale$ = bindValue<number>(CHANNEL, "OutlineThicknessScale", 1);

export const districtR$ = bindValue<number>(CHANNEL, "DistrictR", 128 / 255);
export const districtG$ = bindValue<number>(CHANNEL, "DistrictG", 128 / 255);
export const districtB$ = bindValue<number>(CHANNEL, "DistrictB", 128 / 255);
export const districtA$ = bindValue<number>(CHANNEL, "DistrictA", 64 / 255);

export const guidelineLinesColorR$ = bindValue<number>(CHANNEL, "GuidelineLinesColorR", 0.7);
export const guidelineLinesColorG$ = bindValue<number>(CHANNEL, "GuidelineLinesColorG", 0.7);
export const guidelineLinesColorB$ = bindValue<number>(CHANNEL, "GuidelineLinesColorB", 1);
export const guidelineLinesColorA$ = bindValue<number>(CHANNEL, "GuidelineLinesColorA", 1);

export const guidelinePreviewColorR$ = bindValue<number>(CHANNEL, "GuidelinePreviewColorR", 0.7);
export const guidelinePreviewColorG$ = bindValue<number>(CHANNEL, "GuidelinePreviewColorG", 0.7);
export const guidelinePreviewColorB$ = bindValue<number>(CHANNEL, "GuidelinePreviewColorB", 1);
export const guidelinePreviewColorA$ = bindValue<number>(CHANNEL, "GuidelinePreviewColorA", 1);

// Dashed alignment guide line RGB. Opacity stays on guidelineOpacity$.
export const guidelineDashedColorR$ = bindValue<number>(CHANNEL, "GuidelineDashedColorR", 0.7);
export const guidelineDashedColorG$ = bindValue<number>(CHANNEL, "GuidelineDashedColorG", 0.7);
export const guidelineDashedColorB$ = bindValue<number>(CHANNEL, "GuidelineDashedColorB", 1);

export const guidelineOpacity$ = bindValue<number>(CHANNEL, "GuidelineOpacityPercent", 30);
export const panelOpen$ = bindValue<boolean>(CHANNEL, "PanelOpen", false);
export const panelTooltipsEnabled$ = bindValue<boolean>(CHANNEL, "PanelTooltipsEnabled", true);
export const panelCollapsed$ = bindValue<boolean>(CHANNEL, "PanelCollapsed", false);
export const hoverHighlightsSuppressed$ = bindValue<boolean>(CHANNEL, "HoverHighlightsSuppressed", false);

// Defaults to true so a fresh load never flashes the Standard glass panel before the real
// setting arrives. Dark is also the default panel style for new installs.
export const useDarkerPanel$ = bindValue<boolean>(CHANNEL, "UseDarkerPanel", true);

// Panel background alpha, 30-100 in steps of 5. Keep in sync with kDefaultPanelOpacityPercent.
export const panelOpacityPercent$ = bindValue<number>(CHANNEL, "PanelOpacityPercent", 80);
export const surfaceToolAreasSuppressed$ = bindValue<boolean>(CHANNEL, "SurfaceToolAreasSuppressed", true);
export const specializedIndustryAreasSuppressed$ = bindValue<boolean>(CHANNEL, "SpecializedIndustryAreasSuppressed", true);
export const vanillaOutlineActive$ = bindValue<boolean>(CHANNEL, "VanillaOutlineActive", false);

// Preset stored-color bindings. Set A is shown first.
export const preset1R$ = bindValue<number>(CHANNEL, "Preset1R", 215 / 255);
export const preset1G$ = bindValue<number>(CHANNEL, "Preset1G", 226 / 255);
export const preset1B$ = bindValue<number>(CHANNEL, "Preset1B", 194 / 255);
export const preset1A$ = bindValue<number>(CHANNEL, "Preset1A", 0.67);

export const preset2R$ = bindValue<number>(CHANNEL, "Preset2R", 140 / 255);
export const preset2G$ = bindValue<number>(CHANNEL, "Preset2G", 140 / 255);
export const preset2B$ = bindValue<number>(CHANNEL, "Preset2B", 171 / 255);
export const preset2A$ = bindValue<number>(CHANNEL, "Preset2A", 0.5);

export const preset1Active$ = bindValue<boolean>(CHANNEL, "Preset1Active", false);
export const preset2Active$ = bindValue<boolean>(CHANNEL, "Preset2Active", false);

export const AREA_MENU_NAME_TOKENS = ["SERVICES.NAMES[AREAS]", "SERVICES.NAME[AREAS]", "AREAS"];

export const DISTRICT_AREA_NAME_TOKENS = [
  "ASSETS.NAME[DISTRICT AREA]",
  "ASSETS.DESCRIPTION[DISTRICT AREA]",
  "DISTRICT AREA",
  "DISTRICT",
];

export type ToolbarEntity = { index: number; version: number };

export const sameEntity = (
  a: ToolbarEntity | null | undefined,
  b: ToolbarEntity | null | undefined,
) => a != null && b != null && a.index === b.index && a.version === b.version;
