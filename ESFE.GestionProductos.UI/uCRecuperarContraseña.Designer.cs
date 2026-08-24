using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    partial class uCRecuperarContraseña
    {
        private System.ComponentModel.IContainer components = null;

        private MaterialCard cardRecuperar;
        private Label lblTitulo;
        private Label lblInstruccion;
        private TextBox txtCorreo;
        private MaterialButton btnSiguiente;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cardRecuperar = new MaterialCard();
            this.lblTitulo = new Label();
            this.lblInstruccion = new Label();
            this.txtCorreo = new TextBox();
            this.btnSiguiente = new MaterialButton();

            this.cardRecuperar.SuspendLayout();
            this.SuspendLayout();

            // 
            // uCRecuperarContraseña
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.White;
            this.Name = "uCRecuperarContraseña";
            this.Size = new Size(670, 360);

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Font = new Font(
                "Microsoft Sans Serif",
                15F,
                FontStyle.Bold
            );
            this.lblTitulo.ForeColor = Color.FromArgb(95, 99, 115);
            this.lblTitulo.Location = new Point(110, 45);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(450, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Recuperar Contraseña";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // cardRecuperar
            // 
            this.cardRecuperar.BackColor = Color.White;
            this.cardRecuperar.Depth = 0;
            this.cardRecuperar.ForeColor = Color.FromArgb(222, 0, 0, 0);
            this.cardRecuperar.Location = new Point(110, 95);
            this.cardRecuperar.Margin = new Padding(14);
            this.cardRecuperar.MouseState = MouseState.HOVER;
            this.cardRecuperar.Name = "cardRecuperar";
            this.cardRecuperar.Padding = new Padding(20);
            this.cardRecuperar.Size = new Size(450, 220);
            this.cardRecuperar.TabIndex = 1;

            // 
            // lblInstruccion
            // 
            this.lblInstruccion.AutoSize = false;
            this.lblInstruccion.Font = new Font(
                "Microsoft Sans Serif",
                8F,
                FontStyle.Regular
            );
            this.lblInstruccion.ForeColor = Color.FromArgb(95, 99, 115);
            this.lblInstruccion.Location = new Point(40, 25);
            this.lblInstruccion.Name = "lblInstruccion";
            this.lblInstruccion.Size = new Size(370, 30);
            this.lblInstruccion.TabIndex = 0;
            this.lblInstruccion.Text = "Ingrese el correo electrónico registrado en su cuenta:";
            this.lblInstruccion.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // txtCorreo
            // 
            this.txtCorreo.BorderStyle = BorderStyle.FixedSingle;
            this.txtCorreo.Font = new Font(
                "Microsoft Sans Serif",
                9F,
                FontStyle.Regular
            );
            this.txtCorreo.Location = new Point(118, 78);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new Size(214, 21);
            this.txtCorreo.TabIndex = 0;
            this.txtCorreo.TextAlign = HorizontalAlignment.Center;

            // 
            // btnSiguiente
            // 
            this.btnSiguiente.AutoSize = false;
            this.btnSiguiente.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnSiguiente.Density = MaterialButton.MaterialButtonDensity.Default;
            this.btnSiguiente.Depth = 0;
            this.btnSiguiente.HighEmphasis = true;
            this.btnSiguiente.Icon = null;
            this.btnSiguiente.Location = new Point(120, 141);
            this.btnSiguiente.Margin = new Padding(4, 6, 4, 6);
            this.btnSiguiente.MouseState = MouseState.HOVER;
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.NoAccentTextColor = Color.Empty;
            this.btnSiguiente.Size = new Size(210, 42);
            this.btnSiguiente.TabIndex = 1;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.Type = MaterialButton.MaterialButtonType.Contained;
            this.btnSiguiente.UseAccentColor = false;
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new EventHandler(this.btnSiguiente_Click);

            // 
            // cardRecuperar
            // 
            this.cardRecuperar.Controls.Add(this.lblInstruccion);
            this.cardRecuperar.Controls.Add(this.txtCorreo);
            this.cardRecuperar.Controls.Add(this.btnSiguiente);

            // 
            // uCRecuperarContraseña
            // 
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.cardRecuperar);

            this.cardRecuperar.ResumeLayout(false);
            this.cardRecuperar.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}