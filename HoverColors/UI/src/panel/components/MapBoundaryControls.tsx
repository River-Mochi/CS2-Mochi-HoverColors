// <copyright file="MapBoundaryControls.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: UI/src/panel/components/MapBoundaryControls.tsx
// Optional map-border flyout. Main Areas row stays tiny: map button + quick visibility eye.
// Compact native RGBA/hex pickers; width and dash sliders are visible immediately.

import React from "react";
import { Button } from "cs2/ui";
import { Color } from "cs2/bindings";
import { trigger, useValue } from "cs2/api";
import { CHANNEL,
    mapBoundaryTileR$, mapBoundaryTileG$, mapBoundaryTileB$, mapBoundaryTileA$,
    mapBoundaryMapR$, mapBoundaryMapG$, mapBoundaryMapB$, mapBoundaryMapA$,
    mapBoundaryTileVisible$, mapBoundaryMapVisible$,
    mapBoundaryWidth$, mapBoundaryDashScale$,
} from "../bindings/MochiPanelBindings";
import { compactSwatchStyle, normalizeColorFieldValue } from "../helpers/MochiPanelColorUtils";
import { useMochiPanelText } from "../hooks/useMochiPanelText";
import { SideTooltip, SideTooltipProvider } from "../tooltip/SideTooltip";
import { MochiSlider } from "./MochiSlider";
import mapIconSrc from "../../../images/MapBorders.svg";
import eyeOffIconSrc from "../../../images/EyeOffRedSlash.svg";
import styles from "../../MochiColorPickerPanel.module.scss";

type PanelText = ReturnType<typeof useMochiPanelText>;
type TooltipFn = (text: string) => React.ReactNode | undefined;
type PickerDirection = "up" | "down";

interface MapBoundaryControlsProps {
    text: PanelText;
    tt: TooltipFn;
    ColorField: React.ComponentType<any>;
    focusDisabled: any;
    useDarkerPanel: boolean;
    districtMenuOpen: boolean;
    onOpen: () => void;
    onPickerOpenChange: (open: boolean) => void;
}

