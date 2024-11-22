<template>
    <HeaderApp :items="menuItems" :homePage="true"></HeaderApp>
    <banner></banner>
    <Horario></Horario>
    <Membership></Membership>
</template>
<script setup>
const menuItems = [
    {name: 'Horario', link: '#'},
    {name: 'Membresias', link: '#'},
    {name: 'Entrenadores', link: '#'},
    {name: 'Clases', link: '#'},
    {name: 'Tu espacio', link: '#'}
]
</script>