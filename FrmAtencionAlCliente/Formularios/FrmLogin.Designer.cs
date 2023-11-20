namespace FrmAtencionAlCliente.Formularios
{
    partial class FrmLogin
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
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblCorreo.Location = new Point(12, 9);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(129, 18);
            lblCorreo.TabIndex = 0;
            lblCorreo.Text = "Ingrese su corre:";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(12, 30);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(298, 23);
            txtCorreo.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblPassword.Location = new Point(12, 65);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(170, 18);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Ingrese su contraseña:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(12, 95);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(298, 23);
            txtPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.Location = new Point(12, 124);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(298, 45);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Logearse";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(322, 176);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtCorreo);
            Controls.Add(lblCorreo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
    }
}