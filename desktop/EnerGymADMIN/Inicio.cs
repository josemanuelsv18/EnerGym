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
    public partial class Inicio : Form
    {
        GimnasioService gimnasioService;
        public Inicio()
        {
            InitializeComponent();
            gimnasioService = new GimnasioService();
        }

        private async void Inicio_Load(object sender, EventArgs e)
        {
            string ocupacionPorcentual = await gimnasioService.MostrarOcuapcionPorcentual();
            string ocupacionTexto = await gimnasioService.MostrarOcupacionTexto();

            lblOcupacionTexto.Text = ocupacionTexto + " (" + ocupacionPorcentual + ")";
        }

        private void btnGestion_Click(object sender, EventArgs e)
        {
            GestionUsuarios gestionUsuarios = new GestionUsuarios();
            this.Hide();
            gestionUsuarios.ShowDialog();
            this.Show();
        }

        private void btnAcceso_Click(object sender, EventArgs e)
        {
            Acceso acceso = new Acceso();
            this.Hide();
            acceso.ShowDialog();
            this.Show();
        }

        private void btnClases_Click(object sender, EventArgs e)
        {
            ClasesGrupales clasesGrupales = new ClasesGrupales();
            this.Hide();
            clasesGrupales.ShowDialog();
            this.Show();
        }
    }
}
