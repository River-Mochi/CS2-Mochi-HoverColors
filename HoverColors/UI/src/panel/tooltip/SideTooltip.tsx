// File: UI/src/panel/tooltip/SideTooltip.tsx
// Purpose: Panel-anchored tooltips that render OFF the panel (left / right / above / below), never over it.
// The vanilla cs2/ui Tooltip only centers over the hovered control, so it always covered the panel.
// This renders a single tooltip layer positioned relative to the panel anchor. Because the panel
// uses a CSS transform, position:fixed would be broken, so the tooltip is an absolute child of the
// (transformed) anchor and is positioned with anchor-relative coordinates measured on hover.
//
// Placement is viewport-aware: the panel is draggable, so a requested side can run off screen
// (e.g. the left-side reset tooltips once the panel sits near the left edge). The layer is measured
// after it mounts, then the requested side is kept if it fits, otherwise the first side in its
// fallback chain that does fit is used. The cross axis is clamped so long tooltips stay on screen.

import React, { createContext, useContext, useLayoutEffect, useRef, useState, type ReactNode, type RefObject } from "react";

export type SideTooltipSide = "left" | "right" | "above" | "below";

type ShowFn = (side: SideTooltipSide, content: ReactNode, controlRect: DOMRect) => void;

const SideTooltipContext = createContext<{ show: ShowFn; hide: () => void }>({
    show: () => {},
    hide: () => {},
});

// Gap in pixels between the panel edge and the tooltip. Raise to push tooltips farther off-panel.
const GAP_PX = 8;

// Minimum pixels kept between the tooltip and the game window edge.
const EDGE_PX = 8;

// First side that fits wins. "below" comes before "above" because the panel normally sits high.
const FALLBACK_SIDES: Record<SideTooltipSide, SideTooltipSide[]> = {
    left: ["left", "below", "right", "above"],
    right: ["right", "below", "left", "above"],
    above: ["above", "below", "right", "left"],
    below: ["below", "above", "right", "left"],
};

type Box = { left: number; top: number; width: number; height: number };

// Anchor-relative geometry plus the anchor origin, which is what makes the viewport tests possible.
type TipRequest = {
    content: ReactNode;
    side: SideTooltipSide;
    control: Box;
    panel: Box;
    anchorLeft: number;
    anchorTop: number;
};

type TipPlacement = { left: number; top: number; transform: string };

const clamp = (value: number, min: number, max: number) => (min > max ? min : Math.min(Math.max(value, min), max));

const fitsSide = (side: SideTooltipSide, tip: TipRequest, width: number, height: number) => {
    const { panel, anchorLeft, anchorTop } = tip;
    const panelLeft = anchorLeft + panel.left;
    const panelRight = panelLeft + panel.width;
    const panelTop = anchorTop + panel.top;
    const panelBottom = panelTop + panel.height;

    switch (side) {
        case "left":
            return panelLeft - GAP_PX - width >= EDGE_PX;
        case "right":
            return panelRight + GAP_PX + width <= window.innerWidth - EDGE_PX;
        case "above":
            return panelTop - GAP_PX - height >= EDGE_PX;
        default:
            return panelBottom + GAP_PX + height <= window.innerHeight - EDGE_PX;
    }
};

const placeTip = (tip: TipRequest, width: number, height: number): TipPlacement => {
    const { control, panel, anchorLeft, anchorTop } = tip;
    const side = FALLBACK_SIDES[tip.side].find(candidate => fitsSide(candidate, tip, width, height)) ?? tip.side;

    const controlMidX = control.left + control.width / 2;
    const controlMidY = control.top + control.height / 2;
    const panelRight = panel.left + panel.width;
    const panelBottom = panel.top + panel.height;

    // Cross-axis clamp keeps the centered edge inside the window; the tooltip can slide along the
    // panel edge but never past it.
    if (side === "left" || side === "right") {
        const top = clamp(
            controlMidY,
            EDGE_PX + height / 2 - anchorTop,
            window.innerHeight - EDGE_PX - height / 2 - anchorTop,
        );

        return side === "left"
            ? { left: panel.left - GAP_PX, top, transform: "translate(-100%, -50%)" }
            : { left: panelRight + GAP_PX, top, transform: "translateY(-50%)" };
    }

    const left = clamp(
        controlMidX,
        EDGE_PX + width / 2 - anchorLeft,
        window.innerWidth - EDGE_PX - width / 2 - anchorLeft,
    );

    return side === "above"
        ? { left, top: panel.top - GAP_PX, transform: "translate(-50%, -100%)" }
        : { left, top: panelBottom + GAP_PX, transform: "translateX(-50%)" };
};

