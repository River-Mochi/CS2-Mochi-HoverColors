# Road Info Panel and precise measurements — development plan

Status: design starter, not implemented or tested.
Branch: feat/info-panel-precise
Base: 14c8f2c6c00a02ca3068d9141a716d477189c349 (feat/road-clutter-phase2, before final stability/compatibility testing).
Game reference: Cities: Skylines II 1.6.4f1, decomp-game d8b31884b303e2b9d25914e67a6f9089bab7f83a.

## Scope and release separation

Finish and release the existing Road Clutter Filter (RCF) independently if needed. Do not add new measurement or React work to the branch under active player test. Bring final Phase 2 fixes into this branch before integration (if Phase 2 is squash-merged to main, transplant only the new feature commits onto that released baseline). No version bump yet.

Preserve the current L binding and serialized RoadAngleTooltipsHidden setting; no new required dependency or Harmony patch. Road/path construction only, excluding Replace. Work in Game and Editor. Do not touch generated UI/types.

## Options UI

New tab: Road Clutter Filter. Move the existing ToggleRoadAngleTooltipsBinding to the top of this tab, retaining the action name, persisted binding and default L. Proposed controls:
- Distance decimals: 0, 1, 2; default 2.
- Angle decimals: 0, 1, 2; default 2.
- Road Info Panel: checkbox, default ON.
- No separate Enable Float switches. 0 = keep vanilla formatting on floating tooltips; in particular vanilla guide lengths can show half metres and slope already has one decimal. Do not force integer rounding as a side effect of disabling precision.
- Positive decimal choices enable precise display only when RCF is ON. No new floating tooltips with RCF OFF. Panel is independently enabled, and can show measurements with the selected precision even when RCF is OFF. This is a proposed product decision, to verify with the author.
- Existing angle/guide color controls remain independent of RCF.
- Add matching native Options localization entries to every Locale*.cs, using LocaleEN.cs as source. Add matching panel strings to every existing UI L10n/lang/*.json. Keep labels short. Cover defaults, saved settings migration, reset and live changes. RCF remains OFF by default unless author changes that decision.

## One label per measurement

Vanilla GuideLinesSystem rounds angles to integers and guide distances to half metres before GuideLineTooltipSystem consumes TooltipInfo. Formatting those rounded values with extra zeros is not genuine precision.

Use a shared, read-only snapshot of the active preview:
1. Recover intermediate angle and guide length from the correct raw or generated control points. Reuse the existing PI geometry calculation/verification, including special Continuous geometry where appropriate. Match native type/index/world anchor and value, not rounded angle alone. Recover start/snap angles only from verified native direction selection; if uncertain, preserve vanilla. Never generate missing measurements or invent decimals.
2. Course length uses the horizontal length of the eligible cut preview curves, as in vanilla NetCourseTooltipSystem. Its existing FloatTooltip value is already a float; ET may convert it to U. For precise slope, verify endpoint order/chain before recomputing signed rise divided by horizontal course length; vanilla zeros values below 0.05%, so formatting it cannot restore lost detail. Avoid duplicate queries; reuse snapshot data for panel and floating tooltips.
3. Run precision formatting after existing native tooltip recognition, RCF relocation and layout (which identify FloatTooltip). Prefer supported native numeric formatting where available. If a string widget is required, use a transient presentation replacement preserving icon, group/path/position/category and unit semantics; do not overwrite the cached vanilla/ET FloatTooltip in its persistent group without restoring it. Never append a second [P] tooltip. Verify OFF/ON and unload restoration.
4. Preserve Extended Tooltip metres/U conversion (1 U = 8 m), sign, locale, and unrelated warnings. Do not hard-code metres for U. Keep unsupported angles or units in the original native format rather than misrepresenting precision. Recheck label width with 2 decimals and U. Test installed Road Precision separately; do not suppress foreign mod widgets indiscriminately. Document overlapping feature compatibility if needed.

## Road Info Panel

Use HC's existing CS2 React toolchain, separate small component in the Game and Editor root hosts (UI/src/index.tsx), independent of the color picker panel-open binding and launcher choice. Anchor visually under the top-right game buttons, not inside GameTopRight's horizontal button strip. In Editor anchor in equivalent safe top-right location, allowing for editor chrome. Use existing theme conventions, no focusable controls or pointer interception for read-only text. Hide when disabled, not actively drawing, switching tools, Replace, or no valid preview. Show without needing to open HC. Confirm safe margins with UI scale and other mods.

Proposed compact layout (not final art):
- Road Info
- Length: horizontal road-course length (native definition)
- Distance: straight horizontal start-to-end distance
- Elevation: current endpoint world elevation, with reference defined and tested (not height above terrain unless explicitly calculated)
- Relief: signed end minus start elevation (label Rise/Fall if clearer)
- Slope: signed rise / horizontal course length
- Direction: map-relative bearing and compass sector, based on start-to-end displacement, not camera rotation; verify north/axis conventions in game, show unavailable for near-zero displacement.
- Starting angles: show both distinct reference angles if native provides both, including when start is off screen.
- PI angles and guide distances: optional compact rows in native guide order, label by role/index (not just a flat list of indistinguishable angles). Keep total length distinct from guide distances.

Populate from native preview measurements before screen-space clipping/layout, not only from visible tooltip groups. Use read-only data, no road edits. No stale data when a preview ends. Use existing C# UISystem bindings and React useValue; do not require opening HC UI. Gate work before expensive queries when RCF and panel both inactive, update only during valid road/path preview, reuse bounded data and only publish changed formatted values. No periodic city-wide scan. Game and Editor use appropriate host lifecycle; test load, reload, cancel, tool switch, paused simulation.

## PI stability acceptance criteria

Prioritize stable placement over perfection. Retain a preferred outside side for each verified PI during the same active drag; use a modest deadband/hysteresis rather than recomputing above/below on small movement. Follow the actual circle without stale coordinates. Change sides only for meaningful hard obstruction or clipping; try a small outward/sideways move first. Invalidate state on mode/tool/preview/point identity changes. If current Phase 2 tests show unacceptable jumping, retain native positioning for the problematic mode instead of adding endless heuristics. No measurement deletion. Apply this policy to Simple, Complex and Continuous after reviewing current test recordings. Do not silently broaden Phase 2 tolerance or change geometry.

## References reviewed

- HC: https://github.com/River-Mochi/CS2-Mochi-HoverColors/tree/14c8f2c6c00a02ca3068d9141a716d477189c349
  - HoverColors/Systems/Rendering/RoadAngleTooltipFilterSystem.cs, .MiddleAngle.cs, .Layout.cs
  - HoverColors/Settings/Setting.cs (existing Actions, KeyBindings, About tabs; L binding)
  - HoverColors/Localization/LocaleEN.cs, HoverColors/UI/src/index.tsx (Game/Editor roots and separate launchers)
- Vanilla https://github.com/River-Mochi/decomp-game/tree/d8b31884b303
  - Game.Rendering/GuideLinesSystem.cs: DrawControlPoints and DrawAngleIndicator (includes generated Continuous guide positions)
  - Game.UI.Tooltip/GuideLineTooltipSystem.cs: native group path guideLineTooltip{index}, FloatTooltip and units
  - Game.UI.Tooltip/NetCourseTooltipSystem.cs: eligible CreationDefinition + NetCourse query, horizontal course length, slope, tempNetEdgeStart group, mouse fallback
  - Game.UI.Tooltip/TooltipUISystem.cs: GameOrEditor, clears lists and runs UITooltip before serialization. May skip phase when mouse over UI or tooltips hidden; independent summary must clear stale state.
  - Game.UI.Tooltip/NumberTooltip.cs: value, unit, signed properties; formatting support needs validation in UI.
- Road Precision (MIT, reviewed main at 7aca5530cdbbadeef2573d75114d5bfb2a32dc26): https://github.com/gadeleon/road_precision
  - Separate precision systems add duplicate [P] tooltips; course tooltip offset 300 px; angle matching by rounded values; sliders 0–4 plus Float checkboxes. Reference only; no code copied.
  - PR #17 https://github.com/gadeleon/road_precision/pull/17 open/unmerged when checked 2026-11-25, head 77f4836c6c386f13dc96ab3f4f7d305471d2dbf5. Proposes fixed top-left native TooltipGroup summary; registers PrecisionSummaryPanelSystem but implementation file was not found in its head tree. Do not assume released functionality.
- Extended Tooltip: https://github.com/Mimonsi/cities2-extended-tooltip/blob/650c5115c5e63f6244ab879f0e1405f0ee72758c/ExtendedTooltip/Patches.cs
  - Uses native guideLineTooltip paths and FloatTooltip, changes lengths to floatTwoFractions with U label; RCF must preserve those semantics and its existing mode-hint behavior.
- CS1 Measure It! UI inspiration supplied by author: https://steamcommunity.com/sharedfiles/filedetails/?id=1768810491

## Implementation order / tests

A. Settings and localization; shared snapshot and identity checks; implement native/precise in-place presentation with no duplicates and valid zero precision.
B. Info panel and top-right placement; direction convention verification; panel visibility/clear logic, Game/Editor, offscreen start.
C. Combine with final Phase 2 stable layout, then one combined Release test: 0/1/2 decimals, straight/simple/complex/continuous, near edges, long offscreen start, camera rotation, steep/flat and short roads, mouse movement, RCF/panel independent toggles, ET units, warnings, settings persistence, UI logs, Editor. Compare no-tool overhead; no unsupported precision, duplicate labels or stale panel. Update public documentation only after passing.
