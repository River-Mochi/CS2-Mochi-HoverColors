// <copyright file="RoadEngineerPanel.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

import React, { useCallback, useEffect, useRef, useState } from "react";
import { bindValue, trigger, useValue } from "cs2/api";
import { Button } from "cs2/ui";
import { Color } from "cs2/bindings";
import { usePanelDrag } from "../panel/hooks/usePanelDrag";
import { SideTooltip, SideTooltipProvider } from "../panel/tooltip/SideTooltip";
import { compactSwatchStyle, normalizeColorFieldValue } from "../panel/helpers/MochiPanelColorUtils";
import { VanillaComponentResolver } from "../utils/vanilla/VanillaComponentResolver";
import { CHANNEL, angleArcR$, angleArcG$, angleArcB$, roadAngleTooltipsHidden$ } from "../panel/bindings/MochiPanelBindings";
import closeIconSrc from "../../images/Close.svg";
import dragGripIcon from "../../images/mochi-drag-grip.svg";
import hcStyles from "../MochiColorPickerPanel.module.scss";
import styles from "./RoadEngineerPanel.module.scss";

const active$ = bindValue<boolean>(CHANNEL, "RoadEngineerActive", false);
const course$ = bindValue<string>(CHANNEL, "RoadEngineerCourse", "");
const badgeR$ = bindValue<number>(CHANNEL, "RoadBadgeR", 35 / 255);
const badgeG$ = bindValue<number>(CHANNEL, "RoadBadgeG", 57 / 255);
const badgeB$ = bindValue<number>(CHANNEL, "RoadBadgeB", 77 / 255);
const badgeA$ = bindValue<number>(CHANNEL, "RoadBadgeA", 1);
const customText$ = bindValue<boolean>(CHANNEL, "RoadBadgeCustomText", false);
const textR$ = bindValue<number>(CHANNEL, "RoadBadgeTextR", 1);
const textG$ = bindValue<number>(CHANNEL, "RoadBadgeTextG", 1);
const textB$ = bindValue<number>(CHANNEL, "RoadBadgeTextB", 1);
const enabled$ = bindValue<boolean>(CHANNEL, "RoadEngineerEnabled", true);
const settingsOpen$ = bindValue<boolean>(CHANNEL, "RoadEngineerSettingsOpen", false);
const settingsPinned$ = bindValue<boolean>(CHANNEL, "RoadEngineerSettingsPinned", false);
const units$ = bindValue<boolean>(CHANNEL, "RoadEngineerUseUnits", false);
const positionSet$ = bindValue<boolean>(CHANNEL, "RoadEngineerPositionSet", false);
const offsetX$ = bindValue<number>(CHANNEL, "RoadEngineerOffsetX", 0);
const offsetY$ = bindValue<number>(CHANNEL, "RoadEngineerOffsetY", 0);

const byte = (v: number) => Math.round(Math.max(0, Math.min(1, Number.isFinite(v) ? v : 0)) * 255);
const cssColor = (r: number, g: number, b: number, a: number) => "rgba(" + byte(r) + "," + byte(g) + "," + byte(b) + "," + Math.max(0, Math.min(1, Number.isFinite(a) ? a : 1)) + ")";

const nativeBackground$ = bindValue<boolean>(CHANNEL, "RoadBadgeNativeBackground", false);
const PANEL_WIDTH = 184;
type PickerDirection = "up" | "down";

export const RoadEngineerEntry = () => {
    const active = useValue(active$);
    const pinned = useValue(settingsPinned$);
    const textR = useValue(textR$);
    const textG = useValue(textG$);
    const textB = useValue(textB$);
    const r = useValue(badgeR$);
    const g = useValue(badgeG$);
    const b = useValue(badgeB$);
    const a = useValue(badgeA$);
    // Game and Editor share the document. Do not remove the shared setting
    // when either root unmounts. The stylesheet provides the initial default.
    useEffect(() => {
        document.documentElement.style.setProperty("--mochi-road-badge-color", cssColor(r, g, b, a));
    }, [r, g, b, a]);

    useEffect(() => {
        document.documentElement.style.setProperty("--mochi-road-text-color", cssColor(textR, textG, textB, 1));
    }, [textR, textG, textB]);

    return active || pinned ? <RoadEngineerPanel badge={{ r, g, b, a }} textColor={{ r: textR, g: textG, b: textB, a: 1 }} /> : null;
};

