namespace ESFE.GestionProductos.UI
{
    partial class UcCatalogoProductos
    {
        /// <summary> 
        /// Variable de diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el diseñador; no modifique
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTituloPagina = new MaterialSkin.Controls.MaterialLabel();

            this.cardConsulta = new MaterialSkin.Controls.MaterialCard();
            this.lblConsultaRapida = new MaterialSkin.Controls.MaterialLabel();
            this.txtConsultaRapida = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnBuscar = new MaterialSkin.Controls.MaterialButton();

            this.cardInfoProducto = new MaterialSkin.Controls.MaterialCard();
            this.lblInfoProducto = new MaterialSkin.Controls.MaterialLabel();
            this.txtInfoProducto = new MaterialSkin.Controls.MaterialTextBox2();
            this.pnlResultado = new System.Windows.Forms.Panel();
            this.pnlAlertaStockBajo = new System.Windows.Forms.Panel();
            this.lblAlertaStockBajo = new MaterialSkin.Controls.MaterialLabel();

            this.cardConsulta.SuspendLayout();
            this.cardInfoProducto.SuspendLayout();
            this.pnlResultado.SuspendLayout();
            this.pnlAlertaStockBajo.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTituloPagina
            // 
            this.lblTituloPagina.AutoSize = true;
            this.lblTituloPagina.Depth = 0;
            this.lblTituloPagina.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.lblTituloPagina.Location = new System.Drawing.Point(20, 20);
            this.lblTituloPagina.Name = "lblTituloPagina";
            this.lblTituloPagina.Size = new System.Drawing.Size(220, 29);
            this.lblTituloPagina.TabIndex = 0;
            this.lblTituloPagina.Text = "Catálogo de Productos";

            // 
            // cardConsulta
            // 
            this.cardConsulta.BackColor = System.Drawing.Color.White;
            this.cardConsulta.Controls.Add(this.lblConsultaRapida);
            this.cardConsulta.Controls.Add(this.txtConsultaRapida);
            this.cardConsulta.Controls.Add(this.btnBuscar);
            this.cardConsulta.Depth = 0;
            this.cardConsulta.Location = new System.Drawing.Point(20, 65);
            this.cardConsulta.Name = "cardConsulta";
            this.cardConsulta.Padding = new System.Windows.Forms.Padding(20);
            this.cardConsulta.Size = new System.Drawing.Size(560, 110);
            this.cardConsulta.TabIndex = 1;

            // 
            // lblConsultaRapida
            // 
            this.lblConsultaRapida.AutoSize = true;
            this.lblConsultaRapida.Depth = 0;
            this.lblConsultaRapida.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.lblConsultaRapida.Location = new System.Drawing.Point(20, 15);
            this.lblConsultaRapida.Name = "lblConsultaRapida";
            this.lblConsultaRapida.Size = new System.Drawing.Size(220, 19);
            this.lblConsultaRapida.TabIndex = 0;
            this.lblConsultaRapida.Text = "Consulta Rápida de Productos";

            // 
            // txtConsultaRapida
            // 
            this.txtConsultaRapida.AnimateReadOnly = false;
            this.txtConsultaRapida.Depth = 0;
            this.txtConsultaRapida.Hint = "Buscar por nombre, SKU o código de barras...";
            this.txtConsultaRapida.Location = new System.Drawing.Point(20, 50);
            this.txtConsultaRapida.MaxLength = 100;
            this.txtConsultaRapida.MouseState = MaterialSkin.MouseState.OUT;
            this.txtConsultaRapida.Name = "txtConsultaRapida";
            this.txtConsultaRapida.PasswordChar = '\0';
            this.txtConsultaRapida.SelectedText = "";
            this.txtConsultaRapida.SelectionLength = 0;
            this.txtConsultaRapida.SelectionStart = 0;
            this.txtConsultaRapida.Size = new System.Drawing.Size(400, 36);
            this.txtConsultaRapida.TabIndex = 1;
            this.txtConsultaRapida.UseSystemPasswordChar = false;
            this.txtConsultaRapida.Text = "";

            // 
            // btnBuscar
            // 
            this.btnBuscar.AutoSize = false;
            this.btnBuscar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnBuscar.Depth = 0;
            this.btnBuscar.HighEmphasis = true;
            this.btnBuscar.Icon = null;
            this.btnBuscar.Location = new System.Drawing.Point(440, 50);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnBuscar.Size = new System.Drawing.Size(100, 36);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBuscar.UseAccentColor = false;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);

            // 
            // cardInfoProducto
            // 
            this.cardInfoProducto.BackColor = System.Drawing.Color.White;
            this.cardInfoProducto.Controls.Add(this.lblInfoProducto);
            this.cardInfoProducto.Controls.Add(this.txtInfoProducto);
            this.cardInfoProducto.Controls.Add(this.pnlResultado);
            this.cardInfoProducto.Depth = 0;
            this.cardInfoProducto.Location = new System.Drawing.Point(20, 190);
            this.cardInfoProducto.Name = "cardInfoProducto";
            this.cardInfoProducto.Padding = new System.Windows.Forms.Padding(20);
            this.cardInfoProducto.Size = new System.Drawing.Size(560, 360);
            this.cardInfoProducto.TabIndex = 2;

