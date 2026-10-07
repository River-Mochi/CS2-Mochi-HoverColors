// <copyright file="OverlayThicknessSystems.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/Rendering/OverlayThicknessSystems.cs
// Purpose: Scale only the overlay curves emitted by the Area border and guideline systems.
// Capturing the list lengths on either side of each vanilla producer keeps unrelated overlays and
// the actual road-preview width untouched. The jobs preserve the game's writer dependency chain.

namespace HoverColors.Systems
{
    using System;
    using System.Reflection;
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Areas;
    using Game.Prefabs;
    using Game.Rendering;
    using Game.Tools;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Jobs;
    using Unity.Mathematics;

    internal static class OverlayCurveAccess
    {
        private static readonly FieldInfo? s_ProjectedField = typeof(OverlayRenderSystem).GetField(
            "m_ProjectedData", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo? s_AbsoluteField = typeof(OverlayRenderSystem).GetField(
            "m_AbsoluteData", BindingFlags.Instance | BindingFlags.NonPublic);

        public static bool TryGetLists(
            OverlayRenderSystem overlay,
            out NativeList<OverlayRenderSystem.CurveData> projected,
            out NativeList<OverlayRenderSystem.CurveData> absolute,
            out JobHandle dependencies)
        {
            // GetBuffer creates the lists and returns every producer's registered writer handle.
            overlay.GetBuffer(out dependencies);
            if (s_ProjectedField?.GetValue(overlay) is NativeList<OverlayRenderSystem.CurveData> projectedList
                && s_AbsoluteField?.GetValue(overlay) is NativeList<OverlayRenderSystem.CurveData> absoluteList)
            {
                projected = projectedList;
                absolute = absoluteList;
                return true;
            }

            LogUtils.WarnOnce(
                "overlay-curve-lists-missing",
                () => $"{Mod.ModTag} Overlay thickness is unavailable: curve buffers were not found.");
            projected = default;
            absolute = default;
            return false;
        }

        public static bool TryGetActiveAreaGeometry(
            EntityManager entityManager,
            ToolSystem? toolSystem,
            AreaToolSystem? areaToolSystem,
            PrefabSystem? prefabSystem,
            out AreaGeometryData geometry,
            out bool specializedIndustryLot)
        {
            geometry = default;
            specializedIndustryLot = false;
            if (toolSystem == null || areaToolSystem == null || prefabSystem == null
                || !ReferenceEquals(toolSystem.activeTool, areaToolSystem)
                || areaToolSystem.GetPrefab() is not AreaPrefab prefab
                || !prefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                || !entityManager.HasComponent<AreaGeometryData>(prefabEntity))
            {
                return false;
            }

            geometry = entityManager.GetComponentData<AreaGeometryData>(prefabEntity);
            specializedIndustryLot = geometry.m_Type == AreaType.Lot
                && (entityManager.HasComponent<ExtractorAreaData>(prefabEntity)
                    || entityManager.HasComponent<StorageAreaData>(prefabEntity));
            return true;
        }

        public static bool TryGetSurfaceSnapDistance(
            EntityManager entityManager,
            ToolSystem? toolSystem,
            AreaToolSystem? areaToolSystem,
            PrefabSystem? prefabSystem,
            out float snapDistance)
        {
            snapDistance = 0f;
            if (!TryGetActiveAreaGeometry(entityManager, toolSystem, areaToolSystem, prefabSystem,
                out AreaGeometryData geometry, out _)
                || geometry.m_Type != AreaType.Surface)
            {
                return false;
            }

            snapDistance = geometry.m_SnapDistance;
            return true;
        }
    }

    internal sealed class OverlayCurveSpan
    {
        private readonly OverlayRenderSystem m_Overlay;
        private readonly NativeArray<int2> m_Start;
        private JobHandle m_LastJob;
        private bool m_Active;
        private float m_Scale;
        private float m_AreaSnapDistance;
        private float m_SurfaceCircleScale;
        private bool m_ScaleAreaCircles;

        public OverlayCurveSpan(OverlayRenderSystem overlay)
        {
            m_Overlay = overlay;
            m_Start = new NativeArray<int2>(1, Allocator.Persistent);
        }

