using Semestral.Datos;
using Semestral.Models;

using Microsoft.AspNetCore.Mvc;

namespace Semestral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private DB db = new DB();

        #region VistaDesktop
        //Registrar una clase
        [HttpPost]
        [Route("guardar")]
        public object CrearClaseGrupal(ClaseRequest request)
        {
            var guardado = db.GuardarClaseGrupal(request);
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
        #endregion

        #region VistaWeb
        // Obtener todas las clases disponibles
        [HttpGet]
        [Route("disponibles")]
        public List<Clase> ObtenerClasesDisponibles()
        {
            return db.ObtenerClases();
        }
        #endregion
    }
}