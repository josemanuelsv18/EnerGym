<template>
    <h2 class="text-2xl font-bold mb-4">Datos de Pago</h2>
    <form @submit.prevent="processPayment" class="flex flex-col">
        <!--Seleccionar tarjeta-->
        <div class="mb-4">
            <p class="label">Seleccione el tipo de tarjeta:</p>
            <div class="flex items-center justify-around">
                <div class="flex">
                    <input type="radio" id="card1" v-model="card1" value="Visa" name="card">
                    <label for="card1"><img :src="visaLogo" alt="Visa Logo" class="small-logo"></label>
                </div>
                <div class="flex">
                    <input type="radio" id="card2" v-model="card2" value="Mastercard" name="card">
                    <label for="card2"><img :src="mastercardLogo" alt="Mastercard Logo" class="small-logo"></label>
                </div>    
            </div>
            
        </div>
        <!-- Nombre del titular -->
        <div class="mb-4">
            <label for="cardholder" class="label">Nombre del Titular</label>
            <input 
            type="text" 
            id="cardholder" 
            v-model="cardholder" 
            placeholder="Nombre"
            class="input"
            required
            />
        </div>
        
        <!-- Número de tarjeta -->
        <div class="mb-4">
            <label for="cardNumber" class="label">Número de Tarjeta</label>
            <input 
            type="text" 
            id="cardNumber" 
            v-model="cardNumber" 
            placeholder="XXXX XXXX XXXX XXXX"
            class="input"
            maxlength="19"
            @input="formatCardNumber"
            required
            />
        </div>

        <!-- Fecha de vencimiento -->
        <div class="mb-4 flex gap-4">
            <div class="flex flex-col">
                <label for="expiryMonth" class="label">Mes de Vencimiento</label>
                <input 
                    type="number" 
                    id="expiryMonth" 
                    v-model="expiryMonth" 
                    placeholder="MM"
                    class="input"
                    min="1"
                    max="12"
                    required
                />
            </div>
            <div class="flex flex-col">
            <label for="expiryYear" class="label">Año de Vencimiento</label>
            <input 
                type="number" 
                id="expiryYear" 
                v-model="expiryYear" 
                placeholder="YY"
                class="input"
                min="2024"
                max="2999"
                required
            />
            </div>
        </div>

        <!-- CVV -->
        <div class="mb-4">
            <label for="cvv" class="label">CVV</label>
            <input 
            type="password" 
            id="cvv" 
            v-model="cvv" 
            placeholder="XXX"
            class="input"
            maxlength="3"
            @input="formatCVV"
            required
            />
        </div>

        <!-- Botón de enviar -->
        <button 
            type="submit" 
            class="w-full bg-orange-400 text-white py-2 px-4 rounded shadow hover:bg-orange-500"
        >
            Registrar tarjeta
        </button>
        <nuxt-link to="loggedUserHome" class="w-1/2 bg-orange-400 text-white py-2 px-4 rounded shadow hover:bg-orange-500 self-center text-center mt-4">Continuar →</nuxt-link>
    </form>
</template>

<script setup>
import { ref } from "vue";
//Imagenes
const visaLogo = new URL('../assets/img/Visa.png', import.meta.url).href;
const mastercardLogo = new URL('../assets/img/Mastercard.png', import.meta.url).href;
// Datos del formulario
const cardholder = ref("");
const cardNumber = ref("");
const expiryMonth = ref("");
const expiryYear = ref("");
const cvv = ref("");

// Función para procesar el pago
/*const processPayment = () => {
if (!validateCardNumber(cardNumber.value)) {
    alert("El número de tarjeta no es válido.");
    return;
}

if (!validateExpiryDate(expiryMonth.value, expiryYear.value)) {
    alert("La fecha de vencimiento no es válida.");
    return;
}

alert("Pago realizado con éxito.");
};

// Validar número de tarjeta
const validateCardNumber = (number) => {
const regex = /^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$/; // Visa o Mastercard
return regex.test(number.replace(/\s+/g, ""));
};

// Validar fecha de vencimiento
const validateExpiryDate = (month, year) => {
const currentDate = new Date();
const currentMonth = currentDate.getMonth() + 1;
const currentYear = currentDate.getFullYear() % 100;

if (month < 1 || month > 12) return false;
if (year < currentYear || (year === currentYear && month < currentMonth)) return false;

return true;
};*/

// Formatear el número de tarjeta mientras se escribe
const formatCardNumber = () => {
cardNumber.value = cardNumber.value
    .replace(/\D/g, "") // Eliminar caracteres no numéricos
    .replace(/(\d{4})(?=\d)/g, "$1 "); // Insertar un espacio cada 4 dígitos
};
// Formatear month y year
const formatMonth = () => {
expiryMonth.value = expiryMonth.value
    .replace(/\D/g, "") //Eliminar caracteres no numericos
}
// Formatear el numero CVV
const formatCVV = () => {
cvv.value = cvv.value
    .replace(/\D/g, "") //Eliminar caracteres no numericos
}
</script>