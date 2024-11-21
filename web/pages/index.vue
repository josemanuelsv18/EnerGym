<template>
    <HeaderApp :items="menuItems" :homePage="true"></HeaderApp>
    <banner></banner>
</template>
<script setup>
const menuItems = [
    {name: 'Tu espacio', link: '#'},
    {name: 'Entrenadores', link: '#'},
    {name: 'Membresias', link: '#'},
    {name: 'Horario', link: '#'},
    {name: 'Clases', link: '#'},
]
</script>