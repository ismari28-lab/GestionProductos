using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    public partial class frmProductoModal : MaterialForm
    {
        public int? ProductoId { get; private set; }

        public frmProductoModal(int? id = null)
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            ProductoId = id;

            if (ProductoId.HasValue)
            {
                Text = "Editar Producto";
                CargarDatosProducto(ProductoId.Value);
            }
            else
            {
                Text = "Crear Producto";
            }
        }

        private void CargarDatosProducto(int id)
        {
            // Lógica para cargar el producto si es edición:
            // var prod = _productoService.GetById(id);
            // txtNombre.Text = prod.Nombre;
            // txtPrecio.Text = prod.Precio.ToString("F2");
            // txtStock.Text = prod.Stock.ToString();
            // txtStockMinimo.Text = prod.StockMinimo.ToString();
            // cmbCategoria.SelectedValue = prod.CategoriaId;
            // chkEstado.Checked = prod.Estado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Ingrese una cantidad de stock válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            if (!int.TryParse(txtStockMinimo.Text, out int stockMinimo))
            {
                MessageBox.Show("Ingrese un valor válido para el stock mínimo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStockMinimo.Focus();
                return;
            }

            // TODO: Guardar o actualizar en base de datos

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}