const RoadEngineerPanel = ({ badge, textColor }: { badge: Color; textColor: Color }) => {
    const customText = useValue(customText$);
    const nativeBackground = useValue(nativeBackground$);
    const expanded = useValue(settingsOpen$);
    const enabled = useValue(enabled$);
    const positionSet = useValue(positionSet$);
    const offsetX = useValue(offsetX$);
    const offsetY = useValue(offsetY$);
    const [picker, setPicker] = useState<PickerDirection | null>(null);
    const savePosition = useCallback((x: number, y: number) => {
        trigger(CHANNEL, "SetRoadEngineerPosition", Math.round(x), Math.round(y));
    }, []);
    const { panelElementRef, panelOffset, panelDragging, handlePanelDragStart } = usePanelDrag(
        "road", { left: 0, top: 0 }, positionSet ? { left: offsetX, top: offsetY } : null,
        savePosition, expanded);
    const course = useValue(course$);
    const units = useValue(units$);
    const filtered = useValue(roadAngleTooltipsHidden$);
    const arcR = useValue(angleArcR$);
    const arcG = useValue(angleArcG$);
    const arcB = useValue(angleArcB$);
    const resolver = VanillaComponentResolver.instance;
    const focus = resolver.FOCUS_DISABLED;
    const roundButton = resolver.roundHighlightButtonTheme["button"] ?? "";
    const values = course.split("|").map(Number);
    const valid = course.length > 0 && values.length === 5 && values.every(Number.isFinite);
    const distance = (v: number) => (units ? v / 8 : v).toFixed(2) + (units ? " U" : " m");
    const vertical = (v: number) => v.toFixed(2) + " m";
    const rows = [
        ["Length", valid ? distance(values[0]) : "—", "Horizontal length along the road."],
        ["Distance", valid ? distance(values[1]) : "—", "Straight-line distance between endpoints."],
        ["Elevation", valid ? vertical(values[2]) : "—", "Endpoint world height, not height above ground."],
        ["Relief", valid ? (values[3] > 0 ? "+" : "") + vertical(values[3]) : "—", "End height minus start height."],
        ["Slope", valid ? (values[4] > 0 ? "+" : "") + values[4].toFixed(2) + "%" : "—", "Average slope along the road."],
    ];
    const setBadge = (value: Color) => {
        const c = normalizeColorFieldValue(value);
        trigger(CHANNEL, "SetRoadBadgeColor", c.r, c.g, c.b, c.a);
    };
    // Use a separate body class so HC and RE cannot remove each other's picker state.
    useEffect(() => {
        document.body.classList.toggle("mochiRoadCompactPickerOpen", picker !== null);
        return () => { document.body.classList.remove("mochiRoadCompactPickerOpen"); };
    }, [picker]);
    useEffect(() => { if (!expanded) setPicker(null); }, [expanded]);
    const preview = <span className={styles.preview} style={{
        backgroundColor: nativeBackground ? "var(--tooltipColor)" : cssColor(badge.r, badge.g, badge.b, badge.a),
        color: customText ? cssColor(textColor.r, textColor.g, textColor.b, 1) : undefined,
    }}><img src="Media/Glyphs/Angle.svg" alt="" className={styles.previewIcon} />82°</span>;

    const toggle = (label: string, checked: boolean, action: () => void) =>
        <Button className={`${styles.toggle} ${checked ? styles.toggleOn : ""}`} variant="icon" focusKey={focus}
            aria-pressed={checked} aria-label={label} onSelect={action}>
            <span className={`${styles.toggleDot} ${checked ? styles.toggleDotOn : ""}`} />
            <span>{checked ? "ON" : "OFF"}</span>
        </Button>;
    const choice = (label: string, selected: boolean, action: () => void) =>
        <Button className={`${styles.choice} ${selected ? styles.choiceOn : ""}`} variant="icon"
            focusKey={focus} aria-pressed={selected} onSelect={action}>{label}</Button>;

    return <div ref={panelElementRef} className={styles.panel}
        style={{ transform: "translate(" + panelOffset.x + "px," + panelOffset.y + "px)" }}>
        <SideTooltipProvider anchorRef={panelElementRef} panelRef={panelElementRef} panelWidthRem={PANEL_WIDTH}
            disabled={picker !== null || panelDragging}>
            <div className={styles.header}>
                <SideTooltip tooltip="Drag panel" side="above" align="center">
                    <div className={styles.dragTitle} onMouseDown={handlePanelDragStart}>Road Engineer</div>
                </SideTooltip>
                <SideTooltip tooltip="Settings" side="above">
                    <Button className={`${roundButton} ${styles.iconButton} ${expanded ? styles.iconActive : ""}`}
                        variant="icon" focusKey={focus} onSelect={() => trigger(CHANNEL, "SetRoadEngineerSettingsOpen", !expanded)}
                        aria-label="Road Engineer settings" aria-pressed={expanded}>
                        <img src="Media/Glyphs/Gear.svg" className={`${styles.icon} ${styles.whiteIcon}`} alt="" />
                    </Button>
                </SideTooltip>
                <SideTooltip tooltip="Close. Reopen from HC gear." side="above">
                    <Button className={`${roundButton} ${styles.iconButton}`} variant="icon" focusKey={focus}
                        onSelect={() => trigger(CHANNEL, "CloseRoadEngineer")} aria-label="Close Road Engineer">
                        <img src={closeIconSrc} className={styles.icon} alt="" />
                    </Button>
                </SideTooltip>
            </div>
            <div className={styles.summary}>
                {rows.map(([label, value, hint]) => <SideTooltip key={label} tooltip={hint} side="left">
                    <div className={styles.row}><span className={styles.label}>{label}</span><span className={styles.value}>{value}</span></div>
                </SideTooltip>)}
            </div>
            {expanded && <div className={styles.settings}>
                <SideTooltip tooltip="Automatically show measurements when drawing roads or paths. OFF stops measurement queries; the HC gear still opens settings." side="left">
                    <div className={styles.settingRow}><span>Show panel</span>
                        {toggle("Show panel", enabled, () => trigger(CHANNEL, "SetRoadEngineerEnabled", !enabled))}
                    </div>
                </SideTooltip>
                <SideTooltip tooltip="Metres or 8 m construction units. Floating lengths change with Clutter Filter ON." side="left">
                    <div className={styles.settingRow}><span>Distance units</span><div className={styles.buttons}>
                        {choice("m", !units, () => trigger(CHANNEL, "SetRoadEngineerUseUnits", false))}
                        {choice("U", units, () => trigger(CHANNEL, "SetRoadEngineerUseUnits", true))}
                    </div></div>
                </SideTooltip>
                <SideTooltip tooltip="Live preview of the floating measurement style." side="left">
                    <div className={styles.settingRow}><span>Label preview</span>{preview}</div>
                </SideTooltip>
                <SideTooltip tooltip="Floating label background. Alpha 0% hides the background only." side="left">
                    <div className={styles.settingRow}><span>Background</span>
                        <RoadColorField value={badge} alpha={true} onChange={setBadge} onPicker={setPicker} />
                    </div>
                </SideTooltip>
                <SideTooltip tooltip="Change the floating measurement text color. White restores the native text color; icons are unchanged." side="left">
                    <div className={styles.settingRow}><span>Label text</span><div className={styles.buttons}>
                        {choice("White", !customText, () => trigger(CHANNEL, "ResetRoadBadgeTextColor"))}
                        <RoadColorField value={customText ? textColor : {r:1,g:1,b:1,a:1}} alpha={false}
                            onChange={v => trigger(CHANNEL, "SetRoadBadgeTextColor", v.r, v.g, v.b)} onPicker={setPicker} />
                    </div></div>
                </SideTooltip>
                <SideTooltip tooltip="Color of the road angle indicator." side="left">
                    <div className={styles.settingRow}><span>Angle color</span>
                        <RoadColorField value={{r:arcR,g:arcG,b:arcB,a:1}} alpha={false}
                            onChange={v => trigger(CHANNEL, "SetAngleArcColor", v.r, v.g, v.b)} onPicker={setPicker} />
                    </div>
                </SideTooltip>
                <SideTooltip tooltip="Hide unnecessary road hints and reduce measurement overlaps. Same as the HC angle button and hotkey." side="left">
                    <div className={styles.settingRow}><span>Clutter filter</span>
                        {toggle("Clutter filter", filtered, () => trigger(CHANNEL, "ToggleRoadAngleTooltips"))}
                    </div>
                </SideTooltip>
                <div className={styles.resetRow}>
                    <SideTooltip tooltip="Restore denim, native text and default distance units. Keep arc color and filter unchanged." side="left">
                        <Button className={styles.resetButton} variant="icon" focusKey={focus}
                            onSelect={() => trigger(CHANNEL, "ResetRoadAppearance", false)}>Mod defaults</Button>
                    </SideTooltip>
                    <SideTooltip tooltip="Use the game's background, text color and distance formatter. Keep arc color and filter unchanged." side="left">
                        <Button className={styles.resetButton} variant="icon" focusKey={focus}
                            onSelect={() => trigger(CHANNEL, "ResetRoadAppearance", true)}>Vanilla</Button>
                    </SideTooltip>
                </div>
            </div>}
            <div className={styles.footer}>
                {/* Upward picker can cover the main preview. Repeat the sample below
                    the swatches in reserved footer space only while that picker is open. */}
                {expanded && picker === "up" && preview}
                <div className={`${styles.dragGrip} ${panelDragging ? styles.dragging : ""}`} onMouseDown={handlePanelDragStart}>
                    <img src={dragGripIcon} alt="" />
                </div>
            </div>
        </SideTooltipProvider>
    </div>;
};

