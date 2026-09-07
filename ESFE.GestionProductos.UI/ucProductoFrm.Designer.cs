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
            cardFormulario.Paint += cardFormulario_Paint;
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
            // chkEstado
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
            // chkAplicaIVA
            // 
            chkAplicaIVA.AutoSize = true;
            chkAplicaIVA.Depth = 0;
            chkAplicaIVA.Font = new Font("Microsoft Sans Serif", 16F);
            chkAplicaIVA.Location = new Point(291, 268);
            chkAplicaIVA.Margin = new Padding(0);
            chkAplicaIVA.MouseLocation = new Point(-1, -1);
            chkAplicaIVA.MouseState = MaterialSkin.MouseState.HOVER;
            chkAplicaIVA.Name = "chkAplicaIVA";
            chkAplicaIVA.ReadOnly = false;
            chkAplicaIVA.Ripple = true;
            chkAplicaIVA.Size = new Size(107, 37);
            chkAplicaIVA.TabIndex = 7;
            chkAplicaIVA.Text = "Aplica IVA";
            chkAplicaIVA.UseVisualStyleBackColor = true;
            // 
            // cmbProveedor
            // 
            cmbProveedor.AutoResize = false;
            cmbProveedor.BackColor = Color.FromArgb(255, 255, 255);
            cmbProveedor.Depth = 0;
            cmbProveedor.DrawMode = DrawMode.OwnerDrawVariable;
            cmbProveedor.DropDownHeight = 174;
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.DropDownWidth = 121;
            cmbProveedor.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbProveedor.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbProveedor.IntegralHeight = false;
            cmbProveedor.ItemHeight = 43;
            cmbProveedor.Location = new Point(0, 0);
            cmbProveedor.MaxDropDownItems = 4;
            cmbProveedor.MouseState = MaterialSkin.MouseState.OUT;
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(121, 49);
            cmbProveedor.StartIndex = 0;
            cmbProveedor.TabIndex = 13;
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
            cmbCategoria.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbCategoria.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbCategoria.IntegralHeight = false;
            cmbCategoria.ItemHeight = 43;
            cmbCategoria.Location = new Point(0, 0);
            cmbCategoria.MaxDropDownItems = 4;
            cmbCategoria.MouseState = MaterialSkin.MouseState.OUT;
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(121, 49);
            cmbCategoria.StartIndex = 0;
            cmbCategoria.TabIndex = 14;
            // 
            // txtCodigo
            // 
            txtCodigo.AllowPromptAsInput = true;
            txtCodigo.AnimateReadOnly = false;
            txtCodigo.AsciiOnly = false;
            txtCodigo.BackgroundImageLayout = ImageLayout.None;
            txtCodigo.BeepOnError = false;
            txtCodigo.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtCodigo.Depth = 0;
            txtCodigo.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtCodigo.HidePromptOnLeave = false;
            txtCodigo.HideSelection = true;
            txtCodigo.InsertKeyMode = InsertKeyMode.Default;
            txtCodigo.LeadingIcon = null;
            txtCodigo.Location = new Point(0, 0);
            txtCodigo.Mask = "";
            txtCodigo.MaxLength = 32767;
            txtCodigo.MouseState = MaterialSkin.MouseState.OUT;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.PasswordChar = '\0';
            txtCodigo.PrefixSuffixText = null;
            txtCodigo.PromptChar = '_';
            txtCodigo.ReadOnly = false;
            txtCodigo.RejectInputOnFirstFailure = false;
            txtCodigo.ResetOnPrompt = true;
            txtCodigo.ResetOnSpace = true;
            txtCodigo.RightToLeft = RightToLeft.No;
            txtCodigo.SelectedText = "";
            txtCodigo.SelectionLength = 0;
            txtCodigo.SelectionStart = 0;
            txtCodigo.ShortcutsEnabled = true;
            txtCodigo.Size = new Size(250, 48);
            txtCodigo.SkipLiterals = true;
            txtCodigo.TabIndex = 15;
            txtCodigo.TabStop = false;
            txtCodigo.TextAlign = HorizontalAlignment.Left;
            txtCodigo.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtCodigo.TrailingIcon = null;
            txtCodigo.UseSystemPasswordChar = false;
            txtCodigo.ValidatingType = null;
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
            txtNombre.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtNombre.HidePromptOnLeave = false;
            txtNombre.HideSelection = true;
            txtNombre.InsertKeyMode = InsertKeyMode.Default;
            txtNombre.LeadingIcon = null;
            txtNombre.Location = new Point(0, 0);
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
            txtNombre.Size = new Size(250, 48);
            txtNombre.SkipLiterals = true;
            txtNombre.TabIndex = 16;
            txtNombre.TabStop = false;
            txtNombre.TextAlign = HorizontalAlignment.Left;
            txtNombre.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtNombre.TrailingIcon = null;
            txtNombre.UseSystemPasswordChar = false;
            txtNombre.ValidatingType = null;
            // 
            // txtDescripcion
            // 
            txtDescripcion.AllowPromptAsInput = true;
            txtDescripcion.AnimateReadOnly = false;
            txtDescripcion.AsciiOnly = false;
            txtDescripcion.BackgroundImageLayout = ImageLayout.None;
            txtDescripcion.BeepOnError = false;
            txtDescripcion.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtDescripcion.Depth = 0;
            txtDescripcion.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtDescripcion.HidePromptOnLeave = false;
            txtDescripcion.HideSelection = true;
            txtDescripcion.InsertKeyMode = InsertKeyMode.Default;
            txtDescripcion.LeadingIcon = null;
            txtDescripcion.Location = new Point(0, 0);
            txtDescripcion.Mask = "";
            txtDescripcion.MaxLength = 32767;
            txtDescripcion.MouseState = MaterialSkin.MouseState.OUT;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PasswordChar = '\0';
            txtDescripcion.PrefixSuffixText = null;
            txtDescripcion.PromptChar = '_';
            txtDescripcion.ReadOnly = false;
            txtDescripcion.RejectInputOnFirstFailure = false;
            txtDescripcion.ResetOnPrompt = true;
            txtDescripcion.ResetOnSpace = true;
            txtDescripcion.RightToLeft = RightToLeft.No;
            txtDescripcion.SelectedText = "";
            txtDescripcion.SelectionLength = 0;
            txtDescripcion.SelectionStart = 0;
            txtDescripcion.ShortcutsEnabled = true;
            txtDescripcion.Size = new Size(250, 48);
            txtDescripcion.SkipLiterals = true;
            txtDescripcion.TabIndex = 17;
            txtDescripcion.TabStop = false;
            txtDescripcion.TextAlign = HorizontalAlignment.Left;
            txtDescripcion.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtDescripcion.TrailingIcon = null;
            txtDescripcion.UseSystemPasswordChar = false;
            txtDescripcion.ValidatingType = null;
            // 
            // txtPrecioCompra
            // 
            txtPrecioCompra.AllowPromptAsInput = true;
            txtPrecioCompra.AnimateReadOnly = false;
            txtPrecioCompra.AsciiOnly = false;
            txtPrecioCompra.BackgroundImageLayout = ImageLayout.None;
            txtPrecioCompra.BeepOnError = false;
            txtPrecioCompra.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtPrecioCompra.Depth = 0;
            txtPrecioCompra.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtPrecioCompra.HidePromptOnLeave = false;
            txtPrecioCompra.HideSelection = true;
            txtPrecioCompra.InsertKeyMode = InsertKeyMode.Default;
            txtPrecioCompra.LeadingIcon = null;
            txtPrecioCompra.Location = new Point(0, 0);
            txtPrecioCompra.Mask = "";
            txtPrecioCompra.MaxLength = 32767;
            txtPrecioCompra.MouseState = MaterialSkin.MouseState.OUT;
            txtPrecioCompra.Name = "txtPrecioCompra";
            txtPrecioCompra.PasswordChar = '\0';
            txtPrecioCompra.PrefixSuffixText = null;
            txtPrecioCompra.PromptChar = '_';
            txtPrecioCompra.ReadOnly = false;
            txtPrecioCompra.RejectInputOnFirstFailure = false;
            txtPrecioCompra.ResetOnPrompt = true;
            txtPrecioCompra.ResetOnSpace = true;
            txtPrecioCompra.RightToLeft = RightToLeft.No;
            txtPrecioCompra.SelectedText = "";
            txtPrecioCompra.SelectionLength = 0;
            txtPrecioCompra.SelectionStart = 0;
            txtPrecioCompra.ShortcutsEnabled = true;
            txtPrecioCompra.Size = new Size(250, 48);
            txtPrecioCompra.SkipLiterals = true;
            txtPrecioCompra.TabIndex = 18;
            txtPrecioCompra.TabStop = false;
            txtPrecioCompra.TextAlign = HorizontalAlignment.Left;
            txtPrecioCompra.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtPrecioCompra.TrailingIcon = null;
            txtPrecioCompra.UseSystemPasswordChar = false;
            txtPrecioCompra.ValidatingType = null;
            // 
            // txtPrecioVenta
            // 
            txtPrecioVenta.AllowPromptAsInput = true;
            txtPrecioVenta.AnimateReadOnly = false;
            txtPrecioVenta.AsciiOnly = false;
            txtPrecioVenta.BackgroundImageLayout = ImageLayout.None;
            txtPrecioVenta.BeepOnError = false;
            txtPrecioVenta.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtPrecioVenta.Depth = 0;
            txtPrecioVenta.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtPrecioVenta.HidePromptOnLeave = false;
            txtPrecioVenta.HideSelection = true;
            txtPrecioVenta.InsertKeyMode = InsertKeyMode.Default;
            txtPrecioVenta.LeadingIcon = null;
            txtPrecioVenta.Location = new Point(0, 0);
            txtPrecioVenta.Mask = "";
            txtPrecioVenta.MaxLength = 32767;
            txtPrecioVenta.MouseState = MaterialSkin.MouseState.OUT;
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.PasswordChar = '\0';
            txtPrecioVenta.PrefixSuffixText = null;
            txtPrecioVenta.PromptChar = '_';
            txtPrecioVenta.ReadOnly = false;
            txtPrecioVenta.RejectInputOnFirstFailure = false;
            txtPrecioVenta.ResetOnPrompt = true;
            txtPrecioVenta.ResetOnSpace = true;
            txtPrecioVenta.RightToLeft = RightToLeft.No;
            txtPrecioVenta.SelectedText = "";
            txtPrecioVenta.SelectionLength = 0;
            txtPrecioVenta.SelectionStart = 0;
            txtPrecioVenta.ShortcutsEnabled = true;
            txtPrecioVenta.Size = new Size(250, 48);
            txtPrecioVenta.SkipLiterals = true;
            txtPrecioVenta.TabIndex = 19;
            txtPrecioVenta.TabStop = false;
            txtPrecioVenta.TextAlign = HorizontalAlignment.Left;
            txtPrecioVenta.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtPrecioVenta.TrailingIcon = null;
            txtPrecioVenta.UseSystemPasswordChar = false;
            txtPrecioVenta.ValidatingType = null;
            // 
            // txtPorcentajeIVA
            // 
            txtPorcentajeIVA.AllowPromptAsInput = true;
            txtPorcentajeIVA.AnimateReadOnly = false;
            txtPorcentajeIVA.AsciiOnly = false;
            txtPorcentajeIVA.BackgroundImageLayout = ImageLayout.None;
            txtPorcentajeIVA.BeepOnError = false;
            txtPorcentajeIVA.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtPorcentajeIVA.Depth = 0;
            txtPorcentajeIVA.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtPorcentajeIVA.HidePromptOnLeave = false;
            txtPorcentajeIVA.HideSelection = true;
            txtPorcentajeIVA.InsertKeyMode = InsertKeyMode.Default;
            txtPorcentajeIVA.LeadingIcon = null;
            txtPorcentajeIVA.Location = new Point(0, 0);
            txtPorcentajeIVA.Mask = "";
            txtPorcentajeIVA.MaxLength = 32767;
            txtPorcentajeIVA.MouseState = MaterialSkin.MouseState.OUT;
            txtPorcentajeIVA.Name = "txtPorcentajeIVA";
            txtPorcentajeIVA.PasswordChar = '\0';
            txtPorcentajeIVA.PrefixSuffixText = null;
            txtPorcentajeIVA.PromptChar = '_';
            txtPorcentajeIVA.ReadOnly = false;
            txtPorcentajeIVA.RejectInputOnFirstFailure = false;
            txtPorcentajeIVA.ResetOnPrompt = true;
            txtPorcentajeIVA.ResetOnSpace = true;
            txtPorcentajeIVA.RightToLeft = RightToLeft.No;
            txtPorcentajeIVA.SelectedText = "";
            txtPorcentajeIVA.SelectionLength = 0;
            txtPorcentajeIVA.SelectionStart = 0;
            txtPorcentajeIVA.ShortcutsEnabled = true;
            txtPorcentajeIVA.Size = new Size(250, 48);
            txtPorcentajeIVA.SkipLiterals = true;
            txtPorcentajeIVA.TabIndex = 20;
            txtPorcentajeIVA.TabStop = false;
            txtPorcentajeIVA.TextAlign = HorizontalAlignment.Left;
            txtPorcentajeIVA.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtPorcentajeIVA.TrailingIcon = null;
            txtPorcentajeIVA.UseSystemPasswordChar = false;
            txtPorcentajeIVA.ValidatingType = null;
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