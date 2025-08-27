import path from "node:path";

export function configureCss() {
  return {
    css: {
      modules: {
        localsConvention: "camelCase",
        generateScopedName(className, file) {
          const parts = [
            "tf", // Prefix for all class names
          ];

          const filePath = file.split("?")[0];
          const fileName = path.basename(filePath, ".module.css");
          const foldersPath = path.dirname(filePath).split(path.sep);
          const folderName = foldersPath[foldersPath.length - 1];

          const componentName = fileName !== "root" ? fileName : folderName;

          parts.push(componentName);

          if (className !== "root") {
            parts.push(className);
          }

          return parts.join("-");
        },
      },
    },
  };
}
