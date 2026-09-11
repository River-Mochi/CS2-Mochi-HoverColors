// <copyright file="OutlineColorSystem.HoverToggle.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Systems/OutlineColorSystem.HoverToggle.cs
// Eye-toggle behavior. Never patches or changes the game's raycast/selection result.

namespace HoverColors.Systems
{
    using Game.Common;
    using Game.Tools;
    using HoverColors;
    using Unity.Entities;

    public partial class OutlineColorSystem
    {
        private const int kOwnerMatchDepth = 4;

        private ToolRaycastSystem? m_ToolRaycastSystem;

        private void InitializeHoverToggle()
        {
            // Use vanilla's system if it already exists. Never create a raycast system just for HC.
            m_ToolRaycastSystem = World.GetExistingSystemManaged<ToolRaycastSystem>();
        }

        private void ApplyHoverToggle(
            HoverColorsSettings settings,
            ToolBaseSystem? activeToolSystem,
            ref float outlineA,
            ref float fillA,
            ref float ownerA,
            ref EffectivePalette palette)
        {
            // Only normal Default Tool hover is hidden. Construction/tool safety colors stay untouched.
            if (!settings.HoverHighlightsSuppressed || activeToolSystem is not DefaultToolSystem)
            {
                return;
            }

            // Vanilla owns selection + click sound. We only decide the effective render alpha.
            if (IsSelectedUnderPointer())
            {
                return;
            }

            outlineA = 0f;
            fillA = 0f;
            ownerA = 0f;

            // Force custom writes so zero alpha is respected even if RGB matches vanilla.
            palette = EffectivePalette.Custom;
        }

        private bool IsSelectedUnderPointer()
        {
            if (m_ToolSystem == null)
            {
                return false;
            }

            Entity selected = m_ToolSystem.selected;
            if (selected == Entity.Null || !EntityManager.Exists(selected))
            {
                return false;
            }

            // ToolRaycastSystem should already exist in Game/Editor. Retry lookup only if it was
            // not ready when HC was created; never create or patch it.
            m_ToolRaycastSystem ??= World.GetExistingSystemManaged<ToolRaycastSystem>();
            if (m_ToolRaycastSystem == null)
            {
                return false;
            }

            // Reads vanilla's already-computed result. It does not cast again and never modifies it.
            if (!m_ToolRaycastSystem.GetRaycastResult(out RaycastResult result))
            {
                return false;
            }

            return MatchesSelectedEntity(selected, result.m_Owner)
                || MatchesSelectedEntity(selected, result.m_Hit.m_HitEntity);
        }

        private bool MatchesSelectedEntity(Entity selected, Entity candidate)
        {
            if (candidate == Entity.Null || !EntityManager.Exists(candidate))
            {
                return false;
            }

            if (candidate == selected)
            {
                return true;
            }

            // Generic owner matching handles common subobjects/icons without type-specific scans.
            return OwnerChainContains(candidate, selected)
                || OwnerChainContains(selected, candidate);
        }

        private bool OwnerChainContains(Entity start, Entity target)
        {
            Entity current = start;
            for (int i = 0; i < kOwnerMatchDepth; i++)
            {
                if (current == Entity.Null
                    || !EntityManager.Exists(current)
                    || !EntityManager.HasComponent<Owner>(current))
                {
                    return false;
                }

                current = EntityManager.GetComponentData<Owner>(current).m_Owner;
                if (current == target)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
