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
    public partial class ConfirmarRetirarEliminar : Form
    {
        public ConfirmarRetirarEliminar(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
        }

        public static string Show(string message)
        {
            using (var form = new ConfirmarRetirarEliminar(message))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    return "Retirar";
                }
                else if (result == DialogResult.No)
                {
                    return "Eliminar";
                }
                else
                {
                    return "Cancelar";
                }
            }
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void ConfirmarRetirarEliminar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.Yes && this.DialogResult != DialogResult.No)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}