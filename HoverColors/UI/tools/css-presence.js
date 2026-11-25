const { RawSource } = require("webpack").sources;

exports.CSSPresencePlugin = class CSSPresencePlugin {
  apply(compiler) {
    compiler.hooks.compilation.tap("CSSPresencePlugin", (compilation) => {
      compilation.hooks.processAssets.tap(
        {
          name: "CSSPresencePlugin",
          stage: compilation.PROCESS_ASSETS_STAGE_ADDITIONS,
        },
        () => {
          const cssFiles = Object.keys(compilation.assets).filter((asset) =>
            asset.endsWith(".css")
          );
          const hasCSS = cssFiles.length > 0;

          for (const file of cssFiles) {
            const css = String(compilation.getAsset(file).source.source())
              .replace(/\/\*[\s\S]*?\*\//g, "");
            const unsupported = css.match(/:not\s*\(/);
            const shorthand = css.match(/(?:^|[;{])\s*(?:border(?:-(?:top|right|bottom|left|color|width|style|radius))?|margin|padding|background|flex|transition|overflow|gap|font|animation|inset|outline)\s*:/);
            if (unsupported || shorthand) {
              compilation.errors.push(new Error(
                `HC CSS compatibility: ${file} contains ${(unsupported || shorthand)[0].trim()}. Use explicit supported properties/selectors.`
              ));
            }
          }

          // Inject the `hasCSS` export into the main module source
          for (const chunk of compilation.chunks) {
            for (const file of chunk.files) {
              if (file.endsWith(".mjs")) {
                const asset = compilation.getAsset(file);
                const source = asset.source.source();
                const updatedSource = source.replace(
                  "export {",
                  `const hasCSS = ${hasCSS}; export { hasCSS, `
                );

                // Generate a new source map for the modified source
                const newSourceAndMap = {
                  sources: [file],
                  mappings: "",
                  file,
                  sourceRoot: "",
                  sourcesContent: [updatedSource],
                };

                compilation.updateAsset(
                  file,
                  new RawSource(updatedSource, newSourceAndMap)
                );
              }
            }
          }
        }
      );
    });
  }
};
