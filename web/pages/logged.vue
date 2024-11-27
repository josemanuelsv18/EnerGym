<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <Banner></Banner>
    <Horario></Horario>
</template>
<script setup>
//navbar options
const menuItems = [
    {name: 'Horario', link: '#'},
    {name: 'QR de Acceso', link: 'qr'},
    {name: 'Disponibilidad', link: 'capacity'},
    {name: 'Entrenadores', link: 'trainersLogged'},
    {name: 'Clases', link: 'groupClassesLogged'},
    {name: 'Cerrar sesion', link:'./'}
]
</script>