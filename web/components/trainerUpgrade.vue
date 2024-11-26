<template>
    <div class="bg-orange-400 flex py-5 px-10 rounded-b-3xl">
        <div>
            <div class="header m-3">
                <h3 class="title">Personal Trainer Upgrade</h3>
                <h4>Costo Adicional ${{ personalTrainerPrice }}/mes</h4>  
            </div>
            <p class="mb-2 text-center">
                Sesiones personalizadas con un coach profesional. <br>
                Plan de entrenamiento y dieta personzalizada.<br>
                Monitoreo de progreso constante
            </p>
        </div>
        <div @click="changePrice" class="button bg-white hover:bg-black hover:text-white hover:cursor-pointer w-28 my-5">
            {{ button }}
        </div>
    </div>
</template>
<script setup lang="ts">
import { ref } from "vue";

// Gestionar el botón de añadir/cancelar
const button = ref("Añadir");

// Props definidas
defineProps({
  startPrice: {
    type: Number,
    required: true,
    default: 0, // Usa `Number` en lugar de `Int16Array` para props
  },
  plusPrice: {
    type: Number,
    required: true,
    default: 0,
  },
});

// Variables locales para los precios
const localStartPrice = ref(25);
const localPlusPrice = ref(40);
const personalTrainerPrice = 60;

// Manejar el cambio de precio
const changePrice = () => {
  if (button.value === "Añadir") {
    button.value = "Cancelar";
    localStartPrice.value += personalTrainerPrice;
    localPlusPrice.value += personalTrainerPrice;
  } else {
    button.value = "Añadir";
    localStartPrice.value -= personalTrainerPrice;
    localPlusPrice.value -= personalTrainerPrice;
  }

  // Emitir el evento con los valores actualizados si es necesario
  emit("updatePrices", {
    startPrice: localStartPrice.value,
    plusPrice: localPlusPrice.value,
  });
};

// Emitir eventos al padre
const emit = defineEmits(["updatePrices"]);
</script>