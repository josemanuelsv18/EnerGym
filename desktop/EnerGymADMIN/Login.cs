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

namespace EnerGymADMIN
{
    public partial class Login : Form
    {
        AdminLogin login;
        UsuarioService usuarioService;
        public Login()
        {
            InitializeComponent();
            login = new AdminLogin();
            usuarioService = new UsuarioService();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            var respuesta = await usuarioService.AdminAcceso(txtUsuario.Text);

            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContra.Text))
            {
                MessageBox.Show("Debe llenar el formulario para poder ingersar", "Error");
            }
            else
            {
                if (respuesta.usuario == txtUsuario.Text && respuesta.contras == txtContra.Text)
                {
                    MessageBox.Show("Se ha podido iniciar sesion", "Exito");
                    Inicio inicio = new Inicio();
                    this.Hide();
                    inicio.ShowDialog();
                    txtUsuario.Text = string.Empty;
                    txtContra.Text = string.Empty;
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Crendenciales incorrectas", "Error");
                }
            }
        }
    }
}