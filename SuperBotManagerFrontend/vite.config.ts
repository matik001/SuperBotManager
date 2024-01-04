import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';
import tsconfigPaths from 'vite-tsconfig-paths';

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [react(), tsconfigPaths()],
	build: {
		outDir: './dist',
		sourcemap: false, //// mozna wylaczyc
		emptyOutDir: true
	},
	server: {
		open: '/',
		proxy: {
			'/api': {
				target: 'http://127.0.0.1:6662',
				changeOrigin: true
			},
			'/hub': {
				target: 'http://127.0.0.1:6662',
				changeOrigin: true,
				ws: true
			}
		}
	}
});
