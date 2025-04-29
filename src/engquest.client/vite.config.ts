import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import mdx from '@mdx-js/rollup';

import { fileURLToPath, URL } from 'node:url';

import fs from 'fs';
import path from 'path';
import child_process from 'child_process';

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateName = "engquest.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);

const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (0 !== child_process.spawnSync('dotnet', [
    'dev-certs',
    'https',
    '--export-path',
    certFilePath,
    '--format',
    'Pem',
    '--no-password',
  ], { stdio: 'inherit', }).status) {
    throw new Error("Could not create certificate.");
  }
}

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), mdx()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    proxy: {
      '^/api/.*': {
        target: 'https://localhost:5001/',
        secure: false,
      },
      '^/signin-oidc': {
        target: 'https://localhost:5001/',
        secure: false,
      },
      '^/signout-callback-oidc': {
        target: 'https://localhost:5001/',
        secure: false,
      },
    },
    port: 5173,
    strictPort: true,
    host: true,
    https: {
       key: fs.readFileSync(keyFilePath),
       cert: fs.readFileSync(certFilePath),
    }
  }
})
