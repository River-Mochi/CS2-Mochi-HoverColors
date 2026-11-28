// <copyright file="HoverColorsUISystem.RoadEngineer.cs" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

namespace HoverColors.UI
{
    using System.Globalization;
    using Colossal.UI.Binding;
    using Game.Input;
    using Game.Prefabs;
    using Game.Simulation;
    using Game.Tools;
    using HoverColors.Systems;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    public partial class HoverColorsUISystem
    {
        private EntityQuery m_RoadEngineerQuery;
        private TerrainSystem? m_RoadEngineerTerrain;
        private PrefabSystem? m_RoadEngineerPrefabs;
        private ValueBinding<bool> m_RoadEngineerMatchHC = null!;
        private ValueBinding<bool> m_RoadEngineerActive = null!;
        private ValueBinding<string> m_RoadEngineerCourse = null!;
        private ValueBinding<float> m_RoadBadgeR = null!;
        private ValueBinding<float> m_RoadBadgeG = null!;
        private ValueBinding<float> m_RoadBadgeB = null!;
        private ValueBinding<float> m_RoadBadgeA = null!;
        private ValueBinding<bool> m_RoadBadgeNativeBackground = null!;
        private ValueBinding<bool> m_RoadBadgeCustomText = null!;
        private ValueBinding<float> m_RoadBadgeTextR = null!;
        private ValueBinding<float> m_RoadBadgeTextG = null!;
        private ValueBinding<float> m_RoadBadgeTextB = null!;
        private ValueBinding<bool> m_RoadEngineerEnabled = null!;
        private ValueBinding<bool> m_RoadEngineerSettingsOpenBinding = null!;
        private ValueBinding<bool> m_RoadEngineerSettingsPinnedBinding = null!;
        private bool m_RoadEngineerSettingsOpen;
        private bool m_RoadEngineerSettingsPinned;
        private ValueBinding<bool> m_RoadEngineerUnits = null!;
        private ValueBinding<int> m_RoadDistanceDecimals = null!;
        private ValueBinding<int> m_RoadAngleDecimals = null!;
        private ValueBinding<bool> m_RoadPreciseFloatingLabels = null!;
        private ValueBinding<int> m_RoadLabelFontPercent = null!;
        private ValueBinding<bool> m_RoadEngineerPositionSet = null!;
        private ValueBinding<int> m_RoadEngineerOffsetX = null!;
        private ValueBinding<int> m_RoadEngineerOffsetY = null!;
        private float m_RoadEngineerNextRead;

        private void RegisterRoadEngineerBindings()
        {
            m_RoadEngineerTerrain = World.GetOrCreateSystemManaged<TerrainSystem>();
            m_RoadEngineerPrefabs = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_RoadEngineerMatchHC = AddValueBinding("RoadEngineerMatchHC", false);
            m_RoadEngineerQuery = GetEntityQuery(ComponentType.ReadOnly<CreationDefinition>(),
                ComponentType.ReadOnly<NetCourse>());
            m_RoadEngineerActive = AddValueBinding("RoadEngineerActive", false);
            m_RoadEngineerCourse = AddValueBinding("RoadEngineerCourse", string.Empty);
            m_RoadBadgeR = AddValueBinding("RoadBadgeR", HoverColorsSettings.kDefaultRoadBadgeR);
            m_RoadBadgeG = AddValueBinding("RoadBadgeG", HoverColorsSettings.kDefaultRoadBadgeG);
            m_RoadBadgeB = AddValueBinding("RoadBadgeB", HoverColorsSettings.kDefaultRoadBadgeB);
            m_RoadBadgeA = AddValueBinding("RoadBadgeA", 1f);
            m_RoadBadgeNativeBackground = AddValueBinding("RoadBadgeNativeBackground", false);
            m_RoadBadgeCustomText = AddValueBinding("RoadBadgeCustomText", true);
            m_RoadBadgeTextR = AddValueBinding("RoadBadgeTextR", 1f);
            m_RoadBadgeTextG = AddValueBinding("RoadBadgeTextG", 235f / 255f);
            m_RoadBadgeTextB = AddValueBinding("RoadBadgeTextB", 128f / 255f);
            m_RoadEngineerEnabled = AddValueBinding("RoadEngineerEnabled", Mod.Settings?.RoadEngineerEnabled ?? true);
            m_RoadEngineerSettingsOpenBinding = AddValueBinding("RoadEngineerSettingsOpen", false);
            m_RoadEngineerSettingsPinnedBinding = AddValueBinding("RoadEngineerSettingsPinned", false);
            m_RoadEngineerUnits = AddValueBinding("RoadEngineerUseUnits", false);
            m_RoadDistanceDecimals = AddValueBinding("RoadDistanceDecimals", 2);
            m_RoadAngleDecimals = AddValueBinding("RoadAngleDecimals", 2);
            m_RoadPreciseFloatingLabels = AddValueBinding("RoadPreciseFloatingLabels", false);
            m_RoadLabelFontPercent = AddValueBinding("RoadLabelFontPercent", 100);
            m_RoadEngineerPositionSet = AddValueBinding("RoadEngineerPositionSet", false);
            m_RoadEngineerOffsetX = AddValueBinding("RoadEngineerOffsetX", 0);
            m_RoadEngineerOffsetY = AddValueBinding("RoadEngineerOffsetY", 0);
            AddBinding(new TriggerBinding(Mod.ModId, "CloseRoadEngineer", CloseRoadEngineer));
            AddBinding(new TriggerBinding<float, float, float>(Mod.ModId, "SetRoadBadgeTextColor", SetRoadBadgeTextColor));
            AddBinding(new TriggerBinding(Mod.ModId, "ResetRoadBadgeTextColor", ResetRoadBadgeTextColor));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "ResetRoadAppearance", ResetRoadAppearance));
            AddBinding(new TriggerBinding(Mod.ModId, "OpenRoadEngineerSettings", OpenRoadEngineerSettings));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetRoadEngineerSettingsOpen", SetRoadEngineerSettingsOpen));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetRoadEngineerEnabled", SetRoadEngineerEnabled));
            AddBinding(new TriggerBinding<int, int>(Mod.ModId, "SetRoadEngineerPosition", SetRoadEngineerPosition));
            AddBinding(new TriggerBinding<float, float, float, float>(Mod.ModId, "SetRoadBadgeColor", SetRoadBadgeColor));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetRoadEngineerUseUnits", SetRoadEngineerUnits));
            AddBinding(new TriggerBinding<bool, int>(Mod.ModId, "SetRoadPrecision", SetRoadPrecision));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetRoadPreciseFloatingLabels", SetRoadPreciseFloatingLabels));
            AddBinding(new TriggerBinding<int>(Mod.ModId, "SetRoadLabelFontPercent", SetRoadLabelFontPercent));
        }

        private void OpenRoadEngineerSettings()
        {
            m_RoadEngineerSettingsPinned = true;
            SetRoadEngineerSettingsOpen(true);
        }

        private void SetRoadEngineerSettingsOpen(bool open)
        {
            m_RoadEngineerSettingsOpen = open;
            // Auto-show OFF: keep settings accessible until the gear is closed.
            if (!open) m_RoadEngineerSettingsPinned = false;
            UpdateIfChanged(m_RoadEngineerSettingsOpenBinding, open);
            UpdateIfChanged(m_RoadEngineerSettingsPinnedBinding, m_RoadEngineerSettingsPinned);
        }

        private void SetRoadEngineerEnabled(bool enabled)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || settings.RoadEngineerEnabled == enabled) return;
            settings.RoadEngineerEnabled = enabled;
            settings.ApplyAndSave();
            UpdateIfChanged(m_RoadEngineerEnabled, enabled);
            if (!enabled)
            {
                // Keep the currently opened settings available for editing.
                if (m_RoadEngineerSettingsOpen)
                {
                    m_RoadEngineerSettingsPinned = true;
                    UpdateIfChanged(m_RoadEngineerSettingsPinnedBinding, true);
                }
                UpdateIfChanged(m_RoadEngineerActive, false);
                UpdateIfChanged(m_RoadEngineerCourse, string.Empty);
                m_RoadEngineerNextRead = 0f;
            }
        }

        private void CloseRoadEngineer()
        {
            m_RoadEngineerSettingsPinned = false;
            SetRoadEngineerSettingsOpen(false);
            SetRoadEngineerEnabled(false);
        }

        private void ResetRoadAppearance(bool native)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            settings.RoadBadgeNativeBackground = native;
            // Native mode leaves background painting to the game's tooltip style.
            // Store a representative dark swatch for the next custom edit.
            settings.RoadBadgeR = native ? 20f / 255f : HoverColorsSettings.kDefaultRoadBadgeR;
            settings.RoadBadgeG = native ? 27f / 255f : HoverColorsSettings.kDefaultRoadBadgeG;
            settings.RoadBadgeB = native ? 34f / 255f : HoverColorsSettings.kDefaultRoadBadgeB;
            settings.RoadBadgeA = 1f;
            settings.RoadBadgeCustomText = !native;
            settings.RoadBadgeTextR = 1f;
            settings.RoadBadgeTextG = native ? 1f : 235f / 255f;
            settings.RoadBadgeTextB = native ? 1f : 128f / 255f;
            settings.RoadEngineerUseUnits = false;
            settings.RoadDistanceDecimals = settings.RoadAngleDecimals = native ? 0 : 2;
            settings.RoadPreciseFloatingLabels = false;
            settings.RoadLabelFontPercent = 100;
            // Arc color, filter, visibility and position are independent choices.
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SetRoadBadgeTextColor(float r, float g, float b)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || !math.isfinite(r) || !math.isfinite(g) || !math.isfinite(b)) return;
            r = Clamp01(r);
            g = Clamp01(g);
            b = Clamp01(b);
            if (settings.RoadBadgeCustomText && ApproxEqual(settings.RoadBadgeTextR, r)
                && ApproxEqual(settings.RoadBadgeTextG, g) && ApproxEqual(settings.RoadBadgeTextB, b)) return;
            settings.RoadBadgeTextR = r;
            settings.RoadBadgeTextG = g;
            settings.RoadBadgeTextB = b;
            settings.RoadBadgeCustomText = true;
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void ResetRoadBadgeTextColor()
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || !settings.RoadBadgeCustomText) return;
            settings.RoadBadgeCustomText = false;
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SetRoadEngineerPosition(int x, int y)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            x = System.Math.Max(-16384, System.Math.Min(16384, x));
            y = System.Math.Max(-16384, System.Math.Min(16384, y));
            if (settings.RoadEngineerPositionSet && settings.RoadEngineerOffsetX == x
                && settings.RoadEngineerOffsetY == y) return;
            settings.RoadEngineerPositionSet = true;
            settings.RoadEngineerOffsetX = x;
            settings.RoadEngineerOffsetY = y;
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SetRoadBadgeColor(float r, float g, float b, float a)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || !math.isfinite(r) || !math.isfinite(g) || !math.isfinite(b) || !math.isfinite(a)) return;
            r = Clamp01(r);
            g = Clamp01(g);
            b = Clamp01(b);
            a = Clamp01(a);
            if (!settings.RoadBadgeNativeBackground && ApproxEqual(settings.RoadBadgeR, r) && ApproxEqual(settings.RoadBadgeG, g)
                && ApproxEqual(settings.RoadBadgeB, b) && ApproxEqual(settings.RoadBadgeA, a)) return;
            settings.RoadBadgeNativeBackground = false;
            settings.RoadBadgeR = r;
            settings.RoadBadgeG = g;
            settings.RoadBadgeB = b;
            settings.RoadBadgeA = a;
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SetRoadEngineerUnits(bool units)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || settings.RoadEngineerUseUnits == units) return;
            settings.RoadEngineerUseUnits = units;
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SetRoadLabelFontPercent(int percent)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            percent = HoverColorsSettings.NormalizeRoadLabelFontPercent(percent);
            if (settings.RoadLabelFontPercent == percent) return;
            settings.RoadLabelFontPercent = percent;
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SetRoadPreciseFloatingLabels(bool enabled)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || settings.RoadPreciseFloatingLabels == enabled
                || !settings.RoadAngleTooltipsHidden) return;
            settings.RoadPreciseFloatingLabels = enabled;
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SetRoadPrecision(bool angle, int decimals)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null) return;
            decimals = math.clamp(decimals, 0, 2);
            if (angle)
            {
                if (settings.RoadAngleDecimals == decimals) return;
                settings.RoadAngleDecimals = decimals;
            }
            else
            {
                if (settings.RoadDistanceDecimals == decimals) return;
                settings.RoadDistanceDecimals = decimals;
            }
            settings.ApplyAndSave();
            SyncRoadEngineerSettings();
        }

        private void SyncRoadEngineerSettings()
        {
            HoverColorsSettings? settings = Mod.Settings;
            UpdateIfChanged(m_RoadBadgeR, settings?.RoadBadgeR ?? HoverColorsSettings.kDefaultRoadBadgeR);
            UpdateIfChanged(m_RoadBadgeG, settings?.RoadBadgeG ?? HoverColorsSettings.kDefaultRoadBadgeG);
            UpdateIfChanged(m_RoadBadgeB, settings?.RoadBadgeB ?? HoverColorsSettings.kDefaultRoadBadgeB);
            UpdateIfChanged(m_RoadBadgeA, settings?.RoadBadgeA ?? 1f);
            UpdateIfChanged(m_RoadBadgeNativeBackground, settings?.RoadBadgeNativeBackground ?? false);
            UpdateIfChanged(m_RoadBadgeCustomText, settings?.RoadBadgeCustomText ?? true);
            UpdateIfChanged(m_RoadBadgeTextR, settings?.RoadBadgeTextR ?? 1f);
            UpdateIfChanged(m_RoadBadgeTextG, settings?.RoadBadgeTextG ?? 235f / 255f);
            UpdateIfChanged(m_RoadBadgeTextB, settings?.RoadBadgeTextB ?? 128f / 255f);
            UpdateIfChanged(m_RoadEngineerMatchHC, settings?.RoadEngineerMatchHC ?? false);
            UpdateIfChanged(m_RoadEngineerEnabled, settings?.RoadEngineerEnabled ?? true);
            UpdateIfChanged(m_RoadEngineerUnits, settings?.RoadEngineerUseUnits ?? false);
            UpdateIfChanged(m_RoadDistanceDecimals, math.clamp(settings?.RoadDistanceDecimals ?? 2, 0, 2));
            UpdateIfChanged(m_RoadAngleDecimals, math.clamp(settings?.RoadAngleDecimals ?? 2, 0, 2));
            UpdateIfChanged(m_RoadPreciseFloatingLabels, settings?.RoadPreciseFloatingLabels ?? false);
            UpdateIfChanged(m_RoadLabelFontPercent, HoverColorsSettings.NormalizeRoadLabelFontPercent(settings?.RoadLabelFontPercent ?? 100));
            UpdateIfChanged(m_RoadEngineerPositionSet, settings?.RoadEngineerPositionSet ?? false);
            UpdateIfChanged(m_RoadEngineerOffsetX, settings?.RoadEngineerOffsetX ?? 0);
            UpdateIfChanged(m_RoadEngineerOffsetY, settings?.RoadEngineerOffsetY ?? 0);
        }

        private void UpdateRoadEngineer()
        {
            SyncRoadEngineerSettings();
            if (Mod.Settings?.RoadEngineerEnabled != true
                || m_RoadMeasurementToolSystem?.activeTool is not NetToolSystem netTool
                || netTool.actualMode == NetToolSystem.Mode.Replace
                || (netTool.GetPrefab() is not RoadPrefab && netTool.GetPrefab() is not PathwayPrefab))
            {
                UpdateIfChanged(m_RoadEngineerActive, false);
                UpdateIfChanged(m_RoadEngineerCourse, string.Empty);
                m_RoadEngineerNextRead = 0f;
                if (m_RoadEngineerSettingsOpen && !m_RoadEngineerSettingsPinned)
                    SetRoadEngineerSettingsOpen(false);
                return;
            }

            // Keep settings accessible before drawing and while using a picker.
            UpdateIfChanged(m_RoadEngineerActive, true);
            if (InputManager.instance.mouseOverUI) return;
            float now = UnityEngine.Time.unscaledTime;
            if (now < m_RoadEngineerNextRead) return;
            m_RoadEngineerNextRead = now + 0.1f;

            int count = m_RoadEngineerQuery.CalculateEntityCount();
            if (count <= 0 || count > 256)
            {
                UpdateIfChanged(m_RoadEngineerCourse, string.Empty);
                return;
            }

            // The same read-only query and eligibility rules as vanilla.
            // Bound work and never report an unverified multi-chain preview.
            using NativeArray<CreationDefinition> definitions =
                m_RoadEngineerQuery.ToComponentDataArray<CreationDefinition>(Allocator.Temp);
            using NativeArray<NetCourse> courses =
                m_RoadEngineerQuery.ToComponentDataArray<NetCourse>(Allocator.Temp);
            using NativeList<NetCourse> eligible = new(courses.Length, Allocator.Temp);
            Entity selectedEntity = Entity.Null;
            float slopeLimit = float.NaN;
            if (m_RoadEngineerPrefabs != null && netTool.GetPrefab() is RoadPrefab road
                && m_RoadEngineerPrefabs.TryGetEntity(road, out selectedEntity)
                && m_RoadEngineerPrefabs.TryGetComponentData<NetGeometryData>(road, out NetGeometryData geometry)
                && math.isfinite(geometry.m_MaxSlopeSteepness) && geometry.m_MaxSlopeSteepness > 0f)
            {
                slopeLimit = 100f * geometry.m_MaxSlopeSteepness;
            }
            for (int i = 0; i < courses.Length; i++)
            {
                if (RoadEngineerCourseMeasurements.IsEligible(definitions[i], courses[i]))
                {
                    eligible.Add(courses[i]);
                    if (definitions[i].m_Prefab != selectedEntity) slopeLimit = float.NaN;
                }
            }

            if (!RoadEngineerCourseMeasurements.TryRead(eligible.AsArray(), out RoadEngineerCourseMeasurements result))
            {
                UpdateIfChanged(m_RoadEngineerCourse, string.Empty);
                return;
            }

            // Transmit raw values, not formatted or already-rounded tooltips.
            string snapshot = string.Join("|",
                result.TotalLength.ToString("R", CultureInfo.InvariantCulture),
                result.Distance.ToString("R", CultureInfo.InvariantCulture),
                result.Elevation.ToString("R", CultureInfo.InvariantCulture),
                result.Relief.ToString("R", CultureInfo.InvariantCulture),
                result.Slope.ToString("R", CultureInfo.InvariantCulture),
                ReadGroundOffset(result.End).ToString("R", CultureInfo.InvariantCulture),
                result.Bearing.ToString("R", CultureInfo.InvariantCulture),
                result.MaxCourseGrade.ToString("R", CultureInfo.InvariantCulture),
                slopeLimit.ToString("R", CultureInfo.InvariantCulture));
            UpdateIfChanged(m_RoadEngineerCourse, snapshot);
        }

        // Read the cached native terrain height on the main thread. No GPU
        // readback, new job, or terrain update is requested. Skip unavailable
        // data and out-of-range points rather than report a misleading depth.
        private float ReadGroundOffset(float3 endpoint)
        {
            if (m_RoadEngineerTerrain == null) return float.NaN;
            TerrainHeightData heightData = m_RoadEngineerTerrain.GetHeightData();
            int3 resolution = heightData.resolution;
            if (!heightData.isCreated || resolution.y != 65536
                || resolution.x < 2 || resolution.z < 2
                || (long)resolution.x * resolution.z != heightData.heights.Length
                || !math.all(math.isfinite(heightData.scale)) || heightData.scale.y <= 0f)
            {
                return float.NaN;
            }

            float2 sample = TerrainUtils.ToHeightmapSpace(ref heightData, endpoint).xz;
            if (!math.all(math.isfinite(sample))
                || math.any(sample < 0f)
                || math.any(sample >= new float2(resolution.x - 1, resolution.z - 1)))
            {
                return float.NaN;
            }

            float ground = TerrainUtils.SampleHeight(ref heightData, endpoint);
            return math.isfinite(ground) ? endpoint.y - ground : float.NaN;
        }
    }
}
