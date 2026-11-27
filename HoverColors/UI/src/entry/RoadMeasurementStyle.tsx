// <copyright file="RoadMeasurementStyle.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

import { bindValue, useValue } from "cs2/api";
import { ModuleRegistry } from "cs2/modding";
import { CHANNEL, roadMeasurementStyleActive$ } from "../panel/bindings/MochiPanelBindings";

const MODULE = "game-ui/common/tooltip/tooltip-renderer/tooltip-renderer.tsx";
const NUMBER_TYPE = "Game.UI.Tooltip.NumberTooltip";
const BADGE_CLASS = "mochiRoadMeasurementBadge";
const units$ = bindValue<boolean>(CHANNEL, "RoadEngineerUseUnits", false);
const distanceDecimals$ = bindValue<number>(CHANNEL, "RoadDistanceDecimals", 2);
const angleDecimals$ = bindValue<number>(CHANNEL, "RoadAngleDecimals", 2);
const unitSettings$ = bindValue<{unitSystem: number}>("options", "unitSettings", {unitSystem: 0});
const customText$ = bindValue<boolean>(CHANNEL, "RoadBadgeCustomText", false);
const nativeBackground$ = bindValue<boolean>(CHANNEL, "RoadBadgeNativeBackground", false);

type WidgetData = {
  path?: string;
  props?: {
    __Type?: string;
    icon?: string;
    color?: number;
    className?: string;
    [key: string]: unknown;
  };
  [key: string]: unknown;
};

const isMeasurement = (child: WidgetData | null | undefined) => {
  const props = child?.props;
  return props?.__Type === NUMBER_TYPE
    && (props.color === undefined || props.color === 0)
    && (props.icon === "Media/Glyphs/Angle.svg"
      || props.icon === "Media/Glyphs/Length.svg"
      || props.icon === "Media/Glyphs/Slope.svg");
};

// Preserve native group positions and native row renderers. Add appearance
// classes and use native StringTooltip rows for explicit precision and U lengths.
// No DOM walks, custom tooltip keys, or position/animation calculations.
export const registerRoadMeasurementStyle = (registry: ModuleRegistry) => {
  registry.extend(MODULE, "BoundTooltipGroup", (Original) => {
    const MeasurementGroup = (props: any) => {
      const active = useValue(roadMeasurementStyleActive$);
      const units = useValue(units$);
      const distanceDecimals = useValue(distanceDecimals$);
      const angleDecimals = useValue(angleDecimals$);
      const imperial = useValue(unitSettings$)?.unitSystem === 1;
      const customText = useValue(customText$);
      const nativeBackground = useValue(nativeBackground$);
      if (!active) return <Original {...props} />;

      const children = (props.children as WidgetData[]).map((child) => {
        if (!isMeasurement(child) || !child.props) return child;
        const className = child.props.className;
        const styled: NonNullable<WidgetData["props"]> = {
          ...child.props,
          className: [className, BADGE_CLASS, nativeBackground ? "" : "mochiRoadMeasurementCustomBackground", customText ? "mochiRoadMeasurementCustomText" : ""].filter(Boolean).join(" "),
        };
        // Only native course lengths and geometry-recovered guide values
        // can provide additional meaningful digits. Leave unverified rounded
        // starting angles and generated Continuous measurements native.
        const value = styled.value;
        if (typeof value === "number" && Number.isFinite(value)) {
          let display: string | undefined;
          const guide = /^guideLineTooltip\d+$/.test(props.path);
          const course = props.path === "tempNetEdgeStart" || props.path === "mouse";
          if (styled.icon === "Media/Glyphs/Length.svg" && styled.unit === "length") {
            const recovered = guide && Math.abs(value * 2 - Math.round(value * 2)) > 0.00001;
            if (units || (distanceDecimals > 0 && (course || recovered))) {
              const decimals = distanceDecimals > 0 ? distanceDecimals : 2;
              const converted = units ? value / 8 : imperial ? value / 0.9144 : value;
              display = converted.toFixed(decimals) + (units ? " U" : imperial ? " yd" : " m");
            }
          } else if (styled.icon === "Media/Glyphs/Angle.svg" && styled.unit === "angle"
              && angleDecimals > 0 && guide && Math.abs(value - Math.round(value)) > 0.00001) {
            display = value.toFixed(angleDecimals) + "°";
          }
          if (display !== undefined) {
            // Same native StringTooltip renderer used by the existing U option.
            // Do not use FloatTwoFractions: vanilla rounds magnitudes >=100
            // to integers even with that unit.
            return { ...child, props: { ...styled,
              __Type: "Game.UI.Tooltip.StringTooltip",
              value: { __Type: "Game.UI.Localization.LocalizedString",
                id: null, value: display, args: null },
            } };
          }
        }
        return { ...child, props: styled };
      });

      return <Original {...props} children={children} />;
    };

    return (props: any) => {
      const path = props.path;
      if (typeof path !== "string"
        || !(path === "mouse" || path === "tempNetEdgeStart" || /^guideLineTooltip\d+$/.test(path))
        || !Array.isArray(props.children)) {
        return <Original {...props} />;
      }

      return <MeasurementGroup {...props} />;
    };
  });
};
