using System.Drawing;
using System.Windows.Forms;

namespace ESFE.GestionProductos.UI
{
    partial class FrmUsuario
    {
        private System.ComponentModel.IContainer? components = null;

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
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();

            this.tabPageLista = new System.Windows.Forms.TabPage();

            this.lblTitulo = new MaterialSkin.Controls.MaterialLabel();
            this.lblBusqueda = new MaterialSkin.Controls.MaterialLabel();
            this.txtBuscar = new MaterialSkin.Controls.MaterialTextBox2();
            this.cmbFiltro = new MaterialSkin.Controls.MaterialComboBox();
            this.btnBuscar = new MaterialSkin.Controls.MaterialButton();
            this.btnCrear = new MaterialSkin.Controls.MaterialButton();
            this.lblRegistrados = new MaterialSkin.Controls.MaterialLabel();

            this.dgvUsuarios = new MaterialSkin.Controls.MaterialListView();

            this.colId = new System.Windows.Forms.ColumnHeader();
            this.colNombre = new System.Windows.Forms.ColumnHeader();
            this.colCorreo = new System.Windows.Forms.ColumnHeader();
            this.colRol = new System.Windows.Forms.ColumnHeader();
            this.colEstado = new System.Windows.Forms.ColumnHeader();

            this.tabPageDetalle = new System.Windows.Forms.TabPage();

            this.txtNombre = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtCorreo = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnGuardar = new MaterialSkin.Controls.MaterialButton();


            // =========================================================
            // SUSPEND LAYOUT
            // =========================================================

            this.materialTabControl1.SuspendLayout();
            this.tabPageLista.SuspendLayout();
            this.tabPageDetalle.SuspendLayout();

            ((System.Windows.Forms.Control)this).SuspendLayout();


            // =========================================================
            // TAB SELECTOR
            // =========================================================

            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;

            this.materialTabSelector1.Depth = 0;

            this.materialTabSelector1.Font =
                new System.Drawing.Font(
                    "Roboto",
                    14F,
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Pixel
                );

            this.materialTabSelector1.Location =
                new System.Drawing.Point(0, 64);

            this.materialTabSelector1.MouseState =
                MaterialSkin.MouseState.HOVER;

            this.materialTabSelector1.Name =
                "materialTabSelector1";

            this.materialTabSelector1.Size =
                new System.Drawing.Size(800, 48);


            // =========================================================
            // TAB CONTROL
            // =========================================================

            this.materialTabControl1.Controls.Add(
                this.tabPageLista
            );

            this.materialTabControl1.Controls.Add(
                this.tabPageDetalle
            );

            this.materialTabControl1.Depth = 0;

            this.materialTabControl1.Location =
                new System.Drawing.Point(10, 118);

            this.materialTabControl1.MouseState =
                MaterialSkin.MouseState.HOVER;

            this.materialTabControl1.Name =
                "materialTabControl1";

            this.materialTabControl1.SelectedIndex = 0;

            this.materialTabControl1.Size =
                new System.Drawing.Size(780, 500);


            // =========================================================
            // TAB PAGE LISTA
            // =========================================================

            this.tabPageLista.Controls.Add(this.lblTitulo);
            this.tabPageLista.Controls.Add(this.lblBusqueda);
            this.tabPageLista.Controls.Add(this.txtBuscar);
            this.tabPageLista.Controls.Add(this.cmbFiltro);
            this.tabPageLista.Controls.Add(this.btnBuscar);
            this.tabPageLista.Controls.Add(this.btnCrear);
            this.tabPageLista.Controls.Add(this.lblRegistrados);
            this.tabPageLista.Controls.Add(this.dgvUsuarios);

            this.tabPageLista.Location =
                new System.Drawing.Point(4, 22);

            this.tabPageLista.Name =
                "tabPageLista";

            this.tabPageLista.Padding =
                new System.Windows.Forms.Padding(3);

            this.tabPageLista.Size =
                new System.Drawing.Size(772, 474);

            this.tabPageLista.Text =
                "Consulta de Usuarios";


            // =========================================================
            // LABEL TITULO
            // =========================================================

            this.lblTitulo.AutoSize = true;

            this.lblTitulo.Depth = 0;

            this.lblTitulo.FontType =
                MaterialSkin.MaterialSkinManager.fontType.H5;

            this.lblTitulo.Location =
                new System.Drawing.Point(15, 10);

            this.lblTitulo.Name =
                "lblTitulo";

            this.lblTitulo.Size =
                new System.Drawing.Size(95, 29);

            this.lblTitulo.Text =
                "Usuarios";


            // =========================================================
            // LABEL BUSQUEDA
            // =========================================================

            this.lblBusqueda.AutoSize = true;

            this.lblBusqueda.Depth = 0;

            this.lblBusqueda.FontType =
                MaterialSkin.MaterialSkinManager.fontType.Subtitle1;

