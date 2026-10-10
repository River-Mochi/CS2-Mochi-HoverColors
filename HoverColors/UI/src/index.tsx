// <copyright file="index.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: UI/src/index.tsx
// Purpose: Mod entry point registered with the cs2/modding registry.
//   - Wires the VanillaComponentResolver so all vanilla components resolve once on load.
//   - Registers selectable city launcher locations and mounts the same panel in Editor.
// webpack entry point; only add module-level side effects here.

import { useValue } from "cs2/api";
import { ModRegistrar } from "cs2/modding";
import { MochiColorPickerPanel } from "./MochiColorPickerPanel";
import { launcherLocation$, panelOpen$ } from "./panel/bindings/MochiPanelBindings";
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

const LauncherAt = ({ location }: { location: number }) => {
  const requestedLocation = useValue(launcherLocation$);
  // A missing or invalid saved choice leaves the launcher at its default location.
  const selectedLocation = requestedLocation === 1 || requestedLocation === 2 ? requestedLocation : 0;
  return selectedLocation === location ? <ModIconButton location={location} /> : null;
};

const TopLeftLauncher = () => <LauncherAt location={0} />;
const TopRightLauncher = () => <LauncherAt location={1} />;
// Keep an entry in the game's Mods menu as a fallback if a corner hook is not mounted.
const UniversalMenuLauncher = () => <ModIconButton location={2} />;

const register: ModRegistrar = (moduleRegistry) => {
  VanillaComponentResolver.setRegistry(moduleRegistry);

  // Register each host once. The binding chooses the corner live, while Universal
  // remains available alongside it when the saved choice is a corner.
  moduleRegistry.append("GameTopLeft", TopLeftLauncher);
  moduleRegistry.append("GameTopRight", TopRightLauncher);
  // The general Mods menu remains useful if the game's corner hook is unavailable.
  moduleRegistry.append("UniversalModMenu", UniversalMenuLauncher);

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
