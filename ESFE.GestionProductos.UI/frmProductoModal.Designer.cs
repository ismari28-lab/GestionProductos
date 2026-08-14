namespace ESFE.GestionProductos.UI
{
    partial class frmProductoModal
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

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            lblTituloSeccion = new MaterialSkin.Controls.MaterialLabel();
            txtNombre = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtPrecio = new MaterialSkin.Controls.MaterialMaskedTextBox();
            cmbCategoria = new MaterialSkin.Controls.MaterialComboBox();
            txtStock = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtStockMinimo = new MaterialSkin.Controls.MaterialMaskedTextBox();
            chkEstado = new MaterialSkin.Controls.MaterialCheckbox();
            btnCancelar = new MaterialSkin.Controls.MaterialButton();
            btnGuardar = new MaterialSkin.Controls.MaterialButton();
            materialCard1.SuspendLayout();
            SuspendLayout();
            // 
            // materialCard1
            // 
            materialCard1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(btnGuardar);
            materialCard1.Controls.Add(btnCancelar);
            materialCard1.Controls.Add(chkEstado);
            materialCard1.Controls.Add(txtStockMinimo);
            materialCard1.Controls.Add(txtStock);
            materialCard1.Controls.Add(cmbCategoria);
            materialCard1.Controls.Add(txtPrecio);
            materialCard1.Controls.Add(txtNombre);
            materialCard1.Controls.Add(lblTituloSeccion);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(20, 85);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(24);
            materialCard1.Size = new Size(540, 480);
            materialCard1.TabIndex = 0;
            // 
            // lblTituloSeccion
            // 
            lblTituloSeccion.AutoSize = true;
            lblTituloSeccion.Depth = 0;
            lblTituloSeccion.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTituloSeccion.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            lblTituloSeccion.HighEmphasis = true;
            lblTituloSeccion.Location = new Point(24, 20);
            lblTituloSeccion.MouseState = MaterialSkin.MouseState.HOVER;
            lblTituloSeccion.Name = "lblTituloSeccion";
            lblTituloSeccion.Size = new Size(187, 19);
            lblTituloSeccion.TabIndex = 0;
            lblTituloSeccion.Text = "Información del Producto";
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
            txtNombre.Location = new Point(24, 55);
            txtNombre.Mask = "";
            txtNombre.MaxLength = 150;
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
            txtNombre.Size = new Size(492, 55);
            txtNombre.SkipLiterals = true;
            txtNombre.TabIndex = 1;
            txtNombre.TabStop = false;
            txtNombre.TextAlign = HorizontalAlignment.Left;
            txtNombre.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtNombre.TrailingIcon = null;
            txtNombre.UseSystemPasswordChar = false;
            txtNombre.ValidatingType = null;
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
            txtPrecio.Location = new Point(24, 125);
            txtPrecio.Mask = "";
            txtPrecio.MaxLength = 20;
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
            txtPrecio.Size = new Size(235, 55);
            txtPrecio.SkipLiterals = true;
            txtPrecio.TabIndex = 2;
            txtPrecio.TabStop = false;
            txtPrecio.TextAlign = HorizontalAlignment.Left;
            txtPrecio.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtPrecio.TrailingIcon = null;
            txtPrecio.UseSystemPasswordChar = false;
            txtPrecio.ValidatingType = null;
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
            cmbCategoria.Location = new Point(281, 131);
            cmbCategoria.MaxDropDownItems = 4;
            cmbCategoria.MouseState = MaterialSkin.MouseState.OUT;
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(235, 49);
            cmbCategoria.StartIndex = 0;
            cmbCategoria.TabIndex = 3;
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
            txtStock.Location = new Point(24, 195);
            txtStock.Mask = "";
            txtStock.MaxLength = 10;
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
            txtStock.Size = new Size(235, 55);
            txtStock.SkipLiterals = true;
            txtStock.TabIndex = 4;
            txtStock.TabStop = false;
            txtStock.TextAlign = HorizontalAlignment.Left;
            txtStock.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtStock.TrailingIcon = null;
            txtStock.UseSystemPasswordChar = false;
            txtStock.ValidatingType = null;
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
            txtStockMinimo.Location = new Point(281, 195);
            txtStockMinimo.Mask = "";
            txtStockMinimo.MaxLength = 10;
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
            txtStockMinimo.Size = new Size(235, 55);
            txtStockMinimo.SkipLiterals = true;
            txtStockMinimo.TabIndex = 5;
            txtStockMinimo.TabStop = false;
            txtStockMinimo.TextAlign = HorizontalAlignment.Left;
            txtStockMinimo.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtStockMinimo.TrailingIcon = null;
            txtStockMinimo.UseSystemPasswordChar = false;
            txtStockMinimo.ValidatingType = null;
            // 
            // chkEstado
            // 
            chkEstado.AutoSize = true;
            chkEstado.Checked = true;
            chkEstado.CheckState = CheckState.Checked;
            chkEstado.Depth = 0;
            chkEstado.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point);
            chkEstado.Location = new Point(24, 265);
            chkEstado.Margin = new Padding(0);
            chkEstado.MouseLocation = new Point(-1, -1);
            chkEstado.MouseState = MaterialSkin.MouseState.HOVER;
            chkEstado.Name = "chkEstado";
            chkEstado.Ripple = true;
            chkEstado.Size = new Size(101, 37);
            chkEstado.TabIndex = 6;
            chkEstado.Text = "Activo";
            chkEstado.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.AutoSize = false;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCancelar.Depth = 0;
            btnCancelar.HighEmphasis = false;
            btnCancelar.Icon = null;
            btnCancelar.Location = new Point(200, 405);
            btnCancelar.Margin = new Padding(4, 6, 15, 6);
            btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.NoAccentTextColor = Color.Empty;
            btnCancelar.Size = new Size(140, 48);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnCancelar.UseAccentColor = false;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardar.AutoSize = false;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnGuardar.Depth = 0;
            btnGuardar.HighEmphasis = true;
            btnGuardar.Icon = null;
            btnGuardar.Location = new Point(356, 405);
            btnGuardar.Margin = new Padding(4, 6, 4, 6);
            btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.NoAccentTextColor = Color.Empty;
            btnGuardar.Size = new Size(160, 48);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnGuardar.UseAccentColor = false;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // frmProductoModal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(580, 580);
            Controls.Add(materialCard1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmProductoModal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Crear Producto";
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialLabel lblTituloSeccion;
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