            // 
            // lblInfoProducto
            // 
            this.lblInfoProducto.AutoSize = true;
            this.lblInfoProducto.Depth = 0;
            this.lblInfoProducto.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.lblInfoProducto.Location = new System.Drawing.Point(20, 15);
            this.lblInfoProducto.Name = "lblInfoProducto";
            this.lblInfoProducto.Size = new System.Drawing.Size(170, 19);
            this.lblInfoProducto.TabIndex = 0;
            this.lblInfoProducto.Text = "Información del Producto";

            // 
            // txtInfoProducto
            // 
            this.txtInfoProducto.AnimateReadOnly = false;
            this.txtInfoProducto.Depth = 0;
            this.txtInfoProducto.Hint = "Código o nombre del producto";
            this.txtInfoProducto.Location = new System.Drawing.Point(20, 50);
            this.txtInfoProducto.MaxLength = 100;
            this.txtInfoProducto.MouseState = MaterialSkin.MouseState.OUT;
            this.txtInfoProducto.Name = "txtInfoProducto";
            this.txtInfoProducto.PasswordChar = '\0';
            this.txtInfoProducto.SelectedText = "";
            this.txtInfoProducto.SelectionLength = 0;
            this.txtInfoProducto.SelectionStart = 0;
            this.txtInfoProducto.Size = new System.Drawing.Size(520, 36);
            this.txtInfoProducto.TabIndex = 1;
            this.txtInfoProducto.UseSystemPasswordChar = false;
            this.txtInfoProducto.Text = "";

            // 
            // pnlResultado
            // 
            this.pnlResultado.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlResultado.Controls.Add(this.pnlAlertaStockBajo);
            this.pnlResultado.Location = new System.Drawing.Point(20, 100);
            this.pnlResultado.Name = "pnlResultado";
            this.pnlResultado.Padding = new System.Windows.Forms.Padding(15);
            this.pnlResultado.Size = new System.Drawing.Size(520, 230);
            this.pnlResultado.TabIndex = 2;

            // 
            // pnlAlertaStockBajo
            // 
            this.pnlAlertaStockBajo.BackColor = System.Drawing.Color.FromArgb(150, 165, 205);
            this.pnlAlertaStockBajo.Controls.Add(this.lblAlertaStockBajo);
            this.pnlAlertaStockBajo.Location = new System.Drawing.Point(15, 15);
            this.pnlAlertaStockBajo.Name = "pnlAlertaStockBajo";
            this.pnlAlertaStockBajo.Size = new System.Drawing.Size(490, 36);
            this.pnlAlertaStockBajo.TabIndex = 0;

            // 
            // lblAlertaStockBajo
            // 
            this.lblAlertaStockBajo.AutoSize = false;
            this.lblAlertaStockBajo.Depth = 0;
            this.lblAlertaStockBajo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlertaStockBajo.ForeColor = System.Drawing.Color.White;
            this.lblAlertaStockBajo.Location = new System.Drawing.Point(0, 0);
            this.lblAlertaStockBajo.Name = "lblAlertaStockBajo";
            this.lblAlertaStockBajo.Size = new System.Drawing.Size(490, 36);
            this.lblAlertaStockBajo.TabIndex = 0;
            this.lblAlertaStockBajo.Text = "Alerta de Stock Bajo";
            this.lblAlertaStockBajo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // UcCatalogoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(this.cardInfoProducto);
            this.Controls.Add(this.cardConsulta);
            this.Controls.Add(this.lblTituloPagina);
            this.Name = "UcCatalogoProductos";
            this.Size = new System.Drawing.Size(610, 580);
            this.cardConsulta.ResumeLayout(false);
            this.cardConsulta.PerformLayout();
            this.cardInfoProducto.ResumeLayout(false);
            this.cardInfoProducto.PerformLayout();
            this.pnlResultado.ResumeLayout(false);
            this.pnlAlertaStockBajo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel lblTituloPagina;

        private MaterialSkin.Controls.MaterialCard cardConsulta;
        private MaterialSkin.Controls.MaterialLabel lblConsultaRapida;
        private MaterialSkin.Controls.MaterialTextBox2 txtConsultaRapida;
        private MaterialSkin.Controls.MaterialButton btnBuscar;

        private MaterialSkin.Controls.MaterialCard cardInfoProducto;
        private MaterialSkin.Controls.MaterialLabel lblInfoProducto;
        private MaterialSkin.Controls.MaterialTextBox2 txtInfoProducto;
        private System.Windows.Forms.Panel pnlResultado;
        private System.Windows.Forms.Panel pnlAlertaStockBajo;
        private MaterialSkin.Controls.MaterialLabel lblAlertaStockBajo;
    }
}