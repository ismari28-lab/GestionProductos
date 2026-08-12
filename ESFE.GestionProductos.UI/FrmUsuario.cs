using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    public partial class FrmUsuario : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public FrmUsuario()
        {
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE
            );

            CargarFiltros();
        }

        private void CargarFiltros()
        {
            if (cmbFiltro != null)
            {
                cmbFiltro.Items.Clear();
                cmbFiltro.Items.Add("Todos");
                cmbFiltro.Items.Add("Activos");
                cmbFiltro.Items.Add("Inactivos");
                cmbFiltro.SelectedIndex = 0;
            }
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            string busqueda = txtBuscar?.Text ?? "";
            string filtro = cmbFiltro?.SelectedItem?.ToString() ?? "Todos";

            MaterialMessageBox.Show($"Filtrando usuarios por: '{busqueda}' [{filtro}]", "Búsqueda");
        }

        private void btnCrear_Click(object? sender, EventArgs e)
        {
            if (materialTabControl1 != null && tabPageDetalle != null)
            {
                materialTabControl1.SelectedTab = tabPageDetalle;
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (txtNombre != null && string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MaterialMessageBox.Show("El nombre de usuario es obligatorio.", "Atención");
                return;
            }

            if (dgvUsuarios != null)
            {
                ListViewItem item = new ListViewItem(new[] {
                    DateTime.Now.Ticks.ToString().Substring(14),
                    txtNombre?.Text ?? "",
                    txtCorreo?.Text ?? "",
                    "Administrador",
                    "Activo"
                });

                dgvUsuarios.Items.Add(item);
                MaterialMessageBox.Show("Usuario guardado exitosamente.", "Éxito");
            }

            LimpiarCampos();

            if (materialTabControl1 != null && tabPageLista != null)
            {
                materialTabControl1.SelectedTab = tabPageLista;
            }
        }

        private void LimpiarCampos()
        {
            if (txtNombre != null) txtNombre.Clear();
            if (txtCorreo != null) txtCorreo.Clear();
        }
    }
}