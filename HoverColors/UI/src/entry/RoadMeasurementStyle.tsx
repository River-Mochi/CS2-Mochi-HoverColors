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

// Preserve the vanilla BoundTooltipGroup and its numeric renderer. Only
// augment the className of matching child props; no DOM walks, replacement
// widgets, new tooltip keys, or custom position/animation calculations.
export const registerRoadMeasurementStyle = (registry: ModuleRegistry) => {
  registry.extend(MODULE, "BoundTooltipGroup", (Original) => {
    const MeasurementGroup = (props: any) => {
      const active = useValue(roadMeasurementStyleActive$);
      const units = useValue(units$);
      if (!active) return <Original {...props} />;

      const children = (props.children as WidgetData[]).map((child) => {
        if (!isMeasurement(child) || !child.props) return child;
        const className = child.props.className;
        const styled: NonNullable<WidgetData["props"]> = {
          ...child.props,
          className: className ? className + " " + BADGE_CLASS : BADGE_CLASS,
        };
        // Vanilla supports length, but has no construction-unit formatter.
        // Preserve native NumberTooltip for all other values. The native
        // StringTooltip uses the same row, icon, value class and color.
        if (units && styled.icon === "Media/Glyphs/Length.svg"
          && styled.unit === "length" && typeof styled.value === "number"
          && Number.isFinite(styled.value)) {
          const value = Number((styled.value / 8).toFixed(2));
          return { ...child, props: { ...styled,
            __Type: "Game.UI.Tooltip.StringTooltip",
            value: { __Type: "Game.UI.Localization.LocalizedString",
              id: null, value: "{VALUE} U", args: { VALUE: {
                __Type: "Game.UI.Localization.LocalizedNumber", value, unit: "floatTwoFractions", signed: false,
              } } },
          } };
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
