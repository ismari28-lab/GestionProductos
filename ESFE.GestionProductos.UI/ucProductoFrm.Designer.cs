namespace ESFE.GestionProductos.UI
{
    partial class ucProductoFrm
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

        #region Código generado por el Diseñador de Componentes

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnCerrar = new Button();
            pnlContenedorCentral = new Panel();
            lblSubtitulo = new Label();
            cardFormulario = new MaterialSkin.Controls.MaterialCard();
            btnGuardar = new MaterialSkin.Controls.MaterialButton();
            btnCancelar = new MaterialSkin.Controls.MaterialButton();
            chkEstado = new MaterialSkin.Controls.MaterialCheckbox();
            chkAplicaIVA = new MaterialSkin.Controls.MaterialCheckbox();
            cmbProveedor = new MaterialSkin.Controls.MaterialComboBox();
            cmbCategoria = new MaterialSkin.Controls.MaterialComboBox();
            txtCodigo = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtNombre = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtDescripcion = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtPrecioCompra = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtPrecioVenta = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtPorcentajeIVA = new MaterialSkin.Controls.MaterialMaskedTextBox();
            lblSeccionDatos = new MaterialSkin.Controls.MaterialLabel();
            pnlHeader.SuspendLayout();
            pnlContenedorCentral.SuspendLayout();
            cardFormulario.SuspendLayout();
            SuspendLayout();

            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(63, 81, 181);
            pnlHeader.Controls.Add(btnCerrar);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(2);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(900, 36);
            pnlHeader.TabIndex = 0;

            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseDownBackColor = Color.FromArgb(198, 40, 40);
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 115, 115);
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(864, 6);
            btnCerrar.Margin = new Padding(0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(28, 24);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "X";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;

            // 
            // pnlContenedorCentral
            // 
            pnlContenedorCentral.AutoScroll = true;
            pnlContenedorCentral.BackColor = Color.FromArgb(242, 244, 247);
            pnlContenedorCentral.Controls.Add(lblSubtitulo);
            pnlContenedorCentral.Controls.Add(cardFormulario);
            pnlContenedorCentral.Dock = DockStyle.Fill;
            pnlContenedorCentral.Location = new Point(0, 36);
            pnlContenedorCentral.Margin = new Padding(2);
            pnlContenedorCentral.Name = "pnlContenedorCentral";
            pnlContenedorCentral.Padding = new Padding(0, 0, 0, 18);
            pnlContenedorCentral.Size = new Size(900, 604);
            pnlContenedorCentral.TabIndex = 1;
            pnlContenedorCentral.Resize += pnlContenedorCentral_Resize;

            // 
            // lblSubtitulo
            // 
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.ForeColor = Color.FromArgb(100, 110, 120);
            lblSubtitulo.Location = new Point(28, 15);
            lblSubtitulo.Margin = new Padding(2, 0, 2, 0);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(852, 18);
            lblSubtitulo.TabIndex = 0;
            lblSubtitulo.Text = "Ingrese los detalles para registrar un nuevo producto en el catálogo";

            // 
            // cardFormulario
            // 
            cardFormulario.BackColor = Color.FromArgb(255, 255, 255);
            cardFormulario.Controls.Add(btnGuardar);
            cardFormulario.Controls.Add(btnCancelar);
            cardFormulario.Controls.Add(chkEstado);
            cardFormulario.Controls.Add(chkAplicaIVA);
            cardFormulario.Controls.Add(cmbProveedor);
            cardFormulario.Controls.Add(cmbCategoria);
            cardFormulario.Controls.Add(txtCodigo);
            cardFormulario.Controls.Add(txtNombre);
            cardFormulario.Controls.Add(txtDescripcion);
            cardFormulario.Controls.Add(txtPrecioCompra);
            cardFormulario.Controls.Add(txtPrecioVenta);
            cardFormulario.Controls.Add(txtPorcentajeIVA);
            cardFormulario.Controls.Add(lblSeccionDatos);
            cardFormulario.Depth = 0;
            cardFormulario.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cardFormulario.Location = new Point(24, 39);
            cardFormulario.Margin = new Padding(10, 8, 10, 8);
            cardFormulario.MouseState = MaterialSkin.MouseState.HOVER;
            cardFormulario.Name = "cardFormulario";
            cardFormulario.Padding = new Padding(21, 18, 21, 18);
            cardFormulario.Size = new Size(852, 520);
            cardFormulario.TabIndex = 0;

            // 
            // lblSeccionDatos
            // 
            lblSeccionDatos.AutoSize = true;
            lblSeccionDatos.Depth = 0;
            lblSeccionDatos.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSeccionDatos.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            lblSeccionDatos.HighEmphasis = true;
            lblSeccionDatos.Location = new Point(21, 12);
            lblSeccionDatos.Margin = new Padding(2, 0, 2, 0);
            lblSeccionDatos.MouseState = MaterialSkin.MouseState.HOVER;
            lblSeccionDatos.Name = "lblSeccionDatos";
            lblSeccionDatos.Size = new Size(180, 19);
            lblSeccionDatos.TabIndex = 0;
            lblSeccionDatos.Text = "Información del Producto";

            // 
            // txtCodigo   (Fila 1 izq)
            // 
            ConfigurarTextBox(txtCodigo, "Código", new Point(21, 50), new Size(250, 50), 1);

            // 
            // txtNombre   (Fila 1 der)
            // 
            ConfigurarTextBox(txtNombre, "Nombre del Producto", new Point(291, 50), new Size(540, 50), 2);

            // 
            // txtDescripcion   (Fila 2, ancho completo)
            // 
            ConfigurarTextBox(txtDescripcion, "Descripción", new Point(21, 120), new Size(810, 50), 3);

            // 
            // txtPrecioCompra   (Fila 3 izq)
            // 
            ConfigurarTextBox(txtPrecioCompra, "Precio Compra ($)", new Point(21, 190), new Size(395, 50), 4);

            // 
            // txtPrecioVenta   (Fila 3 der)
            // 
            ConfigurarTextBox(txtPrecioVenta, "Precio Venta ($)", new Point(436, 190), new Size(395, 50), 5);

            // 
            // txtPorcentajeIVA   (Fila 4 izq)
            // 
            ConfigurarTextBox(txtPorcentajeIVA, "Porcentaje IVA (%)", new Point(21, 260), new Size(250, 50), 6);

            // 
            // chkAplicaIVA   (Fila 4 der)
            // 
            chkAplicaIVA.AutoSize = true;
            chkAplicaIVA.Checked = false;
            chkAplicaIVA.CheckState = CheckState.Unchecked;
            chkAplicaIVA.Depth = 0;
            chkAplicaIVA.Font = new Font("Microsoft Sans Serif", 16F);
            chkAplicaIVA.Location = new Point(291, 268);
            chkAplicaIVA.Margin = new Padding(0);
            chkAplicaIVA.MouseLocation = new Point(-1, -1);
            chkAplicaIVA.MouseState = MaterialSkin.MouseState.HOVER;
            chkAplicaIVA.Name = "chkAplicaIVA";
            chkAplicaIVA.ReadOnly = false;
            chkAplicaIVA.Ripple = true;
            chkAplicaIVA.Size = new Size(120, 37);
            chkAplicaIVA.TabIndex = 7;
            chkAplicaIVA.Text = "Aplica IVA";
            chkAplicaIVA.UseVisualStyleBackColor = true;

            // 
            // cmbProveedor   (Fila 5 izq)
            // 
            ConfigurarCombo(cmbProveedor, "Proveedor", new Point(21, 330), new Size(395, 50), 8);

            // 
            // cmbCategoria   (Fila 5 der)
            // 
            ConfigurarCombo(cmbCategoria, "Categoría", new Point(436, 330), new Size(395, 50), 9);

            // 
            // chkEstado   (Fila 6 izq)
            // 
            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.CheckState = CheckState.Checked;
            chkEstado.Depth = 0;
            chkEstado.Font = new Font("Microsoft Sans Serif", 16F);
            chkEstado.Location = new Point(21, 425);
            chkEstado.Margin = new Padding(0);
            chkEstado.MouseLocation = new Point(-1, -1);
            chkEstado.MouseState = MaterialSkin.MouseState.HOVER;
            chkEstado.Name = "chkEstado";
            chkEstado.ReadOnly = false;
            chkEstado.Ripple = true;
            chkEstado.Size = new Size(79, 37);
            chkEstado.TabIndex = 10;
            chkEstado.Text = "Activo";
            chkEstado.UseVisualStyleBackColor = true;

            // 
            // btnCancelar
            // 
            btnCancelar.AutoSize = false;
            btnCancelar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCancelar.Depth = 0;
            btnCancelar.HighEmphasis = false;
            btnCancelar.Icon = null;
            btnCancelar.Location = new Point(597, 470);
            btnCancelar.Margin = new Padding(3, 4, 10, 4);
            btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.NoAccentTextColor = Color.Empty;
            btnCancelar.Size = new Size(105, 29);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnCancelar.UseAccentColor = false;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;

            // 
            // btnGuardar
            // 
            btnGuardar.AutoSize = false;
            btnGuardar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnGuardar.Depth = 0;
            btnGuardar.HighEmphasis = true;
            btnGuardar.Icon = null;
            btnGuardar.Location = new Point(712, 470);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.NoAccentTextColor = Color.Empty;
            btnGuardar.Size = new Size(119, 29);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnGuardar.UseAccentColor = false;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;

            // 
            // ucProductoFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 640);
            Controls.Add(pnlContenedorCentral);
            Controls.Add(pnlHeader);
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ucProductoFrm";
            Padding = new Padding(0);
            Sizable = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ucProductoFrm";
            Load += ucProductoFrm_Load;
            pnlHeader.ResumeLayout(false);
            pnlContenedorCentral.ResumeLayout(false);
            cardFormulario.ResumeLayout(false);
            cardFormulario.PerformLayout();
            ResumeLayout(false);
        }

        // Helpers para no repetir 20 líneas de setup por cada MaterialMaskedTextBox
        private void ConfigurarTextBox(
            MaterialSkin.Controls.MaterialMaskedTextBox tb,
            string hint, Point ubicacion, Size tamano, int tabIndex)
        {
            tb.Depth = 0;
            tb.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            tb.Hint = hint;
            tb.Location = ubicacion;
            tb.Margin = new Padding(2);
            tb.Mask = "";
            tb.MaxLength = 32767;
            tb.MouseState = MaterialSkin.MouseState.OUT;
            tb.Name = "tb_" + hint;
            tb.PromptChar = '_';
            tb.Size = tamano;
            tb.TabIndex = tabIndex;
            tb.TabStop = true;
            tb.TextAlign = HorizontalAlignment.Left;
            tb.TextMaskFormat = MaskFormat.IncludeLiterals;
        }

        private void ConfigurarCombo(
            MaterialSkin.Controls.MaterialComboBox cmb,
            string hint, Point ubicacion, Size tamano, int tabIndex)
        {
            cmb.AutoResize = false;
            cmb.BackColor = Color.FromArgb(255, 255, 255);
            cmb.Depth = 0;
            cmb.DrawMode = DrawMode.OwnerDrawVariable;
            cmb.DropDownHeight = 174;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.DropDownWidth = 121;
            cmb.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmb.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmb.FormattingEnabled = true;
            cmb.Hint = hint;
            cmb.IntegralHeight = false;
            cmb.ItemHeight = 43;
            cmb.Location = ubicacion;
            cmb.Margin = new Padding(2);
            cmb.MaxDropDownItems = 4;
            cmb.MouseState = MaterialSkin.MouseState.OUT;
            cmb.Name = "cmb_" + hint;
            cmb.Size = tamano;
            cmb.StartIndex = 0;
            cmb.TabIndex = tabIndex;
        }

        #endregion

        private Panel pnlHeader;
        private Button btnCerrar;
        private Label lblSubtitulo;
        private Panel pnlContenedorCentral;
        private MaterialSkin.Controls.MaterialCard cardFormulario;
        private MaterialSkin.Controls.MaterialLabel lblSeccionDatos;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtCodigo;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtNombre;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtDescripcion;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtPrecioCompra;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtPrecioVenta;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtPorcentajeIVA;
        private MaterialSkin.Controls.MaterialComboBox cmbProveedor;
        private MaterialSkin.Controls.MaterialComboBox cmbCategoria;
        private MaterialSkin.Controls.MaterialCheckbox chkAplicaIVA;
        private MaterialSkin.Controls.MaterialCheckbox chkEstado;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
    }
}