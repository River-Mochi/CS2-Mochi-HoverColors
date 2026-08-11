// File: UI/src/panel/hooks/usePanelDrag.ts
// Purpose: keeps the Editor or City GTL-anchored panel draggable and clamps it inside the game window.

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
};

type PanelDragContext = "game" | "editor";

const sessionPanelOffsets: Record<PanelDragContext, PanelOffset> = {
    game: { x: 0, y: 0 },
    editor: { x: 0, y: 0 },
};

export const usePanelDrag = (context: PanelDragContext = "game") => {
    const [panelOffset, setPanelOffset] = useState<PanelOffset>(sessionPanelOffsets[context]);

    const [panelDragging, setPanelDragging] = useState(false);

    const panelElementRef = useRef<HTMLDivElement | null>(null);
    const panelDragFrameRef = useRef<number | null>(null);
    const panelDragPendingOffsetRef = useRef(panelOffset);
    const panelDragRef = useRef<PanelDragState | null>(null);

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
            if (panelDragFrameRef.current !== null) {
                window.cancelAnimationFrame(panelDragFrameRef.current);
                panelDragFrameRef.current = null;
            }

            panelDragRef.current = null;
            setPanelDragging(false);
            sessionPanelOffsets[context] = panelDragPendingOffsetRef.current;
            setPanelOffset(panelDragPendingOffsetRef.current);
        };

        window.addEventListener("mousemove", onMove);
        window.addEventListener("mouseup", onUp);

        return () => {
            window.removeEventListener("mousemove", onMove);
            window.removeEventListener("mouseup", onUp);
      };
    }, [panelDragging, context]);

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
