from pathlib import Path
import json
import re

root = Path.cwd()

# 1) Setting.cs: add persistent backup fields for the non-destructive defaults toggle.
path = root / "HoverColors/Settings/Setting.cs"
text = path.read_text(encoding="utf-8")
if "PresetDefaultsToggleActive" not in text:
    marker = "        public int PresetAlt2GuidelinePercent { get; set; }\n"
    insert = """

        // Non-destructive preset defaults toggle.
        // Reset temporarily shows the four mod default colors; second click restores this backup.
        [SettingsUIHidden]
        public bool PresetDefaultsToggleActive { get; set; }

        [SettingsUIHidden]
        public bool PresetDefaultsToggleHasBackup { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackupActiveSet { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup1FillA { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackup1GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackup2FillA { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackup2GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt1FillA { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackupAlt1GuidelinePercent { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2R { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2G { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2B { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2A { get; set; }

        [SettingsUIHidden]
        public float PresetDefaultsBackupAlt2FillA { get; set; }

        [SettingsUIHidden]
        public int PresetDefaultsBackupAlt2GuidelinePercent { get; set; }
"""
    if marker not in text:
        raise SystemExit("Setting.cs anchor not found")
    text = text.replace(marker, marker + insert, 1)
path.write_text(text, encoding="utf-8", newline="\n")

# 2) Setting.Defaults.cs: default and migration cleanup for backup state.
path = root / "HoverColors/Settings/Setting.Defaults.cs"
text = path.read_text(encoding="utf-8")
if "PresetDefaultsToggleActive = false;" not in text:
    marker = "            PresetAlt2GuidelinePercent = kDefaultGuidelineOpacityPercent;\n"
    insert = """

            PresetDefaultsToggleActive = false;
            PresetDefaultsToggleHasBackup = false;
            PresetDefaultsBackupActiveSet = kPresetSetA;
"""
    if marker not in text:
        raise SystemExit("Setting.Defaults.cs defaults anchor not found")
    text = text.replace(marker, marker + insert, 1)
if "PresetDefaultsToggleActive && !PresetDefaultsToggleHasBackup" not in text:
    marker = """            if (ActivePresetSet != kPresetSetA && ActivePresetSet != kPresetSetB)
            {
                ActivePresetSet = kPresetSetA;
                changed = true;
            }
"""
    insert = """

            if (PresetDefaultsToggleActive && !PresetDefaultsToggleHasBackup)
            {
                PresetDefaultsToggleActive = false;
                changed = true;
            }

            if (PresetDefaultsBackupActiveSet != kPresetSetA && PresetDefaultsBackupActiveSet != kPresetSetB)
            {
                PresetDefaultsBackupActiveSet = kPresetSetA;
                changed = true;
            }
"""
    if marker not in text:
        raise SystemExit("Setting.Defaults.cs migration anchor not found")
    text = text.replace(marker, marker + insert, 1)
path.write_text(text, encoding="utf-8", newline="\n")

# 3) MochiColorPickerPanel.tsx: remove two-click confirm wiring if it exists.
path = root / "HoverColors/UI/src/MochiColorPickerPanel.tsx"
text = path.read_text(encoding="utf-8")
text = re.sub(r"\n\s*const \[restorePresetsArmed, setRestorePresetsArmed\] = React\.useState\(false\);", "", text)
text = re.sub(r"\n\s*const restorePresetsTimerRef = React\.useRef<number \| null>\(null\);", "", text)
text = re.sub(r"\n\s*React\.useEffect\(\(\) => \{\n\s*return \(\) => \{\n\s*if \(restorePresetsTimerRef\.current !== null\) \{\n\s*window\.clearTimeout\(restorePresetsTimerRef\.current\);\n\s*}\n\s*};\n\s*}, \[\]\);", "", text, flags=re.S)
# Replace callback confirm handler block with direct simple handlers.
text = re.sub(
    r"\n\s*const clearRestorePresetConfirm = React\.useCallback\(\(\) => \{.*?\n\s*}, \[clearRestorePresetConfirm, restorePresetsArmed\]\);",
    '\n    const handleTogglePresetDefaults = () => trigger(CHANNEL, "TogglePresetDefaults");\n    const handleRestorePresetDefaults = () => trigger(CHANNEL, "RestorePresetDefaults");',
    text,
    flags=re.S,
)
text = text.replace("                        restorePresetsArmed={restorePresetsArmed}\n", "")
path.write_text(text, encoding="utf-8", newline="\n")

