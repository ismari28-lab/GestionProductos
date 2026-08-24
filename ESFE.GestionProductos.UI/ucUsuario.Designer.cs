namespace ESFE.GestionProductos.UI
{
    partial class ucUsuario
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
            components = new System.ComponentModel.Container();
            this.lblTituloUsuarios = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.txtBuscar = new MaterialSkin.Controls.MaterialTextBox2();
            this.cboFiltro = new MaterialSkin.Controls.MaterialComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pnlLista = new System.Windows.Forms.Panel();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTituloLista = new System.Windows.Forms.Label();
            this.pnlPaginacion = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPag1 = new System.Windows.Forms.Label();
            this.lblPag2 = new System.Windows.Forms.Label();
            this.lblPag3 = new System.Windows.Forms.Label();
            this.lblPuntos = new System.Windows.Forms.Label();
            this.lblPagFinal = new System.Windows.Forms.Label();
            this.cmsOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBusqueda.SuspendLayout();
            this.pnlLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.cmsOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTituloUsuarios
            // 
            this.lblTituloUsuarios.AutoSize = true;
            this.lblTituloUsuarios.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTituloUsuarios.ForeColor = System.Drawing.Color.FromArgb(30, 40, 50);
            this.lblTituloUsuarios.Location = new System.Drawing.Point(28, 20);
            this.lblTituloUsuarios.Name = "lblTituloUsuarios";
            this.lblTituloUsuarios.Text = "Usuarios";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblSubtitulo.Location = new System.Drawing.Point(30, 75);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Text = "Gestión de cuentas del sistema";
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrear.BackColor = System.Drawing.Color.FromArgb(90, 70, 180);
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.FlatAppearance.BorderSize = 0;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCrear.ForeColor = System.Drawing.Color.White;
            this.btnCrear.Location = new System.Drawing.Point(855, 70);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(165, 42);
            this.btnCrear.Text = "+  Crear Usuario";
            this.btnCrear.UseVisualStyleBackColor = false;
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBusqueda.BackColor = System.Drawing.Color.White;
            this.pnlBusqueda.Controls.Add(this.txtBuscar);
            this.pnlBusqueda.Controls.Add(this.cboFiltro);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Location = new System.Drawing.Point(30, 130);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(1050, 110);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.AnimateReadOnly = false;
            this.txtBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtBuscar.Depth = 0;
            this.txtBuscar.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBuscar.Hint = "Buscar por nombre o correo...";
            this.txtBuscar.LeadingIcon = null;
            this.txtBuscar.Location = new System.Drawing.Point(25, 31);
            this.txtBuscar.MaxLength = 50;
            this.txtBuscar.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(570, 48);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.Text = "";
            this.txtBuscar.TrailingIcon = null;
            this.txtBuscar.UseTallSize = true;
            // 
            // cboFiltro
            // 
            this.cboFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFiltro.AutoResize = false;
            this.cboFiltro.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.cboFiltro.Depth = 0;
            this.cboFiltro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFiltro.DropDownHeight = 174;
            this.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltro.DropDownWidth = 200;
            this.cboFiltro.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cboFiltro.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            this.cboFiltro.FormattingEnabled = true;
            this.cboFiltro.Hint = "Filtro";
            this.cboFiltro.IntegralHeight = false;
            this.cboFiltro.ItemHeight = 43;
            this.cboFiltro.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            this.cboFiltro.Location = new System.Drawing.Point(610, 31);
            this.cboFiltro.MaxDropDownItems = 4;
            this.cboFiltro.MouseState = MaterialSkin.MouseState.OUT;
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Size = new System.Drawing.Size(200, 49);
            this.cboFiltro.StartIndex = 0;
            this.cboFiltro.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(52, 120, 246);
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(870, 34);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 42);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // pnlLista
            // 
            this.pnlLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLista.BackColor = System.Drawing.Color.White;
            this.pnlLista.Controls.Add(this.dgvUsuarios);
            this.pnlLista.Controls.Add(this.pnlPaginacion);
            this.pnlLista.Controls.Add(this.lblTituloLista);
            this.pnlLista.Location = new System.Drawing.Point(30, 255);
            this.pnlLista.Name = "pnlLista";
            this.pnlLista.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlLista.Size = new System.Drawing.Size(1050, 480);
            // 
            // lblTituloLista
            // 
            this.lblTituloLista.AutoSize = false;
            this.lblTituloLista.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloLista.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloLista.ForeColor = System.Drawing.Color.FromArgb(30, 40, 50);
            this.lblTituloLista.Height = 35;
            this.lblTituloLista.Name = "lblTituloLista";
            this.lblTituloLista.Text = "Usuarios Registrados";
            this.lblTituloLista.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AllowUserToResizeRows = false;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvUsuarios.ColumnHeadersHeight = 40;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colNombre, this.colRol, this.colEstado, this.colActions});
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.GridColor = System.Drawing.Color.FromArgb(235, 238, 242);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowTemplate.Height = 42;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Columnas
            // 
            this.colId.HeaderText = "ID"; this.colId.Name = "colId"; this.colId.ReadOnly = true; this.colId.FillWeight = 40;
            this.colNombre.HeaderText = "Nombre"; this.colNombre.Name = "colNombre"; this.colNombre.ReadOnly = true; this.colNombre.FillWeight = 180;
            this.colRol.HeaderText = "Rol"; this.colRol.Name = "colRol"; this.colRol.ReadOnly = true; this.colRol.FillWeight = 120;
            this.colEstado.HeaderText = "Estado"; this.colEstado.Name = "colEstado"; this.colEstado.ReadOnly = true; this.colEstado.FillWeight = 70;
            this.colActions.HeaderText = "Actions"; this.colActions.Name = "colActions"; this.colActions.ReadOnly = true; this.colActions.FillWeight = 100;
            // 
            // pnlPaginacion
            // 
            this.pnlPaginacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaginacion.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlPaginacion.Height = 45;
            this.pnlPaginacion.Padding = new System.Windows.Forms.Padding(0, 8, 5, 0);
            this.pnlPaginacion.Controls.Add(this.lblPagFinal);
            this.pnlPaginacion.Controls.Add(this.lblPuntos);
            this.pnlPaginacion.Controls.Add(this.lblPag3);
            this.pnlPaginacion.Controls.Add(this.lblPag2);
            this.pnlPaginacion.Controls.Add(this.lblPag1);
            this.pnlPaginacion.Name = "pnlPaginacion";
            // 
            // cmsOpciones
            // 
            this.cmsOpciones.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.itemEditar, this.itemEliminar });
            this.cmsOpciones.Name = "cmsOpciones";
            this.cmsOpciones.Size = new System.Drawing.Size(147, 68);
            // 
            // itemEditar
            // 
            this.itemEditar.Name = "itemEditar";
            this.itemEditar.Size = new System.Drawing.Size(146, 30);
            this.itemEditar.Text = "Editar";
            // 
            // itemEliminar
            // 
            this.itemEliminar.Name = "itemEliminar";
            this.itemEliminar.Size = new System.Drawing.Size(146, 30);
            this.itemEliminar.Text = "Eliminar";
            // 
            // ucUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Controls.Add(this.pnlLista);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblTituloUsuarios);
            this.Name = "ucUsuario";
            this.Size = new System.Drawing.Size(1100, 760);
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.pnlLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.cmsOpciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTituloUsuarios;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Panel pnlBusqueda;
        private MaterialSkin.Controls.MaterialTextBox2 txtBuscar;
        private MaterialSkin.Controls.MaterialComboBox cboFiltro;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel pnlLista;
        private System.Windows.Forms.Label lblTituloLista;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
        private System.Windows.Forms.FlowLayoutPanel pnlPaginacion;
        private System.Windows.Forms.Label lblPag1;
        private System.Windows.Forms.Label lblPag2;
        private System.Windows.Forms.Label lblPag3;
        private System.Windows.Forms.Label lblPuntos;
        private System.Windows.Forms.Label lblPagFinal;
        private System.Windows.Forms.ContextMenuStrip cmsOpciones;
        private System.Windows.Forms.ToolStripMenuItem itemEditar;
        private System.Windows.Forms.ToolStripMenuItem itemEliminar;
    }
}