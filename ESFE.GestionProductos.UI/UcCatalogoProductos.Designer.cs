namespace ESFE.GestionProductos.UI
{
    partial class ucProducto
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
            lblTituloProductos = new Label();
            lblSubtitulo = new Label();
            pnlBusqueda = new Panel();
            txtBuscar = new MaterialSkin.Controls.MaterialTextBox2();
            cboFiltro = new MaterialSkin.Controls.MaterialComboBox();
            btnBuscar = new Button();
            btnCrear = new Button();
            pnlLista = new Panel();
            dgvProductos = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colCodigo = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            colCategoria = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();
            pnlPaginacion = new FlowLayoutPanel();
            lblPagFinal = new Label();
            lblPuntos = new Label();
            lblPag3 = new Label();
            lblPag2 = new Label();
            lblPag1 = new Label();
            lblTituloLista = new Label();
            cmsOpciones = new ContextMenuStrip(components);
            itemEditar = new ToolStripMenuItem();
            itemEliminar = new ToolStripMenuItem();
            pnlBusqueda.SuspendLayout();
            pnlLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            pnlPaginacion.SuspendLayout();
            cmsOpciones.SuspendLayout();
            SuspendLayout();
            // 
            // lblTituloProductos
            // 
            lblTituloProductos.AutoSize = true;
            lblTituloProductos.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTituloProductos.ForeColor = Color.FromArgb(30, 40, 50);
            lblTituloProductos.Location = new Point(28, 20);
            lblTituloProductos.Name = "lblTituloProductos";
            lblTituloProductos.Size = new Size(187, 47);
            lblTituloProductos.TabIndex = 5;
            lblTituloProductos.Text = "Productos";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 12F);
            lblSubtitulo.ForeColor = Color.FromArgb(120, 130, 145);
            lblSubtitulo.Location = new Point(30, 75);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(246, 21);
            lblSubtitulo.TabIndex = 4;
            lblSubtitulo.Text = "Gestión del catálogo de productos";
            // 
            // pnlBusqueda
            // 
            pnlBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlBusqueda.BackColor = Color.White;
            pnlBusqueda.Controls.Add(txtBuscar);
            pnlBusqueda.Controls.Add(cboFiltro);
            pnlBusqueda.Controls.Add(btnBuscar);
            pnlBusqueda.Location = new Point(30, 130);
            pnlBusqueda.Name = "pnlBusqueda";
            pnlBusqueda.Size = new Size(1050, 90);
            pnlBusqueda.TabIndex = 2;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.AnimateReadOnly = false;
            txtBuscar.BackgroundImageLayout = ImageLayout.None;
            txtBuscar.CharacterCasing = CharacterCasing.Normal;
            txtBuscar.Depth = 0;
            txtBuscar.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtBuscar.HideSelection = true;
            txtBuscar.Hint = "Buscar por nombre o código...";
            txtBuscar.LeadingIcon = null;
            txtBuscar.Location = new Point(25, 22);
            txtBuscar.MaxLength = 50;
            txtBuscar.MouseState = MaterialSkin.MouseState.OUT;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PasswordChar = '\0';
            txtBuscar.PrefixSuffixText = null;
            txtBuscar.ReadOnly = false;
            txtBuscar.RightToLeft = RightToLeft.No;
            txtBuscar.SelectedText = "";
            txtBuscar.SelectionLength = 0;
            txtBuscar.SelectionStart = 0;
            txtBuscar.ShortcutsEnabled = true;
            txtBuscar.Size = new Size(570, 48);
            txtBuscar.TabIndex = 0;
            txtBuscar.TabStop = false;
            txtBuscar.TextAlign = HorizontalAlignment.Left;
            txtBuscar.TrailingIcon = null;
            txtBuscar.UseSystemPasswordChar = false;
            // 
            // cboFiltro
            // 
            cboFiltro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboFiltro.AutoResize = false;
            cboFiltro.BackColor = Color.FromArgb(255, 255, 255);
            cboFiltro.Depth = 0;
            cboFiltro.DrawMode = DrawMode.OwnerDrawVariable;
            cboFiltro.DropDownHeight = 174;
            cboFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFiltro.DropDownWidth = 200;
            cboFiltro.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            cboFiltro.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cboFiltro.FormattingEnabled = true;
            cboFiltro.Hint = "Filtro";
            cboFiltro.IntegralHeight = false;
            cboFiltro.ItemHeight = 43;
            cboFiltro.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            cboFiltro.Location = new Point(610, 22);
            cboFiltro.MaxDropDownItems = 4;
            cboFiltro.MouseState = MaterialSkin.MouseState.OUT;
            cboFiltro.Name = "cboFiltro";
            cboFiltro.Size = new Size(200, 49);
            cboFiltro.StartIndex = 0;
            cboFiltro.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.BackColor = Color.FromArgb(52, 120, 246);
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(870, 27);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(150, 42);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // btnCrear
            // 
            btnCrear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCrear.BackColor = Color.FromArgb(90, 70, 180);
            btnCrear.Cursor = Cursors.Hand;
            btnCrear.FlatAppearance.BorderSize = 0;
            btnCrear.FlatStyle = FlatStyle.Flat;
            btnCrear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCrear.ForeColor = Color.White;
            btnCrear.Location = new Point(900, 70);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(165, 42);
            btnCrear.TabIndex = 3;
            btnCrear.Text = "+  Crear Producto";
            btnCrear.UseVisualStyleBackColor = false;
            // 
            // pnlLista
            // 
            pnlLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlLista.BackColor = Color.White;
            pnlLista.Controls.Add(dgvProductos);
            pnlLista.Controls.Add(pnlPaginacion);
            pnlLista.Controls.Add(lblTituloLista);
            pnlLista.Location = new Point(30, 245);
            pnlLista.Name = "pnlLista";
            pnlLista.Padding = new Padding(20, 15, 20, 15);
            pnlLista.Size = new Size(1050, 480);
            pnlLista.TabIndex = 1;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProductos.ColumnHeadersHeight = 40;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colCodigo, colPrecio, colStock, colCategoria, colEstado, colActions });
            dgvProductos.Dock = DockStyle.Fill;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.GridColor = Color.FromArgb(235, 238, 242);
            dgvProductos.Location = new Point(20, 50);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.RowTemplate.Height = 42;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(1010, 370);
            dgvProductos.TabIndex = 0;
            // 
            // colId
            // 
            colId.FillWeight = 40F;
            colId.HeaderText = "ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.FillWeight = 130F;
            colNombre.HeaderText = "Nombre";
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colCodigo
            // 
            colCodigo.FillWeight = 80F;
            colCodigo.HeaderText = "Código";
            colCodigo.Name = "colCodigo";
            colCodigo.ReadOnly = true;
            // 
            // colPrecio
            // 
            colPrecio.FillWeight = 70F;
            colPrecio.HeaderText = "Precio";
            colPrecio.Name = "colPrecio";
            colPrecio.ReadOnly = true;
            // 
            // colStock
            // 
            colStock.FillWeight = 60F;
            colStock.HeaderText = "Stock";
            colStock.Name = "colStock";
            colStock.ReadOnly = true;
            // 
            // colCategoria
            // 
            colCategoria.FillWeight = 90F;
            colCategoria.HeaderText = "Categoría";
            colCategoria.Name = "colCategoria";
            colCategoria.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.FillWeight = 70F;
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colActions
            // 
            colActions.FillWeight = 90F;
            colActions.HeaderText = "Actions";
            colActions.Name = "colActions";
            colActions.ReadOnly = true;
            // 
            // pnlPaginacion
            // 
            pnlPaginacion.Controls.Add(lblPagFinal);
            pnlPaginacion.Controls.Add(lblPuntos);
            pnlPaginacion.Controls.Add(lblPag3);
            pnlPaginacion.Controls.Add(lblPag2);
            pnlPaginacion.Controls.Add(lblPag1);
            pnlPaginacion.Dock = DockStyle.Bottom;
            pnlPaginacion.FlowDirection = FlowDirection.RightToLeft;
            pnlPaginacion.Location = new Point(20, 420);
            pnlPaginacion.Name = "pnlPaginacion";
            pnlPaginacion.Padding = new Padding(0, 8, 5, 0);
            pnlPaginacion.Size = new Size(1010, 45);
            pnlPaginacion.TabIndex = 1;
            // 
            // lblPagFinal
            // 
            lblPagFinal.Location = new Point(902, 8);
            lblPagFinal.Name = "lblPagFinal";
            lblPagFinal.Size = new Size(100, 23);
            lblPagFinal.TabIndex = 0;
            // 
            // lblPuntos
            // 
            lblPuntos.Location = new Point(796, 8);
            lblPuntos.Name = "lblPuntos";
            lblPuntos.Size = new Size(100, 23);
            lblPuntos.TabIndex = 1;
            // 
            // lblPag3
            // 
            lblPag3.Location = new Point(690, 8);
            lblPag3.Name = "lblPag3";
            lblPag3.Size = new Size(100, 23);
            lblPag3.TabIndex = 2;
            // 
            // lblPag2
            // 
            lblPag2.Location = new Point(584, 8);
            lblPag2.Name = "lblPag2";
            lblPag2.Size = new Size(100, 23);
            lblPag2.TabIndex = 3;
            // 
            // lblPag1
            // 
            lblPag1.Location = new Point(478, 8);
            lblPag1.Name = "lblPag1";
            lblPag1.Size = new Size(100, 23);
            lblPag1.TabIndex = 4;
            // 
            // lblTituloLista
            // 
            lblTituloLista.Dock = DockStyle.Top;
            lblTituloLista.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloLista.ForeColor = Color.FromArgb(30, 40, 50);
            lblTituloLista.Location = new Point(20, 15);
            lblTituloLista.Name = "lblTituloLista";
            lblTituloLista.Size = new Size(1010, 35);
            lblTituloLista.TabIndex = 2;
            lblTituloLista.Text = "Productos Registrados";
            lblTituloLista.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmsOpciones
            // 
            cmsOpciones.ImageScalingSize = new Size(24, 24);
            cmsOpciones.Items.AddRange(new ToolStripItem[] { itemEditar, itemEliminar });
            cmsOpciones.Name = "cmsOpciones";
            cmsOpciones.Size = new Size(118, 48);
            // 
            // itemEditar
            // 
            itemEditar.Name = "itemEditar";
            itemEditar.Size = new Size(117, 22);
            itemEditar.Text = "Editar";
            // 
            // itemEliminar
            // 
            itemEliminar.Name = "itemEliminar";
            itemEliminar.Size = new Size(117, 22);
            itemEliminar.Text = "Eliminar";
            // 
            // ucProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(pnlLista);
            Controls.Add(pnlBusqueda);
            Controls.Add(btnCrear);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTituloProductos);
            Name = "ucProducto";
            Size = new Size(1100, 760);
            pnlBusqueda.ResumeLayout(false);
            pnlLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            pnlPaginacion.ResumeLayout(false);
            cmsOpciones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTituloProductos;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlBusqueda;
        private MaterialSkin.Controls.MaterialTextBox2 txtBuscar;
        private MaterialSkin.Controls.MaterialComboBox cboFiltro;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel pnlLista;
        private System.Windows.Forms.Label lblTituloLista;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
        private System.Windows.Forms.ContextMenuStrip cmsOpciones;
        private System.Windows.Forms.ToolStripMenuItem itemEditar;
        private System.Windows.Forms.ToolStripMenuItem itemEliminar;

        private System.Windows.Forms.FlowLayoutPanel pnlPaginacion;
        private System.Windows.Forms.Label lblPag1;
        private System.Windows.Forms.Label lblPag2;
        private System.Windows.Forms.Label lblPag3;
        private System.Windows.Forms.Label lblPuntos;
        private System.Windows.Forms.Label lblPagFinal;
    }
}