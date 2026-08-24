using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    partial class FrmNuevaContraseña
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private MaterialCard cardPrincipal;
        private Label lblInstruccion;
        private TextBox txtNuevaContraseña;
        private MaterialButton btnSiguiente;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.cardPrincipal = new MaterialCard();
            this.lblInstruccion = new Label();
            this.txtNuevaContraseña = new TextBox();
            this.btnSiguiente = new MaterialButton();

            this.cardPrincipal.SuspendLayout();
            this.SuspendLayout();

            // 
            // FrmNuevaContraseña
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.ClientSize = new Size(670, 450);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.cardPrincipal);
            this.Name = "FrmNuevaContraseña";
            this.Padding = new Padding(4, 107, 4, 5);
            this.Text = "Recuperar Contraseña";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += FrmNuevaContraseña_Load;

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
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(450, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Recuperar Contraseña";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // cardPrincipal
            // 
            this.cardPrincipal.BackColor = Color.White;
            this.cardPrincipal.Controls.Add(this.lblInstruccion);
            this.cardPrincipal.Controls.Add(this.txtNuevaContraseña);
            this.cardPrincipal.Controls.Add(this.btnSiguiente);
            this.cardPrincipal.Depth = 0;
            this.cardPrincipal.ForeColor = Color.FromArgb(222, 0, 0, 0);
            this.cardPrincipal.Margin = new Padding(14);
            this.cardPrincipal.MouseState = MouseState.HOVER;
            this.cardPrincipal.Name = "cardPrincipal";
            this.cardPrincipal.Padding = new Padding(20);
            this.cardPrincipal.Size = new Size(450, 205);
            this.cardPrincipal.TabIndex = 1;

            // 
            // lblInstruccion
            // 
            this.lblInstruccion.AutoSize = false;
            this.lblInstruccion.Font = new Font(
                "Microsoft Sans Serif",
                16F,
                FontStyle.Bold
            );
            this.lblInstruccion.ForeColor = Color.FromArgb(90, 96, 110);
            this.lblInstruccion.Location = new Point(40, 25);
            this.lblInstruccion.Name = "lblInstruccion";
            this.lblInstruccion.Size = new Size(370, 35);
            this.lblInstruccion.TabIndex = 0;
            this.lblInstruccion.Text = "Ingrese su nueva contraseña";
            this.lblInstruccion.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // txtNuevaContraseña
            // 
            this.txtNuevaContraseña.BackColor = Color.White;
            this.txtNuevaContraseña.BorderStyle = BorderStyle.FixedSingle;
            this.txtNuevaContraseña.Font = new Font(
                "Microsoft Sans Serif",
                14F,
                FontStyle.Regular
            );
            this.txtNuevaContraseña.Location = new Point(40, 75);
            this.txtNuevaContraseña.Name = "txtNuevaContraseña";
            this.txtNuevaContraseña.Size = new Size(370, 30);
            this.txtNuevaContraseña.TabIndex = 1;
            this.txtNuevaContraseña.UseSystemPasswordChar = true;

            // 
            // btnSiguiente
            // 
            this.btnSiguiente.AutoSize = false;
            this.btnSiguiente.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnSiguiente.Density = MaterialButton.MaterialButtonDensity.Default;
            this.btnSiguiente.Depth = 0;
            this.btnSiguiente.HighEmphasis = true;
            this.btnSiguiente.Icon = null;
            this.btnSiguiente.Location = new Point(40, 130);
            this.btnSiguiente.Margin = new Padding(4, 6, 4, 6);
            this.btnSiguiente.MouseState = MouseState.HOVER;
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.NoAccentTextColor = Color.Empty;
            this.btnSiguiente.Size = new Size(370, 48);
            this.btnSiguiente.TabIndex = 2;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.Type = MaterialButton.MaterialButtonType.Contained;
            this.btnSiguiente.UseAccentColor = false;
            this.btnSiguiente.UseVisualStyleBackColor = true;

            // 
            // Finalizar
            // 
            this.cardPrincipal.ResumeLayout(false);
            this.cardPrincipal.PerformLayout();

            this.ResumeLayout(false);
        }
    }
}