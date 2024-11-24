namespace Semestral.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string contraseña { get; set; } //Encriptar antes de guardar
        public string cedula { get; set; }
        public int edad {  get; set; }
        public string estado { get; set; }
        public string QRcodigo { get; set; }
        public string QRinvitado { get; set; }
        public DateTime fechaInscripcion { get; set; }
        public DateTime updatedAt { get; set; }
    }
}