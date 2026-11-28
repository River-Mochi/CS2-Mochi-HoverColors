// <copyright file="RoadEngineerCourseMeasurements.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

namespace HoverColors.Systems
{
    using Colossal.Mathematics;
    using Game.Tools;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    // Read-only result in native metres and percent, independent of screen position.
    internal readonly struct RoadEngineerCourseMeasurements
    {
        public readonly float TotalLength;
        public readonly float Distance;
        public readonly float Elevation;
        public readonly float Relief;
        public readonly float Slope;
        public readonly float Bearing;
        public readonly float MaxCourseGrade;
        public readonly float3 Start;
        public readonly float3 End;

        private RoadEngineerCourseMeasurements(float totalLength, float3 start, float3 end, float maxCourseGrade)
        {
            TotalLength = totalLength;
            MaxCourseGrade = maxCourseGrade;
            Start = start;
            End = end;
            Distance = math.distance(start.xz, end.xz);
            Elevation = end.y;
            Relief = end.y - start.y;
            Slope = 100f * Relief / totalLength;
            // Map-relative north is +Z (camera yaw zero), east is +X.
            // A closed/zero-chord preview has no meaningful endpoint bearing.
            float2 direction = end.xz - start.xz;
            Bearing = Distance > 0.01f
                ? (math.degrees(math.atan2(direction.x, direction.y)) + 360f) % 360f
                : float.NaN;
        }

        // Same eligibility as vanilla 1.6.4f1 NetCourseTooltipSystem.OnUpdate.
        // Callers must complete the relevant preview-query dependencies first.
        internal static bool IsEligible(in CreationDefinition definition, in NetCourse course)
        {
            const CreationFlags kExcluded = CreationFlags.Permanent | CreationFlags.Delete
                | CreationFlags.Upgrade | CreationFlags.Invert | CreationFlags.Align;

            return definition.m_Original == Entity.Null
                && (definition.m_Flags & kExcluded) == 0
                && (course.m_StartPosition.m_Flags & CoursePosFlags.IsParallel) == 0;
        }

        // Input contains only eligible courses of the current preview.
        // The native tooltip totals cut horizontal Bezier lengths. Use the same
        // native math, but verify the first/last flags and connectivity before
        // reporting signed elevation differences. Do not rely on ECS query order.
        // This starter supports one connected chain; ambiguous multi-chain
        // previews intentionally return false rather than invent a summary.
        internal static bool TryRead(NativeArray<NetCourse> courses,
            out RoadEngineerCourseMeasurements result)
        {
            result = default;
            int count = courses.Length;
            if (count == 0 || count > 64) return false;

            int first = -1;
            int last = -1;
            float length = 0f;
            float maxGrade = 0f;

            for (int i = 0; i < count; i++)
            {
                NetCourse course = courses[i];
                if (!math.all(math.isfinite(course.m_StartPosition.m_Position))
                    || !math.all(math.isfinite(course.m_EndPosition.m_Position)))
                {
                    return false;
                }

                if ((course.m_StartPosition.m_Flags & CoursePosFlags.IsFirst) != 0)
                {
                    if (first >= 0) return false;
                    first = i;
                }
                if ((course.m_EndPosition.m_Flags & CoursePosFlags.IsLast) != 0)
                {
                    if (last >= 0) return false;
                    last = i;
                }

                float2 delta = new(course.m_StartPosition.m_CourseDelta,
                    course.m_EndPosition.m_CourseDelta);
                if (!math.all(math.isfinite(delta)) || delta.x < 0f
                    || delta.y > 1f || delta.y < delta.x)
                {
                    return false;
                }

                Bezier4x3 cut = MathUtils.Cut(t: delta, curve: course.m_Curve);
                float segmentLength = MathUtils.Length(cut.xz);
                if (!math.isfinite(segmentLength) || segmentLength < 0f) return false;
                length += segmentLength;
                // Advisory: preview-course average grade, not final edge/node validation.
                float grade = 100f * math.abs(cut.d.y - cut.a.y) / math.max(0.1f, segmentLength);
                maxGrade = math.max(maxGrade, grade);
            }

            if (first < 0 || last < 0 || !math.isfinite(length) || length <= 0.001f)
            {
                return false;
            }

            // Walk the unique chain without sorting or modifying the native data.
            // A bitset bounds the work and detects cycles; native SortCourses
            // also joins courses by exact endpoint positions.
            int current = first;
            ulong visited = 0;
            for (int step = 0; step < count; step++)
            {
                ulong bit = 1UL << current;
                if ((visited & bit) != 0) return false;
                visited |= bit;

                if (current == last)
                {
                    if (step != count - 1) return false;
                    result = new RoadEngineerCourseMeasurements(length,
                        courses[first].m_StartPosition.m_Position,
                        courses[last].m_EndPosition.m_Position, maxGrade);
                    return math.isfinite(result.Distance) && math.isfinite(result.Relief)
                        && math.isfinite(result.Slope);
                }

                float3 end = courses[current].m_EndPosition.m_Position;
                int next = -1;
                for (int i = 0; i < count; i++)
                {
                    if ((visited & (1UL << i)) != 0
                        || !end.Equals(courses[i].m_StartPosition.m_Position))
                    {
                        continue;
                    }
                    if (next >= 0) return false;
                    next = i;
                }

                if (next < 0) return false;
                current = next;
            }

            return false;
        }
    }
}
