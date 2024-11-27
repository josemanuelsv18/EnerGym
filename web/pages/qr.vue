<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
</template>
<script setup>
//navbar options
const menuItems = [
    {name: 'Home', link: 'logged'},
    {name: 'Disponibilidad', link: 'capacity'},
    {name: 'Entrenadores', link: 'trainersLogged'},
    {name: 'Clases', link: 'groupClassesLogged'},
    {name: 'Cerrar sesion', link:'./'}
]
</script>