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
    "Editor",
    EditorPanelEntry
  );
};

export default register;
