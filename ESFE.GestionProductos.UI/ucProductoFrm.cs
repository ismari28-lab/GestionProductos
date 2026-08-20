using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
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

            if (ProductoId.HasValue)
            {
                lblSubtitulo.Text =
                    $"Modificando la información del producto ID: #{ProductoId.Value}";

                CargarDatosProducto(ProductoId.Value);
            }
            else
            {
                lblSubtitulo.Text =
                    "Ingrese los detalles del producto";
            }
        }

        private void ucProductoFrm_Load(object sender, EventArgs e)
        {
            CentrarTarjeta();
        }

        private void pnlContenedorCentral_Resize(
            object sender,
            EventArgs e)
        {
            CentrarTarjeta();
        }

        private void CentrarTarjeta()
        {
            if (cardFormulario == null ||
                pnlContenedorCentral == null)
                return;

            int posX =
                (pnlContenedorCentral.ClientSize.Width -
                 cardFormulario.Width) / 2;

            if (posX < 15)
                posX = 15;

            int posY = 65;

            cardFormulario.Location =
                new Point(posX, posY);

            if (lblSubtitulo != null)
            {
                lblSubtitulo.Location =
                    new Point(posX, posY - 35);
            }
        }

        private void CargarDatosProducto(int id)
        {
            try
            {
                var lista =
                    _productoLN.Buscar(null, (short)id);

                if (lista.Count > 0)
                {
                    ProductoActual = lista[0];

                    txtNombre.Text =
                        ProductoActual.Nombre ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(
            object sender,
            EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCerrar_Click(
            object sender,
            EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "El nombre del producto es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                if (ProductoActual == null)
                {
                    ProductoActual = new Producto();
                }

                ProductoActual.Nombre =
                    txtNombre.Text.Trim();

                ProductoActual.Estado = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al preparar el producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}