<template>
    <div class="flex w-screen shadow-lg">
       <img :src="logo" alt="logo" class="h-24 my-2 ml-1">
       <ul class="flex items-center justify-around w-full mx-4">
            <li v-for="(item, index) in items" :key="index" class=" hover:bg-gray-300 hover:rounded-full px-4 py-1">
                <nuxt-link :to="item.link">{{ item.name }}</nuxt-link>
            </li>
       </ul>
       <nuxt-link to="newUser" class="button px-5 mx-12 w-48" v-if="homePage">
            ¡Inscribete ya!    
        </nuxt-link>
    </div>
</template>
<script setup>
import { ref } from 'vue';
//props
defineProps({
    items: {
        type: Array,
        required: false,
        default: () => []
    },
    homePage: {
        type: Boolean,
        required: false,
        default: false
    }
});
//logo
const logo = new URL('../assets/img/logo.png', import.meta.url).href
</script>