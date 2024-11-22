namespace Semestral.Models
{
    public class PagoRequest
    {
        public int usuarioID {  get; set; }
        public double monto { get; set; }
        public string metodoPago { get; set; }
    }
}