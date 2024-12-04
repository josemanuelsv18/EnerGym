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
        // Buscar clases
        [HttpGet]
        [Route("buscar")]
        public List<Clase> BuscarClases(string estado = null, string entrenador = null)
        {
            return db.BuscarClases(estado, entrenador);
        }

        // Obtener nombre de entrenadores
        [HttpGet]
        [Route("entrenadores")]
        public List<EntrenadorNombre> ObtenerNombreEntrenadores()
        {
            return db.ObtenerEntrenadoresNombre();
        }

        // Eliminar clase
        [HttpDelete]
        [Route("{id}")]
        public object EliminarClase(int id)
        {
            var eliminado = db.BorrarClase(id);
            if (eliminado > 0)
            {
                return new
                {
                    Titulo = "Éxito al eliminar",
                    Mensaje = "La clase ha sido eliminado correctamente.",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al eliminar",
                Mensaje = "Hubo un problema al eliminar la clase.",
                Code = 400
            };
        }

        // Actualizar estado de la clase
        [HttpPost]
        [Route("estado/{id}")]
        public object ActualizarEstadoClase(int id, [FromBody] string nuevoEstado)
        {
            if (!db.ExisteClase(id))
            {
                return new
                {
                    Titulo = "Error",
                    Mensaje = "Este usuario no esta en nuestra base de datos.",
                    Code = 409
                };
            }

            var actualizado = db.ActualizarEstadoClase(id, nuevoEstado);
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

        [HttpPost]
        [Route("crear")]
        public object CrearClaseGrupal([FromBody] ClaseRequest clase)
        {
            if (Convert.ToDateTime(clase.horarioInicio) < DateTime.Now || Convert.ToDateTime(clase.horarioFinal) < DateTime.Now)
            {
                return new
                {
                    Titulo = "Error al crear clase",
                    Mensaje = "La fecha de inicio o final no puede ser anterior a la fecha actual.",
                    Code = 400
                };
            }

            var claseExistente = db.BuscarClasePorNombreHorarioYEstado(clase.nombreClase, clase.horarioInicio, clase.horarioFinal);
            if (claseExistente > 0)
            {
                return new
                {
                    Titulo = "Clase ya existente",
                    Mensaje = "Ya existe una clase pendiente con la misma información.",
                    Code = 400
                };
            }

            var creado = db.CrearClaseGrupal(clase);
            if (creado)
            {
                return new
                {
                    Titulo = "Exito al crear",
                    Mensaje = "La clase se ha creado exitosamente",
                    Code = 200
                };
            }

            return new
            {
                Titulo = "Error al crear clase",
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