import {heroui} from "@heroui/react";

/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
    "./node_modules/@heroui/theme/dist/**/*.{js,ts,jsx,tsx}"
  ],
  theme: {
    extend: {
      // that is animation class
      animation: {
        fade: 'fadeOut 1.2s ease-in-out',
      },

      // that is actual animation
      keyframes: () => ({
        fadeOut: {
          '0%': { opacity: 1 },
          '100%': { opacity: 0  },
        },
      }),
    },
  },
  darkMode: "class",
  plugins: [heroui({
    themes: {
      purple: {
        extend: "light",
        colors: {
          background: "#F4EEFF",
          primary: {
            50: "#DCD6F7",
            100: "#A6B1E1",
            200: "#424874",
            300: "#424874",
            400: "#424874",
            500: "#424874",
            600: "#424874",
            700: "#424874",
            800: "#424874",
            900: "#424874",
            DEFAULT: "#424874"
          },
        }
      },
      cream: {
        extend: "light",
        colors: {
          background: "#FFF5E4",
          primary: {
            50: "#FFE3E1",
            100: "#FFD1D1",
            200: "#FF9494",
            300: "#FF9494",
            400: "#FF9494",
            500: "#FF9494",
            600: "#FF9494",
            700: "#FF9494",
            800: "#FF9494",
            900: "#FF9494",
            DEFAULT: "#FF9494"
          },
        }
      },
      skin: {
        extend: "light",
        colors: {
          background: "#FFC7C7",
          primary: {
            50: "#FFE2E2",
            100: "#F6F6F6",
            200: "#8785A2",
            300: "#8785A2",
            400: "#8785A2",
            500: "#8785A2",
            600: "#8785A2",
            700: "#8785A2",
            800: "#8785A2",
            900: "#8785A2",
            DEFAULT: "#8785A2"
          },
        }
      },
      teal: {
        extend: "dark",
        colors: {
          background: "#222831",
          primary: {
            50: "#393E46",
            100: "#00ADB5",
            200: "#EEEEEE",
            300: "#EEEEEE",
            400: "#EEEEEE",
            500: "#EEEEEE",
            600: "#EEEEEE",
            700: "#EEEEEE",
            800: "#EEEEEE",
            900: "#EEEEEE",
            DEFAULT: "#EEEEEE"
          },
        }
      },
      navy: {
        extend: "dark",
        colors: {
          background: "#1B262C",
          primary: {
            50: "#0F4C75",
            100: "#3282B8",
            200: "#BBE1FA",
            300: "#BBE1FA",
            400: "#BBE1FA",
            500: "#BBE1FA",
            600: "#BBE1FA",
            700: "#BBE1FA",
            800: "#BBE1FA",
            900: "#BBE1FA",
            DEFAULT: "#BBE1FA"
          },
        }
      },
      night: {
        extend: "dark",
        colors: {
          background: "#27374D",
          primary: {
            50: "#526D82",
            100: "#9DB2BF",
            200: "#DDE6ED",
            300: "#DDE6ED",
            400: "#DDE6ED",
            500: "#DDE6ED",
            600: "#DDE6ED",
            700: "#DDE6ED",
            800: "#DDE6ED",
            900: "#DDE6ED",
            DEFAULT: "#DDE6ED"
          },
        }
      },
    }
  })]
}

