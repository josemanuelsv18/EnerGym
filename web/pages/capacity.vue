<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
</template>
<script setup>
//navbar options
const menuItems = [
    {name: 'Home', link: 'logged'},
    {name: 'QR de Acceso', link: 'qr'},
    {name: 'Entrenadores', link: 'trainersLogged'},
    {name: 'Clases', link: 'groupClassesLogged'},
    {name: 'Cerrar sesion', link:'./'}
]
</script>