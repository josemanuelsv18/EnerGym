<template>
    <h2 class="text-3xl font-bold text-center my-5">Entrena Hoy en Ener<b class="text-orange-400">Gym</b></h2>
    <div class="flex justify-center">
        <div class="w-4/5 flex flex-col m-5">
        <div class="flex justify-between">
            <div class="membership">
                <div class="membership-header">
                    <h3 class="membership-title">Plan Energy Start</h3>
                    <h4>Precio: ${{ startPrice }}/mes</h4>  
                </div>
                
                <p>
                    Acceso ilimitado a todas las areas del Gimnasio.<br>
                    Clases Grupales (yoga, spinning, pilates, crossfit, entrenamiento funcional).<br>
                    Uso gratuito de duchas y lockers.<br>
                    Horarios flexibles.<br>
                    1 Evaluacion Fisica Inicial.<br>
                </p>
                <div class="button w-2/5">Inscribirse</div>
            </div>
            <div class="membership">
                <div class="membership-header">
                    <h3 class="membership-title">Plan Energy Plus</h3>
                    <h4>Precio: ${{ plusPrice }}/mes</h4>
                </div>
                
                <p>
                    Todo lo incluido en el plan Energy Start.<br>
                    Ingreso de un invitado cada vez que quieras.<br>
                    Wi-Fi Gratis.<br>
                    Beneficios y descuentos en más de 40 empresas con tu tarjeta SOCIO ENERGY.
                </p>
                <div class="button w-2/5">Inscribirse</div>
            </div>
        </div>
        <div class="bg-orange-400 flex justify-between py-5 px-20 rounded-b-3xl">
            <div>
                <div class="membership-header m-3">
                    <h3 class="membership-title">Personal Trainer Upgrade</h3>
                    <h4>Costo Adicional $60/mes</h4>  
                </div>
                <p class="mb-2 text-center">
                    Sesiones personalizadas con un coach profesional. <br>
                    Plan de entrenamiento y dieta personzalizada.<br>
                    Monitoreo de progreso constante
                </p>
            </div>
            <div @click="changePrice" class="button bg-white hover:bg-black hover:text-white w-1/6">
                {{ button }}
            </div>
        </div>
    </div>
    </div>
</template>
<script setup>
import { ref } from 'vue';
//Gestionar el precio extra de un entrenador personal

const button = ref('Añadir');
const startPrice = ref(25);
const plusPrice = ref(40);

//funcionalidad
const changePrice = () =>{
    const personalTrainerPrice = 60;
    if(button.value === 'Añadir'){
        button.value = 'Cancelar';
        startPrice.value += personalTrainerPrice;
        plusPrice.value += personalTrainerPrice;
    }else{
        button.value = 'Añadir';
        startPrice.value -= personalTrainerPrice;
        plusPrice.value -=personalTrainerPrice;
    }
    
}
</script>