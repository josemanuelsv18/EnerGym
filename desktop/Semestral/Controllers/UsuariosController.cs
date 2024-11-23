using Semestral.Datos;
using Semestral.Models;

using Microsoft.AspNetCore.Mvc;
using System.Text;
using Azure.Core;
using ZXing;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Identity.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Semestral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private DB db = new DB();
        private Seguridad seguridad = new Seguridad();

        #region VistaDesktop
        private bool Valido(string estado)
        {
            return estado == "Premium" || estado == "General" || estado == "Moroso" || estado == "Retirado";
        }

        private bool ValidoMetodoPago(string metodoPago)
        {
            return metodoPago == "Tarjeta" || metodoPago == "Efectivo";
        }

        private bool CapacidadValida()
        {
            return db.GimnasioCapacidadActual() < 100;
        }

        // Registrar nuevos usuarios
        [HttpPost]
        [Route("guardar")]
        public ApiResponse<object> GuardarUsuario(UsuarioRequest usuario)
        {
            if (!Valido(usuario.estado))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "El estado del usuario no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400,
                    Data = null
                };
            }

            if (db.ExisteCedula(usuario.cedula))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409,
                    Data = null
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
                return new ApiResponse<object>
                {
                    Titulo = "Exito al guardar",
                    Mensaje = "Los datos se han guardado correctamente",
                    Code = 200,
                    Data = null
                };
            }
            return new ApiResponse<object>
            {
                Titulo = "Error al guardar",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400,
                Data = null
            };
        }

        // Editar datos de usuarios
        [HttpPost]
        [Route("editar/{id}")]
        public ApiResponse<object> EditarUsuario(int id, UsuarioRequest usuario)
        {
            if (!Valido(usuario.estado))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "El estado del usuario no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400,
                    Data = null
                };
            }

            if (db.ExisteCedulaParaEditar(id, usuario.cedula))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409,
                    Data = null
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

            var editado = db.ActualizarUsuario(id, usuario);
            if (editado > 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Exito al editar",
                    Mensaje = "Los datos se han editado correctamente",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error al guardar",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400,
                Data = null
            };
        }

        // Buscar usuarios por nombre, apellido y número de cédula
        [HttpGet]
        [Route("buscar")]
        public List<Usuario> BuscarUsuario(string nombre = null, string apellido = null, string cedula = null)
        {
            return db.BuscarUsuario(nombre, apellido, cedula);
        }

        // Actualizar estado del usuario
        [HttpPost]
        [Route("estado/{id}")]
        public ApiResponse<object> ActualizarEstadoUsuario(int id, [FromBody] string nuevoEstado)
        {
            if (!Valido(nuevoEstado))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "El nuevo estado a insertar no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400,
                    Data = null
                };
            }

            if (!db.ExisteUsuario(id))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Este usuario no esta en nuestra base de datos.",
                    Code = 409,
                    Data = null
                };
            }

            var actualizado = db.ActualizarEstado(id, nuevoEstado);
            if (actualizado > 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Exito al guardar",
                    Mensaje = "Los datos se han guardado correctamente",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error al actualizar estado",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400,
                Data = null
            };
        }

        // Eliminar usuario
        [HttpDelete]
        [Route("{id}")]
        public ApiResponse<object> EliminarUsuario(int id)
        {
            var eliminado = db.BorrarUsuario(id);
            if (eliminado > 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Éxito al eliminar",
                    Mensaje = "El usuario ha sido eliminado correctamente.",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error al eliminar",
                Mensaje = "Hubo un problema al eliminar el usuario.",
                Code = 400,
                Data = null
            };
        }

        // Registrar targeta
        [HttpPost]
        [Route("tarjeta/guardar")]
        public ApiResponse<object> GuardarTarjeta([FromBody] TarjetaRequest request)
        {
            if (db.ExisteTarjeta(request.usuarioID, request.numeroTarjeta))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Ya existe una tarjeta registrada para este usuario.",
                    Code = 409,
                    Data = null
                };
            }

            string numeroTarjetaEcriptada = seguridad.Encriptar(request.numeroTarjeta);

            var tarjeta = new TarjetaRequest
            {
                usuarioID = request.usuarioID,
                numeroTarjeta = numeroTarjetaEcriptada,
                nombreTitular = request.nombreTitular,
                FechaExpiracionMes = request.FechaExpiracionMes,
                FechaExpiracionYear = request.FechaExpiracionYear
            };

            var guardado = db.InsertarTarjeta(tarjeta);
            if (guardado > 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Éxito al guardar tarjeta",
                    Mensaje = "La tarjeta se ha guardado correctamente.",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error al guardar tarjeta",
                Mensaje = "Hubo un problema al guardar la tarjeta.",
                Code = 400,
                Data = null
            };
        }

        // Registar deudas
        [HttpPost]
        [Route("deudas")]
        public ApiResponse<object> RegistrarDeudas([FromBody] DeudasRequest request)
        {
            if (!db.ExisteUsuario(request.usuarioID))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Este usuario no esta en nuestra base de datos.",
                    Code = 409,
                    Data = null
                };
            }

            var registrado = db.InsertarDeudas(request);
            if (registrado > 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Exito al registrar",
                    Mensaje = "La deuda se ha registrado correctamente",
                    Code = 200,
                    Data = null
                };
            }
            return new ApiResponse<object>
            {
                Titulo = "Error al registrar",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400,
                Data = null
            };
        }

        // Buscar deudas por usuario
        [HttpGet]
        [Route("deudas/{id}")]
        public List<Deudas> ObtenerDeudasPorUsuario(int usuarioId)
        {
            return db.ObtenerDeudaPorId(usuarioId);
        }

        // Realizar/Registrar pagos
        [HttpPost]
        [Route("pagos")]
        public ApiResponse<object> RealizarPago([FromBody] PagoRequest pago)
        {
            List<Deudas> deuda = db.ObtenerDeudaPorId(pago.usuarioID);
            if (deuda.Count() == 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Esta deuda no existe en nuestra base de datos.",
                    Code = 409,
                    Data = null
                };
            }

            if (!ValidoMetodoPago(pago.metodoPago))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Método de pago no válido. Valores permitidos: Tarjeta, Efectivo.",
                    Code = 400,
                    Data = null
                };
            }

            bool pagoExitoso = db.RegistrarPago(pago);
            if (pagoExitoso)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Exito al realizar pago",
                    Mensaje = "El pago se ha realizado correctamente",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error al realizar pago",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400,
                Data = null
            };
        }

        // Validar acceso de usuario
        [HttpPost]
        [Route("acceso/entrada")]
        public ApiResponse<object> RegistrarEntrada([FromBody] string qrCodigo)
        {
            var usuario = db.BuscarUsuarioPorQR(qrCodigo);
            if (usuario != null)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Código QR inválido.",
                    Code = 404,
                    Data = null
                };
            }

            if (usuario[0].estado != "General" || usuario[0].estado != "Premium")
            {
                return new ApiResponse<object>
                {
                    Titulo = "Acceso Denegado",
                    Mensaje = "El usuario no tiene acceso al gimnasio.",
                    Code = 403,
                    Data = null
                };
            }

            if (!CapacidadValida())
            {
                return new ApiResponse<object>
                {
                    Titulo = "Capacidad Llena",
                    Mensaje = "El gimnasio ha alcanzado su capacidad máxima.",
                    Code = 400,
                    Data = null
                };
            }

            var entradaRegistrada = db.RegistrarEntrada(usuario[0].id, "Entrada", "Generado");
            if (entradaRegistrada)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Acceso Permitido",
                    Mensaje = "Entrada registrada exitosamente.",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error",
                Mensaje = "Ocurrió un error al registrar la entrada.",
                Code = 500,
                Data = null
            };
        }

        // Registrar salida
        [HttpPost]
        [Route("acceso/salida")]
        public ApiResponse<object> RegistrarSalida(int usuarioID)
        {
            int salida = db.RegistrarSalida(usuarioID);

            if (salida > 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Salida registrada",
                    Mensaje = "El registro de salida se realizó correctamente.",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error al registrar salida",
                Mensaje = "No se encontró un registro de entrada válido en las últimas 5 horas. Verifique e intente nuevamente.",
                Code = 400,
                Data = null
            };
        }
        #endregion

        #region VistaWeb
        [HttpPost]
        [Route("login")]
        public ApiResponse<object> IniciarSesion(InicioSesionRequest request)
        {
            var usuario = db.ObtenerUsuarioPorCredenciales(request.cedula, request.contraseña);

            if (usuario.Count == 0) {
                return new ApiResponse<object>
                {
                    Titulo = "Error de autenticación",
                    Mensaje = "Cédula incorrecta.",
                    Code = 401,
                    Data = null
                };
            }
            var xD = 0;
            var contraseñaDesencriptada = seguridad.Desencriptar(usuario[0].contraseña);
            if (contraseñaDesencriptada == request.contraseña) {
                return new ApiResponse<object>
                {
                    Titulo = "Inicio de sesión exitoso",
                    Mensaje = "Bienvenido al sistema.",
                    Code = 200,
                    Data = new { UsuarioID = usuario[0].id, Nombre = usuario[0].nombre }
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error de autenticación",
                Mensaje = "Contraseña incorrecta.",
                Code = 401,
                Data = null
            };
        }

        [HttpPost]
        [Route("retirarse/{id}")]
        public ApiResponse<object> DarseDeBaja(int id) {
            if (!db.ExisteUsuario(id))
            {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Este usuario no esta en nuestra base de datos.",
                    Code = 409,
                    Data = null
                };
            }

            var actualizado = db.ActualizarEstado(id, "Retirado");
            if (actualizado > 0)
            {
                return new ApiResponse<object>
                {
                    Titulo = "Exito",
                    Mensaje = "Los datos se han guardado correctamente",
                    Code = 200,
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Titulo = "Error al actualizar estado",
                Mensaje = "Hubo un error con los datos, por favor revisar.",
                Code = 400,
                Data = null
            };
        }
        #endregion
    }
}