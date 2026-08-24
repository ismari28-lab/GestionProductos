namespace ESFE.GestionProductos.UI
{
    partial class login
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

<<<<<<< HEAD
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
       
=======
>>>>>>> db9b30737bd316a09940f9d771eda075e475465c
        private void InitializeComponent()
        {
            cardLogin = new MaterialSkin.Controls.MaterialCard();
            btnIniciarSesion = new MaterialSkin.Controls.MaterialButton();
            btnRecuperarContraseña = new MaterialSkin.Controls.MaterialButton();
            txtContraseña = new MaterialSkin.Controls.MaterialTextBox2();
            txtUsuario = new MaterialSkin.Controls.MaterialTextBox2();
            lblTitulo = new MaterialSkin.Controls.MaterialLabel();
            cardLogin.SuspendLayout();
            SuspendLayout();
            // 
            // cardLogin
            // 
            cardLogin.BackColor = Color.FromArgb(255, 255, 255);
            cardLogin.Controls.Add(btnRecuperarContraseña);
            cardLogin.Controls.Add(btnIniciarSesion);
            cardLogin.Controls.Add(txtContraseña);
            cardLogin.Controls.Add(txtUsuario);
            cardLogin.Controls.Add(lblTitulo);
            cardLogin.Depth = 0;
            cardLogin.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cardLogin.Location = new Point(310, 120);
            cardLogin.Margin = new Padding(20, 23, 20, 23);
            cardLogin.MouseState = MaterialSkin.MouseState.HOVER;
            cardLogin.Name = "cardLogin";
            cardLogin.Padding = new Padding(20, 23, 20, 23);
            cardLogin.Size = new Size(520, 480);
            cardLogin.TabIndex = 0;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnIniciarSesion.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnIniciarSesion.Depth = 0;
            btnIniciarSesion.HighEmphasis = true;
            btnIniciarSesion.Icon = null;
            btnIniciarSesion.Location = new Point(196, 335);
            btnIniciarSesion.Margin = new Padding(6, 10, 6, 10);
            btnIniciarSesion.MouseState = MaterialSkin.MouseState.HOVER;
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.NoAccentTextColor = Color.Empty;
            btnIniciarSesion.Size = new Size(128, 36);
            btnIniciarSesion.TabIndex = 3;
            btnIniciarSesion.Text = "INICIAR SESIÓN";
            btnIniciarSesion.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnIniciarSesion.UseAccentColor = false;
            btnIniciarSesion.UseVisualStyleBackColor = true;
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            // 
            // btnRecuperarContraseña
            // 
            btnRecuperarContraseña.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRecuperarContraseña.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRecuperarContraseña.Depth = 0;
            btnRecuperarContraseña.HighEmphasis = false;
            btnRecuperarContraseña.Icon = null;
            btnRecuperarContraseña.Location = new Point(150, 385);
            btnRecuperarContraseña.Margin = new Padding(6, 10, 6, 10);
            btnRecuperarContraseña.MouseState = MaterialSkin.MouseState.HOVER;
            btnRecuperarContraseña.Name = "btnRecuperarContraseña";
            btnRecuperarContraseña.NoAccentTextColor = Color.Empty;
            btnRecuperarContraseña.Size = new Size(220, 30);
            btnRecuperarContraseña.TabIndex = 4;
            btnRecuperarContraseña.Text = "¿Olvidó su contraseña?";
            btnRecuperarContraseña.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            btnRecuperarContraseña.UseAccentColor = false;
            btnRecuperarContraseña.UseVisualStyleBackColor = true;
            btnRecuperarContraseña.Click += btnRecuperarContraseña_Click;
            // 
            // txtContraseña
            // 
            txtContraseña.AnimateReadOnly = false;
            txtContraseña.BackgroundImageLayout = ImageLayout.None;
            txtContraseña.CharacterCasing = CharacterCasing.Normal;
            txtContraseña.Depth = 0;
            txtContraseña.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtContraseña.HideSelection = true;
            txtContraseña.Hint = "Contraseña";
            txtContraseña.LeadingIcon = null;
            txtContraseña.Location = new Point(82, 225);
            txtContraseña.Margin = new Padding(4, 5, 4, 5);
            txtContraseña.MaxLength = 32767;
            txtContraseña.MouseState = MaterialSkin.MouseState.OUT;
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.PrefixSuffixText = null;
            txtContraseña.ReadOnly = false;
            txtContraseña.RightToLeft = RightToLeft.No;
            txtContraseña.SelectedText = "";
            txtContraseña.SelectionLength = 0;
            txtContraseña.SelectionStart = 0;
            txtContraseña.ShortcutsEnabled = true;
            txtContraseña.Size = new Size(357, 48);
            txtContraseña.TabIndex = 2;
            txtContraseña.TabStop = false;
            txtContraseña.TextAlign = HorizontalAlignment.Left;
            txtContraseña.TrailingIcon = null;
            txtContraseña.UseSystemPasswordChar = false;
            // 
            // txtUsuario
            // 
            txtUsuario.AnimateReadOnly = false;
            txtUsuario.BackgroundImageLayout = ImageLayout.None;
            txtUsuario.CharacterCasing = CharacterCasing.Normal;
            txtUsuario.Depth = 0;
            txtUsuario.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtUsuario.HideSelection = true;
            txtUsuario.Hint = "Usuario";
            txtUsuario.LeadingIcon = null;
            txtUsuario.Location = new Point(82, 125);
            txtUsuario.Margin = new Padding(4, 5, 4, 5);
            txtUsuario.MaxLength = 32767;
            txtUsuario.MouseState = MaterialSkin.MouseState.OUT;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PasswordChar = '\0';
            txtUsuario.PrefixSuffixText = null;
            txtUsuario.ReadOnly = false;
            txtUsuario.RightToLeft = RightToLeft.No;
            txtUsuario.SelectedText = "";
            txtUsuario.SelectionLength = 0;
            txtUsuario.SelectionStart = 0;
            txtUsuario.ShortcutsEnabled = true;
            txtUsuario.Size = new Size(357, 48);
            txtUsuario.TabIndex = 1;
            txtUsuario.TabStop = false;
            txtUsuario.TextAlign = HorizontalAlignment.Left;
            txtUsuario.TrailingIcon = null;
            txtUsuario.UseSystemPasswordChar = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Depth = 0;
            lblTitulo.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblTitulo.Location = new Point(175, 55);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.MouseState = MaterialSkin.MouseState.HOVER;
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(113, 19);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "INICIAR SESIÓN";
            // 
            // login
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 750);
            Controls.Add(cardLogin);
            Margin = new Padding(4, 5, 4, 5);
            Name = "login";
            Padding = new Padding(4, 107, 4, 5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "login";
            Load += login_Load;
            Resize += login_Resize;
            cardLogin.ResumeLayout(false);
            cardLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialCard cardLogin;
        private MaterialSkin.Controls.MaterialLabel lblTitulo;
        private MaterialSkin.Controls.MaterialTextBox2 txtUsuario;
        private MaterialSkin.Controls.MaterialTextBox2 txtContraseña;
        private MaterialSkin.Controls.MaterialButton btnIniciarSesion;
        private MaterialSkin.Controls.MaterialButton btnRecuperarContraseña;
    }
}