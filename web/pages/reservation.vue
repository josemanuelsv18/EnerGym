<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <div class="flex justify-center">
        <div class="flex top-border border-l border-r border-b mt-7 w-11/12 mb-5 shadow">
            <div class="w-1/2 flex flex-col items-center">
                <div class="h-1/2 flex flex-col justify-around items-center">
                    <div>
                        <h2 class="header title">Agenda tu reserva</h2>
                        <p>Seleccione el dia que desea reservar</p>
                    </div>
                    <button class="button" @click="confirmationReserve">Registrar reserva</button>
                    <p v-if="estado">{{ reserva }}</p>
                </div>
                <img :src="imgReserva" alt="ReservaImg" class="h-1/2 rounded-3xl border-2 border-orange-400 mb-12">
            </div>
        <div class="flex justify-center items-center w-1/2">
            <Calendar class="w-full" @selectDate="showDate"></Calendar>  
        </div>
        </div>
    </div>
    
</template>
<script setup>
import { ref } from "vue";
//Constantes y Variables
//Estados reactivos
const reserva = ref('');
const estado = ref(false);
const imgReserva = new URL('../assets/img/reservaImg.jpg', import.meta.url).href;
// Nombres de los meses (necesario para formatear la fecha)
const monthNames = [
  'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
  'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
];
//fecha guardada en formato para un scope global
let dateInFormat;
let date;

//Navbar options
const menuItems = [
    {name: 'Home', link: './'},
    {name: 'Clases', link: '#'},
    {name: 'Entrenadores', link: '#'},
    {name: 'Workout y Nutricion', link: '#'},
    {name: 'Horarios', link: '#'},
    {name: 'Tu Perfil', link: '#'},
]

//Verificar que la fecha de la reserva no haya pasado
const dateValidation = (d) => {
    const t1 = d;
    const t2 = new Date();
    if(t1 < t2){
        alert('la fecha de la reservacion no puede ser hoy o ya haber pasado');
        return false;
    }else{
        return true;
    }
}

//Manejar evento de seleccion de fecha
const showDate = (d) => {
    dateInFormat = d;
    date = {
        day: d.getDate(),
        month: d.getMonth(),
        year: d.getFullYear()
    }
    const mes = monthNames[date.month];
    reserva.value = `Su reserva ha sido agendada para el día ${date.day} de ${mes} del ${date.year}`;
    
}

//Confirmar la reserva
const confirmationReserve = () => {
    if(dateValidation(dateInFormat)){
        if(reserva.value){
            estado.value = true;
        }else{
            alert('Por favor, seleccione una fecha para hacer la reserva');
        }
    }else{
        date = null;
    }
}
</script>