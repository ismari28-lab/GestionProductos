using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN; // Importamos la LN

namespace ESFE.GestionProductos.UI
{
    public partial class ucEmpleado : UserControl
    {
        // Instanciamos la LN en lugar de la DAL
        private readonly EmpleadoLN _empleadoLN = new EmpleadoLN();

        public ucEmpleado()
        {
            InitializeComponent();
            ConfigurarColumnas();
            VincularEventos();
        }

        private void ConfigurarColumnas()
        {
            dgvEmpleados.AutoGenerateColumns = false;

            colId.DataPropertyName = "ID Empleado";
            colNombre.DataPropertyName = "Nombre Completo";
            colTelefono.DataPropertyName = "Teléfono";
            colCargo.DataPropertyName = "Código de Cargo";
            colUsuario.DataPropertyName = "Usuario de Sistema";
            colEstado.DataPropertyName = "Estado";

            dgvEmpleados.ContextMenuStrip = cmsOpciones;
        }

        private void VincularEventos()
        {
            this.Load += (s, e) => CargarTabla();

            btnCrear.Click += BtnCrear_Click;
            btnBuscar.Click += (s, e) => BuscarEmpleados();
            txtBuscar.TextChanged += (s, e) => BuscarEmpleados();

            itemEditar.Click += ItemEditar_Click;
            itemEliminar.Click += ItemEliminar_Click;

            dgvEmpleados.CellClick += DgvEmpleados_CellClick;
            dgvEmpleados.CellFormatting += DgvEmpleados_CellFormatting;
        }

        private void DgvEmpleados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEmpleados.Columns[e.ColumnIndex].Name == "colActions")
            {
                e.Value = "⋮ Opciones";
            }
        }

        private void DgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvEmpleados.Columns[e.ColumnIndex].Name == "colActions")
            {
                dgvEmpleados.Rows[e.RowIndex].Selected = true;
                Rectangle cellRect = dgvEmpleados.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point location = dgvEmpleados.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));
                cmsOpciones.Show(location);
            }
        }

        public void CargarTabla()
        {
            try
            {
                // Usamos LN
                DataTable dt = _empleadoLN.Listar();
                dgvEmpleados.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            using (frmEmpleadoModal frm = new frmEmpleadoModal("Crear Empleado"))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Usamos el método unificado Guardar de la LN
                        _empleadoLN.Guardar(frm.EmpleadoActual);
                        CargarTabla();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private short? ObtenerIdSeleccionado()
        {
            if (dgvEmpleados.CurrentRow != null && dgvEmpleados.CurrentRow.Cells["colId"].Value != DBNull.Value)
            {
                return Convert.ToInt16(dgvEmpleados.CurrentRow.Cells["colId"].Value);
            }
            return null;
        }

        private void ItemEditar_Click(object sender, EventArgs e)
        {
            short? idEmpleado = ObtenerIdSeleccionado();
            if (idEmpleado.HasValue)
            {
                // Usamos LN
                var lista = _empleadoLN.Buscar(null, idEmpleado.Value);
                if (lista.Count > 0)
                {
                    Empleado empleadoAEditar = lista[0];
                    using (frmEmpleadoModal frm = new frmEmpleadoModal("Editar Empleado", empleadoAEditar))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                // El método Guardar detecta que IdEmpleadoPK > 0 y llama a Actualizar
                                _empleadoLN.Guardar(frm.EmpleadoActual);
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
            short? idEmpleado = ObtenerIdSeleccionado();
            if (idEmpleado.HasValue)
            {
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de desactivar este empleado?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        // Usamos LN
                        _empleadoLN.EliminarLogico(idEmpleado.Value);
                        CargarTabla();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BuscarEmpleados()
        {
            string criterio = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(criterio))
            {
                CargarTabla();
            }
            else
            {
                // Usamos LN
                var resultados = _empleadoLN.Buscar(criterio, null);

                DataTable dt = new DataTable();
                dt.Columns.Add("ID Empleado", typeof(short));
                dt.Columns.Add("Nombre Completo", typeof(string));
                dt.Columns.Add("Teléfono", typeof(string));
                dt.Columns.Add("Código de Cargo", typeof(object));
                dt.Columns.Add("Usuario de Sistema", typeof(object));
                dt.Columns.Add("Estado", typeof(object));

                foreach (var emp in resultados)
                {
                    dt.Rows.Add(
                        emp.IdEmpleadoPK,
                        emp.Nombre,
                        emp.Telefono,
                        (object)emp.Cargo ?? DBNull.Value,
                        (object)emp.IdUsuarioFK ?? DBNull.Value,
                        emp.Estado.HasValue ? (emp.Estado.Value ? "Activo" : "Inactivo") : "Inactivo"
                    );
                }

                dgvEmpleados.DataSource = dt;
            }
        }
    }
}