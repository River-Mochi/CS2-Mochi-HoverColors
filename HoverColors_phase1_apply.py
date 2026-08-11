# <copyright file="HoverColors_phase1_apply.py" company="River-Mochi">
# Copyright (c) 2026 River-Mochi. All rights reserved.
# Licensed under the MIT License. You may not use this file except in compliance with this License.
# See LICENSE file in the project root for full license information.
# This notice and the MIT License notice must be kept with
# all copies or substantial portions of this code.
# ================= </copyright> ======================

#!/usr/bin/env python3
"""Apply Hover Colors phase-1 Eye/Editor changes to the addOffButton branch.

Run from the CS2-Mochi-HoverColors repo root:
  python HoverColors_phase1_apply.py --check
  python HoverColors_phase1_apply.py

The script validates every expected source snippet before writing anything.
"""

from __future__ import annotations

import argparse
import shutil
import sys
from pathlib import Path


def replace_once(text: str, old: str, new: str, label: str) -> str:
    count = text.count(old)
    if count != 1:
        raise RuntimeError(f"{label}: expected source block exactly once, found {count}")
    return text.replace(old, new, 1)


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--check", action="store_true", help="validate only; do not write files")
    parser.add_argument("--repo", default=".", help="repo root (default: current directory)")
    args = parser.parse_args()

    repo = Path(args.repo).resolve()
    package_dir = Path(__file__).resolve().parent

    required = [
        "HoverColors/Mod.cs",
        "HoverColors/Settings/Setting.cs",
        "HoverColors/Settings/Setting.Defaults.cs",
        "HoverColors/Localization/LocaleEN.cs",
        "HoverColors/Systems/HoverColorsUISystem.cs",
        "HoverColors/Systems/OutlineColorSystem.cs",
        "HoverColors/Systems/AreaToolOverlaySystem.cs",
        "HoverColors/UI/src/index.tsx",
        "HoverColors/UI/src/MochiColorPickerPanel.tsx",
        "HoverColors/UI/src/MochiColorPickerPanel.module.scss",
        "HoverColors/UI/src/panel/bindings/MochiPanelBindings.ts",
        "HoverColors/UI/src/panel/hooks/useMochiPanelText.ts",
        "HoverColors/L10n/lang/en-US.json",
    ]

    missing = [p for p in required if not (repo / p).is_file()]
    if missing:
        print("ERROR: repo root does not look like CS2-Mochi-HoverColors.", file=sys.stderr)
        for p in missing:
            print(f"  missing: {p}", file=sys.stderr)
        return 2

    changed: dict[Path, str] = {}

    def load(rel: str) -> str:
        path = repo / rel
        return path.read_text(encoding="utf-8-sig")

    def stage(rel: str, text: str) -> None:
        changed[repo / rel] = text

    # ------------------------------------------------------------------
    # Mod.cs - new optional keybind action ID.
    # ------------------------------------------------------------------
    rel = "HoverColors/Mod.cs"
    text = load(rel)
    text = replace_once(
        text,
        '        public const string kTogglePresetActionName = "TogglePreset";\n',
        '        public const string kTogglePresetActionName = "TogglePreset";\n'
        '        public const string kToggleHoverHighlightsActionName = "ToggleHoverHighlights";\n',
        rel,
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # Setting.cs - persisted eye state + unbound keybind.
    # ------------------------------------------------------------------
    rel = "HoverColors/Settings/Setting.cs"
    text = load(rel)
    text = replace_once(
        text,
        '        [SettingsUIHidden]\n'
        '        public bool PanelCollapsed { get; set; }\n\n'
        '        // Hidden in-city preference for the Surface tool button/hotkey.\n',
        '        [SettingsUIHidden]\n'
        '        public bool PanelCollapsed { get; set; }\n\n'
        '        // Hidden persistent eye-button state. ON suppresses normal hover outline + fill;\n'
        '        // a clicked/selected object still uses the player\'s current HC color under the pointer.\n'
        '        [SettingsUIHidden]\n'
        '        public bool HoverHighlightsSuppressed { get; set; }\n\n'
        '        // Hidden in-city preference for the Surface tool button/hotkey.\n',
        rel + " persisted state",
    )
    text = replace_once(
        text,
        '        [SettingsUISection(Actions, kKeyBindings)]\n'
        '        [SettingsUIKeyboardBinding(BindingKeyboard.J, Mod.kTogglePanelActionName)]\n'
        '        public ProxyBinding TogglePanelBinding { get; set; }\n\n'
        '        [SettingsUISection(Actions, kKeyBindings)]\n'
        '        [SettingsUIKeyboardBinding(BindingKeyboard.L, Mod.kToggleSurfaceToolAreasActionName)]\n',
        '        [SettingsUISection(Actions, kKeyBindings)]\n'
        '        [SettingsUIKeyboardBinding(BindingKeyboard.J, Mod.kTogglePanelActionName)]\n'
        '        public ProxyBinding TogglePanelBinding { get; set; }\n\n'
        '        // Ships unbound so HC never steals a key from the game or another mod.\n'
        '        [SettingsUISection(Actions, kKeyBindings)]\n'
        '        [SettingsUIKeyboardBinding(BindingKeyboard.None, Mod.kToggleHoverHighlightsActionName)]\n'
        '        public ProxyBinding ToggleHoverHighlightsBinding { get; set; }\n\n'
        '        [SettingsUISection(Actions, kKeyBindings)]\n'
        '        [SettingsUIKeyboardBinding(BindingKeyboard.L, Mod.kToggleSurfaceToolAreasActionName)]\n',
        rel + " keybind",
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # Defaults - eye starts ON (normal hover visible), but its OFF state persists.
    # ------------------------------------------------------------------
    rel = "HoverColors/Settings/Setting.Defaults.cs"
    text = load(rel)
    text = replace_once(
        text,
        '            PanelTooltipsEnabled = true;\n'
        '            SurfaceToolAreasSuppressed = true;\n',
        '            PanelTooltipsEnabled = true;\n'
        '            HoverHighlightsSuppressed = false;\n'
        '            SurfaceToolAreasSuppressed = true;\n',
        rel,
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # English Options localization for the new unbound hotkey.
    # ------------------------------------------------------------------
    rel = "HoverColors/Localization/LocaleEN.cs"
    text = load(rel)
    text = replace_once(
        text,
        '                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Toggle Hover Colors panel" },\n\n'
        '                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Toggle Surface tool previews on/off" },\n',
        '                { m_Settings.GetBindingKeyLocaleID(Mod.kTogglePanelActionName), "Toggle Hover Colors panel" },\n\n'
        '                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)), "Hover outline + fill On/Off" },\n'
        '                { m_Settings.GetOptionDescLocaleID(nameof(HoverColorsSettings.ToggleHoverHighlightsBinding)),\n'
        '                    "Optional hotkey for the title-bar Eye button. Ships unbound to avoid key conflicts." },\n'
        '                { m_Settings.GetBindingKeyLocaleID(Mod.kToggleHoverHighlightsActionName), "Hover outline + fill On/Off" },\n\n'
        '                { m_Settings.GetOptionLabelLocaleID(nameof(HoverColorsSettings.ToggleSurfaceToolAreasBinding)), "Toggle Surface tool previews on/off" },\n',
        rel,
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # UISystem - Game + Editor, register eye binding, poll its optional hotkey.
    # ------------------------------------------------------------------
    rel = "HoverColors/Systems/HoverColorsUISystem.cs"
    text = load(rel)
    text = replace_once(
        text,
        '    using Game.SceneFlow;\n',
        '',
        rel + " remove Game.SceneFlow",
    )
    text = replace_once(
        text,
        '    public partial class HoverColorsUISystem : UISystemBase\n'
        '    {\n'
        '        private static bool s_PanelOpen;\n',
        '    public partial class HoverColorsUISystem : UISystemBase\n'
        '    {\n'
        '        public override GameMode gameMode => GameMode.GameOrEditor;\n\n'
        '        private static bool s_PanelOpen;\n',
        rel + " gameMode",
    )
    text = replace_once(
        text,
        '            InitializeKeybindActions();\n'
        '            RegisterValueBindings();\n'
        '            RegisterTriggerBindings();\n',
        '            InitializeKeybindActions();\n'
        '            RegisterValueBindings();\n'
        '            RegisterTriggerBindings();\n'
        '            RegisterHoverVisibilityBindings();\n',
        rel + " OnCreate",
    )
    text = replace_once(
        text,
        '            // Don\'t fire hotkeys in main menu / editor.\n'
        '            if (!IsInGame())\n'
        '            {\n'
        '                return;\n'
        '            }\n\n',
        '',
        rel + " old game-only guard",
    )
    text = replace_once(
        text,
        '            if (m_ToggleSurfaceToolAreasAction?.WasReleasedThisFrame() == true)\n'
        '            {\n'
        '                ToggleSurfaceToolAreas();\n'
        '            }\n\n'
        '            // K toggles between preset slot 1 and slot 2.\n',
        '            UpdateHoverVisibilityHotkey();\n\n'
        '            if (m_ToggleSurfaceToolAreasAction?.WasReleasedThisFrame() == true)\n'
        '            {\n'
        '                ToggleSurfaceToolAreas();\n'
        '            }\n\n'
        '            // K toggles between preset slot 1 and slot 2.\n',
        rel + " visibility hotkey",
    )
    text = replace_once(
        text,
        '\n        private static bool IsInGame()\n'
        '        {\n'
        '            return GameManager.instance != null && GameManager.instance.gameMode == GameMode.Game;\n'
        '        }\n',
        '',
        rel + " remove IsInGame",
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # OutlineColorSystem - two tiny hooks; selection/raycast logic lives in a new partial file.
    # ------------------------------------------------------------------
    rel = "HoverColors/Systems/OutlineColorSystem.cs"
    text = load(rel)
    text = replace_once(
        text,
        '            m_RenderSettingsQuery = GetEntityQuery(ComponentType.ReadWrite<RenderingSettingsData>());\n'
        '            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();\n'
        '            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();\n',
        '            m_RenderSettingsQuery = GetEntityQuery(ComponentType.ReadWrite<RenderingSettingsData>());\n'
        '            m_ToolSystem = World.GetOrCreateSystemManaged<ToolSystem>();\n'
        '            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();\n'
        '            InitializeHoverVisibility();\n',
        rel + " OnCreate",
    )
    text = replace_once(
        text,
        '                palette = MatchesCapturedVanillaProfile(r, g, b, outlineA, fillA, ownerR, ownerG, ownerB, ownerA)\n'
        '                    ? EffectivePalette.CapturedVanilla\n'
        '                    : EffectivePalette.Custom;\n'
        '            }\n\n'
        '            // Hot-path: neither effective slider value nor the override flag has shifted.\n',
        '                palette = MatchesCapturedVanillaProfile(r, g, b, outlineA, fillA, ownerR, ownerG, ownerB, ownerA)\n'
        '                    ? EffectivePalette.CapturedVanilla\n'
        '                    : EffectivePalette.Custom;\n'
        '            }\n\n'
        '            ApplyHoverVisibilitySuppression(\n'
        '                settings,\n'
        '                activeToolSystem,\n'
        '                ref outlineA,\n'
        '                ref fillA,\n'
        '                ref ownerA,\n'
        '                ref palette);\n\n'
        '            // Hot-path: neither effective slider value nor the override flag has shifted.\n',
        rel + " suppression hook",
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # Area-tool overlay suppression also works in Editor.
    # ------------------------------------------------------------------
    rel = "HoverColors/Systems/AreaToolOverlaySystem.cs"
    text = load(rel)
    text = replace_once(
        text,
        '            if (!IsInGame() || m_ToolSystem == null || m_AreaToolSystem == null)\n',
        '            if (!IsInGameOrEditor() || m_ToolSystem == null || m_AreaToolSystem == null)\n',
        rel + " guard",
    )
    text = replace_once(
        text,
        '        private static bool IsInGame()\n'
        '        {\n'
        '            return GameManager.instance != null && GameManager.instance.gameMode == GameMode.Game;\n'
        '        }\n',
        '        private static bool IsInGameOrEditor()\n'
        '        {\n'
        '            return GameManager.instance != null && GameManager.instance.gameMode.IsGameOrEditor();\n'
        '        }\n',
        rel + " helper",
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # UI registration - keep GameTopLeft exactly; add only an Editor panel mount.
    # ------------------------------------------------------------------
    rel = "HoverColors/UI/src/index.tsx"
    text = load(rel)
    text = replace_once(
        text,
        'import ModIconButton from "./entry/ModIconButton";\n',
        'import EditorPanelMount from "./entry/EditorPanelMount";\n'
        'import ModIconButton from "./entry/ModIconButton";\n',
        rel + " import",
    )
    text = replace_once(
        text,
        '    moduleRegistry.append(\n'
        '        "GameTopLeft",\n'
        '        ModIconButton\n'
        '    );\n',
        '    moduleRegistry.append(\n'
        '        "GameTopLeft",\n'
        '        ModIconButton\n'
        '    );\n\n'
        '    moduleRegistry.append(\n'
        '        "Editor",\n'
        '        EditorPanelMount\n'
        '    );\n',
        rel + " register Editor",
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # TS binding for persisted Eye state.
    # ------------------------------------------------------------------
    rel = "HoverColors/UI/src/panel/bindings/MochiPanelBindings.ts"
    text = load(rel)
    text = replace_once(
        text,
        'export const panelCollapsed$ = bindValue<boolean>(CHANNEL, "PanelCollapsed", false);\n'
        'export const useDarkerPanel$ = bindValue<boolean>(CHANNEL, "UseDarkerPanel", false);\n',
        'export const panelCollapsed$ = bindValue<boolean>(CHANNEL, "PanelCollapsed", false);\n'
        'export const hoverHighlightsSuppressed$ = bindValue<boolean>(CHANNEL, "HoverHighlightsSuppressed", false);\n'
        'export const useDarkerPanel$ = bindValue<boolean>(CHANNEL, "UseDarkerPanel", false);\n',
        rel,
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # Central panel text lookup.
    # ------------------------------------------------------------------
    rel = "HoverColors/UI/src/panel/hooks/useMochiPanelText.ts"
    text = load(rel)
    text = replace_once(
        text,
        '            tooltipGuidelinesOpacity: l("HoverColors.UI.Tooltip.GuidelinesOpacity"),\n'
        '            tooltipInfo: tooltipsEnabled\n',
        '            tooltipGuidelinesOpacity: l("HoverColors.UI.Tooltip.GuidelinesOpacity"),\n'
        '            tooltipHoverVisibility: l("HoverColors.UI.Tooltip.HoverVisibility"),\n'
        '            tooltipInfo: tooltipsEnabled\n',
        rel,
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # English panel tooltip. Other UI JSON locales safely fall back to EN for this new key.
    # ------------------------------------------------------------------
    rel = "HoverColors/L10n/lang/en-US.json"
    text = load(rel)
    text = replace_once(
        text,
        '  "HoverColors.UI.Tooltip.CollapsePanel": "Collapse panel to outline row only.\\nClick again expands to full panel.",\n'
        '  "HoverColors.UI.Tooltip.Close": "Close panel. Default hotkey: J.\\nGame top-left icon On/Off.",\n',
        '  "HoverColors.UI.Tooltip.CollapsePanel": "Collapse panel to outline row only.\\nClick again expands to full panel.",\n'
        '  "HoverColors.UI.Tooltip.HoverVisibility": "Outline + fill On/Off.\\nOFF hides normal hover; clicked objects still use your current Hover Colors color.",\n'
        '  "HoverColors.UI.Tooltip.Close": "Close panel. Default hotkey: J.\\nGame top-left icon On/Off.",\n',
        rel,
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # Main panel: bind Eye state and put Eye button before collapse arrow.
    # ------------------------------------------------------------------
    rel = "HoverColors/UI/src/MochiColorPickerPanel.tsx"
    text = load(rel)
    text = replace_once(
        text,
        '    panelCollapsed$,\n'
        '    panelTooltipsEnabled$,\n',
        '    panelCollapsed$,\n'
        '    hoverHighlightsSuppressed$,\n'
        '    panelTooltipsEnabled$,\n',
        rel + " binding import",
    )
    text = replace_once(
        text,
        '    const useDarkerPanel = useValue(useDarkerPanel$);\n'
        '    const surfaceToolAreasSuppressed = useValue(surfaceToolAreasSuppressed$);\n',
        '    const useDarkerPanel = useValue(useDarkerPanel$);\n'
        '    const hoverHighlightsSuppressed = useValue(hoverHighlightsSuppressed$);\n'
        '    const surfaceToolAreasSuppressed = useValue(surfaceToolAreasSuppressed$);\n',
        rel + " state",
    )
    text = replace_once(
        text,
        '    const closeButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.closeButton}`;\n'
        '    const collapseButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.collapseButton}`;\n',
        '    const closeButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.closeButton}`;\n'
        '    const collapseButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.collapseButton}`;\n'
        '    const eyeButtonClass = `${roundHighlightButtonTheme["button"] ?? ""} ${styles.eyeButton}`;\n',
        rel + " button class",
    )
    text = replace_once(
        text,
        '                        <SideTooltip tooltip={tt(text.tooltipCollapse)} side="right">\n'
        '                            <Button\n'
        '                                className={collapseButtonClass}\n',
        '                        <SideTooltip tooltip={tt(text.tooltipHoverVisibility)} side="right">\n'
        '                            <Button\n'
        '                                className={eyeButtonClass}\n'
        '                                variant="icon"\n'
        '                                onSelect={() => trigger(CHANNEL, "ToggleHoverHighlights")}\n'
        '                                focusKey={focusDisabled}\n'
        '                                aria-pressed={hoverHighlightsSuppressed}\n'
        '                            >\n'
        '                                <img\n'
        '                                    src={hoverHighlightsSuppressed\n'
        '                                        ? "Media/PhotoMode/HideUIOn.svg"\n'
        '                                        : "Media/PhotoMode/HideUIOff.svg"}\n'
        '                                    className={styles.eyeIcon}\n'
        '                                    alt=""\n'
        '                                />\n'
        '                            </Button>\n'
        '                        </SideTooltip>\n\n'
        '                        <SideTooltip tooltip={tt(text.tooltipCollapse)} side="right">\n'
        '                            <Button\n'
        '                                className={collapseButtonClass}\n',
        rel + " eye button",
    )
    stage(rel, text)

    # ------------------------------------------------------------------
    # Panel styling: slightly larger title, Eye sizing, slightly tighter arrow/X gap.
    # ------------------------------------------------------------------
    rel = "HoverColors/UI/src/MochiColorPickerPanel.module.scss"
    text = load(rel)
    text = replace_once(
        text,
        '    --hc-title-font-size: 12rem;\n',
        '    --hc-title-font-size: 13rem;\n',
        rel + " title font",
    )
    text = replace_once(
        text,
        '.closeButton,\n'
        '.collapseButton {\n'
        '    flex: 0 0 24rem;\n'
        '    width: 24rem;\n'
        '    height: 24rem;\n'
        '    min-width: 24rem;\n'
        '    margin-left: 8rem;\n'
        '    padding: 0 !important;\n'
        '    overflow: visible;\n'
        '}\n\n'
        '.closeButton .closeIcon,\n'
        '.collapseButton .collapseIcon {\n',
        '.closeButton,\n'
        '.collapseButton,\n'
        '.eyeButton {\n'
        '    flex: 0 0 24rem;\n'
        '    width: 24rem;\n'
        '    height: 24rem;\n'
        '    min-width: 24rem;\n'
        '    margin-left: 6rem;\n'
        '    padding: 0 !important;\n'
        '    overflow: visible;\n'
        '}\n\n'
        '.closeButton {\n'
        '    margin-left: 5rem;\n'
        '}\n\n'
        '.closeButton .closeIcon,\n'
        '.collapseButton .collapseIcon,\n'
        '.eyeButton .eyeIcon {\n',
        rel + " buttons",
    )
    text = replace_once(
        text,
        '.collapseButton:hover .collapseIcon {\n'
        '    opacity: 1;\n'
        '    transform: scale(1.10);\n'
        '}\n\n'
        '.infoButton,\n',
        '.collapseButton:hover .collapseIcon {\n'
        '    opacity: 1;\n'
        '    transform: scale(1.10);\n'
        '}\n\n'
        '.eyeButton .eyeIcon {\n'
        '    width: 18rem;\n'
        '    height: 18rem;\n'
        '    opacity: 0.88;\n'
        '}\n\n'
        '.eyeButton:hover .eyeIcon {\n'
        '    opacity: 1;\n'
        '    transform: scale(1.10);\n'
        '}\n\n'
        '.infoButton,\n',
        rel + " eye icon",
    )
    stage(rel, text)

    # Validate new files do not already exist unless they exactly match this package.
    new_files = [
        "HoverColors/Systems/OutlineColorSystem.Visibility.cs",
        "HoverColors/Systems/HoverColorsUISystem.Visibility.cs",
        "HoverColors/UI/src/entry/EditorPanelMount.tsx",
        "HoverColors/UI/src/entry/EditorPanelMount.module.scss",
    ]
    for rel in new_files:
        src = package_dir / "new-files" / rel
        dst = repo / rel
        if not src.is_file():
            raise RuntimeError(f"package missing new file: {src}")
        if dst.exists() and dst.read_text(encoding="utf-8-sig") != src.read_text(encoding="utf-8-sig"):
            raise RuntimeError(f"{rel}: already exists with different content; refusing to overwrite")

    print("Validated addOffButton source blocks successfully.")
    print(f"Existing files to modify: {len(changed)}")
    print(f"New files to add: {len(new_files)}")

    if args.check:
        print("CHECK ONLY: no files changed.")
        return 0

    for path, content in changed.items():
        path.write_text(content, encoding="utf-8", newline="\n")

    for rel in new_files:
        src = package_dir / "new-files" / rel
        dst = repo / rel
        dst.parent.mkdir(parents=True, exist_ok=True)
        shutil.copyfile(src, dst)

    print("Applied Hover Colors phase-1 changes.")
    print("Next: build in VS2026, then review with: git diff --check && git diff")
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Exception as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        raise SystemExit(1)
