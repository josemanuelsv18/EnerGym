<template>
    <HeaderApp :items="menuItems":homePage="true"></HeaderApp>
</template>
<script setup>
const menuItems = [
    {name: 'Inicio', link: '/'},
    {name: 'Horario', link: '#'},
    {name: 'Membresias', link: '#'},
    {name: 'Entrenadores', link: '#'},
    {name: 'Clases', link: '#'},
]
</script>