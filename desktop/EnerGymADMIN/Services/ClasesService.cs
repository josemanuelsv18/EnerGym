using EnerGymADMIN.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EnerGymADMIN.Services
{
    public class ClasesService
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage respuesta = new HttpResponseMessage();

        public async Task<List<Clases>> BuscarClases(string busqueda, string dato)
        {
            if (string.IsNullOrEmpty(busqueda) && string.IsNullOrEmpty(dato))
            {
                respuesta = await client.GetAsync("https://localhost:7274/api/clases/buscar");
            }
            else if (busqueda == "Entrenador")
            {
                respuesta = await client.GetAsync($"https://localhost:7274/api/clases/buscar?entrenador={dato}");
            }
            else if (busqueda == "Estado")
            {
                respuesta = await client.GetAsync($"https://localhost:7274/api/clases/buscar?estado={dato}");
            }

            return JsonConvert.DeserializeObject<List<Clases>>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<List<Entrenador>> ObtenerNombreEntrenadores()
        {
            respuesta = await client.GetAsync($"https://localhost:7274/api/clases/entrenadores");

            List<Entrenador> entrenadores = JsonConvert.DeserializeObject<List<Entrenador>>(respuesta.Content.ReadAsStringAsync().Result);

            return entrenadores;
        }

        public async Task<Respuesta> EliminarClase(int id)
        {
            respuesta = await client.DeleteAsync($"https://localhost:7274/api/clases/{id}");

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> ActualizarEstado(int id, string nuevoEstado)
        {

            var datos = JsonConvert.SerializeObject(nuevoEstado);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync($"https://localhost:7274/api/clases/estado/{id}", data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> GuardarClase(ClasesRequest clase)
        {
            var datos = JsonConvert.SerializeObject(clase);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync("https://localhost:7274/api/clases/crear", data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }
    }
}