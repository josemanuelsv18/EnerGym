<template>
    <div class="border-t-[20px] border-orange-400 rounded-3xl shadow w-1/5 m-4 flex flex-col justify-center">
        <img :src="profilePic" alt="Foto de perfil" class="h-64" >
        <p class="py-2 text-center bg-orange-400 shadow rounded-b-3xl font-bold">
            {{ name }}
        </p>
    </div>
</template>
<script setup>
//props
defineProps({
    profilePic: '',
    name: ''
})
</script>