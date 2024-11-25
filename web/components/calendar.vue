<template>
<div>
    <div class="shadow-md rounded-lg p-6 w-full max-w-2xl border border-orange-400">
      <!-- Encabezado -->
      <div class="flex justify-between items-center mb-4">
        <button 
          class="calendar-button"
          @click="changeMonth(-1)"
        >
          &lt; Anterior
        </button>
        <h1 class="text-xl font-bold">{{ monthNames[currentMonth] }} {{ currentYear }}</h1>
        <button 
          class="calendar-button"
          @click="changeMonth(1)"
        >
          Siguiente &gt;
        </button>
      </div>

      <!-- Días de la semana -->
      <div class="grid grid-cols-7 text-center text-sm font-medium text-gray-700">
        <div v-for="day in weekDays" :key="day" class="py-2">
          {{ day }}
        </div>
      </div>

      <!-- Días del mes -->
      <div class="grid grid-cols-7 text-center text-sm text-gray-800">
        <!-- Días vacíos antes del inicio del mes -->
        <div 
          v-for="blank in firstDayOfMonth" 
          :key="'blank-' + blank"
          class="py-2"
        ></div>
        
        <!-- Días del mes -->
        <div 
        v-for="day in daysInMonth" 
        :key="'day-' + day"
        @click="selectDay(day)"
        :class="{'py-2 rounded-full bg-orange-400 text-white w-10 mx-6': day === selectedDay, 'py-2': day !== selectedDay}"
        >
            {{ day }}
        </div>
        </div>
    
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'

// Nombres de los meses y días de la semana
const monthNames = [
  'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 
  'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
]
const weekDays = ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'];

// Día seleccionado
const selectedDay = ref(null) // Día seleccionado inicialmente es `null`

// Función para seleccionar un día
const selectDay = (day) => {
  selectedDay.value = day // Cambia el día seleccionado al día clickeado
}

// Estados reactivos
const currentYear = ref(new Date().getFullYear())
const currentMonth = ref(new Date().getMonth())

// Computed para el primer día del mes
const firstDayOfMonth = computed(() => {
  const firstDay = new Date(currentYear.value, currentMonth.value, 1).getDay()
  return firstDay
})

// Computed para calcular la cantidad de días del mes actual
const daysInMonth = computed(() => {
  return new Date(currentYear.value, currentMonth.value + 1, 0).getDate()
})

// Función para cambiar de mes
function changeMonth(direction) {
  currentMonth.value += direction
  if (currentMonth.value < 0) {
    currentMonth.value = 11
    currentYear.value -= 1
  } else if (currentMonth.value > 11) {
    currentMonth.value = 0
    currentYear.value += 1
  }
}
</script>