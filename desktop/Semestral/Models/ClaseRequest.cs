namespace Semestral.Models
{
    public class ClaseRequest
    {
        public string nombreClase { get; set; }
        public string nombreEntrenador { get; set; }
        public string horarioInicio { get; set; }
        public string horarioFinal { get; set; }
        public int estado { get; set; }
    }
}