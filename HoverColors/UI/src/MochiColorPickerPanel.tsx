// File: UI/src/MochiColorPickerPanel.tsx
// Purpose: Compact in-city hover-color panel anchored under the GameTopLeft icon button.
// Layout: title bar + color control rows + bottom action bar.

import React from "react";
import { Button, FormattedParagraphs } from "cs2/ui";
import { Color } from "cs2/bindings";
import { trigger, useValue } from "cs2/api";
import { VanillaComponentResolver } from "./utils/vanilla/VanillaComponentResolver";
import {
    CHANNEL,
    COMPACT_PICKER_BODY_CLASS,
    PICKER_OPEN_BODY_CLASS,
    districtA$,
    districtB$,
    districtG$,
    districtR$,
    fillA$,
    fillB$,
    fillG$,
    fillR$,
    hoverHighlightsSuppressed$,
    guidelineLinesColorA$,
    guidelineLinesColorB$,
    guidelineLinesColorG$,
    guidelineLinesColorR$,
    guidelineDashedColorB$,
    guidelineDashedColorG$,
    guidelineDashedColorR$,
    guidelineOpacity$,
    guidelinePreviewColorA$,
    guidelinePreviewColorB$,
    guidelinePreviewColorG$,
    guidelinePreviewColorR$,
    outlineA$,
    outlineThicknessScale$,
    outlineB$,
    outlineG$,
    outlineR$,
    ownerA$,
    ownerB$,
    ownerG$,
    ownerR$,
    panelCollapsed$,
    panelTooltipsEnabled$,
    preset1A$,
    preset1Active$,
    preset1B$,
    preset1G$,
    preset1R$,
    preset2A$,
    preset2Active$,
    preset2B$,
    preset2G$,
    preset2R$,
    specializedIndustryAreasSuppressed$,
    surfaceToolAreasSuppressed$,
    useDarkerPanel$,
    vanillaOutlineActive$,
} from "./panel/bindings/MochiPanelBindings";
import { MochiPanelActionBar } from "./panel/components/MochiPanelActionBar";
import { MochiPanelControlRows } from "./panel/components/MochiPanelControlRows";
import { DragGrip } from "./panel/components/MochiPanelPieces";
import { normalizeColorFieldValue } from "./panel/helpers/MochiPanelColorUtils";
import { useDistrictHold } from "./panel/hooks/useDistrictHold";
import { useDistrictToolPanel } from "./panel/hooks/useDistrictToolPanel";
import { useMochiPanelText } from "./panel/hooks/useMochiPanelText";
import { usePanelDrag } from "./panel/hooks/usePanelDrag";
import { usePresetHold } from "./panel/hooks/usePresetHold";
import { SideTooltip, SideTooltipProvider } from "./panel/tooltip/SideTooltip";
import infoIconSrc from "../images/AdvisorInfoViewWhite.svg";
import closeIconSrc from "../images/Close.svg";
// Highlights-OFF eye: mod icon so the slash can be red. ON state keeps the vanilla eye.
import eyeOffIconSrc from "../images/EyeOffRedSlash.svg";
import styles from "./MochiColorPickerPanel.module.scss";

type MochiColorPickerPanelProps = {
    editorMode?: boolean;
};

