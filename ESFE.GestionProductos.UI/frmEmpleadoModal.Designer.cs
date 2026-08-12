namespace ESFE.GestionProductos.UI
{
    partial class frmEmpleadoModal
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            txtNombre = new MaterialSkin.Controls.MaterialMaskedTextBox();
            txtTelefono = new MaterialSkin.Controls.MaterialMaskedTextBox();
            cmbCargo = new MaterialSkin.Controls.MaterialComboBox();
            cmbUsuario = new MaterialSkin.Controls.MaterialComboBox();
            chkActivo = new MaterialSkin.Controls.MaterialCheckbox();
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
            materialCard1.Controls.Add(chkActivo);
            materialCard1.Controls.Add(cmbUsuario);
            materialCard1.Controls.Add(cmbCargo);
            materialCard1.Controls.Add(txtTelefono);
            materialCard1.Controls.Add(txtNombre);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(20, 80);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(24);
            materialCard1.Size = new Size(580, 620);
            materialCard1.TabIndex = 0;
            // 
            // txtNombre (CAMPO 1)
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
            txtNombre.Hint = "Nombre Completo";
            txtNombre.InsertKeyMode = InsertKeyMode.Default;
            txtNombre.LeadingIcon = null;
            txtNombre.Location = new Point(24, 25);
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
            txtNombre.Size = new Size(532, 55);
            txtNombre.SkipLiterals = true;
            txtNombre.TabIndex = 0;
            txtNombre.TabStop = false;
            txtNombre.TextAlign = HorizontalAlignment.Left;
            txtNombre.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtNombre.TrailingIcon = null;
            txtNombre.UseSystemPasswordChar = false;
            txtNombre.ValidatingType = null;
            // 
            // txtTelefono (CAMPO 2 - CON ESPACIADO DE 30PX)
            // 
            txtTelefono.AllowPromptAsInput = true;
            txtTelefono.AnimateReadOnly = false;
            txtTelefono.AsciiOnly = false;
            txtTelefono.BackgroundImageLayout = ImageLayout.None;
            txtTelefono.BeepOnError = false;
            txtTelefono.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            txtTelefono.Depth = 0;
            txtTelefono.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtTelefono.HidePromptOnLeave = false;
            txtTelefono.HideSelection = true;
            txtTelefono.Hint = "Teléfono (0000-0000)";
            txtTelefono.InsertKeyMode = InsertKeyMode.Default;
            txtTelefono.LeadingIcon = null;
            txtTelefono.Location = new Point(24, 110);
            txtTelefono.Mask = "0000-0000";
            txtTelefono.MaxLength = 32767;
            txtTelefono.MouseState = MaterialSkin.MouseState.OUT;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PasswordChar = '\0';
            txtTelefono.PrefixSuffixText = null;
            txtTelefono.PromptChar = '_';
            txtTelefono.ReadOnly = false;
            txtTelefono.RejectInputOnFirstFailure = false;
            txtTelefono.ResetOnPrompt = true;
            txtTelefono.ResetOnSpace = true;
            txtTelefono.RightToLeft = RightToLeft.No;
            txtTelefono.SelectedText = "";
            txtTelefono.SelectionLength = 0;
            txtTelefono.SelectionStart = 0;
            txtTelefono.ShortcutsEnabled = true;
            txtTelefono.Size = new Size(532, 55);
            txtTelefono.SkipLiterals = true;
            txtTelefono.TabIndex = 1;
            txtTelefono.TabStop = false;
            txtTelefono.TextAlign = HorizontalAlignment.Left;
            txtTelefono.TextMaskFormat = MaskFormat.IncludeLiterals;
            txtTelefono.TrailingIcon = null;
            txtTelefono.UseSystemPasswordChar = false;
            txtTelefono.ValidatingType = null;
            // 
            // cmbCargo (CAMPO 3)
            // 
            cmbCargo.AutoResize = false;
            cmbCargo.BackColor = Color.FromArgb(255, 255, 255);
            cmbCargo.Depth = 0;
            cmbCargo.DrawMode = DrawMode.OwnerDrawVariable;
            cmbCargo.DropDownHeight = 174;
            cmbCargo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCargo.DropDownWidth = 121;
            cmbCargo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbCargo.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbCargo.FormattingEnabled = true;
            cmbCargo.Hint = "Cargo";
            cmbCargo.IntegralHeight = false;
            cmbCargo.ItemHeight = 43;
            cmbCargo.Location = new Point(24, 195);
            cmbCargo.MaxDropDownItems = 4;
            cmbCargo.MouseState = MaterialSkin.MouseState.OUT;
            cmbCargo.Name = "cmbCargo";
            cmbCargo.Size = new Size(532, 49);
            cmbCargo.StartIndex = 0;
            cmbCargo.TabIndex = 2;
            // 
            // cmbUsuario (CAMPO 4)
            // 
            cmbUsuario.AutoResize = false;
            cmbUsuario.BackColor = Color.FromArgb(255, 255, 255);
            cmbUsuario.Depth = 0;
            cmbUsuario.DrawMode = DrawMode.OwnerDrawVariable;
            cmbUsuario.DropDownHeight = 174;
            cmbUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUsuario.DropDownWidth = 121;
            cmbUsuario.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbUsuario.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.Hint = "Usuario Asignado (Opcional)";
            cmbUsuario.IntegralHeight = false;
            cmbUsuario.ItemHeight = 43;
            cmbUsuario.Location = new Point(24, 280);
            cmbUsuario.MaxDropDownItems = 4;
            cmbUsuario.MouseState = MaterialSkin.MouseState.OUT;
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(532, 49);
            cmbUsuario.StartIndex = 0;
            cmbUsuario.TabIndex = 3;
            // 
            // chkActivo (CHECKBOX MÁS HOLGADO)
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Depth = 0;
            chkActivo.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point);
            chkActivo.Location = new Point(24, 365);
            chkActivo.Margin = new Padding(0);
            chkActivo.MouseLocation = new Point(-1, -1);
            chkActivo.MouseState = MaterialSkin.MouseState.HOVER;
            chkActivo.Name = "chkActivo";
            chkActivo.Ripple = true;
            chkActivo.Size = new Size(101, 37);
            chkActivo.TabIndex = 4;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // btnCancelar (BOTÓN CANCELAR AMPLIO)
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.AutoSize = false;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCancelar.Depth = 0;
            btnCancelar.HighEmphasis = false;
            btnCancelar.Icon = null;
            btnCancelar.Location = new Point(236, 530);
            btnCancelar.Margin = new Padding(4, 6, 15, 6);
            btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.NoAccentTextColor = Color.Empty;
            btnCancelar.Size = new Size(150, 54);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnCancelar.UseAccentColor = false;
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar (BOTÓN GUARDAR AMPLIO)
            // 
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardar.AutoSize = false;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnGuardar.Depth = 0;
            btnGuardar.HighEmphasis = true;
            btnGuardar.Icon = null;
            btnGuardar.Location = new Point(406, 530);
            btnGuardar.Margin = new Padding(4, 6, 4, 6);
            btnGuardar.MouseState = MaterialSkin.MouseState.HOVER;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.NoAccentTextColor = Color.Empty;
            btnGuardar.Size = new Size(150, 54);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnGuardar.UseAccentColor = false;
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // frmEmpleadoModal
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 720);
            Controls.Add(materialCard1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmEmpleadoModal";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editar Empleado";
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtNombre;
        private MaterialSkin.Controls.MaterialMaskedTextBox txtTelefono;
        private MaterialSkin.Controls.MaterialComboBox cmbCargo;
        private MaterialSkin.Controls.MaterialComboBox cmbUsuario;
        private MaterialSkin.Controls.MaterialCheckbox chkActivo;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private MaterialSkin.Controls.MaterialButton btnGuardar;
    }
}