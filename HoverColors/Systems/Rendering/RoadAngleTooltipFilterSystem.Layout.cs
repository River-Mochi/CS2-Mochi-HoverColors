// <copyright file="RoadAngleTooltipFilterSystem.Layout.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// Experimental, bounded screen-space layout of existing road measurements.
// Never change geometry, tooltip values, alignment, units or visibility.

namespace HoverColors.Systems
{
    using System.Collections.Generic;
    using CS2Shared.RiverMochi;
    using Game.Input;
    using Game.Rendering;
    using Game.Tools;
    using Game.UI.Tooltip;
    using Unity.Collections;
    using Unity.Jobs;
    using Unity.Mathematics;
    using UnityEngine;

    public partial class RoadAngleTooltipFilterSystem
    {
        private readonly List<Rect> m_OccupiedMeasurements = new(12);
        private readonly List<string> m_GuidelinePaths = new(8);
        private TooltipGroup? m_AnchoredMiddleAngle;
        private float2 m_LayoutStart;
        private Rect m_LayoutJointBounds;
        private bool m_HasLayoutStart;
        private bool m_LayoutLogged;

        private static float GetLayoutScale()
        {
            return Mathf.Clamp(Screen.height / 1080f, 0.65f, 3f);
        }

        private void ArrangeCurveMeasurements(NetToolSystem netTool, TooltipUISystem tooltipUI)
        {
            NetToolSystem.Mode mode = netTool.actualMode;
            if (m_MiddleAngleGuideLines == null || tooltipUI.groups.Count < 1
                || (mode != NetToolSystem.Mode.SimpleCurve
                    && mode != NetToolSystem.Mode.ComplexCurve
                    && mode != NetToolSystem.Mode.Continuous))
            {
                return;
            }

            NativeList<GuideLinesSystem.TooltipInfo> entries =
                m_MiddleAngleGuideLines.GetTooltips(out JobHandle dependencies);
            dependencies.Complete();
            if (entries.Length == 0 || entries.Length > 32)
            {
                return;
            }

            while (m_GuidelinePaths.Count < entries.Length)
            {
                m_GuidelinePaths.Add($"guideLineTooltip{m_GuidelinePaths.Count}");
            }

            float scale = GetLayoutScale();
            m_OccupiedMeasurements.Clear();
            // TooltipUISystem sets mouseGroup.position AFTER this update phase.
            // Reserve its predicted on-screen rectangle, not last frame's position.
            if (TryGetCursorBounds(tooltipUI, scale, out Rect cursorBounds))
            {
                m_OccupiedMeasurements.Add(cursorBounds);
            }

            // Keep the verified middle angle fixed above its handle. Lengths have
            // next priority; starting alignment angles move only when useful.
            if (m_AnchoredMiddleAngle != null)
            {
                m_OccupiedMeasurements.Add(MeasurementRect(m_AnchoredMiddleAngle.position, true, scale));
                m_OccupiedMeasurements.Add(m_LayoutJointBounds);
            }

            int moved = 0;
            for (int pass = 0; pass < 2; pass++)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    GuideLinesSystem.TooltipInfo entry = entries[i];
                    bool angle = entry.m_Type == GuideLinesSystem.TooltipType.Angle;
                    if ((pass == 0 && angle) || (pass == 1 && !angle)
                        || (!angle && entry.m_Type != GuideLinesSystem.TooltipType.Length))
                    {
                        continue;
                    }

                    TooltipGroup? group = FindMeasurement(tooltipUI, i, angle);
                    if (group == null || ReferenceEquals(group, m_AnchoredMiddleAngle))
                    {
                        continue;
                    }

                    float2 original = group.position;
                    if (!math.all(math.isfinite(original)))
                    {
                        continue;
                    }

                    // Never pull a genuinely off-screen guide into the viewport.
                    float margin = 50f * scale;
                    if (original.x < -margin || original.x > Screen.width + margin
                        || original.y < -margin || original.y > Screen.height + margin)
                    {
                        continue;
                    }

                    float2 preferred = original;
                    if (angle && m_HasLayoutStart)
                    {
                        // In a verified three-point curve the other angles are
                        // start alignment references. Vanilla places text at 1.5x
                        // the arc radius; move distant labels slightly toward it.
                        float2 radial = original - m_LayoutStart;
                        float distance = math.length(radial);
                        if (distance > 120f * scale)
                        {
                            preferred -= radial / distance * math.min(distance * 0.2f, 40f * scale);
                        }
                    }

                    float2 position = FindClearPosition(original, preferred, angle, scale);
                    m_OccupiedMeasurements.Add(MeasurementRect(position, angle, scale));
                    if (!position.Equals(original))
                    {
                        group.position = position;
                        group.SetChildrenChanged();
                        moved++;
                    }
                }
            }

            if (moved > 0 && !m_LayoutLogged)
            {
                m_LayoutLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: adjusted {moved} measurement label positions");
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

        // Approximate native tooltip bounds; actual COHTML text sizes are not
        // exposed here. Extra padding provides clearance for unit formats.
        private static Rect MeasurementRect(float2 center, bool angle, float scale)
        {
            float width = (angle ? 110f : 142f) * scale;
            float height = 40f * scale;
            return new Rect(center.x - width * 0.5f, center.y - height * 0.5f, width, height);
        }

        private float2 FindClearPosition(float2 original, float2 preferred, bool angle, float scale)
        {
            // A bounded local search keeps labels associated with their own
            // guideline. Never hide a label just because no free slot exists.
            float2 best = original;
            float bestCost = float.PositiveInfinity;
            for (int y = -3; y <= 3; y++)
            {
                for (int x = -3; x <= 3; x++)
                {
                    float2 candidate = preferred + new float2(x, y) * (20f * scale);
                    Rect bounds = MeasurementRect(candidate, angle, scale);
                    if (bounds.xMin < 4f || bounds.yMin < 4f
                        || bounds.xMax > Screen.width - 4f || bounds.yMax > Screen.height - 4f
                        || math.distancesq(candidate, original) > 110f * scale * (110f * scale))
                    {
                        continue;
                    }

                    bool overlaps = false;
                    for (int i = 0; i < m_OccupiedMeasurements.Count; i++)
                    {
                        if (bounds.Overlaps(m_OccupiedMeasurements[i]))
                        {
                            overlaps = true;
                            break;
                        }
                    }

                    if (overlaps)
                    {
                        continue;
                    }

                    float cost = math.distancesq(candidate, preferred);
                    if (cost < bestCost)
                    {
                        best = candidate;
                        bestCost = cost;
                        if (cost == 0f)
                        {
                            return best;
                        }
                    }
                }
            }

            return best;
        }
    }
}
