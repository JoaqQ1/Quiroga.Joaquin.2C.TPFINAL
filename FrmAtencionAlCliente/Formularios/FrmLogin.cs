using Entidades.Class;
using Entidades.Class.Archivos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmAtencionAlCliente.Formularios
{
    public partial class FrmLogin : Form
    {
        public delegate void UsuarioDelegate(Usuario usuario);
        private UsuarioDelegate OnUsuario;

        private Usuario usuarioLogeado;
        public FrmLogin(UsuarioDelegate usuarioDelegado)
        {
            this.OnUsuario += usuarioDelegado;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCorreo.Text) && string.IsNullOrEmpty(this.txtPassword.Text))
            {
                MessageBox.Show("Complete correctamente los campos para logearse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    this.usuarioLogeado = ManejadorDeUsuario.RetornarUsuarioLogeado(this.txtCorreo.Text, this.txtPassword.Text);
                    if (this.usuarioLogeado is not null)
                    {
                        MessageBox.Show($"Usuario {usuarioLogeado.Nombre} logeado correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.OnUsuario(this.usuarioLogeado);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se han cargado usuarios aun", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
