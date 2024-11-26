<template>
    <h2 class="text-3xl font-bold text-center my-5">Entrena Hoy en Ener<b class="text-orange-400">Gym</b></h2>
    <div class="flex justify-center">
        <div class="w-4/5 flex flex-col m-5">
        <div class="flex justify-between">
            <MembershipCard 
            plan="Plan Energy Start" 
            :precio="startPrice" 
            :beneficios="benefitsStart"
            ></MembershipCard>
            <MembershipCard
            plan="Plan Energy Plus"
            :precio="plusPrice"
            :beneficios="benefitsPlus"
            ></MembershipCard>
        </div>
        <TrainerUpgrade
        :startPrice="startPrice"
        :plusPrice="plusPrice"
        @updatePrices="handlePriceUpdate"
        class="justify-between pr-32"
        ></TrainerUpgrade>
    </div>
    </div>
</template>
<script setup>
import { ref } from 'vue';
//Gestionar el precio extra de un entrenador personal
const button = ref('Añadir');
const startPrice = ref(25);
const plusPrice = ref(40);

//Variables
const benefitsStart = [
    'Acceso ilimitado a todas las areas del Gimnasio.',
    'Clases Grupales (yoga, spinning, pilates, crossfit, entrenamiento funcional).',
    'Uso gratuito de duchas y lockers.',
    'Horarios flexibles.',
    '1 Evaluacion Fisica Inicial.'
];
const benefitsPlus = [
    'Todo lo incluido en el plan Energy Start.',
    'Ingreso de un invitado cada vez que quieras.',
    'Wi-Fi Gratis.',
    'Beneficios y descuentos en más de 40 empresas con tu tarjeta SOCIO ENERGY.'
];
// Manejar el evento emitido por TrainerUpgrade
const handlePriceUpdate = ({ startPrice: newStartPrice, plusPrice: newPlusPrice }) => {
    startPrice.value = newStartPrice;
    plusPrice.value = newPlusPrice;
};
</script>