<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
</template>
<script setup>
//navbar options
const menuItems = [
    {name: 'Inicio', link: './'},
    {name: 'Entrenadores', link: 'trainers'},
    {name: 'Tu espacio', link: 'login'}
]
</script>