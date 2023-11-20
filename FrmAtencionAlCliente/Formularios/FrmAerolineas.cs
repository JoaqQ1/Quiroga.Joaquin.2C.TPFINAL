using Entidades.Class;
using Entidades.DBO;
using Entidades.Excepciones;
using Entidades.MetodosExtencion;
using FrmAtencionAlCliente.Formularios;
using System;
using System.Text;
using System.Windows.Forms;


namespace FrmAtencionAlCliente
{
    public partial class FrmAerolineas : Form
    {
        //Formularios
        private FrmLogin frmLog;
        private FrmCrearUsuario frmCrearUsario;
        //Clases
        private Usuario _usuario;
        private List<Avion> listaDeAviones;

        //Tareas en secundario compra de vuelo
        private Task comprarVueloEnSecundario;
        private CancellationTokenSource cancelarHiloCompra = new CancellationTokenSource();
        //Tareas en secundario actualizacion de la lista de vuelo
        private Task actualizacionDeVuelos;
        private CancellationTokenSource cancelarActualizacionDeVuelos = new CancellationTokenSource();

        public FrmAerolineas()
        {
            InitializeComponent();
            this.rbLowCost.Checked = true;
        }
        private void FrmAerolineas_Load(object sender, EventArgs e)
        {
            this.Text = $"Aerolineas -{Environment.UserName}-";
            this.cbOrigen.DataSource = new object[] { "Argentina" };
            this.cbDestino.DataSource = new object[] { "Brasil", "Peru", "Mexico", "Venezuela", "Uruguay", "Paraguay", "España" };
            this.btnComprarVuelo.Enabled = false;
            this.btnMostrarVueloComprado.Enabled = false;
            this.timerTiempo.Interval = 1000;
            this.timerTiempo.Start();
            this.actualizacionDeVuelos = new Task(ActualizarVuelos, this.cancelarActualizacionDeVuelos.Token);
            Task.Run(() =>
            {
                while (!this.cancelarActualizacionDeVuelos.IsCancellationRequested)
                {
                    this.ActualizarVuelos();
                    Thread.Sleep(5000);
                }
            });
        }


