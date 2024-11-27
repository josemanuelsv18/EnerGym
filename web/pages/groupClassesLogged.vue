<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <div v-if="error || !data">
        <h2>{{ error }}</h2>
    </div>
    <div v-else>
        <class 
        v-for="(groupClass, index) in classes" 
        :key="index"
        :nombre="groupClass.clase"
        :entrenador="groupClass.entrenador"
        :horarioInicial="groupClass.fechaInicio"
        :horarioFinal="groupClass.fechaFinal"
        class="m-5 mr-0"
        ></class>  
    </div>
</template>
<script setup>
//navbar options
const menuItems = [
    {name: 'Home', link: 'logged'},
    {name: 'QR de Acceso', link: 'qr'},
    {name: 'Disponibilidad', link: 'capacity'},
    {name: 'Entrenadores', link: 'trainersLogged'},
    {name: 'Cerrar sesion', link:'./'}
]
const apiUrl = 'https://localhost:7274/api/clases/disponibles'
const { data, error } = await useFetch(apiUrl, {
    method: 'GET',
});

const classes = data.value || [];

watchEffect(() => {
    if (error.value) {
        console.error('Error al cargar los datos:', error.value);
    } else if (data.value) {
        console.log('Datos cargados correctamente:', data.value);
    }
});
</script>