        public void Capture(float scale, float areaSnapDistance = 0f, float surfaceCircleScale = 1f,
            bool scaleAreaCircles = true)
        {
            m_Active = false;
            scale = math.clamp(scale, 0.1f, 1f);
            surfaceCircleScale = math.clamp(surfaceCircleScale, 0.1f, 1f);
            if ((scale >= 0.999f && surfaceCircleScale >= 0.999f)
                || !OverlayCurveAccess.TryGetLists(m_Overlay, out NativeList<OverlayRenderSystem.CurveData> projected,
                    out NativeList<OverlayRenderSystem.CurveData> absolute, out JobHandle dependencies))
            {
                return;
            }

            m_Scale = scale;
            m_AreaSnapDistance = areaSnapDistance;
            m_SurfaceCircleScale = surfaceCircleScale;
            m_ScaleAreaCircles = scaleAreaCircles;
            m_LastJob = new CaptureLengthsJob
            {
                Projected = projected,
                Absolute = absolute,
                Start = m_Start,
            }.Schedule(dependencies);
            m_Overlay.AddBufferWriter(m_LastJob);
            m_Active = true;
        }

        public void Apply(bool dashedOnly)
        {
            if (!m_Active
                || !OverlayCurveAccess.TryGetLists(m_Overlay, out NativeList<OverlayRenderSystem.CurveData> projected,
                    out NativeList<OverlayRenderSystem.CurveData> absolute, out JobHandle dependencies))
            {
                return;
            }

            m_LastJob = new ScaleCurvesJob
            {
                Projected = projected,
                Absolute = absolute,
                Start = m_Start,
                Scale = m_Scale,
                DashedOnly = dashedOnly,
                AreaSnapDistance = m_AreaSnapDistance,
                SurfaceCircleScale = m_SurfaceCircleScale,
                ScaleAreaCircles = m_ScaleAreaCircles,
            }.Schedule(dependencies);
            m_Overlay.AddBufferWriter(m_LastJob);
            m_Active = false;
        }

        public void Dispose()
        {
            m_LastJob.Complete();
            if (m_Start.IsCreated)
            {
                m_Start.Dispose();
            }
        }

        private struct CaptureLengthsJob : IJob
        {
            [ReadOnly] public NativeList<OverlayRenderSystem.CurveData> Projected;
            [ReadOnly] public NativeList<OverlayRenderSystem.CurveData> Absolute;
            public NativeArray<int2> Start;

            public void Execute()
            {
                Start[0] = new int2(Projected.Length, Absolute.Length);
            }
        }

        private struct ScaleCurvesJob : IJob
        {
            public NativeList<OverlayRenderSystem.CurveData> Projected;
            public NativeList<OverlayRenderSystem.CurveData> Absolute;
            [ReadOnly] public NativeArray<int2> Start;
            public float Scale;
            public bool DashedOnly;
            public float AreaSnapDistance;
            public float SurfaceCircleScale;
            public bool ScaleAreaCircles;

            public void Execute()
            {
                int2 start = Start[0];
                ScaleRange(Projected, start.x);
                ScaleRange(Absolute, start.y);
            }

            private void ScaleRange(NativeList<OverlayRenderSystem.CurveData> curves, int start)
            {
                for (int i = math.clamp(start, 0, curves.Length); i < curves.Length; i++)
                {
                    OverlayRenderSystem.CurveData curve = curves[i];
                    if (DashedOnly)
                    {
                        if (curve.m_DashLengths.x > 0f && Scale < 0.999f)
                        {
                            curve.m_Size.x *= Scale;
                        }
                        else if (SurfaceCircleScale < 0.999f && IsCircle(curve)
                            && math.abs(curve.m_Size.x - AreaSnapDistance * 0.5f) <= 0.001f)
                        {
                            // The Area tool's active control-point circle comes from
                            // GuideLinesSystem, separate from AreaBorderRenderSystem's joint dots.
                            curve.m_Size *= SurfaceCircleScale;
                            curve.m_DashLengths.y *= SurfaceCircleScale;
                        }
                        else
                        {
                            continue;
                        }

                        curves[i] = curve;
                        continue;
                    }

                    if (!ScaleAreaCircles && IsCircle(curve))
                    {
                        // Keep Lot joint dots at vanilla size so their handles remain easy to see.
                        continue;
                    }

                    if (math.abs(curve.m_Size.x - AreaSnapDistance * 0.3f) > 0.001f
                        && math.abs(curve.m_Size.x - AreaSnapDistance * 0.2f) > 0.001f)
                    {
                        continue;
                    }

                    curve.m_Size.x *= Scale;
                    if (IsCircle(curve))
                    {
                        // AreaBorderRenderSystem's joint dots are filled circles. Their two size
                        // axes and dash extent all use the authored diameter.
                        curve.m_Size.y *= Scale;
                        curve.m_DashLengths.y *= Scale;
                    }

                    // The original quad/matrix and bounds remain a superset of the thinner shape.
                    // Scaling only m_Size avoids changing snap geometry or overlay culling.
                    curves[i] = curve;
                }
            }

