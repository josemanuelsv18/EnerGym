<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <h2>Conoce a nuestros entrenadores</h2>
    <div class="flex justify-center">
        <!-- Mensaje de error -->
        <div v-if="error" class="flex flex-wrap justify-between w-5/6">
            <h3>Ha ocurrido un error: {{ error.message }}</h3>
        </div>
        <!-- Contenido -->
        <div v-else>
            <div 
                v-for="(trainer, index) in trainers" 
                :key="index" 
                class="flex flex-wrap justify-between w-5/6"
            >
                <img :src="trainer.foto" alt="Foto de perfil">
                <p>{{ trainer.nombre }}</p>
            </div>
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
// Opciones del menú
const menuItems = [
    { name: 'Inicio', link: '/' },
    { name: 'Clases', link: '/groupClasses' },
    { name: 'Tu espacio', link: '/login' },
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
