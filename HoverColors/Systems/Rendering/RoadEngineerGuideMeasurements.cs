// <copyright file="RoadEngineerGuideMeasurements.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

namespace HoverColors.Systems
{
    using Game.Rendering;
    using Game.Tools;
    using Unity.Collections;
    using Unity.Mathematics;
    using UnityEngine;

    // Recover unrounded measurements only when native identity is verified.
    // No widget mutation, no additional tooltip groups, no tool geometry changes.
    // Source: 1.6.4f1 GuideLinesSystem.DrawControlPoints / DrawAngleIndicator.
    internal static class RoadEngineerGuideMeasurements
    {
        private const float kAnchorTolerance = 0.05f;

        // Ordinary Simple / Complex / Continuous guide lengths. The special
        // generated Continuous branch has no native guide-length entries.
        internal static bool TryReadDistance(NativeList<ControlPoint> points,
            GuideLinesSystem.TooltipInfo entry, out float precise)
        {
            precise = default;
            if (entry.m_Type != GuideLinesSystem.TooltipType.Length
                || points.Length < 3 || points.Length > 4 || !(entry.m_Value > 0f))
            {
                return false;
            }

            int matches = 0;
            for (int i = 1; i < points.Length; i++)
            {
                float3 start = points[i - 1].m_Position;
                float3 end = points[i].m_Position;
                float length = math.distance(start.xz, end.xz);
                if (!math.isfinite(length)
                    || Mathf.RoundToInt(length * 2f) * 0.5f != entry.m_Value
                    || !MatchesAnchor(entry.m_Position, (start + end) * 0.5f))
                {
                    continue;
                }

                precise = length;
                matches++;
            }

            if (matches == 1) return true;
            precise = default;
            return false;
        }

        // Only ordinary PI entries after the native guide-length block.
        // Never identify a start-reference angle or generated Continuous PI by
        // its rounded value alone. Those cases need their own verified matching.
        internal static bool TryReadOrdinaryPi(NativeList<ControlPoint> points,
            NativeList<GuideLinesSystem.TooltipInfo> entries, int index,
            NetToolSystem.Mode mode, float snapDistance, out float precise)
        {
            precise = default;
            if (index < 0 || index >= entries.Length || points.Length < 3
                || points.Length > 4 || !math.isfinite(snapDistance) || snapDistance <= 0f
                || (mode != NetToolSystem.Mode.SimpleCurve
                    && mode != NetToolSystem.Mode.ComplexCurve
                    && mode != NetToolSystem.Mode.Continuous))
            {
                return false;
            }
            if (mode == NetToolSystem.Mode.Continuous
                && math.dot(points[points.Length - 1].m_Direction,
                    points[points.Length - 2].m_Direction) <= -0.01f)
            {
                return false;
            }

            GuideLinesSystem.TooltipInfo entry = entries[index];
            if (entry.m_Type != GuideLinesSystem.TooltipType.Angle
                || !(entry.m_Value > 0f && entry.m_Value < 180f))
            {
                return false;
            }

            int lastLength = -1;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].m_Type == GuideLinesSystem.TooltipType.Length) lastLength = i;
            }
            if (lastLength < 0 || index <= lastLength) return false;

            // Match the vanilla prefab snap-distance cap and minimum leg size.
            float snap = math.min(snapDistance, 16f);
            float minLength = snap * 0.125f * 7f;
            int matches = 0;
            for (int i = 1; i < points.Length - 1; i++)
            {
                float3 joint = points[i].m_Position;
                float2 incoming = joint.xz - points[i - 1].m_Position.xz;
                float2 outgoing = joint.xz - points[i + 1].m_Position.xz;
                float len1 = math.length(incoming);
                float len2 = math.length(outgoing);
                if (!(len1 > minLength) || !(len2 > minLength)
                    || !math.isfinite(len1) || !math.isfinite(len2)) continue;

                float2 dir1 = incoming / len1;
                float2 dir2 = outgoing / len2;
                float angle = math.degrees(math.acos(math.clamp(math.dot(dir1, dir2), -1f, 1f)));
                if (!math.isfinite(angle) || Mathf.RoundToInt(angle) != entry.m_Value) continue;

                float size = math.min(snap * 4f, math.min(len1, len2)) * 0.5f;
                float3 anchor = joint;
                anchor.xz -= math.normalizesafe(dir1 + dir2) * (size * 1.5f);
                if (!MatchesAnchor(entry.m_Position, anchor)) continue;

                precise = angle;
                matches++;
            }

            if (matches == 1) return true;
            precise = default;
            return false;
        }

        private static bool MatchesAnchor(float3 native, float3 predicted)
        {
            return math.all(math.isfinite(native)) && math.all(math.isfinite(predicted))
                && math.distancesq(native, predicted) <= kAnchorTolerance * kAnchorTolerance;
        }
    }
}
