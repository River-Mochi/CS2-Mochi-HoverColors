// <copyright file="RoadAngleTooltipFilterSystem.Layout.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// Stable native-anchor layout of existing road measurements.
// Never change geometry, tooltip values, alignment, units or visibility.

namespace HoverColors.Systems
{
    using System.Collections.Generic;
    using CS2Shared.RiverMochi;
    using Game.Input;
    using Game.Prefabs;
    using Game.Rendering;
    using Game.Settings;
    using Game.Tools;
    using Game.UI.Tooltip;
    using Unity.Collections;
    using Unity.Jobs;
    using Unity.Mathematics;
    using UnityEngine;

    public partial class RoadAngleTooltipFilterSystem
    {
        private const int kMaxMeasurements = 32;
        private const int kGuideReturnClearUpdates = 8;
        private const float kGuideReturnStep = 2f;
        private const float kPiAnchorMaxRadius = 58f;
        private const float kPiCollisionLimit = 40f;
        private readonly List<Rect> m_OccupiedMeasurements = new(12);
        private readonly List<string> m_GuidelinePaths = new(8);
        private readonly float2[] m_LayoutOffsets = new float2[kMaxMeasurements];
        private readonly byte[] m_GuideClearUpdates = new byte[kMaxMeasurements];
        private GuideLinesSystem? m_LayoutGuideLines;
        private uint m_ValidOffsets;
        private uint m_PreviewAngleMask;
        private int m_PreviewFrame = -1;
        private int m_PreviewEntryCount;
        private int m_PreviewPointCount;
        private NetToolSystem.Mode m_PreviewMode;
        private NetPrefab? m_PreviewPrefab;
        private float3 m_PreviewStart;
        private bool m_PreviewGenerated;
        private bool m_LayoutLogged;
        private bool m_LayoutScaleLogged;
        private bool m_LayoutCrowdedLogged;
        private bool m_CompactPiLogged;

        private void InitializeMeasurementLayout()
        {
            m_LayoutGuideLines = World.GetOrCreateSystemManaged<GuideLinesSystem>();
        }

        private void ResetMeasurementLayout()
        {
            m_PreviewFrame = -1;
            m_PreviewPrefab = null;
            m_ValidOffsets = 0;
        }

        private void PrepareMeasurementLayout(NetToolSystem netTool, float3 start, int pointCount,
            int entryCount, uint angleMask, bool generated)
        {
            int frame = UnityEngine.Time.frameCount;
            if (m_PreviewFrame < 0 || frame - m_PreviewFrame > 3
                || m_PreviewMode != netTool.actualMode || m_PreviewPrefab != netTool.prefab
                || m_PreviewPointCount != pointCount || m_PreviewEntryCount != entryCount
                || m_PreviewAngleMask != angleMask || m_PreviewGenerated != generated
                || math.distancesq(start, m_PreviewStart) > 0.0625f)
            {
                m_ValidOffsets = 0;
                m_PreviewStart = start;
            }

            m_PreviewFrame = frame;
            m_PreviewMode = netTool.actualMode;
            m_PreviewPrefab = netTool.prefab;
            m_PreviewPointCount = pointCount;
            m_PreviewEntryCount = entryCount;
            m_PreviewAngleMask = angleMask;
            m_PreviewGenerated = generated;
        }

        private static float GetLayoutScale()
        {
            // The game also exposes a separate accessibility text scale.
            // Multiply the resolution estimate by that scale so larger UI text
            // receives more clearance. Do not assume the startup resolution.
            float textScale = SharedSettings.instance?.userInterface?.textScale ?? 1f;
            return Mathf.Clamp(Screen.height / 1080f * Mathf.Clamp(textScale, 1f, 1.5f), 0.65f, 4.5f);
        }

