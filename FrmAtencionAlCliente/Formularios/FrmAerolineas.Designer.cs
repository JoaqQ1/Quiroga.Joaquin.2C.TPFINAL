namespace FrmAtencionAlCliente
{
    partial class FrmAerolineas
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTiempo = new Label();
            timerTiempo = new System.Windows.Forms.Timer(components);
            lbVuelos = new ListBox();
            grbConsultarVuelos = new GroupBox();
            btnMostrarTodosLosVuelos = new Button();
            btnConsultar = new Button();
            cbDestino = new ComboBox();
            cbOrigen = new ComboBox();
            gpbClase = new GroupBox();
            rbLowCost = new RadioButton();
            rbPrimeraClase = new RadioButton();
            lblDestino = new Label();
            lblOrigen = new Label();
            btnComprarVuelo = new Button();
            btnCrearUsuario = new Button();
            btnSalir = new Button();
            lblBienvenidoUsuario = new Label();
            btnLogearse = new Button();
            btnMostrarVueloComprado = new Button();
            grbConsultarVuelos.SuspendLayout();
            gpbClase.SuspendLayout();
            SuspendLayout();
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.Font = new Font("Segoe UI Emoji", 25.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTiempo.ForeColor = SystemColors.ActiveCaptionText;
            lblTiempo.Location = new Point(533, 9);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(264, 46);
            lblTiempo.TabIndex = 0;
            lblTiempo.Text = "HORA: 00:00:00";
            lblTiempo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timerTiempo
            // 
            timerTiempo.Tick += timerTiempo_Tick;
            // 
            // lbVuelos
            // 
            lbVuelos.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbVuelos.FormattingEnabled = true;
            lbVuelos.ItemHeight = 16;
            lbVuelos.Location = new Point(301, 72);
            lbVuelos.Name = "lbVuelos";
            lbVuelos.Size = new Size(487, 244);
            lbVuelos.TabIndex = 6;
            // 
            // grbConsultarVuelos
            // 
            grbConsultarVuelos.Controls.Add(btnMostrarTodosLosVuelos);
            grbConsultarVuelos.Controls.Add(btnConsultar);
            grbConsultarVuelos.Controls.Add(cbDestino);
            grbConsultarVuelos.Controls.Add(cbOrigen);
            grbConsultarVuelos.Controls.Add(gpbClase);
            grbConsultarVuelos.Controls.Add(lblDestino);
            grbConsultarVuelos.Controls.Add(lblOrigen);
            grbConsultarVuelos.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            grbConsultarVuelos.Location = new Point(12, 12);
            grbConsultarVuelos.Name = "grbConsultarVuelos";
            grbConsultarVuelos.Size = new Size(283, 311);
            grbConsultarVuelos.TabIndex = 8;
            grbConsultarVuelos.TabStop = false;
            grbConsultarVuelos.Text = "Consultar vuelos:";
            // 
            // btnMostrarTodosLosVuelos
            // 
            btnMostrarTodosLosVuelos.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnMostrarTodosLosVuelos.Location = new Point(165, 233);
            btnMostrarTodosLosVuelos.Name = "btnMostrarTodosLosVuelos";
            btnMostrarTodosLosVuelos.Size = new Size(110, 50);
            btnMostrarTodosLosVuelos.TabIndex = 14;
            btnMostrarTodosLosVuelos.Text = "Mostrar todos los vuelos";
            btnMostrarTodosLosVuelos.UseVisualStyleBackColor = true;
            btnMostrarTodosLosVuelos.Click += btnMostrarTodosLosVuelos_Click;
            // 
            // btnConsultar
            // 
            btnConsultar.Location = new Point(6, 233);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(110, 50);
            btnConsultar.TabIndex = 9;
            btnConsultar.Text = "Consultar";
            btnConsultar.UseVisualStyleBackColor = true;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // cbDestino
            // 
            cbDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDestino.FormattingEnabled = true;
            cbDestino.Items.AddRange(new object[] { "Brasil,", " Peru,", " Mexico, ", "Venezuela, ", "Uruguay, ", "Paraguay, ", "España" });
            cbDestino.Location = new Point(75, 72);
            cbDestino.Name = "cbDestino";
            cbDestino.Size = new Size(127, 24);
            cbDestino.TabIndex = 7;
            // 
            // cbOrigen
            // 
            cbOrigen.DisplayMember = "aRGENTINA";
            cbOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOrigen.Items.AddRange(new object[] { "Argentina" });
            cbOrigen.Location = new Point(75, 33);
            cbOrigen.Name = "cbOrigen";
            cbOrigen.Size = new Size(127, 24);
            cbOrigen.TabIndex = 6;
            // 
            // gpbClase
            // 
            gpbClase.Controls.Add(rbLowCost);
            gpbClase.Controls.Add(rbPrimeraClase);
            gpbClase.Location = new Point(6, 129);
            gpbClase.Name = "gpbClase";
            gpbClase.Size = new Size(153, 79);
            gpbClase.TabIndex = 5;
            gpbClase.TabStop = false;
            gpbClase.Text = "Clase";
            // 
            // rbLowCost
            // 
            rbLowCost.AutoSize = true;
            rbLowCost.Location = new Point(6, 47);
            rbLowCost.Name = "rbLowCost";
            rbLowCost.Size = new Size(81, 20);
            rbLowCost.TabIndex = 1;
            rbLowCost.TabStop = true;
            rbLowCost.Text = "Low cost";
            rbLowCost.UseVisualStyleBackColor = true;
            // 
            // rbPrimeraClase
            // 
            rbPrimeraClase.AutoSize = true;
            rbPrimeraClase.Location = new Point(6, 21);
            rbPrimeraClase.Name = "rbPrimeraClase";
            rbPrimeraClase.Size = new Size(113, 20);
            rbPrimeraClase.TabIndex = 0;
            rbPrimeraClase.TabStop = true;
            rbPrimeraClase.Text = "Primera clase";
            rbPrimeraClase.UseVisualStyleBackColor = true;
            // 
            // lblDestino
            // 
            lblDestino.AutoSize = true;
            lblDestino.Location = new Point(6, 75);
            lblDestino.Name = "lblDestino";
            lblDestino.Size = new Size(58, 16);
            lblDestino.TabIndex = 1;
            lblDestino.Text = "Destino:";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Location = new Point(6, 36);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(54, 16);
            lblOrigen.TabIndex = 0;
            lblOrigen.Text = "Origen:";
            // 
            // btnComprarVuelo
            // 
            btnComprarVuelo.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnComprarVuelo.Location = new Point(9, 366);
            btnComprarVuelo.Name = "btnComprarVuelo";
            btnComprarVuelo.Size = new Size(119, 55);
            btnComprarVuelo.TabIndex = 9;
            btnComprarVuelo.Text = "Comprar vuelo";
            btnComprarVuelo.UseVisualStyleBackColor = true;
            btnComprarVuelo.Click += btnComprarVuelo_Click;
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCrearUsuario.Location = new Point(553, 366);
            btnCrearUsuario.Name = "btnCrearUsuario";
            btnCrearUsuario.Size = new Size(119, 55);
            btnCrearUsuario.TabIndex = 10;
            btnCrearUsuario.Text = "Crear usuario";
            btnCrearUsuario.UseVisualStyleBackColor = true;
            btnCrearUsuario.Click += btnCrearUsuario_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSalir.Location = new Point(678, 366);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(119, 55);
            btnSalir.TabIndex = 11;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblBienvenidoUsuario
            // 
            lblBienvenidoUsuario.AutoSize = true;
            lblBienvenidoUsuario.Font = new Font("Arial", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblBienvenidoUsuario.Location = new Point(335, 21);
            lblBienvenidoUsuario.Name = "lblBienvenidoUsuario";
            lblBienvenidoUsuario.Size = new Size(149, 18);
            lblBienvenidoUsuario.TabIndex = 12;
            lblBienvenidoUsuario.Text = "Bienvenido Usuario";
            // 
            // btnLogearse
            // 
            btnLogearse.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogearse.Location = new Point(428, 366);
            btnLogearse.Name = "btnLogearse";
            btnLogearse.Size = new Size(119, 55);
            btnLogearse.TabIndex = 13;
            btnLogearse.Text = "Logearse";
            btnLogearse.UseVisualStyleBackColor = true;
            btnLogearse.Click += btnLogearse_Click;
            // 
            // btnMostrarVueloComprado
            // 
            btnMostrarVueloComprado.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnMostrarVueloComprado.Location = new Point(177, 366);
            btnMostrarVueloComprado.Name = "btnMostrarVueloComprado";
            btnMostrarVueloComprado.Size = new Size(119, 55);
            btnMostrarVueloComprado.TabIndex = 14;
            btnMostrarVueloComprado.Text = "Mostra vuelo comprado";
            btnMostrarVueloComprado.UseVisualStyleBackColor = true;
            btnMostrarVueloComprado.Click += btnMostrarVueloComprado_Click;
            // 
            // FrmAerolineas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(btnMostrarVueloComprado);
            Controls.Add(btnLogearse);
            Controls.Add(lblBienvenidoUsuario);
            Controls.Add(btnSalir);
            Controls.Add(btnCrearUsuario);
            Controls.Add(btnComprarVuelo);
            Controls.Add(grbConsultarVuelos);
            Controls.Add(lbVuelos);
            Controls.Add(lblTiempo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAerolineas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aerolineas";
            FormClosing += FrmAerolineas_FormClosing;
            Load += FrmAerolineas_Load;
            grbConsultarVuelos.ResumeLayout(false);
            grbConsultarVuelos.PerformLayout();
            gpbClase.ResumeLayout(false);
            gpbClase.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTiempo;
        private System.Windows.Forms.Timer timerTiempo;
        private ListBox lbVuelos;
        private GroupBox grbConsultarVuelos;
        private Label lblOrigen;
        private Label lblDestino;
        private GroupBox gpbClase;
        private RadioButton rbLowCost;
        private RadioButton rbPrimeraClase;
        private Button btnConsultar;
        private ComboBox cbDestino;
        private ComboBox cbOrigen;
        private Button btnComprarVuelo;
        private Button btnCrearUsuario;
        private Button btnSalir;
        private Label lblBienvenidoUsuario;
        private Button btnLogearse;
        private Button btnMostrarTodosLosVuelos;
        private Button btnMostrarVueloComprado;
    }
}