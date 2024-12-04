using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerGymADMIN.Models
{
    public class ClasesRequest
    {
        public string nombreClase { get; set; }
        public string nombreEntrenador { get; set; }
        public string horarioInicio { get; set; }
        public string horarioFinal { get; set; }
        public int estado { get; set; }
    }
}
