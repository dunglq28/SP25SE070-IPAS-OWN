import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";
import path from "path";
import svgr from "vite-plugin-svgr";

export default defineConfig({
  plugins: [react(), svgr()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
  server: {
    watch: {
      usePolling: true,
    },
    headers: {
      "Cross-Origin-Opener-Policy": "same-origin-allow-popups",
      "Cross-Origin-Embedder-Policy": "require-corp",
    },
  },
  build: {
    // Tăng giới hạn kích thước chunk cảnh báo (KB)
    chunkSizeWarningLimit: 1000, // 1000 KB (hoặc giá trị bạn muốn)

    rollupOptions: {
      output: {
        // Thủ công chia các thư viện thành chunk riêng biệt
        manualChunks(id) {
          if (id.includes("node_modules/react")) {
            return "react-vendor"; // Chia react thành một chunk riêng
          }
          if (id.includes("node_modules/lodash")) {
            return "lodash-vendor"; // Chia lodash thành một chunk riêng
          }
        },
      },
    },
  },
});
