using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using EnerGymADMIN.Services;
using ZXing;

namespace EnerGymADMIN
{
    public partial class Acceso : Form
    {
        public Acceso()
        {
            InitializeComponent();
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            AccesoEntrada entrada = new AccesoEntrada();
            this.Hide();
            entrada.ShowDialog();
            this.Show();
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            AccesoSalida salida = new AccesoSalida();
            this.Hide();
            salida.ShowDialog();
            this.Show();

        }
    }
}