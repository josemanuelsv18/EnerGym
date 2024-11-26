<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <div class="flex justify-center">
        <div class="flex h-[95vh] mt-[3vh] mb-[2vh] flex-row w-4/5">
            <div class="flex w-3/5">
                <div class="w-1/2 flex flex-col items-end">
                    <!-- Plan Energy Start -->
                    <MembershipCard 
                        plan="Plan Energy Start" 
                        :precio="startPrice" 
                        :beneficios="benefitsStart"
                        class="w-11/12 border border-l-[30px] rounded-r-none"
                    ></MembershipCard>
                    <!-- Plan Energy Plus -->
                    <MembershipCard
                        plan="Plan Energy Plus"
                        :precio="plusPrice"
                        :beneficios="benefitsPlus"
                        class="w-11/12 border border-l-[30px] border-t-0 rounded-r-none rounded-b-3xl rounded-t-none"
                    ></MembershipCard>
                </div>
                <!-- Trainer Upgrade -->
                <TrainerUpgrade
                    :startPrice="startPrice"
                    :plusPrice="plusPrice"
                    @updatePrices="handlePriceUpdate"
                    class="flex-col px-2 rounded-b-none justify-center w-1/2 text-center"
                ></TrainerUpgrade> 
            </div>
            <div class="w-2/5 flex flex-col justify-center items-center border border-orange-400 border-r-[30px] rounded-r-3xl">
                <PayCardData></PayCardData>
            </div>        
        </div>
    </div>
    
  </template>
  
  <script setup>
  import { ref } from 'vue';
  
  // Opciones del menú de navegación
  const menuItems = [
    { name: 'Inicio', link: '/' },
    { name: 'Horario', link: '#' },
    { name: 'Membresias', link: '#' },
    { name: 'Entrenadores', link: '#' },
    { name: 'Clases', link: '#' }
  ];
  
  // Variables reactivas para los precios
  const startPrice = ref(25);
  const plusPrice = ref(40);
  
  // Beneficios de cada plan
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