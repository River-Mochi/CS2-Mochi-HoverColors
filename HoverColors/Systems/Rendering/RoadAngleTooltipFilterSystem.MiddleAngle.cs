// <copyright file="RoadAngleTooltipFilterSystem.MiddleAngle.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// Phase 2 experiment: move the existing V1/V2 angle label above its control point.
// Do not change road geometry, snapping, guide lines, or the displayed angle.

namespace HoverColors.Systems
{
    using CS2Shared.RiverMochi;
    using Game.Prefabs;
    using Game.Rendering;
    using Game.Tools;
    using Game.UI.Tooltip;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Jobs;
    using Unity.Mathematics;
    using UnityEngine;

    public partial class RoadAngleTooltipFilterSystem
    {
        // Screen-space clearance above the circle, scaled by render height.
        // Tune this value if the vanilla tooltip font/scale requires more room.
        private const float kMiddleAngleOffsetAt1080 = 32f;
        private const float kWorldPositionTolerance = 0.25f;
        private const float kScreenPositionTolerance = 2f;

        private GuideLinesSystem? m_MiddleAngleGuideLines;
        private PrefabSystem? m_MiddleAnglePrefabs;
        private int m_LastMiddleAngleIndex = -1;
        private string m_MiddleAnglePath = string.Empty;
        private bool m_MiddleAngleLogged;

        private void InitializeMiddleAnglePlacement()
        {
            m_MiddleAngleGuideLines = World.GetOrCreateSystemManaged<GuideLinesSystem>();
            m_MiddleAnglePrefabs = World.GetOrCreateSystemManaged<PrefabSystem>();
        }

