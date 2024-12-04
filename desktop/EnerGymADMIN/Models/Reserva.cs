using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerGymADMIN.Models
{
    public class Reserva
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fechaReserva { get; set; }
        public string estado { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}