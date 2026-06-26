// <copyright file="Setting.Save.cs" company="River-Mochi">
// Copyright (c) 2026 River-Mochi. All rights reserved.
// Licensed under the MIT License. You may not use this file except in compliance with this License.
// See LICENSE file in the project root for full license information.
// This notice and the MIT License notice must be kept with
// all copies or substantial portions of this code.
// ================= </copyright> ======================

// File: Settings/Setting.Save.cs
// Purpose: Queued/retried settings save helpers for HoverColorsSettings.

namespace HoverColors.Settings
{
    using System; // Exception handling for failed settings saves.
    using System.IO; // IOException detection for busy .coc save files.
    using System.Threading.Tasks; // Async save queue + retry delay.

    using Colossal.IO.AssetDatabase; // AssetDatabase.global.SaveSpecificSetting.

    using CS2Shared.RiverMochi; // Popup-safe LogUtils warnings/errors.

    public partial class HoverColorsSettings
    {
        private readonly object m_SaveGate = new object();
        private bool m_SaveInProgress;
        private bool m_SaveAgainRequested;

        public override async void ApplyAndSave()
        {
            Apply();
            await SaveSpecificSettingQueuedAsync();
        }

        private async Task SaveSpecificSettingQueuedAsync()
        {
            lock (m_SaveGate)
            {
                if (m_SaveInProgress)
                {
                    m_SaveAgainRequested = true;
                    return;
                }

                m_SaveInProgress = true;
            }

            while (true)
            {
                lock (m_SaveGate)
                {
                    m_SaveAgainRequested = false;
                }

                await SaveSpecificSettingWithRetryAsync();

                lock (m_SaveGate)
                {
                    if (!m_SaveAgainRequested)
                    {
                        m_SaveInProgress = false;
                        return;
                    }
                }
            }
        }

        private async Task SaveSpecificSettingWithRetryAsync()
        {
            const int MaxAttempts = 3;

            for (int attempt = 1; attempt <= MaxAttempts; attempt++)
            {
                try
                {
                    await AssetDatabase.global.SaveSpecificSetting(GetType().Name);
                    return;
                }
                catch (IOException ex) when (attempt < MaxAttempts)
                {
                    LogUtils.Warn(() => $"{Mod.ModTag} Settings save busy; retrying ({attempt}/{MaxAttempts - 1}): {ex.Message}");
                    await Task.Delay(150 * attempt);
                }
                catch (Exception ex)
                {
                    LogUtils.Error(() => $"{Mod.ModTag} Settings save failed: {ex.GetType().Name}: {ex.Message}", ex);
                    return;
                }
            }
        }
    }
}