        private void ArrangeCurveMeasurements(NetToolSystem netTool, TooltipUISystem tooltipUI)
        {
            NetToolSystem.Mode mode = netTool.actualMode;
            if (m_LayoutGuideLines == null || tooltipUI.groups.Count == 0
                || (mode != NetToolSystem.Mode.Straight
                    && mode != NetToolSystem.Mode.SimpleCurve
                    && mode != NetToolSystem.Mode.ComplexCurve
                    && mode != NetToolSystem.Mode.Continuous))
            {
                ResetMeasurementLayout();
                return;
            }

            NativeList<GuideLinesSystem.TooltipInfo> entries =
                m_LayoutGuideLines.GetTooltips(out JobHandle dependencies);
            dependencies.Complete();
            if (entries.Length == 0 || entries.Length > kMaxMeasurements)
            {
                ResetMeasurementLayout();
                return;
            }

            NativeList<ControlPoint> points = netTool.GetControlPoints(out JobHandle pointDependencies);
            pointDependencies.Complete();
            if (points.Length < 2 || !math.all(math.isfinite(points[0].m_Position)))
            {
                ResetMeasurementLayout();
                return;
            }

            // Native DrawControlPoints emits the start references first, then
            // guide distances, then PI angles. Special Continuous emits up to
            // two start references followed by up to two generated PI angles.
            // Only use the ordered PI suffix after verifying the complete guide block.
            bool generated = mode == NetToolSystem.Mode.Continuous && points.Length >= 3
                && math.dot(points[points.Length - 1].m_Direction,
                    points[points.Length - 2].m_Direction) <= -0.01f;
            int firstLength = -1;
            uint angleMask = 0;
            bool allAngles = true;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].m_Type == GuideLinesSystem.TooltipType.Angle)
                {
                    angleMask |= 1u << i;
                }
                else
                {
                    allAngles = false;
                    if (firstLength < 0 && entries[i].m_Type == GuideLinesSystem.TooltipType.Length)
                    {
                        firstLength = i;
                    }
                }
            }

            int startCount = 0;
            if (firstLength > 0 && firstLength <= 2
                && (angleMask & ((1u << firstLength) - 1u)) == (1u << firstLength) - 1u)
            {
                startCount = firstLength;
            }
            else if (allAngles && mode == NetToolSystem.Mode.Straight && points.Length == 2 && entries.Length <= 2)
            {
                startCount = entries.Length;
            }
            else if (allAngles && generated && entries.Length <= 4)
            {
                // At most two PI entries exist, so only this many prefix
                // entries are certainly start references if a PI is absent.
                startCount = math.max(0, entries.Length - 2);
            }

            PrepareMeasurementLayout(netTool, points[0].m_Position, points.Length,
                entries.Length, angleMask, generated);
            EnsureGuidelinePaths(entries.Length);

            float scale = GetLayoutScale();
            if (!m_LayoutScaleLogged)
            {
                m_LayoutScaleLogged = true;
                InterfaceSettings? uiSettings = SharedSettings.instance?.userInterface;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter layout: screen={Screen.width}x{Screen.height}, textScale={uiSettings?.textScale ?? 1f:F2}, interfaceScaling={uiSettings?.interfaceScaling}, estimateScale={scale:F2}");
            }

            float2 startScreen = default;
            bool hasStart = false;
            Camera? camera = Camera.main;
            if (startCount > 0 && camera != null)
            {
                Vector3 projected = camera.WorldToScreenPoint(points[0].m_Position);
                startScreen = new float2(projected.x, Screen.height - projected.y);
                hasStart = projected.z > 0f && math.all(math.isfinite(startScreen));
            }

            m_OccupiedMeasurements.Clear();
            if (TryGetCursorBounds(tooltipUI, scale, out Rect cursorBounds))
            {
                m_OccupiedMeasurements.Add(cursorBounds);
            }

            uint fixedGuides = GetFixedGuideMask(points, entries, mode, firstLength, out bool completeGuideBlock);
            int piStart = firstLength + points.Length - 1;
            bool orderedPiSuffix = completeGuideBlock && entries.Length == piStart + points.Length - 2;
            int moved = 0;
            int crowded = 0;
            uint seen = 0;
            // Reserve fixed guide legs before angles and the moving final leg.
            // When native identity is uncertain, keep the existing first-guide
            // priority. All angles retain their native inside/reference anchor.
            for (int pass = 0; pass < 3; pass++)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    GuideLinesSystem.TooltipInfo entry = entries[i];
                    bool angle = entry.m_Type == GuideLinesSystem.TooltipType.Angle;
                    bool length = entry.m_Type == GuideLinesSystem.TooltipType.Length;
                    uint bit = 1u << i;
                    bool fixedGuide = (fixedGuides & bit) != 0;
                    if ((pass == 0 && !fixedGuide)
                        || (pass == 1 && !angle)
                        || (pass == 2 && (!length || fixedGuide)))
                    {
                        continue;
                    }

                    TooltipGroup? group = FindMeasurement(tooltipUI, i, angle);
                    if (group == null) continue;

                    float2 original = group.position;
                    if (!math.all(math.isfinite(original))) continue;

                    // Never pull a genuinely off-screen measurement into view.
                    float margin = 90f * scale;
                    if (original.x < -margin || original.x > Screen.width + margin
                        || original.y < -margin || original.y > Screen.height + margin)
                    {
                        continue;
                    }

                    float2 preferred = original;
                    float2 piCenter = default;
                    float2 piInward = default;
                    bool compactPi = angle && orderedPiSuffix && i >= piStart && camera != null
                        && group.children[0] is FloatTooltip piValue && piValue.value == entry.m_Value
                        && TryGetCompactPiAnchor(camera, points, i - piStart + 1, entry,
                            original, scale, out preferred, out piCenter, out piInward);
                    if (!compactPi) preferred = original;
                    if (compactPi && !m_CompactPiLogged)
                    {
                        m_CompactPiLogged = true;
                        LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: compact inside-PI anchor active");
                    }

                    if (!compactPi && angle && hasStart && i < startCount
                        && group.children[0] is FloatTooltip startAngle && startAngle.value == entry.m_Value)
                    {
                        // Continuous radial compression, not a threshold jump.
                        // Keep the native bearing and avoid the start handle.
                        float2 radial = original - startScreen;
                        float distance = math.length(radial);
                        if (distance > 0.01f)
                        {
                            float shift = math.min(distance * 0.30f, 100f * scale);
                            shift = math.min(shift, math.max(0f, distance - 70f * scale));
                            preferred -= radial / distance * shift;
                        }
                    }

                    float2 anchor = ClampToScreen(preferred, angle, scale);
                    float2 desired = anchor;
                    if ((m_ValidOffsets & bit) != 0)
                    {
                        // Reuse the offset, not the old screen position.
                        // The label continues tracking its live native anchor.
                        desired = anchor + m_LayoutOffsets[i] * scale;
                        if (length && !fixedGuide)
                        {
                            // Only a displaced, non-fixed distance may ease back
                            // toward its own guide. Wait for sustained clear space
                            // and never jump straight back after a brief opening.
                            desired = EaseGuideOffset(i, anchor, desired, scale);
                        }
                        else
                        {
                            m_GuideClearUpdates[i] = 0;
                        }
                    }
                    else
                    {
                        m_GuideClearUpdates[i] = 0;
                    }

                    // Bound the extra movement of a verified PI more tightly than
                    // other angles. Never send it across the joint to clear a label.
                    float2 position = FindClearPosition(anchor, desired, angle, scale,
                        compactPi, piCenter, piInward, out bool clear);
                    if (!clear || !position.Equals(desired))
                    {
                        m_GuideClearUpdates[i] = 0;
                    }
                    m_LayoutOffsets[i] = (position - anchor) / scale;
                    seen |= bit;
                    if (!clear) crowded++;
                    m_OccupiedMeasurements.Add(MeasurementRect(position, angle, scale));
                    if (!position.Equals(original))
                    {
                        group.position = position;
                        group.SetChildrenChanged();
                        moved++;
                    }
                }
            }
            m_ValidOffsets = seen;

            if (moved > 0 && !m_LayoutLogged)
            {
                m_LayoutLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: native-anchor layout active; adjusted {moved} labels (fixed guide priority, persistent local offsets, compact PI anchors)");
            }
            if (crowded > 0 && !m_LayoutCrowdedLogged)
            {
                m_LayoutCrowdedLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: {crowded} crowded labels after bounded local search ({mode}, screen={Screen.width}x{Screen.height})");
            }
        }

        // Verify native guide-length entries against the actual consecutive
        // control-point midpoints before assigning earlier legs priority.
        // Ordinary Simple/Complex previews have one moving final leg.
        // Keep the previous first-guide priority in other configurations.
        private static uint GetFixedGuideMask(NativeList<ControlPoint> points,
            NativeList<GuideLinesSystem.TooltipInfo> entries, NetToolSystem.Mode mode, int firstLength,
            out bool completeGuideBlock)
        {
            completeGuideBlock = false;
            if (firstLength < 0) return 0;
            uint fallback = 1u << firstLength;
            bool ordinary = (mode == NetToolSystem.Mode.SimpleCurve && points.Length == 3)
                || (mode == NetToolSystem.Mode.ComplexCurve && (points.Length == 3 || points.Length == 4));
            int legCount = points.Length - 1;
            if (!ordinary || firstLength + legCount > entries.Length) return fallback;

            for (int leg = 0; leg < legCount; leg++)
            {
                GuideLinesSystem.TooltipInfo entry = entries[firstLength + leg];
                float3 start = points[leg].m_Position;
                float3 end = points[leg + 1].m_Position;
                float length = math.distance(start.xz, end.xz);
                if (entry.m_Type != GuideLinesSystem.TooltipType.Length
                    || !math.isfinite(length) || !(entry.m_Value > 0f)
                    || Mathf.RoundToInt(length * 2f) * 0.5f != entry.m_Value
                    || !(math.distancesq(entry.m_Position, (start + end) * 0.5f) <= 0.0025f))
                {
                    return fallback;
                }
            }

            // Exclude the final leg (the live mouse-controlled endpoint).
            completeGuideBlock = true;
            uint mask = 0;
            for (int leg = 0; leg < legCount - 1; leg++)
            {
                mask |= 1u << (firstLength + leg);
            }
            return mask;
        }

        // The complete native length block and PI count establish joint order.
        // Retain the native angle's interior side, but bias toward its fixed
        // incoming leg and reduce the radius. This reduces movement when only
        // the outgoing leg changes, symmetrically in either drawing direction.
        private static bool TryGetCompactPiAnchor(Camera camera, NativeList<ControlPoint> points,
            int jointIndex, GuideLinesSystem.TooltipInfo entry, float2 original, float scale,
            out float2 preferred, out float2 center, out float2 inward)
        {
            preferred = original;
            center = default;
            inward = default;
            if (jointIndex < 1 || jointIndex >= points.Length - 1
                || entry.m_Type != GuideLinesSystem.TooltipType.Angle
                || !(entry.m_Value > 0f && entry.m_Value <= 180f))
            {
                return false;
            }

            float3 joint = points[jointIndex].m_Position;
            float3 previous = points[jointIndex - 1].m_Position;
            float3 next = points[jointIndex + 1].m_Position;
            float2 back = math.normalizesafe(previous.xz - joint.xz);
            float2 forward = math.normalizesafe(next.xz - joint.xz);
            if (math.lengthsq(back) < 0.5f || math.lengthsq(forward) < 0.5f) return false;

            float angle = math.degrees(math.acos(math.clamp(math.dot(back, forward), -1f, 1f)));
            if (!math.isfinite(angle) || math.abs(angle - entry.m_Value) > 1f
                || !math.all(math.isfinite(entry.m_Position))
                || math.abs(entry.m_Position.y - joint.y) > 0.05f)
            {
                return false;
            }

            Vector3 jointScreen = camera.WorldToScreenPoint(joint);
            Vector3 backScreen = camera.WorldToScreenPoint(previous);
            if (jointScreen.z <= 0f || backScreen.z <= 0f) return false;
            center = new float2(jointScreen.x, Screen.height - jointScreen.y);
            float2 backPos = new(backScreen.x, Screen.height - backScreen.y);
            float2 radial = original - center;
            float radius = math.length(radial);
            if (!math.all(math.isfinite(center)) || !math.all(math.isfinite(backPos))
                || !math.isfinite(radius) || radius < 0.01f)
            {
                return false;
            }

            inward = radial / radius;
            // For ordinary angles the native anchor must lie along the
            // interior bisector. At 180 degrees the native normal is retained.
            float2 nativeWorld = math.normalizesafe(entry.m_Position.xz - joint.xz);
            if (entry.m_Value < 180f
                && math.dot(nativeWorld, math.normalizesafe(back + forward)) < 0.98f)
            {
                return false;
            }

            // Keep the location within the native radius (including tight
            // curves), so this does not push a label away from its own joint.
            // Bias toward the incoming leg, using screen-space direction only.
            float2 backDirection = math.normalizesafe(backPos - center);
            float2 direction = math.normalizesafe(inward * 0.65f + backDirection * 0.35f, inward);
            float compactRadius = math.min(radius, math.min(kPiAnchorMaxRadius * scale,
                math.max(26f * scale, radius * 0.60f)));
            preferred = center + direction * compactRadius;
            return math.all(math.isfinite(preferred));
        }

        private float2 EaseGuideOffset(int index, float2 anchor, float2 desired, float scale)
        {
            float2 delta = anchor - desired;
            float distance = math.length(delta);
            if (distance <= 0.01f || !math.isfinite(distance)
                || GetOverlap(MeasurementRect(anchor, false, scale)) > 0f
                || GetOverlap(MeasurementRect(desired, false, scale)) > 0f)
            {
                m_GuideClearUpdates[index] = 0;
                return desired;
            }

            if (m_GuideClearUpdates[index] < kGuideReturnClearUpdates)
            {
                m_GuideClearUpdates[index]++;
                return desired;
            }

            float2 candidate = ClampToScreen(desired + delta / distance
                * math.min(distance, kGuideReturnStep * scale), false, scale);
            if (GetOverlap(MeasurementRect(candidate, false, scale)) == 0f)
            {
                return candidate;
            }

            m_GuideClearUpdates[index] = 0;
            return desired;
        }

        private void EnsureGuidelinePaths(int count)
        {
            while (m_GuidelinePaths.Count < count)
            {
                m_GuidelinePaths.Add($"guideLineTooltip{m_GuidelinePaths.Count}");
            }
        }

        private TooltipGroup? FindMeasurement(TooltipUISystem tooltipUI, int index, bool angle)
        {
            string icon = angle ? "Media/Glyphs/Angle.svg" : "Media/Glyphs/Length.svg";
            for (int i = 0; i < tooltipUI.groups.Count; i++)
            {
                TooltipGroup group = tooltipUI.groups[i];
                if (group.path == m_GuidelinePaths[index]
                    && group.horizontalAlignment == TooltipGroup.Alignment.Center
                    && group.verticalAlignment == TooltipGroup.Alignment.Center
                    && group.children.Count == 1
                    && group.children[0] is FloatTooltip value
                    && value.icon == icon
                    && (!angle || value.unit == "angle"))
                {
                    return group;
                }
            }

            return null;
        }

        private static bool TryGetCursorBounds(TooltipUISystem tooltipUI, float scale, out Rect bounds)
        {
            bounds = default;
            if (tooltipUI.mouseGroup.children.Count == 0 || !InputManager.instance.mouseOnScreen)
            {
                return false;
            }

            Vector3 mouse = InputManager.instance.mousePosition;
            float2 cursor = new(mouse.x, Screen.height - mouse.y + 16f);
            if (!math.all(math.isfinite(cursor)))
            {
                return false;
            }

            bounds = new Rect(cursor.x - 8f * scale, cursor.y - 8f * scale,
                160f * scale, (tooltipUI.mouseGroup.children.Count * 38f + 16f) * scale);
            return true;
        }

        // Estimate visible boxes from 1080p in-game measurements, with modest
        // padding. Large conservative boxes sent distances too far from their guides.
        // COHTML does not expose the actual rendered bounds to this C# pass.
        private static Rect MeasurementRect(float2 center, bool angle, float scale)
        {
            float width = (angle ? 100f : 116f) * scale;
            float height = 34f * scale;
            return new Rect(center.x - width * 0.5f, center.y - height * 0.5f, width, height);
        }

        private static float2 ClampToScreen(float2 point, bool angle, float scale)
        {
            Rect bounds = MeasurementRect(point, angle, scale);
            float halfWidth = bounds.width * 0.5f + 4f;
            float halfHeight = bounds.height * 0.5f + 4f;
            return new float2(
                Mathf.Clamp(point.x, halfWidth, math.max(halfWidth, Screen.width - halfWidth)),
                Mathf.Clamp(point.y, halfHeight, math.max(halfHeight, Screen.height - halfHeight)));
        }

        private static float OverlapArea(Rect a, Rect b)
        {
            return math.max(0f, math.min(a.xMax, b.xMax) - math.max(a.xMin, b.xMin))
                * math.max(0f, math.min(a.yMax, b.yMax) - math.max(a.yMin, b.yMin));
        }

        private float GetOverlap(Rect bounds)
        {
            float area = 0f;
            for (int i = 0; i < m_OccupiedMeasurements.Count; i++)
            {
                area += OverlapArea(bounds, m_OccupiedMeasurements[i]);
            }
            return area;
        }

        private static bool IsLocalCandidate(float2 candidate, float2 anchor, float maxDistanceSq,
            bool compactPi, float2 piCenter, float2 piInward)
        {
            return math.distancesq(candidate, anchor) <= maxDistanceSq
                && (!compactPi || math.dot(candidate - piCenter, piInward) >= 0f);
        }

        private float2 FindClearPosition(float2 anchor, float2 preferred, bool angle, float scale,
            bool compactPi, float2 piCenter, float2 piInward, out bool clear)
        {
            // Prefer the last clear local offset during this preview. Bound all
            // candidates relative to the native anchor, not the previous offset.
            // There is no separate outside/inside placement state to oscillate.
            float maxDistance = (compactPi ? kPiCollisionLimit : angle ? 96f : 144f) * scale;
            float maxDistanceSq = maxDistance * maxDistance;
            preferred = ClampToScreen(preferred, angle, scale);
            if (!IsLocalCandidate(preferred, anchor, maxDistanceSq, compactPi, piCenter, piInward))
            {
                preferred = anchor;
            }
            float2 best = preferred;
            float bestOverlap = GetOverlap(MeasurementRect(best, angle, scale));
            float bestCost = 0f;
            if (bestOverlap == 0f)
            {
                clear = true;
                return best;
            }

            float anchorOverlap = GetOverlap(MeasurementRect(anchor, angle, scale));
            if (anchorOverlap < bestOverlap)
            {
                best = anchor;
                bestOverlap = anchorOverlap;
                bestCost = math.distancesq(anchor, preferred);
            }

            // Try the nearest edges of occupied boxes first, rather than
            // jumping a whole search step past a narrow but usable gap.
            Rect preferredBounds = MeasurementRect(preferred, angle, scale);
            float halfWidth = preferredBounds.width * 0.5f;
            float halfHeight = preferredBounds.height * 0.5f;
            for (int i = 0; i < m_OccupiedMeasurements.Count; i++)
            {
                Rect obstacle = m_OccupiedMeasurements[i];
                // For compact PI labels also try a vertically aligned badge
                // beside a nearby obstacle, without moving or merging widgets.
                for (int side = 0; side < (compactPi ? 6 : 4); side++)
                {
                    float2 candidate = preferred;
                    switch (side)
                    {
                        case 0: candidate.x = obstacle.xMin - halfWidth - scale; break;
                        case 1: candidate.x = obstacle.xMax + halfWidth + scale; break;
                        case 2: candidate.y = obstacle.yMin - halfHeight - scale; break;
                        case 3: candidate.y = obstacle.yMax + halfHeight + scale; break;
                        case 4:
                            candidate.x = obstacle.center.x;
                            candidate.y = obstacle.yMin - halfHeight - scale;
                            break;
                        default:
                            candidate.x = obstacle.center.x;
                            candidate.y = obstacle.yMax + halfHeight + scale;
                            break;
                    }

                    candidate = ClampToScreen(candidate, angle, scale);
                    if (!IsLocalCandidate(candidate, anchor, maxDistanceSq, compactPi, piCenter, piInward)) continue;
                    float overlap = GetOverlap(MeasurementRect(candidate, angle, scale));
                    float cost = math.distancesq(candidate, preferred);
                    if (overlap < bestOverlap || (overlap == bestOverlap && cost < bestCost))
                    {
                        best = candidate;
                        bestOverlap = overlap;
                        bestCost = cost;
                    }
                }
            }

            float step = 12f * scale;
            int maxRings = compactPi ? 4 : angle ? 8 : 12;
            for (int ring = 1; ring <= maxRings; ring++)
            {
                for (int y = -ring; y <= ring; y++)
                {
                    for (int x = -ring; x <= ring; x++)
                    {
                        if (math.abs(x) != ring && math.abs(y) != ring) continue;
                        float2 candidate = ClampToScreen(preferred + new float2(x, y) * step, angle, scale);
                        if (!IsLocalCandidate(candidate, anchor, maxDistanceSq, compactPi, piCenter, piInward)) continue;

                        float overlap = GetOverlap(MeasurementRect(candidate, angle, scale));
                        float cost = math.distancesq(candidate, preferred);
                        if (overlap < bestOverlap || (overlap == bestOverlap && cost < bestCost))
                        {
                            best = candidate;
                            bestOverlap = overlap;
                            bestCost = cost;
                        }
                    }
                }

                if (bestOverlap == 0f)
                {
                    // Next ring can only improve on a diagonal solution.
                    float nextDistance = (ring + 1) * step;
                    if (bestCost <= nextDistance * nextDistance) break;
                }
            }

            clear = bestOverlap == 0f;
            // Never drop a measurement. If a tight curve cannot fit all labels
            // locally, prefer a small overlap to a distant, misleading label.
            return best;
        }
    }
}
