// <copyright file="RoadAngleTooltipFilterSystem.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/Rendering/RoadAngleTooltipFilterSystem.cs
// Purpose: Hide net preview angle and placement hints while preserving cost and measurements.

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
        }

        protected override void OnUpdate()
        {
            // The toggle is intended for NEW road/path construction only.
            // Other networks (rail, pipes, power, etc.) must keep their vanilla hints.
            // RoadPrefab and PathwayPrefab can be identified directly, without the
            // fragile ECS PrefabSystem.TryGetEntity + RoadData prerequisite.
            if (Mod.Settings?.RoadAngleTooltipsHidden != true
                || m_TooltipUISystem == null
                || m_ToolSystem?.activeTool is not NetToolSystem netTool
                || netTool.actualMode == NetToolSystem.Mode.Replace)
            {
                return;
            }

            if (netTool.GetPrefab() is not RoadPrefab
                && netTool.GetPrefab() is not PathwayPrefab)
            {
                return;
            }

            // One-time diagnostic to distinguish "system not executing" from "no match"
            // without spamming logs for every drag frame.
            if (!m_ExecutionLogged)
            {
                m_ExecutionLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road tooltip filter running (road/path build, hide ON)");
            }

            // Runs after vanilla InputHintsTooltipSystem in UITooltip, before
            // TooltipUISystem serializes groups. Keep cost, length and slope intact.
            int removedAngles = 0;
            for (int i = m_TooltipUISystem.groups.Count - 1; i >= 0; i--)
            {
                TooltipGroup group = m_TooltipUISystem.groups[i];
                if (group.category == TooltipGroup.Category.Network
                    && group.children.Count == 1
                    && group.children[0] is FloatTooltip angle
                    && angle.unit == "angle")
                {
                    m_TooltipUISystem.groups.RemoveAt(i);
                    removedAngles++;
                }
            }

            int removedHints = 0;
            var mouseHints = m_TooltipUISystem.mouseGroup.children;
            for (int i = mouseHints.Count - 1; i >= 0; i--)
            {
                if (mouseHints[i] is InputHintTooltip hint && IsNetPlaceOrUndoHint(hint))
                {
                    mouseHints.RemoveAt(i);
                    removedHints++;
                }
            }

            // Only once per city/world: proof in HoverColors.log that the filter ran.
            // No frame-by-frame logging.
            if (!m_FilterLogged && (removedAngles != 0 || removedHints != 0))
            {
                m_FilterLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road tooltip filter active: " +
                    $"removed {removedAngles} angle, {removedHints} Place/Undo hints");
            }
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
