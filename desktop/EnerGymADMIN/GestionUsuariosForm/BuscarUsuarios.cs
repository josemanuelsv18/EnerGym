using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using EnerGymADMIN.Models;
using Newtonsoft.Json;
using EnerGymADMIN.Services;
using EnerGymADMIN.GestionUsuariosForm;
using EnerGymADMIN.GestionUsuariosForm.BuscarUsuariosForm;

namespace EnerGymADMIN.GestionUsuariosForm
{
    public partial class BuscarUsuarios : Form
    {
        private UsuarioService usuarioService;
        private List<Usuario> usuarios;
        private Usuario usuarioSeleccionado;
        public BuscarUsuarios()
        {
            InitializeComponent();
            usuarioService = new UsuarioService();
            usuarios = new List<Usuario>();
            usuarioSeleccionado = new Usuario();

            lblError.Visible = false;
            btnEditar.Visible = false;
            btnActualizar.Visible = false;
            btnRetElim.Visible = false;
            btnReservar.Visible = false;
            cmbOpcion.SelectedIndex = -1;
        }

        public void LimpiarGridView()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.Rows.Clear();
            lblError.Visible = false;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            var dato = txtBusqueda.Text;

            if ((!string.IsNullOrEmpty(dato)) && (cmbOpcion.SelectedIndex == -1)) {
                dgvUsuarios.Visible = false;
                lblError.Text = "Debe colocar en que categoría quiere buscar";
                lblError.Visible = true;
            }

            if ((cmbOpcion.SelectedIndex == -1) && string.IsNullOrEmpty(dato))
            {
                lblError.Visible = false;
                usuarios = await usuarioService.BuscarUsuario("", "");
                dgvUsuarios.Visible = true;
                if (usuarios.Count == 0)
                {
                    dgvUsuarios.Visible = false;
                    lblError.Text = "No hay registros por buscar";
                    lblError.Visible = true;
                }
            }
            else if ((cmbOpcion.SelectedIndex != -1) && string.IsNullOrEmpty(dato))
            {
                lblError.Visible = false;
                usuarios = await usuarioService.BuscarUsuario("", "");
                dgvUsuarios.Visible = true;
                if (usuarios.Count == 0)
                {
                    dgvUsuarios.Visible = false;
                    lblError.Text = "No hay registros por buscar";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 1)
            {
                lblError.Visible = false;
                usuarios = await usuarioService.BuscarUsuario("Apellido", dato);
                dgvUsuarios.Visible = true;
                if (usuarios.Count == 0)
                {
                    dgvUsuarios.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 2)
            {
                lblError.Visible = false;
                usuarios = await usuarioService.BuscarUsuario("Cédula", dato);
                dgvUsuarios.Visible = true;
                if (usuarios.Count == 0)
                {
                    dgvUsuarios.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }
            else if (cmbOpcion.SelectedIndex == 0)
            {
                lblError.Visible = false;
                usuarios = await usuarioService.BuscarUsuario("Nombre", dato);
                dgvUsuarios.Visible = true;
                if (usuarios.Count == 0)
                {
                    dgvUsuarios.Visible = false;
                    lblError.Text = "No se ha encontrado nada";
                    lblError.Visible = true;
                }
            }

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarios;

            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                if (dgvUsuarios.Columns[e.ColumnIndex].Name == "id")
                {
                    usuarioSeleccionado = usuarios[e.RowIndex];
                    btnActualizar.Visible = true;
                    btnEditar.Visible = true;
                    btnRetElim.Visible = true;
                    btnReservar.Visible = true;
                }
                else
                {
                    btnEditar.Visible = false;
                    btnActualizar.Visible = false;
                    btnRetElim.Visible = false;
                    btnReservar.Visible = false;
                    MessageBox.Show("Seleccione un id para actulizar","Corregir");
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarEstado actualizarEstado = new ActualizarEstado(usuarioSeleccionado);
            this.Hide();
            actualizarEstado.ShowDialog();
            LimpiarGridView();
            this.Show();
            btnActualizar.Visible = false;
            btnEditar.Visible = false;
            btnRetElim.Visible = false;
            btnReservar.Visible = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarDatos editarDatos = new EditarDatos(usuarioSeleccionado);
            this.Hide();
            editarDatos.ShowDialog();
            LimpiarGridView();
            this.Show();
            btnActualizar.Visible = false;
            btnEditar.Visible = false;
            btnRetElim.Visible = false;
            btnReservar.Visible = false;
        }

        private async void btnRetElim_Click(object sender, EventArgs e)
        {
            var id = usuarioSeleccionado.id;
            
            string resultado = ConfirmarRetirarEliminar.Show("¿Qué quiere hacer el usuario?");

            if (resultado == "Retirar")
            {
                DialogResult dialog = MessageBox.Show("Seguro que deseas retirar a este usuario?", "Confirmar Retiración", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    var respuesta = await usuarioService.RetirarseGym(id, "Retirado");
                    MessageBox.Show(respuesta.Mensaje, respuesta.Titulo);
                    LimpiarGridView();
                }
                else
                {
                    LimpiarGridView();
                }
            }
            else if (resultado == "Eliminar")
            {
                DialogResult dialog = MessageBox.Show("Seguro que deseas eliminar este usuario?", "Confirmar Eliminación", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    var respuesta = await usuarioService.EliminarUsuario(id);
                    MessageBox.Show(respuesta.Mensaje, respuesta.Titulo);
                    LimpiarGridView();
                }
                else
                {
                    LimpiarGridView();
                }
            }
            else
            {
                LimpiarGridView();
            }

            btnActualizar.Visible = false;
            btnEditar.Visible = false;
            btnRetElim.Visible = false;
            btnReservar.Visible = false;
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            ReservarPuesto reservarPuesto = new ReservarPuesto(usuarioSeleccionado);
            this.Hide();
            reservarPuesto.ShowDialog();
            LimpiarGridView();
            this.Show();
            btnActualizar.Visible = false;
            btnEditar.Visible = false;
            btnRetElim.Visible = false;
            btnReservar.Visible = false;
        }
    }
}