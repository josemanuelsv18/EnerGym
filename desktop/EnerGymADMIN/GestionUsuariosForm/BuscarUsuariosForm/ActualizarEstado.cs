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
    public partial class ActualizarEstado : Form
    {
        private Usuario usuarioSeleccionado;
        private UsuarioService usuarioService;
        public ActualizarEstado(Usuario usuario)
        {
            InitializeComponent();
            usuarioSeleccionado = usuario;
            usuarioService = new UsuarioService();
        }

        private void ActualizarEstado_Load(object sender, EventArgs e)
        {
            lblEstado.Text = usuarioSeleccionado.estado;
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe elegir un nuevo estado para el usuario", "Error");
            }

            if (cmbEstado.SelectedIndex != -1)
            {
                var cambio = await usuarioService.ActualizarEstado(usuarioSeleccionado.id, cmbEstado.SelectedItem.ToString());
                MessageBox.Show(cambio.Mensaje, cambio.Titulo);
                this.Close();
            }
        }
    }
}