            this.lblBusqueda.Location =
                new System.Drawing.Point(20, 50);

            this.lblBusqueda.Name =
                "lblBusqueda";

            this.lblBusqueda.Size =
                new System.Drawing.Size(130, 19);

            this.lblBusqueda.Text =
                "Búsqueda Usuario";


            // =========================================================
            // TEXTBOX BUSCAR
            // =========================================================

            this.txtBuscar.AnimateReadOnly = false;

            this.txtBuscar.Depth = 0;

            this.txtBuscar.Hint =
                "Nombre de usuario...";

            this.txtBuscar.Location =
                new System.Drawing.Point(20, 75);

            this.txtBuscar.MaxLength =
                32767;

            this.txtBuscar.Name =
                "txtBuscar";

            this.txtBuscar.PasswordChar =
                '\0';

            this.txtBuscar.Size =
                new System.Drawing.Size(260, 48);


            // =========================================================
            // COMBOBOX FILTRO
            // =========================================================

            this.cmbFiltro.AutoResize = false;

            this.cmbFiltro.Depth = 0;

            this.cmbFiltro.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cmbFiltro.Hint =
                "FILTRO";

            this.cmbFiltro.Location =
                new System.Drawing.Point(300, 74);

            this.cmbFiltro.Name =
                "cmbFiltro";

            this.cmbFiltro.Size =
                new System.Drawing.Size(200, 49);


            // =========================================================
            // BOTON BUSCAR
            // =========================================================

            this.btnBuscar.AutoSizeMode =
                System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            this.btnBuscar.Density =
                MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;

            this.btnBuscar.Depth = 0;

            this.btnBuscar.HighEmphasis = true;

            this.btnBuscar.Location =
                new System.Drawing.Point(650, 55);

            this.btnBuscar.Name =
                "btnBuscar";

            this.btnBuscar.Size =
                new System.Drawing.Size(88, 36);

            this.btnBuscar.Text =
                "Buscar";

            this.btnBuscar.Type =
                MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;

            this.btnBuscar.Click +=
                new System.EventHandler(this.btnBuscar_Click);


            // =========================================================
            // BOTON CREAR
            // =========================================================

            this.btnCrear.AutoSizeMode =
                System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            this.btnCrear.Density =
                MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;

            this.btnCrear.Depth = 0;

            this.btnCrear.HighEmphasis = true;

            this.btnCrear.Location =
                new System.Drawing.Point(650, 100);

            this.btnCrear.Name =
                "btnCrear";

            this.btnCrear.Size =
                new System.Drawing.Size(88, 36);

            this.btnCrear.Text =
                "+";

            this.btnCrear.Type =
                MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;

            this.btnCrear.Click +=
                new System.EventHandler(this.btnCrear_Click);


            // =========================================================
            // LABEL REGISTRADOS
            // =========================================================

            this.lblRegistrados.AutoSize = true;

            this.lblRegistrados.Depth = 0;

            this.lblRegistrados.FontType =
                MaterialSkin.MaterialSkinManager.fontType.Subtitle1;

            this.lblRegistrados.Location =
                new System.Drawing.Point(20, 145);

            this.lblRegistrados.Name =
                "lblRegistrados";

            this.lblRegistrados.Size =
                new System.Drawing.Size(155, 19);

            this.lblRegistrados.Text =
                "Usuarios Registrados:";


            // =========================================================
            // LISTA DE USUARIOS
            // =========================================================

            this.dgvUsuarios.AutoSizeTable = false;

            this.dgvUsuarios.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvUsuarios.Columns.AddRange(
                new System.Windows.Forms.ColumnHeader[]
                {
                    this.colId,
                    this.colNombre,
                    this.colCorreo,
                    this.colRol,
                    this.colEstado
                }
            );

            this.dgvUsuarios.Depth = 0;

            this.dgvUsuarios.FullRowSelect = true;

            this.dgvUsuarios.Location =
                new System.Drawing.Point(20, 175);

            this.dgvUsuarios.Name =
                "dgvUsuarios";

            this.dgvUsuarios.OwnerDraw = true;

            this.dgvUsuarios.Size =
                new System.Drawing.Size(720, 270);

            this.dgvUsuarios.UseCompatibleStateImageBehavior =
                false;

            this.dgvUsuarios.View =
                System.Windows.Forms.View.Details;


            // COLUMNAS

            this.colId.Text = "ID";
            this.colId.Width = 70;

            this.colNombre.Text = "Nombre";
            this.colNombre.Width = 180;

            this.colCorreo.Text = "Correo";
            this.colCorreo.Width = 200;

            this.colRol.Text = "Rol";
            this.colRol.Width = 140;

            this.colEstado.Text = "Estado";
            this.colEstado.Width = 110;


            // =========================================================
            // TAB PAGE DETALLE
            // =========================================================

