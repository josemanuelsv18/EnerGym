namespace Semestral.Models
{
    public class ClaseRequest
    {
        public string nombre { get; set; }
        public string entrenador { get; set; }
        public DateTime horarioInicio { get; set; }
        public DateTime horarioFinal { get; set; }
    }
}