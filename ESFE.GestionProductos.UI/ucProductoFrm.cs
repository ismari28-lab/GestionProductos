using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN;

namespace ESFE.GestionProductos.UI
{
    public partial class ucProductoFrm : MaterialForm
    {
        private readonly ProductoLN _productoLN = new ProductoLN();

        public int? ProductoId { get; private set; }
        public Producto ProductoActual { get; private set; }

        public ucProductoFrm(int? id = null)
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Sizable = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            ProductoId = id;
        }

        private void ucProductoFrm_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarCategorias();
            CentrarTarjeta();

            if (ProductoId.HasValue)
            {
                lblSubtitulo.Text =
                    $"Modificando la información del producto ID: #{ProductoId.Value}";
                CargarDatosProducto(ProductoId.Value);
            }
            else
            {
                lblSubtitulo.Text = "Ingrese los detalles del producto";
            }
        }

        private void pnlContenedorCentral_Resize(object sender, EventArgs e)
        {
            CentrarTarjeta();
        }

        private void CentrarTarjeta()
        {
            if (cardFormulario == null || pnlContenedorCentral == null) return;

            int posX = (pnlContenedorCentral.ClientSize.Width - cardFormulario.Width) / 2;
            if (posX < 15) posX = 15;
            int posY = 65;

            cardFormulario.Location = new Point(posX, posY);
            if (lblSubtitulo != null)
                lblSubtitulo.Location = new Point(posX, posY - 35);
        }

        // ---------- Carga de combos ----------

        private void CargarProveedores()
        {
            try
            {
                cmbProveedor.DataSource = null;
                cmbProveedor.DisplayMember = "Empresa";
                cmbProveedor.ValueMember = "IdProveedorPK";
                cmbProveedor.DataSource = new ProveedorLN().ObtenerActivos();
                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los proveedores: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                cmbCategoria.DataSource = null;
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "IdCategoriaPK";
                cmbCategoria.DataSource = new CategoriaLN().ObtenerActivas();
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar las categorías: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---------- Cargar en modo edición ----------

        private void CargarDatosProducto(int id)
        {
            try
            {
                var lista = _productoLN.Buscar(null, (short)id);
                if (lista.Count == 0) return;

                ProductoActual = lista[0];

                txtCodigo.Text = ProductoActual.Codigo ?? "";
                txtNombre.Text = ProductoActual.Nombre ?? "";
                txtDescripcion.Text = ProductoActual.Descripcion ?? "";
                txtPrecioCompra.Text = ProductoActual.PrecioCompra?.ToString() ?? "";
                txtPrecioVenta.Text = ProductoActual.PrecioVenta?.ToString() ?? "";
                txtPorcentajeIVA.Text = ProductoActual.PorcentajeIVA?.ToString() ?? "";
                chkAplicaIVA.Checked = ProductoActual.AplicaIVA ?? false;
                chkEstado.Checked = ProductoActual.Estado ?? true;

                if (ProductoActual.IdProveedorFK.HasValue)
                    cmbProveedor.SelectedValue = ProductoActual.IdProveedorFK.Value;

                if (ProductoActual.IdCategoriaFK.HasValue)
                    cmbCategoria.SelectedValue = ProductoActual.IdCategoriaFK.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Botones ----------

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El código del producto es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal? precioCompra = ParseDecimalOpcional(txtPrecioCompra.Text, "Precio Compra");
            if (precioCompra == null && !string.IsNullOrWhiteSpace(txtPrecioCompra.Text)) return;

            decimal? precioVenta = ParseDecimalOpcional(txtPrecioVenta.Text, "Precio Venta");
            if (precioVenta == null && !string.IsNullOrWhiteSpace(txtPrecioVenta.Text)) return;

            decimal? porcentajeIVA = ParseDecimalOpcional(txtPorcentajeIVA.Text, "Porcentaje IVA");
            if (porcentajeIVA == null && !string.IsNullOrWhiteSpace(txtPorcentajeIVA.Text)) return;

            try
            {
                if (ProductoActual == null)
                    ProductoActual = new Producto();

                ProductoActual.Codigo = txtCodigo.Text.Trim();
                ProductoActual.Nombre = txtNombre.Text.Trim();
                ProductoActual.Descripcion = txtDescripcion.Text.Trim();
                ProductoActual.PrecioCompra = precioCompra;
                ProductoActual.PrecioVenta = precioVenta;
                ProductoActual.PorcentajeIVA = porcentajeIVA;
                ProductoActual.AplicaIVA = chkAplicaIVA.Checked;
                ProductoActual.Estado = chkEstado.Checked;

                if (cmbProveedor.SelectedValue != null &&
                    short.TryParse(cmbProveedor.SelectedValue.ToString(), out short idProv))
                    ProductoActual.IdProveedorFK = idProv;
                else
                    ProductoActual.IdProveedorFK = null;

                if (cmbCategoria.SelectedValue != null &&
                    short.TryParse(cmbCategoria.SelectedValue.ToString(), out short idCat))
                    ProductoActual.IdCategoriaFK = idCat;
                else
                    ProductoActual.IdCategoriaFK = null;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al preparar el producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal? ParseDecimalOpcional(string texto, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;

            if (decimal.TryParse(texto.Trim(), out decimal valor))
                return valor;

            MessageBox.Show($"El campo '{nombreCampo}' debe ser numérico.",
                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }
}