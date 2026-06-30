// File: UI/src/panel/MochiPanelControlRows.tsx
// Purpose: Visual rows for Outline, Fill, and Guidelines. Logic/state stays in MochiColorPickerPanel.tsx.

import React from "react";
import { Color } from "cs2/bindings";
import { SideTooltip } from "./tooltip/SideTooltip";
import { PresetSlotButton } from "./components/MochiPanelPieces";
import { MochiSlider } from "./components/MochiSlider";
import {
    compactSwatchStyle,
    holdBarStyle,
    presetNumberColor,
    presetPreviewStyle,
} from "./helpers/MochiPanelColorUtils";
import { useMochiPanelText } from "./hooks/useMochiPanelText";
import fillIconSrc from "../../images/MainElements-Fill3.svg";
import outlineIconSrc from "../../images/MainElements_short_bigTriangle.svg";
import guidelinesIconSrc from "../../images/GuideLines4.svg";
import resetIconSrc from "../../images/Reset_Button2.svg";
import styles from "../MochiColorPickerPanel.module.scss";

type PickerDirection = "up" | "down";
type TooltipFn = (text: string) => React.ReactElement | undefined;
type ColorFieldComponent = React.ComponentType<any>;
type SliderComponent = React.ComponentType<any>;
type PanelText = ReturnType<typeof useMochiPanelText>;

interface MochiPanelControlRowsProps {
    text: PanelText;
    tt: TooltipFn;
    ColorField: ColorFieldComponent;
    Slider: SliderComponent;
    focusDisabled: any;
    numberFieldClass: string;
    useDarkerPanel: boolean;
    collapsed: boolean;

    outline: Color;
    ownerColor: Color;
    fillA: number;
    guidelineLinesColor: Color;
    guidelinePreviewColor: Color;
    guidelineDashedColor: Color;
    guidelineOpacity: number;
    preset1Color: Color;
    preset2Color: Color;

    colorPickerDirection: PickerDirection;
    ownerPickerDirection: PickerDirection;
    guidelineLinesPickerDirection: PickerDirection;
    guidelinePreviewPickerDirection: PickerDirection;
    guidelineDashedPickerDirection: PickerDirection;

    vanillaOutlineActive: boolean;
    preset1Active: boolean;
    preset2Active: boolean;

    swatchHovered: boolean;
    ownerSwatchHovered: boolean;
    guidelineLinesHovered: boolean;
    guidelinePreviewHovered: boolean;
    guidelineDashedHovered: boolean;
    preset1Hovered: boolean;
    preset2Hovered: boolean;

    setSwatchHovered: React.Dispatch<React.SetStateAction<boolean>>;
    setOwnerSwatchHovered: React.Dispatch<React.SetStateAction<boolean>>;
    setGuidelineLinesHovered: React.Dispatch<React.SetStateAction<boolean>>;
    setGuidelinePreviewHovered: React.Dispatch<React.SetStateAction<boolean>>;
    setGuidelineDashedHovered: React.Dispatch<React.SetStateAction<boolean>>;
    setPreset1Hovered: React.Dispatch<React.SetStateAction<boolean>>;
    setPreset2Hovered: React.Dispatch<React.SetStateAction<boolean>>;
    setOwnerPickerOpen: React.Dispatch<React.SetStateAction<boolean>>;
    setGuidelineLinesPickerOpen: React.Dispatch<React.SetStateAction<boolean>>;
    setGuidelinePreviewPickerOpen: React.Dispatch<React.SetStateAction<boolean>>;
    setGuidelineDashedPickerOpen: React.Dispatch<React.SetStateAction<boolean>>;

    holdSlot: number | null;
    holdProgress: number;
    cancelHold: () => void;
    handlePresetMouseDown: (slot: 1 | 2) => React.MouseEventHandler;
    handlePresetMouseUp: (slot: 1 | 2) => () => void;

    outlineSwatchRef: React.RefObject<HTMLDivElement>;
    ownerSwatchRef: React.RefObject<HTMLDivElement>;
    guidelineLinesPickerRef: React.RefObject<HTMLDivElement>;
    guidelinePreviewPickerRef: React.RefObject<HTMLDivElement>;
    guidelineDashedPickerRef: React.RefObject<HTMLDivElement>;

