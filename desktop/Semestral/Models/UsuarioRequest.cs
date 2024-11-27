namespace Semestral.Models
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