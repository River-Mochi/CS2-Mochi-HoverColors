// <copyright file="DistrictColorSystem.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/DistrictColorSystem.cs
// Purpose: Applies player's District overlay color/opacity to the vanilla District area prefab.
//
// Prefab data here:
//   Districts are rendered through Game.Prefabs.AreaColorData on the default District prefab entity.
//   AreaBufferSystem reads those prefab fill + edge colors while rebuilding the overlay
//   buffer, so we write prefab-side colors and then mark live District area entities Updated.
//   Important: do not query every prefab with DistrictData. Custom assets can define their
//   own district-like area prefabs, so we now target the exact vanilla District Area prefab ID.

namespace HoverColors.Systems
{
    using Colossal.Serialization.Entities;
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Common;
    using Game.Areas;
    using Game.Prefabs;
    using Game.Rendering;

    using Unity.Entities;
    using UnityEngine;
    using AreaComponent = Game.Areas.Area;
    using AreaNode = Game.Areas.Node;
    using AreaTriangle = Game.Areas.Triangle;
    using DistrictComponent = Game.Areas.District;
    using PrefabAreaColorData = Game.Prefabs.AreaColorData;

    public partial class DistrictColorSystem : GameSystemBase
    {
        private static readonly PrefabID s_VanillaDistrictPrefabId =
            new(nameof(DistrictPrefab), "District Area");

        private static readonly Color s_FallbackDistrictFill = new(128f / 255f, 128f / 255f, 128f / 255f, 64f / 255f);
        private static readonly Color s_FallbackDistrictEdge = new(128f / 255f, 128f / 255f, 128f / 255f, 128f / 255f);
        private static readonly Color s_FallbackDistrictSelectionFill = new(128f / 255f, 128f / 255f, 128f / 255f, 128f / 255f);
        private static readonly Color s_FallbackDistrictSelectionEdge = new(128f / 255f, 128f / 255f, 128f / 255f, 1f);

        public static Color CapturedDistrictFillColor { get; private set; } = s_FallbackDistrictFill;
        public static Color CapturedDistrictEdgeColor { get; private set; } = s_FallbackDistrictEdge;
        private static Color CapturedDistrictSelectionFillColor { get; set; } = s_FallbackDistrictSelectionFill;
        private static Color CapturedDistrictSelectionEdgeColor { get; set; } = s_FallbackDistrictSelectionEdge;
        public static bool HasCapturedDistrictDefaults { get; private set; }

        private PrefabSystem? m_PrefabSystem;
        private AreaBufferSystem? m_AreaBufferSystem;
        private EntityQuery m_DistrictAreaQuery;
        private static readonly int s_AreaParameters = Shader.PropertyToID("colossal_AreaParameters");
        private Material? m_DistrictMaterial;
        private Vector4 m_VanillaAreaParameters;
        private float m_LastBorderScale = 1f;
        private bool m_BorderParametersLogged;

        private bool m_CaptureLogged;
        private bool m_SeededSettingsFromCapture;
        private bool m_Applied;
        private bool m_LastEnabled;
        private float m_LastR;
        private float m_LastG;
        private float m_LastB;
        private float m_LastA;

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_AreaBufferSystem = World.GetOrCreateSystemManaged<AreaBufferSystem>();
            m_DistrictAreaQuery = SystemAPI.QueryBuilder()
                .WithAll<AreaComponent, DistrictComponent, AreaNode, AreaTriangle>()
                .WithNone<Deleted>()
                .Build();

