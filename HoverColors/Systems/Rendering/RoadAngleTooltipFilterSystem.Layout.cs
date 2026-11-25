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
    using Game.Settings;
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
        private bool m_LayoutScaleLogged;
        private bool m_LayoutCrowdedLogged;

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
            if (m_MiddleAngleGuideLines == null || tooltipUI.groups.Count < 1
                || (mode != NetToolSystem.Mode.Straight
                    && mode != NetToolSystem.Mode.SimpleCurve
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
            if (!m_LayoutScaleLogged)
            {
                m_LayoutScaleLogged = true;
                InterfaceSettings? uiSettings = SharedSettings.instance?.userInterface;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter layout: screen={Screen.width}x{Screen.height}, textScale={uiSettings?.textScale ?? 1f:F2}, interfaceScaling={uiSettings?.interfaceScaling}, estimateScale={scale:F2}");
            }
            if (mode == NetToolSystem.Mode.Straight)
            {
                NativeList<ControlPoint> points = netTool.GetControlPoints(out JobHandle pointDependencies);
                pointDependencies.Complete();
                Camera? camera = Camera.main;
                if (points.Length == 2 && camera != null)
                {
                    Vector3 start = camera.WorldToScreenPoint(points[0].m_Position);
                    m_LayoutStart = new float2(start.x, Screen.height - start.y);
                    m_HasLayoutStart = start.z > 0f && math.all(math.isfinite(m_LayoutStart));
                }
            }
            m_OccupiedMeasurements.Clear();
            // TooltipUISystem sets mouseGroup.position AFTER this update phase.
            // Reserve its predicted on-screen rectangle, not last frame's position.
            if (TryGetCursorBounds(tooltipUI, scale, out Rect cursorBounds))
            {
                m_OccupiedMeasurements.Add(cursorBounds);
            }

            // Keep the verified middle angle fixed on its selected side of the handle.
            // Starting alignment angles have priority over lengths: the lengths
            // can slide locally without losing their associated guide segment.
            if (m_AnchoredMiddleAngle != null)
            {
                m_OccupiedMeasurements.Add(MeasurementRect(m_AnchoredMiddleAngle.position, true, scale));
                m_OccupiedMeasurements.Add(m_LayoutJointBounds);
            }

            int moved = 0;
            int crowded = 0;
            for (int pass = 0; pass < 2; pass++)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    GuideLinesSystem.TooltipInfo entry = entries[i];
                    bool angle = entry.m_Type == GuideLinesSystem.TooltipType.Angle;
                    if ((pass == 0 && !angle) || (pass == 1 && angle)
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
                    float margin = 90f * scale;
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
                        if (distance > 85f * scale)
                        {
                            // Aim nearer the arc, not at the start point itself.
                            float shift = math.min(distance * 0.30f, 100f * scale);
                            preferred -= radial / distance * shift;
                        }
                    }

                    float2 position = FindClearPosition(original, preferred, angle, scale, out bool clear);
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

            if (moved > 0 && !m_LayoutLogged)
            {
                m_LayoutLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: adjusted {moved} measurement label positions; screen={Screen.width}x{Screen.height}, scale={scale:F2}");
            }
            if (crowded > 0 && !m_LayoutCrowdedLogged)
            {
                m_LayoutCrowdedLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Road Clutter Phase 2: {crowded} crowded labels after bounded search ({mode}, screen={Screen.width}x{Screen.height})");
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
            float width = (angle ? 132f : 160f) * scale;
            float height = 48f * scale;
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

        private float2 FindClearPosition(float2 original, float2 preferred, bool angle, float scale, out bool clear)
        {
            // First attempt the desired position. If crowded, expand in rings
            // around it. Clamping also keeps partially clipped edge labels visible.
            preferred = ClampToScreen(preferred, angle, scale);
            float2 best = preferred;
            float bestOverlap = GetOverlap(MeasurementRect(best, angle, scale));
            float bestCost = 0f;
            if (bestOverlap == 0f)
            {
                clear = true;
                return best;
            }

            float step = 18f * scale;
            float maxDistance = 240f * scale;
            for (int ring = 1; ring <= 12; ring++)
            {
                for (int y = -ring; y <= ring; y++)
                {
                    for (int x = -ring; x <= ring; x++)
                    {
                        if (math.abs(x) != ring && math.abs(y) != ring) continue;
                        float2 candidate = ClampToScreen(preferred + new float2(x, y) * step, angle, scale);
                        if (math.distancesq(candidate, original) > maxDistance * maxDistance) continue;

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
            // Never drop a measurement; least-overlapping fallback is preferable
            // to the old behavior of restoring a fully obscured vanilla label.
            return best;
        }
    }
}
