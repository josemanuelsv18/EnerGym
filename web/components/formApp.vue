<template>
    <div class="w-1/2 flex flex-col items-center h-full justify-around">
        <div class="w-full">
            <div class="mb-4 w-full" v-for="(input, index) in inputs" :key="index">
                <label :for="input.id" class="label">
                    {{ input.label }}
                </label>
                <input 
                    :id="input.id"
                    :type="input.type" 
                    :placeholder="input.placeholder" 
                    :maxlength="input.max"
                    v-model="inputValues[input.id]" 
                    class="input">  
            </div>    
        </div>
        
        <!-- Botón de enviar -->
        <button class="button px-10 mb-4 justify-self-end" type="submit">
            {{ buttonText }}
        </button>
    </div>
</template>

<script setup>
import { reactive, defineProps } from "vue";

// Definir los props
const props = defineProps({
    inputs: {
        type: Array,
        required: true
    },
    buttonText: {
        type: String,
        required: true
    }
});

// Crear un objeto reactivo para almacenar los valores de los inputs
const inputValues = reactive({});

// Inicializar los valores de los inputs con las claves correctas
props.inputs.forEach(input => {
    inputValues[input.id] = ''; // Inicializar cada campo con una cadena vacía
});
</script>
