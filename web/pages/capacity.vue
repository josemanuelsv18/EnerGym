<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <div 
    v-if="error">
        <h3>Ha ocurrido un error</h3>
    </div>
    <div class="flex justify-center" v-else>
        <div class="flex justify-center items-center font-bold text-3xl h-64 mt-[25vh] w-64 bg-orange-400 rounded-full">
            {{ data }}
        </div>
    </div>
    
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
const apiUrl = 'https://localhost:7274/api/gimnasio/capacidad%'
const { data, error } = await useFetch(apiUrl, {
    method: 'GET',
});
</script>