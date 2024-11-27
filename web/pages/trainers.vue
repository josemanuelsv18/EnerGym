<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <h2>Conoce a nuestros entrenadores</h2>
    <div class="flex justify-center">
        <div v-if="error || !data" class="flex flex-wrap justify-between w-5/6">
            <!--
            <h3>Ha ocurrido un error</h3>
            Temporal
            -->
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
            <PhotoCard :profilePic="logo" name="Pedro Perez"></PhotoCard>
        </div>
        <div v-else>
            <div v-for="(trainer, index) in data" :key="index" class="flex flex-wrap justify-between w-5/6">
                <img :src="trainer.foto" alt="Foto de perfil">
                <p>{{ trainer.nombre }}</p>
            </div>
            <PhotoCard 
            v-for="(trainer, index) in data" :key="index"
            :profilePic="trainer.foto"
            :name="trainer.nombre"
            ></PhotoCard>
        </div>
    </div>
</template>
<script setup>
//Menu options
const menuItems = [
    {name: 'Inicio', link: '/'},
    {name: 'Clases', link: 'groupClasses'},
    {name: 'Tu espacio', link: 'login'},
]
/*const api = 'https://localhost:7274/api'
const { data, error } = await useFetch(`${api}/gimnasio/entrenadores`);*/
//temp
const data = false
const logo = new URL('../assets/img/logo.png', import.meta.url).href

const imgList = []
if(data){
    for(trainer in data){
        imgList.push(new URL(trainer.foto, import.meta.url).href)
    }
}
</script>