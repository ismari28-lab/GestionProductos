using System.Drawing;
using System.Windows.Forms;

namespace ESFE.GestionProductos.UI
{
    public partial class ucInicio : UserControl
    {
        public ucInicio()
        {
            InitializeComponent();
            EstilizarGrid();
            CargarTarjetasResumen();
            CargarVentasRecientes();

            this.Load += (s, e) =>
            {
                // Asegurar que la paginación está dentro del panel de ventas
                if (pnlPaginacion.Parent != pnlVentas)
                {
                    pnlPaginacion.Parent = pnlVentas;
                }

                pnlPaginacion.Location = new Point(
                    pnlVentas.Width - pnlPaginacion.Width - 20,
                    pnlVentas.Height - pnlPaginacion.Height - 15
                );

                pnlPaginacion.BringToFront();
            };
        }

        private void CargarTarjetasResumen()
        {
            panelContenedorTarjetas.Controls.Clear();

            var verde = Color.FromArgb(40, 167, 69);
            var azul = Color.FromArgb(52, 120, 246);
            var morado = Color.FromArgb(120, 90, 200);
            var naranja = Color.FromArgb(245, 130, 40);

            AgregarCard("Total Vendido", "21,324", "+2,031", true, "$", verde);
            AgregarCard("Ventas Totales", "8,549", "342", true, "#", azul);
            AgregarCard("Nuevos Clientes", "1,287", "89", true, "+", morado);
            AgregarCard("Productos Vendidos", "4,812", "-156", false, "•", naranja);
        }

        private void AgregarCard(string titulo, string valor, string tend, bool positivo, string icono, Color color)
        {
            var card = new ucCardResumen
            {
                Size = new Size(260, 150),   // <-- antes 250x115
                Margin = new Padding(0, 0, 15, 0)
            };
            card.ConfigurarTarjeta(titulo, valor, tend, positivo, icono, color);
            panelContenedorTarjetas.Controls.Add(card);
        }

        private void EstilizarGrid()
        {
            // Celdas normales
            dgvVentasRecientes.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvVentasRecientes.DefaultCellStyle.ForeColor = Color.FromArgb(60, 70, 85);
            dgvVentasRecientes.DefaultCellStyle.BackColor = Color.White;
            dgvVentasRecientes.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);

            // Fila seleccionada: fondo gris muy claro
            dgvVentasRecientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgvVentasRecientes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(60, 70, 85);

            // Headers
            dgvVentasRecientes.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvVentasRecientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(120, 130, 145);
            dgvVentasRecientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvVentasRecientes.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgvVentasRecientes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvVentasRecientes.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvVentasRecientes.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(120, 130, 145);

            // Quitar el sort glyph
            foreach (DataGridViewColumn col in dgvVentasRecientes.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Columna Actions en morado alineada a la derecha
            colActions.DefaultCellStyle.ForeColor = Color.FromArgb(90, 70, 180);
            colActions.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colActions.DefaultCellStyle.SelectionForeColor = Color.FromArgb(90, 70, 180);
            colActions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
        }

        private void CargarVentasRecientes()
        {
            dgvVentasRecientes.Rows.Clear();
            dgvVentasRecientes.Rows.Add("Marcus Wellington", "Dec 14, 2024", "(415) 555-0172", "San Francisco, CA", "Yes", "Options  Detalle");
            dgvVentasRecientes.Rows.Add("Priya Chatterjee", "Dec 15, 2024", "(212) 555-0198", "Brooklyn, NY", "Yes", "Options  Detalle");
            dgvVentasRecientes.Rows.Add("Derek Okonkwo", "Dec 12, 2024", "(713) 555-0154", "Houston, TX", "No", "Options  Detalle");
            dgvVentasRecientes.Rows.Add("Samantha Liu", "Dec 11, 2024", "(206) 555-0156", "Seattle, WA", "Yes", "Options  Detalle");

            dgvVentasRecientes.ClearSelection();
            // NO poner CurrentCell = null aquí
        }
    }
}