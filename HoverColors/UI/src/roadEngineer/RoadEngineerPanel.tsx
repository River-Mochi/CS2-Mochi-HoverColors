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
import dragGripHoverIcon from "../../images/mochi-drag-grip-hover.svg";
import hcStyles from "../MochiColorPickerPanel.module.scss";
import styles from "./RoadEngineerPanel.module.scss";

const active$ = bindValue<boolean>(CHANNEL, "RoadEngineerActive", false);
const course$ = bindValue<string>(CHANNEL, "RoadEngineerCourse", "");
const badgeR$ = bindValue<number>(CHANNEL, "RoadBadgeR", 8 / 255);
const badgeG$ = bindValue<number>(CHANNEL, "RoadBadgeG", 41 / 255);
const badgeB$ = bindValue<number>(CHANNEL, "RoadBadgeB", 2 / 255);
const badgeA$ = bindValue<number>(CHANNEL, "RoadBadgeA", 1);
const customText$ = bindValue<boolean>(CHANNEL, "RoadBadgeCustomText", true);
const textR$ = bindValue<number>(CHANNEL, "RoadBadgeTextR", 1);
const textG$ = bindValue<number>(CHANNEL, "RoadBadgeTextG", 235 / 255);
const textB$ = bindValue<number>(CHANNEL, "RoadBadgeTextB", 128 / 255);
const enabled$ = bindValue<boolean>(CHANNEL, "RoadEngineerEnabled", true);
const matchHC$ = bindValue<boolean>(CHANNEL, "RoadEngineerMatchHC", false);
const panelOpacity$ = bindValue<number>(CHANNEL, "RoadEngineerPanelOpacity", 100);
const settingsOpen$ = bindValue<boolean>(CHANNEL, "RoadEngineerSettingsOpen", false);
const settingsPinned$ = bindValue<boolean>(CHANNEL, "RoadEngineerSettingsPinned", false);
const units$ = bindValue<boolean>(CHANNEL, "RoadEngineerUseUnits", false);
const distanceDecimals$ = bindValue<number>(CHANNEL, "RoadDistanceDecimals", 2);
const angleDecimals$ = bindValue<number>(CHANNEL, "RoadAngleDecimals", 2);
const preciseFloating$ = bindValue<boolean>(CHANNEL, "RoadPreciseFloatingLabels", false);
const labelFontPercent$ = bindValue<number>(CHANNEL, "RoadLabelFontPercent", 100);
const unitSettings$ = bindValue<{unitSystem: number}>("options", "unitSettings", {unitSystem: 0});
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
    const matchHC = useValue(matchHC$);
    const panelAlpha = useValue(panelOpacity$) / 100;
    const [confirmOff, setConfirmOff] = useState(false);
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
    const distanceDecimals = useValue(distanceDecimals$);
    const angleDecimals = useValue(angleDecimals$);
    const preciseFloating = useValue(preciseFloating$);
    const labelFontPercent = useValue(labelFontPercent$);
    const imperial = useValue(unitSettings$)?.unitSystem === 1;
    const filtered = useValue(roadAngleTooltipsHidden$);
    const arcR = useValue(angleArcR$);
    const arcG = useValue(angleArcG$);
    const arcB = useValue(angleArcB$);
    const resolver = VanillaComponentResolver.instance;
    const focus = resolver.FOCUS_DISABLED;
    const roundButton = resolver.roundHighlightButtonTheme["button"] ?? "";
    const values = course.split("|").map(Number);
    const valid = course.length > 0 && values.length === 9 && values.slice(0, 5).every(Number.isFinite);
    const distance = (v: number) => (units ? v / 8 : imperial ? v / 0.9144 : v).toFixed(distanceDecimals) + (units ? " U" : imperial ? " yd" : " m");
    const vertical = (v: number) => (imperial ? v / 0.3048 : v).toFixed(distanceDecimals) + (imperial ? " ft" : " m");
    const signed = (v: number) => (v > 0 ? "+" : "") + vertical(v);
    const compassPoints = ["N", "NE", "E", "SE", "S", "SW", "W", "NW"];
    const bearing = valid && Number.isFinite(values[6])
        ? (Number(values[6].toFixed(angleDecimals)) % 360).toFixed(angleDecimals) + "°"
        : "—";
    const compass = valid && Number.isFinite(values[6]) ? compassPoints[Math.round(values[6] / 45) % 8] : "";
    const gradeKnown = valid && Number.isFinite(values[7]) && Number.isFinite(values[8]) && values[8] > 0;
    const ratio = gradeKnown ? values[7] / values[8] : 0;
    const gradeColor = !gradeKnown ? "#87939b" : ratio >= 1 ? "#ef5350" : ratio >= 0.75 ? "#f5ce42" : "#55d881";
    const gradeHint = gradeKnown
        ? "Average slope. Steepest preview course: " + values[7].toFixed(2) + "%. Prefab limit: " + values[8].toFixed(2) + "%. Advisory, not final game validation."
        : "Average slope. No verified prefab grade limit.";
    const rows = [
        ["Length", valid ? distance(values[0]) : "—", "Horizontal length along the road."],
        ["Distance", valid ? distance(values[1]) : "—", "Straight-line distance between endpoints."],
        ["Elevation", valid ? vertical(values[2]) : "—", "Endpoint world height, not height above ground."],
        ["Height/depth", valid && Number.isFinite(values[5]) ? signed(values[5]) : "—", "Endpoint height above (+) or below (−) terrain. Uses the cached terrain surface."],
        ["Relief", valid ? (values[3] > 0 ? "+" : "") + vertical(values[3]) : "—", "End height minus start height."],
        ["Slope", valid ? (values[4] > 0 ? "+" : "") + values[4].toFixed(distanceDecimals) + "%" : "—", gradeHint],
        ["Direction", bearing, "Map-relative start-to-end bearing. Independent of camera rotation."],
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
    useEffect(() => { if (!expanded) { setPicker(null); } }, [expanded]);
    useEffect(() => { if (!enabled) setConfirmOff(false); }, [enabled]);
    const preview = <span className={styles.preview} style={{
        backgroundColor: nativeBackground ? "var(--tooltipColor)" : cssColor(badge.r, badge.g, badge.b, badge.a),
        color: customText ? cssColor(textColor.r, textColor.g, textColor.b, 1) : undefined,
    }}><img src="Media/Glyphs/Angle.svg" alt="" className={styles.previewIcon} /><span style={{ fontSize: labelFontPercent + "%" }}>{(82.347).toFixed(filtered && preciseFloating ? angleDecimals : 0)}°</span></span>;

    const toggle = (label: string, checked: boolean, action: () => void, disabled = false) =>
        <Button className={`${styles.toggle} ${checked ? styles.toggleOn : ""}`} variant="icon" focusKey={focus}
            aria-pressed={checked} aria-label={label} disabled={disabled} onSelect={action}>
            <span className={`${styles.toggleDot} ${checked ? styles.toggleDotOn : ""}`} />
            <span>{checked ? "ON" : "OFF"}</span>
        </Button>;
    const choice = (label: string, selected: boolean, action: () => void, disabled = false) =>
        <Button className={`${styles.choice} ${label.length > 2 ? styles.wideChoice : ""} ${selected ? styles.choiceOn : ""}`} variant="icon"
            focusKey={focus} disabled={disabled} aria-pressed={selected} onSelect={action}>{label}</Button>;

    return <div ref={panelElementRef} className={styles.panel}
        style={{ transform: "translate(" + panelOffset.x + "px," + panelOffset.y + "px)",
            backgroundColor: matchHC ? "transparent" : cssColor(20 / 255, 27 / 255, 34 / 255, panelAlpha),
            backgroundImage: matchHC ? "linear-gradient(" + cssColor(39 / 255, 61 / 255, 72 / 255, panelAlpha) + "," + cssColor(29 / 255, 42 / 255, 59 / 255, panelAlpha) + ")" : "none",
        }}>
        <SideTooltipProvider anchorRef={panelElementRef} panelRef={panelElementRef} panelWidthRem={PANEL_WIDTH}
            disabled={picker !== null || panelDragging || confirmOff} preferOppositeSide={true} adaptivePanelWidth={true}>
            <div className={styles.header}>
                <SideTooltip tooltip="Drag panel" side="above" align="center">
                    <div className={styles.dragTitle} onMouseDown={handlePanelDragStart}>Road Engineer</div>
                </SideTooltip>
                <SideTooltip tooltip="Settings" side="above">
                    <Button className={`${roundButton} ${styles.iconButton} ${expanded ? styles.iconActive : ""}`}
                        variant="icon" focusKey={focus} onSelect={() => trigger(CHANNEL, "SetRoadEngineerSettingsOpen", !expanded)}
                        aria-label="Road Engineer settings" aria-pressed={expanded}>
                        <img src="Media/Glyphs/Gear.svg" className={`${styles.icon} ${styles.whiteIcon} ${styles.gearIcon}`} alt="" />
                    </Button>
                </SideTooltip>
                <SideTooltip tooltip="Close for now. Reopens next road-tool session if Auto show is ON." side="above">
                    <Button className={`${roundButton} ${styles.iconButton}`} variant="icon" focusKey={focus}
                        onSelect={() => trigger(CHANNEL, "CloseRoadEngineer")} aria-label="Close Road Engineer">
                        <img src={closeIconSrc} className={styles.icon} alt="" />
                    </Button>
                </SideTooltip>
            </div>
            {confirmOff && enabled && <div className={styles.confirmBox}>
                <span className={styles.confirmTitle}>Are you sure?</span>
                <span className={styles.confirmHint}>Disable Auto show panel?</span>
                <div className={styles.confirmActions}>
                    <Button className={styles.confirmButton} variant="icon" focusKey={focus}
                        onSelect={() => setConfirmOff(false)}>No</Button>
                    <Button className={`${styles.confirmButton} ${styles.confirmYes}`} variant="icon" focusKey={focus}
                        onSelect={() => { setConfirmOff(false); trigger(CHANNEL, "SetRoadEngineerEnabled", false); }}>Yes</Button>
                </div>
            </div>}
            <div className={styles.summary}>
                {rows.map(([label, value, hint]) => <SideTooltip key={label} tooltip={hint} side="left">
                    <div className={styles.row}>
                        <span className={styles.label}>{label}{label === "Slope" && valid && <span className={styles.gradeDot} style={{ backgroundColor: gradeColor }} />}</span>
                        <span className={styles.value}>{label === "Direction" && compass && <><span className={styles.compass}>{compass}</span><span className={styles.bearing}>{value}</span></>}{(label !== "Direction" || !compass) && value}</span>
                    </div>
                </SideTooltip>)}
            </div>
            {expanded && <div className={styles.settings}>
                <SideTooltip tooltip="Increase floating measurement text relative to vanilla. Summary text is unchanged. Requires Clutter Filter." side="right">
                    <div className={styles.settingRow}><span>Label Font</span><div className={styles.buttons}>
                        {choice("−", false, () => trigger(CHANNEL, "SetRoadLabelFontPercent", labelFontPercent - 10), labelFontPercent <= 100)}
                        <span className={styles.sizeValue}>{labelFontPercent}%</span>
                        {choice("+", false, () => trigger(CHANNEL, "SetRoadLabelFontPercent", labelFontPercent + 10), labelFontPercent >= 120)}
                    </div></div>
                </SideTooltip>
                <SideTooltip tooltip="Floating label background. Alpha 0% hides the background only." side="right">
                    <div className={styles.settingRow}><span>Background</span>
                        <RoadColorField value={badge} alpha={true} onChange={setBadge} onPicker={setPicker} />
                    </div>
                </SideTooltip>
                <SideTooltip tooltip="Change the floating measurement text color. White restores the native text color; icons are unchanged." side="right">
                    <div className={styles.settingRow}><span>Label text</span><div className={styles.buttons}>
                        {choice("White", !customText, () => trigger(CHANNEL, "ResetRoadBadgeTextColor"))}
                        <RoadColorField value={customText ? textColor : {r:1,g:1,b:1,a:1}} alpha={false}
                            onChange={v => trigger(CHANNEL, "SetRoadBadgeTextColor", v.r, v.g, v.b)} onPicker={setPicker} />
                    </div></div>
                </SideTooltip>
                <SideTooltip tooltip="Color of the angle arc and right-angle guide in the world, not the white angle icon inside the label." side="right">
                    <div className={styles.settingRow}><span>Arc color</span>
                        <RoadColorField value={{r:arcR,g:arcG,b:arcB,a:1}} alpha={false}
                            onChange={v => trigger(CHANNEL, "SetAngleArcColor", v.r, v.g, v.b)} onPicker={setPicker} />
                    </div>
                </SideTooltip>
                <SideTooltip tooltip="Preview of the floating measurement label. Its white icon is separate from the world arc color." side="right">
                    <div className={`${styles.settingRow} ${styles.previewRow}`}>{preview}<span>Label preview</span></div>
                </SideTooltip>
                <SideTooltip tooltip="Metres or 8 m construction units. Floating lengths change with Clutter Filter ON." side="right">
                    <div className={styles.settingRow}><span>Distance units</span><div className={styles.buttons}>
                        {choice(imperial ? "yd" : "m", !units, () => trigger(CHANNEL, "SetRoadEngineerUseUnits", false))}
                        {choice("U", units, () => trigger(CHANNEL, "SetRoadEngineerUseUnits", true))}
                    </div></div>
                </SideTooltip>
                <SideTooltip tooltip="Decimals for summary lengths, heights and slope. Also used for floating lengths when Precise labels is ON." side="right">
                    <div className={styles.settingRow}><span>Length decimals</span><div className={styles.buttons}>
                        {[0,1,2].map(n => <React.Fragment key={n}>{choice(String(n), distanceDecimals === n, () => trigger(CHANNEL, "SetRoadPrecision", false, n))}</React.Fragment>)}
                    </div></div>
                </SideTooltip>
                <SideTooltip tooltip="Decimals for bearing and verified Point of Intersection (PI) angles when Precise labels is ON." side="right">
                    <div className={styles.settingRow}><span>Angle decimals</span><div className={styles.buttons}>
                        {[0,1,2].map(n => <React.Fragment key={n}>{choice(String(n), angleDecimals === n, () => trigger(CHANNEL, "SetRoadPrecision", true, n))}</React.Fragment>)}
                    </div></div>
                </SideTooltip>
                <SideTooltip tooltip="OFF: compact floating labels. ON: use the chosen decimals where verified geometry is available. Requires Clutter Filter. Summary precision and m/U are independent." side="right">
                    <div className={styles.settingRow}><span>Precise labels</span>
                        {toggle("Precise floating labels", filtered && preciseFloating, () => trigger(CHANNEL, "SetRoadPreciseFloatingLabels", !preciseFloating), !filtered)}
                    </div>
                </SideTooltip>
                <SideTooltip tooltip="Hide unnecessary road hints and reduce measurement overlaps. Same as the HC angle button and hotkey." side="right">
                    <div className={styles.settingRow}><span>Clutter filter</span>
                        {toggle("Clutter filter", filtered, () => trigger(CHANNEL, "ToggleRoadAngleTooltips"))}
                    </div>
                </SideTooltip>
                <SideTooltip tooltip="Auto-open when drawing roads. OFF disables automatic opening only. Open manually from HC gear; X closes for now." side="right">
                    <div className={styles.settingRow}><span>Auto show panel</span>
                        {toggle("Auto show panel", enabled, () => {
                            if (enabled) setConfirmOff(true);
                            else trigger(CHANNEL, "SetRoadEngineerEnabled", true);
                        })}
                    </div>
                </SideTooltip>
                <div className={styles.resetRow}>
                    <SideTooltip tooltip="Restore green background, yellow text, default units, two summary decimals, compact floating labels and native text size. Keep arc color and filter unchanged." side="right">
                        <Button className={styles.resetButton} variant="icon" focusKey={focus}
                            onSelect={() => trigger(CHANNEL, "ResetRoadAppearance", false)}>Mod defaults</Button>
                    </SideTooltip>
                    <SideTooltip tooltip="Use the game's background, text color and floating precision. Keep arc color and filter unchanged." side="right">
                        <Button className={styles.resetButton} variant="icon" focusKey={focus}
                            onSelect={() => trigger(CHANNEL, "ResetRoadAppearance", true)}>Vanilla</Button>
                    </SideTooltip>
                </div>
            </div>}
            <div className={styles.footer}>
                <div className={`${styles.dragGrip} ${panelDragging ? styles.dragging : ""}`} onMouseDown={handlePanelDragStart}>
                    <img src={dragGripIcon} className={styles.gripNormal} alt="" />
                    <img src={dragGripHoverIcon} className={styles.gripHover} alt="" />
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
        // Prefer below; use the available screen height rather than the midpoint.
        const rem = Math.max(0.65, (shell.current?.getBoundingClientRect().width ?? 20) / 20);
        const needed = (alpha ? 225 : 190) * rem + 16;
        const next = rect && window.innerHeight - rect.bottom < needed && rect.top > window.innerHeight - rect.bottom ? "up" : "down";
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
