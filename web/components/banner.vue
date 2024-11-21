<template>
    <div class="flex justify-center">
        <img :src="banner" alt="banner" class="border-t-[20px] border-orange-400 rounded-2xl w-4/5 mt-5 shadow">
    </div>
    
</template>
<script setup>
//banner img
const banner = new URL('../assets/img/gym.jpg', import.meta.url).href
</script>