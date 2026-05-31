# Hover Colors [HC]

Recolor hover highlights, reduce visual clutter, and make area tools easier to see in Cities: Skylines II.

- Pick an outline color with the vanilla color picker, including alpha.
- Adjust hover fill opacity and guideline opacity from a compact in-game panel.
- Change District overlay/border color and opacity with a compact color picker.
- Hide Surface tool preview fill so layered surfaces can be judged without the cloudy overlay.
- Keep player colors and presets saved between sessions in `ModsSettings/HoverColors/HoverColors.coc`.
- No Harmony patches.

## How It Works

Hover Colors uses the game's own systems where possible: ECS systems, prefab color data, `RenderingSettingsData`, and the HDRP `OutlinesWorldUIPass` material values. The visible vanilla blue hover look is not one RGBA field, so the mod captures/restores the relevant vanilla profile instead of guessing a single hard-coded color.

The Surface preview toggle adjusts the Area tool's required area mask while active, then restores it when disabled or when the tool changes.

## License

MIT - see [LICENSE](LICENSE).
