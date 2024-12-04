using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerGymADMIN.Models
{
    public class UsuarioRequest
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string contraseña { get; set; }
        public string cedula { get; set; }
        public int edad { get; set; }
        public string estado { get; set; }
    }
}
