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
    using Game.Tools;
    using Game.UI.Tooltip;

    public partial class RoadAngleTooltipFilterSystem : GameSystemBase
    {
        private ToolSystem? m_ToolSystem;
        private TooltipUISystem? m_TooltipUISystem;
        private bool m_FilterLogged;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_TooltipUISystem = World.GetOrCreateSystemManaged<TooltipUISystem>();
        }

        protected override void OnUpdate()
        {
            // No prefab lookup here. NetToolSystem can draw using either its selected
            // prefab or a lane prefab; GetPrefab()/TryGetEntity()/RoadData gating could
            // cause the entire filter to return before processing vanilla tooltips.
            if (Mod.Settings?.RoadAngleTooltipsHidden != true
                || m_TooltipUISystem == null
                || m_ToolSystem?.activeTool is not NetToolSystem)
            {
                return;
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
            // NetToolSystem retrieves these actions by these exact names in 1.6.4f1.
            // DisplayNameOverride.source is presentation metadata and can vary across
            // game versions or modded UI; the action's map/name are stable identifiers.
            if (!string.Equals(hint.m_Action.mapName, "NetToolSystem", StringComparison.Ordinal))
            {
                return false;
            }

            string name = hint.m_Action.name;
            return string.Equals(name, "Place Net Control Point", StringComparison.Ordinal)
                || string.Equals(name, "Place Net Edge", StringComparison.Ordinal)
                || string.Equals(name, "Place Net Node", StringComparison.Ordinal)
                || string.Equals(name, "Undo Net Control Point", StringComparison.Ordinal);
        }
    }
}
