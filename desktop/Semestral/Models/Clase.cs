namespace Semestral.Models
{
    public class Clase
    {
        public int id {  get; set; }
        public string nombre { get; set; }
        public string entrenador { get; set; }
        public int estado { get; set; }
        public DateTime horarioInicio { get; set; }
        public DateTime horarioFinal { get; set; }
        public DateTime createddAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}