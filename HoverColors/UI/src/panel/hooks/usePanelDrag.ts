// <copyright file="usePanelDrag.ts" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: UI/src/panel/hooks/usePanelDrag.ts
// Purpose: keeps the Editor or City panel draggable and restores its saved city position.

import { useCallback, useEffect, useRef, useState, type MouseEvent as ReactMouseEvent } from "react";

type PanelOffset = {
    x: number;
    y: number;
};

type PanelDragState = {
    pointerX: number;
    pointerY: number;
    originX: number;
    originY: number;
    originLeft: number;
    originTop: number;
    originWidth: number;
    originHeight: number;
    moved: boolean;
};

type PanelDragContext = "game" | "editor";

type PanelPosition = { left: number; top: number };

const sessionPanelOffsets: Record<PanelDragContext, PanelOffset> = {
    game: { x: 0, y: 0 },
    editor: { x: 0, y: 0 },
};

export const usePanelDrag = (
    context: PanelDragContext,
    origin: PanelPosition,
    savedPosition: PanelPosition | null,
    onPositionChanged: (left: number, top: number) => void,
) => {
    const [panelOffset, setPanelOffset] = useState<PanelOffset>(() => savedPosition === null
        ? sessionPanelOffsets[context]
        : { x: savedPosition.left - origin.left, y: savedPosition.top - origin.top });

    const [panelDragging, setPanelDragging] = useState(false);

    const panelElementRef = useRef<HTMLDivElement | null>(null);
    const panelDragFrameRef = useRef<number | null>(null);
    const panelDragPendingOffsetRef = useRef(panelOffset);
    const panelDragRef = useRef<PanelDragState | null>(null);

    useEffect(() => {
        if (context !== "game" || savedPosition === null || panelDragRef.current !== null) return;
        const restored = { x: savedPosition.left - origin.left, y: savedPosition.top - origin.top };
        sessionPanelOffsets.game = restored;
        panelDragPendingOffsetRef.current = restored;
        setPanelOffset(restored);
    }, [context, origin.left, origin.top, savedPosition?.left, savedPosition?.top]);

    useEffect(() => {
        const clamp = () => {
            const rect = panelElementRef.current?.getBoundingClientRect();
            if (rect === undefined || panelDragRef.current !== null) return;
            const x = panelDragPendingOffsetRef.current.x + Math.min(0, window.innerWidth - rect.right) - Math.min(0, rect.left);
            const y = panelDragPendingOffsetRef.current.y + Math.min(0, window.innerHeight - rect.bottom) - Math.min(0, rect.top);
            if (x !== panelDragPendingOffsetRef.current.x || y !== panelDragPendingOffsetRef.current.y) {
                panelDragPendingOffsetRef.current = { x, y };
                sessionPanelOffsets[context] = { x, y };
                setPanelOffset({ x, y });
            }
        };
        const frame = window.requestAnimationFrame(clamp);
        const timeout = window.setTimeout(clamp, 150);
        window.addEventListener("resize", clamp);
        return () => {
            window.cancelAnimationFrame(frame);
            window.clearTimeout(timeout);
            window.removeEventListener("resize", clamp);
        };
    }, [context, origin.left, origin.top, savedPosition?.left, savedPosition?.top]);

    useEffect(() => {
        if (!panelDragging) {
            return;
        }

        const onMove = (event: MouseEvent) => {
            const dragState = panelDragRef.current;
            if (dragState === null) {
                return;
            }

            const deltaX = event.clientX - dragState.pointerX;
            const deltaY = event.clientY - dragState.pointerY;
            if (deltaX !== 0 || deltaY !== 0) dragState.moved = true;
            let nextX = dragState.originX + deltaX;
            let nextY = dragState.originY + deltaY;
            const nextLeft = dragState.originLeft + deltaX;
            const nextTop = dragState.originTop + deltaY;
            const nextRight = nextLeft + dragState.originWidth;
            const nextBottom = nextTop + dragState.originHeight;

            if (nextLeft < 0) {
                nextX -= nextLeft;
            }
            if (nextTop < 0) {
                nextY -= nextTop;
            }
            if (nextRight > window.innerWidth) {
                nextX -= nextRight - window.innerWidth;
            }
            if (nextBottom > window.innerHeight) {
                nextY -= nextBottom - window.innerHeight;
            }

            panelDragPendingOffsetRef.current = { x: nextX, y: nextY };
            if (panelDragFrameRef.current === null) {
                panelDragFrameRef.current = window.requestAnimationFrame(() => {
                    panelDragFrameRef.current = null;
                    sessionPanelOffsets[context] = panelDragPendingOffsetRef.current;
                    setPanelOffset(panelDragPendingOffsetRef.current);
                });
            }
        };

        const onUp = () => {
            const moved = panelDragRef.current?.moved ?? false;
            if (panelDragFrameRef.current !== null) {
                window.cancelAnimationFrame(panelDragFrameRef.current);
                panelDragFrameRef.current = null;
            }

            panelDragRef.current = null;
            setPanelDragging(false);
            sessionPanelOffsets[context] = panelDragPendingOffsetRef.current;
            setPanelOffset(panelDragPendingOffsetRef.current);
            if (context === "game" && moved) {
                onPositionChanged(
                    origin.left + panelDragPendingOffsetRef.current.x,
                    origin.top + panelDragPendingOffsetRef.current.y,
                );
            }
        };

        window.addEventListener("mousemove", onMove);
        window.addEventListener("mouseup", onUp);

        return () => {
            window.removeEventListener("mousemove", onMove);
            window.removeEventListener("mouseup", onUp);
      };
    }, [panelDragging, context, onPositionChanged, origin.left, origin.top]);

    useEffect(() => () => {
        if (panelDragFrameRef.current !== null) {
            window.cancelAnimationFrame(panelDragFrameRef.current);
            panelDragFrameRef.current = null;
        }
    }, []);

    const handlePanelDragStart = useCallback((event: ReactMouseEvent<HTMLDivElement>) => {
        event.preventDefault();
        event.stopPropagation();

        const rect = panelElementRef.current?.getBoundingClientRect();
        if (rect === undefined) {
            return;
        }

        panelDragPendingOffsetRef.current = panelOffset;
        panelDragRef.current = {
            pointerX: event.clientX,
            pointerY: event.clientY,
            originX: panelOffset.x,
            originY: panelOffset.y,
            originLeft: rect.left,
            originTop: rect.top,
            originWidth: rect.width,
            originHeight: rect.height,
            moved: false,
        };
        setPanelDragging(true);
    }, [panelOffset]);

    return {
        panelOffset,
        panelDragging,
        panelElementRef,
        handlePanelDragStart,
    };
};
