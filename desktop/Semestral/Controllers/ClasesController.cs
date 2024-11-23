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

        //Registrar una clase
        [HttpPost]
        [Route("guardar")]
        public ApiResponse<object> CrearClaseGrupal(ClaseRequest request) {
            var guardado = db.GuardarClaseGrupal(request);
            if(guardado > 0)
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

        // Obtener todas las clases disponibles
        [HttpGet]
        [Route("disponibles")]
        public List<Clase> ObtenerClasesDisponibles()
        {
            return db.ObtenerClases();
        }
    }
}