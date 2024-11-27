<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    
</template>
<script setup>
//navbar options
const menuItems = [
    {name: 'Home', link: 'logged'},
    {name: 'QR de Acceso', link: 'qr'},
    {name: 'Disponibilidad', link: 'capacity'},
    {name: 'Entrenadores', link: 'trainersLogged'},
    {name: 'Cerrar sesion', link:'./'}
]
/*const api = 'https://localhost:7274/api'
const { data, error } = await useFetch(`${api}/`);*/
//temp
const data = false;
</script>