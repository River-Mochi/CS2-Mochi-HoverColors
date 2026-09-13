// <copyright file="ModIconButton.tsx" company="River-Mochi">
// Copyright (C) 2026 River-Mochi.
// Licensed under the GNU General Public License v3.0 or later,
// with the Cities: Skylines II Linking Exception.
// See LICENSE and LICENSE-EXCEPTION in the project root.
// Copyright and license notices MUST be preserved.
// ================= </copyright> ======================

// File: UI/src/entry/ModIconButton.tsx
// GameTopLeft launcher for the Hover Colors in-city panel.

import { trigger, useValue } from "cs2/api";
import { Button, Tooltip } from "cs2/ui";
import { usePanelLocalization } from "../localization";
import { CHANNEL, panelOpen$ } from "../panel/bindings/MochiPanelBindings";
import styles from "./ModIconButton.module.scss";

// SVG passed via Button.src so its own fills render (single color today, multi-color future).
import ModIconPath from "../../images/icon-GTL1.svg";

export default () => {
  const isOpen = useValue(panelOpen$);
  const translatePanel = usePanelLocalization();
  const tooltip = translatePanel("HoverColors.UI.TopLeft.Tooltip");

  return (
    // The panel itself mounts at the "Game" root, not here, so it is not trapped in this
    // container's stacking context. data-hc-launcher is how the panel finds this button to open
    // underneath it; the attribute is read only, never written to.
    <div className={styles.anchor} data-hc-launcher="true">
      <Tooltip tooltip={tooltip}>
        <Button
          variant="floating"
          src={ModIconPath}
          // No selected prop: hover lightens, but open panel does not keep the GTL icon tinted.
          onSelect={() => trigger(CHANNEL, "SetPanelOpen", !isOpen)}
        />
      </Tooltip>
    </div>
  );
};
