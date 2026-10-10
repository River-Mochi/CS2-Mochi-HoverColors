// <copyright file="MochiPanelActionBar.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: UI/src/panel/components/MochiPanelActionBar.tsx
// Purpose: Surface boundary controls, area fill toggles, and the District color menu.

import React from "react";
import { Button } from "cs2/ui";
import { Color } from "cs2/bindings";
import { SideTooltip } from "../tooltip/SideTooltip";
import { MochiSlider } from "./MochiSlider";
import { PanelSectionToggle } from "./MochiPanelPieces";
import { compactSwatchStyle, holdBarStyle } from "../helpers/MochiPanelColorUtils";
import { useMochiPanelText } from "../hooks/useMochiPanelText";
import lotToolIconSrc from "../../../images/LotTool03.svg";
import specializedIndustryIconSrc from "../../../images/LotToolSpecializedIndustry.svg";
import surfaceIconSrc from "../../../images/Districts03.svg";
import outlineThicknessIconSrc from "../../../images/outline-thickness.svg";
import styles from "../../MochiColorPickerPanel.module.scss";

type PickerDirection = "up" | "down";
type TooltipFn = (text: string) => React.ReactNode | undefined;
type ColorFieldComponent = React.ComponentType<any>;
type PanelText = ReturnType<typeof useMochiPanelText>;

interface MochiPanelActionBarProps {
    text: PanelText;
    tt: TooltipFn;
    ColorField: ColorFieldComponent;
    focusDisabled: any;
    useDarkerPanel: boolean;
    areasExpanded: boolean;
    handleToggleAreas: () => void;
    surfaceBorderThicknessScale: number;
    handleSurfaceBorderThicknessChange: (value: number) => void;
    handleResetSurfaceBorderThickness: () => void;
    extractorBorderThicknessScale: number;
    handleExtractorBorderThicknessChange: (value: number) => void;
    handleResetExtractorBorderThickness: () => void;
    districtBorderThicknessScale: number;
    handleDistrictBorderThicknessChange: (value: number) => void;
    handleResetDistrictBorderThickness: () => void;

    surfaceToolAreasSuppressed: boolean;
    specializedIndustryAreasSuppressed: boolean;
    districtMenuOpen: boolean;
    districtColor: Color;
    districtPickerDirection: PickerDirection;
    districtSwatchHovered: boolean;
    districtHoldProgress: number;

    districtPickerRef: React.RefObject<HTMLDivElement>;
    districtMenuRef: React.RefObject<HTMLDivElement>;
    districtColorSwatchRef: React.RefObject<HTMLDivElement>;

    setDistrictPickerOpen: React.Dispatch<React.SetStateAction<boolean>>;
    setDistrictSwatchHovered: React.Dispatch<React.SetStateAction<boolean>>;

    cancelDistrictHold: () => void;
    openAreasToolPanel: () => void;
    updateDistrictPickerDirection: () => void;

    handleToggleSurfaceToolAreas: () => void;
    handleToggleSpecializedIndustryAreas: () => void;
    handleDistrictMouseDownCapture: React.MouseEventHandler;
    handleDistrictMouseUpCapture: React.MouseEventHandler;
    handleDistrictClickCapture: React.MouseEventHandler;
    handleDistrictColorChange: (value: Color) => void;
    handleResetDistrict: () => void;
}

