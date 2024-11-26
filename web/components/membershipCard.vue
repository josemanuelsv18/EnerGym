<template>
    <div class="membership">
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
        <div class="button w-2/5">{{ accion }}</div>
    </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
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
    }
});
</script>