            this.tabPageDetalle.Controls.Add(
                this.btnGuardar
            );

            this.tabPageDetalle.Controls.Add(
                this.txtCorreo
            );

            this.tabPageDetalle.Controls.Add(
                this.txtNombre
            );

            this.tabPageDetalle.Location =
                new System.Drawing.Point(4, 22);

            this.tabPageDetalle.Name =
                "tabPageDetalle";

            this.tabPageDetalle.Padding =
                new System.Windows.Forms.Padding(3);

            this.tabPageDetalle.Size =
                new System.Drawing.Size(772, 474);

            this.tabPageDetalle.Text =
                "Registro / Edición";


            // =========================================================
            // TEXTBOX NOMBRE
            // =========================================================

            this.txtNombre.AnimateReadOnly = false;

            this.txtNombre.Depth = 0;

            this.txtNombre.Hint =
                "Nombre del Usuario";

            this.txtNombre.Location =
                new System.Drawing.Point(30, 40);

            this.txtNombre.MaxLength =
                32767;

            this.txtNombre.Name =
                "txtNombre";

            this.txtNombre.PasswordChar =
                '\0';

            this.txtNombre.Size =
                new System.Drawing.Size(400, 48);


            // =========================================================
            // TEXTBOX CORREO
            // =========================================================

            this.txtCorreo.AnimateReadOnly = false;

            this.txtCorreo.Depth = 0;

            this.txtCorreo.Hint =
                "Correo Electrónico";

            this.txtCorreo.Location =
                new System.Drawing.Point(30, 110);

            this.txtCorreo.MaxLength =
                32767;

            this.txtCorreo.Name =
                "txtCorreo";

            this.txtCorreo.PasswordChar =
                '\0';

            this.txtCorreo.Size =
                new System.Drawing.Size(400, 48);


            // =========================================================
            // BOTON GUARDAR
            // =========================================================

            this.btnGuardar.AutoSizeMode =
                System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            this.btnGuardar.Density =
                MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;

            this.btnGuardar.Depth = 0;

            this.btnGuardar.HighEmphasis = true;

            this.btnGuardar.Location =
                new System.Drawing.Point(30, 180);

            this.btnGuardar.Name =
                "btnGuardar";

            this.btnGuardar.Size =
                new System.Drawing.Size(88, 36);

            this.btnGuardar.Text =
                "Guardar";

            this.btnGuardar.Type =
                MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;

            this.btnGuardar.Click +=
                new System.EventHandler(this.btnGuardar_Click);


            // =========================================================
            // FORM
            // =========================================================

            this.ClientSize =
                new System.Drawing.Size(800, 630);

            this.Controls.Add(
                this.materialTabControl1
            );

            this.Controls.Add(
                this.materialTabSelector1
            );

            this.Name =
                "FrmUsuario";

            this.Text =
                "Gestión de Usuarios";


            // =========================================================
            // RESUME LAYOUT
            // =========================================================

            this.materialTabControl1.ResumeLayout(false);

            this.tabPageLista.ResumeLayout(false);
            this.tabPageLista.PerformLayout();

            this.tabPageDetalle.ResumeLayout(false);
            this.tabPageDetalle.PerformLayout();

            ((System.Windows.Forms.Control)this).ResumeLayout(false);
        }

        #endregion


        // =============================================================
        // CONTROLES
        // =============================================================

        private MaterialSkin.Controls.MaterialTabSelector? materialTabSelector1;

        private MaterialSkin.Controls.MaterialTabControl? materialTabControl1;

        private System.Windows.Forms.TabPage? tabPageLista;

        private System.Windows.Forms.TabPage? tabPageDetalle;

        private MaterialSkin.Controls.MaterialLabel? lblTitulo;

        private MaterialSkin.Controls.MaterialLabel? lblBusqueda;

        private MaterialSkin.Controls.MaterialTextBox2? txtBuscar;

        private MaterialSkin.Controls.MaterialComboBox? cmbFiltro;

        private MaterialSkin.Controls.MaterialButton? btnBuscar;

        private MaterialSkin.Controls.MaterialButton? btnCrear;

        private MaterialSkin.Controls.MaterialLabel? lblRegistrados;

        private MaterialSkin.Controls.MaterialListView? dgvUsuarios;

        private System.Windows.Forms.ColumnHeader? colId;

        private System.Windows.Forms.ColumnHeader? colNombre;

        private System.Windows.Forms.ColumnHeader? colCorreo;

        private System.Windows.Forms.ColumnHeader? colRol;

        private System.Windows.Forms.ColumnHeader? colEstado;

        private MaterialSkin.Controls.MaterialTextBox2? txtNombre;

        private MaterialSkin.Controls.MaterialTextBox2? txtCorreo;

        private MaterialSkin.Controls.MaterialButton? btnGuardar;
    }
}