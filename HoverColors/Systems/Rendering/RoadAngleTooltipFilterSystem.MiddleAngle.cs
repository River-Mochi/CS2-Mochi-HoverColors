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
    using Colossal.Mathematics;
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
        private const float kContinuousWorldTolerance = 2f;
        private const float kContinuousScreenTolerance = 16f;

        private GuideLinesSystem? m_MiddleAngleGuideLines;
        private PrefabSystem? m_MiddleAnglePrefabs;
        private bool m_MiddleAngleLogged;
        private bool m_MultiAngleLogged;
        private bool m_ContinuousAnglesLogged;
        private bool m_ContinuousToleranceLogged;
        private bool m_ContinuousMatchMissLogged;
        private bool m_PiNearbyLogged;
        // 0 = undecided, -1 = above, +1 = below. Keep only the side,
        // never a stale screen coordinate or native widget reference.
        private int m_FirstPiSide;
        private int m_SecondPiSide;
        private int m_PiPreviewFrame = -1;
        private NetToolSystem.Mode m_PiPreviewMode;
        private NetPrefab? m_PiPreviewPrefab;
        private float3 m_PiPreviewStart;
        private int m_PiPreviewPointCount;
        private bool m_PiPreviewGenerated;
        private bool m_StablePiLogged;

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
                ResetPiPlacementState();
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
            // Match the special Continuous branch of vanilla DrawControlPoints.
            // Its rendered guide handles need not coincide with raw tool points.
            bool generatedContinuous = mode == NetToolSystem.Mode.Continuous
                && points.Length >= 3
                && math.dot(points[points.Length - 1].m_Direction,
                    points[points.Length - 2].m_Direction) <= -0.01f;
            bool multiJoint = generatedContinuous
                || (mode == NetToolSystem.Mode.ComplexCurve && points.Length == 4);
            if (points.Length != 3 && !multiJoint)
            {
                ResetPiPlacementState();
                return;
            }

            PreparePiPlacementState(netTool, mode, points[0].m_Position,
                points.Length, generatedContinuous);

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
            if (generatedContinuous)
            {
                // Reconstruct only the two guide positions in local values.
                // Never write to the tool's control points or native tooltip list.
                ControlPoint start = points[0];
                ControlPoint middle = points[points.Length - 2];
                ControlPoint end = points[points.Length - 1];
                if (!TryGetContinuousGuideJoints(start, middle, end,
                    out float3 firstJoint, out float3 secondJoint))
                {
                    return;
                }

                // The special vanilla branch appends the two joint angles in
                // guide order and emits no segment-length entries. Validate BOTH
                // trailing entries before allowing a small managed/Burst mismatch.
                // If one joint is missing, use the original strict matching only.
                bool verifiedPair = TryVerifyContinuousPair(entries, camera, start.m_Position,
                    firstJoint, secondJoint, end.m_Position, snapDistance, scale);
                if (verifiedPair)
                {
                    SetStartingReferenceLayout(camera, start.m_Position, entries.Length - 2);
                }

                ReservePiHandle(camera, firstJoint, snapDistance, scale);
                ReservePiHandle(camera, secondJoint, snapDistance, scale);
                TryPlaceJointAngle(tooltipUI, entries, camera, start.m_Position,
                    firstJoint, secondJoint, snapDistance, scale, true, hasCursor, cursorBounds,
                    jointSlot: 0, verifiedIndex: verifiedPair ? entries.Length - 2 : -1);
                TryPlaceJointAngle(tooltipUI, entries, camera, firstJoint,
                    secondJoint, end.m_Position, snapDistance, scale, true, hasCursor, cursorBounds,
                    jointSlot: 1, verifiedIndex: verifiedPair ? entries.Length - 1 : -1);

                if (m_AnchoredMiddleAngles.Count == 2 && !m_ContinuousAnglesLogged)
                {
                    m_ContinuousAnglesLogged = true;
                    LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: anchored both generated Continuous curve PI angles");
                }

                return;
            }

            // Vanilla adds starting angles before segment lengths, then PI
            // angles. A length boundary is required before classifying a prefix.
            // If that boundary is missing, do not guess which angles are starts.
            int startingCount = 0;
            while (startingCount < entries.Length
                && entries[startingCount].m_Type == GuideLinesSystem.TooltipType.Angle)
            {
                startingCount++;
            }
            if (startingCount > 0 && startingCount <= 2 && startingCount < entries.Length
                && entries[startingCount].m_Type == GuideLinesSystem.TooltipType.Length)
            {
                SetStartingReferenceLayout(camera, points[0].m_Position, startingCount);
            }

            for (int jointIndex = 1; jointIndex < points.Length - 1; jointIndex++)
            {
                ReservePiHandle(camera, points[jointIndex].m_Position, snapDistance, scale);
            }
            for (int jointIndex = 1; jointIndex < points.Length - 1; jointIndex++)
            {
                TryPlaceJointAngle(tooltipUI, entries, camera, points[jointIndex - 1].m_Position,
                    points[jointIndex].m_Position, points[jointIndex + 1].m_Position,
                    snapDistance, scale, multiJoint, hasCursor, cursorBounds, jointSlot: jointIndex - 1);
            }

            if (multiJoint && m_AnchoredMiddleAngles.Count == 2 && !m_MultiAngleLogged)
            {
                m_MultiAngleLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: anchored both Complex curve PI angles");
            }
        }

        private void ResetPiPlacementState()
        {
            m_PiPreviewFrame = -1;
            m_PiPreviewPrefab = null;
            m_FirstPiSide = 0;
            m_SecondPiSide = 0;
        }

        private void PreparePiPlacementState(NetToolSystem netTool, NetToolSystem.Mode mode,
            float3 start, int pointCount, bool generated)
        {
            int frame = UnityEngine.Time.frameCount;
            if (m_PiPreviewFrame < 0 || frame - m_PiPreviewFrame > 3
                || m_PiPreviewMode != mode || m_PiPreviewPrefab != netTool.prefab
                || m_PiPreviewPointCount != pointCount || m_PiPreviewGenerated != generated
                || math.distancesq(start, m_PiPreviewStart) > 0.0625f)
            {
                m_FirstPiSide = 0;
                m_SecondPiSide = 0;
                m_PiPreviewStart = start;
            }

            m_PiPreviewFrame = frame;
            m_PiPreviewMode = mode;
            m_PiPreviewPrefab = netTool.prefab;
            m_PiPreviewPointCount = pointCount;
            m_PiPreviewGenerated = generated;
        }

        private void SetStartingReferenceLayout(Camera camera, float3 start, int count)
        {
            Vector3 screen = camera.WorldToScreenPoint(start);
            m_LayoutStart = new float2(screen.x, Screen.height - screen.y);
            m_HasLayoutStart = count > 0 && screen.z > 0f && math.all(math.isfinite(m_LayoutStart));
            m_LayoutStartAngleCount = count;
        }

        // Reserve both actual intermediate handles before placing either angle.
        // A default rect keeps the slot index stable when projection is invalid.
        private void ReservePiHandle(Camera camera, float3 joint, float snapDistance, float scale)
        {
            Vector3 center = camera.WorldToScreenPoint(joint);
            if (!(center.z > 0f) || !math.isfinite(center.x) || !math.isfinite(center.y))
            {
                m_LayoutJointBounds.Add(default);
                return;
            }

            float radius = snapDistance * 0.125f * 2.5f;
            float top = center.y;
            float bottom = center.y;
            float left = center.x;
            float right = center.x;
            for (int i = 0; i < 8; i++)
            {
                float radians = i * (Mathf.PI * 0.25f);
                float3 sample = joint;
                sample.xz += new float2(math.cos(radians), math.sin(radians)) * radius;
                Vector3 screen = camera.WorldToScreenPoint(sample);
                if (screen.z > 0f && math.isfinite(screen.x) && math.isfinite(screen.y))
                {
                    top = math.max(top, screen.y);
                    bottom = math.min(bottom, screen.y);
                    left = math.min(left, screen.x);
                    right = math.max(right, screen.x);
                }
            }

            float padding = 4f * scale;
            m_LayoutJointBounds.Add(new Rect(left - padding, Screen.height - top - padding,
                right - left + padding * 2f, top - bottom + padding * 2f));
        }

        // Mirrors the guide-only Continuous calculation in 1.6.4f1
        // GuideLinesSystem.NetToolGuideLinesJob.DrawControlPoints. Reuse the
        // game's curve and intersection helpers to avoid a second approximation.
        private static bool TryGetContinuousGuideJoints(ControlPoint start, ControlPoint middle,
            ControlPoint end, out float3 firstJoint, out float3 secondJoint)
        {
            firstJoint = middle.m_Position;
            secondJoint = middle.m_Position;
            if (!math.all(math.isfinite(start.m_Position))
                || !math.all(math.isfinite(middle.m_Position))
                || !math.all(math.isfinite(end.m_Position))
                || !math.all(math.isfinite(middle.m_Direction))
                || !math.all(math.isfinite(end.m_Direction)))
            {
                return false;
            }

            float3 startTangent = new(middle.m_Direction.x, 0f, middle.m_Direction.y);
            float3 endTangent = new(end.m_Direction.x, 0f, end.m_Direction.y);
            float alignment = math.dot(math.normalizesafe(end.m_Position.xz - start.m_Position.xz),
                middle.m_Direction);
            Bezier4x3 curve;
            if (alignment <= -0.01f)
            {
                float3 endPosition = end.m_Position + endTangent * alignment;
                curve = Game.Net.NetUtils.FitCurve(start.m_Position, startTangent, endTangent, endPosition);
                curve.d = end.m_Position;
            }
            else
            {
                curve = Game.Net.NetUtils.FitCurve(start.m_Position, startTangent, endTangent, end.m_Position);
            }

            float2 midpoint = MathUtils.Position(curve, 0.5f).xz;
            float2 tangent = MathUtils.Tangent(curve, 0.5f).xz;
            if (!math.all(math.isfinite(midpoint)) || !math.all(math.isfinite(tangent)))
            {
                return false;
            }

            Line2 middleLine = new(midpoint, midpoint + tangent);
            Line2 startLine = new(start.m_Position.xz, start.m_Position.xz + middle.m_Direction);
            Line2 endLine = new(end.m_Position.xz, end.m_Position.xz - end.m_Direction);

            // Vanilla leaves the corresponding intermediate point unchanged on
            // a failed intersection. Preserve that fallback and its original Y.
            if (MathUtils.Intersect(startLine, middleLine, out float2 firstIntersection))
            {
                firstJoint.xz = MathUtils.Position(startLine, firstIntersection.x);
            }

            if (MathUtils.Intersect(endLine, middleLine, out float2 secondIntersection))
            {
                secondJoint.xz = MathUtils.Position(endLine, secondIntersection.x);
            }

            // Each resulting joint must still match a unique native tooltip's
            // angle, world anchor and screen position before it can be moved.
            return math.all(math.isfinite(firstJoint)) && math.all(math.isfinite(secondJoint));
        }

        private static bool TryGetJointMeasurement(float3 previous, float3 joint, float3 next,
            float snapDistance, out int expectedAngle, out float3 expectedWorldPosition)
        {
            expectedAngle = 0;
            expectedWorldPosition = default;
            float2 incoming = joint.xz - previous.xz;
            float2 reverseOutgoing = joint.xz - next.xz;
            float incomingLength = math.length(incoming);
            float outgoingLength = math.length(reverseOutgoing);
            float minLength = snapDistance * 0.125f * 7f;
            if (!math.isfinite(incomingLength) || !math.isfinite(outgoingLength)
                || !(snapDistance > 0f) || !(incomingLength > minLength)
                || !(outgoingLength > minLength))
            {
                return false;
            }

            float2 dir1 = incoming / incomingLength;
            float2 dir2 = reverseOutgoing / outgoingLength;
            expectedAngle = Mathf.RoundToInt(math.degrees(math.acos(math.clamp(math.dot(dir1, dir2), -1f, 1f))));
            // Zero has no angle widget; 180 uses a different side-dependent anchor.
            if (expectedAngle <= 0 || expectedAngle >= 180)
            {
                return false;
            }

            float size = math.min(snapDistance * 4f, math.min(incomingLength, outgoingLength)) * 0.5f;
            expectedWorldPosition = joint;
            expectedWorldPosition.xz -= math.normalizesafe(dir1 + dir2) * (size * 1.5f);
            return math.all(math.isfinite(expectedWorldPosition));
        }

        private bool TryVerifyContinuousPair(NativeList<GuideLinesSystem.TooltipInfo> entries,
            Camera camera, float3 start, float3 first, float3 second, float3 end,
            float snapDistance, float scale)
        {
            if (entries.Length < 2
                || !TryGetJointMeasurement(start, first, second, snapDistance,
                    out int firstAngle, out float3 firstAnchor)
                || !TryGetJointMeasurement(first, second, end, snapDistance,
                    out int secondAngle, out float3 secondAnchor))
            {
                return false;
            }

            // The native sequence must consist only of angles (at most two
            // starting reference angles followed by the two generated PI angles).
            if (entries.Length > 4) return false;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].m_Type != GuideLinesSystem.TooltipType.Angle) return false;
            }

            GuideLinesSystem.TooltipInfo firstEntry = entries[entries.Length - 2];
            GuideLinesSystem.TooltipInfo secondEntry = entries[entries.Length - 1];
            // Do not use relaxed matching if the same native anchor could
            // reasonably belong to either joint.
            if (math.distancesq(firstAnchor, secondAnchor)
                <= 4f * kContinuousWorldTolerance * kContinuousWorldTolerance)
            {
                return false;
            }

            bool verified = MatchesContinuousAnchor(firstEntry, firstAngle, firstAnchor, camera, scale)
                && MatchesContinuousAnchor(secondEntry, secondAngle, secondAnchor, camera, scale);
            if (!verified && !m_ContinuousMatchMissLogged)
            {
                m_ContinuousMatchMissLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Continuous: pair verification fallback; "
                    + $"expected={firstAngle}/{secondAngle}, native={firstEntry.m_Value}/{secondEntry.m_Value}, "
                    + $"worldDelta={math.distance(firstAnchor, firstEntry.m_Position):F2}/{math.distance(secondAnchor, secondEntry.m_Position):F2}");
            }

            return verified;
        }

        private static bool MatchesContinuousAnchor(GuideLinesSystem.TooltipInfo entry,
            int expectedAngle, float3 expectedAnchor, Camera camera, float scale)
        {
            if (entry.m_Type != GuideLinesSystem.TooltipType.Angle
                || !(entry.m_Value > 0f && entry.m_Value < 180f)
                || !(math.abs(entry.m_Value - expectedAngle) <= 1f)
                || !(math.distancesq(entry.m_Position, expectedAnchor)
                    <= kContinuousWorldTolerance * kContinuousWorldTolerance))
            {
                return false;
            }

            Vector3 actual = camera.WorldToScreenPoint(entry.m_Position);
            Vector3 predicted = camera.WorldToScreenPoint(expectedAnchor);
            float screenTolerance = kContinuousScreenTolerance * scale;
            return actual.z > 0f && predicted.z > 0f
                && math.distancesq(new float2(actual.x, actual.y), new float2(predicted.x, predicted.y))
                    <= screenTolerance * screenTolerance;
        }

        private void TryPlaceJointAngle(TooltipUISystem tooltipUI,
            NativeList<GuideLinesSystem.TooltipInfo> entries, Camera camera,
            float3 previous, float3 joint, float3 next, float snapDistance, float scale,
            bool multiJoint, bool hasCursor, Rect cursorBounds,
            int jointSlot, int verifiedIndex = -1)
        {
            if (!TryGetJointMeasurement(previous, joint, next, snapDistance,
                out int expectedAngle, out float3 expectedWorldPosition))
            {
                return;
            }

            // Match both the rounded angle and vanilla world-space anchor.
            // Four-point curves have two intermediate entries; never identify
            // a widget by its angle alone, or guess when two entries coincide.
            int index = verifiedIndex;
            if (index < 0)
            {
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
            }

            if (index < 0) return;
            GuideLinesSystem.TooltipInfo entry = entries[index];
            bool usedTolerance = verifiedIndex >= 0 && (entry.m_Value != expectedAngle
                || math.distancesq(entry.m_Position, expectedWorldPosition)
                    > kWorldPositionTolerance * kWorldPositionTolerance);

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
            Rect handle = m_LayoutJointBounds[jointSlot];
            if (!(handle.width > 0f) || !(handle.height > 0f)) return;

            // Handle bounds already include 4px of padding. Preserve the
            // previously tested 26px clearance from the real circle edge.
            float clearance = 22f * scale;
            float2 above = new(jointScreen.x, math.min(Screen.height - jointScreen.y
                - kMiddleAngleOffsetAt1080 * scale, handle.yMin - clearance));
            float2 below = new(jointScreen.x, handle.yMax + clearance);

            int side = jointSlot == 0 ? m_FirstPiSide : m_SecondPiSide;
            if (side == 0)
            {
                Vector3 startScreen = camera.WorldToScreenPoint(previous);
                Vector3 endScreen = camera.WorldToScreenPoint(next);
                bool preferBelow = startScreen.z > 0f && endScreen.z > 0f
                    && (multiJoint
                        ? jointScreen.y < (startScreen.y + endScreen.y) * 0.5f
                        : jointScreen.y < math.min(startScreen.y, endScreen.y) - 18f * scale);
                side = preferBelow ? 1 : -1;
            }

            // Keep the chosen side for this preview. Other ordinary angle and
            // length labels can move later; they must not cause a PI side flip.
            float2 preferred = side > 0 ? below : above;
            float2 opposite = side > 0 ? above : below;
            bool nearby = false;
            float2 newPosition = preferred;
            if (!IsPiPositionClear(preferred, scale, hasCursor, cursorBounds))
            {
                if (TryFindNearbyPiPosition(preferred, side, scale, hasCursor, cursorBounds, out newPosition))
                {
                    nearby = true;
                }
                else
                {
                    side = -side;
                    newPosition = opposite;
                    if (!IsPiPositionClear(opposite, scale, hasCursor, cursorBounds))
                    {
                        if (!TryFindNearbyPiPosition(opposite, side, scale, hasCursor, cursorBounds, out newPosition))
                        {
                            return;
                        }
                        nearby = true;
                    }
                }
            }

            // Persist the successful side, even after an obstruction clears.
            // Reproject the position from the live circle every frame.
            if (jointSlot == 0) m_FirstPiSide = side;
            else m_SecondPiSide = side;

            group.position = newPosition;
            group.SetChildrenChanged();
            m_AnchoredMiddleAngles.Add(group);
            if (!m_StablePiLogged)
            {
                m_StablePiLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: stable PI side placement active");
            }
            if (nearby && !m_PiNearbyLogged)
            {
                m_PiNearbyLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: used near-handle placement fallback");
            }
            if (usedTolerance && !m_ContinuousToleranceLogged)
            {
                m_ContinuousToleranceLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Continuous: verified ordered PI with bounded tolerance "
                    + $"(expected={expectedAngle}, native={entry.m_Value}, worldDelta={math.distance(expectedWorldPosition, entry.m_Position):F2})");
            }
            if (!m_MiddleAngleLogged)
            {
                m_MiddleAngleLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: moved middle {expectedAngle}° angle {(newPosition.y < Screen.height - jointScreen.y ? "above" : "below")} control point");
            }
        }

        private bool TryFindNearbyPiPosition(float2 center, int side, float scale,
            bool hasCursor, Rect cursorBounds, out float2 result)
        {
            result = default;
            // Prefer the smallest shift on the current side. At most twelve
            // candidates, with no scan of movable angle/distance widgets.
            for (int step = 1; step <= 4; step++)
            {
                float delta = step * 12f * scale;
                for (int axis = 0; axis < 3; axis++)
                {
                    float2 candidate = center + (axis == 0 ? new float2(0f, side * delta)
                        : axis == 1 ? new float2(-delta, 0f) : new float2(delta, 0f));
                    if (IsPiPositionClear(candidate, scale, hasCursor, cursorBounds))
                    {
                        result = candidate;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsPiPositionClear(float2 center, float scale, bool hasCursor, Rect cursorBounds)
        {
            if (!math.all(math.isfinite(center))) return false;
            Rect bounds = MeasurementRect(center, true, scale);
            if (bounds.xMin < 4f || bounds.yMin < 4f
                || bounds.xMax > Screen.width - 4f || bounds.yMax > Screen.height - 4f
                || (hasCursor && OverlapArea(bounds, cursorBounds) > 0f))
            {
                return false;
            }

            for (int i = 0; i < m_AnchoredMiddleAngles.Count; i++)
            {
                if (OverlapArea(bounds, MeasurementRect(m_AnchoredMiddleAngles[i].position, true, scale)) > 0f)
                {
                    return false;
                }
            }
            for (int i = 0; i < m_LayoutJointBounds.Count; i++)
            {
                if (OverlapArea(bounds, m_LayoutJointBounds[i]) > 0f) return false;
            }

            return true;
        }
    }
}
