<template>
    <div class="border-l-[30px] border border-orange-400 rounded-3xl shadow w-full p-4 rounded-r-none border-r-0">
        <p>{{ nombre }}</p>
        <p>Impartida por: {{ entrenador }}</p>
        <p>{{ horarioInicial }} a {{ horarioFinal }}</p>
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