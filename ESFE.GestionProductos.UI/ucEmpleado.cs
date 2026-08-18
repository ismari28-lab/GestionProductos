using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.UI
{
    public partial class ucEmpleado : UserControl
    {
        private readonly EmpleadoDAL _empleadoDAL = new EmpleadoDAL();

        public ucEmpleado()
        {
            InitializeComponent();
            ConfigurarColumnas();
            VincularEventos();
        }

        private void ConfigurarColumnas()
        {
            // 1. Evitar la creación automática de columnas duplicadas
            dgvEmpleados.AutoGenerateColumns = false;

            // 2. Mapear las columnas definidas en tu Designer con las columnas de la BD/SP
            colId.DataPropertyName = "ID Empleado";
            colNombre.DataPropertyName = "Nombre Completo";
            colTelefono.DataPropertyName = "Teléfono";
            colCargo.DataPropertyName = "Código de Cargo";
            colUsuario.DataPropertyName = "Usuario de Sistema";
            colEstado.DataPropertyName = "Estado";

            // 3. Asignar el ContextMenuStrip al DataGridView para clic derecho
            dgvEmpleados.ContextMenuStrip = cmsOpciones;
        }

        private void VincularEventos()
        {
            this.Load += (s, e) => CargarTabla();

            // Evento del botón Crear (usando el nombre 'btnCrear' del Designer)
            btnCrear.Click += BtnCrear_Click;

            // Eventos de búsqueda
            btnBuscar.Click += (s, e) => BuscarEmpleados();
            txtBuscar.TextChanged += (s, e) => BuscarEmpleados();

            // Eventos de los menú items del ContextMenuStrip
            itemEditar.Click += ItemEditar_Click;
            itemEliminar.Click += ItemEliminar_Click;

            // Mostrar el menú contextual al hacer clic en la columna Actions o formato de texto
            dgvEmpleados.CellClick += DgvEmpleados_CellClick;
            dgvEmpleados.CellFormatting += DgvEmpleados_CellFormatting;
        }

        private void DgvEmpleados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Colocar texto visible en la columna "Actions"
            if (e.RowIndex >= 0 && dgvEmpleados.Columns[e.ColumnIndex].Name == "colActions")
            {
                e.Value = "⋮ Opciones";
            }
        }

        private void DgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Abrir el menú Editar/Eliminar al dar clic sobre la columna colActions
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
                DataTable dt = _empleadoDAL.Listar();
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
                    _empleadoDAL.Insertar(frm.EmpleadoActual);
                    CargarTabla();
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
                var lista = _empleadoDAL.Buscar(null, idEmpleado.Value);
                if (lista.Count > 0)
                {
                    Empleado empleadoAEditar = lista[0];
                    using (frmEmpleadoModal frm = new frmEmpleadoModal("Editar Empleado", empleadoAEditar))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            _empleadoDAL.Actualizar(frm.EmpleadoActual);
                            CargarTabla();
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
                    _empleadoDAL.EliminarLogico(idEmpleado.Value);
                    CargarTabla();
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
                var resultados = _empleadoDAL.Buscar(criterio, null);

                // Creamos un DataTable con las mismas columnas que configuramos en ConfigurarColumnas()
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