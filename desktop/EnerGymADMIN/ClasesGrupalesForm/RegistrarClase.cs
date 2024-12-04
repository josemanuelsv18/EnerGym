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
    public partial class RegistrarClase : Form
    {
        private ClasesService clasesService;
        private List<Entrenador> entrenadores;
        public RegistrarClase()
        {
            InitializeComponent();
            clasesService = new ClasesService();
        }

        private async void RegistrarClase_Load(object sender, EventArgs e)
        {
            cmbEntrendor.Items.Clear();

            entrenadores = await clasesService.ObtenerNombreEntrenadores();
            foreach (Entrenador entrenador in entrenadores)
            {
                cmbEntrendor.Items.Add(entrenador.nombre);
            }
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {   
            string horaFormateadaIni = ((int)nudHoraFechaIni.Value).ToString("D2");
            string minFormateadoIni = ((int)nudMinFechaIni.Value).ToString("D2");

            string horaFormateadaFin = ((int)nudHoraFechaFin.Value).ToString("D2");
            string minFormateadoFin = ((int)nudMinFechaFin.Value).ToString("D2");


            string temaClase = txtTema.Text;
            string entrenador = cmbEntrendor.SelectedItem.ToString();
            string horarioIni = dtpFechaIni.Text + " " + horaFormateadaIni + ":" + minFormateadoIni;
            string horarioFin = dtpFechaFin.Text + " " + horaFormateadaFin + ":" + minFormateadoFin;

            var clase = new ClasesRequest()
            {
                nombreClase = temaClase,
                nombreEntrenador = entrenador,
                horarioInicio = horarioIni,
                horarioFinal = horarioFin,
                estado = 0
            };

            var respuesta = await clasesService.GuardarClase(clase);
            MessageBox.Show(respuesta.Mensaje, respuesta.Titulo);
            this.Close();
        }
    }
}
