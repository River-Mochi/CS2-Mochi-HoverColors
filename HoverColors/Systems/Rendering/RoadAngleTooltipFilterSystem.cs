// <copyright file="RoadAngleTooltipFilterSystem.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/Rendering/RoadAngleTooltipFilterSystem.cs
// Purpose: Reduce road/path clutter: keep angles and segment guides, move slope and total length to cursor; hide cost and hints.

namespace HoverColors.Systems
{
    using System;
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Prefabs;
    using Game.Tools;
    using Game.UI.Tooltip;

    public partial class RoadAngleTooltipFilterSystem : GameSystemBase
    {
        private ToolSystem? m_ToolSystem;
        private TooltipUISystem? m_TooltipUISystem;
        private bool m_FilterLogged;
        private bool m_ExecutionLogged;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_TooltipUISystem = World.GetOrCreateSystemManaged<TooltipUISystem>();
            InitializeMeasurementLayout();
        }

        protected override void OnUpdate()
        {
            if (m_TooltipUISystem == null || !ShouldFilter(m_ToolSystem))
            {
                ResetMeasurementLayout();
                return;
            }

            // One-time diagnostic to distinguish "system not executing" from "no match"
            // without spamming logs for every drag frame.
            if (!m_ExecutionLogged)
            {
                m_ExecutionLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road tooltip filter running (road/path build, hide ON)");
            }

            // Runs after tooltip producers and before the game's UI serializes groups.
            // Keep vanilla and Extended Tooltip angle groups and guideline distances.
            // NetCourseTooltipSystem supplies the separate total *road curve* length
            // plus slope in one world-positioned "tempNetEdgeStart" group.
            int movedCourseGroups = 0;
            for (int i = m_TooltipUISystem.groups.Count - 1; i >= 0; i--)
            {
                TooltipGroup group = m_TooltipUISystem.groups[i];
                if (group.path == "tempNetEdgeStart"
                    && group.children.Count == 2
                    && group.children[0] is FloatTooltip length
                    // Extended Tooltip can change the unit to floatTwoFractions (U).
                    // The course group path and icons identify these widgets regardless of units.
                    && length.icon == "Media/Glyphs/Length.svg"
                    && group.children[1] is FloatTooltip slope
                    && slope.icon == "Media/Glyphs/Slope.svg")
                {
                    // Leave the guideline U7/U7 distance labels in their original
                    // positions. Move ONLY the vanilla road-curve total length and
                    // slope to the right-side cursor tooltip list, in this order:
                    // slope FIRST, total length SECOND. Reuse the original widgets
                    // and values; do not change the game's measurements or group.
                    // TooltipUISystem recreates its lists every frame, so turning
                    // the filter OFF restores vanilla positioning immediately.
                    m_TooltipUISystem.groups.RemoveAt(i);
                    m_TooltipUISystem.mouseGroup.children.Add(slope);
                    m_TooltipUISystem.mouseGroup.children.Add(length);
                    movedCourseGroups++;
                }
            }

            int removedHints = 0;
            int removedModes = 0;
            int removedCosts = 0;
            var mouseHints = m_TooltipUISystem.mouseGroup.children;
            for (int i = mouseHints.Count - 1; i >= 0; i--)
            {
                if (mouseHints[i] is IntTooltip cost && cost.path == "cost")
                {
                    // Vanilla TempCostTooltipSystem: suppress cost only when road
                    // hint filtering is ON. Do not affect refunds or other tool costs.
                    mouseHints.RemoveAt(i);
                    removedCosts++;
                }
                else if (mouseHints[i] is StringTooltip mode && mode.path == "etNetToolMode")
                {
                    // Extended Tooltip 1.7.0 adds the current Net Tool mode here.
                    // Match its widget path without referencing that mod's assembly.
                    mouseHints.RemoveAt(i);
                    removedModes++;
                }
                else if (mouseHints[i] is InputHintTooltip hint && IsNetPlaceOrUndoHint(hint))
                {
                    mouseHints.RemoveAt(i);
                    removedHints++;
                }
            }

            // Keep native inside-angle anchors; resolve only nearby collisions.
            if (m_ToolSystem?.activeTool is NetToolSystem netTool)
            {
                ArrangeCurveMeasurements(netTool, m_TooltipUISystem);
            }

            // Only once per city/world: proof in HoverColors.log that the filter ran.
            // No frame-by-frame logging.
            if (!m_FilterLogged &&
                (removedHints != 0 || removedModes != 0 ||
                 removedCosts != 0 || movedCourseGroups != 0))
            {
                m_FilterLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road tooltip filter active: " +
                    $"removed {removedHints} Place/Undo, {removedModes} mode, " +
                    $"{removedCosts} cost; relocated {movedCourseGroups} slope/total-length pairs (angles retained)");
            }
        }

        // Used by the single, late UITooltip filter pass.
        // Do not suppress anything in rail, subway, pipe, or upgrade tools.
        internal static bool ShouldFilter(ToolSystem? toolSystem)
        {
            if (Mod.Settings?.RoadAngleTooltipsHidden != true
                || toolSystem?.activeTool is not NetToolSystem netTool
                || netTool.actualMode == NetToolSystem.Mode.Replace)
            {
                return false;
            }

            PrefabBase? selectedPrefab = netTool.GetPrefab();
            return selectedPrefab is RoadPrefab || selectedPrefab is PathwayPrefab;
        }

        private static bool IsNetPlaceOrUndoHint(InputHintTooltip hint)
        {
            // UIInputActionCollection.GetActionState(name, "NetToolSystem") creates a
            // DisplayNameOverride with source "name (NetToolSystem)". The underlying
            // ProxyAction.mapName is NOT guaranteed to be "NetToolSystem".
            // This is why the previous mapName-based filter failed for Place/Undo.
            string? source = hint.m_Action.displayOverride?.source;
            if (string.Equals(source, "Place Net Control Point (NetToolSystem)", StringComparison.Ordinal)
                || string.Equals(source, "Place Net Edge (NetToolSystem)", StringComparison.Ordinal)
                || string.Equals(source, "Place Net Node (NetToolSystem)", StringComparison.Ordinal)
                || string.Equals(source, "Undo Net Control Point (NetToolSystem)", StringComparison.Ordinal))
            {
                return true;
            }

            // If another UI overrides the display source, allow only these specific
            // NetToolSystem action names. The caller already checks the active tool.
            string name = hint.m_Action.name;
            return string.Equals(name, "Place Net Control Point", StringComparison.Ordinal)
                || string.Equals(name, "Place Net Edge", StringComparison.Ordinal)
                || string.Equals(name, "Place Net Node", StringComparison.Ordinal)
                || string.Equals(name, "Undo Net Control Point", StringComparison.Ordinal);
        }
    }
}
