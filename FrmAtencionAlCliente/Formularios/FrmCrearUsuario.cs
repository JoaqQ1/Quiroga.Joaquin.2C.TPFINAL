using Entidades.Class;
using Entidades.Class.Archivos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmAtencionAlCliente.Formularios
{
    public partial class FrmCrearUsuario : Form
    {

        private UsuarioModelo usuarioModelo = new UsuarioModelo();

        public FrmCrearUsuario()
        {
            InitializeComponent();
        }

        private void FrmCrearUsuario_Load(object sender, EventArgs e)
        {
            this.rdbOtro.Checked = true;

        }
        private string DevolverGenero()
        {
            string genero = string.Empty;
            foreach (RadioButton item in this.grbGenero.Controls)
            {
                if (item.Checked)
                {
                    genero = item.Text;
                }
            }
            return genero;
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (this.ValidarTextBoxs())
            {
                //                Creacion de Usuario                // 

                this.usuarioModelo.Nombre = this.txtNombre.Text;
                this.usuarioModelo.Apellido = this.txtApellido.Text;           
                this.usuarioModelo.Genero = DevolverGenero();         
                this.usuarioModelo.Edad =(int) this.udEdad.Value;
                this.usuarioModelo.Correo = this.txtCorreo.Text;
                this.usuarioModelo.Password = this.txtContraseña.Text;
                
                Usuario usuario = this.usuarioModelo.GetElement();
                try 
                {
                    List<Usuario> usuarios = ManejadorDeUsuario.DevolverListaDeUsuarios();
                    if(usuarios.Count == 0 || !ManejadorDeUsuario.BuscarUsuarioEnArchivo(usuario))
                    {
                        usuario.Id = ManejadorDeUsuario.GetUltimoIdUsuario();
                        usuarios.Add(usuario);
                        ManejadorDeUsuario.SerializarUsuarios(usuarios);
                        MessageBox.Show($"Se puedo agregar correctamente el usuario al archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else 
                    {
                        MessageBox.Show($"El usuario ya se encuentra registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                     
                    }                    
                }
                catch (Exception)
                {
                    MessageBox.Show($"No se puedo agregar correctamente el usuario al archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Debe completar todas las casillas con sus datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Corrobora que los texbox no sean nulos ni vacios
        /// </summary>
        /// <returns>Retornando true en caso de que no lo estes o false de caso contraio</returns>
        private bool ValidarTextBoxs()
        {
            foreach (Control item in this.grbDatos.Controls)
            {
                if (item is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
