<template>
    <div class="border-l-[30px] border border-orange-400 rounded-3xl shadow w-full">
        <p>Clase de: {{ nombre }}</p>
        <p>Impartida por: {{ entrenador }}</p>
        <p>De {{ horarioInicial }} a {{ horarioFinal }}</p>
    </div>
</template>
<script setup>
//props
defineProps({
    nombre: '',
    entrenador: '',
    horarioInicial: null,
    horarioFinal: null,
})
</script>