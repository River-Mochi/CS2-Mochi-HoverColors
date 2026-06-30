// File: UI/src/panel/hooks/usePresetHold.ts
// Purpose: Hold-to-save behavior for the outline preset buttons.

import React from "react";
import { trigger } from "cs2/api";
import { CHANNEL, PRESET_HOLD_MS } from "../bindings/MochiPanelBindings";

export type PresetSlot = 1 | 2;

export const usePresetHold = () => {
    const [holdSlot, setHoldSlot] = React.useState<0 | PresetSlot>(0);
    const [holdProgress, setHoldProgress] = React.useState(0);
    const holdTimerRef = React.useRef<number | null>(null);
    const holdStartRef = React.useRef<number>(0);
    const holdRafRef = React.useRef<number | null>(null);

    const cancelHold = React.useCallback(() => {
        if (holdTimerRef.current != null) {
            clearTimeout(holdTimerRef.current);
            holdTimerRef.current = null;
        }

        if (holdRafRef.current != null) {
            cancelAnimationFrame(holdRafRef.current);
            holdRafRef.current = null;
        }

        setHoldSlot(0);
        setHoldProgress(0);
    }, []);

    React.useEffect(() => cancelHold, [cancelHold]);

    const handlePresetMouseDown = React.useCallback((slot: PresetSlot) => (event: React.MouseEvent) => {
        event.preventDefault();
        cancelHold();

        holdStartRef.current = performance.now();
        setHoldSlot(slot);
        setHoldProgress(0);

        const sweepDelay = 150;
        const tick = () => {
            const elapsed = performance.now() - holdStartRef.current;

            if (elapsed >= sweepDelay) {
                const progress = Math.min((elapsed - sweepDelay) / (PRESET_HOLD_MS - sweepDelay), 1);
                setHoldProgress(progress);

                if (progress < 1) {
                    holdRafRef.current = requestAnimationFrame(tick);
                }

                return;
            }

            holdRafRef.current = requestAnimationFrame(tick);
        };

        holdRafRef.current = requestAnimationFrame(tick);
        holdTimerRef.current = window.setTimeout(() => {
            holdTimerRef.current = null;

            if (holdRafRef.current != null) {
                cancelAnimationFrame(holdRafRef.current);
                holdRafRef.current = null;
            }

            trigger(CHANNEL, "SavePreset", slot);
            setHoldSlot(0);
            setHoldProgress(0);
        }, PRESET_HOLD_MS);
    }, [cancelHold]);

    const handlePresetMouseUp = React.useCallback((slot: PresetSlot) => () => {
        if (holdTimerRef.current != null) {
            clearTimeout(holdTimerRef.current);
            holdTimerRef.current = null;
            trigger(CHANNEL, "ApplyPreset", slot);
        }

        if (holdRafRef.current != null) {
            cancelAnimationFrame(holdRafRef.current);
            holdRafRef.current = null;
        }

        setHoldSlot(0);
        setHoldProgress(0);
    }, []);

    return {
        holdSlot,
        holdProgress,
        cancelHold,
        handlePresetMouseDown,
        handlePresetMouseUp,
    };
};
