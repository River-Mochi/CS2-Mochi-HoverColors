// File: Systems/HoverPowerUISystem.cs
// Purpose: Bridges Mod settings to/from the cs2/api bindings the UI (cohtml) layer reads.
// Post-redesign binding shape:
//   Getters (settings -> UI):
//     OutlineR, OutlineG, OutlineB, OutlineA, FillA, GuidelineOpacityPercent
//   Triggers (UI -> settings):
//     SetOutlineColor(r, g, b, a)         — fired by the vanilla ColorField onChange
//     SetFillAlpha(a)                     — fired by the Fill alpha slider onChange
//     SetGuidelineOpacity(percent)        — fired by the in-city Guidelines slider
//                                           (Options-UI slider for the same field stays as fallback)
// OutlineColorSystem + GuidelineColorSystem (Rendering phase) pick up changes via their dirty-flag.

using Colossal.UI.Binding;
using Game.UI;
using HoverPower.Settings;

namespace HoverPower.UI
{
    public partial class HoverPowerUISystem : UISystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            AddUpdateBinding(new GetterValueBinding<float>(
                Mod.ModId, "OutlineR",
                () => Mod.Settings?.OutlineR ?? 0.502f));

            AddUpdateBinding(new GetterValueBinding<float>(
                Mod.ModId, "OutlineG",
                () => Mod.Settings?.OutlineG ?? 0.869f));

            AddUpdateBinding(new GetterValueBinding<float>(
                Mod.ModId, "OutlineB",
                () => Mod.Settings?.OutlineB ?? 1f));

            AddUpdateBinding(new GetterValueBinding<float>(
                Mod.ModId, "OutlineA",
                () => Mod.Settings?.OutlineA ?? 0.855f));

            AddUpdateBinding(new GetterValueBinding<float>(
                Mod.ModId, "FillA",
                () => Mod.Settings?.FillA ?? 0f));

            AddUpdateBinding(new GetterValueBinding<int>(
                Mod.ModId, "GuidelineOpacityPercent",
                () => Mod.Settings?.GuidelineOpacityPercent ?? 100));

            AddBinding(new TriggerBinding<float, float, float, float>(
                Mod.ModId,
                "SetOutlineColor",
                (r, g, b, a) =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    settings.OutlineR = r;
                    settings.OutlineG = g;
                    settings.OutlineB = b;
                    settings.OutlineA = a;
                    settings.ApplyAndSave();
                }));

            AddBinding(new TriggerBinding<float>(
                Mod.ModId,
                "SetFillAlpha",
                a =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    settings.FillA = a;
                    settings.ApplyAndSave();
                }));

            AddBinding(new TriggerBinding<int>(
                Mod.ModId,
                "SetGuidelineOpacity",
                percent =>
                {
                    HoverPowerSettings? settings = Mod.Settings;
                    if (settings == null) return;

                    if (percent < 0) percent = 0;
                    if (percent > 100) percent = 100;

                    settings.GuidelineOpacityPercent = percent;
                    settings.ApplyAndSave();
                }));
        }
    }
}
