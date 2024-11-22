<template>
    <div class="flex justify-center">
        <img :src="banner" alt="banner" class="border-t-[30px] border-orange-400 rounded-3xl w-4/5 m-5 shadow">
    </div>
    
</template>
<script setup>
//banner img
const banner = new URL('../assets/img/gym.jpg', import.meta.url).href
</script>