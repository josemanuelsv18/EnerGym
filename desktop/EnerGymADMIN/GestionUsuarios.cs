using EnerGymADMIN.GestionUsuariosForm;
using EnerGymADMIN.GestionUsuariosForm.BuscarUsuariosForm;
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
    public partial class GestionUsuarios : Form
    {
        public GestionUsuarios()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuarios buscar = new BuscarUsuarios();
            this.Hide();
            buscar.ShowDialog();
            this.Show();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RegistrarUsuarios registrar = new RegistrarUsuarios();
            this.Hide();
            registrar.ShowDialog();
            this.Show();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            VerReservas verReservas = new VerReservas();
            this.Hide();
            verReservas.ShowDialog();
            this.Show();
        }
    }
}