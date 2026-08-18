using System;
using System.Windows.Forms;

namespace ESFE.GestionProductos.UI
{
    /// <summary>
    /// UserControl que replica el wireframe "Catálogo de Productos":
    /// una consulta rápida y una sección de información del producto
    /// con alerta de stock bajo.
    /// Requiere el paquete NuGet MaterialSkin.2
    /// </summary>
    public partial class UcCatalogoProductos : UserControl
    {
        public UcCatalogoProductos()
        {
            InitializeComponent();

            // Oculta la tarjeta de alerta hasta que exista un resultado con stock bajo
            pnlAlertaStockBajo.Visible = false;
        }

        /// <summary>
        /// Evento público que la pantalla contenedora puede suscribir
        /// para ejecutar la búsqueda contra tu capa de datos.
        /// </summary>
        public event EventHandler<string> BuscarProducto;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string termino = txtConsultaRapida.Text?.Trim();

            if (string.IsNullOrEmpty(termino))
            {
                MessageBox.Show("Ingrese un término de búsqueda.", "Consulta Rápida",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Delega la búsqueda real a quien use el control
            BuscarProducto?.Invoke(this, termino);
        }

        /// <summary>
        /// Método de ayuda para que el formulario padre pinte el resultado
        /// en el campo de información del producto.
        /// </summary>
        public void MostrarInformacionProducto(string textoInfo, bool stockBajo)
        {
            txtInfoProducto.Text = textoInfo;
            pnlAlertaStockBajo.Visible = stockBajo;
        }

        /// <summary>
        /// Limpia los campos de búsqueda y resultado.
        /// </summary>
        public void Limpiar()
        {
            txtConsultaRapida.Text = string.Empty;
            txtInfoProducto.Text = string.Empty;
            pnlAlertaStockBajo.Visible = false;
        }
    }
}