using EnerGymADMIN.Models;
using EnerGymADMIN.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnerGymADMIN.GestionUsuariosForm.BuscarUsuariosForm
{
    public partial class ReservarPuesto : Form
    {
        private Usuario usuarioSeleccionado;
        private UsuarioService usuarioService;
        public ReservarPuesto(Usuario usuario)
        {
            InitializeComponent();
            usuarioSeleccionado = usuario;
            usuarioService = new UsuarioService();
        }

        private async void btnAsignar_Click(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = dtpFecha.Value;

            DateTime fechaSola = fechaSeleccionada.Date;

            if (fechaSola < DateTime.Now)
            {
                MessageBox.Show("No se puede seleccionar una fecha anterior a la de hoy", "Error");
            }
            else
            {
                var reserva = new ReservaRequest()
                {
                    usuarioId = usuarioSeleccionado.id,
                    fechaReserva = fechaSola
                };

                var respuesta = await usuarioService.ReservarPuesto(reserva);
                MessageBox.Show(respuesta.Mensaje, respuesta.Titulo);
                this.Close();
            }

            
        }
    }
}