export const MochiColorPickerPanel = ({ editorMode = false }: MochiColorPickerPanelProps) => {
    const boundOutline: Color = {
        r: useValue(outlineR$),
        g: useValue(outlineG$),
        b: useValue(outlineB$),
        a: useValue(outlineA$),
    };
    const boundOwner: Color = {
        r: useValue(ownerR$),
        g: useValue(ownerG$),
        b: useValue(ownerB$),
        a: useValue(ownerA$),
    };
    const boundDistrict: Color = {
        r: useValue(districtR$),
        g: useValue(districtG$),
        b: useValue(districtB$),
        a: useValue(districtA$),
    };
    const boundGuidelineLinesColor: Color = {
        r: useValue(guidelineLinesColorR$),
        g: useValue(guidelineLinesColorG$),
        b: useValue(guidelineLinesColorB$),
        a: useValue(guidelineLinesColorA$),
    };
    const boundGuidelinePreviewColor: Color = {
        r: useValue(guidelinePreviewColorR$),
        g: useValue(guidelinePreviewColorG$),
        b: useValue(guidelinePreviewColorB$),
        a: useValue(guidelinePreviewColorA$),
    };
    const boundFillA = useValue(fillA$);
    const boundFill: Color = {
        r: useValue(fillR$),
        g: useValue(fillG$),
        b: useValue(fillB$),
        a: boundFillA,
    };
    const boundOutlineThicknessScale = useValue(outlineThicknessScale$);
    const boundGuideline = useValue(guidelineOpacity$);
    const boundGuidelineDashedColor: Color = {
        r: useValue(guidelineDashedColorR$),
        g: useValue(guidelineDashedColorG$),
        b: useValue(guidelineDashedColorB$),
        a: Math.max(0, Math.min(1, boundGuideline / 100)),
    };
    const useDarkerPanel = useValue(useDarkerPanel$);
    const surfaceToolAreasSuppressed = useValue(surfaceToolAreasSuppressed$);
    const specializedIndustryAreasSuppressed = useValue(specializedIndustryAreasSuppressed$);
    const vanillaOutlineActive = useValue(vanillaOutlineActive$);
    const preset1Active = useValue(preset1Active$);
    const preset2Active = useValue(preset2Active$);

    // Stored preset slots. Preview uses RGB for readability; alpha is still saved/applied by C#.
    const preset1Color: Color = { r: useValue(preset1R$), g: useValue(preset1G$), b: useValue(preset1B$), a: useValue(preset1A$) };
    const preset2Color: Color = { r: useValue(preset2R$), g: useValue(preset2G$), b: useValue(preset2B$), a: useValue(preset2A$) };

    const text = useMochiPanelText();
    const tooltipsEnabled = useValue(panelTooltipsEnabled$);
    const panelCollapsed = useValue(panelCollapsed$);
    const hoverHighlightsSuppressed = useValue(hoverHighlightsSuppressed$);

    // FormattedParagraphs lets vanilla Tooltip render JSON \n as real line breaks.
    const tt = React.useCallback(
        (s: string): React.ReactElement | undefined => (
            tooltipsEnabled
                ? <FormattedParagraphs>{s.split("\n")}</FormattedParagraphs>
                : undefined
        ),
        [tooltipsEnabled],
    );

    const ttAlways = React.useCallback(
        (s: string): React.ReactElement => <FormattedParagraphs>{s.split("\n")}</FormattedParagraphs>,
        [],
    );

    const [outline, setOutline] = React.useState<Color>(boundOutline);
    const [ownerColor, setOwnerColor] = React.useState<Color>(boundOwner);
    const [fillA, setFillA] = React.useState<number>(boundFillA);
    const [fillColor, setFillColor] = React.useState<Color>(boundFill);
    const [outlineThicknessScale, setOutlineThicknessScale] = React.useState<number>(boundOutlineThicknessScale);
    const [districtColor, setDistrictColor] = React.useState<Color>(boundDistrict);
    const [guidelineLinesColor, setGuidelineLinesColor] = React.useState<Color>(boundGuidelineLinesColor);
    const [guidelinePreviewColor, setGuidelinePreviewColor] = React.useState<Color>(boundGuidelinePreviewColor);
    const [guidelineDashedColor, setGuidelineDashedColor] = React.useState<Color>(boundGuidelineDashedColor);
    const [guidelineOpacity, setGuidelineOpacity] = React.useState<number>(boundGuideline);

    const [colorPickerDirection, setColorPickerDirection] = React.useState<"up" | "down">("down");
    const [fillPickerDirection, setFillPickerDirection] = React.useState<"up" | "down">("down");
    const [ownerPickerDirection, setOwnerPickerDirection] = React.useState<"up" | "down">("down");
    const [guidelineLinesPickerDirection, setGuidelineLinesPickerDirection] = React.useState<"up" | "down">("up");
    const [guidelinePreviewPickerDirection, setGuidelinePreviewPickerDirection] = React.useState<"up" | "down">("up");
    const [guidelineDashedPickerDirection, setGuidelineDashedPickerDirection] = React.useState<"up" | "down">("up");
    const [districtPickerDirection, setDistrictPickerDirection] = React.useState<"up" | "down">("up");

    const [outlinePickerOpen, setOutlinePickerOpen] = React.useState(false);
    const [fillPickerOpen, setFillPickerOpen] = React.useState(false);
    const [ownerPickerOpen, setOwnerPickerOpen] = React.useState(false);
    const [districtPickerOpen, setDistrictPickerOpen] = React.useState(false);
    const [districtMenuOpen, setDistrictMenuOpen] = React.useState(false);
    const [guidelineLinesPickerOpen, setGuidelineLinesPickerOpen] = React.useState(false);
    const [guidelinePreviewPickerOpen, setGuidelinePreviewPickerOpen] = React.useState(false);
    const [guidelineDashedPickerOpen, setGuidelineDashedPickerOpen] = React.useState(false);

    // ColorField can swallow hover events; React hover state keeps the visible rings reliable in COHTML.
    const [swatchHovered, setSwatchHovered] = React.useState(false);
    const [fillSwatchHovered, setFillSwatchHovered] = React.useState(false);
    const [ownerSwatchHovered, setOwnerSwatchHovered] = React.useState(false);
    const [guidelineLinesHovered, setGuidelineLinesHovered] = React.useState(false);
    const [guidelinePreviewHovered, setGuidelinePreviewHovered] = React.useState(false);
    const [guidelineDashedHovered, setGuidelineDashedHovered] = React.useState(false);
    const [districtSwatchHovered, setDistrictSwatchHovered] = React.useState(false);
    const [preset1Hovered, setPreset1Hovered] = React.useState(false);
    const [preset2Hovered, setPreset2Hovered] = React.useState(false);

    const panelAnchorRef = React.useRef<HTMLDivElement>(null);
    const outlineSwatchRef = React.useRef<HTMLDivElement>(null);
    const fillSwatchRef = React.useRef<HTMLDivElement>(null);
    const ownerSwatchRef = React.useRef<HTMLDivElement>(null);
    const guidelineLinesPickerRef = React.useRef<HTMLDivElement>(null);
    const guidelinePreviewPickerRef = React.useRef<HTMLDivElement>(null);
    const guidelineDashedPickerRef = React.useRef<HTMLDivElement>(null);
    const districtPickerRef = React.useRef<HTMLDivElement>(null);
    const districtMenuRef = React.useRef<HTMLDivElement>(null);
    const districtColorSwatchRef = React.useRef<HTMLDivElement>(null);

    const {
        holdSlot,
        holdProgress,
        cancelHold,
        handlePresetMouseDown,
        handlePresetMouseUp,
    } = usePresetHold();
    const {
        panelOffset,
        panelDragging,
        panelElementRef,
        handlePanelDragStart,
    } = usePanelDrag(editorMode ? "editor" : "game");
    const { openAreasToolPanel } = useDistrictToolPanel();

    // Keep local controls synced when C# settings change through presets, reset buttons, or game reload.
    React.useEffect(() => { setOutline(boundOutline); }, [boundOutline.r, boundOutline.g, boundOutline.b, boundOutline.a]);
    React.useEffect(() => { setOwnerColor(boundOwner); }, [boundOwner.r, boundOwner.g, boundOwner.b, boundOwner.a]);
    React.useEffect(() => { setFillA(boundFillA); }, [boundFillA]);
    React.useEffect(() => { setFillColor(boundFill); }, [boundFill.r, boundFill.g, boundFill.b, boundFill.a]);
    React.useEffect(() => { setOutlineThicknessScale(boundOutlineThicknessScale); }, [boundOutlineThicknessScale]);
    React.useEffect(() => { setDistrictColor(boundDistrict); }, [boundDistrict.r, boundDistrict.g, boundDistrict.b, boundDistrict.a]);
    React.useEffect(() => { setGuidelineLinesColor(boundGuidelineLinesColor); }, [boundGuidelineLinesColor.r, boundGuidelineLinesColor.g, boundGuidelineLinesColor.b, boundGuidelineLinesColor.a]);
    React.useEffect(() => { setGuidelinePreviewColor(boundGuidelinePreviewColor); }, [boundGuidelinePreviewColor.r, boundGuidelinePreviewColor.g, boundGuidelinePreviewColor.b, boundGuidelinePreviewColor.a]);
    React.useEffect(() => { setGuidelineDashedColor(boundGuidelineDashedColor); }, [boundGuidelineDashedColor.r, boundGuidelineDashedColor.g, boundGuidelineDashedColor.b, boundGuidelineDashedColor.a]);
    React.useEffect(() => { setGuidelineOpacity(boundGuideline); }, [boundGuideline]);

    // The vanilla picker popup rides the game's anchored-balloon layer, whose z-index is
    // var(--tooltipIndex) = 20 by default, while this panel sits at 10000. In the city the panel is
    // nested inside GameTopLeft's stacking context so the balloon still wins, but in the Editor the
    // panel outranks it and covers the picker. Raising --tooltipIndex to the value the game itself
    // uses behind a modal backdrop puts the popup above the panel in both modes.
    React.useEffect(() => {
        if (typeof document === "undefined") {
            return;
        }

        const anyPickerOpen = outlinePickerOpen
            || fillPickerOpen
            || ownerPickerOpen
            || districtPickerOpen
            || guidelineLinesPickerOpen
            || guidelinePreviewPickerOpen
            || guidelineDashedPickerOpen;

        document.body.classList.toggle(PICKER_OPEN_BODY_CLASS, anyPickerOpen);
        return () => document.body.classList.remove(PICKER_OPEN_BODY_CLASS);
    }, [
        districtPickerOpen,
        fillPickerOpen,
        guidelineDashedPickerOpen,
        guidelineLinesPickerOpen,
        guidelinePreviewPickerOpen,
        outlinePickerOpen,
        ownerPickerOpen,
    ]);

    React.useEffect(() => {
        if (typeof document === "undefined") {
            return;
        }

        const compactPickerOpen = ownerPickerOpen || districtPickerOpen || guidelineLinesPickerOpen || guidelinePreviewPickerOpen || guidelineDashedPickerOpen;
        document.body.classList.toggle(COMPACT_PICKER_BODY_CLASS, compactPickerOpen);

        if (!compactPickerOpen) {
            return () => document.body.classList.remove(COMPACT_PICKER_BODY_CLASS);
        }

        const onMouseDown = (event: MouseEvent) => {
            const target = event.target as Element | null;
            if (target == null) {
                return;
            }

            // Vanilla ColorField closes on outside clicks but does not always call onClosePicker.
            if (
                districtMenuRef.current?.contains(target)
                || districtPickerRef.current?.contains(target)
                || districtColorSwatchRef.current?.contains(target)
                || ownerSwatchRef.current?.contains(target)
                || guidelineLinesPickerRef.current?.contains(target)
                || guidelinePreviewPickerRef.current?.contains(target)
                || guidelineDashedPickerRef.current?.contains(target)
                || target.closest(".color-picker-container_Sj5")
            ) {
                return;
            }

            setOutlinePickerOpen(false);
            setFillPickerOpen(false);
            setDistrictPickerOpen(false);
            setOwnerPickerOpen(false);
            setGuidelineLinesPickerOpen(false);
            setGuidelinePreviewPickerOpen(false);
            setGuidelineDashedPickerOpen(false);
        };

        document.addEventListener("mousedown", onMouseDown);
        return () => {
            document.removeEventListener("mousedown", onMouseDown);
            document.body.classList.remove(COMPACT_PICKER_BODY_CLASS);
        };
    }, [districtPickerOpen, guidelineDashedPickerOpen, guidelineLinesPickerOpen, guidelinePreviewPickerOpen, ownerPickerOpen]);

    React.useEffect(() => {
        if (!districtMenuOpen || typeof document === "undefined") {
            return;
        }

        const onMouseDown = (event: MouseEvent) => {
            const target = event.target as Element | null;
            if (
                target == null
                || districtPickerRef.current?.contains(target)
                || districtMenuRef.current?.contains(target)
                || target.closest(".color-picker-container_Sj5")
            ) {
                return;
            }

            setDistrictMenuOpen(false);
        };

        document.addEventListener("mousedown", onMouseDown);
        return () => document.removeEventListener("mousedown", onMouseDown);
    }, [districtMenuOpen]);

    const handleResetDistrict = React.useCallback(() => {
        trigger(CHANNEL, "ResetDistrictToVanilla");
        setDistrictPickerOpen(false);
    }, []);

    const handleOpenDistrictMenu = React.useCallback(() => {
        setDistrictMenuOpen(open => {
            const nextOpen = !open;
            if (nextOpen) {
                openAreasToolPanel();
            }

            return nextOpen;
        });
    }, [openAreasToolPanel]);

    const {
        districtHoldProgress,
        cancelDistrictHold,
        handleDistrictMouseDownCapture,
        handleDistrictMouseUpCapture,
        handleDistrictClickCapture,
    } = useDistrictHold({
        onReset: handleResetDistrict,
        onQuickClick: handleOpenDistrictMenu,
    });

    const handleOutlineChange = (value: Color) => {
        setOutline(value);
        trigger(CHANNEL, "SetOutlineColor", value.r, value.g, value.b, value.a);
    };

    const handleOwnerColorChange = (value: Color) => {
        const syncedValue = normalizeColorFieldValue(value);
        setOwnerColor(syncedValue);
        trigger(CHANNEL, "SetOwnerColor", syncedValue.r, syncedValue.g, syncedValue.b, syncedValue.a);
    };

    const handleFillAChange = (v: number) => {
        const value = Math.max(0, Math.min(1, v));
        setFillA(value);
        setFillColor(prev => ({ ...prev, a: value }));
        trigger(CHANNEL, "SetFillAlpha", value);
    };

    // Slider steps in 0.1; rounding here keeps the readout off floating-point drift.
    const handleOutlineThicknessChange = (v: number) => {
        const value = Math.round(Math.max(0, Math.min(2, v)) * 10) / 10;
        setOutlineThicknessScale(value);
        trigger(CHANNEL, "SetOutlineThickness", value);
    };

    // Swatch owns tint + opacity; the slider is the same alpha shown a second way.
    const handleFillColorChange = (value: Color) => {
        const syncedValue = normalizeColorFieldValue(value);
        const alpha = Math.max(0, Math.min(1, typeof syncedValue.a === "number" ? syncedValue.a : 1));
        const fillValue = { ...syncedValue, a: alpha };
        setFillColor(fillValue);
        setFillA(alpha);
        trigger(CHANNEL, "SetFillColor", fillValue.r, fillValue.g, fillValue.b, alpha);
    };

    const handleDistrictColorChange = (value: Color) => {
        cancelDistrictHold();
        setDistrictColor(value);
        trigger(CHANNEL, "SetDistrictColor", value.r, value.g, value.b, value.a);
    };

    const handleGuidelineLinesColorChange = (value: Color) => {
        const syncedValue = normalizeColorFieldValue(value);
        setGuidelineLinesColor(syncedValue);
        trigger(CHANNEL, "SetGuidelineLinesColor", syncedValue.r, syncedValue.g, syncedValue.b, syncedValue.a);
    };

    const handleGuidelinePreviewColorChange = (value: Color) => {
        const syncedValue = normalizeColorFieldValue(value);
        setGuidelinePreviewColor(syncedValue);
        trigger(CHANNEL, "SetGuidelinePreviewColor", syncedValue.r, syncedValue.g, syncedValue.b, syncedValue.a);
    };

    const handleGuidelineDashedColorChange = (value: Color) => {
        const syncedValue = normalizeColorFieldValue(value);
        const alpha = typeof syncedValue.a === "number" ? syncedValue.a : 1;
        const percent = Math.max(0, Math.min(100, Math.round((alpha * 100) / 5) * 5));
        const dashedValue = { ...syncedValue, a: percent / 100 };
        setGuidelineDashedColor(dashedValue);
        setGuidelineOpacity(percent);
        trigger(CHANNEL, "SetGuidelineDashedColor", dashedValue.r, dashedValue.g, dashedValue.b, dashedValue.a);
    };

    const handleGuidelineChange = (v: number) => {
        const value = Math.max(0, Math.min(100, Math.round(v / 5) * 5));
        setGuidelineOpacity(value);
        setGuidelineDashedColor(prev => ({ ...prev, a: value / 100 }));
        trigger(CHANNEL, "SetGuidelineOpacity", value);
    };

    const handleClosePanel = () => trigger(CHANNEL, "SetPanelOpen", false);
    const handleToggleHighlights = () => trigger(CHANNEL, "ToggleHighlights");
    const handleToggleCollapse = () => trigger(CHANNEL, "SetPanelCollapsed", !panelCollapsed);
    const handleResetOutline = () => trigger(CHANNEL, "ResetOutlineToVanilla");
    const handleResetFill = () => trigger(CHANNEL, "ResetFillToVanilla");
    const handleResetOutlineThickness = () => trigger(CHANNEL, "ResetOutlineThickness");
    const handleResetGuidelines = () => trigger(CHANNEL, "ResetGuidelines");
    const handleToggleSurfaceToolAreas = () => trigger(CHANNEL, "ToggleSurfaceToolAreas");
    const handleToggleSpecializedIndustryAreas = () => trigger(CHANNEL, "ToggleSpecializedIndustryAreas");
    const handleTogglePresetDefaults = () => trigger(CHANNEL, "TogglePresetDefaults");
    const handleRestorePresetDefaults = () => trigger(CHANNEL, "RestorePresetDefaults");
    const handleInfoButtonClick = () => {
        if (!tooltipsEnabled) {
            trigger(CHANNEL, "SetPanelTooltipsEnabled", true);
        }
    };

    const updatePickerDirection = React.useCallback((element: HTMLElement | null, setDirection: React.Dispatch<React.SetStateAction<"up" | "down">>) => {
        if (element == null) {
            return;
        }

        const rect = element.getBoundingClientRect();
        setDirection(rect.top + rect.height / 2 < window.innerHeight / 2 ? "down" : "up");
    }, []);

    const updateColorPickerDirection = React.useCallback(() => updatePickerDirection(outlineSwatchRef.current, setColorPickerDirection), [updatePickerDirection]);
    const updateFillPickerDirection = React.useCallback(() => updatePickerDirection(fillSwatchRef.current, setFillPickerDirection), [updatePickerDirection]);
    const updateOwnerPickerDirection = React.useCallback(() => updatePickerDirection(ownerSwatchRef.current, setOwnerPickerDirection), [updatePickerDirection]);
    const updateDistrictPickerDirection = React.useCallback(() => updatePickerDirection(districtColorSwatchRef.current ?? districtPickerRef.current, setDistrictPickerDirection), [updatePickerDirection]);
    const updateGuidelineLinesPickerDirection = React.useCallback(() => updatePickerDirection(guidelineLinesPickerRef.current, setGuidelineLinesPickerDirection), [updatePickerDirection]);
    const updateGuidelinePreviewPickerDirection = React.useCallback(() => updatePickerDirection(guidelinePreviewPickerRef.current, setGuidelinePreviewPickerDirection), [updatePickerDirection]);
    const updateGuidelineDashedPickerDirection = React.useCallback(() => updatePickerDirection(guidelineDashedPickerRef.current, setGuidelineDashedPickerDirection), [updatePickerDirection]);

    const resolver = VanillaComponentResolver.instance;
    const ColorField = resolver.ColorField;
    const focusDisabled = resolver.FOCUS_DISABLED;
    const numberFieldClass = resolver.mouseToolOptionsTheme["number-field"];
    const roundHighlightButtonTheme = resolver.roundHighlightButtonTheme;
    const panelBaseTheme = resolver.panelBaseTheme;
    const panelTheme = resolver.panelTheme;
    const infoviewMenuTheme = resolver.infoviewMenuTheme;
    const eyeButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.eyeButton}`;
    const collapseButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.collapseButton}`;
    const closeButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.closeButton}`;
    const panelFrameClass = `${panelBaseTheme.panel ?? "panel_YqS"} ${infoviewMenuTheme.menu ?? "menu_O_M"} ${styles.panelFrame}`;
    const panelSurfaceClass = useDarkerPanel ? styles.panelDarker : styles.panelStandard;
    const panelContentClass = `${panelTheme.content ?? "content_XD5 content_AD7 child-opacity-transition_nkS"} ${infoviewMenuTheme.content ?? "content_Hzl"} ${styles.panelContent} ${panelSurfaceClass}`;

    return (
        <div
            ref={panelAnchorRef}
            className={`${styles.panelAnchor} ${editorMode ? styles.panelAnchorEditor : ""}`}
            style={{ transform: `translate(${panelOffset.x}px, ${panelOffset.y}px)` }}
        >
            <SideTooltipProvider anchorRef={panelAnchorRef} panelRef={panelElementRef} disabled={panelDragging}>
            <div ref={panelElementRef} className={panelFrameClass}>
                <div className={panelContentClass}>
                    <div className={styles.titleBar}>
                        <SideTooltip tooltip={ttAlways(text.tooltipInfo)} side="above">
                            <Button
                                className={`${styles.infoButton} ${!tooltipsEnabled ? styles.infoButtonTooltipsOff : ""}`}
                                variant="icon"
                                onSelect={handleInfoButtonClick}
                                focusKey={focusDisabled}
                                aria-pressed={!tooltipsEnabled}
                            >
                                <img src={infoIconSrc} className={`${styles.infoIcon} ${styles.idleIcon}`} alt="" />
                            </Button>
                        </SideTooltip>

                        <SideTooltip tooltip={tt(text.tooltipDraggable)} side="right">
                            <div
                                className={`${styles.titleDragHandle} ${panelDragging ? styles.titleDragHandleActive : ""}`}
                                onMouseDown={handlePanelDragStart}
                            >
                                <span className={styles.titleText}>{text.title}</span>
                            </div>
                        </SideTooltip>

                        <SideTooltip tooltip={tt(text.tooltipHoverToggle)} side="right">
                            <Button
                                className={eyeButtonClass}
                                variant="icon"
                                onSelect={handleToggleHighlights}
                                focusKey={focusDisabled}
                                aria-pressed={hoverHighlightsSuppressed}
                            >
                                <img
                                    src={hoverHighlightsSuppressed
                                        ? eyeOffIconSrc
                                        : "Media/PhotoMode/HideUIOff.svg"}
                                    className={`${styles.eyeIcon} ${hoverHighlightsSuppressed ? styles.eyeIconOff : ""}`}
                                    alt=""
                                />
                            </Button>
                        </SideTooltip>

                        <SideTooltip tooltip={tt(text.tooltipCollapse)} side="right">
                            <Button
                                className={collapseButtonClass}
                                variant="icon"
                                onSelect={handleToggleCollapse}
                                focusKey={focusDisabled}
                                aria-pressed={panelCollapsed}
                            >
                                <img
                                    src={panelCollapsed ? "Media/Glyphs/ThickStrokeArrowRight.svg" : "Media/Glyphs/ThickStrokeArrowDown.svg"}
                                    className={styles.collapseIcon}
                                    alt=""
                                />
                            </Button>
                        </SideTooltip>

                        <SideTooltip tooltip={tt(text.tooltipClose)} side="right">
                            <Button
                                className={closeButtonClass}
                                variant="icon"
                                onSelect={handleClosePanel}
                                focusKey={focusDisabled}
                                aria-label={text.ariaClosePanel}
                            >
                                <img src={closeIconSrc} className={styles.closeIcon} alt="" />
                            </Button>
                        </SideTooltip>
                    </div>

                    <MochiPanelControlRows
                        text={text}
                        tt={tt}
                        ColorField={ColorField}
                        focusDisabled={focusDisabled}
                        numberFieldClass={numberFieldClass}
                        useDarkerPanel={useDarkerPanel}
                        collapsed={panelCollapsed}
                        outline={outline}
                        ownerColor={ownerColor}
                        fillA={fillA}
                        fillColor={fillColor}
                        outlineThicknessScale={outlineThicknessScale}
                        guidelineLinesColor={guidelineLinesColor}
                        guidelinePreviewColor={guidelinePreviewColor}
                        guidelineDashedColor={guidelineDashedColor}
                        guidelineOpacity={guidelineOpacity}
                        preset1Color={preset1Color}
                        preset2Color={preset2Color}
                        colorPickerDirection={colorPickerDirection}
                        fillPickerDirection={fillPickerDirection}
                        ownerPickerDirection={ownerPickerDirection}
                        guidelineLinesPickerDirection={guidelineLinesPickerDirection}
                        guidelinePreviewPickerDirection={guidelinePreviewPickerDirection}
                        guidelineDashedPickerDirection={guidelineDashedPickerDirection}
                        vanillaOutlineActive={vanillaOutlineActive}
                        preset1Active={preset1Active}
                        preset2Active={preset2Active}
                        swatchHovered={swatchHovered}                  
                        fillSwatchHovered={fillSwatchHovered}
                        ownerSwatchHovered={ownerSwatchHovered}
                        guidelineLinesHovered={guidelineLinesHovered}
                        guidelinePreviewHovered={guidelinePreviewHovered}
                        guidelineDashedHovered={guidelineDashedHovered}
                        preset1Hovered={preset1Hovered}
                        preset2Hovered={preset2Hovered}
                        setSwatchHovered={setSwatchHovered}
                        setFillSwatchHovered={setFillSwatchHovered}
                        setOwnerSwatchHovered={setOwnerSwatchHovered}
                        setGuidelineLinesHovered={setGuidelineLinesHovered}
                        setGuidelinePreviewHovered={setGuidelinePreviewHovered}
                        setGuidelineDashedHovered={setGuidelineDashedHovered}
                        setPreset1Hovered={setPreset1Hovered}
                        setPreset2Hovered={setPreset2Hovered}
                        setOutlinePickerOpen={setOutlinePickerOpen}
                        setFillPickerOpen={setFillPickerOpen}
                        setOwnerPickerOpen={setOwnerPickerOpen}
                        setGuidelineLinesPickerOpen={setGuidelineLinesPickerOpen}
                        setGuidelinePreviewPickerOpen={setGuidelinePreviewPickerOpen}
                        setGuidelineDashedPickerOpen={setGuidelineDashedPickerOpen}
                        holdSlot={holdSlot}
                        holdProgress={holdProgress}
                        cancelHold={cancelHold}
                        handlePresetMouseDown={handlePresetMouseDown}
                        handlePresetMouseUp={handlePresetMouseUp}
                        outlineSwatchRef={outlineSwatchRef}
                        fillSwatchRef={fillSwatchRef}
                        ownerSwatchRef={ownerSwatchRef}
                        guidelineLinesPickerRef={guidelineLinesPickerRef}
                        guidelinePreviewPickerRef={guidelinePreviewPickerRef}
                        guidelineDashedPickerRef={guidelineDashedPickerRef}
                        handleOutlineChange={handleOutlineChange}
                        handleOwnerColorChange={handleOwnerColorChange}
                        handleFillAChange={handleFillAChange}
                        handleFillColorChange={handleFillColorChange}
                        handleOutlineThicknessChange={handleOutlineThicknessChange}
                        handleResetOutlineThickness={handleResetOutlineThickness}
                        handleGuidelineLinesColorChange={handleGuidelineLinesColorChange}
                        handleGuidelinePreviewColorChange={handleGuidelinePreviewColorChange}
                        handleGuidelineDashedColorChange={handleGuidelineDashedColorChange}
                        handleGuidelineChange={handleGuidelineChange}
                        handleResetOutline={handleResetOutline}
                        handleResetFill={handleResetFill}
                        handleResetGuidelines={handleResetGuidelines}
                        handleTogglePresetDefaults={handleTogglePresetDefaults}
                        handleRestorePresetDefaults={handleRestorePresetDefaults}
                        updateColorPickerDirection={updateColorPickerDirection}
                        updateFillPickerDirection={updateFillPickerDirection}
                        updateOwnerPickerDirection={updateOwnerPickerDirection}
                        updateGuidelineLinesPickerDirection={updateGuidelineLinesPickerDirection}
                        updateGuidelinePreviewPickerDirection={updateGuidelinePreviewPickerDirection}
                        updateGuidelineDashedPickerDirection={updateGuidelineDashedPickerDirection}
                    />

                    {!panelCollapsed && (
                        <>
                            <MochiPanelActionBar
                                text={text}
                                tt={tt}
                                ColorField={ColorField}
                                focusDisabled={focusDisabled}
                                useDarkerPanel={useDarkerPanel}
                                surfaceToolAreasSuppressed={surfaceToolAreasSuppressed}
                                specializedIndustryAreasSuppressed={specializedIndustryAreasSuppressed}
                                districtMenuOpen={districtMenuOpen}
                                districtColor={districtColor}
                                districtPickerDirection={districtPickerDirection}
                                districtSwatchHovered={districtSwatchHovered}
                                districtHoldProgress={districtHoldProgress}
                                districtPickerRef={districtPickerRef}
                                districtMenuRef={districtMenuRef}
                                districtColorSwatchRef={districtColorSwatchRef}
                                setDistrictPickerOpen={setDistrictPickerOpen}
                                setDistrictSwatchHovered={setDistrictSwatchHovered}
                                cancelDistrictHold={cancelDistrictHold}
                                openAreasToolPanel={openAreasToolPanel}
                                updateDistrictPickerDirection={updateDistrictPickerDirection}
                                handleToggleSurfaceToolAreas={handleToggleSurfaceToolAreas}
                                handleToggleSpecializedIndustryAreas={handleToggleSpecializedIndustryAreas}
                                handleDistrictMouseDownCapture={handleDistrictMouseDownCapture}
                                handleDistrictMouseUpCapture={handleDistrictMouseUpCapture}
                                handleDistrictClickCapture={handleDistrictClickCapture}
                                handleDistrictColorChange={handleDistrictColorChange}
                                handleResetDistrict={handleResetDistrict}
                            />

                            <DragGrip
                                active={panelDragging}
                                tooltip={tt(text.tooltipDraggable)}
                                tooltipSide="below"
                                onMouseDown={handlePanelDragStart}
                            />
                        </>
                    )}
                </div>
            </div>
            </SideTooltipProvider>
        </div>
    );
};
