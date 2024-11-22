namespace Semestral.Models
{
    public class TarjetaRequest
    {
        public int usuarioID { get; set; }
        public string numeroTarjeta { get; set; } //Encriptar antes de guardar
        public string nombreTitular { get; set; }
        public int FechaExpiracionMes { get; set; }
        public int FechaExpiracionYear { get; set; }
    }
}