<template>
    <div class="flex w-screen shadow-lg">
       <img :src="logo" alt="logo" class="h-24 my-2 ml-1">
       <ul class="flex items-center justify-around w-3/4 mx-4">
            <li v-for="(item, index) in items" :key="index" class=" hover:bg-gray-300 hover:rounded-full px-4 py-1">
                <nuxt-link :to="item.link">{{ item.name }}</nuxt-link>
            </li>
       </ul>
       <div v-if="homePage" class="bg-orange-400 rounded-full text-xl px-5 py-2 self-center shadow mx-4 hover:bg-yellow-400 hover:shadow-xl">
            ¡Inscribete ya!
       </div>
    </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';
//props
defineProps({
    items: {
        type: Array,
        required: true,
        default: () => []
    },
    homePage: {
        type: Boolean,
        required: false,
        default: () => [false]
    }
});
//logo
const logo = new URL('../assets/img/logo.png', import.meta.url).href
</script>