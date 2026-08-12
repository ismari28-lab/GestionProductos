namespace ESFE.GestionProductos.UI
{
    partial class frmEmpleado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            materialCard2 = new MaterialSkin.Controls.MaterialCard();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            materialListView1 = new MaterialSkin.Controls.MaterialListView();
            id = new ColumnHeader();
            nombre = new ColumnHeader();
            apellido = new ColumnHeader();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            panel1 = new Panel();
            btnBuscar = new MaterialSkin.Controls.MaterialButton();
            btnCrear = new MaterialSkin.Controls.MaterialButton();
            SlctFiltro = new MaterialSkin.Controls.MaterialComboBox();
            materialMaskedTextBox1 = new MaterialSkin.Controls.MaterialMaskedTextBox();
            lblBuscador = new MaterialSkin.Controls.MaterialLabel();
            lblEmpleadoTitulo = new MaterialSkin.Controls.MaterialLabel();
            materialTabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            materialCard2.SuspendLayout();
            materialCard3.SuspendLayout();
            materialCard1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // materialTabControl1
            // 
            materialTabControl1.Controls.Add(tabPage1);
            materialTabControl1.Controls.Add(tabPage2);
            materialTabControl1.Depth = 0;
            materialTabControl1.Dock = DockStyle.Fill;
            materialTabControl1.Location = new Point(3, 64);
            materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new Size(998, 580);
            materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(990, 542);
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
            tabPage2.Size = new Size(990, 542);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // materialCard2
            // 
            materialCard2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialCard2.BackColor = Color.FromArgb(255, 255, 255);
            materialCard2.Controls.Add(materialCard3);
            materialCard2.Controls.Add(materialLabel2);
            materialCard2.Depth = 0;
            materialCard2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard2.Location = new Point(27, 285);
            materialCard2.Margin = new Padding(14);
            materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard2.Name = "materialCard2";
            materialCard2.Padding = new Padding(14);
            materialCard2.Size = new Size(946, 240);
            materialCard2.TabIndex = 1;
            // 
            // materialCard3
            // 
            materialCard3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.Controls.Add(materialListView1);
            materialCard3.Depth = 0;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(28, 47);
            materialCard3.Margin = new Padding(14);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(14);
            materialCard3.Size = new Size(890, 119);
            materialCard3.TabIndex = 1;
            // 
            // materialListView1
            // 
            materialListView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            materialListView1.AutoSizeTable = false;
            materialListView1.BackColor = Color.FromArgb(255, 255, 255);
            materialListView1.BorderStyle = BorderStyle.None;
            materialListView1.Columns.AddRange(new ColumnHeader[] { id, nombre, apellido });
            materialListView1.Depth = 0;
            materialListView1.FullRowSelect = true;
            materialListView1.Location = new Point(39, 9);
            materialListView1.MinimumSize = new Size(200, 100);
            materialListView1.MouseLocation = new Point(-1, -1);
            materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            materialListView1.MultiSelect = false;
            materialListView1.Name = "materialListView1";
            materialListView1.OwnerDraw = true;
            materialListView1.Size = new Size(807, 100);
            materialListView1.TabIndex = 1;
            materialListView1.UseCompatibleStateImageBehavior = false;
            materialListView1.View = View.Details;
            // 
            // id
            // 
            id.Text = "ID";
            id.Width = 80;
            // 
            // nombre
            // 
            nombre.Text = "Nombre";
            nombre.Width = 200;
            // 
            // apellido
            // 
            apellido.Text = "Apellido";
            apellido.Width = 200;
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(17, 14);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(172, 19);
            materialLabel2.TabIndex = 0;
            materialLabel2.Text = "Empleados Registrados:";
            // 
            // materialCard1
            // 
            materialCard1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(panel1);
            materialCard1.Controls.Add(lblEmpleadoTitulo);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(22, 11);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(946, 223);
            materialCard1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(btnBuscar);
            panel1.Controls.Add(btnCrear);
            panel1.Controls.Add(SlctFiltro);
            panel1.Controls.Add(materialMaskedTextBox1);
            panel1.Controls.Add(lblBuscador);
            panel1.Location = new Point(13, 57);
            panel1.Name = "panel1";
            panel1.Size = new Size(917, 149);
            panel1.TabIndex = 6;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnBuscar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnBuscar.Depth = 0;
            btnBuscar.HighEmphasis = true;
            btnBuscar.Icon = null;
            btnBuscar.Location = new Point(826, 72);
            btnBuscar.Margin = new Padding(4, 6, 4, 6);
            btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            btnBuscar.Name = "btnBuscar";
            btnBuscar.NoAccentTextColor = Color.Empty;
            btnBuscar.Size = new Size(77, 36);
            btnBuscar.TabIndex = 5;
            btnBuscar.Text = "Buscar";
            btnBuscar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnBuscar.UseAccentColor = false;
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCrear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCrear.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCrear.Depth = 0;
            btnCrear.HighEmphasis = true;
            btnCrear.Icon = null;
            btnCrear.Location = new Point(826, 15);
            btnCrear.Margin = new Padding(4, 6, 4, 6);
            btnCrear.MouseState = MaterialSkin.MouseState.HOVER;
            btnCrear.Name = "btnCrear";
            btnCrear.NoAccentTextColor = Color.Empty;
            btnCrear.Size = new Size(79, 36);
            btnCrear.TabIndex = 4;
            btnCrear.Text = "Crear +";
            btnCrear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnCrear.UseAccentColor = false;
            btnCrear.UseVisualStyleBackColor = true;
            // 
            // SlctFiltro
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
            SlctFiltro.Location = new Point(405, 59);
            SlctFiltro.MaxDropDownItems = 4;
            SlctFiltro.MouseState = MaterialSkin.MouseState.OUT;
            SlctFiltro.Name = "SlctFiltro";
            SlctFiltro.Size = new Size(266, 49);
            SlctFiltro.StartIndex = 0;
            SlctFiltro.TabIndex = 3;
            // 
            // materialMaskedTextBox1
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
            materialMaskedTextBox1.Hint = "Nombre o DUI";
            materialMaskedTextBox1.InsertKeyMode = InsertKeyMode.Default;
            materialMaskedTextBox1.LeadingIcon = null;
            materialMaskedTextBox1.Location = new Point(32, 60);
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
            materialMaskedTextBox1.Size = new Size(274, 48);
            materialMaskedTextBox1.SkipLiterals = true;
            materialMaskedTextBox1.TabIndex = 2;
            materialMaskedTextBox1.TabStop = false;
            materialMaskedTextBox1.TextAlign = HorizontalAlignment.Left;
            materialMaskedTextBox1.TextMaskFormat = MaskFormat.IncludeLiterals;
            materialMaskedTextBox1.TrailingIcon = null;
            materialMaskedTextBox1.UseSystemPasswordChar = false;
            materialMaskedTextBox1.ValidatingType = null;
            // 
            // lblBuscador
            // 
            lblBuscador.AutoSize = true;
            lblBuscador.Depth = 0;
            lblBuscador.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblBuscador.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            lblBuscador.Location = new Point(15, 15);
            lblBuscador.MouseState = MaterialSkin.MouseState.HOVER;
            lblBuscador.Name = "lblBuscador";
            lblBuscador.Size = new Size(161, 24);
            lblBuscador.TabIndex = 1;
            lblBuscador.Text = "Buscar Empleado:";
            // 
            // lblEmpleadoTitulo
            // 
            lblEmpleadoTitulo.AutoSize = true;
            lblEmpleadoTitulo.Depth = 0;
            lblEmpleadoTitulo.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblEmpleadoTitulo.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            lblEmpleadoTitulo.Location = new Point(17, 14);
            lblEmpleadoTitulo.MouseState = MaterialSkin.MouseState.HOVER;
            lblEmpleadoTitulo.Name = "lblEmpleadoTitulo";
            lblEmpleadoTitulo.Size = new Size(152, 41);
            lblEmpleadoTitulo.TabIndex = 0;
            lblEmpleadoTitulo.Text = "Empleado";
            // 
            // frmEmpleado
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1004, 647);
            Controls.Add(materialTabControl1);
            DrawerIsOpen = true;
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = materialTabControl1;
            Name = "frmEmpleado";
            Text = "Empleado";
            materialTabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            materialCard2.ResumeLayout(false);
            materialCard2.PerformLayout();
            materialCard3.ResumeLayout(false);
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private ColumnHeader apellido;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private MaterialSkin.Controls.MaterialLabel lblBuscador;
        private MaterialSkin.Controls.MaterialComboBox SlctFiltro;
        private MaterialSkin.Controls.MaterialButton btnCrear;
        private Panel panel1;
        private MaterialSkin.Controls.MaterialButton btnBuscar;
        private MaterialSkin.Controls.MaterialMaskedTextBox materialMaskedTextBox1;
    }
}