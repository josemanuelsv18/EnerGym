/** @type {import('tailwindcss').Config} */
export default {
  content: [
    './pages/**/*.{vue,js,ts}', // Páginas
    './components/**/*.{vue,js,ts}', // Componentes
    './layouts/**/*.{vue,js,ts}', // Layouts
    './app.vue', // Archivo raíz en Nuxt 3
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}

