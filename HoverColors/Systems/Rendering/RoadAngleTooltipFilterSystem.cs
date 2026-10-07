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
    using Game;
    using Game.Prefabs;
    using Game.Tools;
    using Game.UI.Tooltip;
    using Unity.Entities;

    public partial class RoadAngleTooltipFilterSystem : GameSystemBase
    {
        private ToolSystem? m_ToolSystem;
        private PrefabSystem? m_PrefabSystem;
        private TooltipUISystem? m_TooltipUISystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_TooltipUISystem = World.GetOrCreateSystemManaged<TooltipUISystem>();
        }

        protected override void OnUpdate()
        {
            if (Mod.Settings?.RoadAngleTooltipsHidden != true
                || m_PrefabSystem == null
                || m_TooltipUISystem == null
                || m_ToolSystem?.activeTool is not NetToolSystem netTool
                || netTool.GetPrefab() is not PrefabBase netPrefab
                || !m_PrefabSystem.TryGetEntity(netPrefab, out Entity prefabEntity)
                || (netPrefab is not PathwayPrefab && !EntityManager.HasComponent<RoadData>(prefabEntity)))
            {
                return;
            }

            // The vanilla guideline and input-hint systems have already populated these lists.
            // Remove only angle boxes and the road/path Place/Undo hints before UI serialization.
            for (int i = m_TooltipUISystem.groups.Count - 1; i >= 0; i--)
            {
                TooltipGroup group = m_TooltipUISystem.groups[i];
                if (group.category == TooltipGroup.Category.Network
                    && group.children.Count == 1
                    && group.children[0] is FloatTooltip angle
                    && angle.unit == "angle"
                    && angle.icon == "Media/Glyphs/Angle.svg")
                {
                    m_TooltipUISystem.groups.RemoveAt(i);
                }
            }

            var mouseHints = m_TooltipUISystem.mouseGroup.children;
            for (int i = mouseHints.Count - 1; i >= 0; i--)
            {
                if (mouseHints[i] is InputHintTooltip hint && IsNetPlaceOrUndoHint(hint))
                {
                    mouseHints.RemoveAt(i);
                }
            }
        }

        private static bool IsNetPlaceOrUndoHint(InputHintTooltip hint)
        {
            string? source = hint.m_Action.displayOverride?.source;
            return string.Equals(source, "Place Net Control Point (NetToolSystem)", StringComparison.Ordinal)
                || string.Equals(source, "Place Net Edge (NetToolSystem)", StringComparison.Ordinal)
                || string.Equals(source, "Place Net Node (NetToolSystem)", StringComparison.Ordinal)
                || string.Equals(source, "Undo Net Control Point (NetToolSystem)", StringComparison.Ordinal);
        }
    }
}
