// File: UI/src/panel/useMochiPanelText.ts
// Purpose: Localized text + tooltip gating for MochiColorPickerPanel.

import React from "react";
import { useValue } from "cs2/api";
import { LocaleKey, usePanelLocalization } from "../localization";
import { panelTooltipsEnabled$ } from "./MochiPanelBindings";

export type MochiPanelText = {
    ariaClosePanel: string;
    title: string;
    tooltipClose: string;
    tooltipDraggable: string;
    tooltipFillOpacity: string;
    tooltipGuidelinesColor: string;
    tooltipGuidelinesPreviewColor: string;
    tooltipGuidelinesOpacity: string;
    tooltipInfo: string;
    tooltipOutlineSwatch: string;
    tooltipOwnerSwatch: string;
    tooltipPreset1: string;
    tooltipPreset2: string;
    tooltipResetFill: string;
    tooltipResetGuidelines: string;
    tooltipResetOutline: string;
    tooltipResetPresets: string;
    tooltipSurfaceToggle: string;
    tooltipSpecializedIndustryToggle: string;
    tooltipDistrictColors: string;
    districtMenuAllDistricts: string;
    districtMenuResetAll: string;
};

export const useMochiPanelText = () => {
    const translatePanel = usePanelLocalization();
    const tooltipsEnabled = useValue(panelTooltipsEnabled$);

    // Tooltips return undefined when disabled. Info tooltip stays visible for re-enable.
    const tt = React.useCallback(
        (s: string): string | undefined => (tooltipsEnabled ? s : undefined),
        [tooltipsEnabled],
    );

    const text = React.useMemo<MochiPanelText>(() => {
        const l = (key: LocaleKey) => translatePanel(key);
        return {
            ariaClosePanel: l("HoverColors.UI.Aria.ClosePanel"),
            title: l("HoverColors.UI.Title"),
            tooltipClose: l("HoverColors.UI.Tooltip.Close"),
            tooltipDraggable: l("HoverColors.UI.Tooltip.Draggable"),
            tooltipFillOpacity: l("HoverColors.UI.Tooltip.FillOpacity"),
            tooltipGuidelinesColor: l("HoverColors.UI.Tooltip.GuidelinesColor"),
            tooltipGuidelinesPreviewColor: l("HoverColors.UI.Tooltip.GuidelinesPreviewColor"),
            tooltipGuidelinesOpacity: l("HoverColors.UI.Tooltip.GuidelinesOpacity"),
            tooltipInfo: l("HoverColors.UI.Tooltip.Info"),
            tooltipOutlineSwatch: l("HoverColors.UI.Tooltip.OutlineSwatch"),
            tooltipOwnerSwatch: l("HoverColors.UI.Tooltip.OwnerSwatch"),
            tooltipPreset1: l("HoverColors.UI.Tooltip.Preset1"),
            tooltipPreset2: l("HoverColors.UI.Tooltip.Preset2"),
            tooltipResetFill: l("HoverColors.UI.Tooltip.ResetFill"),
            tooltipResetGuidelines: l("HoverColors.UI.Tooltip.ResetGuidelines"),
            tooltipResetOutline: l("HoverColors.UI.Tooltip.ResetOutline"),
            tooltipResetPresets: l("HoverColors.UI.Tooltip.ResetPresets"),
            tooltipSurfaceToggle: l("HoverColors.UI.Tooltip.SurfaceToggle"),
            tooltipSpecializedIndustryToggle: l("HoverColors.UI.Tooltip.SpecializedIndustryToggle"),
            tooltipDistrictColors: l("HoverColors.UI.Tooltip.DistrictColors"),
            districtMenuAllDistricts: l("HoverColors.UI.DistrictMenu.AllDistricts"),
            districtMenuResetAll: l("HoverColors.UI.DistrictMenu.ResetAll"),
        };
    }, [translatePanel]);

    return { text, tooltipsEnabled, tt };
};
