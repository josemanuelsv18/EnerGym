﻿namespace Semestral.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public int edad {  get; set; }
        public string estado { get; set; }
        public DateTime fechaInscripcion { get; set; }
        public DateTime updatedAt { get; set; }
    }
}