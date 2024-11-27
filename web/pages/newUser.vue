<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <div class="flex justify-center">
    <div class="top-border border-b border-r border-l w-2/5 flex flex-col items-center shadow h-[90vh] my-[5vh]">
        <h2 class="header title my-4">Crea tu usuario</h2>
        <form action="" class="w-1/2 flex flex-col items-center h-full justify-around">
            <div class="mb-4 w-full">
                <label for="name" class="label">Nombre:</label>
                <input type="text" v-model="name" id="name" placeholder="Nombre" class="input" maxlength="50">  
            </div>
            <div class="mb-4 w-full">
                <label for="lastname" class="label">Apellido: </label>
                <input type="text" v-model="lastname" id="lastname" placeholder="Apellido" class="input" maxlength="50">
            </div>
            <div class="mb-4 w-full">
                <label for="id" class="label">Cedula:</label>
                <input type="text" v-model="id" id="id" placeholder="XX-XXXX-XXXX" class="input" maxlength="20">
            </div>
            <div class="mb-4 w-full">
                <label for="age" class="label">Edad:</label>
                <input type="number" v-model="age" id="age" placeholder="XX" class="input" min="16" max="99">
            </div>
            <div class="mb-8 w-full">
                <label for="password" class="label">Contraseña:</label>
                <input type="password" v-model="password" id="password" placeholder="****" class="input" maxlength="100">
            </div>
            <!-- Botón de enviar -->
            <nuxt-link to="register" class="button px-10 mb-4">
                <button 
                type="submit" 
                >
                    Registrarse
                </button>   
            </nuxt-link>
        </form>
    </div>   
    </div>
    
</template>
<script setup>
// Opciones del menú de navegación
const menuItems = [
    { name: 'Inicio', link: '/' },
    { name: 'Entrenadores', link: 'trainers' },
    { name: 'Clases', link: 'groupClasses' }
  ];
</script>