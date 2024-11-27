<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    
    <!-- Mostrar mensaje de error si ocurre algún problema -->
    <div v-if="error">
      <h3>Ha ocurrido un error</h3>
    </div>
    
    <!-- Mostrar el código QR si no hay error -->
    <div class="flex justify-center mt-10 flex-col items-center" v-else>
      <canvas ref="qrCanvas" class="border-4 rounded-3xl shadow border-orange-400 p-4"></canvas>
      <p class="my-4 font-bold">Escanee su codigo de acceso en administracion para acceder al gimnasio</p>
    </div>
  </template>
  
<script setup>
import { ref, onMounted } from 'vue';
import QRCode from 'qrcode';

// Navbar options
const menuItems = [
{ name: 'Home', link: './' },
{ name: 'Disponibilidad', link: 'capacity' },
{ name: 'Entrenadores', link: 'trainersLogged' },
{ name: 'Clases', link: 'groupClassesLogged' },
];

// API URL
const apiUrl = 'https://localhost:7274/api/usuarios/qr/123456789';



// Hacer la solicitud a la API
const { data, error } = await useFetch(apiUrl, {
method: 'GET',
});
const qrData = ref(null)
const qrCanvas = ref(null);
qrData.value = data.value;
onMounted(() => {
    QRCode.toCanvas(qrCanvas.value, qrData.value, { width: 450 });
});
</script>
  