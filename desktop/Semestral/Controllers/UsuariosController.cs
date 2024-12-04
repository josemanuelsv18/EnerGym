using Semestral.Datos;
using Semestral.Models;

using Microsoft.AspNetCore.Mvc;

namespace Semestral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private DB db = new DB();
        private Seguridad seguridad = new Seguridad();

        #region VistaDesktop
        // Validar si el estado esta dentro de los permitidos
        private bool Valido(string estado)
        {
            return estado == "Premium" || estado == "General" || estado == "Retirado";
        }

        // Validar si la capacidad del gimnasio permite mas entradas
        private bool CapacidadValida()
        {
            return db.GimnasioOcupacionActual() < 100;
        }

        // Registrar nuevos usuarios
        [HttpPost]
        [Route("guardar")]
        public object GuardarUsuario([FromBody] UsuarioRequest usuario)
        {
            if (!Valido(usuario.estado))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "El estado del usuario no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400
                };
            }

            if (db.ExisteCedula(usuario.cedula))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409
                };
            }

            string contraseñaEncriptada = seguridad.Encriptar(usuario.contraseña);

            var usuariO = new UsuarioRequest
            {
                nombre = usuario.nombre,
                apellido = usuario.apellido,
                contraseña = contraseñaEncriptada,
                cedula = usuario.cedula,
                edad = usuario.edad,
                estado = usuario.estado
            };
            
            var guardado = db.InsertarUsuario(usuariO);
            if (guardado > 0)
            {
                return new
                {
                    Titulo = "Exito al guardar",
                    Mensaje = "Los datos se han guardado correctamente",
                    Code = 200
                };
            }
            return new
            {
                Titulo = "Error al guardar",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400
            };
        }

        // Editar datos de usuarios
        [HttpPost]
        [Route("editar/{id}")]
        public object EditarUsuario(int id, [FromBody] EditarRequest editar)
        {
            if (db.ExisteCedulaParaEditar(id, editar.cedula))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409
                };
            }

            var editaR = new EditarRequest
            {
                nombre = editar.nombre,
                apellido = editar.apellido,
                cedula = editar.cedula,
                edad = editar.edad
            };

            var editado = db.ActualizarUsuario(id, editaR);
            if (editado > 0)
            {
                return new
                {
                    Titulo = "Exito al editar",
                    Mensaje = "Los datos se han editado correctamente",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al guardar",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400
            };
        }

        // Buscar usuarios por nombre, apellido y número de cédula
        [HttpGet]
        [Route("buscar")]
        public List<Usuario> BuscarUsuario(string nombre = null, string apellido = null, string cedula = null)
        {
            return db.BuscarUsuario(nombre, apellido, cedula);
        }

        [HttpGet]
        [Route("buscar/reservas")]
        public List<Reserva> BuscarReservas(string nombre = null, string estado = null)
        {
            return db.BuscarReservas(nombre, estado);
        }

        // Actualizar estado del usuario
        [HttpPost]
        [Route("estado/{id}")]
        public object ActualizarEstadoUsuario(int id, [FromBody] string nuevoEstado)
        {
            if (!Valido(nuevoEstado))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "El nuevo estado a insertar no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400
                };
            }

            if (!db.ExisteUsuario(id))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Este usuario no esta en nuestra base de datos.",
                    Code = 409
                };
            }

            var actualizado = db.ActualizarEstado(id, nuevoEstado);
            if (actualizado > 0)
            {
                return new
                {
                    Titulo = "Exito al guardar",
                    Mensaje = "Los datos se han guardado correctamente",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al actualizar estado",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400
            };
        }

        // Eliminar usuario
        [HttpDelete]
        [Route("{id}")]
        public object EliminarUsuario(int id)
        {
            var eliminado = db.BorrarUsuario(id);
            if (eliminado > 0)
            {
                return new
                {
                    Titulo = "Éxito al eliminar",
                    Mensaje = "El usuario ha sido eliminado correctamente.",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al eliminar",
                Mensaje = "Hubo un problema al eliminar el usuario.",
                Code = 400
            };
        }

        [HttpPost]
        [Route("acceso/entrada")]
        public object RegistrarEntrada([FromBody] string qrCodigo)
        {
            var usuario = db.BuscarUsuarioPorQR(qrCodigo);
            if (usuario.Count == 0)
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Código QR inválido.",
                    Code = 404
                };
            }

            if (usuario[0].estado != "General" && usuario[0].estado != "Premium")
            {
                return new
                {
                    Titulo = "Acceso Denegado",
                    Mensaje = "El usuario no tiene acceso al gimnasio.",
                    Code = 403
                };
            }

            if (qrCodigo.Length == 4 && qrCodigo[0] == 'P')
            {
                bool invitadoEntradaActiva = db.ValidarInvitadoEntradaAvtiva(usuario[0].id, "Invitado");
                if (invitadoEntradaActiva)
                {
                    return new
                    {
                        Titulo = "Error",
                        Mensaje = "El invitado ya tiene una entrada activa. Registre su salida antes de intentar ingresar nuevamente.",
                        Code = 400
                    };
                }
            }
            else
            {
                bool tieneEntradaActiva = db.ValidarEntradaActiva(usuario[0].id);
                if (tieneEntradaActiva)
                {
                    return new
                    {
                        Titulo = "Error",
                        Mensaje = "El usuario ya tiene una entrada activa. Registre su salida antes de intentar ingresar nuevamente.",
                        Code = 400
                    };
                }
            }

            if (!CapacidadValida())
            {
                return new
                {
                    Titulo = "Capacidad Llena",
                    Mensaje = "El gimnasio ha alcanzado su capacidad máxima.",
                    Code = 400
                };
            }

            var entradaRegistrada = db.RegistrarEntrada(usuario[0].id, "Entrada", qrCodigo);
            if (entradaRegistrada)
            {
                var reserva = db.ObtenerReservaPorId(usuario[0].id);
                if (reserva > 0)
                {
                    db.ActualizarCapacidad(0);
                    db.ActualizarEstadoReserva(usuario[0].id);
                }
                else
                {
                    db.ActualizarCapacidad(1);
                }
                return new
                {
                    Titulo = "Acceso Permitido",
                    Mensaje = "Entrada registrada exitosamente.",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error",
                Mensaje = "Ocurrió un error al registrar la entrada.",
                Code = 500
            };
        }


        // Registrar salida
        [HttpPost]
        [Route("acceso/salida/{qrCodigo}")]
        public object RegistrarSalida([FromBody] int usuarioID, string qrCodigo)
        {
            if (qrCodigo.Length == 4 && qrCodigo[0] == 'P')
            {
                int salidaInvValida = db.ValidarInvitadoSalida(usuarioID);
                if (salidaInvValida > 0)
                {
                    return new
                    {
                        Titulo = "Error al registrar salida",
                        Mensaje = "No se encontró un registro de entrada válido desde la última salida registrada para este invitado. Verifique e intente nuevamente.",
                        Code = 400
                    };
                }
            }
            else
            {
                int salidaValida = db.ValidarSalida(usuarioID);
                if (salidaValida > 0)
                {
                    return new
                    {
                        Titulo = "Error al registrar salida",
                        Mensaje = "No se encontró un registro de entrada válido desde la última salida registrada para este usuario. Verifique e intente nuevamente.",
                        Code = 400
                    };
                }
            }

            var salidaRegistrada = db.RegistrarSalida(usuarioID, "Salida", qrCodigo);

            if (salidaRegistrada)
            {
                db.ActualizarCapacidad(-1);
                return new
                {
                    Titulo = "Salida registrada",
                    Mensaje = "El registro de salida se realizó correctamente.",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error",
                Mensaje = "Ocurrió un error al registrar la salida.",
                Code = 500
            };
        }
        #endregion

        #region VistaWeb
        // Proceso de inscripcion del usuario
        [HttpPost]
        [Route("inscribir")]
        public object InscribirUsuario(UsuarioRequest usuario)
        {
            if (!Valido(usuario.estado))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "El estado del usuario no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400
                };
            }

            if (db.ExisteCedula(usuario.cedula))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409
                };
            }

            string contraseñaEncriptada = seguridad.Encriptar(usuario.contraseña);

            var usuariO = new UsuarioRequest
            {
                nombre = usuario.nombre,
                apellido = usuario.apellido,
                contraseña = contraseñaEncriptada,
                cedula = usuario.cedula,
                edad = usuario.edad,
                estado = usuario.estado
            };

            var guardado = db.InsertarUsuario(usuariO);
            if (guardado > 0)
            {
                return new
                {
                    Titulo = "Exito al guardar",
                    Mensaje = "Los datos se han guardado correctamente",
                    Code = 200
                };
            }
            return new
            {
                Titulo = "Error al guardar",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400
            };
        }

        // Inicio de sesion del usuario
        [HttpPost]
        [Route("login")]
        public object IniciarSesion(InicioSesionRequest request)
        {
            var usuario = db.ObtenerUsuarioPorCredenciales(request.cedula, request.contraseña);

            if (usuario.Count == 0) {
                return new
                {
                    Titulo = "Error de autenticación",
                    Mensaje = "Cédula incorrecta.",
                    Code = 401
                };
            }
            var contraseñaDesencriptada = seguridad.Desencriptar(usuario[0].contraseña);
            if (contraseñaDesencriptada == request.contraseña) {
                return new
                {
                    Titulo = "Inicio de sesión exitoso",
                    Mensaje = "Bienvenido al sistema.",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error de autenticación",
                Mensaje = "Contraseña incorrecta.",
                Code = 401
            };
        }

        // Edición propia de los datos del usuario
        [HttpPost]
        [Route("editar-datos{id}")]
        public object EditarUsuarioWeb(int id, EditarWeb editar)
        {
            if (db.ExisteCedulaParaEditar(id, editar.cedula))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409
                };
            }

            string contraseñaEncriptada = seguridad.Encriptar(editar.contraseña);
            
            var editaR = new EditarWeb
            {
                nombre = editar.nombre,
                apellido = editar.apellido,
                contraseña = contraseñaEncriptada,
                cedula = editar.cedula,
                edad = editar.edad
            };

            var editado = db.ActualizarUsuarioWeb(id, editaR);
            if (editado > 0)
            {
                return new
                {
                    Titulo = "Exito al editar",
                    Mensaje = "Los datos se han editado correctamente",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al guardar",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400
            };
        }

        // Generar codigo de codigoQR del usuario
        [HttpGet]
        [Route("QR/{cedula}")]
        public string ObtenerQR(string cedula)
        {
            return db.QRUsuario(cedula);
        }

        // Generar codigo de codigoQR del invitado (de ser necesario)
        [HttpGet]
        [Route("QR-inv/{cedula}")]
        public string ObtenerQRInv(string cedula)
        {
            return db.QRUsuarioInv(cedula);
        }
        #endregion

        #region VistaAmbos

        // Darse de baja del gimnasio
        [HttpPost]
        [Route("retirarse/{id}")]
        public object DarseDeBaja(int id, [FromBody] string estado)
        {
            if (!db.ExisteUsuario(id))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Este usuario no esta en nuestra base de datos.",
                    Code = 409
                };
            }

            var actualizado = db.ActualizarEstado(id, estado);
            if (actualizado > 0)
            {
                return new
                {
                    Titulo = "Exito",
                    Mensaje = "Los datos se han guardado correctamente",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al actualizar estado",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400
            };
        }

        // Creacion de las reservas
        [HttpPost]
        [Route("reservas/crear")]
        public object CrearReserva([FromBody] ReservaRequest reserva)
        {
            if (!CapacidadValida())
            {
                return new
                {
                    Titulo = "Capacidad llena",
                    Mensaje = "No hay disponibilidad para realizar la reserva en este momento.",
                    Code = 400
                };
            }

            var reservas = db.ValidarReservaPorId(reserva.usuarioId, reserva.fechaReserva);
            if (reservas > 0)
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Ya hay una reserva para esta fecha.",
                    Code = 400
                };
            }

            var reservaCreada = db.CrearReserva(reserva);
            if (reservaCreada)
            {
                db.ActualizarCapacidad(1);
                return new
                {
                    Titulo = "Reserva confirmada",
                    Mensaje = "La reserva se ha realizado correctamente.",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al crear reserva",
                Mensaje = "Hubo un problema al crear la reserva. Por favor intente de nuevo.",
                Code = 500
            };
        }
        #endregion
    }
}