            private static bool IsCircle(OverlayRenderSystem.CurveData curve)
            {
                return curve.m_Curve.m00 == curve.m_Curve.m03
                    && curve.m_Curve.m10 == curve.m_Curve.m13
                    && curve.m_Curve.m20 == curve.m_Curve.m23;
            }
        }
    }

    public partial class AreaBorderWidthCaptureSystem : GameSystemBase
    {
        private OverlayCurveSpan? m_Span;
        private ToolSystem? m_ToolSystem;
        private AreaToolSystem? m_AreaToolSystem;
        private PrefabSystem? m_PrefabSystem;

        internal OverlayCurveSpan? Span => m_Span;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Span = new OverlayCurveSpan(World.GetOrCreateSystemManaged<OverlayRenderSystem>());
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_AreaToolSystem = World.GetOrCreateSystemManaged<AreaToolSystem>();
            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
        }

        protected override void OnUpdate()
        {
            float surfaceScale = Mod.Settings?.SurfaceBorderThicknessScale ?? 1f;
            float extractorScale = Mod.Settings?.ExtractorBorderThicknessScale ?? 1f;
            if (surfaceScale >= 0.999f && extractorScale >= 0.999f)
            {
                m_Span?.Capture(1f);
                return;
            }

            if (!OverlayCurveAccess.TryGetActiveAreaGeometry(
                EntityManager, m_ToolSystem, m_AreaToolSystem, m_PrefabSystem,
                out AreaGeometryData geometry, out bool specializedIndustryLot))
            {
                m_Span?.Capture(1f);
                return;
            }

            float scale = geometry.m_Type switch
            {
                AreaType.Surface => surfaceScale,
                AreaType.Lot when specializedIndustryLot => extractorScale,
                _ => 1f,
            };
            m_Span?.Capture(scale, geometry.m_SnapDistance, scaleAreaCircles: geometry.m_Type == AreaType.Surface);
        }

        protected override void OnDestroy()
        {
            m_Span?.Dispose();
            base.OnDestroy();
        }
    }

    public partial class AreaBorderWidthApplySystem : GameSystemBase
    {
        private AreaBorderWidthCaptureSystem? m_Capture;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Capture = World.GetOrCreateSystemManaged<AreaBorderWidthCaptureSystem>();
        }

        protected override void OnUpdate()
        {
            m_Capture?.Span?.Apply(dashedOnly: false);
        }
    }

    public partial class DashedGuidelineWidthCaptureSystem : GameSystemBase
    {
        private OverlayCurveSpan? m_Span;
        private ToolSystem? m_ToolSystem;
        private AreaToolSystem? m_AreaToolSystem;
        private PrefabSystem? m_PrefabSystem;

        internal OverlayCurveSpan? Span => m_Span;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Span = new OverlayCurveSpan(World.GetOrCreateSystemManaged<OverlayRenderSystem>());
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_AreaToolSystem = World.GetOrCreateSystemManaged<AreaToolSystem>();
            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
        }

        protected override void OnUpdate()
        {
            float dashedScale = Mod.Settings?.GuidelineDashedThicknessScale ?? 1f;
            float surfaceScale = Mod.Settings?.SurfaceBorderThicknessScale ?? 1f;
            if (surfaceScale >= 0.999f
                || !OverlayCurveAccess.TryGetSurfaceSnapDistance(
                    EntityManager, m_ToolSystem, m_AreaToolSystem, m_PrefabSystem, out float snapDistance))
            {
                m_Span?.Capture(dashedScale);
                return;
            }

            m_Span?.Capture(dashedScale, snapDistance, surfaceScale);
        }

        protected override void OnDestroy()
        {
            m_Span?.Dispose();
            base.OnDestroy();
        }
    }

    public partial class DashedGuidelineWidthApplySystem : GameSystemBase
    {
        private DashedGuidelineWidthCaptureSystem? m_Capture;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Capture = World.GetOrCreateSystemManaged<DashedGuidelineWidthCaptureSystem>();
        }

        protected override void OnUpdate()
        {
            m_Capture?.Span?.Apply(dashedOnly: true);
        }
    }
}
