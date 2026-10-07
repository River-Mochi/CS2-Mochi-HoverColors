// <copyright file="OverlayThicknessSystems.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/Rendering/OverlayThicknessSystems.cs
// Purpose: Adjust overlay curves emitted by Area borders, guidelines, and building lot previews.
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
            out AreaGeometryData geometry)
        {
            geometry = default;
            if (toolSystem == null || areaToolSystem == null || prefabSystem == null
                || !ReferenceEquals(toolSystem.activeTool, areaToolSystem)
                || areaToolSystem.GetPrefab() is not AreaPrefab prefab
                || !prefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                || !entityManager.HasComponent<AreaGeometryData>(prefabEntity))
            {
                return false;
            }

            geometry = entityManager.GetComponentData<AreaGeometryData>(prefabEntity);
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
                out AreaGeometryData geometry)
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
        private float4 m_LotHoveredColor;
        private float4 m_LotOutlineColor;
        private float4 m_LotFillColor;
        private bool m_HideLot;

        public bool IsActive => m_Active;
        public bool NeedsHighPriorityColor => m_Scale < 0.999f;

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

        public void Apply(bool dashedOnly, float4 highPriorityColor = default, bool hasHighPriorityColor = false)
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
                HighPriorityColor = highPriorityColor,
                HasHighPriorityColor = hasHighPriorityColor,
            }.Schedule(dependencies);
            m_Overlay.AddBufferWriter(m_LastJob);
            m_Active = false;
        }

        public void CaptureLotPreview(float scale, float4 hoveredColor, float4 outlineColor, float4 fillColor,
            bool hideLot = false)
        {
            m_Active = false;
            if (!OverlayCurveAccess.TryGetLists(m_Overlay, out NativeList<OverlayRenderSystem.CurveData> projected,
                    out NativeList<OverlayRenderSystem.CurveData> absolute, out JobHandle dependencies))
            {
                return;
            }

            m_Scale = math.clamp(scale, 0f, 2f);
            m_LotHoveredColor = hoveredColor;
            m_LotOutlineColor = outlineColor;
            m_LotFillColor = fillColor;
            m_HideLot = hideLot;
            m_LastJob = new CaptureLengthsJob
            {
                Projected = projected,
                Absolute = absolute,
                Start = m_Start,
            }.Schedule(dependencies);
            m_Overlay.AddBufferWriter(m_LastJob);
            m_Active = true;
        }

        public void ApplyLotPreview()
        {
            if (!m_Active
                || !OverlayCurveAccess.TryGetLists(m_Overlay, out NativeList<OverlayRenderSystem.CurveData> projected,
                    out _, out JobHandle dependencies))
            {
                return;
            }

            m_LastJob = new ApplyLotPreviewJob
            {
                Projected = projected,
                Start = m_Start,
                Scale = m_Scale,
                HoveredColor = m_LotHoveredColor,
                OutlineColor = m_LotOutlineColor,
                FillColor = m_LotFillColor,
                HideLot = m_HideLot,
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

        private struct ApplyLotPreviewJob : IJob
        {
            public NativeList<OverlayRenderSystem.CurveData> Projected;
            [ReadOnly] public NativeArray<int2> Start;
            public float Scale;
            public float4 HoveredColor;
            public float4 OutlineColor;
            public float4 FillColor;
            public bool HideLot;

            public void Execute()
            {
                for (int i = math.clamp(Start[0].x, 0, Projected.Length); i < Projected.Length; i++)
                {
                    OverlayRenderSystem.CurveData curve = Projected[i];
                    // DrawLot is the only projected 0.2/0.4 outline in this producer. The
                    // hovered RGB test preserves warning/error/owner preview feedback.
                    if ((math.abs(curve.m_OutlineWidth - 0.2f) > 0.001f
                            && math.abs(curve.m_OutlineWidth - 0.4f) > 0.001f)
                        || math.abs(curve.m_OutlineColor.r - HoveredColor.x) > 0.001f
                        || math.abs(curve.m_OutlineColor.g - HoveredColor.y) > 0.001f
                        || math.abs(curve.m_OutlineColor.b - HoveredColor.z) > 0.001f)
                    {
                        continue;
                    }

                    if (HideLot)
                    {
                        // Default Tool's selected Temp lot forces 0.25 outline alpha and a
                        // 0.05 fill even when the Eyeball zeroes the hovered render color.
                        // Warning/error lots have different alpha and remain visible.
                        if (math.abs(curve.m_OutlineColor.a - 0.25f) > 0.001f
                            || math.abs(curve.m_FillColor.a - 0.05f) > 0.001f)
                        {
                            continue;
                        }

                        curve.m_OutlineColor.a = 0f;
                        curve.m_FillColor.a = 0f;
                        Projected[i] = curve;
                        continue;
                    }

                    // Keep a visible hairline at slider zero, matching the outline slider's
                    // appearance. Geometry, snap targets, and culling bounds stay untouched.
                    curve.m_OutlineWidth *= math.max(0.1f, Scale);
                    curve.m_OutlineColor = new UnityEngine.Color(
                        OutlineColor.x, OutlineColor.y, OutlineColor.z, OutlineColor.w);
                    curve.m_FillColor = new UnityEngine.Color(
                        FillColor.x, FillColor.y, FillColor.z, FillColor.w);
                    Projected[i] = curve;
                }
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
            public float4 HighPriorityColor;
            public bool HasHighPriorityColor;

            public void Execute()
            {
                int2 start = Start[0];
                ScaleRange(Projected, start.x, projected: true);
                ScaleRange(Absolute, start.y, projected: false);
            }

            private void ScaleRange(NativeList<OverlayRenderSystem.CurveData> curves, int start, bool projected)
            {
                for (int i = math.clamp(start, 0, curves.Length); i < curves.Length; i++)
                {
                    OverlayRenderSystem.CurveData curve = curves[i];
                    if (DashedOnly)
                    {
                        if (Scale < 0.999f && (curve.m_DashLengths.x > 0f
                            || (!projected && HasHighPriorityColor && !IsCircle(curve)
                                && MatchesHighPriorityColor(curve))))
                        {
                            // Solid high-priority angle guides share the dashed swatch. Exclude
                            // projected curves so road-width previews retain their real width.
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

                    bool authoredWidth = math.abs(curve.m_Size.x - AreaSnapDistance * 0.3f) <= 0.001f
                        || math.abs(curve.m_Size.x - AreaSnapDistance * 0.2f) <= 0.001f;
                    // AreaBorderRenderSystem doubles the Lot snap distance for infoview borders.
                    bool infoviewLotWidth = !ScaleAreaCircles
                        && (math.abs(curve.m_Size.x - AreaSnapDistance * 0.6f) <= 0.001f
                            || math.abs(curve.m_Size.x - AreaSnapDistance * 0.4f) <= 0.001f);
                    if (!authoredWidth && !infoviewLotWidth)
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

            private bool MatchesHighPriorityColor(OverlayRenderSystem.CurveData curve)
            {
                return math.abs(curve.m_FillColor.r - HighPriorityColor.x) < 0.001f
                    && math.abs(curve.m_FillColor.g - HighPriorityColor.y) < 0.001f
                    && math.abs(curve.m_FillColor.b - HighPriorityColor.z) < 0.001f
                    && math.abs(curve.m_FillColor.a - HighPriorityColor.w) < 0.001f;
            }
        }
    }

    public partial class AreaBorderWidthCaptureSystem : GameSystemBase
    {
        private OverlayCurveSpan? m_Span;
        private ToolSystem? m_ToolSystem;
        private AreaToolSystem? m_AreaToolSystem;
        private PrefabSystem? m_PrefabSystem;
        private EntityQuery m_AreaBorderQuery;
        private EntityQuery m_AreaBorderInfoviewQuery;

        internal OverlayCurveSpan? Span => m_Span;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Span = new OverlayCurveSpan(World.GetOrCreateSystemManaged<OverlayRenderSystem>());
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_AreaToolSystem = World.GetOrCreateSystemManaged<AreaToolSystem>();
            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            // Match the two queries used by vanilla AreaBorderRenderSystem. When its chosen
            // query is empty, it emits no curves and there is nothing for HC to scale.
            m_AreaBorderQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[] { ComponentType.ReadOnly<Game.Areas.Area>() },
                Any = new[]
                {
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Error>(),
                    ComponentType.ReadOnly<Warning>(),
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Hidden>(),
                    ComponentType.ReadOnly<Game.Common.Deleted>(),
                },
            });
            m_AreaBorderInfoviewQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[] { ComponentType.ReadOnly<Game.Areas.Area>() },
                Any = new[]
                {
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Error>(),
                    ComponentType.ReadOnly<Warning>(),
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Hidden>(),
                    ComponentType.ReadOnly<Game.Common.Deleted>(),
                },
            }, new EntityQueryDesc
            {
                All = new[]
                {
                    ComponentType.ReadOnly<Lot>(),
                    ComponentType.ReadOnly<Game.Common.Owner>(),
                },
                None = new[]
                {
                    ComponentType.ReadOnly<Hidden>(),
                    ComponentType.ReadOnly<Game.Common.Deleted>(),
                    ComponentType.ReadOnly<Batch>(),
                },
            });
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

            EntityQuery sourceQuery = m_ToolSystem?.activeInfoview != null
                ? m_AreaBorderInfoviewQuery
                : m_AreaBorderQuery;
            if (sourceQuery.IsEmptyIgnoreFilter)
            {
                m_Span?.Capture(1f);
                return;
            }

            bool hasActiveArea = OverlayCurveAccess.TryGetActiveAreaGeometry(
                EntityManager, m_ToolSystem, m_AreaToolSystem, m_PrefabSystem,
                out AreaGeometryData geometry);
            if (hasActiveArea && geometry.m_Type == AreaType.Surface && surfaceScale < 0.999f)
            {
                m_Span?.Capture(surfaceScale, geometry.m_SnapDistance);
                return;
            }

            if (extractorScale < 0.999f)
            {
                // ObjectToolSystem draws the Lot hover border before switching to AreaToolSystem
                // for node editing. Match the game's Lot width even when no Area prefab is active.
                float lotSnapDistance = hasActiveArea && geometry.m_Type == AreaType.Lot
                    ? geometry.m_SnapDistance
                    : AreaUtils.GetMinNodeDistance(AreaType.Lot);
                m_Span?.Capture(extractorScale, lotSnapDistance, scaleAreaCircles: false);
                return;
            }

            m_Span?.Capture(1f);
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
        private EntityQuery m_GuidelineSettingsQuery;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Capture = World.GetOrCreateSystemManaged<DashedGuidelineWidthCaptureSystem>();
            m_GuidelineSettingsQuery = GetEntityQuery(ComponentType.ReadOnly<GuideLineSettingsData>());
        }

        protected override void OnUpdate()
        {
            OverlayCurveSpan? span = m_Capture?.Span;
            if (span?.IsActive != true)
            {
                return;
            }

            // Surface control-point circles can need scaling even with Dashes at 1.0.
            // Only dashed/angle-guide scaling needs the High priority color lookup.
            if (!span.NeedsHighPriorityColor || m_GuidelineSettingsQuery.IsEmptyIgnoreFilter)
            {
                span.Apply(dashedOnly: true);
                return;
            }

            GuideLineSettingsData settings = EntityManager.GetComponentData<GuideLineSettingsData>(
                m_GuidelineSettingsQuery.GetSingletonEntity());
            UnityEngine.Color high = settings.m_HighPriorityColor.linear;
            span.Apply(dashedOnly: true,
                highPriorityColor: new float4(high.r, high.g, high.b, high.a),
                hasHighPriorityColor: true);
        }
    }

    public partial class BuildingLotPreviewCaptureSystem : GameSystemBase
    {
        private OverlayCurveSpan? m_Span;
        private ToolSystem? m_ToolSystem;
        private EntityQuery m_TempQuery;
        private EntityQuery m_RenderingSettingsQuery;

        internal OverlayCurveSpan? Span => m_Span;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Span = new OverlayCurveSpan(World.GetOrCreateSystemManaged<OverlayRenderSystem>());
            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();
            m_TempQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[] { ComponentType.ReadOnly<Temp>() },
                Any = new[]
                {
                    ComponentType.ReadOnly<Game.Buildings.Building>(),
                    ComponentType.ReadOnly<Game.Buildings.Extension>(),
                    ComponentType.ReadOnly<Game.Objects.AssetStamp>(),
                    ComponentType.ReadOnly<Game.Net.Taxiway>(),
                    ComponentType.ReadOnly<Game.Objects.Tree>(),
                },
            });
            m_RenderingSettingsQuery = GetEntityQuery(ComponentType.ReadOnly<RenderingSettingsData>());
        }

        protected override void OnUpdate()
        {
            HoverColorsSettings? settings = Mod.Settings;
            bool placement = m_ToolSystem?.activeTool is ObjectToolSystem;
            bool hidePlacedLot = settings?.HoverHighlightsSuppressed == true
                && m_ToolSystem?.activeTool is DefaultToolSystem;
            if (settings == null || (!placement && !hidePlacedLot)
                || m_TempQuery.IsEmptyIgnoreFilter || m_RenderingSettingsQuery.IsEmptyIgnoreFilter)
            {
                return;
            }

            UnityEngine.Color hovered = EntityManager.GetComponentData<RenderingSettingsData>(
                m_RenderingSettingsQuery.GetSingletonEntity()).m_HoveredColor.linear;
            UnityEngine.Color outline = new UnityEngine.Color(
                settings.OutlineR, settings.OutlineG, settings.OutlineB, settings.OutlineA).linear;
            UnityEngine.Color fill = new UnityEngine.Color(
                settings.FillR, settings.FillG, settings.FillB, settings.FillA).linear;
            m_Span?.CaptureLotPreview(settings.OutlineThicknessScale,
                new float4(hovered.r, hovered.g, hovered.b, hovered.a),
                new float4(outline.r, outline.g, outline.b, outline.a),
                new float4(fill.r, fill.g, fill.b, fill.a), hidePlacedLot);
        }

        protected override void OnDestroy()
        {
            m_Span?.Dispose();
            base.OnDestroy();
        }
    }

    public partial class BuildingLotPreviewApplySystem : GameSystemBase
    {
        private BuildingLotPreviewCaptureSystem? m_Capture;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Capture = World.GetOrCreateSystemManaged<BuildingLotPreviewCaptureSystem>();
        }

        protected override void OnUpdate()
        {
            m_Capture?.Span?.ApplyLotPreview();
        }
    }
}
