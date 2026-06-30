// File: UI/src/panel/components/MochiPanelPieces.tsx
// Purpose: Small reusable pieces for MochiColorPickerPanel.tsx.

import React from "react";
import { Color } from "cs2/bindings";
import { SideTooltip, type SideTooltipSide } from "../tooltip/SideTooltip";
import styles from "../../MochiColorPickerPanel.module.scss";

type PresetSlotButtonProps = {
    slot: 1 | 2;
    color: Color;
    active: boolean;
    holdActive: boolean;
    holdProgress: number;
    tooltip?: React.ReactNode;
    tooltipSide?: SideTooltipSide;
    marginLeft: string;
    numberColor: string;
    presetPreviewStyle: (color: Color) => React.CSSProperties;
    holdBarStyle: (progress: number) => React.CSSProperties;
    onMouseEnter: () => void;
    onMouseDown: (event: React.MouseEvent<HTMLButtonElement>) => void;
    onMouseUp: () => void;
    onMouseLeave: () => void;
};

export const PresetSlotButton = ({
    slot,
    color,
    active,
    holdActive,
    holdProgress,
    tooltip,
    tooltipSide,
    marginLeft,
    numberColor,
    presetPreviewStyle,
    holdBarStyle,
    onMouseEnter,
    onMouseDown,
    onMouseUp,
    onMouseLeave,
}: PresetSlotButtonProps) => (
    <SideTooltip tooltip={tooltip} side={tooltipSide ?? "right"}>
        <button
            type="button"
            className={`${styles.presetSlot} ${active ? styles.presetSlotActive : ""}`}
            style={{ marginLeft }}
            onMouseEnter={onMouseEnter}
            onMouseDown={onMouseDown}
            onMouseUp={onMouseUp}
            onMouseLeave={onMouseLeave}
        >
            {holdActive && holdProgress > 0 && (
                <span className={styles.holdBar} style={holdBarStyle(holdProgress)} />
            )}
            <span className={styles.presetNumber} style={{ color: numberColor }}>
                {slot}
            </span>
            <span className={styles.presetSwatch} style={presetPreviewStyle(color)} />

            {/* Real child rings are more reliable than pseudo-elements in Cohtml. */}
            <span className={styles.presetHoverRing} aria-hidden="true" />
            <span className={styles.presetActiveRing} aria-hidden="true" />
        </button>
    </SideTooltip>
);

type DragGripProps = {
    active: boolean;
    tooltip?: React.ReactNode;
    tooltipSide?: SideTooltipSide;
    onMouseDown: (event: React.MouseEvent<HTMLDivElement>) => void;
};

export const DragGrip = ({ active, tooltip, tooltipSide, onMouseDown }: DragGripProps) => (
    <SideTooltip tooltip={tooltip} side={tooltipSide ?? "below"}>
        <div
            className={`${styles.dragGrip} ${active ? styles.dragGripActive : ""}`}
            onMouseDown={onMouseDown}
        />
    </SideTooltip>
);
