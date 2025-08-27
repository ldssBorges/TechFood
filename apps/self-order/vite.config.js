import { defineConfig } from "vite";
import { VitePWA } from "vite-plugin-pwa";
import react from "@vitejs/plugin-react";
import { configureCss, configureServer } from "../../scripts";

export default defineConfig({
  base: "/self-order",
  ...configureCss(),
  ...configureServer(),
  plugins: [
    react(),
    VitePWA({
      registerType: "autoUpdate",
      manifest: false,
    }),
  ],
});
