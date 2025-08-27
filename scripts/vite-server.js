export function configureServer() {
  return {
    server: {
      proxy: {
        "/api": {
          target: "http://localhost:49752",
          changeOrigin: true,
          secure: false,
          rewrite: (path) => path.replace(/^\/api/, ""),
        },
      },
    },
  };
}
