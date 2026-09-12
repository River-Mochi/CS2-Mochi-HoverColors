// File: UI/src/index.tsx
// Purpose: Mod entry point registered with the cs2/modding registry.
//   - Wires the VanillaComponentResolver so all vanilla components resolve once on load.
//   - Appends city button to GameTopLeft and mounts the same panel in Editor.
// webpack entry point; only add module-level side effects here.

import { useValue } from "cs2/api";
import { ModRegistrar } from "cs2/modding";
import { MochiColorPickerPanel } from "./MochiColorPickerPanel";
import { panelOpen$ } from "./panel/bindings/MochiPanelBindings";
import { VanillaComponentResolver } from "./utils/vanilla/VanillaComponentResolver";
import "./MochiColorPickerPanel.global.scss";

import ModIconButton from "./entry/ModIconButton";

// The panel mounts at the "Game" root rather than inside the GameTopLeft launcher. Nested in
// GameTopLeft it shared that container's stacking context, so vanilla panels the player dragged over
// it painted on top no matter what z-index the panel carried. All Speed Limits appends its own
// window to "Game" for the same reason. The launcher keeps only the button.
const GamePanelEntry = () => {
  const isOpen = useValue(panelOpen$);
  return isOpen ? <MochiColorPickerPanel /> : null;
};

const EditorPanelEntry = () => {
  const isOpen = useValue(panelOpen$);
  return isOpen ? <MochiColorPickerPanel editorMode /> : null;
};

const register: ModRegistrar = (moduleRegistry) => {
  VanillaComponentResolver.setRegistry(moduleRegistry);

  moduleRegistry.append(
    "GameTopLeft",
    ModIconButton
  );

  moduleRegistry.append(
    "Game",
    GamePanelEntry
  );

  moduleRegistry.append(
    "Editor",
    EditorPanelEntry
  );
};

export default register;
