using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN;

namespace ESFE.GestionProductos.UI
{
    public partial class ucProducto : UserControl
    {
        private readonly ProductoLN _productoLN = new ProductoLN();

        public ucProducto()
        {
            InitializeComponent();
            ConfigurarColumnas();
            VincularEventos();
        }

        private void ConfigurarColumnas()
        {
            dgvProductos.AutoGenerateColumns = false;

            colId.DataPropertyName = "IdProductoPK";
            colNombre.DataPropertyName = "Nombre";
            colPrecio.DataPropertyName = "PrecioVenta";
            colCategoria.DataPropertyName = "IdCategoriaFK";
            colEstado.DataPropertyName = "Estado";

            dgvProductos.ContextMenuStrip = cmsOpciones;
        }

        private void VincularEventos()
        {
            this.Load += (s, e) => CargarTabla();

            btnCrear.Click += BtnCrear_Click;
            btnBuscar.Click += (s, e) => BuscarProductos();
            txtBuscar.TextChanged += (s, e) => BuscarProductos();

            itemEditar.Click += ItemEditar_Click;
            itemEliminar.Click += ItemEliminar_Click;

            dgvProductos.CellClick += DgvProductos_CellClick;
            dgvProductos.CellFormatting += DgvProductos_CellFormatting;
        }

        private void DgvProductos_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                dgvProductos.Columns[e.ColumnIndex].Name == "colActions")
            {
                e.Value = "⋮ Opciones";
            }
        }

        private void DgvProductos_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvProductos.Columns[e.ColumnIndex].Name == "colActions")
            {
                dgvProductos.Rows[e.RowIndex].Selected = true;

                Rectangle cellRect =
                    dgvProductos.GetCellDisplayRectangle(
                        e.ColumnIndex,
                        e.RowIndex,
                        true);

                Point location = dgvProductos.PointToScreen(
                    new Point(cellRect.Left, cellRect.Bottom));

                cmsOpciones.Show(location);
            }
        }

        public void CargarTabla()
        {
            try
            {
                DataTable dt = _productoLN.Listar();
                dgvProductos.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar productos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            using (ucProductoFrm frm = new ucProductoFrm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _productoLN.Guardar(frm.ProductoActual);
                        CargarTabla();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            "Advertencia",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private int? ObtenerIdSeleccionado()
        {
            if (dgvProductos.CurrentRow != null)
            {
                object valor =
                    dgvProductos.CurrentRow.Cells["colId"].Value;

                if (valor != null && valor != DBNull.Value)
                {
                    return Convert.ToInt32(valor);
                }
            }

            return null;
        }

        private void ItemEditar_Click(object sender, EventArgs e)
        {
            int? idProducto = ObtenerIdSeleccionado();

            if (idProducto.HasValue)
            {
                using (ucProductoFrm frm =
                       new ucProductoFrm(idProducto.Value))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            _productoLN.Guardar(frm.ProductoActual);
                            CargarTabla();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(
                                ex.Message,
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void ItemEliminar_Click(object sender, EventArgs e)
        {
            int? idProducto = ObtenerIdSeleccionado();

            if (idProducto.HasValue)
            {
                DialogResult confirmacion =
                    MessageBox.Show(
                        "¿Está seguro de desactivar este producto?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        _productoLN.EliminarLogico(
                            (short)idProducto.Value);

                        CargarTabla();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BuscarProductos()
        {
            string criterio = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(criterio))
            {
                CargarTabla();
                return;
            }

            var resultados =
                _productoLN.Buscar(criterio, null);

            DataTable dt = new DataTable();

            dt.Columns.Add("IdProductoPK", typeof(short));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("PrecioVenta", typeof(decimal));
            dt.Columns.Add("IdCategoriaFK", typeof(short));
            dt.Columns.Add("Estado", typeof(bool));

            foreach (var prod in resultados)
            {
                dt.Rows.Add(
                    prod.IdProductoPK,
                    prod.Nombre,
                    prod.PrecioVenta ?? 0,
                    prod.IdCategoriaFK ?? (object)DBNull.Value,
                    prod.Estado ?? false
                );
            }

            dgvProductos.DataSource = dt;
        }
    }
}