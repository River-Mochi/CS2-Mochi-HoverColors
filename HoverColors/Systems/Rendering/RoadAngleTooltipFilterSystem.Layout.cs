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
        private const float kPiAnchorRadius = 48f;
        private const float kPiMaxRadius = 64f;
        private const float kPiIncomingAngle = 12f;
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

            uint fixedGuides = GetFixedGuideMask(points, entries, mode, firstLength);
            // Ordinary Continuous uses the same native raw-point angle suffix
            // as Simple. Generated Continuous uses fitted/intersected joints
            // instead; retain its previously tested layout until independently
            // verified. The native guide-length block must exist for Continuous.
            bool ordinary = (mode == NetToolSystem.Mode.SimpleCurve && points.Length == 3)
                || (mode == NetToolSystem.Mode.ComplexCurve && (points.Length == 3 || points.Length == 4))
                || (mode == NetToolSystem.Mode.Continuous && !generated
                    && points.Length == 3 && firstLength >= 0);
            int lastLength = -1;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].m_Type == GuideLinesSystem.TooltipType.Length) lastLength = i;
            }
            int moved = 0;
            int crowded = 0;
            uint seen = 0;
            // Fixed leg distances first; PI labels next, before movable
            // starting references and final leg. Unknown ordinary angles keep
            // their native positions instead of inheriting arbitrary offsets.
            for (int pass = 0; pass < 4; pass++)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    GuideLinesSystem.TooltipInfo entry = entries[i];
                    bool angle = entry.m_Type == GuideLinesSystem.TooltipType.Angle;
                    bool length = entry.m_Type == GuideLinesSystem.TooltipType.Length;
                    uint bit = 1u << i;
                    bool fixedGuide = (fixedGuides & bit) != 0;
                    bool protectPi = ordinary && angle && i > lastLength && i >= startCount;
                    if ((pass == 0 && !fixedGuide)
                        || (pass == 1 && !protectPi)
                        || (pass == 2 && (!angle || protectPi))
                        || (pass == 3 && (!length || fixedGuide)))
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

                    // Never apply the general collision offset cache to an
                    // ordinary PI. Native angle suffix follows the length block.
                    // Match each PI independently, including when the other PI
                    // is too short to emit a tooltip. If uncertain, keep native.
                    if (protectPi)
                    {
                        float2 piPosition = original;
                        if (lastLength >= 0 && camera != null
                            && group.children[0] is FloatTooltip piValue && piValue.value == entry.m_Value
                            && TryGetCompactPiAnchor(camera, points, entry, original, scale,
                                out float2 center, out float2 direction, out float radius))
                        {
                            piPosition = FindPiPosition(center, direction, radius, original, scale);
                            if (!m_CompactPiLogged)
                            {
                                m_CompactPiLogged = true;
                                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: fixed-leg interior PI anchor active");
                            }
                        }

                        Rect piBounds = MeasurementRect(piPosition, true, scale);
                        if (GetOverlap(piBounds) > 0f) crowded++;
                        m_OccupiedMeasurements.Add(piBounds);
                        m_GuideClearUpdates[i] = 0;
                        if (!piPosition.Equals(original))
                        {
                            group.position = piPosition;
                            group.SetChildrenChanged();
                            moved++;
                        }
                        continue;
                    }

                    float2 preferred = original;
                    if (angle && hasStart && i < startCount
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

                    float2 position = FindClearPosition(anchor, desired, angle, scale, out bool clear);
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
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: native-anchor layout active; adjusted {moved} labels (fixed guide priority, persistent local offsets, fixed-leg interior PI anchors)");
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
            NativeList<GuideLinesSystem.TooltipInfo> entries, NetToolSystem.Mode mode, int firstLength)
        {
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
            uint mask = 0;
            for (int leg = 0; leg < legCount - 1; leg++)
            {
                mask |= 1u << (firstLength + leg);
            }
            return mask;
        }

        // Native DrawAngleIndicator places ordinary angle labels on the
        // interior bisector, at 1.5 times its arc radius. Verify the value,
        // height and native radial direction for a unique joint, not its index
        // or rounded value alone. A 180-degree reference has no unique interior
        // and keeps vanilla placement.
        private static bool TryGetCompactPiAnchor(Camera camera, NativeList<ControlPoint> points,
            GuideLinesSystem.TooltipInfo entry, float2 original, float scale,
            out float2 center, out float2 direction, out float radius)
        {
            center = default;
            direction = default;
            radius = 0f;
            if (entry.m_Type != GuideLinesSystem.TooltipType.Angle
                || !(entry.m_Value > 0f && entry.m_Value < 180f)
                || !math.all(math.isfinite(entry.m_Position)))
            {
                return false;
            }

            int match = -1;
            float2 matchedBack = default;
            float2 matchedForward = default;
            float matchedAngle = 0f;
            for (int j = 1; j < points.Length - 1; j++)
            {
                float3 joint = points[j].m_Position;
                float2 backVector = points[j - 1].m_Position.xz - joint.xz;
                float2 forwardVector = points[j + 1].m_Position.xz - joint.xz;
                float backLength = math.length(backVector);
                float forwardLength = math.length(forwardVector);
                if (!(backLength > 0.01f) || !(forwardLength > 0.01f)
                    || !math.isfinite(backLength) || !math.isfinite(forwardLength))
                {
                    continue;
                }

                float2 back = backVector / backLength;
                float2 forward = forwardVector / forwardLength;
                float radians = math.acos(math.clamp(math.dot(back, forward), -1f, 1f));
                float2 nativeRadial = entry.m_Position.xz - joint.xz;
                float nativeRadius = math.length(nativeRadial);
                // Vanilla caps the arc radius by half the shorter leg.
                if (!math.isfinite(radians) || math.abs(math.degrees(radians) - entry.m_Value) > 1f
                    || math.abs(entry.m_Position.y - joint.y) > 0.05f
                    || !(nativeRadius > 0.01f)
                    || !(nativeRadius <= math.min(backLength, forwardLength) * 0.75f + 0.05f)
                    || math.dot(nativeRadial / nativeRadius, math.normalizesafe(back + forward)) < 0.995f)
                {
                    continue;
                }

                if (match >= 0) return false;
                match = j;
                matchedBack = back;
                matchedForward = forward;
                matchedAngle = radians;
            }

            if (match < 0) return false;
            float3 pi = points[match].m_Position;
            Vector3 projected = camera.WorldToScreenPoint(pi);
            if (!(projected.z > 0f)) return false;
            center = new float2(projected.x, Screen.height - projected.y);
            float nativeScreenRadius = math.distance(original, center);
            if (!math.all(math.isfinite(center)) || !math.isfinite(nativeScreenRadius)
                || nativeScreenRadius <= 0.01f)
            {
                return false;
            }

            // A fixed angle from the incoming leg, on the interior side.
            // The direction is unchanged for a fixed incoming leg and turn
            // side until the angle becomes narrower than twice this offset.
            // No horizontal screen-axis preference: mirrored curves work alike.
            float turn = matchedBack.x * matchedForward.y - matchedBack.y * matchedForward.x;
            if (math.abs(turn) < 0.0001f) return false;
            float offset = math.min(math.radians(kPiIncomingAngle), matchedAngle * 0.5f)
                * math.sign(turn);
            float sin = math.sin(offset);
            float cos = math.cos(offset);
            float2 inward = new(matchedBack.x * cos - matchedBack.y * sin,
                matchedBack.x * sin + matchedBack.y * cos);

            // Project a ray in the same horizontal plane as the native arc.
            // This keeps the label center inside the sector under camera tilt.
            float3 sample = pi;
            sample.xz += inward;
            Vector3 ray = camera.WorldToScreenPoint(sample);
            if (!(ray.z > 0f)) return false;
            float2 radial = new float2(ray.x, Screen.height - ray.y) - center;
            direction = math.normalizesafe(radial);
            radius = math.min(nativeScreenRadius, kPiAnchorRadius * scale);
            return math.all(math.isfinite(direction)) && math.lengthsq(direction) > 0.5f
                && math.isfinite(radius);
        }

        private float2 FindPiPosition(float2 center, float2 direction, float radius,
            float2 original, float scale)
        {
            // All candidates lie on the same verified interior ray. No
            // arbitrary X/Y shift or persistent screen offset is allowed.
            // Prefer the fixed compact position; only consider radial changes
            // if a substantial part of the badge would overlap another label.
            float2 preferred = center + direction * radius;
            if (!preferred.Equals(ClampToScreen(preferred, true, scale)))
            {
                // Avoid clamping a label to a point outside its own angle.
                // Native fallback is safer when the ray leaves the viewport.
                return original;
            }

            Rect bounds = MeasurementRect(preferred, true, scale);
            float bestOverlap = GetOverlap(bounds);
            float threshold = bounds.width * bounds.height * 0.25f;
            if (bestOverlap <= threshold) return preferred;

            float2 best = preferred;
            for (int step = 1; step <= 2; step++)
            {
                for (int side = -1; side <= 1; side += 2)
                {
                    float candidateRadius = radius + side * step * 8f * scale;
                    if (candidateRadius < math.min(radius, 24f * scale)
                        || candidateRadius > math.min(kPiMaxRadius * scale, radius + 16f * scale))
                    {
                        continue;
                    }

                    float2 candidate = center + direction * candidateRadius;
                    if (!candidate.Equals(ClampToScreen(candidate, true, scale))) continue;
                    float overlap = GetOverlap(MeasurementRect(candidate, true, scale));
                    // Do not move for negligible collision improvements.
                    if (overlap + threshold * 0.5f < bestOverlap)
                    {
                        best = candidate;
                        bestOverlap = overlap;
                    }
                }
            }

            return best;
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

        private float2 FindClearPosition(float2 anchor, float2 preferred, bool angle, float scale,
            out bool clear)
        {
            // Prefer the last clear local offset during this preview. Bound all
            // candidates relative to the native anchor, not the previous offset.
            // There is no separate outside/inside placement state to oscillate.
            float maxDistance = (angle ? 96f : 144f) * scale;
            float maxDistanceSq = maxDistance * maxDistance;
            preferred = ClampToScreen(preferred, angle, scale);
            if (math.distancesq(preferred, anchor) > maxDistanceSq)
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
                for (int side = 0; side < 4; side++)
                {
                    float2 candidate = preferred;
                    switch (side)
                    {
                        case 0: candidate.x = obstacle.xMin - halfWidth - scale; break;
                        case 1: candidate.x = obstacle.xMax + halfWidth + scale; break;
                        case 2: candidate.y = obstacle.yMin - halfHeight - scale; break;
                        default: candidate.y = obstacle.yMax + halfHeight + scale; break;
                    }

                    candidate = ClampToScreen(candidate, angle, scale);
                    if (math.distancesq(candidate, anchor) > maxDistanceSq) continue;
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
            int maxRings = angle ? 8 : 12;
            for (int ring = 1; ring <= maxRings; ring++)
            {
                for (int y = -ring; y <= ring; y++)
                {
                    for (int x = -ring; x <= ring; x++)
                    {
                        if (math.abs(x) != ring && math.abs(y) != ring) continue;
                        float2 candidate = ClampToScreen(preferred + new float2(x, y) * step, angle, scale);
                        if (math.distancesq(candidate, anchor) > maxDistanceSq) continue;

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
