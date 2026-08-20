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

            colId.DataPropertyName = "ID Producto";
            colNombre.DataPropertyName = "Nombre Completo";
            colCodigo.DataPropertyName = "Código";
            colPrecio.DataPropertyName = "Precio";
            colStock.DataPropertyName = "Stock";
            colCategoria.DataPropertyName = "Categoría";
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

        private void DgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.Columns[e.ColumnIndex].Name == "colActions")
            {
                e.Value = "⋮ Opciones";
            }
        }

        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProductos.Columns[e.ColumnIndex].Name == "colActions")
            {
                dgvProductos.Rows[e.RowIndex].Selected = true;
                Rectangle cellRect = dgvProductos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point location = dgvProductos.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));
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
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            using (frmProductoModal frm = new frmProductoModal("Crear Producto"))
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
                        MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private int? ObtenerIdSeleccionado()
        {
            if (dgvProductos.CurrentRow != null && dgvProductos.CurrentRow.Cells["colId"].Value != DBNull.Value)
            {
                return Convert.ToInt32(dgvProductos.CurrentRow.Cells["colId"].Value);
            }
            return null;
        }

        private void ItemEditar_Click(object sender, EventArgs e)
        {
            int? idProducto = ObtenerIdSeleccionado();
            if (idProducto.HasValue)
            {
                var lista = _productoLN.Buscar(null, idProducto.Value);
                if (lista.Count > 0)
                {
                    Producto productoAEditar = lista[0];
                    using (frmProductoModal frm = new frmProductoModal("Editar Producto", productoAEditar))
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
                                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
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
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de desactivar este producto?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        _productoLN.EliminarLogico(idProducto.Value);
                        CargarTabla();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            else
            {
                var resultados = _productoLN.Buscar(criterio, null);

                DataTable dt = new DataTable();
                dt.Columns.Add("ID Producto", typeof(int));
                dt.Columns.Add("Nombre Completo", typeof(string));
                dt.Columns.Add("Código", typeof(string));
                dt.Columns.Add("Precio", typeof(decimal));
                dt.Columns.Add("Stock", typeof(int));
                dt.Columns.Add("Categoría", typeof(object));
                dt.Columns.Add("Estado", typeof(object));

                foreach (var prod in resultados)
                {
                    dt.Rows.Add(
                        prod.IdProductoPK,
                        prod.Nombre,
                        prod.Codigo,
                        prod.Precio,
                        prod.Stock,
                        (object)prod.IdCategoriaFK ?? DBNull.Value,
                        prod.Estado.HasValue ? (prod.Estado.Value ? "Activo" : "Inactivo") : "Inactivo"
                    );
                }

                dgvProductos.DataSource = dt;
            }
        }
    }
}