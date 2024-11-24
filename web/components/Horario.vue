<template>
    <div class="flex justify-center">
       <div class="w-4/5  flex justify-between m-5 shadow">
            <div class="flex flex-col items-center justify-around w-1/2 border-l border-b rounded-bl-3xl border-orange-400 py-12">
                <h3 class="text-xl font-bold">Apertura de sucursal</h3>
                <div class="flex flex-col justify-between h-3/4 items-center">
                    <p>Lunes a Viernes<br>5:00am a 10:00pm</p>
                    <p>Sabados<br>6:00am a 6:00pm</p>
                    <p>Domingos<br>8:00am a 5:00pm</p>
                </div>
                
            </div>
            <img :src="horarioImg" alt="horarioBanner" class="w-1/2 rounded-br-3xl">
        </div> 
    </div>
    
</template>
<script setup>
const horarioImg = new URL('../assets/img/horario.jpg', import.meta.url).href
</script>