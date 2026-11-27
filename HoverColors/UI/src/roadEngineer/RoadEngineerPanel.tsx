// <copyright file="RoadEngineerPanel.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

import { bindValue, trigger, useValue } from "cs2/api";
import { useEffect, useState } from "react";
import { Button } from "cs2/ui";
import { Color } from "cs2/bindings";
import { VanillaComponentResolver } from "../utils/vanilla/VanillaComponentResolver";
import { CHANNEL, angleArcR$, angleArcG$, angleArcB$, roadAngleTooltipsHidden$ } from "../panel/bindings/MochiPanelBindings";
import styles from "./RoadEngineerPanel.module.scss";

const active$ = bindValue<boolean>(CHANNEL, "RoadEngineerActive", false);
const course$ = bindValue<string>(CHANNEL, "RoadEngineerCourse", "");
const badgeR$ = bindValue<number>(CHANNEL, "RoadBadgeR", 35 / 255);
const badgeG$ = bindValue<number>(CHANNEL, "RoadBadgeG", 57 / 255);
const badgeB$ = bindValue<number>(CHANNEL, "RoadBadgeB", 77 / 255);
const units$ = bindValue<boolean>(CHANNEL, "RoadEngineerUseUnits", false);

const byte = (v: number) => Math.round(Math.max(0, Math.min(1, Number.isFinite(v) ? v : 0)) * 255);
const cssColor = (r: number, g: number, b: number) => "rgb(" + byte(r) + "," + byte(g) + "," + byte(b) + ")";

export const RoadEngineerEntry = () => {
    const active = useValue(active$);
    const r = useValue(badgeR$);
    const g = useValue(badgeG$);
    const b = useValue(badgeB$);
    // Style lives independently of whether the gear or panel is visible.
    // No per-frame DOM scans or replacement numeric tooltip renderers.
    useEffect(() => {
        document.documentElement.style.setProperty("--mochi-road-badge-color", cssColor(r, g, b));
        return () => { document.documentElement.style.removeProperty("--mochi-road-badge-color"); };
    }, [r, g, b]);

    return active ? <RoadEngineerPanel badge={{ r, g, b, a: 1 }} /> : null;
};

const RoadEngineerPanel = ({ badge }: { badge: Color }) => {
    const [expanded, setExpanded] = useState(false);
    const course = useValue(course$);
    const units = useValue(units$);
    const filtered = useValue(roadAngleTooltipsHidden$);
    const arcR = useValue(angleArcR$);
    const arcG = useValue(angleArcG$);
    const arcB = useValue(angleArcB$);
    const resolver = VanillaComponentResolver.instance;
    const ColorField = resolver.ColorField;
    const focus = resolver.FOCUS_DISABLED;
    const values = course.split("|").map(Number);
    const valid = course.length > 0 && values.length === 5 && values.every(Number.isFinite);
    const distance = (value: number) => (units ? value / 8 : value).toFixed(2) + (units ? " U" : " m");
    const vertical = (value: number) => value.toFixed(2) + " m";
    const rows = valid ? [
        ["Total length", distance(values[0]), "Horizontal length along the preview road."],
        ["Distance", distance(values[1]), "Horizontal straight-line distance between endpoints."],
        ["Elevation", vertical(values[2]), "End point world elevation."],
        ["Relief", (values[3] > 0 ? "+" : "") + vertical(values[3]), "End minus start elevation."],
        ["Slope", (values[4] > 0 ? "+" : "") + values[4].toFixed(2) + "%", "Relief divided by horizontal course length."],
    ] : [];

    return <div className={styles.panel}>
        <div className={styles.header}>
            <span>Road Engineer</span>
            <Button variant="icon" focusKey={focus} onSelect={() => setExpanded(!expanded)}
                aria-label="Road Engineer settings" aria-pressed={expanded}>⚙</Button>
        </div>
        {valid ? rows.map(([label, value, hint]) =>
            <div key={label} className={styles.row} title={hint}><span>{label}</span><span>{value}</span></div>)
            : <div className={styles.empty}>Draw a road or path</div>}
        {expanded && <div className={styles.settings}>
            <div className={styles.row}><span>Distance units</span><div className={styles.buttons}>
                <Button focusKey={focus} onSelect={() => trigger(CHANNEL, "SetRoadEngineerUseUnits", false)} aria-pressed={!units}>m</Button>
                <Button focusKey={focus} onSelect={() => trigger(CHANNEL, "SetRoadEngineerUseUnits", true)} aria-pressed={units}>U</Button>
            </div></div>
            <div className={styles.row}><span>Label background</span>
                <ColorField focusKey={focus} className={styles.swatch} value={badge} alpha={false}
                    popupDirection="left" hexInput={true} hideHint={true}
                    onChange={value => trigger(CHANNEL, "SetRoadBadgeColor", value.r, value.g, value.b)} />
            </div>
            <div className={styles.row}><span>Angle color</span>
                <ColorField focusKey={focus} className={styles.swatch} value={{ r: arcR, g: arcG, b: arcB, a: 1 }} alpha={false}
                    popupDirection="left" hexInput={true} hideHint={true}
                    onChange={value => trigger(CHANNEL, "SetAngleArcColor", value.r, value.g, value.b)} />
            </div>
            <div className={styles.row}><span>Clutter filter (L)</span>
                <Button focusKey={focus} aria-pressed={filtered} onSelect={() => trigger(CHANNEL, "ToggleRoadAngleTooltips")}>{filtered ? "ON" : "OFF"}</Button>
            </div>
            <div className={styles.row}><span>Label preview</span><span className={styles.preview} style={{ backgroundColor: cssColor(badge.r, badge.g, badge.b) }}>∠ 82°</span></div>
            <Button focusKey={focus} onSelect={() => trigger(CHANNEL, "SetRoadBadgeColor", 35 / 255, 57 / 255, 77 / 255)}>Reset denim</Button>
        </div>}
    </div>;
};
