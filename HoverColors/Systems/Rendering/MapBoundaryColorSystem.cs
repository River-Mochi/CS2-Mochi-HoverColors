// <copyright file="MapBoundaryColorSystem.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: Systems/Rendering/MapBoundaryColorSystem.cs
// Applies map border colors/visibility/size to the vanilla prefab on change only.
// The vanilla boundary mesh system still owns geometry, tile updates and drawing.

namespace HoverColors.Systems
{
    using CS2Shared.RiverMochi;
    using Game;
    using Game.Prefabs;
    using Game.Rendering;
    using Unity.Entities;
    using UnityEngine;

    public partial class MapBoundaryColorSystem : GameSystemBase
    {
        public static Color CapturedTileColor { get; private set; } = new(1f, 1f, 1f, 0.75f);
        public static Color CapturedMapColor { get; private set; } = new(1f, 1f, 1f, 0.25f);
        public static float CapturedWidth { get; private set; } = 20f;
        public static float CapturedTilingLength { get; private set; } = 80f;

        private PrefabSystem? m_PrefabSystem;
        private CityBoundaryMeshSystem? m_MeshSystem;
        private CityBoundaryPrefab? m_CapturedPrefab;
        private EntityQuery m_BoundaryPrefabQuery;
        private float m_NextRefreshTime;

        public static Color GetTileColor(HoverColorsSettings? settings)
        {
            return settings != null && settings.MapBoundaryTileCustom
                ? new Color(settings.MapBoundaryTileR, settings.MapBoundaryTileG,
                    settings.MapBoundaryTileB, settings.MapBoundaryTileA)
                : CapturedTileColor;
        }

        public static Color GetMapColor(HoverColorsSettings? settings)
        {
            return settings != null && settings.MapBoundaryMapCustom
                ? new Color(settings.MapBoundaryMapR, settings.MapBoundaryMapG,
                    settings.MapBoundaryMapB, settings.MapBoundaryMapA)
                : CapturedMapColor;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_MeshSystem = World.GetOrCreateSystemManaged<CityBoundaryMeshSystem>();
            m_BoundaryPrefabQuery = SystemAPI.QueryBuilder()
                .WithAll<CityBoundaryData>()
                .Build();
            RequireForUpdate(m_BoundaryPrefabQuery);
        }

        protected override void OnUpdate()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || m_PrefabSystem == null || m_MeshSystem == null ||
                m_BoundaryPrefabQuery.IsEmptyIgnoreFilter)
            {
                return;
            }

            Entity prefabEntity = m_BoundaryPrefabQuery.GetSingletonEntity();
            if (!m_PrefabSystem.TryGetPrefab(prefabEntity, out PrefabBase prefabBase) ||
                prefabBase is not CityBoundaryPrefab prefab)
            {
                return;
            }

            // Only capture authored vanilla values, not our own modified values. A new prefab
            // instance indicates that the game has reconstructed the prefab data.
            if (!ReferenceEquals(m_CapturedPrefab, prefab))
            {
                m_CapturedPrefab = prefab;
                CapturedWidth = prefab.m_Width;
                CapturedTilingLength = prefab.m_TilingLength;
                CapturedTileColor = prefab.m_CityBorderColor;
                CapturedMapColor = prefab.m_MapBorderColor;
                LogUtils.Info(() => $"{Mod.ModTag} Map borders captured: width={CapturedWidth:F1}, " +
                    $"spacing={CapturedTilingLength:F1}, tile={CapturedTileColor}, map={CapturedMapColor}");
            }

            Color tile = GetTileColor(settings);
            Color map = GetMapColor(settings);
            if (!settings.MapBoundaryTileVisible) tile.a = 0f;
            if (!settings.MapBoundaryMapVisible) map.a = 0f;

            float width = CapturedWidth * Mathf.Clamp(settings.MapBoundaryWidthScale, 0.5f, 2f);
            float tiling = CapturedTilingLength * Mathf.Clamp(settings.MapBoundaryDashScale, 0.5f, 2f);

            // Zero width guarantees a completely hidden mesh when both sets are off, even if the
            // boundary material's dark fringe is not driven solely by vertex-color alpha.
            if (!settings.MapBoundaryTileVisible && !settings.MapBoundaryMapVisible)
            {
                width = 0f;
            }

            if (Mathf.Abs(prefab.m_Width - width) < 0.001f &&
                Mathf.Abs(prefab.m_TilingLength - tiling) < 0.001f &&
                SameColor(prefab.m_CityBorderColor, tile) &&
                SameColor(prefab.m_MapBorderColor, map))
            {
                return;
            }

            // Slider dragging can generate several UI events in one instant; avoid
            // rebuilding all tile boundaries for every individual intermediate value.
            if (UnityEngine.Time.unscaledTime < m_NextRefreshTime)
            {
                return;
            }

            prefab.m_Width = width;
            prefab.m_TilingLength = tiling;
            prefab.m_CityBorderColor = tile;
            prefab.m_MapBorderColor = map;

            // 1.6.4f1: CityBoundaryMeshSystem only rebuilds when map tiles are updated or
            // a load is flagged. Its public IPreDeserialize hook clears the generated mesh
            // and requests regeneration on the next vanilla PreCulling update.
            // No fake tile edits, duplicated meshes, or Harmony patches.
            m_MeshSystem.PreDeserialize(default);
            m_NextRefreshTime = UnityEngine.Time.unscaledTime + 0.12f;
        }

        public void RestoreVanilla()
        {
            if (m_CapturedPrefab == null)
            {
                return;
            }

            m_CapturedPrefab.m_Width = CapturedWidth;
            m_CapturedPrefab.m_TilingLength = CapturedTilingLength;
            m_CapturedPrefab.m_CityBorderColor = CapturedTileColor;
            m_CapturedPrefab.m_MapBorderColor = CapturedMapColor;
            m_MeshSystem?.PreDeserialize(default);
        }

        private static bool SameColor(Color left, Color right)
        {
            return Mathf.Abs(left.r - right.r) < 0.0005f
                && Mathf.Abs(left.g - right.g) < 0.0005f
                && Mathf.Abs(left.b - right.b) < 0.0005f
                && Mathf.Abs(left.a - right.a) < 0.0005f;
        }
    }
}
