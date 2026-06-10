// File: UI/src/panel/useDistrictToolPanel.ts
// Purpose: Opens the vanilla Areas > District tool after the District color menu is used.
// Keep this separate from the visual District menu so the panel component stays readable.

import React from "react";
import { toolbar } from "cs2/bindings";
import { useMapValue, useValue } from "cs2/api";
import {
    AREA_MENU_NAME_TOKENS,
    DISTRICT_AREA_NAME_TOKENS,
    sameEntity,
} from "./MochiPanelBindings";

export const useDistrictToolPanel = () => {
    const toolbarGroups = useValue(toolbar.toolbarGroups$);
    const selectedAssetMenu = useValue(toolbar.selectedAssetMenu$);
    const selectedAssetCategory = useValue(toolbar.selectedAssetCategory$);

    const [pendingDistrictToolOpen, setPendingDistrictToolOpen] = React.useState(false);
    const areaPanelOpenTimerRef = React.useRef<number | null>(null);
    const districtToolOpenTimeoutRef = React.useRef<number | null>(null);
    const districtToolSelectRetryRef = React.useRef<number | null>(null);

    const normalizeToolbarName = React.useCallback((value: string) => value.toUpperCase(), []);

    const areasMenu = React.useMemo(() => (
        toolbarGroups
            ?.flatMap(group => group.children ?? [])
            .find(item =>
                item.type === toolbar.ToolbarItemType.menu
                && AREA_MENU_NAME_TOKENS.some(token => normalizeToolbarName(item.name).includes(token)))
    ), [normalizeToolbarName, toolbarGroups]);

    const areasCategories = useMapValue(toolbar.assetCategories$, areasMenu?.entity);

    const districtCategory = React.useMemo(() => (
        areasCategories
            ?.find(item => DISTRICT_AREA_NAME_TOKENS.some(token => normalizeToolbarName(item.name).includes(token)))
    ), [areasCategories, normalizeToolbarName]);

    const districtAssets = useMapValue(toolbar.assets$, districtCategory?.entity);

    const districtAsset = React.useMemo(() => (
        districtAssets
            ?.find(item => DISTRICT_AREA_NAME_TOKENS.some(token => normalizeToolbarName(item.name).includes(token)))
        ?? districtAssets?.[0]
    ), [districtAssets, normalizeToolbarName]);

    React.useEffect(() => () => {
        if (areaPanelOpenTimerRef.current != null) {
            clearTimeout(areaPanelOpenTimerRef.current);
            areaPanelOpenTimerRef.current = null;
        }

        if (districtToolOpenTimeoutRef.current != null) {
            clearTimeout(districtToolOpenTimeoutRef.current);
            districtToolOpenTimeoutRef.current = null;
        }

        if (districtToolSelectRetryRef.current != null) {
            clearTimeout(districtToolSelectRetryRef.current);
            districtToolSelectRetryRef.current = null;
        }
    }, []);

    React.useEffect(() => {
        if (!pendingDistrictToolOpen) {
            return;
        }

        if (areasMenu == null) {
            setPendingDistrictToolOpen(false);
            return;
        }

        if (!sameEntity(selectedAssetMenu, areasMenu.entity)) {
            toolbar.selectAssetMenu(areasMenu.entity);
            return;
        }

        if (districtCategory == null) {
            return;
        }

        if (!sameEntity(selectedAssetCategory, districtCategory.entity)) {
            toolbar.selectAssetCategory(districtCategory.entity);
            return;
        }

        if (districtAsset == null) {
            return;
        }

        // Vanilla can restore the previous subtool, so select District now and retry once.
        toolbar.clearAssetSelection();
        toolbar.selectAssetMenu(areasMenu.entity);
        toolbar.selectAssetCategory(districtCategory.entity);
        toolbar.selectAsset(districtAsset.entity, true);

        if (districtToolSelectRetryRef.current != null) {
            clearTimeout(districtToolSelectRetryRef.current);
        }

        const areasMenuEntity = areasMenu.entity;
        const districtCategoryEntity = districtCategory.entity;
        const districtEntity = districtAsset.entity;

        districtToolSelectRetryRef.current = window.setTimeout(() => {
            toolbar.clearAssetSelection();
            toolbar.selectAssetMenu(areasMenuEntity);
            toolbar.selectAssetCategory(districtCategoryEntity);
            toolbar.selectAsset(districtEntity, true);
            districtToolSelectRetryRef.current = null;
        }, 250);

        if (districtToolOpenTimeoutRef.current != null) {
            clearTimeout(districtToolOpenTimeoutRef.current);
            districtToolOpenTimeoutRef.current = null;
        }

        setPendingDistrictToolOpen(false);
    }, [
        areasMenu,
        districtAsset,
        districtCategory,
        pendingDistrictToolOpen,
        selectedAssetCategory,
        selectedAssetMenu,
    ]);

    const openAreasToolPanel = React.useCallback(() => {
        if (areaPanelOpenTimerRef.current != null) {
            clearTimeout(areaPanelOpenTimerRef.current);
        }

        if (districtToolOpenTimeoutRef.current != null) {
            clearTimeout(districtToolOpenTimeoutRef.current);
        }

        if (districtToolSelectRetryRef.current != null) {
            clearTimeout(districtToolSelectRetryRef.current);
            districtToolSelectRetryRef.current = null;
        }

        // Defer until after the picker/menu click finishes; toolbar bindings arrive in steps.
        areaPanelOpenTimerRef.current = window.setTimeout(() => {
            toolbar.clearAssetSelection();
            setPendingDistrictToolOpen(true);
            areaPanelOpenTimerRef.current = null;
        }, 80);

        // Bail out if the expected toolbar data never arrives.
        districtToolOpenTimeoutRef.current = window.setTimeout(() => {
            setPendingDistrictToolOpen(false);
            districtToolOpenTimeoutRef.current = null;
        }, 1500);
    }, []);

    return { openAreasToolPanel };
};
