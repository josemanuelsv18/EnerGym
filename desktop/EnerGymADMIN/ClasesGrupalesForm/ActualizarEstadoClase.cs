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

namespace EnerGymADMIN.ClasesGrupalesForm
{
    public partial class ActualizarEstadoClase : Form
    {
        private Clases claseSeleccionada;
        private ClasesService clasesService;
        public ActualizarEstadoClase(Clases clase)
        {
            InitializeComponent();
            claseSeleccionada = clase;
            clasesService = new ClasesService();
        }

        private void ActualizarEstadoClase_Load(object sender, EventArgs e)
        {
            lblEstado.Text = claseSeleccionada.estado;
        }

        private async void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe elegir un nuevo estado para el usuario", "Error");
            }

            if (cmbEstado.SelectedIndex != -1)
            {
                var cambio = await clasesService.ActualizarEstado(claseSeleccionada.id, cmbEstado.SelectedItem.ToString());
                MessageBox.Show(cambio.Mensaje, cambio.Titulo);
                this.Close();
            }
        }
    }
}
