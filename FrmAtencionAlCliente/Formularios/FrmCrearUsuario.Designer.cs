using Entidades.Class;

namespace FrmAtencionAlCliente.Formularios
{
    partial class FrmCrearUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grbDatos = new GroupBox();
            udEdad = new NumericUpDown();
            txtContraseña = new TextBox();
            txtCorreo = new TextBox();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            lblContraseña = new Label();
            lblCorreo = new Label();
            lblEdad = new Label();
            lblApellido = new Label();
            lblNombre = new Label();
            grbGenero = new GroupBox();
            rdbOtro = new RadioButton();
            rdbFemenino = new RadioButton();
            rdbMasculino = new RadioButton();
            btnCrear = new Button();
            grbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)udEdad).BeginInit();
            grbGenero.SuspendLayout();
            SuspendLayout();
            // 
            // grbDatos
            // 
            grbDatos.Controls.Add(udEdad);
            grbDatos.Controls.Add(txtContraseña);
            grbDatos.Controls.Add(txtCorreo);
            grbDatos.Controls.Add(txtApellido);
            grbDatos.Controls.Add(txtNombre);
            grbDatos.Controls.Add(lblContraseña);
            grbDatos.Controls.Add(lblCorreo);
            grbDatos.Controls.Add(lblEdad);
            grbDatos.Controls.Add(lblApellido);
            grbDatos.Controls.Add(lblNombre);
            grbDatos.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grbDatos.Location = new Point(12, 12);
            grbDatos.Name = "grbDatos";
            grbDatos.Size = new Size(213, 211);
            grbDatos.TabIndex = 0;
            grbDatos.TabStop = false;
            grbDatos.Text = "Datos";
            // 
            // udEdad
            // 
            udEdad.Location = new Point(55, 98);
            udEdad.Name = "udEdad";
            udEdad.Size = new Size(120, 21);
            udEdad.TabIndex = 9;
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(89, 155);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(100, 21);
            txtContraseña.TabIndex = 8;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(65, 126);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(100, 21);
            txtCorreo.TabIndex = 7;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(72, 66);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(100, 21);
            txtApellido.TabIndex = 6;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(73, 35);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 21);
            txtNombre.TabIndex = 5;
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblContraseña.Location = new Point(16, 158);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(73, 15);
            lblContraseña.TabIndex = 4;
            lblContraseña.Text = "Contraseña";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCorreo.Location = new Point(16, 129);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(46, 15);
            lblCorreo.TabIndex = 3;
            lblCorreo.Text = "Correo";
            // 
            // lblEdad
            // 
            lblEdad.AutoSize = true;
            lblEdad.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblEdad.Location = new Point(16, 100);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(35, 15);
            lblEdad.TabIndex = 2;
            lblEdad.Text = "Edad";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblApellido.Location = new Point(16, 66);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(52, 15);
            lblApellido.TabIndex = 1;
            lblApellido.Text = "Apellido";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNombre.Location = new Point(16, 35);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(52, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            // 
            // grbGenero
            // 
            grbGenero.Controls.Add(rdbOtro);
            grbGenero.Controls.Add(rdbFemenino);
            grbGenero.Controls.Add(rdbMasculino);
            grbGenero.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grbGenero.Location = new Point(231, 12);
            grbGenero.Name = "grbGenero";
            grbGenero.Size = new Size(145, 102);
            grbGenero.TabIndex = 10;
            grbGenero.TabStop = false;
            grbGenero.Text = "Genero";
            // 
            // rdbOtro
            // 
            rdbOtro.AutoSize = true;
            rdbOtro.Location = new Point(21, 70);
            rdbOtro.Name = "rdbOtro";
            rdbOtro.Size = new Size(50, 19);
            rdbOtro.TabIndex = 2;
            rdbOtro.TabStop = true;
            rdbOtro.Text = "Otro";
            rdbOtro.UseVisualStyleBackColor = true;
            // 
            // rdbFemenino
            // 
            rdbFemenino.AutoSize = true;
            rdbFemenino.Location = new Point(21, 45);
            rdbFemenino.Name = "rdbFemenino";
            rdbFemenino.Size = new Size(80, 19);
            rdbFemenino.TabIndex = 1;
            rdbFemenino.TabStop = true;
            rdbFemenino.Text = "Femenino";
            rdbFemenino.UseVisualStyleBackColor = true;
            // 
            // rdbMasculino
            // 
            rdbMasculino.AutoSize = true;
            rdbMasculino.Location = new Point(21, 22);
            rdbMasculino.Name = "rdbMasculino";
            rdbMasculino.Size = new Size(83, 19);
            rdbMasculino.TabIndex = 0;
            rdbMasculino.TabStop = true;
            rdbMasculino.Text = "Masculino";
            rdbMasculino.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCrear.Location = new Point(12, 229);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(367, 90);
            btnCrear.TabIndex = 3;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // FrmCrearUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(391, 333);
            Controls.Add(btnCrear);
            Controls.Add(grbGenero);
            Controls.Add(grbDatos);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCrearUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Crear usuario";
            Load += FrmCrearUsuario_Load;
            grbDatos.ResumeLayout(false);
            grbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)udEdad).EndInit();
            grbGenero.ResumeLayout(false);
            grbGenero.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grbDatos;
        private NumericUpDown udEdad;
        private TextBox txtContraseña;
        private TextBox txtCorreo;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private Label lblContraseña;
        private Label lblCorreo;
        private Label lblEdad;
        private Label lblApellido;
        private Label lblNombre;
        private GroupBox grbGenero;
        private RadioButton rdbOtro;
        private RadioButton rdbFemenino;
        private RadioButton rdbMasculino;
        private Button btnCrear;
    }
}