# 4) useMochiPanelText.ts: remove unused confirm key if it exists.
path = root / "HoverColors/UI/src/panel/hooks/useMochiPanelText.ts"
text = path.read_text(encoding="utf-8")
text = text.replace('            tooltipRestorePresetDefaultsConfirm: l("HoverColors.UI.Tooltip.RestorePresetDefaultsConfirm"),\n', '')
path.write_text(text, encoding="utf-8", newline="\n")

# 5) Module SCSS: close icon dimmer, collapse arrow less fat, bigger gap between switch/reset.
path = root / "HoverColors/UI/src/MochiColorPickerPanel.module.scss"
text = path.read_text(encoding="utf-8")
text = re.sub(r"\.closeIcon \{.*?\n\}", ".closeIcon {\n    width: 18rem;\n    height: 18rem;\n    display: block;\n    pointer-events: none;\n    opacity: 0.70;\n    transition: opacity 120ms ease-out, transform 120ms ease-out;\n}\n\n.closeButton:hover .closeIcon {\n    opacity: 1;\n}", text, count=1, flags=re.S)
text = re.sub(r"\.collapseIcon \{.*?\n\}", ".collapseIcon {\n    width: 17rem;\n    height: 17rem;\n    display: block;\n    pointer-events: none;\n    filter: brightness(0) invert(1) opacity(0.92);\n}", text, count=1, flags=re.S)
text = re.sub(r"(\.outlineRight \{\s*display: flex;\s*align-items: center;\s*flex-shrink: 0;\s*gap: )\d+rem;", r"\g<1>4rem;", text, count=1, flags=re.S)
path.write_text(text, encoding="utf-8", newline="\n")

# 6) Preset buttons: make switch icon larger and remove duplicate bigger hover.
path = root / "HoverColors/UI/src/panel/styles/_presetButtons.scss"
text = path.read_text(encoding="utf-8")
text = re.sub(r"\.resetIconSwitch \{.*?\n\}", ".resetIconSwitch {\n  width: 21rem;\n  height: 18rem;\n  transform: none;\n}", text, count=1, flags=re.S)
# normalize all switch-hover scale rules to 1.10
text = re.sub(r"\.presetResetBare:hover \.resetIconSwitch \{\s*transform: scale\([^)]*\);\s*\}", ".presetResetBare:hover .resetIconSwitch {\n  transform: scale(1.10);\n}", text, flags=re.S)
path.write_text(text, encoding="utf-8", newline="\n")

# 7) English panel strings. Other locales may keep older text; no build problem.
path = root / "HoverColors/L10n/lang/en-US.json"
if path.exists():
    data = json.loads(path.read_text(encoding="utf-8"))
    data["HoverColors.UI.Tooltip.ResetPresets"] = "⇔ Switch between Set A 1+2 and Set B 1+2.\nEach set has P1 + P2. Hold down P1 or P2 to save over them."
    data["HoverColors.UI.Tooltip.RestorePresetDefaults"] = "↺ Toggle mod default presets / your saved presets.\nClick again to restore your saved Set A and Set B colors."
    data.pop("HoverColors.UI.Tooltip.RestorePresetDefaultsConfirm", None)
    path.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8", newline="\n")

print("Done. Non-destructive preset defaults toggle patch applied.")
