<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <div class="flex justify-center">
        <div class="flex top-border border-l border-r border-b mt-7 w-11/12">
            <div class="w-1/2 flex flex-col items-center">
                <div class="h-1/2 flex flex-col justify-around">
                    <div>
                        <h2 class="header title">Agenda tu reserva</h2>
                        <p>Seleccione el dia que desea reservar</p>
                    </div>
                    <div class="button">Registrar reserva</div>
                    <p v-if="estado">{{ reserva }}</p>
                </div>
                <img :src="imgReserva" alt="ReservaImg" class="h-1/2 rounded-3xl border-2 border-orange-400 mb-12">
            </div>
        <div class="flex justify-center items-center w-1/2">
            <Calendar class="w-full"></Calendar>  
        </div>
        
        </div>
    </div>
    
</template>
<script setup>
import { ref } from "vue";
const reserva = ref('');
const estado = ref(false);
const imgReserva = new URL('../assets/img/reservaImg.jpg', import.meta.url).href
//Navbar options
const menuItems = [
    {name: 'Home', link: './'},
    {name: 'Clases', link: '#'},
    {name: 'Entrenadores', link: '#'},
    {name: 'Workout y Nutricion', link: '#'},
    {name: 'Horarios', link: '#'},
    {name: 'Tu Perfil', link: '#'},
]
</script>