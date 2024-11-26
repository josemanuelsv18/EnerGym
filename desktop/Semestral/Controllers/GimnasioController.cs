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
        public int ObtenerOcupacionActual()
        {
            return db.GimnasioOcupacionActual();
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

        // Obtener los horarios de apertura
        [HttpGet]
        [Route("horarios")]
        public List<Horarios> ObtenerHorarios()
        {
            return db.ObtenerHorarios();
        }
        #endregion
    }
}