        private void TryMoveMiddleAngle(NetToolSystem netTool, TooltipUISystem tooltipUI)
        {
            NetToolSystem.Mode mode = netTool.actualMode;
            if (tooltipUI.groups.Count == 0 || m_MiddleAngleGuideLines == null
                || m_MiddleAnglePrefabs == null
                || (mode != NetToolSystem.Mode.SimpleCurve
                    && mode != NetToolSystem.Mode.ComplexCurve
                    && mode != NetToolSystem.Mode.Continuous))
            {
                return;
            }

            // Vanilla and Extended Tooltip have already consumed this list.
            // Read only, with the producer's dependencies completed. Never mutate it.
            NativeList<GuideLinesSystem.TooltipInfo> entries =
                m_MiddleAngleGuideLines.GetTooltips(out JobHandle guideDependencies);
            guideDependencies.Complete();
            if (entries.Length == 0)
            {
                return;
            }

            NativeList<ControlPoint> points = netTool.GetControlPoints(out JobHandle pointDependencies);
            pointDependencies.Complete();
            if (points.Length != 3)
            {
                return;
            }

            // Continuous can synthesize two different intermediate points.
            // Leave that special path, and all multi-joint curves, entirely vanilla.
            if (mode == NetToolSystem.Mode.Continuous
                && math.dot(points[2].m_Direction, points[1].m_Direction) <= -0.01f)
            {
                return;
            }

            float3 joint = points[1].m_Position;
            float2 incoming = joint.xz - points[0].m_Position.xz;
            float2 reverseOutgoing = joint.xz - points[2].m_Position.xz;
            float incomingLength = math.length(incoming);
            float outgoingLength = math.length(reverseOutgoing);

            // Match the 1.6.4 NetToolGuideLinesJob.DrawControlPoints size rules.
            // The optional prefab lookup is local to this experiment only; a miss
            // cannot disable the main road clutter filter.
            float snapDistance = 1f;
            NetPrefab prefab = netTool.prefab;
            if (prefab != null)
            {
                if (!m_MiddleAnglePrefabs.TryGetEntity(prefab, out Entity prefabEntity))
                {
                    return;
                }

                if (EntityManager.HasComponent<PlaceableNetData>(prefabEntity))
                {
                    snapDistance = math.min(EntityManager.GetComponentData<PlaceableNetData>(prefabEntity).m_SnapDistance, 16f);
                }
            }

            float minLength = snapDistance * 0.125f * 7f;
            if (!math.isfinite(incomingLength) || !math.isfinite(outgoingLength)
                || !(snapDistance > 0f) || !(incomingLength > minLength)
                || !(outgoingLength > minLength))
            {
                return;
            }

            float2 dir1 = incoming / incomingLength;
            float2 dir2 = reverseOutgoing / outgoingLength;
            int expectedAngle = Mathf.RoundToInt(math.degrees(math.acos(math.clamp(math.dot(dir1, dir2), -1f, 1f))));
            // Zero has no angle widget; 180 uses a different side-dependent anchor.
            if (expectedAngle <= 0 || expectedAngle >= 180)
            {
                return;
            }

            float size = math.min(snapDistance * 4f, math.min(incomingLength, outgoingLength)) * 0.5f;
            float3 expectedWorldPosition = joint;
            expectedWorldPosition.xz -= math.normalizesafe(dir1 + dir2) * (size * 1.5f);

            // For the ordinary three-point road preview the joint angle is appended
            // last, after start/snap angles and V1/V2 lengths. Verify its geometry
            // AND angle so a different 90-degree label cannot be selected by value.
            int index = entries.Length - 1;
            GuideLinesSystem.TooltipInfo entry = entries[index];
            if (entry.m_Type != GuideLinesSystem.TooltipType.Angle
                || entry.m_Value != expectedAngle
                || !(math.distancesq(entry.m_Position, expectedWorldPosition)
                    <= kWorldPositionTolerance * kWorldPositionTolerance))
            {
                return;
            }

            Camera? camera = Camera.main;
            if (camera == null)
            {
                return;
            }

            Vector3 oldScreen = camera.WorldToScreenPoint(entry.m_Position);
            Vector3 jointScreen = camera.WorldToScreenPoint(joint);
            if (!(oldScreen.z > 0f) || !(jointScreen.z > 0f))
            {
                return;
            }

            float2 oldPosition = new(oldScreen.x, Screen.height - oldScreen.y);
            float offset = Mathf.Clamp(kMiddleAngleOffsetAt1080 * Screen.height / 1080f, 20f, 80f);
            float2 newPosition = new(jointScreen.x, Screen.height - jointScreen.y - offset);
            if (!math.all(math.isfinite(newPosition))
                || newPosition.x < offset || newPosition.x > Screen.width - offset
                || newPosition.y < offset || newPosition.y > Screen.height - offset)
            {
                return;
            }

            // Both vanilla and Extended Tooltip use the index as the widget path.
            // Cache the path instead of allocating one every frame.
            if (m_LastMiddleAngleIndex != index)
            {
                m_LastMiddleAngleIndex = index;
                m_MiddleAnglePath = $"guideLineTooltip{index}";
            }

            for (int i = 0; i < tooltipUI.groups.Count; i++)
            {
                TooltipGroup group = tooltipUI.groups[i];
                if (group.path != m_MiddleAnglePath
                    || group.children.Count != 1
                    || group.children[0] is not FloatTooltip angle
                    || angle.unit != "angle"
                    || angle.icon != "Media/Glyphs/Angle.svg"
                    || angle.value != entry.m_Value
                    || !(math.distancesq(group.position, oldPosition)
                        <= kScreenPositionTolerance * kScreenPositionTolerance))
                {
                    continue;
                }

                // Only reposition the existing widget. Vanilla resets the position
                // next frame; with the filter OFF this method is never called.
                group.position = newPosition;
                group.SetChildrenChanged();
                if (!m_MiddleAngleLogged)
                {
                    m_MiddleAngleLogged = true;
                    LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: moved middle {expectedAngle}° angle above control point ({mode})");
                }

                return;
            }
        }
    }
}
