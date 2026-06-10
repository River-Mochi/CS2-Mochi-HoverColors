# File: Scripts/FixDashedGuideLocaleEntries.ps1
# Purpose: Remove old dashed-guide Options dropdown locale entries after moving color to the in-city panel.

$ErrorActionPreference = "Stop"

$scriptPath = $MyInvocation.MyCommand.Path
$scriptDir = Split-Path -Parent $scriptPath
$modRoot = Split-Path -Parent $scriptDir

$utf8NoBom = [System.Text.UTF8Encoding]::new($false)

# Keep Setting.cs using comments short and make sure FileLocation still compiles.
$settingPath = Join-Path $modRoot "Settings\Setting.cs"
if (Test-Path $settingPath) {
    $text = [System.IO.File]::ReadAllText($settingPath, $utf8NoBom)

    if ($text -notmatch "using\s+Colossal\.IO\.AssetDatabase;") {
        $text = $text -replace "(?m)^\s*using\s+Game\.Input;.*$", "    using Colossal.IO.AssetDatabase; // FileLocation.`r`n    using Game.Input;       // BindingKeyboard."
    }

    $text = $text -replace "(?m)^\s*using\s+Colossal\.IO\.AssetDatabase;.*$", "    using Colossal.IO.AssetDatabase; // FileLocation."
    $text = $text -replace "(?m)^\s*using\s+Game\.Input;.*$", "    using Game.Input;       // BindingKeyboard."
    $text = $text -replace "(?m)^\s*using\s+Game\.Modding;.*$", "    using Game.Modding;     // IMod."
    $text = $text -replace "(?m)^\s*using\s+Game\.Settings;.*$", "    using Game.Settings;    // ModSetting, attributes."
    $text = $text -replace "(?m)^\s*using\s+Game\.UI;.*$", "    using Game.UI;          // ProxyBinding."
    $text = $text -replace "(?m)^\s*using\s+Game\.UI\.Widgets;.*$", "    using Game.UI.Widgets;  // Unit.kPercentage."

    [System.IO.File]::WriteAllText($settingPath, $text, $utf8NoBom)
}

$localeDir = Join-Path $modRoot "Localization"
if (!(Test-Path $localeDir)) {
    throw "Could not find Localization folder at: $localeDir"
}

$files = Get-ChildItem -Path $localeDir -Filter "Locale*.cs" -File
foreach ($file in $files) {
    $text = [System.IO.File]::ReadAllText($file.FullName, $utf8NoBom)
    $original = $text

    # Remove the old section comment if it is now orphaned.
    $text = [System.Text.RegularExpressions.Regex]::Replace(
        $text,
        "(?m)^\s*//\s*Dashed alignment guide color\s*\r?\n",
        "")

    # Remove old dictionary entries for the removed Options dropdown.
    # This works even if a bad earlier script left the entry on the same line as the previous entry.
    $patterns = @(
        '\{\s*m_Settings\.GetOptionLabelLocaleID\(nameof\(HoverColorsSettings\.GuidelineDashedColorPreset\)\).*?\},\s*',
        '\{\s*m_Settings\.GetOptionDescLocaleID\(nameof\(HoverColorsSettings\.GuidelineDashedColorPreset\)\).*?\},\s*',
        '\{\s*m_Settings\.GetGuidelineDashedColorPresetLocaleID\("[^"]+"\).*?\},\s*'
    )

    foreach ($pattern in $patterns) {
        $text = [System.Text.RegularExpressions.Regex]::Replace(
            $text,
            $pattern,
            "",
            [System.Text.RegularExpressions.RegexOptions]::Singleline)
    }

    # Clean spacing where a removed entry was glued to the next section comment.
    $text = $text -replace "(?m)\s+// Guidelines opacity slider", "`r`n                // Guidelines opacity slider"

    if ($text -ne $original) {
        [System.IO.File]::WriteAllText($file.FullName, $text, $utf8NoBom)
        Write-Host "Cleaned $($file.Name)"
    }
}

Write-Host "Done. Check should return no Localization hits:"
Write-Host "  grep -R \"GetGuidelineDashedColorPresetLocaleID\|GuidelineDashedColorPreset\" HoverColors/Localization"
