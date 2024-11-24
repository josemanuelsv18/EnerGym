using Semestral.Datos;
using Semestral.Models;

using Microsoft.AspNetCore.Mvc;

namespace Semestral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GimnasioController : ControllerBase
    {
        private DB db = new DB();

        // Obtener capacidad actual del gimnasio
        [HttpGet]
        [Route("capacidad-actual")]
        public int ObtenerCapacidadActual() {
            return db.GimnasioCapacidadActual();
        }

        [HttpGet]
        [Route("capacidad%")]
        public string ObtenerCapacidadPor()
        {
            return db.GimnasioCapacidadPorcentual();
        }

        [HttpGet]
        [Route("horarios")]
        public List<Horarios> ObtenerHorarios()
        {
            return db.ObtenerHorarios();
        }
    }
}