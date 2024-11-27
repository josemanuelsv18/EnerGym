<template>
    <HeaderApp :items="menuItems" :homePage="false"></HeaderApp>
    <!-- Formulario registro -->
    <div class="flex justify-center">
        <div class="top-border border-b border-r border-l w-2/5 flex flex-col items-center shadow h-[90vh] my-[5vh]">
            <h2 class="header title">Crea tu usuario</h2>
            <form @submit.prevent="submitForm" class="w-full flex flex-col items-center my-5">
                <!-- Pasamos el arreglo de inputs y el texto del botón -->
                <formApp 
                    :inputs="inputs" 
                    v-model="formData"
                    buttonText="Registrarse">
                </formApp>
            </form>
            <p v-if="error" class="text-red-500 mt-4">{{ error }}</p>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

// Opciones del menú de navegación
const menuItems = [
    { name: 'Inicio', link: '/' },
    { name: 'Entrenadores', link: 'trainers' },
    { name: 'Clases', link: 'groupClasses' }
];

// Inputs para el formulario
const inputs = [
    { id: 'nombre', label: 'Nombre:', type: 'text', placeholder: 'Nombre', max: 50 },
    { id: 'apellido', label: 'Apellido:', type: 'text', placeholder: 'Apellido', max: 50 },
    { id: 'cedula', label: 'Cédula:', type: 'text', placeholder: 'XX-XXXX-XXXX', max: 20 },
    { id: 'edad', label: 'Edad:', type: 'number', placeholder: 'XX', max: 2 },
    { id: 'contraseña', label: 'Contraseña:', type: 'password', placeholder: '********', max: 100 }
];

// URL de la API
const apiUrl = 'https://localhost:7274/api/usuarios/inscribir';

// Estado reactivo para manejar los datos del formulario
const formData = ref({
    nombre: '',
    apellido: '',
    contraseña: '',
    cedula: '',
    edad: null,
    estado: 'General'
});

// Estado para manejar errores
const error = ref(null);

// Router para redirigir después del registro
const router = useRouter();

// Validar datos del formulario
const validateFormData = () => {
    if (!formData.value.nombre || !formData.value.apellido) {
        error.value = 'Nombre y apellido son obligatorios.';
        return false;
    }
    if (!formData.value.edad || formData.value.edad <= 0) {
        error.value = 'La edad debe ser mayor a 0.';
        return false;
    }
    if (!formData.value.cedula) {
        error.value = 'El campo cédula es obligatorio.';
        return false;
    }
    return true;
};

// Enviar formulario
const submitForm = async () => {
    console.log('Método submitForm llamado', formData.value);
    error.value = null;

    if (!validateFormData()) {
        return;
    }

    try {
        const response = await axios.post(apiUrl, formData.value, {
            headers: { 'Content-Type': 'application/json' }
        });
        if (response.data.Code === 200) {
            console.log('Usuario registrado exitosamente');
            await router.push('/register');
        } else {
            error.value = response.data.Mensaje || 'Error desconocido.';
        }
    } catch (err) {
        console.error('Error completo:', err.response?.data || err.message);
        error.value = err.response?.data?.Mensaje || 'Hubo un problema al registrarse.';
    }
};
</script>
