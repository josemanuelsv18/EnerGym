using Semestral.Datos;
using Semestral.Models;

using Microsoft.AspNetCore.Mvc;
using System.Text;
using Azure.Core;

namespace Semestral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private DB db = new DB();
        private Seguridad seguridad = new Seguridad();

        private bool Valido(string estado) {
            return estado == "Premium" || estado == "General" || estado == "Moroso" || estado == "Retirado";
        }

        private bool ValidoMetodoPago (string metodoPago)
        {
            return metodoPago == "Tarjeta" || metodoPago == "Efectivo";
        }

        // Registrar nuevos usuarios
        [HttpPost]
        [Route("guardar")]
        public ApiResponse<object> GuardarUsuario(UsuarioRequest usuario) {
            if (!Valido(usuario.estado)) {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "El estado del usuario no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400,
                    Data = null
                };
            }

            if (db.ExisteCedula(usuario.cedula)) {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409,
                    Data = null
                };
            }

            var guardado = db.InsertarUsuario(usuario);
            if (guardado > 0) {
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
        public ApiResponse<object> EditarUsuario(int id, UsuarioRequest usuario) {
            if (!Valido(usuario.estado)) {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "El estado del usuario no es válido. Valores permitidos: Premium, General, Moroso, Retirado.",
                    Code = 400,
                    Data = null
                };
            }

            if (db.ExisteCedulaParaEditar(id, usuario.cedula)) {
                return new ApiResponse<object>
                {
                    Titulo = "Error",
                    Mensaje = "Esta cédula ya está registrada.",
                    Code = 409,
                    Data = null
                };
            }

            var editado = db.ActualizarUsuario(id, usuario);
            if (editado > 0) {
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
        public List<Usuario> BuscarUsuario(string nombre = null, string apellido = null, string cedula = null) {
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
            if (actualizado > 0) {
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
        public ApiResponse<object> EliminarUsuario(int id) {
            var eliminado = db.BorrarUsuario(id);
            if (eliminado > 0) {
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
        public ApiResponse<object> GuardarTarjeta([FromBody] TarjetaRequest request) {
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
            if (guardado > 0) {
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
        public ApiResponse<object> RegistrarDeudas([FromBody] DeudasRequest request) {
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
        public List<Deudas> ObtenerDeudasPorUsuario(int usuarioId) {
            return db.ObtenerDeudaPorId(usuarioId);
        }

        // Realizar/Registrar pagos
        [HttpPost]
        [Route("pagos")]
        public ApiResponse<object> RealizarPago([FromBody] PagoRequest pago) {
            List<Deudas> deuda = db.ObtenerDeudaPorId(pago.usuarioID);
            if (deuda.Count() == 0) {
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
            if (pagoExitoso) {
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
    }
}