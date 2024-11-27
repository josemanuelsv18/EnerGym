# EnerGym
- Josemanuel Sifontes
- Derlin Romero
- Austin Bernal
- Karen Sanchez

### Requisitos del Sistema para un Gimnasio

Se requiere un sistema compuesto por **dos interfaces**:  
1. **Interfaz web para usuarios/clientes**.  
2. **Aplicación de escritorio para usuarios administrativos**.

---

### **Requisitos para la Aplicación de Escritorio (Administración)**

La interfaz administrativa deberá incluir las siguientes funcionalidades:

#### **Gestión de Usuarios**
- Registrar nuevos usuarios.
- Buscar usuarios por:
  - Nombre.
  - Apellido.
  - Número de cédula.
- Datos extra a registrar por usuario (opcionales):
  - Edad
  - Fecha Inscripcion
  - Datos de pago (tarjeta de credito encriptada)
- Modificar el estado de los usuarios:
  - **Estados posibles**: Premium, General, Retirado, Moroso.
  - Gestionar deudas de usuarios con el gimnasio.
  - Permitir el pago de la mensualidad en efectivo.
- Retirar y eliminar usuarios del sistema.

#### **Control de Accesos**
- Permitir o denegar el acceso a usuarios utilizando:
  - **Código QR** generado por la aplicación web del cliente.
  - Estado del usuario (acceso denegado si es *Retirado*, *Moroso* o si el gimnasio está lleno).  
- Registrar las **salidas de los usuarios** para:
  - Gestionar la capacidad del gimnasio en tiempo real.
  - Denegar nuevas entradas si el local está lleno.

#### **Capacidad del Gimnasio**
- Monitorear el nivel de ocupación del gimnasio para garantizar que no exceda su capacidad máxima.

### **Registrar clases**
- El administrador debe poder registrar clases grupales de las que se deben tener:
  - Nombre o tema de la clase
  - Entrenador
  - Horario
- Las clases son abiertas para todos los miembros del gimnasio y no hace falta registrarse

#### **Ventana de Pruebas**
- Incluir una herramienta para simular cambios en los estados del sistema (por ejemplo, modificar la capacidad del gimnasio) para facilitar la demostración del proyecto.

#### **Funciones Adicionales (Opcionales)**
- Enviar notificaciones a los usuarios de la aplicación web.

---

### **Requisitos para la Aplicación Web (Usuarios/Clientes)**

La interfaz web deberá proporcionar las siguientes funcionalidades para los usuarios del gimnasio:

#### **Acceso y Gestión de Perfil**
- Inicio de sesión.
- Inscripción y retiro del gimnasio.
  - Se deben solicitar los mismos datos para la inscripcion web que la inscripcion por admin
- Generar un **código QR** de acceso:
  - Usuarios *Premium* podrán generar un segundo QR para invitados.

#### **Pagos**
- Realizar pagos de mensualidades.
- Almacenar información de pago para facilitar transacciones futuras.

#### **Consultas y Reservas**
- Mostrar:
  - Horarios de apertura.
  - Disponibilidad actual del gimnasio como porcentaje.
- Permitir la creación de **reservas** en un calendario:
  - Las reservas afectan la capacidad disponible del gimnasio.
  - Asegurar el acceso del usuario aunque el gimnasio esté lleno en el momento de su reserva.

#### **Workouts y Nutrición**
- Sección con:
  - Ejercicios y rutinas específicas para las máquinas disponibles.
  - Recomendaciones de nutrición personalizadas según los objetivos del usuario.

#### **Clases y Entrenadores**
- Visualización y reserva de:
  - Clases especiales.
  - Entrenadores personales.

---

### **Aspectos Técnicos y Consideraciones**
- **Sincronización de datos** entre la aplicación web y la de escritorio para garantizar consistencia en tiempo real.
- Implementar medidas de seguridad para la gestión de pagos, accesos y almacenamiento de información personal.
- Garantizar la escalabilidad del sistema para adaptarse al crecimiento del gimnasio.
