using EnerGymADMIN.ClasesGrupalesForm;
using EnerGymADMIN.GestionUsuariosForm.BuscarUsuariosForm;
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
    public partial class ClasesGrupales : Form
    {
        private ClasesService clasesService;
        private List<Clases> clases;
        private List<Entrenador> entrenadores;
        private Clases claseSeleccionada;
        public ClasesGrupales()
        {
            InitializeComponent();
            clases = new List<Clases>();
            clasesService = new ClasesService();
            claseSeleccionada = new Clases();

            lblError.Visible = false;
            btnRegistrar.Location = new Point(117, 372);
            btnActualizar.Visible = false;
            btnElim.Visible = false;
            cmbOpcion.SelectedIndex = -1;
        }

        public void LimpiarGridView()
        {
            dgvClases.DataSource = null;
            dgvClases.Rows.Clear();
            lblError.Visible = false;
        }

        private async void ClasesGrupales_Load(object sender, EventArgs e)
        {
            dgvClases.DataSource = null;
            cmbInfo.Items.Clear();
            entrenadores = await clasesService.ObtenerNombreEntrenadores();
        }

        private void cmbOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOpcion.SelectedIndex == -1 || string.IsNullOrEmpty(cmbOpcion.SelectedItem?.ToString()))
            {
                cmbInfo.Items.Clear();
            }
            else if (cmbOpcion.SelectedIndex == 0)
            {
                cmbInfo.Items.Clear();
                cmbInfo.Items.Add("Pendiente");
                cmbInfo.Items.Add("En Curso");
                cmbInfo.Items.Add("Finalizada");
            }
            else if (cmbOpcion.SelectedIndex == 1)
            {
                cmbInfo.Items.Clear();
                foreach (Entrenador entrenador in entrenadores)
                {
                    cmbInfo.Items.Add(entrenador.nombre);
                }
            }
            else
            {
                cmbInfo.Items.Clear();
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            var dato = cmbInfo.Text;

            if ((!string.IsNullOrEmpty(dato)) && (cmbOpcion.SelectedIndex == -1))
            {
                dgvClases.Visible = false;
                lblError.Text = "Debe colocar en que categoría quiere buscar";
                lblError.Visible = true;
            }

            if ((cmbOpcion.SelectedIndex == -1) && string.IsNullOrEmpty(dato))
            {
                lblError.Visible = false;
                clases = await clasesService.BuscarClases("", "");
                dgvClases.Visible = true;
                if (clases.Count == 0)
                {
                    dgvClases.Visible = false;
                    lblError.Text = "No hay registros por buscar";
                    lblError.Visible = true;
                }
            }
            else if ((cmbOpcion.SelectedIndex != -1) && string.IsNullOrEmpty(dato))
            {
                lblError.Visible = false;
                clases = await clasesService.BuscarClases("", "");
                dgvClases.Visible = true;
                if (clases.Count == 0)
                {
                    dgvClases.Visible = false;
                    lblError.Text = "No hay registros por buscar";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 1)
            {
                lblError.Visible = false;
                clases = await clasesService.BuscarClases("Entrenador", dato);
                dgvClases.Visible = true;
                if (clases.Count == 0)
                {
                    dgvClases.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 0)
            {
                lblError.Visible = false;
                clases = await clasesService.BuscarClases("Estado", dato);
                dgvClases.Visible = true;
                if (clases.Count == 0)
                {
                    dgvClases.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }

            dgvClases.DataSource = null;
            dgvClases.DataSource = clases;

            dgvClases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void dgvClases_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvClases.Columns[e.ColumnIndex].Name == "id")
                {
                    claseSeleccionada = clases[e.RowIndex];
                    btnActualizar.Visible = true;
                    btnRegistrar.Location = new Point(3, 372);
                    btnElim.Visible = true;
                }
                else
                {
                    btnRegistrar.Location = new Point(117, 372);
                    btnActualizar.Visible = false;
                    btnElim.Visible = false;
                    MessageBox.Show("Seleccione un id para actulizar", "Corregir");
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarEstadoClase actualizarEstadoClase = new ActualizarEstadoClase(claseSeleccionada);
            this.Hide();
            actualizarEstadoClase.ShowDialog();
            LimpiarGridView();
            this.Show();
            btnRegistrar.Location = new Point(117, 372);
            btnActualizar.Visible = false;
            btnElim.Visible = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RegistrarClase registrarClase = new RegistrarClase();
            this.Hide();
            registrarClase.ShowDialog();
            LimpiarGridView();
            this.Show();
            btnRegistrar.Location = new Point(117, 372);
            btnActualizar.Visible = false;
            btnElim.Visible = false;
        }

        private async void btnElim_Click(object sender, EventArgs e)
        {
            var id = claseSeleccionada.id;

            DialogResult dialog = MessageBox.Show("Seguro que deseas eliminar esta clase?", "Confirmar Eliminación", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                var respuesta = await clasesService.EliminarClase(id);
                MessageBox.Show(respuesta.Mensaje, respuesta.Titulo);
                LimpiarGridView();
            }
            else
            {
                LimpiarGridView();
            }

            btnRegistrar.Location = new Point(117, 372);
            btnActualizar.Visible = false;
            btnElim.Visible = false;
        }
    }
}
