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
    public class GimnasioService
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage respuesta = new HttpResponseMessage();

        public async Task<string> MostrarOcuapcionPorcentual()
        {
            respuesta = await client.GetAsync("https://localhost:7274/api/gimnasio/capacidad%");

            string ocupacion = await respuesta.Content.ReadAsStringAsync();

            return ocupacion;
        }

        public async Task<string> MostrarOcupacionTexto()
        {
            respuesta = await client.GetAsync("https://localhost:7274/api/gimnasio/capacidad-actual");

            string ocupacion = await respuesta.Content.ReadAsStringAsync();

            return ocupacion;
        }
    }
}