export const MochiPanelActionBar = ({
    text,
    tt,
    ColorField,
    focusDisabled,
    useDarkerPanel,
    areasExpanded,
    handleToggleAreas,
    surfaceBorderThicknessScale,
    handleSurfaceBorderThicknessChange,
    handleResetSurfaceBorderThickness,
    extractorBorderThicknessScale,
    handleExtractorBorderThicknessChange,
    handleResetExtractorBorderThickness,
    districtBorderThicknessScale,
    handleDistrictBorderThicknessChange,
    handleResetDistrictBorderThickness,
    surfaceToolAreasSuppressed,
    specializedIndustryAreasSuppressed,
    districtMenuOpen,
    districtColor,
    districtPickerDirection,
    districtSwatchHovered,
    districtHoldProgress,
    districtPickerRef,
    districtMenuRef,
    districtColorSwatchRef,
    setDistrictPickerOpen,
    setDistrictSwatchHovered,
    cancelDistrictHold,
    openAreasToolPanel,
    updateDistrictPickerDirection,
    handleToggleSurfaceToolAreas,
    handleToggleSpecializedIndustryAreas,
    handleDistrictMouseDownCapture,
    handleDistrictMouseUpCapture,
    handleDistrictClickCapture,
    handleDistrictColorChange,
    handleResetDistrict,
}: MochiPanelActionBarProps) => {
    const districtShellStyle = React.useCallback(
        (color: Color, hovered: boolean) => compactSwatchStyle(color, hovered, useDarkerPanel),
        [useDarkerPanel],
    );

    return (
        <div className={styles.actions}>
            <PanelSectionToggle label={text.sectionAreas} expanded={areasExpanded} onToggle={handleToggleAreas} focusDisabled={focusDisabled} />
            {areasExpanded && (
                <>
            <div className={styles.surfaceControlsRow}>
                <SideTooltip tooltip={tt(text.tooltipResetSurfaceBorderThickness)} side="left">
                    <Button
                        className={styles.controlIconButton}
                        variant="icon"
                        onSelect={handleResetSurfaceBorderThickness}
                        focusKey={focusDisabled}
                    >
                        <img src={outlineThicknessIconSrc} className={`${styles.controlIcon} ${styles.idleIcon}`} alt="" />
                    </Button>
                </SideTooltip>

                <SideTooltip tooltip={tt(text.tooltipSurfaceBorderThickness)} side="right">
                    <div className={styles.areaThicknessControl}>
                        <div className={styles.areaThicknessTrack}>
                            <MochiSlider
                                focusKey={focusDisabled}
                                className={styles.areaThicknessSlider}
                                value={surfaceBorderThicknessScale}
                                start={0.1}
                                end={1}
                                gamepadStep={0.1}
                                onChange={handleSurfaceBorderThicknessChange}
                            />
                            <span className={styles.areaThicknessValue}>{surfaceBorderThicknessScale.toFixed(1)}</span>
                        </div>
                        <span className={styles.areaThicknessLabel}>{text.labelSurfaceBorder}</span>
                    </div>
                </SideTooltip>

                <SideTooltip tooltip={tt(text.tooltipSurfaceToggle)} side="below">
                    <Button
                        className={`${styles.actionButton} ${styles.surfaceButton} ${surfaceToolAreasSuppressed ? styles.surfaceButtonActive : ""}`}
                        variant="icon"
                        onSelect={handleToggleSurfaceToolAreas}
                        focusKey={focusDisabled}
                    >
                        <img src={lotToolIconSrc} className={`${styles.controlIcon} ${styles.idleIcon}`} alt="" />
                    </Button>
                </SideTooltip>

            </div>

            <div className={styles.extractorControlsRow}>
                <SideTooltip tooltip={tt(text.tooltipResetExtractorBorderThickness)} side="left">
                    <Button
                        className={styles.controlIconButton}
                        variant="icon"
                        onSelect={handleResetExtractorBorderThickness}
                        focusKey={focusDisabled}
                    >
                        <img src={outlineThicknessIconSrc} className={`${styles.controlIcon} ${styles.idleIcon}`} alt="" />
                    </Button>
                </SideTooltip>

                <SideTooltip tooltip={tt(text.tooltipExtractorBorderThickness)} side="right">
                    <div className={styles.areaThicknessControl}>
                        <div className={styles.areaThicknessTrack}>
                            <MochiSlider
                                focusKey={focusDisabled}
                                className={styles.areaThicknessSlider}
                                value={extractorBorderThicknessScale}
                                start={0.1}
                                end={1}
                                gamepadStep={0.1}
                                onChange={handleExtractorBorderThicknessChange}
                            />
                            <span className={styles.areaThicknessValue}>{extractorBorderThicknessScale.toFixed(1)}</span>
                        </div>
                        <span className={styles.areaThicknessLabel}>{text.labelExtractorBorder}</span>
                    </div>
                </SideTooltip>

                <SideTooltip tooltip={tt(text.tooltipSpecializedIndustryToggle)} side="below">
                    <Button
                        className={`${styles.actionButton} ${styles.surfaceButton} ${specializedIndustryAreasSuppressed ? styles.surfaceButtonActive : ""}`}
                        variant="icon"
                        onSelect={handleToggleSpecializedIndustryAreas}
                        focusKey={focusDisabled}
                    >
                        <img src={specializedIndustryIconSrc} className={`${styles.controlIcon} ${styles.idleIcon}`} alt="" />
                    </Button>
                </SideTooltip>
            </div>

            <div className={styles.districtActionsRow}>
                <SideTooltip tooltip={tt(text.tooltipDistrictColors)} side="below">
                    <div ref={districtPickerRef}>
                        <Button
                            className={`${styles.actionButton} ${styles.surfaceButton} ${styles.districtPickerButton} ${districtMenuOpen ? styles.districtPickerButtonActive : ""}`}
                            variant="icon"
                            onMouseOver={updateDistrictPickerDirection}
                            // Hold resets District colors; quick click opens the mini menu.
                            onMouseDown={handleDistrictMouseDownCapture}
                            onMouseUp={handleDistrictMouseUpCapture}
                            onMouseLeave={cancelDistrictHold}
                            onClick={handleDistrictClickCapture}
                            focusKey={focusDisabled}
                        >
                            {districtHoldProgress > 0 && <span className={styles.holdBar} style={holdBarStyle(districtHoldProgress)} />}
                            <img src={surfaceIconSrc} className={`${styles.controlIcon} ${styles.idleIcon} ${styles.districtPickerIcon}`} alt="" />
                        </Button>
                    </div>
                </SideTooltip>

                <SideTooltip tooltip={tt(text.tooltipDistrictBorderThickness)} side="right">
                    <div className={styles.areaThicknessControl}>
                        <div className={styles.areaThicknessTrack}>
                            <MochiSlider
                                focusKey={focusDisabled}
                                className={styles.areaThicknessSlider}
                                value={districtBorderThicknessScale}
                                start={0.1}
                                end={1}
                                gamepadStep={0.1}
                                onChange={handleDistrictBorderThicknessChange}
                            />
                            <span className={styles.areaThicknessValue}>{districtBorderThicknessScale.toFixed(1)}</span>
                        </div>
                        <span className={styles.areaThicknessLabel}>{text.labelDistrictBorder}</span>
                    </div>
                </SideTooltip>

                <SideTooltip tooltip={tt(text.tooltipResetDistrictBorderThickness)} side="right">
                    <Button
                        className={styles.controlIconButton}
                        variant="icon"
                        onSelect={handleResetDistrictBorderThickness}
                        focusKey={focusDisabled}
                    >
                        <img src={outlineThicknessIconSrc} className={`${styles.controlIcon} ${styles.idleIcon}`} alt="" />
                    </Button>
                </SideTooltip>
            </div>

            {districtMenuOpen && (
                <div ref={districtMenuRef} className={styles.districtMenu}>
                    <div className={styles.districtMenuRow}>
                        <div
                            ref={districtColorSwatchRef}
                            className={styles.districtMenuSwatch}
                            style={districtShellStyle(districtColor, districtSwatchHovered)}
                            onMouseOver={() => {
                                if (!districtSwatchHovered) {
                                    setDistrictSwatchHovered(true);
                                }
                                updateDistrictPickerDirection();
                            }}
                            onMouseMove={() => {
                                if (!districtSwatchHovered) {
                                    setDistrictSwatchHovered(true);
                                }
                            }}
                            onMouseLeave={() => setDistrictSwatchHovered(false)}
                            onMouseDown={updateDistrictPickerDirection}
                        >
                            {/* Phase 2 can render one of these rows per District. */}
                            <span
                                className={styles.districtMenuSwatchPreview}
                                style={compactSwatchStyle(districtColor, false)}
                                aria-hidden="true"
                            />

                            <ColorField
                                focusKey={focusDisabled}
                                className={styles.districtColorField}
                                value={districtColor}
                                alpha={true}
                                popupDirection={districtPickerDirection}
                                hideHint={true}
                                hexInput={true}
                                colorWheel={false}
                                onChange={handleDistrictColorChange}
                                onOpenPicker={() => {
                                    cancelDistrictHold();
                                    setDistrictPickerOpen(true);
                                    openAreasToolPanel();
                                    updateDistrictPickerDirection();
                                }}
                                onClosePicker={() => setDistrictPickerOpen(false)}
                            />
                        </div>

                        <span className={styles.districtMenuName}>
                            {text.districtMenuAllDistricts}
                        </span>

                        <SideTooltip tooltip={tt(text.tooltipResetDistrictColors)} side="right">
                            <Button
                                className={styles.districtMenuReset}
                                onSelect={handleResetDistrict}
                                focusKey={focusDisabled}
                            >
                                {text.districtMenuResetAll}
                            </Button>
                        </SideTooltip>
                    </div>
                </div>
            )}
                </>
            )}
        </div>
    );
};
