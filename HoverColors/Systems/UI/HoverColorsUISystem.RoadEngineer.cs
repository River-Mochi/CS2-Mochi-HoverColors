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
    using Game.Tools;
    using HoverColors.Systems;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    public partial class HoverColorsUISystem
    {
        private EntityQuery m_RoadEngineerQuery;
        private ValueBinding<bool> m_RoadEngineerActive = null!;
        private ValueBinding<string> m_RoadEngineerCourse = null!;
        private ValueBinding<float> m_RoadBadgeR = null!;
        private ValueBinding<float> m_RoadBadgeG = null!;
        private ValueBinding<float> m_RoadBadgeB = null!;
        private ValueBinding<bool> m_RoadEngineerUnits = null!;
        private float m_RoadEngineerNextRead;

        private void RegisterRoadEngineerBindings()
        {
            m_RoadEngineerQuery = GetEntityQuery(ComponentType.ReadOnly<CreationDefinition>(),
                ComponentType.ReadOnly<NetCourse>());
            m_RoadEngineerActive = AddValueBinding("RoadEngineerActive", false);
            m_RoadEngineerCourse = AddValueBinding("RoadEngineerCourse", string.Empty);
            m_RoadBadgeR = AddValueBinding("RoadBadgeR", HoverColorsSettings.kDefaultRoadBadgeR);
            m_RoadBadgeG = AddValueBinding("RoadBadgeG", HoverColorsSettings.kDefaultRoadBadgeG);
            m_RoadBadgeB = AddValueBinding("RoadBadgeB", HoverColorsSettings.kDefaultRoadBadgeB);
            m_RoadEngineerUnits = AddValueBinding("RoadEngineerUseUnits", false);
            AddBinding(new TriggerBinding<float, float, float>(Mod.ModId, "SetRoadBadgeColor", SetRoadBadgeColor));
            AddBinding(new TriggerBinding<bool>(Mod.ModId, "SetRoadEngineerUseUnits", SetRoadEngineerUnits));
        }

        private void SetRoadBadgeColor(float r, float g, float b)
        {
            HoverColorsSettings? settings = Mod.Settings;
            if (settings == null || !math.isfinite(r) || !math.isfinite(g) || !math.isfinite(b)) return;
            r = Clamp01(r);
            g = Clamp01(g);
            b = Clamp01(b);
            if (ApproxEqual(settings.RoadBadgeR, r) && ApproxEqual(settings.RoadBadgeG, g)
                && ApproxEqual(settings.RoadBadgeB, b)) return;
            settings.RoadBadgeR = r;
            settings.RoadBadgeG = g;
            settings.RoadBadgeB = b;
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

        private void SyncRoadEngineerSettings()
        {
            HoverColorsSettings? settings = Mod.Settings;
            UpdateIfChanged(m_RoadBadgeR, settings?.RoadBadgeR ?? HoverColorsSettings.kDefaultRoadBadgeR);
            UpdateIfChanged(m_RoadBadgeG, settings?.RoadBadgeG ?? HoverColorsSettings.kDefaultRoadBadgeG);
            UpdateIfChanged(m_RoadBadgeB, settings?.RoadBadgeB ?? HoverColorsSettings.kDefaultRoadBadgeB);
            UpdateIfChanged(m_RoadEngineerUnits, settings?.RoadEngineerUseUnits ?? false);
        }

        private void UpdateRoadEngineer()
        {
            SyncRoadEngineerSettings();
            if (m_RoadMeasurementToolSystem?.activeTool is not NetToolSystem netTool
                || netTool.actualMode == NetToolSystem.Mode.Replace
                || (netTool.GetPrefab() is not RoadPrefab && netTool.GetPrefab() is not PathwayPrefab))
            {
                UpdateIfChanged(m_RoadEngineerActive, false);
                UpdateIfChanged(m_RoadEngineerCourse, string.Empty);
                m_RoadEngineerNextRead = 0f;
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
            for (int i = 0; i < courses.Length; i++)
            {
                if (RoadEngineerCourseMeasurements.IsEligible(definitions[i], courses[i]))
                    eligible.Add(courses[i]);
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
                result.Slope.ToString("R", CultureInfo.InvariantCulture));
            UpdateIfChanged(m_RoadEngineerCourse, snapshot);
        }
    }
}