export const MapBoundaryControls = ({
    text, tt, ColorField, focusDisabled, useDarkerPanel,
    districtMenuOpen, onOpen, onPickerOpenChange,
}: MapBoundaryControlsProps) => {
    const boundTile: Color = {
        r: useValue(mapBoundaryTileR$),
        g: useValue(mapBoundaryTileG$),
        b: useValue(mapBoundaryTileB$),
        a: useValue(mapBoundaryTileA$),
    };
    const boundMap: Color = {
        r: useValue(mapBoundaryMapR$),
        g: useValue(mapBoundaryMapG$),
        b: useValue(mapBoundaryMapB$),
        a: useValue(mapBoundaryMapA$),
    };
    const tileVisible = useValue(mapBoundaryTileVisible$);
    const mapVisible = useValue(mapBoundaryMapVisible$);
    const width = useValue(mapBoundaryWidth$);
    const dashes = useValue(mapBoundaryDashScale$);
    const allHidden = !tileVisible && !mapVisible;

    const [tileColor, setTileColor] = React.useState<Color>(boundTile);
    const [mapColor, setMapColor] = React.useState<Color>(boundMap);
    const [menuOpen, setMenuOpen] = React.useState(false);
    const [activePicker, setActivePicker] = React.useState<"tile" | "map" | null>(null);
    const [pickerDirection, setPickerDirection] = React.useState<PickerDirection>("up");
    const [menuOnLeft, setMenuOnLeft] = React.useState(false);

    const launcherRef = React.useRef<HTMLDivElement>(null);
    const menuRef = React.useRef<HTMLDivElement>(null);
    const tileSwatchRef = React.useRef<HTMLDivElement>(null);
    const mapSwatchRef = React.useRef<HTMLDivElement>(null);

    React.useEffect(() => {
        setTileColor(boundTile);
    }, [boundTile.r, boundTile.g, boundTile.b, boundTile.a]);
    React.useEffect(() => {
        setMapColor(boundMap);
    }, [boundMap.r, boundMap.g, boundMap.b, boundMap.a]);

    const closePicker = React.useCallback(() => {
        setActivePicker(null);
        onPickerOpenChange(false);
    }, [onPickerOpenChange]);

    React.useEffect(() => {
        if (!districtMenuOpen) return;
        setMenuOpen(false);
        closePicker();
    }, [districtMenuOpen, closePicker]);

    React.useEffect(() => {
        if (!menuOpen) return;
        const onOutside = (event: MouseEvent) => {
            const target = event.target as Element | null;
            if (target == null ||
                launcherRef.current?.contains(target) ||
                menuRef.current?.contains(target) ||
                target.closest(".color-picker-container_Sj5")) return;
            setMenuOpen(false);
            closePicker();
        };
        document.addEventListener("mousedown", onOutside);
        return () => document.removeEventListener("mousedown", onOutside);
    }, [menuOpen, closePicker]);

    React.useEffect(() => () => onPickerOpenChange(false), [onPickerOpenChange]);

    const updatePickerDirection = (target: HTMLElement | null) => {
        if (target == null) return;
        const rect = target.getBoundingClientRect();
        setPickerDirection(rect.top + rect.height / 2 < window.innerHeight / 2 ? "down" : "up");
    };

    const openPicker = (type: "tile" | "map", target: HTMLElement | null) => {
        updatePickerDirection(target);
        setActivePicker(type);
        onPickerOpenChange(true);
    };

    const handleOpen = () => {
        if (menuOpen) {
            setMenuOpen(false);
            closePicker();
        } else {
            // Open outside the main panel, flip side for right-edge launchers.
            const actions = launcherRef.current?.parentElement?.parentElement;
            const rect = actions?.getBoundingClientRect();
            setMenuOnLeft(rect != null && window.innerWidth - rect.right < 250);
            onOpen();
            setMenuOpen(true);
        }
    };

    const handleTileChange = (value: Color) => {
        const color = normalizeColorFieldValue(value);
        setTileColor(color);
        trigger(CHANNEL, "SetMapBoundaryTileColor", color.r, color.g, color.b, color.a);
    };
    const handleMapChange = (value: Color) => {
        const color = normalizeColorFieldValue(value);
        setMapColor(color);
        trigger(CHANNEL, "SetMapBoundaryMapColor", color.r, color.g, color.b, color.a);
    };

    return (
        <>
            <div ref={launcherRef} className={styles.mapBoundaryLauncher}>
                <SideTooltip tooltip={tt(text.tooltipMapBoundary)} side="below">
                    <Button
                        className={`${styles.actionButton} ${styles.surfaceButton} ${menuOpen ? styles.mapBoundaryButtonActive : ""}`}
                        variant="icon"
                        onSelect={handleOpen}
                        focusKey={focusDisabled}
                        aria-label={text.mapBoundaryTitle}
                        aria-expanded={menuOpen}
                    >
                        <img src={mapIconSrc} className={styles.mapBoundaryIcon} alt="" />
                    </Button>
                </SideTooltip>
                <SideTooltip tooltip={tt(text.tooltipMapBoundaryVisibility)} side="below">
                    <Button
                        className={`${styles.actionButton} ${styles.surfaceButton} ${styles.mapBoundaryEyeButton} ${allHidden ? styles.mapBoundaryEyeOff : ""}`}
                        variant="icon"
                        onSelect={() => trigger(CHANNEL, "ToggleMapBoundaryVisibility")}
                        focusKey={focusDisabled}
                        aria-label={text.tooltipMapBoundaryVisibility}
                        aria-pressed={allHidden}
                    >
                        <img src={allHidden ? eyeOffIconSrc : "Media/PhotoMode/HideUIOff.svg"}
                            className={styles.mapBoundaryEyeIcon} alt="" />
                    </Button>
                </SideTooltip>
            </div>

            {menuOpen && (
                <div ref={menuRef} className={`${styles.mapBoundaryMenu} ${menuOnLeft ? styles.mapBoundaryMenuLeft : ""}`}>
                    <SideTooltipProvider anchorRef={menuRef} panelRef={menuRef}>
                    <div className={styles.mapBoundaryMenuHeader}>
                        <div className={styles.mapBoundaryMenuTitle}>{text.mapBoundaryTitle}</div>
                        <SideTooltip tooltip={tt(text.tooltipMapBoundaryReset)} side={menuOnLeft ? "below" : "right"}>
                            <Button className={styles.mapBoundaryReset}
                                onSelect={() => trigger(CHANNEL, "ResetMapBoundary")}
                                focusKey={focusDisabled}>
                                {text.mapBoundaryReset}
                            </Button>
                        </SideTooltip>
                    </div>
                    <div className={styles.mapBoundaryColorRow}>
                        <SideTooltip tooltip={tt(text.tooltipMapBoundaryTile)} side={menuOnLeft ? "below" : "right"}>
                            <div ref={tileSwatchRef} className={styles.mapBoundarySwatch}
                                onMouseOver={() => updatePickerDirection(tileSwatchRef.current)}
                                onMouseDown={() => updatePickerDirection(tileSwatchRef.current)}
                                style={compactSwatchStyle(tileColor, activePicker === "tile", useDarkerPanel)}>
                                <span className={styles.mapBoundarySwatchPreview}
                                    style={compactSwatchStyle(tileColor)} aria-hidden="true" />
                                <ColorField
                                    focusKey={focusDisabled}
                                    className={styles.mapBoundaryColorField}
                                    value={tileColor}
                                    alpha={true}
                                    hexInput={true}
                                    colorWheel={false}
                                    popupDirection={pickerDirection}
                                    hideHint={true}
                                    onChange={handleTileChange}
                                    onOpenPicker={() => openPicker("tile", tileSwatchRef.current)}
                                    onClosePicker={closePicker}
                                />
                            </div>
                        </SideTooltip>
                        <span className={styles.mapBoundaryColorLabel}>{text.mapBoundaryTile}</span>
                        <SideTooltip tooltip={tt(text.tooltipMapBoundaryTileVisible)} side={menuOnLeft ? "below" : "right"}>
                            <Button className={styles.mapBoundarySmallEye} variant="icon"
                                onSelect={() => trigger(CHANNEL, "SetMapBoundaryTileVisible", !tileVisible)}
                                focusKey={focusDisabled}
                                aria-pressed={!tileVisible}
                                aria-label={text.tooltipMapBoundaryTileVisible}>
                                <img src={tileVisible ? "Media/PhotoMode/HideUIOff.svg" : eyeOffIconSrc}
                                    className={styles.mapBoundarySmallEyeIcon} alt="" />
                            </Button>
                        </SideTooltip>
                    </div>

                    <div className={styles.mapBoundaryColorRow}>
                        <SideTooltip tooltip={tt(text.tooltipMapBoundaryMap)} side={menuOnLeft ? "below" : "right"}>
                            <div ref={mapSwatchRef} className={styles.mapBoundarySwatch}
                                onMouseOver={() => updatePickerDirection(mapSwatchRef.current)}
                                onMouseDown={() => updatePickerDirection(mapSwatchRef.current)}
                                style={compactSwatchStyle(mapColor, activePicker === "map", useDarkerPanel)}>
                                <span className={styles.mapBoundarySwatchPreview}
                                    style={compactSwatchStyle(mapColor)} aria-hidden="true" />
                                <ColorField
                                    focusKey={focusDisabled}
                                    className={styles.mapBoundaryColorField}
                                    value={mapColor}
                                    alpha={true}
                                    hexInput={true}
                                    colorWheel={false}
                                    popupDirection={pickerDirection}
                                    hideHint={true}
                                    onChange={handleMapChange}
                                    onOpenPicker={() => openPicker("map", mapSwatchRef.current)}
                                    onClosePicker={closePicker}
                                />
                            </div>
                        </SideTooltip>
                        <span className={styles.mapBoundaryColorLabel}>{text.mapBoundaryMap}</span>
                        <SideTooltip tooltip={tt(text.tooltipMapBoundaryMapVisible)} side={menuOnLeft ? "below" : "right"}>
                            <Button className={styles.mapBoundarySmallEye} variant="icon"
                                onSelect={() => trigger(CHANNEL, "SetMapBoundaryMapVisible", !mapVisible)}
                                focusKey={focusDisabled}
                                aria-pressed={!mapVisible}
                                aria-label={text.tooltipMapBoundaryMapVisible}>
                                <img src={mapVisible ? "Media/PhotoMode/HideUIOff.svg" : eyeOffIconSrc}
                                    className={styles.mapBoundarySmallEyeIcon} alt="" />
                            </Button>
                        </SideTooltip>
                    </div>

                    <div className={styles.mapBoundarySliders}>
                        <SideTooltip tooltip={tt(text.tooltipMapBoundaryWidth)} side={menuOnLeft ? "below" : "right"}>
                            <div className={styles.mapBoundarySliderBlock}>
                                <div className={styles.mapBoundarySliderTitle}>
                                    <span>{text.mapBoundaryWidth}</span><span>{width.toFixed(0)}</span>
                                </div>
                                <MochiSlider value={width} start={1} end={200}
                                    gamepadStep={1} step={1}
                                    onChange={(v: number) => trigger(CHANNEL, "SetMapBoundaryWidth", v)}
                                    focusKey={focusDisabled} className={styles.mapBoundarySlider} />
                            </div>
                        </SideTooltip>
                        <SideTooltip tooltip={tt(text.tooltipMapBoundaryDashes)} side={menuOnLeft ? "below" : "right"}>
                            <div className={styles.mapBoundarySliderBlock}>
                                <div className={styles.mapBoundarySliderTitle}>
                                    <span>{text.mapBoundaryDashes}</span><span>{dashes.toFixed(1)}×</span>
                                </div>
                                <MochiSlider value={dashes} start={0.5} end={2}
                                    gamepadStep={0.1} step={0.1}
                                    onChange={(v: number) => trigger(CHANNEL, "SetMapBoundaryDashScale", v)}
                                    focusKey={focusDisabled} className={styles.mapBoundarySlider} />
                            </div>
                        </SideTooltip>
                    </div>
                    </SideTooltipProvider>
                </div>
            )}
        </>
    );
};
