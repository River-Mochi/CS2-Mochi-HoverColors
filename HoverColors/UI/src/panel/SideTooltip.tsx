// File: UI/src/panel/SideTooltip.tsx
// Purpose: Panel-anchored tooltips that render OFF the panel (left / right / above / below), never over it.
// The vanilla cs2/ui Tooltip only centers over the hovered control, so it always covered the panel.
// This renders a single tooltip layer positioned relative to the panel anchor. Because the panel
// uses a CSS transform, position:fixed would be broken, so the tooltip is an absolute child of the
// (transformed) anchor and is positioned with anchor-relative coordinates measured on hover.

import React, { createContext, useContext, useState, type ReactNode, type RefObject } from "react";

export type SideTooltipSide = "left" | "right" | "above" | "below";

type ShowFn = (side: SideTooltipSide, content: ReactNode, controlRect: DOMRect) => void;

const SideTooltipContext = createContext<{ show: ShowFn; hide: () => void }>({
    show: () => {},
    hide: () => {},
});

// Gap in pixels between the panel edge and the tooltip. Raise to push tooltips farther off-panel.
const GAP_PX = 8;

type TipState = {
    content: ReactNode;
    left: number;
    top: number;
    transform: string;
} | null;

type ProviderProps = {
    anchorRef: RefObject<HTMLElement | null>;
    panelRef: RefObject<HTMLElement | null>;
    children: ReactNode;
};

export const SideTooltipProvider = ({ anchorRef, panelRef, children }: ProviderProps) => {
    const [tip, setTip] = useState<TipState>(null);

    const show: ShowFn = (side, content, control) => {
        const anchor = anchorRef.current?.getBoundingClientRect();
        const panel = panelRef.current?.getBoundingClientRect() ?? anchor;
        if (content == null || anchor == null || panel == null) {
            return;
        }

        // Make everything relative to the anchor so the absolute tooltip moves with the panel.
        const controlMidY = control.top - anchor.top + control.height / 2;
        const controlMidX = control.left - anchor.left + control.width / 2;

        if (side === "right") {
            setTip({ content, left: panel.right - anchor.left + GAP_PX, top: controlMidY, transform: "translateY(-50%)" });
        } else if (side === "left") {
            setTip({ content, left: panel.left - anchor.left - GAP_PX, top: controlMidY, transform: "translate(-100%, -50%)" });
        } else if (side === "above") {
            setTip({ content, left: controlMidX, top: panel.top - anchor.top - GAP_PX, transform: "translate(-50%, -100%)" });
        } else {
            setTip({ content, left: controlMidX, top: panel.bottom - anchor.top + GAP_PX, transform: "translateX(-50%)" });
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
                    maxWidth: "260rem",
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
                    lineHeight: "1.35"
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