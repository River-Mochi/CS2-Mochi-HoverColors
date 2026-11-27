// <copyright file="RoadEngineerPanel.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

import { bindValue, trigger, useValue } from "cs2/api";
import { useCallback, useEffect } from "react";
import { usePanelDrag } from "../panel/hooks/usePanelDrag";
import dragGripIcon from "../../images/mochi-drag-grip.svg";
import { Button, Tooltip } from "cs2/ui";
import hcStyles from "../MochiColorPickerPanel.module.scss";
import { compactSwatchStyle, normalizeColorFieldValue } from "../panel/helpers/MochiPanelColorUtils";
import { Color } from "cs2/bindings";
import { VanillaComponentResolver } from "../utils/vanilla/VanillaComponentResolver";
import { CHANNEL, angleArcR$, angleArcG$, angleArcB$, roadAngleTooltipsHidden$ } from "../panel/bindings/MochiPanelBindings";
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
    const expanded = useValue(settingsOpen$);
    const enabled = useValue(enabled$);
    const setExpanded = (open: boolean) => trigger(CHANNEL, "SetRoadEngineerSettingsOpen", open);
    const positionSet = useValue(positionSet$);
    const offsetX = useValue(offsetX$);
    const offsetY = useValue(offsetY$);
    const savePosition = useCallback((x: number, y: number) => {
        trigger(CHANNEL, "SetRoadEngineerPosition", Math.round(x), Math.round(y));
    }, []);
    // Saved offsets are relative to the top-right CSS anchor, not the HC panel.
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
    const values = course.split("|").map(Number);
    const valid = course.length > 0 && values.length === 5 && values.every(Number.isFinite);
    const distance = (value: number) => (units ? value / 8 : value).toFixed(2) + (units ? " U" : " m");
    const vertical = (value: number) => value.toFixed(2) + " m";
    // Keep every row mounted, even when the tool has no valid preview.
    const rows = [
        ["Total length", valid ? distance(values[0]) : "—", "Length along the road."],
        ["Distance", valid ? distance(values[1]) : "—", "Straight-line distance."],
        ["Elevation", valid ? vertical(values[2]) : "—", "End point world elevation."],
        ["Relief", valid ? (values[3] > 0 ? "+" : "") + vertical(values[3]) : "—", "End minus start elevation."],
        ["Slope", valid ? (values[4] > 0 ? "+" : "") + values[4].toFixed(2) + "%" : "—", "Average slope."],
    ];
    const panelBase = resolver.panelBaseTheme;
    const panelTheme = resolver.panelTheme;
    const menuTheme = resolver.infoviewMenuTheme;
    const setBadge = (value: Color) => {
        const color = normalizeColorFieldValue(value);
        trigger(CHANNEL, "SetRoadBadgeColor", color.r, color.g, color.b, color.a);
    };

    return <div ref={panelElementRef} className={`${panelBase.panel ?? "panel_YqS"} ${menuTheme.menu ?? "menu_O_M"} ${hcStyles.panelFrame} ${styles.panel}`}
        style={{ position: "fixed", top: "72rem", right: "18rem", transform: "translate(" + panelOffset.x + "px," + panelOffset.y + "px)" }}>
        <div className={`${panelTheme.content ?? "content_XD5 content_AD7 child-opacity-transition_nkS"} ${menuTheme.content ?? "content_Hzl"} ${hcStyles.panelContent} ${hcStyles.panelDarker} ${styles.content}`}>
        <div className={`${hcStyles.titleBar} ${styles.header}`}>
            <div className={`${hcStyles.titleDragHandle} ${styles.dragTitle}`} onMouseDown={handlePanelDragStart}><span className={hcStyles.titleText}>Road Engineer</span></div>
            <Button className={styles.iconButton} variant="icon" focusKey={focus} onSelect={() => setExpanded(!expanded)}
                tooltipLabel="Settings" aria-label="Road Engineer settings" aria-pressed={expanded}>
                <img src="Media/Glyphs/Gear.svg" className={styles.icon} alt="" />
            </Button>
            <Button className={styles.iconButton} variant="icon" focusKey={focus} onSelect={() => trigger(CHANNEL, "CloseRoadEngineer")}
                tooltipLabel="Close panel. Reopen from HC gear." aria-label="Close Road Engineer">
                <img src="Media/Glyphs/Close.svg" className={styles.icon} alt="" />
            </Button>
        </div>
        <div className={styles.body}>
        {rows.map(([label, value, hint]) =>
            <Tooltip key={label} tooltip={hint} direction="left">
                <div className={styles.row}><span className={styles.label}>{label}</span><span className={styles.value}>{value}</span></div>
            </Tooltip>)}
        {expanded && <div className={styles.settings}>
            <div className={styles.row}><span>Show panel</span>
                <Button className={styles.optionButton} variant="flat" focusKey={focus} selected={enabled}
                    onSelect={() => trigger(CHANNEL, "SetRoadEngineerEnabled", !enabled)}>{enabled ? "ON" : "OFF"}</Button>
            </div>
            <div className={styles.row}><span>Distance units</span><div className={styles.buttons}>
                <Button className={styles.optionButton} variant="flat" focusKey={focus} selected={!units} onSelect={() => trigger(CHANNEL, "SetRoadEngineerUseUnits", false)}>m</Button>
                <Button className={styles.optionButton} variant="flat" focusKey={focus} selected={units} onSelect={() => trigger(CHANNEL, "SetRoadEngineerUseUnits", true)}>U</Button>
            </div></div>
            <div className={styles.row}><span>Label background</span>
                <RoadColorField value={badge} alpha={true} onChange={setBadge} />
            </div>
            <div className={styles.row}><span>Label text</span><div className={styles.buttons}>
                <Button className={styles.optionButton} variant="flat" focusKey={focus} selected={!customText}
                    tooltipLabel="Restore native text color" onSelect={() => trigger(CHANNEL, "ResetRoadBadgeTextColor")}>Auto</Button>
                <RoadColorField value={textColor} alpha={false} onChange={value => trigger(CHANNEL, "SetRoadBadgeTextColor", value.r, value.g, value.b)} />
            </div></div>
            <div className={styles.row}><span>Angle color</span>
                <RoadColorField value={{ r: arcR, g: arcG, b: arcB, a: 1 }} alpha={false}
                    onChange={value => trigger(CHANNEL, "SetAngleArcColor", value.r, value.g, value.b)} />
            </div>
            <div className={styles.row}><span>Clutter filter</span>
                <Button className={styles.optionButton} variant="flat" focusKey={focus} selected={filtered} onSelect={() => trigger(CHANNEL, "ToggleRoadAngleTooltips")}>{filtered ? "ON" : "OFF"}</Button>
            </div>
            <div className={styles.row}><span>Label preview</span><span className={styles.preview}
                style={{ backgroundColor: cssColor(badge.r, badge.g, badge.b, badge.a),
                    color: customText ? cssColor(textColor.r, textColor.g, textColor.b, 1) : undefined }}>∠ 82°</span></div>
            <Button className={styles.resetButton} variant="flat" focusKey={focus}
                onSelect={() => { trigger(CHANNEL, "SetRoadBadgeColor", 35 / 255, 57 / 255, 77 / 255, 1); trigger(CHANNEL, "ResetRoadBadgeTextColor"); }}>Reset colors</Button>
        </div>}
        </div>
        <div className={styles.dragFooter}>
            <div className={styles.dragGrip} onMouseDown={handlePanelDragStart}
                style={{ cursor: panelDragging ? "grabbing" : "grab" }}>
                <img src={dragGripIcon} alt="" />
            </div>
        </div>
        </div>
    </div>;
};

// Same visible shell and native ColorField click target as the HC swatches.
const RoadColorField = ({ value, alpha, onChange }: { value: Color; alpha: boolean; onChange: (value: Color) => void }) => {
    const resolver = VanillaComponentResolver.instance;
    const ColorField = resolver.ColorField;
    return <div className={`${hcStyles.guidelineColorShell} ${styles.swatch}`}>
        <ColorField focusKey={resolver.FOCUS_DISABLED} className={hcStyles.guidelineColorField}
            value={value} alpha={alpha} popupDirection="left" hexInput={true} hideHint={true}
            colorWheel={false} onChange={onChange} />
        <span className={hcStyles.guidelineColorPreview} style={compactSwatchStyle(value, false, true)} aria-hidden="true" />
    </div>;
};
