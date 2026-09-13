// <copyright file="OutlineColorSystem.Tools.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/Rendering/OutlineColorSystem.Tools.cs
// Purpose: Classifies active tools and preserves vanilla placement-error behavior.

namespace HoverColors.Systems
{
    using System;
    using CS2Shared.RiverMochi;
    using Game.Prefabs;
    using Game.Tools;
    using Unity.Entities;

    public partial class OutlineColorSystem
    {
        private ToolKind GetActiveToolKind(ToolBaseSystem? tool)
        {
            if (tool == null)
            {
                return ToolKind.None;
            }

            if (tool is BulldozeToolSystem)
            {
                return ToolKind.Bulldoze;
            }

            if (tool is NetToolSystem netTool)
            {
                return GetNetToolKind(netTool);
            }

            // Better Bulldozer may still drive vanilla BulldozeToolSystem, but this keeps the
            // feature resilient if a tool wrapper becomes active instead.
            string typeName = tool.GetType().Name;
            if (typeName.IndexOf("Bulldoze", StringComparison.OrdinalIgnoreCase) >= 0
                || typeName.IndexOf("Bulldozer", StringComparison.OrdinalIgnoreCase) >= 0
                || SafeToolId(tool).IndexOf("Bulldoze", StringComparison.OrdinalIgnoreCase) >= 0
                || SafeToolId(tool).IndexOf("Bulldozer", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return ToolKind.Bulldoze;
            }

            // Mod Selection tools (e.g. AllSpeedLimits) own their own
            // multi-segment selection and should stay as high-visibility as the "road tools", so a
            // low-visibility player hover preset can't hide what is currently selected. Matched by
            // tool id, with the tool's namespace as backup so there is no hard reference to the other mod.
            // Mapping to NetRoad means: options menu, Recommended/Vanilla mode -> high-vis vanilla
            // cyan (the default), Custom mode -> the player's chosen color (power-user override).
            string toolId = SafeToolId(tool);

            // Parking Control mod's No Parking selector behaves like a road upgrade tool for colors.
            if (string.Equals(
                    toolId,
                    "ParkingControl.NoParking",
                    StringComparison.Ordinal))
            {
                return ToolKind.NetRoad;
            }

            // For compatibility with AllSpeedLimits mod
            if (string.Equals(toolId, "SpeedLimitTool", StringComparison.Ordinal)
                || (tool.GetType().Namespace?.StartsWith("RoadRailSpeeds", StringComparison.Ordinal) ?? false))
            {
                return ToolKind.NetRoad;
            }

            return ToolKind.None;
        }

        private ToolKind GetNetToolKind(NetToolSystem netTool)
        {
            if (m_PrefabSystem == null)
            {
                return ToolKind.NetRoad;
            }

            PrefabBase? selectedPrefab = netTool.GetPrefab();
            if (selectedPrefab == null || !m_PrefabSystem.TryGetEntity(selectedPrefab, out Entity prefabEntity))
            {
                return ToolKind.NetRoad;
            }

            if (EntityManager.HasComponent<RoadData>(prefabEntity))
            {
                return ToolKind.NetRoad;
            }

            // EDT fences/hedges/markings and similar detail tools enter through NetTool
            // as lanes or fence prefabs. To keep this check cheap: selected prefab only.
            if (selectedPrefab is NetLanePrefab
                || EntityManager.HasComponent<NetLaneData>(prefabEntity)
                || EntityManager.HasComponent<FenceData>(prefabEntity))
            {
                return ToolKind.NetDetailing;
            }

            return ToolKind.NetRoad;
        }

        private bool HasSupportedPlacementError(ToolBaseSystem? tool)
        {
            if (tool == null)
            {
                return false;
            }

            // Keep this option scoped to item/network placement. AreaTool uses Error for
            // extractor/district/surface limits too, and those should not become salmon.
            if (tool is not ObjectToolSystem && tool is not NetToolSystem)
            {
                return false;
            }

            if (s_ToolErrorQueryField == null)
            {
                LogUtils.WarnOnce(
                    "tool-error-query-field-missing",
                    () => $"{Mod.ModTag} Cannot preserve placement error color: ToolBaseSystem.m_ErrorQuery not found.");
                return false;
            }

            try
            {
                // Vanilla ToolBaseSystem.GetAllowApply() checks this same query. In these
                // tools it covers blocking placement errors, including Overlapping items.
                if (!ReferenceEquals(m_CachedErrorTool, tool))
                {
                    object? value = s_ToolErrorQueryField.GetValue(tool);
                    m_CachedErrorTool = tool;
                    m_HasCachedErrorQuery = value is EntityQuery;
                    if (m_HasCachedErrorQuery)
                    {
                        m_CachedErrorQuery = (EntityQuery)value!;
                    }
                }

                return m_HasCachedErrorQuery && !m_CachedErrorQuery.IsEmptyIgnoreFilter;
            }
            catch (Exception ex)
            {
                LogUtils.WarnOnce(
                    "tool-error-query-read-failed",
                    () => $"{Mod.ModTag} Cannot preserve placement error color: {ex.GetType().Name}: {ex.Message}",
                    ex);
                return false;
            }
        }

        private static string SafeToolId(ToolBaseSystem tool)
        {
            try
            {
                return tool.toolID ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