            Enabled = true;
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);
            m_Applied = false;
            RestoreDistrictBorderWidth();
        }

        protected override void OnDestroy()
        {
            RestoreDistrictBorderWidth();
            base.OnDestroy();
        }

        protected override void OnUpdate()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null)
            {
                return;
            }

            ApplyDistrictBorderWidth(settings.DistrictBorderThicknessScale);
            TryCaptureDefaults();
            SeedSettingsFromCapture(settings);

            bool enabled = settings.DistrictColorEnabled;
            float r = enabled ? Mathf.Clamp01(settings.DistrictR) : CapturedDistrictFillColor.r;
            float g = enabled ? Mathf.Clamp01(settings.DistrictG) : CapturedDistrictFillColor.g;
            float b = enabled ? Mathf.Clamp01(settings.DistrictB) : CapturedDistrictFillColor.b;
            float a = enabled ? Mathf.Clamp01(settings.DistrictA) : CapturedDistrictFillColor.a;

            if (m_Applied
                && enabled == m_LastEnabled
                && ApproximatelyEqual(r, m_LastR)
                && ApproximatelyEqual(g, m_LastG)
                && ApproximatelyEqual(b, m_LastB)
                && ApproximatelyEqual(a, m_LastA))
            {
                return;
            }

            if (!enabled)
            {
                ApplyDistrictColors(
                    CapturedDistrictFillColor,
                    CapturedDistrictEdgeColor,
                    CapturedDistrictSelectionFillColor,
                    CapturedDistrictSelectionEdgeColor);
                MarkDistrictAreasUpdated();

                m_LastEnabled = false;
                m_LastR = r;
                m_LastG = g;
                m_LastB = b;
                m_LastA = a;
                m_Applied = true;
                return;
            }

            Color customColor = new(r, g, b, a);
            ApplyDistrictColors(customColor, customColor, customColor, customColor);
            MarkDistrictAreasUpdated();

            m_LastEnabled = true;
            m_LastR = r;
            m_LastG = g;
            m_LastB = b;
            m_LastA = a;
            m_Applied = true;
        }

        private void TryCaptureDefaults()
        {
            if (HasCapturedDistrictDefaults || !TryGetDefaultDistrictPrefab(out Entity prefabEntity))
            {
                return;
            }

            if (m_PrefabSystem != null
                && m_PrefabSystem.TryGetPrefab(prefabEntity, out PrefabBase prefabBase)
                && prefabBase is AreaPrefab areaPrefab)
            {
                CapturedDistrictFillColor = areaPrefab.m_Color;
                CapturedDistrictEdgeColor = areaPrefab.m_EdgeColor;
                CapturedDistrictSelectionFillColor = areaPrefab.m_SelectionColor;
                CapturedDistrictSelectionEdgeColor = areaPrefab.m_SelectionEdgeColor;
                HasCapturedDistrictDefaults = true;
            }
            else
            {
                PrefabAreaColorData colorData = EntityManager.GetComponentData<PrefabAreaColorData>(prefabEntity);
                CapturedDistrictFillColor = colorData.m_FillColor;
                CapturedDistrictEdgeColor = colorData.m_EdgeColor;
                CapturedDistrictSelectionFillColor = colorData.m_SelectionFillColor;
                CapturedDistrictSelectionEdgeColor = colorData.m_SelectionEdgeColor;
                HasCapturedDistrictDefaults = true;
            }

            if (!m_CaptureLogged && HasCapturedDistrictDefaults)
            {
                m_CaptureLogged = true;
                LogUtils.Info(() => $"{Mod.ModTag} Captured vanilla District fill color: " +
                    $"RGBA=({CapturedDistrictFillColor.r:F3}, {CapturedDistrictFillColor.g:F3}, " +
                    $"{CapturedDistrictFillColor.b:F3}, {CapturedDistrictFillColor.a:F3}); edge " +
                    $"RGBA=({CapturedDistrictEdgeColor.r:F3}, {CapturedDistrictEdgeColor.g:F3}, " +
                    $"{CapturedDistrictEdgeColor.b:F3}, {CapturedDistrictEdgeColor.a:F3}); selection fill " +
                    $"RGBA=({CapturedDistrictSelectionFillColor.r:F3}, {CapturedDistrictSelectionFillColor.g:F3}, " +
                    $"{CapturedDistrictSelectionFillColor.b:F3}, {CapturedDistrictSelectionFillColor.a:F3}); selection edge " +
                    $"RGBA=({CapturedDistrictSelectionEdgeColor.r:F3}, {CapturedDistrictSelectionEdgeColor.g:F3}, " +
                    $"{CapturedDistrictSelectionEdgeColor.b:F3}, {CapturedDistrictSelectionEdgeColor.a:F3})");
            }
        }

        private void SeedSettingsFromCapture(HoverColorsSettings settings)
        {
            if (m_SeededSettingsFromCapture || settings.DistrictColorEnabled || !HasCapturedDistrictDefaults)
            {
                return;
            }

            // In-memory only: this lets the panel show the real captured vanilla color without
            // writing to .coc until the player chooses a custom District color.
            settings.DistrictR = CapturedDistrictFillColor.r;
            settings.DistrictG = CapturedDistrictFillColor.g;
            settings.DistrictB = CapturedDistrictFillColor.b;
            settings.DistrictA = CapturedDistrictFillColor.a;
            m_SeededSettingsFromCapture = true;
        }

        private void ApplyDistrictColors(
            Color fillColor,
            Color edgeColor,
            Color selectionFillColor,
            Color selectionEdgeColor)
        {
            if (!TryGetDefaultDistrictPrefab(out Entity prefabEntity))
            {
                return;
            }

            PrefabAreaColorData data = EntityManager.GetComponentData<PrefabAreaColorData>(prefabEntity);

            // AreaBufferSystem consumes all four colors. Driving edge + selection edge here
            // is what controls the persistent District boundary line shown while editing.
            data.m_FillColor = fillColor;
            data.m_EdgeColor = edgeColor;
            data.m_SelectionFillColor = selectionFillColor;
            data.m_SelectionEdgeColor = selectionEdgeColor;
            EntityManager.SetComponentData(prefabEntity, data);
        }

        private void MarkDistrictAreasUpdated()
        {
            if (m_DistrictAreaQuery.IsEmptyIgnoreFilter)
            {
                return;
            }

            // AreaBufferSystem uses this exact signal when district display needs rebuilding
            // (for example on localization/name changes), so this uses same safe vanilla path.
            EntityManager.AddComponent<Updated>(m_DistrictAreaQuery);
        }

        private void ApplyDistrictBorderWidth(float requestedScale)
        {
            float scale = Mathf.Clamp(requestedScale, HoverColorsSettings.kMinOverlayThicknessScale, 1f);
            if (scale >= 0.999f && m_DistrictMaterial == null)
            {
                return;
            }

            if (m_DistrictMaterial != null && Mathf.Abs(scale - m_LastBorderScale) < 0.001f)
            {
                return;
            }

            if (m_AreaBufferSystem == null)
            {
                return;
            }

            // The district mesh has its own material. Fetch it only when the slider changes or
            // a newly loaded city needs the saved scale, avoiding a buffer query every frame.
            m_AreaBufferSystem.GetAreaBuffer(AreaType.District, out _, out Material material, out _);
            if (material == null)
            {
                // The game creates this material when its area-type prefab is ready. Retry on
                // the next update while a city is loading; do not log a normal load delay.
                return;
            }

            if (!ReferenceEquals(material, m_DistrictMaterial))
            {
                m_DistrictMaterial = material;
                m_VanillaAreaParameters = material.GetVector(s_AreaParameters);
                m_LastBorderScale = 1f;
                if (!m_BorderParametersLogged)
                {
                    m_BorderParametersLogged = true;
                    LogUtils.Info(() => $"{Mod.ModTag} Captured vanilla District area parameters: " +
                        $"({m_VanillaAreaParameters.x:F3}, {m_VanillaAreaParameters.y:F3}, " +
                        $"{m_VanillaAreaParameters.z:F3}, {m_VanillaAreaParameters.w:F3})");
                }
            }

            Vector4 scaledParameters = m_VanillaAreaParameters;
            scaledParameters.x *= scale;
            material.SetVector(s_AreaParameters, scaledParameters);
            m_LastBorderScale = scale;
        }

        private void RestoreDistrictBorderWidth()
        {
            if (m_DistrictMaterial != null)
            {
                m_DistrictMaterial.SetVector(s_AreaParameters, m_VanillaAreaParameters);
            }

            m_DistrictMaterial = null;
            m_LastBorderScale = 1f;
        }

        private bool TryGetDefaultDistrictPrefab(out Entity prefabEntity)
        {
            prefabEntity = Entity.Null;

            if (m_PrefabSystem == null
                || !m_PrefabSystem.TryGetPrefab(s_VanillaDistrictPrefabId, out PrefabBase prefabBase)
                || !m_PrefabSystem.TryGetEntity(prefabBase, out Entity candidate)
                || candidate == Entity.Null
                || !EntityManager.Exists(candidate)
                || !EntityManager.HasComponent<PrefabAreaColorData>(candidate)
                || !EntityManager.HasComponent<DistrictData>(candidate))
            {
                return false;
            }

            prefabEntity = candidate;
            return true;
        }

        private static bool ApproximatelyEqual(float a, float b)
        {
            return Mathf.Abs(a - b) < 0.0005f;
        }
    }
}
