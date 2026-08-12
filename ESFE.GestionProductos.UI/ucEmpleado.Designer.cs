namespace ESFE.GestionProductos.UI
{
    partial class ucEmpleado
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            materialCard2 = new MaterialSkin.Controls.MaterialCard();
            btnEliminar = new MaterialSkin.Controls.MaterialButton();
            btnEditar = new MaterialSkin.Controls.MaterialButton();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            materialListView1 = new MaterialSkin.Controls.MaterialListView();
            id = new ColumnHeader();
            nombre = new ColumnHeader();
            telefono = new ColumnHeader();
            cargo = new ColumnHeader();
            usuario = new ColumnHeader();
            estado = new ColumnHeader();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            panel1 = new Panel();
            btnBuscar = new MaterialSkin.Controls.MaterialButton();
            btnCrear = new MaterialSkin.Controls.MaterialButton();
            SlctFiltro = new MaterialSkin.Controls.MaterialComboBox();
            materialMaskedTextBox1 = new MaterialSkin.Controls.MaterialMaskedTextBox();
            lblBuscador = new MaterialSkin.Controls.MaterialLabel();
            lblEmpleadoTitulo = new MaterialSkin.Controls.MaterialLabel();
            cmsOpciones = new ContextMenuStrip(components);
            itemEditar = new ToolStripMenuItem();
            itemEliminar = new ToolStripMenuItem();
            materialTabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            materialCard2.SuspendLayout();
            materialCard3.SuspendLayout();
            materialCard1.SuspendLayout();
            panel1.SuspendLayout();
            cmsOpciones.SuspendLayout();
            SuspendLayout();
            // 
            // materialTabControl1
            // 
            materialTabControl1.Controls.Add(tabPage1);
            materialTabControl1.Controls.Add(tabPage2);
            materialTabControl1.Depth = 0;
            materialTabControl1.Dock = DockStyle.Fill;
            materialTabControl1.Location = new Point(0, 0);
            materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 1;
            materialTabControl1.Size = new Size(1000, 920);
            materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(992, 882);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(materialCard2);
            tabPage2.Controls.Add(materialCard1);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(992, 882);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // materialCard1
            // 
            materialCard1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(panel1);
            materialCard1.Controls.Add(lblEmpleadoTitulo);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(20, 15);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(952, 350);
            materialCard1.TabIndex = 0;
            // 
            // lblEmpleadoTitulo
            // 
            lblEmpleadoTitulo.AutoSize = true;
            lblEmpleadoTitulo.Depth = 0;
            lblEmpleadoTitulo.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblEmpleadoTitulo.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            lblEmpleadoTitulo.Location = new Point(20, 20);
            lblEmpleadoTitulo.MouseState = MaterialSkin.MouseState.HOVER;
            lblEmpleadoTitulo.Name = "lblEmpleadoTitulo";
            lblEmpleadoTitulo.Size = new Size(152, 41);
            lblEmpleadoTitulo.TabIndex = 0;
            lblEmpleadoTitulo.Text = "Empleado";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(btnBuscar);
            panel1.Controls.Add(btnCrear);
            panel1.Controls.Add(SlctFiltro);
            panel1.Controls.Add(materialMaskedTextBox1);
            panel1.Controls.Add(lblBuscador);
            panel1.Location = new Point(20, 75);
            panel1.Name = "panel1";
            panel1.Size = new Size(912, 255);
            panel1.TabIndex = 6;
            // 
            // lblBuscador (REUBICADO PARA DAR PASO A LA ALINEACIÓN)
            // 
            lblBuscador.AutoSize = true;
            lblBuscador.Depth = 0;
            lblBuscador.Font = new Font("Roboto Medium", 22F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblBuscador.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            lblBuscador.Location = new Point(0, 30); // Bajado a Y=30
            lblBuscador.MouseState = MaterialSkin.MouseState.HOVER;
            lblBuscador.Name = "lblBuscador";
            lblBuscador.Size = new Size(160, 24);
            lblBuscador.TabIndex = 1;
            lblBuscador.Text = "Buscar Empleado:";
            // 
            // materialMaskedTextBox1 (ALINEADO EXACTO A Y=95 CON EL BOTON BUSCAR)
            // 
            materialMaskedTextBox1.AllowPromptAsInput = true;
            materialMaskedTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialMaskedTextBox1.AnimateReadOnly = false;
            materialMaskedTextBox1.AsciiOnly = false;
            materialMaskedTextBox1.BackgroundImageLayout = ImageLayout.None;
            materialMaskedTextBox1.BeepOnError = false;
            materialMaskedTextBox1.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            materialMaskedTextBox1.Depth = 0;
            materialMaskedTextBox1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialMaskedTextBox1.HidePromptOnLeave = false;
            materialMaskedTextBox1.HideSelection = true;
            materialMaskedTextBox1.Hint = "Nombre o Teléfono";
            materialMaskedTextBox1.InsertKeyMode = InsertKeyMode.Default;
            materialMaskedTextBox1.LeadingIcon = null;
            materialMaskedTextBox1.Location = new Point(0, 95); // Bajado exactamente a Y=95
            materialMaskedTextBox1.Mask = "";
            materialMaskedTextBox1.MaxLength = 32767;
            materialMaskedTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            materialMaskedTextBox1.Name = "materialMaskedTextBox1";
            materialMaskedTextBox1.PasswordChar = '\0';
            materialMaskedTextBox1.PrefixSuffixText = null;
            materialMaskedTextBox1.PromptChar = '_';
            materialMaskedTextBox1.ReadOnly = false;
            materialMaskedTextBox1.RejectInputOnFirstFailure = false;
            materialMaskedTextBox1.ResetOnPrompt = true;
            materialMaskedTextBox1.ResetOnSpace = true;
            materialMaskedTextBox1.RightToLeft = RightToLeft.No;
            materialMaskedTextBox1.SelectedText = "";
            materialMaskedTextBox1.SelectionLength = 0;
            materialMaskedTextBox1.SelectionStart = 0;
            materialMaskedTextBox1.ShortcutsEnabled = true;
            materialMaskedTextBox1.Size = new Size(460, 48);
            materialMaskedTextBox1.SkipLiterals = true;
            materialMaskedTextBox1.TabIndex = 2;
            materialMaskedTextBox1.TabStop = false;
            materialMaskedTextBox1.TextAlign = HorizontalAlignment.Left;
            materialMaskedTextBox1.TextMaskFormat = MaskFormat.IncludeLiterals;
            materialMaskedTextBox1.TrailingIcon = null;
            materialMaskedTextBox1.UseSystemPasswordChar = false;
            materialMaskedTextBox1.ValidatingType = null;
            // 
            // SlctFiltro (ALINEADO EXACTO A Y=95)
            // 
            SlctFiltro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SlctFiltro.AutoResize = false;
            SlctFiltro.BackColor = Color.FromArgb(255, 255, 255);
            SlctFiltro.Depth = 0;
            SlctFiltro.DrawMode = DrawMode.OwnerDrawVariable;
            SlctFiltro.DropDownHeight = 174;
            SlctFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            SlctFiltro.DropDownWidth = 121;
            SlctFiltro.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            SlctFiltro.ForeColor = Color.FromArgb(222, 0, 0, 0);
            SlctFiltro.FormattingEnabled = true;
            SlctFiltro.Hint = "Filtros";
            SlctFiltro.IntegralHeight = false;
            SlctFiltro.ItemHeight = 43;
            SlctFiltro.Location = new Point(485, 95); // Bajado a Y=95
            SlctFiltro.MaxDropDownItems = 4;
            SlctFiltro.MouseState = MaterialSkin.MouseState.OUT;
            SlctFiltro.Name = "SlctFiltro";
            SlctFiltro.Size = new Size(240, 49);
            SlctFiltro.StartIndex = 0;
            SlctFiltro.TabIndex = 3;
            // 
            // btnCrear
            // 
            btnCrear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCrear.AutoSize = false;
            btnCrear.Cursor = Cursors.Hand;
            btnCrear.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCrear.Depth = 0;
            btnCrear.HighEmphasis = true;
            btnCrear.Icon = null;
            btnCrear.Location = new Point(750, 15);
            btnCrear.Margin = new Padding(4, 6, 4, 6);
            btnCrear.MouseState = MaterialSkin.MouseState.HOVER;
            btnCrear.Name = "btnCrear";
            btnCrear.NoAccentTextColor = Color.Empty;
            btnCrear.Size = new Size(150, 56);
            btnCrear.TabIndex = 4;
            btnCrear.Text = "CREAR +";
            btnCrear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnCrear.UseAccentColor = false;
            btnCrear.UseVisualStyleBackColor = true;
            // 
            // btnBuscar (PUNTO DE REFERENCIA EN Y=95)
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.AutoSize = false;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnBuscar.Depth = 0;
            btnBuscar.HighEmphasis = true;
            btnBuscar.Icon = null;
            btnBuscar.Location = new Point(750, 95); // Posición Y=95
            btnBuscar.Margin = new Padding(4, 6, 4, 6);
            btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            btnBuscar.Name = "btnBuscar";
            btnBuscar.NoAccentTextColor = Color.Empty;
            btnBuscar.Size = new Size(150, 56);
            btnBuscar.TabIndex = 5;
            btnBuscar.Text = "BUSCAR";
            btnBuscar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnBuscar.UseAccentColor = false;
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // materialCard2
            // 
            materialCard2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            materialCard2.BackColor = Color.FromArgb(255, 255, 255);
            materialCard2.Controls.Add(btnEliminar);
            materialCard2.Controls.Add(btnEditar);
            materialCard2.Controls.Add(materialCard3);
            materialCard2.Controls.Add(materialLabel2);
            materialCard2.Depth = 0;
            materialCard2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard2.Location = new Point(20, 415);
            materialCard2.Margin = new Padding(14);
            materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard2.Name = "materialCard2";
            materialCard2.Padding = new Padding(14);
            materialCard2.Size = new Size(952, 450);
            materialCard2.TabIndex = 1;
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 28F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            materialLabel2.Location = new Point(20, 25);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(295, 34);
            materialLabel2.TabIndex = 0;
            materialLabel2.Text = "Empleados Registrados:";
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditar.AutoSize = false;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnEditar.Depth = 0;
            btnEditar.HighEmphasis = true;
            btnEditar.Icon = null;
            btnEditar.Location = new Point(620, 20);
            btnEditar.Margin = new Padding(4, 6, 20, 6);
            btnEditar.MouseState = MaterialSkin.MouseState.HOVER;
            btnEditar.Name = "btnEditar";
            btnEditar.NoAccentTextColor = Color.Empty;
            btnEditar.Size = new Size(130, 56);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "EDITAR";
            btnEditar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnEditar.UseAccentColor = false;
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminar.AutoSize = false;
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnEliminar.Depth = 0;
            btnEliminar.HighEmphasis = false;
            btnEliminar.Icon = null;
            btnEliminar.Location = new Point(770, 20);
            btnEliminar.Margin = new Padding(4, 6, 4, 6);
            btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.NoAccentTextColor = Color.Empty;
            btnEliminar.Size = new Size(140, 56);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnEliminar.UseAccentColor = false;
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // materialCard3 (RESTRUCTURADO PARA OCUPAR TODO EL ESPACIO DE LA TABLA)
            // 
            materialCard3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.Controls.Add(materialListView1);
            materialCard3.Depth = 0;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(20, 95);
            materialCard3.Margin = new Padding(14);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(4); // Disminuido para que la lista use todo el borde
            materialCard3.Size = new Size(912, 335);
            materialCard3.TabIndex = 1;
            // 
            // materialListView1 (DOCK FILL PARA EXPANDIRSE AL 100% DE CARD3)
            // 
            materialListView1.AutoSizeTable = false;
            materialListView1.BackColor = Color.FromArgb(255, 255, 255);
            materialListView1.BorderStyle = BorderStyle.None;
            materialListView1.Columns.AddRange(new ColumnHeader[] { id, nombre, telefono, cargo, usuario, estado });
            materialListView1.Depth = 0;
            materialListView1.Dock = DockStyle.Fill; // Ocupa el 100% del Card
            materialListView1.Font = new Font("Roboto", 20F, FontStyle.Regular, GraphicsUnit.Point);
            materialListView1.FullRowSelect = true;
            materialListView1.Location = new Point(4, 4);
            materialListView1.MinimumSize = new Size(200, 100);
            materialListView1.MouseLocation = new Point(-1, -1);
            materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            materialListView1.MultiSelect = false;
            materialListView1.Name = "materialListView1";
            materialListView1.OwnerDraw = true;
            materialListView1.Size = new Size(904, 327);
            materialListView1.TabIndex = 1;
            materialListView1.UseCompatibleStateImageBehavior = false;
            materialListView1.View = View.Details;
            // 
            // id
            // 
            id.Text = "ID";
            id.Width = 100;
            // 
            // nombre
            // 
            nombre.Text = "Nombre";
            nombre.Width = 320;
            // 
            // telefono
            // 
            telefono.Text = "Teléfono";
            telefono.Width = 190;
            // 
            // cargo
            // 
            cargo.Text = "Cargo";
            cargo.Width = 200;
            // 
            // usuario
            // 
            usuario.Text = "Usuario";
            usuario.Width = 200;
            // 
            // estado
            // 
            estado.Text = "Estado";
            estado.Width = 150;
            // 
            // cmsOpciones
            // 
            cmsOpciones.ImageScalingSize = new Size(24, 24);
            cmsOpciones.Items.AddRange(new ToolStripItem[] { itemEditar, itemEliminar });
            cmsOpciones.Name = "cmsOpciones";
            cmsOpciones.Size = new Size(147, 68);
            // 
            // itemEditar
            // 
            itemEditar.Name = "itemEditar";
            itemEditar.Size = new Size(146, 30);
            itemEditar.Text = "✏️ Editar";
            // 
            // itemEliminar
            // 
            itemEliminar.Name = "itemEliminar";
            itemEliminar.Size = new Size(146, 30);
            itemEliminar.Text = "🗑️ Eliminar";
            // 
            // ucEmpleado
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(materialTabControl1);
            Name = "ucEmpleado";
            Size = new Size(1000, 920);
            materialTabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            materialCard2.ResumeLayout(false);
            materialCard2.PerformLayout();
            materialCard3.ResumeLayout(false);
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            cmsOpciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialLabel lblEmpleadoTitulo;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private MaterialSkin.Controls.MaterialListView materialListView1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private ColumnHeader id;
        private ColumnHeader nombre;
        private ColumnHeader telefono;
        private ColumnHeader cargo;
        private ColumnHeader usuario;
        private ColumnHeader estado;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private MaterialSkin.Controls.MaterialLabel lblBuscador;
        private MaterialSkin.Controls.MaterialComboBox SlctFiltro;
        private MaterialSkin.Controls.MaterialButton btnCrear;
        private Panel panel1;
        private MaterialSkin.Controls.MaterialButton btnBuscar;
        private MaterialSkin.Controls.MaterialMaskedTextBox materialMaskedTextBox1;
        private MaterialSkin.Controls.MaterialButton btnEliminar;
        private MaterialSkin.Controls.MaterialButton btnEditar;
        private ContextMenuStrip cmsOpciones;
        private ToolStripMenuItem itemEditar;
        private ToolStripMenuItem itemEliminar;
    }
}