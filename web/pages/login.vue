<template>
    <HeaderApp :items="menuItems":homePage="false"></HeaderApp>
    <div class="flex justify-center">
        <div class="top-border border-b border-r border-l w-2/5 flex flex-col items-center shadow h-[70vh] my-[5vh]">
            <h2 class="header title">Inicia tu sesion</h2>
            <formApp :inputs="inputs" buttonText="Iniciar sesion"></formApp>
            <p class="text-sm mb-2">no tienes una cuenta?</p>
            <nuxt-link to="newUser" class="button px-12 mb-10">Registrate</nuxt-link>
        </div> 
    </div>
    
    
</template>
<script setup>
const menuItems = [
    {name: 'Inicio', link: '/'},
    {name: 'Entrenadores', link: 'trainers'},
    {name: 'Clases', link: 'groupClasses'},
]
const inputs = [
    {
        id: 'id' , 
        label: 'Cedula:', 
        type: 'number', 
        placeholder: 'XX-XXXX-XXXX', 
        max: 20
    },
    {
        id: 'password',
        label: 'Contraseña:',
        type: 'password',
        placeholder: '********',
        max: 100
    }
]
</script>