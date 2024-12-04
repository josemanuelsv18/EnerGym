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
    public partial class VerReservas : Form
    {
        private UsuarioService usuarioService;
        private List<Reserva> reservas;
        public VerReservas()
        {
            InitializeComponent();
            usuarioService = new UsuarioService();
            reservas = new List<Reserva>();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            var busqueda = cmbOpcion.SelectedIndex;
            var nombre = txtNombre.Text;
            var estado = cmbEstado.SelectedIndex;

            if ((!string.IsNullOrEmpty(nombre) || estado != -1) && (cmbOpcion.SelectedIndex == -1))
            {
                dgvReservas.Visible = false;
                lblError.Text = "Debe colocar en que categoría quiere buscar";
                lblError.Visible = true;
            }

            if ((cmbOpcion.SelectedIndex == -1) && string.IsNullOrEmpty(nombre) && estado == -1)
            {
                reservas = await usuarioService.BuscarReservas(busqueda, nombre, estado);
                dgvReservas.Visible = true;
                if (reservas.Count == 0)
                {
                    lblError.Text = "No hay registros por buscar";
                    lblError.Visible = true;
                }
            }
            else if ((cmbOpcion.SelectedIndex != -1) && string.IsNullOrEmpty(nombre) && estado == -1)
            {
                lblError.Visible = false;
                reservas = await usuarioService.BuscarReservas(busqueda, "", estado);
                dgvReservas.Visible = true;
                if (reservas.Count == 0)
                {
                    dgvReservas.Visible = false;
                    lblError.Text = "No hay registros por buscar";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 1)
            {
                lblError.Visible = false;
                reservas = await usuarioService.BuscarReservas(busqueda,nombre, estado);
                dgvReservas.Visible = true;
                if (reservas.Count == 0)
                {
                    dgvReservas.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 2)
            {
                lblError.Visible = false;
                reservas = await usuarioService.BuscarReservas(busqueda, nombre, estado);
                dgvReservas.Visible = true;
                if (reservas.Count == 0)
                {
                    dgvReservas.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 0)
            {
                lblError.Visible = false;
                reservas = await usuarioService.BuscarReservas(busqueda, "", estado);
                dgvReservas.Visible = true;
                if (reservas.Count == 0)
                {
                    dgvReservas.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }

            dgvReservas.DataSource = null;
            dgvReservas.DataSource = reservas;

            dgvReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
