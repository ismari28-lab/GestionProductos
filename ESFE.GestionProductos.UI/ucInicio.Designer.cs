namespace ESFE.GestionProductos.UI
{
    partial class ucInicio
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

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            this.lblBienvenido = new System.Windows.Forms.Label();
            this.lblResumenMensual = new System.Windows.Forms.Label();
            this.panelContenedorTarjetas = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlVentas = new System.Windows.Forms.Panel();
            this.lblTituloVentas = new System.Windows.Forms.Label();
            this.dgvVentasRecientes = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegistered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlPaginacion = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPag1 = new System.Windows.Forms.Label();
            this.lblPag2 = new System.Windows.Forms.Label();
            this.lblPag3 = new System.Windows.Forms.Label();
            this.lblPag4 = new System.Windows.Forms.Label();
            this.lblPag5 = new System.Windows.Forms.Label();
            this.lblPuntos = new System.Windows.Forms.Label();
            this.lblPag20 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvVentasRecientes)).BeginInit();
            this.pnlVentas.SuspendLayout();
            this.pnlPaginacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBienvenido
            // 
            this.lblBienvenido.AutoSize = true;
            this.lblBienvenido.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblBienvenido.ForeColor = System.Drawing.Color.FromArgb(30, 40, 50);
            this.lblBienvenido.Location = new System.Drawing.Point(28, 20);
            this.lblBienvenido.Text = "Bienvenido";
            // 
            // lblResumenMensual
            // 
            this.lblResumenMensual.AutoSize = true;
            this.lblResumenMensual.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblResumenMensual.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblResumenMensual.Location = new System.Drawing.Point(30, 75);
            this.lblResumenMensual.Text = "Resumen Mensual";
            // 
            // panelContenedorTarjetas
            // 
            this.panelContenedorTarjetas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContenedorTarjetas.Location = new System.Drawing.Point(30, 110);
            this.panelContenedorTarjetas.Name = "panelContenedorTarjetas";
            this.panelContenedorTarjetas.Size = new System.Drawing.Size(1050, 165);   // antes 135
            // 
            // pnlVentas  (contenedor blanco con borde para la tabla)
            // 
            this.pnlVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlVentas.BackColor = System.Drawing.Color.White;
            this.pnlVentas.Controls.Add(this.dgvVentasRecientes);
            this.pnlVentas.Controls.Add(this.lblTituloVentas);
            this.pnlVentas.Controls.Add(this.pnlPaginacion);
            this.pnlVentas.Location = new System.Drawing.Point(30, 300);   // antes 270
            this.pnlVentas.Name = "pnlVentas";
            this.pnlVentas.Padding = new System.Windows.Forms.Padding(20);
            this.pnlVentas.Size = new System.Drawing.Size(1050, 300);
            // 
            // lblTituloVentas
            // 
            this.lblTituloVentas.AutoSize = true;
            this.lblTituloVentas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloVentas.ForeColor = System.Drawing.Color.FromArgb(30, 40, 50);
            this.lblTituloVentas.Location = new System.Drawing.Point(20, 15);
            this.lblTituloVentas.Text = "Ventas Recientes";
            // 
            // dgvVentasRecientes
            // 
            this.dgvVentasRecientes.AllowUserToAddRows = false;
            this.dgvVentasRecientes.AllowUserToDeleteRows = false;
            this.dgvVentasRecientes.AllowUserToResizeRows = false;
            this.dgvVentasRecientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVentasRecientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVentasRecientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvVentasRecientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVentasRecientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvVentasRecientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvVentasRecientes.ColumnHeadersHeight = 40;
            this.dgvVentasRecientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
        this.colName, this.colOrderDate, this.colPhone, this.colLocation, this.colRegistered, this.colActions});
            this.dgvVentasRecientes.EnableHeadersVisualStyles = false;
            this.dgvVentasRecientes.GridColor = System.Drawing.Color.FromArgb(235, 238, 242);
            this.dgvVentasRecientes.Location = new System.Drawing.Point(20, 50);
            this.dgvVentasRecientes.Name = "dgvVentasRecientes";
            this.dgvVentasRecientes.ReadOnly = true;
            this.dgvVentasRecientes.RowHeadersVisible = false;
            this.dgvVentasRecientes.RowTemplate.Height = 42;
            this.dgvVentasRecientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVentasRecientes.Size = new System.Drawing.Size(1010, 190);
            // 
            // Columnas
            // 
            this.colName.HeaderText = "Name"; this.colName.Name = "colName"; this.colName.ReadOnly = true;
            this.colOrderDate.HeaderText = "Order Date"; this.colOrderDate.Name = "colOrderDate"; this.colOrderDate.ReadOnly = true;
            this.colPhone.HeaderText = "Phone Number"; this.colPhone.Name = "colPhone"; this.colPhone.ReadOnly = true;
            this.colLocation.HeaderText = "Location"; this.colLocation.Name = "colLocation"; this.colLocation.ReadOnly = true;
            this.colRegistered.HeaderText = "Registered"; this.colRegistered.Name = "colRegistered"; this.colRegistered.ReadOnly = true;
            this.colActions.HeaderText = "Actions"; this.colActions.Name = "colActions"; this.colActions.ReadOnly = true;
            // 
            // pnlPaginacion
            // 
            this.pnlPaginacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPaginacion.Controls.Add(this.lblPag1);
            this.pnlPaginacion.Controls.Add(this.lblPag2);
            this.pnlPaginacion.Controls.Add(this.lblPag3);
            this.pnlPaginacion.Controls.Add(this.lblPag4);
            this.pnlPaginacion.Controls.Add(this.lblPag5);
            this.pnlPaginacion.Controls.Add(this.lblPuntos);
            this.pnlPaginacion.Controls.Add(this.lblPag20);
            this.pnlPaginacion.Location = new System.Drawing.Point(430, 255);
            this.pnlPaginacion.Name = "pnlPaginacion";
            this.pnlPaginacion.Size = new System.Drawing.Size(230, 35);
            // 
            // Labels de paginación (estilo pill)
            // 
            ConfigurarLabelPagDesigner(this.lblPag1, "1", false);
            ConfigurarLabelPagDesigner(this.lblPag2, "2", true);
            ConfigurarLabelPagDesigner(this.lblPag3, "3", false);
            ConfigurarLabelPagDesigner(this.lblPag4, "4", false);
            ConfigurarLabelPagDesigner(this.lblPag5, "5", false);
            this.lblPuntos.AutoSize = true;
            this.lblPuntos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPuntos.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblPuntos.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lblPuntos.Text = "...";
            ConfigurarLabelPagDesigner(this.lblPag20, "20", false);
            // 
            // ucInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Controls.Add(this.pnlVentas);
            this.Controls.Add(this.panelContenedorTarjetas);
            this.Controls.Add(this.lblResumenMensual);
            this.Controls.Add(this.lblBienvenido);
            this.Name = "ucInicio";
            this.Size = new System.Drawing.Size(1100, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentasRecientes)).EndInit();
            this.pnlVentas.ResumeLayout(false);
            this.pnlVentas.PerformLayout();
            this.pnlPaginacion.ResumeLayout(false);
            this.pnlPaginacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigurarLabelPagDesigner(System.Windows.Forms.Label lbl, string texto, bool activo)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9F, activo ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular);
            lbl.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            lbl.Margin = new System.Windows.Forms.Padding(3);
            lbl.Text = texto;
            if (activo)
            {
                lbl.BackColor = System.Drawing.Color.FromArgb(90, 70, 180);
                lbl.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lbl.ForeColor = System.Drawing.Color.FromArgb(80, 90, 100);
            }
        }

        #endregion

        private System.Windows.Forms.Label lblBienvenido;
        private System.Windows.Forms.Label lblResumenMensual;
        private System.Windows.Forms.FlowLayoutPanel panelContenedorTarjetas;
        private System.Windows.Forms.Panel pnlVentas;
        private System.Windows.Forms.Label lblTituloVentas;
        private System.Windows.Forms.DataGridView dgvVentasRecientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegistered;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
        private System.Windows.Forms.FlowLayoutPanel pnlPaginacion;
        private System.Windows.Forms.Label lblPag1;
        private System.Windows.Forms.Label lblPag2;
        private System.Windows.Forms.Label lblPag3;
        private System.Windows.Forms.Label lblPag4;
        private System.Windows.Forms.Label lblPag5;
        private System.Windows.Forms.Label lblPuntos;
        private System.Windows.Forms.Label lblPag20;
    }
}