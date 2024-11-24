<template>
    <div class="flex justify-center">
        <img :src="banner" alt="banner" class="top-border w-4/5 m-5 shadow">
    </div>
    
</template>
<script setup>
//banner img
const banner = new URL('../assets/img/gym.jpg', import.meta.url).href
</script>