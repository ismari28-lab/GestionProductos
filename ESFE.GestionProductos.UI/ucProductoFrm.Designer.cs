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
            txtStockMinimo = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtStock = new MaterialSkin.Controls.MaterialMaskedTextBox();
            cmbCategoria = new MaterialSkin.Controls.MaterialComboBox();
            txtPrecio = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtNombre = new MaterialSkin.Controls.MaterialMaskedTextBox();
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
            pnlHeader.Margin = new Padding(2, 2, 2, 2);
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
            pnlContenedorCentral.Margin = new Padding(2, 2, 2, 2);
            pnlContenedorCentral.Name = "pnlContenedorCentral";
            pnlContenedorCentral.Padding = new Padding(0, 0, 0, 18);
            pnlContenedorCentral.Size = new Size(900, 444);
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
            cardFormulario.Controls.Add(txtStockMinimo);
            cardFormulario.Controls.Add(txtStock);
            cardFormulario.Controls.Add(cmbCategoria);
            cardFormulario.Controls.Add(txtPrecio);
            cardFormulario.Controls.Add(txtNombre);
            cardFormulario.Controls.Add(lblSeccionDatos);
            cardFormulario.Depth = 0;
            cardFormulario.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cardFormulario.Location = new Point(24, 39);
            cardFormulario.Margin = new Padding(10, 8, 10, 8);
            cardFormulario.MouseState = MaterialSkin.MouseState.HOVER;
            cardFormulario.Name = "cardFormulario";
            cardFormulario.Padding = new Padding(21, 18, 21, 18);
            cardFormulario.Size = new Size(852, 360);
            cardFormulario.TabIndex = 0;
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
            btnGuardar.Location = new Point(712, 310);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.NoAccentTextColor = Color.Empty;
            btnGuardar.Size = new Size(119, 29);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnGuardar.UseAccentColor = false;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
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
            btnCancelar.Location = new Point(597, 310);
            btnCancelar.Margin = new Padding(3, 4, 10, 4);
            btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.NoAccentTextColor = Color.Empty;
            btnCancelar.Size = new Size(105, 29);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnCancelar.UseAccentColor = false;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // chkEstado
            // 
            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.CheckState = CheckState.Checked;
            chkEstado.Depth = 0;
            chkEstado.Font = new Font("Microsoft Sans Serif", 16F);
            chkEstado.Location = new Point(21, 260);
            chkEstado.Margin = new Padding(0);
            chkEstado.MouseLocation = new Point(-1, -1);
            chkEstado.MouseState = MaterialSkin.MouseState.HOVER;
            chkEstado.Name = "chkEstado";
            chkEstado.ReadOnly = false;
            chkEstado.Ripple = true;
            chkEstado.Size = new Size(79, 37);
            chkEstado.TabIndex = 6;
            chkEstado.Text = "Activo";
            chkEstado.UseVisualStyleBackColor = true;
            // 
            // txtStockMinimo
            // 
            txtStockMinimo.AllowPromptAsInput = true;
            txtStockMinimo.AnimateReadOnly = false;
            txtStockMinimo.AsciiOnly = false;
            txtStockMinimo.BackgroundImageLayout = ImageLayout.None;
            txtStockMinimo.BeepOnError = false;
            txtStockMinimo.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtStockMinimo.Depth = 0;
            txtStockMinimo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtStockMinimo.HidePromptOnLeave = false;
            txtStockMinimo.HideSelection = true;
            txtStockMinimo.Hint = "Límite Alerta (Stock Bajo)";
            txtStockMinimo.InsertKeyMode = InsertKeyMode.Default;
            txtStockMinimo.LeadingIcon = null;
            txtStockMinimo.Location = new Point(436, 190);
            txtStockMinimo.Margin = new Padding(2, 2, 2, 2);
            txtStockMinimo.Mask = "";
            txtStockMinimo.MaxLength = 32767;
            txtStockMinimo.MouseState = MaterialSkin.MouseState.OUT;
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.PasswordChar = '\0';
            txtStockMinimo.PrefixSuffixText = null;
            txtStockMinimo.PromptChar = '_';
            txtStockMinimo.ReadOnly = false;
            txtStockMinimo.RejectInputOnFirstFailure = false;
            txtStockMinimo.ResetOnPrompt = true;
            txtStockMinimo.ResetOnSpace = true;
            txtStockMinimo.RightToLeft = RightToLeft.No;
            txtStockMinimo.SelectedText = "";
            txtStockMinimo.SelectionLength = 0;
            txtStockMinimo.SelectionStart = 0;
            txtStockMinimo.ShortcutsEnabled = true;
            txtStockMinimo.Size = new Size(395, 50);
            txtStockMinimo.SkipLiterals = true;
            txtStockMinimo.TabIndex = 5;
            txtStockMinimo.TabStop = false;
            txtStockMinimo.TextAlign = HorizontalAlignment.Left;
            txtStockMinimo.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtStockMinimo.TrailingIcon = null;
            txtStockMinimo.UseSystemPasswordChar = false;
            txtStockMinimo.ValidatingType = null;
            // 
            // txtStock
            // 
            txtStock.AllowPromptAsInput = true;
            txtStock.AnimateReadOnly = false;
            txtStock.AsciiOnly = false;
            txtStock.BackgroundImageLayout = ImageLayout.None;
            txtStock.BeepOnError = false;
            txtStock.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtStock.Depth = 0;
            txtStock.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtStock.HidePromptOnLeave = false;
            txtStock.HideSelection = true;
            txtStock.Hint = "Stock Actual";
            txtStock.InsertKeyMode = InsertKeyMode.Default;
            txtStock.LeadingIcon = null;
            txtStock.Location = new Point(21, 190);
            txtStock.Margin = new Padding(2, 2, 2, 2);
            txtStock.Mask = "";
            txtStock.MaxLength = 32767;
            txtStock.MouseState = MaterialSkin.MouseState.OUT;
            txtStock.Name = "txtStock";
            txtStock.PasswordChar = '\0';
            txtStock.PrefixSuffixText = null;
            txtStock.PromptChar = '_';
            txtStock.ReadOnly = false;
            txtStock.RejectInputOnFirstFailure = false;
            txtStock.ResetOnPrompt = true;
            txtStock.ResetOnSpace = true;
            txtStock.RightToLeft = RightToLeft.No;
            txtStock.SelectedText = "";
            txtStock.SelectionLength = 0;
            txtStock.SelectionStart = 0;
            txtStock.ShortcutsEnabled = true;
            txtStock.Size = new Size(395, 50);
            txtStock.SkipLiterals = true;
            txtStock.TabIndex = 4;
            txtStock.TabStop = false;
            txtStock.TextAlign = HorizontalAlignment.Left;
            txtStock.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtStock.TrailingIcon = null;
            txtStock.UseSystemPasswordChar = false;
            txtStock.ValidatingType = null;
            // 
            // cmbCategoria
            // 
            cmbCategoria.AutoResize = false;
            cmbCategoria.BackColor = Color.FromArgb(255, 255, 255);
            cmbCategoria.Depth = 0;
            cmbCategoria.DrawMode = DrawMode.OwnerDrawVariable;
            cmbCategoria.DropDownHeight = 174;
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.DropDownWidth = 121;
            cmbCategoria.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbCategoria.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Hint = "Categoría";
            cmbCategoria.IntegralHeight = false;
            cmbCategoria.ItemHeight = 43;
            cmbCategoria.Location = new Point(436, 120);
            cmbCategoria.Margin = new Padding(2, 2, 2, 2);
            cmbCategoria.MaxDropDownItems = 4;
            cmbCategoria.MouseState = MaterialSkin.MouseState.OUT;
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(395, 50);
            cmbCategoria.StartIndex = 0;
            cmbCategoria.TabIndex = 3;
            // 
            // txtPrecio
            // 
            txtPrecio.AllowPromptAsInput = true;
            txtPrecio.AnimateReadOnly = false;
            txtPrecio.AsciiOnly = false;
            txtPrecio.BackgroundImageLayout = ImageLayout.None;
            txtPrecio.BeepOnError = false;
            txtPrecio.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtPrecio.Depth = 0;
            txtPrecio.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtPrecio.HidePromptOnLeave = false;
            txtPrecio.HideSelection = true;
            txtPrecio.Hint = "Precio ($)";
            txtPrecio.InsertKeyMode = InsertKeyMode.Default;
            txtPrecio.LeadingIcon = null;
            txtPrecio.Location = new Point(21, 120);
            txtPrecio.Margin = new Padding(2, 2, 2, 2);
            txtPrecio.Mask = "";
            txtPrecio.MaxLength = 32767;
            txtPrecio.MouseState = MaterialSkin.MouseState.OUT;
            txtPrecio.Name = "txtPrecio";
            txtPrecio.PasswordChar = '\0';
            txtPrecio.PrefixSuffixText = null;
            txtPrecio.PromptChar = '_';
            txtPrecio.ReadOnly = false;
            txtPrecio.RejectInputOnFirstFailure = false;
            txtPrecio.ResetOnPrompt = true;
            txtPrecio.ResetOnSpace = true;
            txtPrecio.RightToLeft = RightToLeft.No;
            txtPrecio.SelectedText = "";
            txtPrecio.SelectionLength = 0;
            txtPrecio.SelectionStart = 0;
            txtPrecio.ShortcutsEnabled = true;
            txtPrecio.Size = new Size(395, 50);
            txtPrecio.SkipLiterals = true;
            txtPrecio.TabIndex = 2;
            txtPrecio.TabStop = false;
            txtPrecio.TextAlign = HorizontalAlignment.Left;
            txtPrecio.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtPrecio.TrailingIcon = null;
            txtPrecio.UseSystemPasswordChar = false;
            txtPrecio.ValidatingType = null;
            // 
            // txtNombre
            // 
            txtNombre.AllowPromptAsInput = true;
            txtNombre.AnimateReadOnly = false;
            txtNombre.AsciiOnly = false;
            txtNombre.BackgroundImageLayout = ImageLayout.None;
            txtNombre.BeepOnError = false;
            txtNombre.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtNombre.Depth = 0;
            txtNombre.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtNombre.HidePromptOnLeave = false;
            txtNombre.HideSelection = true;
            txtNombre.Hint = "Nombre del Producto";
            txtNombre.InsertKeyMode = InsertKeyMode.Default;
            txtNombre.LeadingIcon = null;
            txtNombre.Location = new Point(21, 50);
            txtNombre.Margin = new Padding(2, 2, 2, 2);
            txtNombre.Mask = "";
            txtNombre.MaxLength = 32767;
            txtNombre.MouseState = MaterialSkin.MouseState.OUT;
            txtNombre.Name = "txtNombre";
            txtNombre.PasswordChar = '\0';
            txtNombre.PrefixSuffixText = null;
            txtNombre.PromptChar = '_';
            txtNombre.ReadOnly = false;
            txtNombre.RejectInputOnFirstFailure = false;
            txtNombre.ResetOnPrompt = true;
            txtNombre.ResetOnSpace = true;
            txtNombre.RightToLeft = RightToLeft.No;
            txtNombre.SelectedText = "";
            txtNombre.SelectionLength = 0;
            txtNombre.SelectionStart = 0;
            txtNombre.ShortcutsEnabled = true;
            txtNombre.Size = new Size(810, 50);
            txtNombre.SkipLiterals = true;
            txtNombre.TabIndex = 1;
            txtNombre.TabStop = false;
            txtNombre.TextAlign = HorizontalAlignment.Left;
            txtNombre.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtNombre.TrailingIcon = null;
            txtNombre.UseSystemPasswordChar = false;
            txtNombre.ValidatingType = null;
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
            // ucProductoFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 480);
            Controls.Add(pnlContenedorCentral);
            Controls.Add(pnlHeader);
            Margin = new Padding(2, 2, 2, 2);
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

        #endregion

        private Panel pnlHeader;
        private Button btnCerrar;
        private Label lblSubtitulo;
        private Panel pnlContenedorCentral;
        private MaterialSkin.Controls.MaterialCard cardFormulario;
        private MaterialSkin.Controls.MaterialLabel lblSeccionDatos;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtNombre;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtPrecio;
        private MaterialSkin.Controls.MaterialComboBox cmbCategoria;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtStock;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtStockMinimo;
        private MaterialSkin.Controls.MaterialCheckbox chkEstado;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
    }
}