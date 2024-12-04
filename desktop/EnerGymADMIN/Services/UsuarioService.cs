using EnerGymADMIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Forms;
using EnerGymADMIN.GestionUsuariosForm.BuscarUsuariosForm;

namespace EnerGymADMIN.Services
{
    public class UsuarioService
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage respuesta = new HttpResponseMessage();
        public async Task<List<Usuario>> BuscarUsuario(string busqueda, string dato) {
            if (string.IsNullOrEmpty(busqueda) && string.IsNullOrEmpty(dato)) {
                respuesta = await client.GetAsync("https://localhost:7274/api/usuarios/buscar");
            }
            else if (busqueda == "Apellido")
            {
                respuesta = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar?apellido={dato}");
            }
            else if (busqueda == "Cédula")
            {
                respuesta  = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar?cedula={dato}");
            }
            else if (busqueda == "Nombre")
            {
                respuesta = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar?nombre={dato}");
            }

            return JsonConvert.DeserializeObject<List<Usuario>>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> GuardarUsuarios(UsuarioRequest usuario) {
            var datos = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync("https://localhost:7274/api/usuarios/guardar", data);
            
            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> ActualizarEstado(int id, string nuevoEstado) {
            
            var datos = JsonConvert.SerializeObject(nuevoEstado);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync($"https://localhost:7274/api/usuarios/estado/{id}", data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> ActualizarUsuario(int id, EditarRequest editar)
        {
            var datos = JsonConvert.SerializeObject(editar);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync($"https://localhost:7274/api/usuarios/editar/{id}", data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> AccesoEntrada(string qrCodigo) {
            var datos = JsonConvert.SerializeObject(qrCodigo);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync("https://localhost:7274/api/usuarios/acceso/entrada", data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> AccesoSalida(int usuarioID, string qrCodigo)
        {
            var datos = JsonConvert.SerializeObject(usuarioID);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync($"https://localhost:7274/api/usuarios/acceso/salida/{qrCodigo}", data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<AdminLogin> AdminAcceso(string usuario) {
            var datos = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.GetAsync($"https://localhost:7274/api/admin/credenciales{usuario}");

            return JsonConvert.DeserializeObject<AdminLogin>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> RetirarseGym(int id, string estado)
        {
            var datos = JsonConvert.SerializeObject(estado);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync($"https://localhost:7274/api/usuarios/retirarse/{id}",data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> EliminarUsuario(int id)
        {
            respuesta = await client.DeleteAsync($"https://localhost:7274/api/clases/{id}");

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<Respuesta> ReservarPuesto(ReservaRequest reserva)
        {
            var datos = JsonConvert.SerializeObject(reserva);
            var data = new StringContent(datos, Encoding.UTF8, "application/json");
            respuesta = await client.PostAsync("https://localhost:7274/api/usuarios/reservas/crear", data);

            return JsonConvert.DeserializeObject<Respuesta>(respuesta.Content.ReadAsStringAsync().Result);
        }

        public async Task<List<Reserva>> BuscarReservas(int busqueda, string nombre, int estado)
        {
            if (busqueda == -1 && string.IsNullOrEmpty(nombre) && estado == -1)
            {
                respuesta = await client.GetAsync("https://localhost:7274/api/usuarios/buscar/reservas");
            }
            else if (busqueda == 1)
            {
                respuesta = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar/reservas?nombre={nombre}");
            }
            else if (busqueda == 0)
            {
                if (estado == 0)
                {
                    respuesta = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar/reservas?estado=Activa");
                }
                else
                {
                    respuesta = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar/reservas?estado=Cumplida");
                }
            }
            else if (busqueda == 2)
            {
                if (estado == 0)
                {
                    respuesta = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar/reservas?nombre={nombre}&estado=Activa");
                }
                else
                {
                    respuesta = await client.GetAsync($"https://localhost:7274/api/usuarios/buscar/reservas?nombre={nombre}&estado=Cumplida");
                }
            }

            return JsonConvert.DeserializeObject<List<Reserva>>(respuesta.Content.ReadAsStringAsync().Result);
        }
    }
}