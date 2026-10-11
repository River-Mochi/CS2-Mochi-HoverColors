// <copyright file="index.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: UI/src/index.tsx
// Purpose: Mod entry point registered with the cs2/modding registry.
//   - Wires the VanillaComponentResolver once on load.
//   - Registers Game + Editor panel hosts.
//   - Registers GTL, GTR and Universal independently so one failed host cannot block another.

import { trigger, useValue } from "cs2/api";
import { ModRegistrar } from "cs2/modding";
import { useEffect } from "react";
import { MochiColorPickerPanel } from "./MochiColorPickerPanel";
import { CHANNEL, launcherLocation$, panelOpen$ } from "./panel/bindings/MochiPanelBindings";
import { VanillaComponentResolver } from "./utils/vanilla/VanillaComponentResolver";
import "./MochiColorPickerPanel.global.scss";

import ModIconButton from "./entry/ModIconButton";

const reportUIStatus = (detail: string) => {
  try {
    trigger(CHANNEL, "ReportUIStatus", detail);
  } catch {
    // If the C# bridge is unavailable, leave evidence in UI.log instead.
    console.log(`[HoverColors] UI: ${detail}`);
  }
};

const reportUIFailure = (host: string, error: unknown) => {
  const detail = `${host}: ${String(error)}`;

  try {
    trigger(CHANNEL, "ReportUIRegistrationFailure", detail);
  } catch {
    console.error(`[HoverColors] UI registration failed: ${detail}`);
  }
};

// The panel lives at Game root instead of inside GTL/GTR.
// This keeps J independent from whichever launcher location the player selected.
const GamePanelEntry = () => {
  const isOpen = useValue(panelOpen$);

  useEffect(() => {
    reportUIStatus("mounted:Game");
  }, []);

  // This only commits if the panel tree successfully renders.
  useEffect(() => {
    if (isOpen) {
      reportUIStatus("panel-open:Game");
    }
  }, [isOpen]);

  return isOpen ? <MochiColorPickerPanel /> : null;
};

const EditorPanelEntry = () => {
  const isOpen = useValue(panelOpen$);

  useEffect(() => {
    reportUIStatus("mounted:Editor");
  }, []);

  // Editor has no GTL/GTR/UMM launcher, so the hotkey is the access path here.
  useEffect(() => {
    if (isOpen) {
      reportUIStatus("panel-open:Editor");
    }
  }, [isOpen]);

  return isOpen ? <MochiColorPickerPanel editorMode /> : null;
};

const LauncherAt = ({ location }: { location: number }) => {
  const requestedLocation = useValue(launcherLocation$);

  // Unknown/missing values always fall back to GTL.
  const selectedLocation =
    requestedLocation === 1 || requestedLocation === 2
      ? requestedLocation
      : 0;

  const hostName =
    location === 1
      ? "GameTopRight"
      : "GameTopLeft";

  useEffect(() => {
    reportUIStatus(`mounted:${hostName}`);
  }, [hostName]);

  // If this appears in HoverColors.log, React successfully committed the selected launcher.
  useEffect(() => {
    if (selectedLocation === location) {
      reportUIStatus(`launcher-visible:${hostName}`);
    }
  }, [hostName, location, selectedLocation]);

  return selectedLocation === location
    ? <ModIconButton location={location} />
    : null;
};

const TopLeftLauncher = () => <LauncherAt location={0} />;

const TopRightLauncher = () => <LauncherAt location={1} />;

// Universal is intentionally always registered. When GTL/GTR is selected it is
// a second tidy-menu access point; when Universal-only is selected it is the only icon.
const UniversalMenuLauncher = () => {
  useEffect(() => {
    // UMM contents may not mount until the player actually opens the Universal Mod Menu.
    reportUIStatus("launcher-visible:UniversalModMenu");
  }, []);

  return <ModIconButton location={2} />;
};

const register: ModRegistrar = (moduleRegistry) => {
  reportUIStatus("registrar-start");

  // Resolver failure should not stop the simple launcher buttons from registering.
  try {
    VanillaComponentResolver.setRegistry(moduleRegistry);
    reportUIStatus("resolver-ready");
  } catch (error) {
    reportUIFailure("VanillaComponentResolver", error);
  }

  // Each host is deliberately isolated. GTL/GTR/UMM failure must never block
  // Game or Editor, because the hotkey panel must still work.
  try {
    moduleRegistry.append("Game", GamePanelEntry);
    reportUIStatus("registered:Game");
  } catch (error) {
    reportUIFailure("Game", error);
  }

  try {
    moduleRegistry.append("Editor", EditorPanelEntry);
    reportUIStatus("registered:Editor");
  } catch (error) {
    reportUIFailure("Editor", error);
  }

  try {
    moduleRegistry.append("GameTopLeft", TopLeftLauncher);
    reportUIStatus("registered:GameTopLeft");
  } catch (error) {
    reportUIFailure("GameTopLeft", error);
  }

  try {
    moduleRegistry.append("GameTopRight", TopRightLauncher);
    reportUIStatus("registered:GameTopRight");
  } catch (error) {
    reportUIFailure("GameTopRight", error);
  }

  try {
    moduleRegistry.append("UniversalModMenu", UniversalMenuLauncher);
    reportUIStatus("registered:UniversalModMenu");
  } catch (error) {
    reportUIFailure("UniversalModMenu", error);
  }

  reportUIStatus("registrar-complete");
};

export default register;
