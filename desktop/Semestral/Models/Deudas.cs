namespace Semestral.Models
{
    public class Deudas
    {
        public int id { get; set; }
        public int usuarioID { get; set; }
        public double monto { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int estado { get; set; }
        public DateTime? fechaPago { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
