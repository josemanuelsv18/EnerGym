<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <h2 class="header title my-5">Conoce a nuestros entrenadores</h2>
    <div class="flex justify-center items-center">
        <!-- Mensaje de error -->
        <div v-if="error" class="flex flex-wrap justify-between w-5/6">
            <h3>Ha ocurrido un error: {{ error.message }}</h3>
        </div>
        <!-- Contenido -->
        <div v-else class="flex flex-wrap justify-center">
            <PhotoCard 
                v-for="(trainer, index) in trainers" 
                :key="index"
                :profilePic="trainer.foto"
                :name="trainer.nombre"
            ></PhotoCard>
        </div>
    </div>
</template>
<script setup>
//Menu options
const menuItems = [
    {name: 'Home', link: 'logged'},
    {name: 'QR de Acceso', link: 'qr'},
    {name: 'Disponibilidad', link: 'capacity'},
    {name: 'Clases', link: 'groupClassesLogged'},
    {name: 'Cerrar sesion', link:'./'}
]
const apiUrl = 'https://localhost:7274/api/gimnasio/entrenadores';
const { data, error } = await useFetch(apiUrl, {
    method: 'GET',
});

// Reactividad
const trainers = data.value || []

watchEffect(() => {
    if (error.value) {
        console.error('Error al cargar los datos:', error.value);
    } else if (data.value) {
        console.log('Datos cargados correctamente:', data.value);
    }
});
</script>