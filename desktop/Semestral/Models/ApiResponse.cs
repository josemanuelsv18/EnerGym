namespace Semestral.Models
{
    public class ApiResponse<T>
    {
        public string Titulo { get; set; }
        public string Mensaje {  get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
    }
}