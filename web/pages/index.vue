<template>
    <HeaderApp :items="menuItems" :homePage="true"></HeaderApp>
    <banner></banner>
    <Horario></Horario>
    <Membership></Membership>
</template>
<script setup>
const menuItems = [
    {name: 'Entrenadores', link: 'trainers'},
    {name: 'Clases', link: 'groupClasses'},
    {name: 'Disponibilidad', link: 'capacity'},
    {name: 'Acceso QR', link: 'qr'}
    //{name: 'Tu espacio', link: 'login'}
]
</script>