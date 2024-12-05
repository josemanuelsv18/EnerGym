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

namespace EnerGymADMIN.GestionUsuariosForm
{
    public partial class RegistrarUsuarios : Form
    {
        UsuarioService usuarioService;
        string estadoSelec;

        public RegistrarUsuarios()
        {
            InitializeComponent();
            usuarioService = new UsuarioService();
            txtCedula.MaxLength = 12;
        }

        private void RegistrarUsuarios_Load(object sender, EventArgs e)
        {
            txtCedula.Text = "XX-XXXX-XXXX";
            txtCedula.ForeColor = Color.LightGray;
        }

        private void txtCedula_Enter(object sender, EventArgs e)
        {
            if (txtCedula.Text == "XX-XXXX-XXXX")
            {
                txtCedula.Text = "";
                txtCedula.ForeColor = Color.Black;
            }
        }

        private void txtCedula_Leave(object sender, EventArgs e)
        {
            if (txtCedula.Text == "")
            {
                txtCedula.Text = "XX-XXXX-XXXX";
                txtCedula.ForeColor = Color.LightGray;
            }
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtCedula.Text) || nudEdad.Value < 15)
            {
                MessageBox.Show("Debe rellenar todos los datos, por favor corrija.", "Error");
            }
            else
            {
                var usuario = new UsuarioRequest()
                {
                    nombre = txtNombre.Text,
                    apellido = txtApellido.Text,
                    contraseña = txtNombre.Text.ToLower(),
                    cedula = txtCedula.Text,
                    edad = Convert.ToInt32(nudEdad.Value),
                    estado = "General"
                };

                var respuesta = await usuarioService.GuardarUsuarios(usuario);
                MessageBox.Show(respuesta.Mensaje, respuesta.Titulo);
                this.Close();
            }
        }
    }
}