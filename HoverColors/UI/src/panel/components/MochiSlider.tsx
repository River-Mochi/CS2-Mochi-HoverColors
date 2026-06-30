// File: UI/src/panel/components/MochiSlider.tsx
// Purpose: Thin wrapper around the vanilla CS2 slider, matching the RoadRailSpeeds approach.
//
// Important: set slider variables through the inline style prop. In HC, class-based CSS
// variables looked like they were not reaching the vanilla slider's internal track pieces.

import { VanillaComponentResolver } from "../../utils/vanilla/VanillaComponentResolver";

export const MochiSlider = (props: any) => {
    const VSlider = VanillaComponentResolver.instance.Slider;

    const sliderStyle = {
        ...(props.style ?? {}),
        // Same visual language as RRS: quiet grey track, visible left fill, transparent right side.
        "--backgroundColor": "rgba(176, 184, 188, 0.20)",
        "--sliderRangeColor": "rgba(140, 148, 154, 0.56)",
        "--sliderRangeColorFocused": "rgba(165, 173, 179, 0.66)",
        "--sliderThumbColor": "rgba(235, 237, 239, 1)",
        "--sliderThumbColorFocused": "rgba(255, 255, 255, 1)",
        "--trackSize": "8rem",
        "--focusedColor": "transparent",
    };

    return (
        <VSlider
            {...props}
            style={sliderStyle}
            noFill={props.noFill ?? false}
            step={props.step}
            onChange={(value: number) => props.onChange(value)}
        />
    );
};
