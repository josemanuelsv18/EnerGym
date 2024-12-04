using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnerGymADMIN.Models
{
    public class ReservaRequest
    {
        public int usuarioId { get; set; }
        public DateTime fechaReserva { get; set; }
    }
}