using Microsoft.AspNetCore.Mvc;
using Semestral.Datos;
using Semestral.Models;

namespace Semestral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private DB db = new DB();

        #region VistaDesktop
        // Login de la aplicacion Desktop
        [HttpGet]
        [Route("credenciales{usuario}")]
        public AdminLogin ObtenerCredenciales(string usuario)
        {
            return db.ObtenerLogin(usuario);
        }
        #endregion
    }
}