    handleOutlineChange: (value: Color) => void;
    handleOwnerColorChange: (value: Color) => void;
    handleFillAChange: (value: number) => void;
    handleGuidelineLinesColorChange: (value: Color) => void;
    handleGuidelinePreviewColorChange: (value: Color) => void;
    handleGuidelineDashedColorChange: (value: Color) => void;
    handleGuidelineChange: (value: number) => void;
    handleResetOutline: () => void;
    handleResetFill: () => void;
    handleResetGuidelines: () => void;
    handleTogglePresetDefaults: () => void;

    updateColorPickerDirection: () => void;
    updateOwnerPickerDirection: () => void;
    updateGuidelineLinesPickerDirection: () => void;
    updateGuidelinePreviewPickerDirection: () => void;
    updateGuidelineDashedPickerDirection: () => void;
}

export const MochiPanelControlRows = ({
    text,
    tt,
    ColorField,
    Slider: _Slider,
    focusDisabled,
    numberFieldClass,
    useDarkerPanel,
    collapsed,
    outline,
    ownerColor,
    fillA,
    guidelineLinesColor,
    guidelinePreviewColor,
    guidelineDashedColor,
    guidelineOpacity,
    preset1Color,
    preset2Color,
    colorPickerDirection,
    ownerPickerDirection,
    guidelineLinesPickerDirection,
    guidelinePreviewPickerDirection,
    guidelineDashedPickerDirection,
    vanillaOutlineActive,
    preset1Active,
    preset2Active,
    swatchHovered,
    ownerSwatchHovered,
    guidelineLinesHovered,
    guidelinePreviewHovered,
    guidelineDashedHovered,
    preset1Hovered,
    preset2Hovered,
    setSwatchHovered,
    setOwnerSwatchHovered,
    setGuidelineLinesHovered,
    setGuidelinePreviewHovered,
    setGuidelineDashedHovered,
    setPreset1Hovered,
    setPreset2Hovered,
    setOwnerPickerOpen,
    setGuidelineLinesPickerOpen,
    setGuidelinePreviewPickerOpen,
    setGuidelineDashedPickerOpen,
    holdSlot,
    holdProgress,
    cancelHold,
    handlePresetMouseDown,
    handlePresetMouseUp,
    outlineSwatchRef,
    ownerSwatchRef,
    guidelineLinesPickerRef,
    guidelinePreviewPickerRef,
    guidelineDashedPickerRef,
    handleOutlineChange,
    handleOwnerColorChange,
    handleFillAChange,
    handleGuidelineLinesColorChange,
    handleGuidelinePreviewColorChange,
    handleGuidelineDashedColorChange,
    handleGuidelineChange,
    handleResetOutline,
    handleResetFill,
    handleResetGuidelines,
    handleTogglePresetDefaults,
    updateColorPickerDirection,
    updateOwnerPickerDirection,
    updateGuidelineLinesPickerDirection,
    updateGuidelinePreviewPickerDirection,
    updateGuidelineDashedPickerDirection,
}: MochiPanelControlRowsProps) => {
    void _Slider;

    const compactShellStyle = React.useCallback(
        (color: Color, hovered: boolean) => compactSwatchStyle(color, hovered, useDarkerPanel),
        [useDarkerPanel],
    );

    const outlineSwatchActive = !preset1Active && !preset2Active;

    return (
        <div className={`${styles.body} ${collapsed ? styles.bodyCollapsed : ""}`}>
            <div className={`${styles.controlRow} ${styles.outlineRow}`}>
                <SideTooltip tooltip={tt(text.tooltipResetOutline)} side="left">
                    <button
                        type="button"
                        className={`${styles.controlIconButton} ${vanillaOutlineActive ? styles.vanillaActiveButton : ""}`}
                        onClick={handleResetOutline}
                    >
                        <img src={outlineIconSrc} className={`${styles.controlIcon} ${styles.idleIcon}`} alt="" />
                    </button>
                </SideTooltip>

                <div className={`${styles.controlBody} ${styles.outlineControlBody}`}>
                    <div className={styles.outlineLeft}>
                        <SideTooltip tooltip={tt(text.tooltipOutlineSwatch)} side="right">
                            <div
                                ref={outlineSwatchRef}
                                className={`${styles.outlineFieldShell} ${swatchHovered ? styles.outlineFieldShellHovered : ""} ${outlineSwatchActive ? styles.outlineFieldShellActive : ""}`}
                                onMouseOver={() => {
                                    if (!swatchHovered) {
                                        setSwatchHovered(true);
                                    }
                                    updateColorPickerDirection();
                                }}
                                onMouseMove={() => {
                                    if (!swatchHovered) {
                                        setSwatchHovered(true);
                                    }
                                    updateColorPickerDirection();
                                }}
                                onMouseLeave={() => setSwatchHovered(false)}
                                onMouseDown={updateColorPickerDirection}
                            >
                                <ColorField
                                    focusKey={focusDisabled}
                                    className={styles.outlineField}
                                    value={outline}
                                    alpha={true}
                                    popupDirection={colorPickerDirection}
                                    hideHint={true}
                                    hexInput={true}
                                    colorWheel={true}
                                    onMouseEnter={() => setSwatchHovered(true)}
                                    onMouseLeave={() => setSwatchHovered(false)}
                                    onChange={handleOutlineChange}
                                    onOpenPicker={updateColorPickerDirection}
                                />
                                <span className={styles.outlineFieldHoverRing} aria-hidden="true" />
                                <span className={styles.outlineFieldActiveDot} aria-hidden="true" />
                            </div>
                        </SideTooltip>

                        <SideTooltip tooltip={tt(text.tooltipOwnerSwatch)} side="right">
                            <div
                                ref={ownerSwatchRef}
                                className={`${styles.ownerFieldShell} ${ownerSwatchHovered ? styles.ownerFieldShellHovered : ""}`}
                                style={compactShellStyle(ownerColor, ownerSwatchHovered)}
                                onMouseOver={() => {
                                    if (!ownerSwatchHovered) {
                                        setOwnerSwatchHovered(true);
                                    }
                                    updateOwnerPickerDirection();
                                }}
                                onMouseMove={() => {
                                    if (!ownerSwatchHovered) {
                                        setOwnerSwatchHovered(true);
                                    }
                                    updateOwnerPickerDirection();
                                }}
                                onMouseLeave={() => setOwnerSwatchHovered(false)}
                                onMouseDown={updateOwnerPickerDirection}
                            >
                                <ColorField
                                    focusKey={focusDisabled}
                                    className={styles.ownerField}
                                    value={ownerColor}
                                    alpha={true}
                                    popupDirection={ownerPickerDirection}
                                    hideHint={true}
                                    hexInput={true}
                                    colorWheel={false}
                                    onChange={handleOwnerColorChange}
                                    onOpenPicker={() => {
                                        setOwnerPickerOpen(true);
                                        updateOwnerPickerDirection();
                                    }}
                                    onClosePicker={() => setOwnerPickerOpen(false)}
                                />
                                <span
                                    className={styles.ownerColorPreview}
                                    style={compactSwatchStyle(ownerColor, ownerSwatchHovered, useDarkerPanel)}
                                    aria-hidden="true"
                                />
                            </div>
                        </SideTooltip>

                        <PresetSlotButton
                            slot={1}
                            color={preset1Color}
                            active={preset1Active}
                            holdActive={holdSlot === 1}
                            holdProgress={holdProgress}
                            tooltip={tt(text.tooltipPreset1)}
                            tooltipSide="right"
                            marginLeft="10rem"
                            numberColor={presetNumberColor(preset1Active, preset1Hovered)}
                            presetPreviewStyle={presetPreviewStyle}
                            holdBarStyle={holdBarStyle}
                            onMouseEnter={() => setPreset1Hovered(true)}
                            onMouseDown={handlePresetMouseDown(1)}
                            onMouseUp={handlePresetMouseUp(1)}
                            onMouseLeave={() => {
                                setPreset1Hovered(false);
                                cancelHold();
                            }}
                        />

                        <PresetSlotButton
                            slot={2}
                            color={preset2Color}
                            active={preset2Active}
                            holdActive={holdSlot === 2}
                            holdProgress={holdProgress}
                            tooltip={tt(text.tooltipPreset2)}
                            tooltipSide="right"
                            marginLeft="5rem"
                            numberColor={presetNumberColor(preset2Active, preset2Hovered)}
                            presetPreviewStyle={presetPreviewStyle}
                            holdBarStyle={holdBarStyle}
                            onMouseEnter={() => setPreset2Hovered(true)}
                            onMouseDown={handlePresetMouseDown(2)}
                            onMouseUp={handlePresetMouseUp(2)}
                            onMouseLeave={() => {
                                setPreset2Hovered(false);
                                cancelHold();
                            }}
                        />
                    </div>

                    <div className={styles.outlineRight}>
                        <SideTooltip tooltip={tt(text.tooltipResetPresets)} side="right">
                            <button
                                type="button"
                                className={styles.presetResetBare}
                                onClick={handleTogglePresetDefaults}
                            >
                                <img src={resetIconSrc} className={styles.resetIcon} alt="" />
                            </button>
                        </SideTooltip>
                    </div>
                </div>
            </div>

            {!collapsed && (
            <>
            <div className={styles.controlRow}>
                <SideTooltip tooltip={tt(text.tooltipResetFill)} side="left">
                    <button type="button" className={styles.controlIconButton} onClick={handleResetFill}>
                        <img src={fillIconSrc} className={`${styles.controlIcon} ${styles.idleIcon}`} alt="" />
                    </button>
                </SideTooltip>

                <SideTooltip tooltip={tt(text.tooltipFillOpacity)} side="right">
                    <div className={styles.controlBody}>
                        <div className={styles.sliderRow}>
                            <MochiSlider
                                focusKey={focusDisabled}
                                className={styles.slider}
                                value={fillA}
                                start={0}
                                end={1}
                                gamepadStep={0.01}
                                onChange={handleFillAChange}
                            />
                            <div className={`${styles.valueField} ${numberFieldClass}`}>
                                {`${Math.round(fillA * 100)}%`}
                            </div>
                        </div>
                    </div>
                </SideTooltip>
            </div>

            <div className={styles.controlRow}>
                <SideTooltip tooltip={tt(text.tooltipResetGuidelines)} side="left">
                    <button
                        type="button"
                        className={styles.controlIconButton}
                        onClick={handleResetGuidelines}
                    >
                        <img src={guidelinesIconSrc} className={`${styles.controlIcon} ${styles.idleIcon} ${styles.guidelinesIcon}`} alt="" />
                    </button>
                </SideTooltip>

                <div className={`${styles.controlBody} ${styles.guidelineControlBody}`}>
                    <div className={styles.guidelineSwatches}>
                        <SideTooltip tooltip={tt(text.tooltipGuidelinesColor)} side="right">
                            <div
                                ref={guidelineLinesPickerRef}
                                className={`${styles.guidelineColorShell} ${guidelineLinesHovered ? styles.guidelineColorShellHovered : ""}`}
                                style={compactShellStyle(guidelineLinesColor, guidelineLinesHovered)}
                                onMouseOver={() => {
                                    if (!guidelineLinesHovered) {
                                        setGuidelineLinesHovered(true);
                                    }
                                    updateGuidelineLinesPickerDirection();
                                }}
                                onMouseMove={() => {
                                    if (!guidelineLinesHovered) {
                                        setGuidelineLinesHovered(true);
                                    }
                                }}
                                onMouseLeave={() => setGuidelineLinesHovered(false)}
                                onMouseDown={updateGuidelineLinesPickerDirection}
                            >
                                <ColorField
                                    focusKey={focusDisabled}
                                    className={styles.guidelineColorField}
                                    value={guidelineLinesColor}
                                    alpha={true}
                                    popupDirection={guidelineLinesPickerDirection}
                                    hideHint={true}
                                    hexInput={true}
                                    colorWheel={false}
                                    onChange={handleGuidelineLinesColorChange}
                                    onOpenPicker={() => {
                                        setGuidelineLinesPickerOpen(true);
                                        updateGuidelineLinesPickerDirection();
                                    }}
                                    onClosePicker={() => setGuidelineLinesPickerOpen(false)}
                                />
                                <span
                                    className={styles.guidelineColorPreview}
                                    style={compactSwatchStyle(guidelineLinesColor, guidelineLinesHovered, useDarkerPanel)}
                                    aria-hidden="true"
                                />
                            </div>
                        </SideTooltip>

                        <SideTooltip tooltip={tt(text.tooltipGuidelinesPreviewColor)} side="right">
                            <div
                                ref={guidelinePreviewPickerRef}
                                className={`${styles.guidelineColorShell} ${styles.guidelinePreviewColorShell} ${guidelinePreviewHovered ? styles.guidelineColorShellHovered : ""}`}
                                style={compactShellStyle(guidelinePreviewColor, guidelinePreviewHovered)}
                                onMouseOver={() => {
                                    if (!guidelinePreviewHovered) {
                                        setGuidelinePreviewHovered(true);
                                    }
                                    updateGuidelinePreviewPickerDirection();
                                }}
                                onMouseMove={() => {
                                    if (!guidelinePreviewHovered) {
                                        setGuidelinePreviewHovered(true);
                                    }
                                }}
                                onMouseLeave={() => setGuidelinePreviewHovered(false)}
                                onMouseDown={updateGuidelinePreviewPickerDirection}
                            >
                                <ColorField
                                    focusKey={focusDisabled}
                                    className={styles.guidelineColorField}
                                    value={guidelinePreviewColor}
                                    alpha={true}
                                    popupDirection={guidelinePreviewPickerDirection}
                                    hideHint={true}
                                    hexInput={true}
                                    colorWheel={false}
                                    onChange={handleGuidelinePreviewColorChange}
                                    onOpenPicker={() => {
                                        setGuidelinePreviewPickerOpen(true);
                                        updateGuidelinePreviewPickerDirection();
                                    }}
                                    onClosePicker={() => setGuidelinePreviewPickerOpen(false)}
                                />
                                <span
                                    className={styles.guidelineColorPreview}
                                    style={compactSwatchStyle(guidelinePreviewColor, guidelinePreviewHovered, useDarkerPanel)}
                                    aria-hidden="true"
                                />
                            </div>
                        </SideTooltip>

                        <SideTooltip tooltip={tt(text.tooltipGuidelinesDashedColor)} side="right">
                            <div
                                ref={guidelineDashedPickerRef}
                                className={`${styles.guidelineColorShell} ${styles.guidelineDashedColorShell} ${guidelineDashedHovered ? styles.guidelineColorShellHovered : ""}`}
                                style={compactShellStyle(guidelineDashedColor, guidelineDashedHovered)}
                                onMouseOver={() => {
                                    if (!guidelineDashedHovered) {
                                        setGuidelineDashedHovered(true);
                                    }
                                    updateGuidelineDashedPickerDirection();
                                }}
                                onMouseMove={() => {
                                    if (!guidelineDashedHovered) {
                                        setGuidelineDashedHovered(true);
                                    }
                                }}
                                onMouseLeave={() => setGuidelineDashedHovered(false)}
                                onMouseDown={updateGuidelineDashedPickerDirection}
                            >
                                <ColorField
                                    focusKey={focusDisabled}
                                    className={styles.guidelineColorField}
                                    value={guidelineDashedColor}
                                    alpha={true}
                                    popupDirection={guidelineDashedPickerDirection}
                                    hideHint={true}
                                    hexInput={true}
                                    colorWheel={false}
                                    onChange={handleGuidelineDashedColorChange}
                                    onOpenPicker={() => {
                                        setGuidelineDashedPickerOpen(true);
                                        updateGuidelineDashedPickerDirection();
                                    }}
                                    onClosePicker={() => setGuidelineDashedPickerOpen(false)}
                                />
                                <span
                                    className={styles.guidelineColorPreview}
                                    style={compactSwatchStyle(guidelineDashedColor, guidelineDashedHovered, useDarkerPanel)}
                                    aria-hidden="true"
                                />
                            </div>
                        </SideTooltip>
                    </div>

                    <SideTooltip tooltip={tt(text.tooltipGuidelinesOpacity)} side="right">
                        <div className={`${styles.sliderRow} ${styles.guidelineSliderRow}`}>
                            <MochiSlider
                                focusKey={focusDisabled}
                                className={styles.slider}
                                value={guidelineOpacity}
                                start={0}
                                end={100}
                                gamepadStep={5}
                                onChange={handleGuidelineChange}
                            />
                            <div className={`${styles.valueField} ${numberFieldClass}`}>
                                {`${guidelineOpacity}%`}
                            </div>
                        </div>
                    </SideTooltip>
                </div>
            </div>
            </>
            )}
        </div>
    );
};
