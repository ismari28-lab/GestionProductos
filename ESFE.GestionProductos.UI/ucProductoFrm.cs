using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    public partial class ucProductoFrm : MaterialForm
    {
        public int? ProductoId { get; private set; }

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
                
                lblSubtitulo.Text = $"Modificando la información del producto ID: #{ProductoId.Value}";
                CargarDatosProducto(ProductoId.Value);
            }
            else
            {
                
                lblSubtitulo.Text = "Ingrese los detalles del producto";
            }
        }

        private void ucProductoFrm_Load(object sender, EventArgs e)
        {
            CentrarTarjeta();
        }

        private void pnlContenedorCentral_Resize(object sender, EventArgs e)
        {
            CentrarTarjeta();
        }

        /// <summary>
        /// Posiciona la tarjeta de forma estática respetando los márgenes superior e inferior.
        /// </summary>
        private void CentrarTarjeta()
        {
            if (cardFormulario == null || pnlContenedorCentral == null) return;

            // Centrado horizontal
            int posX = (pnlContenedorCentral.ClientSize.Width - cardFormulario.Width) / 2;
            if (posX < 15) posX = 15;

            // Posición vertical fija que da el respiro perfecto arriba y abajo
            int posY = 65;

            cardFormulario.Location = new Point(posX, posY);

            if (lblSubtitulo != null)
            {
                lblSubtitulo.Location = new Point(posX, posY - 35);
            }
        }

        private void CargarDatosProducto(int id)
        {
            // Lógica de carga desde BD
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}