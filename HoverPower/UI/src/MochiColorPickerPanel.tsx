// File: UI/src/MochiColorPickerPanel.tsx
// Purpose: In-city panel anchored under the GameTopLeft icon button.
// Layout (current):
//   - Outline section:   vanilla ColorField (RGB + alpha + hex input + wheel popup)
//   - Fill section:      single MochiColorSlider for alpha only (Fill RGB sliders dropped — they
//                        didn't visibly do anything because we route Outline RGB to all surfaces)
//   - Guidelines section: single MochiColorSlider mirroring the Options-UI Guidelines opacity
//                         (both surfaces write to Settings.GuidelineOpacityPercent; either works)
//   - Presets row:       3 rectangular vanilla Buttons (Set1, Set2, Reset)
//
// State sync model:
//   useValue() on each bound channel reads live from C# (Settings.OutlineR etc.).
//   We keep a local React copy so preset clicks update the panel instantly without waiting on
//   the next binding tick.

import React from "react";
import { Button, Tooltip } from "cs2/ui";
import { Color } from "cs2/bindings";
import { bindValue, trigger, useValue } from "cs2/api";
import { VanillaResolver } from "./utils/VanilliaResolver";
import { MochiColorSlider } from "./utils/MochiColorSlider";

const CHANNEL = "HoverPower";

const outlineR$ = bindValue<number>(CHANNEL, "OutlineR", 0.502);
const outlineG$ = bindValue<number>(CHANNEL, "OutlineG", 0.869);
const outlineB$ = bindValue<number>(CHANNEL, "OutlineB", 1);
const outlineA$ = bindValue<number>(CHANNEL, "OutlineA", 0.855);
const fillA$ = bindValue<number>(CHANNEL, "FillA", 0);
const guidelineOpacity$ = bindValue<number>(CHANNEL, "GuidelineOpacityPercent", 100);

// -----------------------------------------------------------------------
// Presets (keep in sync with Settings/Setting.cs SetDefaults() for the Reset case)
// -----------------------------------------------------------------------

// Vanilla cyan-blue — exact values from the OutlinesWorldUIPass material defaults.
const PRESET_VANILLA_OUTLINE: Color = { r: 0.502, g: 0.869, b: 1, a: 0.855 };
const PRESET_VANILLA_FILL_A = 0;

// Set1: light gray with 10% halo alpha — a subtle ambient highlight.
const PRESET_LIGHT_GRAY_OUTLINE: Color = { r: 0.85, g: 0.85, b: 0.88, a: 0.10 };
const PRESET_LIGHT_GRAY_FILL_A = 0;

// Set2: purple-gray from yenyang's HighlightsAndGuidelinesTweaks POC.
// Yenyang's original m_HoveredColor was (0.25, 0.15, 0.25, 0.01). We keep the RGB and bump alpha
// to 0.50 so the halo is actually visible as a usable preset rather than near-invisible.
const PRESET_YENYANG_OUTLINE: Color = { r: 0.25, g: 0.15, b: 0.25, a: 0.5 };
const PRESET_YENYANG_FILL_A = 0;

export const MochiColorPickerPanel = () => {
    // Live values from C# settings via bindings.
    const boundOutline: Color = {
        r: useValue(outlineR$),
        g: useValue(outlineG$),
        b: useValue(outlineB$),
        a: useValue(outlineA$),
    };
    const boundFillA = useValue(fillA$);
    const boundGuideline = useValue(guidelineOpacity$);

    // Local mirrors so preset clicks snap state without waiting on the binding round-trip.
    const [outline, setOutline] = React.useState<Color>(boundOutline);
    const [fillA, setFillA] = React.useState<number>(boundFillA);
    const [guidelineOpacity, setGuidelineOpacity] = React.useState<number>(boundGuideline);

    const handleOutlineChange = (c: Color) => {
        setOutline(c);
        trigger(CHANNEL, "SetOutlineColor", c.r, c.g, c.b, c.a);
    };

    const handleFillAChange = (sliderPercent: number) => {
        const a = Math.max(0, Math.min(1, sliderPercent / 100));
        setFillA(a);
        trigger(CHANNEL, "SetFillAlpha", a);
    };

    const handleGuidelineChange = (percent: number) => {
        const clamped = Math.max(0, Math.min(100, Math.round(percent)));
        setGuidelineOpacity(clamped);
        trigger(CHANNEL, "SetGuidelineOpacity", clamped);
    };

    const applyPreset = (outlineColor: Color, fillAlpha: number) => {
        setOutline(outlineColor);
        setFillA(fillAlpha);
        trigger(CHANNEL, "SetOutlineColor", outlineColor.r, outlineColor.g, outlineColor.b, outlineColor.a);
        trigger(CHANNEL, "SetFillAlpha", fillAlpha);
    };

    const handleSet1 = () => applyPreset(PRESET_LIGHT_GRAY_OUTLINE, PRESET_LIGHT_GRAY_FILL_A);
    const handleSet2 = () => applyPreset(PRESET_YENYANG_OUTLINE, PRESET_YENYANG_FILL_A);
    const handleReset = () => applyPreset(PRESET_VANILLA_OUTLINE, PRESET_VANILLA_FILL_A);

    const ColorField = VanillaResolver.instance.ColorField;
    const Section = VanillaResolver.instance.Section;

    return (
        <div style={{
            position: "absolute",
            top: "100%",
            left: 0,
            marginTop: "6rem",
            zIndex: 10000,
        }}>
            <div className="panel_YqS menu_O_M">
                <div className=" content_XD5 content_AD7 child-opacity-transition_nkS content_Hzl">
                    <div style={{ padding: "8rem 8rem 4rem 8rem", display: "flex", flexDirection: "column", gap: "8rem" }}>
                        <Section title="Outline">
                            <ColorField
                                value={outline}
                                alpha={true}
                                hexInput={true}
                                colorWheel={true}
                                onChange={handleOutlineChange}
                            />
                        </Section>

                        <Section title="Fill">
                            <MochiColorSlider
                                className="sliderAlpha"
                                min={0}
                                max={100}
                                step={1}
                                value={fillA * 100}
                                onChange={handleFillAChange}
                                formatValue={(v) => v.toFixed(0)}
                            />
                        </Section>

                        <Section title="Guidelines">
                            <MochiColorSlider
                                min={0}
                                max={100}
                                step={5}
                                value={guidelineOpacity}
                                onChange={handleGuidelineChange}
                                formatValue={(v) => `${v.toFixed(0)}%`}
                            />
                        </Section>
                    </div>

                    <div style={{
                        display: "flex",
                        justifyContent: "flex-end",
                        gap: "4rem",
                        padding: "8rem",
                        borderTop: "1rem solid rgba(255, 255, 255, 0.15)",
                    }}>
                        <Tooltip tooltip="Preset 1: light gray, 10% halo alpha. Subtle ambient highlight.">
                            <Button variant="menu" onSelect={handleSet1}>
                                Set1
                            </Button>
                        </Tooltip>
                        <Tooltip tooltip="Preset 2: purple-gray, from yenyang's highlights tweaks proof of concept.">
                            <Button variant="menu" onSelect={handleSet2}>
                                Set2
                            </Button>
                        </Tooltip>
                        <Tooltip tooltip="Reset colors to game defaults, cyan blue.">
                            <Button variant="menu" onSelect={handleReset}>
                                Reset
                            </Button>
                        </Tooltip>
                    </div>
                </div>
            </div>
        </div>
    );
};
