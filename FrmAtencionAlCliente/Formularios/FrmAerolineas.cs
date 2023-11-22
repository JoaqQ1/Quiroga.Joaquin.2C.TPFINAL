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
        //Delegado
        private delegate void DelegateOptimizacion(List<Avion> lista,List<Usuario> usuarios);
        //Evento
        private event DelegateOptimizacion onOptimizacion;
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
        private CancellationTokenSource cancelarActualizacionDeVuelos = new CancellationTokenSource();

        public FrmAerolineas()
        {
            InitializeComponent();
        }
        private void FrmAerolineas_Load(object sender, EventArgs e)
        {
            // Configuración del título del formulario
            this.Text = $"Aerolíneas - {Environment.UserName} -";

            // Configuración de las fuentes de datos para ComboBox
            this.cbOrigen.DataSource = new[] { "Argentina" };
            this.cbDestino.DataSource = new[] { "Brasil", "Perú", "México", "Venezuela", "Uruguay", "Paraguay", "España" };

            // Configuración de los botones deshabilitados inicialmente
            this.btnComprarVuelo.Enabled = false;
            this.btnMostrarVueloComprado.Enabled = false;

            // Configuración del intervalo y inicio del temporizador
            this.timerTiempo.Interval = 1000;
            this.timerTiempo.Start();

            // Suscripción al evento de optimización
            this.onOptimizacion += EliminarVuelosYUsuarios;

            // Actualización automática de vuelos en segundo plano
            Task.Run(() =>
            {
                while (!this.cancelarActualizacionDeVuelos.IsCancellationRequested)
                {
                    this.ActualizarVuelos();
                    Thread.Sleep(1000);
                }
            });
        }



        private void ActualizarVuelos()
        {
            try
            {
                // Obtener la lista de vuelos desde la base de datos
                this.listaDeAviones = DBOAviones.GetVuelos();

                // Actualizar los estados de los vuelos en función de la hora actual
                this.listaDeAviones.ActualizarVuelos(DateTime.Now);

                // Actualizar la base de datos con los cambios en los vuelos
                DBOAviones.ActualizarVuelos(this.listaDeAviones);

                // Si la lista está vacía, agregar nuevos vuelos
                if (this.listaDeAviones.Count == 0)
                {
                    List<Avion>? nuevosVuelos = this.listaDeAviones.GeneradorDeVuelos();
                    DBOAviones.AgregarAviones(nuevosVuelos);
                }

                // Si la lista supera los 20 vuelos, invocar el evento de optimización
                if (this.listaDeAviones.Count > 20 && this.onOptimizacion is not null)
                {
                    List<Avion>? vuelosNoDisponibles = DBOAviones.GetVuelosFiltrados("disponible", "0");
                    List<Usuario> usuariosSinVuelos = DBOUsuarios.GetUsuarios();
                    this.onOptimizacion.Invoke(vuelosNoDisponibles, usuariosSinVuelos);
                }

                // Obtener el horario del último vuelo
                DateTime ultimoVuelo = this.listaDeAviones.DevolverHorarioDeUltimoVuelo();

                // Si el último vuelo ya ha salido, actualizar la lista de aviones creando nuevos vuelos
                if (ultimoVuelo < DateTime.Now)
                {
                    // Actualizar el estado de los aviones de disponibles a no disponibles
                    List<Avion>? nuevosVuelos = this.listaDeAviones.GeneradorDeVuelos();
                    DBOAviones.AgregarAviones(nuevosVuelos);
                    MessageBox.Show("Vuelos actualizados", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones de manera adecuada (log, mensaje al usuario, etc.)
                MessageBox.Show($"Error al actualizar los vuelos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarVuelos(List<Avion> aviones)
        {
            if (aviones.Count == 0)
            {
                MessageBox.Show("Error lista de aviones vacía", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                aviones.ActualizarVuelos(DateTime.Now);
                foreach (Avion item in aviones)
                {
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
            // Al salir se cancela las actualizaciones de los vuelos
            this.cancelarActualizacionDeVuelos.Cancel();
            this.Close();
        }


        private void btnComprarVuelo_Click(object sender, EventArgs e)
        {
            //Se obtinene un vuelo seleccionado con el click
            Avion? avion = this.lbVuelos.SelectedItem as Avion;

            if (avion == null || avion.Disponible == false)
            {
                MessageBox.Show("Debe seleccionar un avion disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.comprarVueloEnSecundario = new Task(() => { RealizaCompra(avion); });
                //Se iniciara la tarea en secundario para realizar la compra del avion
                this.comprarVueloEnSecundario.Start();
            }

        }
        private async void RealizaCompra(Avion avion)
        {
            // Invocar la función de manera asincrónica si es necesario
            if (this.InvokeRequired)
            {
                this.BeginInvoke(() => RealizaCompra(avion));
            }
            else
            {
                // Se corrobora que el usuario este seguro de comprar el vuelo
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

                            int horas = avion.HoraDeSalida.Hour - DateTime.Now.Hour;
                            int minutos = avion.HoraDeSalida.Minute - DateTime.Now.Minute;

                            // Utilizar "await Task.Delay" para pausar la ejecución sin bloquear el hilo principal
                            await Task.Delay(4000);

                            string mensaje = avion.Disponible ? $"Su vuelo sale dentro de {horas}:{minutos:D2} y tuvo un costo de {avion.Costo}" : $"Su vuelo ya salió y tuvo un costo de {avion.Costo}";
                            MessageBox.Show(mensaje, "Compra Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Usuario no logeado correctamente, si esta logeado espere unos segundo al hacer la compra no precione aceptar al instante", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error no se puedo completar la compra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
            }
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

                // Filtrar los vuelos según el país seleccionado en el ComboBox destino
                this.listaDeAviones = DBOAviones.GetVuelosFiltrados("destino", pais);

                if (this.listaDeAviones.Count == 0)
                {
                    MessageBox.Show($"No hay vuelos para {pais} disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MostrarVuelos(this.listaDeAviones);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar vuelos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            catch (Exception)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMostrarVueloComprado_Click(object sender, EventArgs e)
        {
            try
            {
                this._usuario = DBOUsuarios.GetUsuarioFiltrado("correo", this._usuario.Correo);
                if(this._usuario is not null)
                {
                    Avion avion = DBOAviones.ObtenerVueloPorId(this._usuario.Id_vuelo);
                    MessageBox.Show($"{avion}", "Vuelo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Sin vuelos comprados", "Vuelo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }            
            catch(ElementoNoEncontradoException)
            {
                MessageBox.Show($"Su vuelo ya salio y ya no se encuantra en la base de datos, lo sentimos", "Vuelo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no se encontro el vuelo del usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
       
        private void EliminarVuelosYUsuarios(List<Avion> aviones,List<Usuario> usuarios)        
        {
            if(usuarios.Count > 0)
            {
                foreach(Avion avion in aviones)
                {
                    foreach(Usuario usario in usuarios)
                    {
                        if(usario.Id_vuelo == avion.Id)
                        {
                            DBOUsuarios.EliminarUsuario(usario);
                        }
                    }
                }
            }
            if (aviones.Count > 0)
            {
                DBOAviones.EliminarVuelos(aviones);
            }
        }
    }
 
}