type ProviderProps = {
    anchorRef: RefObject<HTMLElement | null>;
    panelRef: RefObject<HTMLElement | null>;
    disabled?: boolean;
    children: ReactNode;
};

export const SideTooltipProvider = ({ anchorRef, panelRef, disabled = false, children }: ProviderProps) => {
    const [tip, setTip] = useState<TipRequest | null>(null);
    const [placement, setPlacement] = useState<TipPlacement | null>(null);
    const tipRef = useRef<HTMLDivElement>(null);

    React.useEffect(() => {
        if (disabled) {
            setTip(null);
            setPlacement(null);
        }
    }, [disabled]);

    // Runs before paint, so the un-placed first render is never visible.
    useLayoutEffect(() => {
        if (tip == null) {
            setPlacement(null);
            return;
        }

        const element = tipRef.current;
        if (element == null) {
            return;
        }

        const rect = element.getBoundingClientRect();
        setPlacement(placeTip(tip, rect.width, rect.height));
    }, [tip]);

    const show: ShowFn = (side, content, control) => {
        if (disabled) {
            return;
        }

        const anchor = anchorRef.current?.getBoundingClientRect();
        const panel = panelRef.current?.getBoundingClientRect() ?? anchor;
        if (content == null || anchor == null || panel == null) {
            return;
        }

        // Make everything relative to the anchor so the absolute tooltip moves with the panel.
        setPlacement(null);
        setTip({
            content,
            side,
            control: {
                left: control.left - anchor.left,
                top: control.top - anchor.top,
                width: control.width,
                height: control.height,
            },
            panel: {
                left: panel.left - anchor.left,
                top: panel.top - anchor.top,
                width: panel.width,
                height: panel.height,
            },
            anchorLeft: anchor.left,
            anchorTop: anchor.top,
        });
    };

    const hide = () => {
        setTip(null);
        setPlacement(null);
    };

    return (
        <SideTooltipContext.Provider value={{ show, hide }}>
            {children}
            {tip && (
                <div ref={tipRef} style={{
                    position: "absolute",
                    left: `${placement?.left ?? 0}px`,
                    top: `${placement?.top ?? 0}px`,
                    transform: placement?.transform ?? "none",
                    visibility: placement == null ? "hidden" : "visible",
                    zIndex: 1000050,
                    pointerEvents: "none",
                    maxWidth: "240rem",
                    paddingTop: "7rem",
                    paddingRight: "10rem",
                    paddingBottom: "7rem",
                    paddingLeft: "10rem",
                    backgroundColor: "rgba(20, 23, 28, 0.96)",
                    color: "rgba(255, 255, 255, 0.95)",
                    borderRadius: "4rem",
                    borderWidth: "1rem",
                    borderStyle: "solid",
                    borderColor: "rgba(120, 220, 255, 0.45)",
                    fontSize: "13rem",
                    lineHeight: "1.35",
                    whiteSpace: "normal",
                    overflowWrap: "break-word"
                }}>
                    {tip.content}
                </div>
            )}
        </SideTooltipContext.Provider>
    );
};

type SideTooltipProps = {
    tooltip?: ReactNode;
    side: SideTooltipSide;
    children: React.ReactElement;
};

// Drop-in replacement for the vanilla <Tooltip>: wraps one child, adds hover handlers that drive the
// shared tooltip layer. Uses cloneElement so it adds no extra DOM and never disturbs flex layout.
export const SideTooltip = ({ tooltip, side, children }: SideTooltipProps) => {
    const { show, hide } = useContext(SideTooltipContext);
    const child = React.Children.only(children) as React.ReactElement<any>;

    return React.cloneElement(child, {
        onMouseEnter: (e: React.MouseEvent) => {
            if (tooltip != null) {
                show(side, tooltip, (e.currentTarget as HTMLElement).getBoundingClientRect());
            }
            child.props.onMouseEnter?.(e);
        },
        onMouseLeave: (e: React.MouseEvent) => {
            hide();
            child.props.onMouseLeave?.(e);
        },
    });
};
