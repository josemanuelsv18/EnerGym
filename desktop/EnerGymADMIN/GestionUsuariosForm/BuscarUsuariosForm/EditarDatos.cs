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
    public partial class EditarDatos : Form
    {
        private Usuario usuarioSeleccionado;
        private UsuarioService usuarioService;
        public EditarDatos(Usuario usuario)
        {
            InitializeComponent();
            usuarioSeleccionado = usuario;
            usuarioService = new UsuarioService();

            txtNombre.Text = usuarioSeleccionado.nombre;
            txtApellido.Text = usuarioSeleccionado.apellido;
            txtCedula.Text = usuarioSeleccionado.cedula;
            nudEdad.Value = usuarioSeleccionado.edad;
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtCedula.Text) || nudEdad.Value == 0)
            {
                MessageBox.Show("No debe dejar ningún espacio vacío.", "Error");
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Seguro que deseas enviar estos datos?", "Confirmar Actualización", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    var usuarioActualizado = new EditarRequest
                    {
                        nombre = txtNombre.Text,
                        apellido = txtApellido.Text,
                        cedula = txtCedula.Text,
                        edad = Convert.ToInt32(nudEdad.Value)
                    };

                    var actualizado = await usuarioService.ActualizarUsuario(usuarioSeleccionado.id, usuarioActualizado);
                    MessageBox.Show(actualizado.Mensaje, actualizado.Titulo);
                    this.Close();
                }
            }
        }
    }
}