// <copyright file="RoadAngleTooltipFilterSystem.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/Rendering/RoadAngleTooltipFilterSystem.cs
// Purpose: Hide road angle readouts without changing other game or mod tooltips.

namespace HoverColors.Systems
{
    using Game;
    using Game.Prefabs;
    using Game.Rendering;
    using Game.Tools;
    using Game.UI.Tooltip;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Jobs;

    public partial class RoadAngleTooltipFilterSystem : GameSystemBase
    {
        private GuideLinesSystem? m_GuideLinesSystem;
        private ToolSystem? m_ToolSystem;
        private PrefabSystem? m_PrefabSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_GuideLinesSystem = World.GetOrCreateSystemManaged<GuideLinesSystem>();
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
        }

        protected override void OnUpdate()
        {
            if (Mod.Settings?.RoadAngleTooltipsHidden != true
                || m_GuideLinesSystem == null
                || m_PrefabSystem == null
                || m_ToolSystem?.activeTool is not NetToolSystem netTool
                || netTool.GetPrefab() is not PrefabBase roadPrefab
                || !m_PrefabSystem.TryGetEntity(roadPrefab, out Entity prefabEntity)
                || !EntityManager.HasComponent<RoadData>(prefabEntity))
            {
                return;
            }

            NativeList<GuideLinesSystem.TooltipInfo> tooltips = m_GuideLinesSystem.GetTooltips(out JobHandle dependencies);
            dependencies.Complete();
            for (int i = tooltips.Length - 1; i >= 0; i--)
            {
                if (tooltips[i].m_Type == GuideLinesSystem.TooltipType.Angle)
                {
                    tooltips.RemoveAtSwapBack(i);
                }
            }
        }
    }
}
