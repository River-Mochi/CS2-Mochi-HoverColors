// <copyright file="RoadAngleTooltipPruneSystem.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/Rendering/RoadAngleTooltipPruneSystem.cs
// Purpose: Remove ONLY angle labels from vanilla guideline tooltip data before UI construction.
// Leaves visible blue guideline overlays, lengths, slopes and costs unchanged.

namespace HoverColors.Systems
{
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Rendering;
    using Game.Tools;
    using Unity.Collections;
    using Unity.Jobs;

    public partial class RoadAngleTooltipPruneSystem : GameSystemBase
    {
        private ToolSystem? m_ToolSystem;
        private GuideLinesSystem? m_GuideLinesSystem;
        private bool m_Logged;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_GuideLinesSystem = World.GetOrCreateSystemManaged<GuideLinesSystem>();
        }

        protected override void OnUpdate()
        {
            if (m_GuideLinesSystem == null || !RoadAngleTooltipFilterSystem.ShouldFilter(m_ToolSystem))
            {
                return;
            }

            // Vanilla GuideLineTooltipSystem completes this job immediately afterward.
            // Completing here allows us to filter its small NativeList before vanilla
            // builds angle/length UI widgets. No Harmony, no mesh or preview changes.
            NativeList<GuideLinesSystem.TooltipInfo> items =
                m_GuideLinesSystem.GetTooltips(out JobHandle dependencies);
            dependencies.Complete();

            // Compact in place so the remaining length labels keep their order and
            // the vanilla guideLineTooltip{index} widget paths stay stable.
            int kept = 0;
            int originalCount = items.Length;
            for (int i = 0; i < originalCount; i++)
            {
                GuideLinesSystem.TooltipInfo item = items[i];
                if (item.m_Type != GuideLinesSystem.TooltipType.Angle)
                {
                    if (kept != i)
                    {
                        items[kept] = item;
                    }

                    kept++;
                }
            }

            int removed = originalCount - kept;
            if (removed > 0)
            {
                items.ResizeUninitialized(kept);
            }

            if (removed > 0 && !m_Logged)
            {
                m_Logged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road angle tooltip prune active: removed {removed} angle entries before vanilla UI");
            }
        }
    }
}