        private void ActualizarVuelos()
        {
            try
            {
                this.listaDeAviones = DBOAviones.GetVuelos();
                DateTime ultimoVuelo = this.listaDeAviones.DevolverHorarioDeUltimoVuelo();
                try
                {
                    if (this.listaDeAviones.Count == 0 || ultimoVuelo > DateTime.Now)
                    {
                        this.listaDeAviones.ActualizarVuelos();
                        DBOAviones.ActualizarVuelos(this.listaDeAviones);
                        List<Avion>? nuevosVuelos = this.listaDeAviones.GeneradorDeVuelos();
                        DBOAviones.AgregarAviones(nuevosVuelos);
                        MessageBox.Show("Vuelos actualizados", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al actualizar los vuelos", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (DataBaseErrorException)
            {
                MessageBox.Show("Error en la conexion con la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarVuelos(List<Avion> aviones)
        {
            if (aviones.Count == 0)
            {
                MessageBox.Show("Error lista de aviones vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (Avion item in aviones)
                {
                    if (item.HoraDeSalida > DateTime.Now)
                    {
                        item.Disponible = false;
                    }

                    this.lbVuelos.Items.Add(item);
                }
            }
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            this.frmCrearUsario = new FrmCrearUsuario();
            this.frmCrearUsario.ShowDialog();
        }

        private void ActualizarUsuario(Usuario usuario)
        {
            this._usuario = usuario;
            this.lblBienvenidoUsuario.Text = $"Bienvenido {this._usuario.Nombre}";
            this.btnComprarVuelo.Enabled = true;
            this.btnMostrarVueloComprado.Enabled = true;
        }

        private void btnLogearse_Click(object sender, EventArgs e)
        {
            this.frmLog = new FrmLogin(this.ActualizarUsuario);
            this.frmLog.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.cancelarActualizacionDeVuelos.Cancel();
            this.Close();
        }


        private void btnComprarVuelo_Click(object sender, EventArgs e)
        {
            Avion avion = this.lbVuelos.SelectedItem as Avion;

            if (avion == null || avion.Disponible == false)
            {
                MessageBox.Show("Debe seleccionar un avion disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.comprarVueloEnSecundario = new Task(() => { RealizaCompra(avion); });
                this.comprarVueloEnSecundario.Start();
            }

        }
        private async void RealizaCompra(Avion avion)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(() => RealizaCompra(avion));
            }
            else
            {
                DialogResult respuesta = MessageBox.Show($"Seguro de que quiere comprar el vuelo a {avion.Destino} con un costo de ${avion.Costo}?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (respuesta == DialogResult.Yes && !this.cancelarHiloCompra.IsCancellationRequested)
                {
                    MessageBox.Show("Procesando su pago..", "Procesando", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        if (DBOUsuarios.BuscarCoincidencia("correo", this._usuario.Correo))
                        {
                            MessageBox.Show("Este usuario ya compro un vuelo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            this._usuario.Id_vuelo = avion.Id;

                            DBOUsuarios.AgregarUnUsuario(this._usuario);

                            //Calcular el tiempo que falta para que salga el vuelo.
                            int horas = avion.HoraDeSalida.Hour - DateTime.Now.Hour;
                            int minutos = avion.HoraDeSalida.Minute - DateTime.Now.Minute;

                            //Costo con aumento
                            decimal precioBruto = avion.CalcularCostoConAumento();

                            if ("Low cost" == this.DevolverClase())
                            {
                                precioBruto = (decimal)avion.Costo;
                            }

                            //Use el await porque mi aplicacion no me dejaba interactuar con mi formulario correctamente con el metodo Task.Sleep()
                            await Task.Delay(4000);
                            MessageBox.Show($"Su vuelo sale dentro de {horas}:{minutos:D2} y tuvo un costo de {precioBruto}", "Compro con exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error no se puedo completar la compra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
            }
        }

        private string DevolverClase()
        {
            string clase = string.Empty;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(() => DevolverClase());
            }
            else
            {
                foreach (RadioButton item in this.gpbClase.Controls)
                {
                    if (item.Checked)
                    {
                        clase = item.Text;
                    }
                }
            }
            return clase;
        }


        private void timerTiempo_Tick(object sender, EventArgs e)
        {
            this.lblTiempo.Text = "HORA: " + DateTime.Now.ToString("HH:mm:ss tt");
        }

        private void FrmAerolineas_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Seguro de que quiere salir?", "Cerrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (!(respuesta == DialogResult.Yes))
            {
                e.Cancel = true;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                this.lbVuelos.Items.Clear();
                string pais = this.cbDestino.Text;
                this.listaDeAviones = DBOAviones.GetVuelosFiltrados("destino", pais);

                if (this.listaDeAviones.Count == 0)
                {
                    MessageBox.Show($"No hay vuelos para {pais} disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.MostrarVuelos(this.listaDeAviones);
                }
            }
            catch (DataBaseErrorException)
            {
                MessageBox.Show("Error en la conexion en la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMostrarTodosLosVuelos_Click(object sender, EventArgs e)
        {
            try
            {
                this.lbVuelos.Items.Clear();
                this.listaDeAviones = DBOAviones.GetVuelos();

                if (this.listaDeAviones.Count == 0)
                {
                    MessageBox.Show($"No hay vuelos en la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                this.MostrarVuelos(this.listaDeAviones);
            }
            catch (DataBaseErrorException)
            {
                MessageBox.Show("Error en la conexion en la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMostrarVueloComprado_Click(object sender, EventArgs e)
        {
            try
            {
                this._usuario = DBOUsuarios.GetUsuarioFiltrado("correo", this._usuario.Correo);
                Avion avion = DBOAviones.ObtenerVueloPorId(this._usuario.Id_vuelo);
                MessageBox.Show($"{avion}", "Vuelo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("Error no se encontro el vuelo del usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}