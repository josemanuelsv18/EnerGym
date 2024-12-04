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

        #region VistaDesktop
        // Obtener la ocupación actual del gimnasio
        [HttpGet]
        [Route("capacidad-actual")]
        public string ObtenerOcupacionActual()
        {
            return db.GimnasioOcupacionTexto();
        }
        #endregion

        #region VistaWeb
        // Obtener la ocupación actual del gimnasio en porcentaje
        [HttpGet]
        [Route("capacidad%")]
        public string ObtenerOcupacionPor()
        {
            return db.GimnasioOcupacionPorcentual();
        }

        [HttpGet]
        [Route("entrenadores")]
        public List<Entrenador> TraerEntrenadores()
        {
            return db.ObtenerEntrenadores();
        }
        #endregion
    }
}