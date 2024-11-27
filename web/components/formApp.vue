<template>
    <form action="" class="w-1/2 flex flex-col items-center h-full justify-around">
        <div class="w-full">
            <div class="mb-4 w-full" v-for="(input, index) in inputs" :key="index">
                <label 
                :for="input.id" 
                class="label">
                    {{ input.label }}
                </label>
                <input 
                :id="input.id"
                :v-model="input.id"
                :type="input.type" 
                :placeholder="input.placeholder" 
                :maxlength="input.max"
                class="input">  
            </div>    
        </div>
        
        <!-- Botón de enviar -->
        <button class="button px-10 mb-4 justify-self-end"
        type="submit" 
        >
            {{ buttonText }}
        </button>
    </form>
</template>
<script setup>
import { ref } from "vue";
//props
defineProps({
    inputs: {
        type: Array,
        required: true,
        default: () => []
    },
    buttonText: {
        type: String,
        required: true,
        default: ''
    }
})
</script>