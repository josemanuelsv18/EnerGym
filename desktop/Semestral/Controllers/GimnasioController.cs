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

        private bool CapacidadValida()
        {
            return db.GimnasioCapacidadActual() < 100;
        }

        // Obtener capacidad actual del gimnasio
        [HttpGet]
        [Route("capacidad-actual")]
        public int ObtenerCapacidadActual() {
            return db.GimnasioCapacidadActual();
        }
    }
}