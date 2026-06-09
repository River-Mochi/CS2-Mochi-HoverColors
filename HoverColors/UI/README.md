# HoverColors UI SCSS cleanup split

This ZIP replaces the current large override-piled SCSS with a cleaner split.

## Files to copy into HoverColors/UI

Copy/overwrite these paths:

```text
src/MochiColorPickerPanel.module.scss
src/MochiColorPickerPanel.tsx
src/panel/MochiPanelPieces.tsx
src/panel/MochiPanelBindings.ts
src/panel/_presetButtons.scss
src/panel/_dragGrip.scss
src/panel/_activeIndicators.scss
tsconfig.json
```

## What changed

- `MochiColorPickerPanel.module.scss` is reduced from ~1700 lines to ~850 lines.
- Preset button styles moved to `src/panel/_presetButtons.scss`.
- Drag foot styles moved to `src/panel/_dragGrip.scss`.
- Active/toggle bar styles moved to `src/panel/_activeIndicators.scss`.
- The old bottom override stack is removed.
- Drag grip dots are removed from `MochiPanelPieces.tsx`.

## Build

From `HoverColors/UI`:

```bash
npm run build
```

If build passes:

```bash
git status -sb
git add src/MochiColorPickerPanel.module.scss src/MochiColorPickerPanel.tsx src/panel/MochiPanelPieces.tsx src/panel/MochiPanelBindings.ts src/panel/_presetButtons.scss src/panel/_dragGrip.scss src/panel/_activeIndicators.scss tsconfig.json
git commit -m "split compact panel styles"
git push
```
