// File: UI/src/panel/tooltip/SideTooltip.tsx
// Purpose: Panel-anchored tooltips that render OFF the panel (left / right / above / below), never over it.
// The vanilla cs2/ui Tooltip only centers over the hovered control, so it always covered the panel.
// This renders a single tooltip layer positioned relative to the panel anchor. Because the panel
// uses a CSS transform, position:fixed would be broken, so the tooltip is an absolute child of the
// (transformed) anchor and is positioned with anchor-relative coordinates measured on hover.
//
// NOTHING about the tooltip itself is measured, on purpose. Earlier versions measured the layer
// after mounting it and placed it from that; in Cohtml a getBoundingClientRect() taken in the same
// frame the content changed is not reliable, so identical hovers produced different placements and
// tooltips sometimes landed on top of the control. Every number here comes from the anchor, panel
// and control rects, which are stable because those elements were laid out long before the hover.
//
// Instead of a measured size the layer is capped at the panel's own width, and the edges the layout
// needs are produced by CSS transforms (-100% to right-align, -50% to centre) rather than by
// arithmetic. That makes placement a pure function of the panel's position:
//
//   left   right edge on the panel's left edge,  vertically centred on the control
//   right  left edge on the panel's right edge,  vertically centred on the control
//   above  bottom edge on the panel's top edge,  left edge flush with the panel
//   below  top edge on the panel's bottom edge,  left edge flush with the panel
//
// Because the cap equals the panel width, an above/below tooltip can never stick out past the panel,
// and a left/right one can never reach back over it. Only the horizontal sides can run out of room,
// so only those fall back (to "below"); above/below never flip, which is what keeps the title-bar
// tooltip reliably above the panel.

import React, { createContext, useContext, useState, type ReactNode, type RefObject } from "react";

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

type TipState = {
    content: ReactNode;
    left: number;
    top: number;
    transform: string;
    maxWidth: number;
} | null;

type ProviderProps = {
    anchorRef: RefObject<HTMLElement | null>;
    panelRef: RefObject<HTMLElement | null>;
    disabled?: boolean;
    children: ReactNode;
};

export const SideTooltipProvider = ({ anchorRef, panelRef, disabled = false, children }: ProviderProps) => {
    const [tip, setTip] = useState<TipState>(null);

    React.useEffect(() => {
        if (disabled) {
            setTip(null);
        }
    }, [disabled]);

    const show: ShowFn = (side, content, control) => {
        if (disabled) {
            return;
        }

        const anchor = anchorRef.current?.getBoundingClientRect();
        const panel = panelRef.current?.getBoundingClientRect() ?? anchor;
        if (content == null || anchor == null || panel == null) {
            return;
        }

        // Anchor-relative so the absolute tooltip travels with the dragged panel.
        const panelLeft = panel.left - anchor.left;
        const panelTop = panel.top - anchor.top;
        const controlMidY = control.top - anchor.top + control.height / 2;

        // The layer is never wider than the panel, so the panel's own width is a safe stand-in for
        // the tooltip's width in the fit tests below.
        const maxWidth = panel.width;

        let placed = side;
        if (side === "left" && panel.left - GAP_PX - maxWidth < EDGE_PX) {
            placed = "below";
        } else if (side === "right" && panel.right + GAP_PX + maxWidth > window.innerWidth - EDGE_PX) {
            placed = "below";
        }

        if (placed === "left") {
            setTip({ content, left: panelLeft - GAP_PX, top: controlMidY, transform: "translate(-100%, -50%)", maxWidth });
        } else if (placed === "right") {
            setTip({ content, left: panelLeft + panel.width + GAP_PX, top: controlMidY, transform: "translateY(-50%)", maxWidth });
        } else if (placed === "above") {
            setTip({ content, left: panelLeft, top: panelTop - GAP_PX, transform: "translateY(-100%)", maxWidth });
        } else {
            setTip({ content, left: panelLeft, top: panelTop + panel.height + GAP_PX, transform: "none", maxWidth });
        }
    };

    const hide = () => setTip(null);

    return (
        <SideTooltipContext.Provider value={{ show, hide }}>
            {children}
            {tip && (
                <div style={{
                    position: "absolute",
                    left: `${tip.left}px`,
                    top: `${tip.top}px`,
                    transform: tip.transform,
                    zIndex: 1000050,
                    pointerEvents: "none",
                    boxSizing: "border-box",
                    maxWidth: `${tip.maxWidth}px`,
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
