// Remembers which launcher opened the panel when a corner and Universal both show it.
// Universal may close as soon as its button is clicked, so keep its last measured bounds
// briefly for the panel's first layout. A later hotkey open uses a mounted launcher.
type LauncherBounds = { left: number; right: number; bottom: number; width: number; height: number };

let lastLauncherLocation: number | null = null;
let lastLauncherBounds: LauncherBounds | null = null;
let lastLauncherClickTime = 0;

export const rememberLauncherLocation = (location: number, element: HTMLElement | null) => {
  lastLauncherLocation = location;
  const rect = element?.getBoundingClientRect();
  lastLauncherBounds = rect == null ? null : {
    left: rect.left,
    right: rect.right,
    bottom: rect.bottom,
    width: rect.width,
    height: rect.height,
  };
  lastLauncherClickTime = Date.now();
};

export const getLastLauncherLocation = () => lastLauncherLocation;

export const getRecentLauncherBounds = (location: number): LauncherBounds | null =>
  lastLauncherLocation === location && Date.now() - lastLauncherClickTime < 1000
    ? lastLauncherBounds
    : null;
