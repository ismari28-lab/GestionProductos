using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    partial class FrmCodigoAcceso
    {
        private System.ComponentModel.IContainer components = null;

        private MaterialCard cardPrincipal;
        private Panel pnlCodigo;
        private Label lblVerificarCodigo;

        private TextBox txtCodigo1;
        private TextBox txtCodigo2;
        private TextBox txtCodigo3;
        private TextBox txtCodigo4;
        private TextBox txtCodigo5;
        private TextBox txtCodigo6;

        private MaterialButton btnVolverEnviar;
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
            this.cardPrincipal = new MaterialCard();
            this.pnlCodigo = new Panel();
            this.lblVerificarCodigo = new Label();

            this.txtCodigo1 = new TextBox();
            this.txtCodigo2 = new TextBox();
            this.txtCodigo3 = new TextBox();
            this.txtCodigo4 = new TextBox();
            this.txtCodigo5 = new TextBox();
            this.txtCodigo6 = new TextBox();

            this.btnVolverEnviar = new MaterialButton();
            this.btnSiguiente = new MaterialButton();

            this.cardPrincipal.SuspendLayout();
            this.pnlCodigo.SuspendLayout();
            this.SuspendLayout();

            // 
            // FrmCodigoAcceso
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(250, 250, 250);
            this.ClientSize = new Size(670, 480);
            this.Controls.Add(this.cardPrincipal);
            this.Name = "FrmCodigoAcceso";
            this.Padding = new Padding(4, 107, 4, 5);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Código de Acceso";

            // 
            // cardPrincipal
            // 
            this.cardPrincipal.BackColor = Color.White;
            this.cardPrincipal.Controls.Add(this.pnlCodigo);
            this.cardPrincipal.Depth = 0;
            this.cardPrincipal.ForeColor = Color.FromArgb(222, 0, 0, 0);
            this.cardPrincipal.Location = new Point(110, 125);
            this.cardPrincipal.Margin = new Padding(14);
            this.cardPrincipal.MouseState = MouseState.HOVER;
            this.cardPrincipal.Name = "cardPrincipal";
            this.cardPrincipal.Padding = new Padding(20);
            this.cardPrincipal.Size = new Size(450, 295);
            this.cardPrincipal.TabIndex = 1;

            // 
            // pnlCodigo
            // 
            this.pnlCodigo.BackColor = Color.FromArgb(245, 245, 245);
            this.pnlCodigo.Controls.Add(this.lblVerificarCodigo);
            this.pnlCodigo.Controls.Add(this.txtCodigo1);
            this.pnlCodigo.Controls.Add(this.txtCodigo2);
            this.pnlCodigo.Controls.Add(this.txtCodigo3);
            this.pnlCodigo.Controls.Add(this.txtCodigo4);
            this.pnlCodigo.Controls.Add(this.txtCodigo5);
            this.pnlCodigo.Controls.Add(this.txtCodigo6);
            this.pnlCodigo.Controls.Add(this.btnVolverEnviar);
            this.pnlCodigo.Controls.Add(this.btnSiguiente);
            this.pnlCodigo.Location = new Point(79, 51);
            this.pnlCodigo.Name = "pnlCodigo";
            this.pnlCodigo.Size = new Size(292, 192);
            this.pnlCodigo.TabIndex = 0;

            // 
            // lblVerificarCodigo
            // 
            this.lblVerificarCodigo.AutoSize = false;
            this.lblVerificarCodigo.Font = new Font(
                "Microsoft Sans Serif",
                9.5F,
                FontStyle.Bold
            );
            this.lblVerificarCodigo.ForeColor = Color.FromArgb(35, 35, 35);
            this.lblVerificarCodigo.Location = new Point(15, 8);
            this.lblVerificarCodigo.Name = "lblVerificarCodigo";
            this.lblVerificarCodigo.Size = new Size(262, 40);
            this.lblVerificarCodigo.TabIndex = 0;
            this.lblVerificarCodigo.Text = "Ingrese el código de acceso enviado a su correo";
            this.lblVerificarCodigo.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // txtCodigo1
            // 
            this.txtCodigo1.BorderStyle = BorderStyle.FixedSingle;
            this.txtCodigo1.Font = new Font("Microsoft Sans Serif", 14F);
            this.txtCodigo1.Location = new Point(35, 55);
            this.txtCodigo1.MaxLength = 1;
            this.txtCodigo1.Name = "txtCodigo1";
            this.txtCodigo1.Size = new Size(25, 29);
            this.txtCodigo1.TabIndex = 1;
            this.txtCodigo1.TextAlign = HorizontalAlignment.Center;

            // 
            // txtCodigo2
            // 
            this.txtCodigo2.BorderStyle = BorderStyle.FixedSingle;
            this.txtCodigo2.Font = new Font("Microsoft Sans Serif", 14F);
            this.txtCodigo2.Location = new Point(75, 55);
            this.txtCodigo2.MaxLength = 1;
            this.txtCodigo2.Name = "txtCodigo2";
            this.txtCodigo2.Size = new Size(25, 29);
            this.txtCodigo2.TabIndex = 2;
            this.txtCodigo2.TextAlign = HorizontalAlignment.Center;

            // 
            // txtCodigo3
            // 
            this.txtCodigo3.BorderStyle = BorderStyle.FixedSingle;
            this.txtCodigo3.Font = new Font("Microsoft Sans Serif", 14F);
            this.txtCodigo3.Location = new Point(115, 55);
            this.txtCodigo3.MaxLength = 1;
            this.txtCodigo3.Name = "txtCodigo3";
            this.txtCodigo3.Size = new Size(25, 29);
            this.txtCodigo3.TabIndex = 3;
            this.txtCodigo3.TextAlign = HorizontalAlignment.Center;

            // 
            // txtCodigo4
            // 
            this.txtCodigo4.BorderStyle = BorderStyle.FixedSingle;
            this.txtCodigo4.Font = new Font("Microsoft Sans Serif", 14F);
            this.txtCodigo4.Location = new Point(155, 55);
            this.txtCodigo4.MaxLength = 1;
            this.txtCodigo4.Name = "txtCodigo4";
            this.txtCodigo4.Size = new Size(25, 29);
            this.txtCodigo4.TabIndex = 4;
            this.txtCodigo4.TextAlign = HorizontalAlignment.Center;

            // 
            // txtCodigo5
            // 
            this.txtCodigo5.BorderStyle = BorderStyle.FixedSingle;
            this.txtCodigo5.Font = new Font("Microsoft Sans Serif", 14F);
            this.txtCodigo5.Location = new Point(195, 55);
            this.txtCodigo5.MaxLength = 1;
            this.txtCodigo5.Name = "txtCodigo5";
            this.txtCodigo5.Size = new Size(25, 29);
            this.txtCodigo5.TabIndex = 5;
            this.txtCodigo5.TextAlign = HorizontalAlignment.Center;

            // 
            // txtCodigo6
            // 
            this.txtCodigo6.BorderStyle = BorderStyle.FixedSingle;
            this.txtCodigo6.Font = new Font("Microsoft Sans Serif", 14F);
            this.txtCodigo6.Location = new Point(235, 55);
            this.txtCodigo6.MaxLength = 1;
            this.txtCodigo6.Name = "txtCodigo6";
            this.txtCodigo6.Size = new Size(25, 29);
            this.txtCodigo6.TabIndex = 6;
            this.txtCodigo6.TextAlign = HorizontalAlignment.Center;

            // 
            // btnVolverEnviar
            // 
            this.btnVolverEnviar.AutoSize = false;
            this.btnVolverEnviar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnVolverEnviar.Density = MaterialButton.MaterialButtonDensity.Default;
            this.btnVolverEnviar.Depth = 0;
            this.btnVolverEnviar.HighEmphasis = false;
            this.btnVolverEnviar.Icon = null;
            this.btnVolverEnviar.Location = new Point(8, 128);
            this.btnVolverEnviar.Margin = new Padding(4, 6, 4, 6);
            this.btnVolverEnviar.MouseState = MouseState.HOVER;
            this.btnVolverEnviar.Name = "btnVolverEnviar";
            this.btnVolverEnviar.NoAccentTextColor = Color.Empty;
            this.btnVolverEnviar.Size = new Size(150, 30);
            this.btnVolverEnviar.TabIndex = 7;
            this.btnVolverEnviar.Text = "Volver a enviar";
            this.btnVolverEnviar.Type = MaterialButton.MaterialButtonType.Contained;
            this.btnVolverEnviar.UseAccentColor = false;
            this.btnVolverEnviar.UseVisualStyleBackColor = true;

            // 
            // btnSiguiente
            // 
            this.btnSiguiente.AutoSize = false;
            this.btnSiguiente.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnSiguiente.Density = MaterialButton.MaterialButtonDensity.Default;
            this.btnSiguiente.Depth = 0;
            this.btnSiguiente.HighEmphasis = true;
            this.btnSiguiente.Icon = null;
            this.btnSiguiente.Location = new Point(166, 128);
            this.btnSiguiente.Margin = new Padding(4, 6, 4, 6);
            this.btnSiguiente.MouseState = MouseState.HOVER;
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.NoAccentTextColor = Color.Empty;
            this.btnSiguiente.Size = new Size(120, 30);
            this.btnSiguiente.TabIndex = 8;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.Type = MaterialButton.MaterialButtonType.Contained;
            this.btnSiguiente.UseAccentColor = false;
            this.btnSiguiente.UseVisualStyleBackColor = true;

            // 
            // Finalizar
            // 
            this.pnlCodigo.ResumeLayout(false);
            this.pnlCodigo.PerformLayout();

            this.cardPrincipal.ResumeLayout(false);
            this.cardPrincipal.PerformLayout();

            this.ResumeLayout(false);
        }
    }
}