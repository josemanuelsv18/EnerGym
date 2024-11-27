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
    {name: 'Entrenadores', link: 'trainers'},
    {name: 'Clases', link: 'groupClasses'},
    {name: 'Tu espacio', link: 'login'}
]
</script>