// Reuse HC's visible color shell and native click target. Match HC's direction
// selection using the actual screen rectangle, not a fixed left popup.
const RoadColorField = ({ value, alpha, onChange, onPicker }: {
    value: Color; alpha: boolean; onChange: (value: Color) => void; onPicker: (direction: PickerDirection | null) => void;
}) => {
    const resolver = VanillaComponentResolver.instance;
    const ColorField = resolver.ColorField;
    const shell = useRef<HTMLDivElement>(null);
    const [hovered, setHovered] = useState(false);
    const [direction, setDirection] = useState<PickerDirection>("down");
    const updateDirection = () => {
        const rect = shell.current?.getBoundingClientRect();
        const next = rect && rect.top + rect.height / 2 >= window.innerHeight / 2 ? "up" : "down";
        setDirection(next);
        return next;
    };
    const colorStyle = compactSwatchStyle(value, hovered, true);
    return <div ref={shell} className={`${hcStyles.guidelineColorShell} ${styles.swatch} ${hovered ? styles.swatchHovered : ""}`}
        style={colorStyle} onMouseOver={() => { setHovered(true); updateDirection(); }}
        onMouseLeave={() => setHovered(false)} onMouseDown={updateDirection}>
        <ColorField focusKey={resolver.FOCUS_DISABLED} className={hcStyles.guidelineColorField}
            value={value} alpha={alpha} popupDirection={direction} hexInput={true} hideHint={true}
            colorWheel={false} onChange={onChange} onOpenPicker={() => onPicker(updateDirection())}
            onClosePicker={() => onPicker(null)} />
        <span className={hcStyles.guidelineColorPreview} style={colorStyle} aria-hidden="true" />
    </div>;
};
