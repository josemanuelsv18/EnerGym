<template>
    <div class="membership flex flex-col justify-around">
        <div class="header">
            <h3 class="title">{{ plan }}</h3>
            <h4>Precio: ${{ precio }}/mes</h4>  
        </div>
        <div>
            
        </div>
        <p class="">
            <p v-for="(benefit, index) in beneficios" :key="index" class="list-item ml-5">
            {{ benefit }}
            </p> 
        </p>
        <!--
        <nuxt-link :to="linked" class="button w-2/5" @click="pushButton">
            {{ accion }}
        </nuxt-link>
        -->
        
    </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
//componentes reactivos
const isSelectedStart = ref(false);
const isSelectedPlus = ref(false);
//props
defineProps({
    plan: {
        type: String,
        required: true,
        default: ''
    },
    precio: {
        type: Number,
        required: true,
        default: 0,
    },
    beneficios: {
        type: Array,
        required: true,
        default: '' 
    },
    accion: {
        type: String,
        required: true,
        default: 'Inscribirse'
    },
    linked:{
        type: String,
        required: true,
        default: '#'
    }
});
//Emits
const emit = defineEmits([
    "selectMembership",
]);
//Metodo para emitir y actualizar evento
const pushButton = () => {
    emit("selectMembership", {
        selectedStart: isSelectedStart.value,
        selectedPlus: isSelectedPlus.value
    });
}
</script>