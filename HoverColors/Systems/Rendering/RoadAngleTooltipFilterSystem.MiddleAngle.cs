// <copyright file="RoadAngleTooltipFilterSystem.MiddleAngle.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// Phase 2: place verified intermediate angle labels beside their control points.
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
        // Minimum center offset above the joint, scaled by render height.
        private const float kMiddleAngleOffsetAt1080 = 30f;
        private const float kWorldPositionTolerance = 0.25f;
        private const float kScreenPositionTolerance = 2f;

        private GuideLinesSystem? m_MiddleAngleGuideLines;
        private PrefabSystem? m_MiddleAnglePrefabs;
        private bool m_MiddleAngleLogged;
        private bool m_MultiAngleLogged;

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
            if (entries.Length == 0 || entries.Length > 32)
            {
                return;
            }

            NativeList<ControlPoint> points = netTool.GetControlPoints(out JobHandle pointDependencies);
            pointDependencies.Complete();
            bool multiJoint = mode == NetToolSystem.Mode.ComplexCurve && points.Length == 4;
            if (points.Length != 3 && !multiJoint)
            {
                return;
            }

            // Continuous can synthesize two different intermediate points.
            // Do not assume its generated geometry matches the tool's raw points.
            if (mode == NetToolSystem.Mode.Continuous
                && math.dot(points[2].m_Direction, points[1].m_Direction) <= -0.01f)
            {
                return;
            }

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

            Camera? camera = Camera.main;
            if (camera == null || !(snapDistance > 0f)) return;

            EnsureGuidelinePaths(entries.Length);
            float scale = GetLayoutScale();
            bool hasCursor = TryGetCursorBounds(tooltipUI, scale, out Rect cursorBounds);
            for (int jointIndex = 1; jointIndex < points.Length - 1; jointIndex++)
            {
                TryPlaceJointAngle(tooltipUI, entries, camera, points[jointIndex - 1].m_Position,
                    points[jointIndex].m_Position, points[jointIndex + 1].m_Position,
                    snapDistance, scale, multiJoint, hasCursor, cursorBounds);
            }

            // Retain the verified three-point reference-angle refinement only.
            if (!multiJoint && m_AnchoredMiddleAngles.Count == 1)
            {
                Vector3 start = camera.WorldToScreenPoint(points[0].m_Position);
                m_LayoutStart = new float2(start.x, Screen.height - start.y);
                m_HasLayoutStart = start.z > 0f && math.all(math.isfinite(m_LayoutStart));
            }

            if (multiJoint && m_AnchoredMiddleAngles.Count == 2 && !m_MultiAngleLogged)
            {
                m_MultiAngleLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: anchored both Complex curve PI angles");
            }
        }

        private void TryPlaceJointAngle(TooltipUISystem tooltipUI,
            NativeList<GuideLinesSystem.TooltipInfo> entries, Camera camera,
            float3 previous, float3 joint, float3 next, float snapDistance, float scale,
            bool multiJoint, bool hasCursor, Rect cursorBounds)
        {
            float2 incoming = joint.xz - previous.xz;
            float2 reverseOutgoing = joint.xz - next.xz;
            float incomingLength = math.length(incoming);
            float outgoingLength = math.length(reverseOutgoing);
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

            // Match both the rounded angle and vanilla world-space anchor.
            // Four-point curves have two intermediate entries; never identify
            // a widget by its angle alone, or guess when two entries coincide.
            int index = -1;
            for (int i = entries.Length - 1; i >= 0; i--)
            {
                GuideLinesSystem.TooltipInfo candidate = entries[i];
                if (candidate.m_Type != GuideLinesSystem.TooltipType.Angle
                    || candidate.m_Value != expectedAngle
                    || !(math.distancesq(candidate.m_Position, expectedWorldPosition)
                        <= kWorldPositionTolerance * kWorldPositionTolerance))
                {
                    continue;
                }
                if (index >= 0) return;
                index = i;
            }
            if (index < 0) return;
            GuideLinesSystem.TooltipInfo entry = entries[index];

            Vector3 oldScreen = camera.WorldToScreenPoint(entry.m_Position);
            Vector3 jointScreen = camera.WorldToScreenPoint(joint);
            if (!(oldScreen.z > 0f) || !(jointScreen.z > 0f))
            {
                return;
            }

            float2 oldPosition = new(oldScreen.x, Screen.height - oldScreen.y);
            TooltipGroup? group = FindMeasurement(tooltipUI, index, true);
            if (group == null || m_AnchoredMiddleAngles.Contains(group)
                || group.children[0] is not FloatTooltip angle || angle.value != entry.m_Value
                || !(math.distancesq(group.position, oldPosition)
                    <= kScreenPositionTolerance * kScreenPositionTolerance))
            {
                return;
            }
            float radius = snapDistance * 0.125f * 2.5f;
            // The handle is a world-space circle. Its apparent radius grows when
            // zooming in, so a fixed pixel offset alone cannot preserve clearance.
            float top = jointScreen.y;
            float bottom = jointScreen.y;
            float left = jointScreen.x;
            float right = jointScreen.x;
            for (int i = 0; i < 8; i++)
            {
                float radians = i * (Mathf.PI * 0.25f);
                float3 sample = joint;
                sample.xz += new float2(math.cos(radians), math.sin(radians)) * radius;
                Vector3 screen = camera.WorldToScreenPoint(sample);
                if (screen.z > 0f && math.isfinite(screen.y) && math.isfinite(screen.x))
                {
                    top = math.max(top, screen.y);
                    bottom = math.min(bottom, screen.y);
                    left = math.min(left, screen.x);
                    right = math.max(right, screen.x);
                }
            }

            // Keep the successful PI clearance while shrinking collision boxes.
            float clearance = 26f * scale;
            float offset = math.max(kMiddleAngleOffsetAt1080 * scale, top - jointScreen.y + clearance);
            float2 above = new(jointScreen.x, Screen.height - jointScreen.y - offset);
            float2 below = new(jointScreen.x, Screen.height - bottom + clearance);
            Vector3 startScreen = camera.WorldToScreenPoint(previous);
            Vector3 endScreen = camera.WorldToScreenPoint(next);
            // Choose the outward side: above for a high apex, below for a low
            // apex. For side-facing curves retain above as the stable default.
            bool preferBelow = startScreen.z > 0f && endScreen.z > 0f
                && (multiJoint
                    ? jointScreen.y < (startScreen.y + endScreen.y) * 0.5f
                    : jointScreen.y < math.min(startScreen.y, endScreen.y) - 18f * scale);
            float aboveCost = MiddleSideCost(above, tooltipUI, oldPosition, scale, hasCursor, cursorBounds);
            float belowCost = MiddleSideCost(below, tooltipUI, oldPosition, scale, hasCursor, cursorBounds);
            if (float.IsPositiveInfinity(aboveCost) && float.IsPositiveInfinity(belowCost)) return;
            // The other labels can move, so a modest overlap should not make
            // the anchor flicker between sides. Cursor/edge obstruction wins.
            Rect labelBounds = MeasurementRect(above, true, scale);
            float preference = (multiJoint ? 0.75f : 0.5f) * labelBounds.width * labelBounds.height;
            float2 newPosition = belowCost + (preferBelow ? 0f : preference)
                < aboveCost + (preferBelow ? preference : 0f) ? below : above;

            // Only reposition the verified existing widget, never its value.
            group.position = newPosition;
            group.SetChildrenChanged();
            m_AnchoredMiddleAngles.Add(group);
            float padding = 4f * scale;
            m_LayoutJointBounds.Add(new Rect(left - padding, Screen.height - top - padding,
                right - left + padding * 2f, top - bottom + padding * 2f));
            if (!m_MiddleAngleLogged)
            {
                m_MiddleAngleLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: moved middle {expectedAngle}° angle {(newPosition.y < Screen.height - jointScreen.y ? "above" : "below")} control point");
            }
        }

        private float MiddleSideCost(float2 center, TooltipUISystem tooltipUI,
            float2 oldPosition, float scale, bool hasCursor, Rect cursorBounds)
        {
            if (!math.all(math.isfinite(center))) return float.PositiveInfinity;
            Rect bounds = MeasurementRect(center, true, scale);
            if (bounds.xMin < 4f || bounds.yMin < 4f
                || bounds.xMax > Screen.width - 4f || bounds.yMax > Screen.height - 4f)
            {
                return float.PositiveInfinity;
            }

            // Never lock two PI labels on top of each other, a reserved handle,
            // or the cursor. If neither centered side fits, the general layout
            // can still search inward and around the existing measurements.
            if (hasCursor && OverlapArea(bounds, cursorBounds) > 0f)
            {
                return float.PositiveInfinity;
            }
            for (int i = 0; i < m_AnchoredMiddleAngles.Count; i++)
            {
                if (OverlapArea(bounds, MeasurementRect(m_AnchoredMiddleAngles[i].position, true, scale)) > 0f
                    || OverlapArea(bounds, m_LayoutJointBounds[i]) > 0f)
                {
                    return float.PositiveInfinity;
                }
            }

            float cost = 0f;
            for (int i = 0; i < tooltipUI.groups.Count; i++)
            {
                TooltipGroup group = tooltipUI.groups[i];
                if (m_AnchoredMiddleAngles.Contains(group)
                    || group.children.Count != 1 || group.children[0] is not FloatTooltip value
                    || group.horizontalAlignment != TooltipGroup.Alignment.Center
                    || group.verticalAlignment != TooltipGroup.Alignment.Center
                    || math.distancesq(group.position, oldPosition) <= 4f)
                {
                    continue;
                }

                bool angle = value.icon == "Media/Glyphs/Angle.svg";
                if (angle || value.icon == "Media/Glyphs/Length.svg")
                {
                    cost += OverlapArea(bounds, MeasurementRect(group.position, angle, scale));
                }
            }
            return cost;
        }
    }
}
