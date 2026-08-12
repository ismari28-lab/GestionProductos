using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ESFE.GestionProductos.EN;
using System.Linq;

namespace ESFE.GestionProductos.UI
{
    public partial class ucEmpleado : UserControl
    {
        private List<Empleado> listaEmpleadosMemoria = new List<Empleado>();
        // Paginación
        private const int TAMANIO_PAGINA = 8;
        private int paginaActual = 1;
        private int totalPaginas = 1;
        public ucEmpleado()
        {
            InitializeComponent();

            EstilizarGrid();
            ConfigurarPaginacion();


            btnCrear.Click += BtnCrear_Click;
            btnBuscar.Click += (s, e) => LlenarGrid();

            dgvEmpleados.CellClick += DgvEmpleados_CellClick;
            dgvEmpleados.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditarSeleccionado(); };
            dgvEmpleados.MouseDown += DgvEmpleados_MouseDown;

            itemEditar.Click += (s, e) => EditarSeleccionado();
            itemEliminar.Click += (s, e) => EliminarSeleccionado();

            cboFiltro.SelectedIndex = 0;

            CargarDatosPrueba();
            LlenarGrid();
        }

        
        

        private void EstilizarGrid()
        {
            dgvEmpleados.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvEmpleados.DefaultCellStyle.ForeColor = Color.FromArgb(60, 70, 85);
            dgvEmpleados.DefaultCellStyle.BackColor = Color.White;
            dgvEmpleados.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgvEmpleados.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgvEmpleados.DefaultCellStyle.SelectionForeColor = Color.FromArgb(60, 70, 85);

            dgvEmpleados.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvEmpleados.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(120, 130, 145);
            dgvEmpleados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvEmpleados.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgvEmpleados.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvEmpleados.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvEmpleados.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(120, 130, 145);

            foreach (DataGridViewColumn col in dgvEmpleados.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            colActions.DefaultCellStyle.ForeColor = Color.FromArgb(90, 70, 180);
            colActions.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colActions.DefaultCellStyle.SelectionForeColor = Color.FromArgb(90, 70, 180);
            colActions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);

            int filaHoverActual = -1;

            dgvEmpleados.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                if (e.ColumnIndex == colActions.Index)
                {
                    dgvEmpleados.Cursor = Cursors.Hand;
                    dgvEmpleados.Rows[e.RowIndex].Cells[colActions.Index].Style.BackColor =
                        Color.FromArgb(240, 240, 250);
                }

                if (filaHoverActual != e.RowIndex)
                {
                    filaHoverActual = e.RowIndex;
                    dgvEmpleados.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        Color.FromArgb(250, 251, 253);
                }
            };

            dgvEmpleados.CellMouseLeave += (s, e) =>
            {
                dgvEmpleados.Cursor = Cursors.Default;
                if (e.RowIndex >= 0)
                {
                    dgvEmpleados.Rows[e.RowIndex].Cells[colActions.Index].Style.BackColor =
                        Color.White;
                }
            };

            dgvEmpleados.RowLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgvEmpleados.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                filaHoverActual = -1;
            };
        }

        private void CargarDatosPrueba()
        {
            listaEmpleadosMemoria = new List<Empleado>
            {
                new Empleado { IdEmpleadoPK = 1, Nombre = "Juan Pérez",    Telefono = "7777-8888", Cargo = 1, IdUsuarioFK = 1,    Estado = true  },
                new Empleado { IdEmpleadoPK = 2, Nombre = "María López",   Telefono = "2222-3333", Cargo = 2, IdUsuarioFK = null, Estado = true  },
                new Empleado { IdEmpleadoPK = 3, Nombre = "Carlos Gómez",  Telefono = "7123-4567", Cargo = 3, IdUsuarioFK = 2,    Estado = false }
            };
        }

        private void LlenarGrid()
        {
            dgvEmpleados.Rows.Clear();

            string filtro = txtBuscar.Text?.Trim().ToLower() ?? "";
            string estado = cboFiltro.SelectedItem?.ToString() ?? "Todos";

            // Filtrar la lista completa primero
            var filtrados = new List<Empleado>();
            foreach (var emp in listaEmpleadosMemoria)
            {
                if (!string.IsNullOrEmpty(filtro))
                {
                    bool coincide = (emp.Nombre ?? "").ToLower().Contains(filtro)
                                 || (emp.Telefono ?? "").ToLower().Contains(filtro);
                    if (!coincide) continue;
                }
                if (estado == "Activos" && emp.Estado != true) continue;
                if (estado == "Inactivos" && emp.Estado == true) continue;

                filtrados.Add(emp);
            }

            // Calcular total de páginas
            totalPaginas = (int)Math.Ceiling((double)filtrados.Count / TAMANIO_PAGINA);
            if (totalPaginas == 0) totalPaginas = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;

            // Obtener solo los registros de la página actual
            var paginados = filtrados
                .Skip((paginaActual - 1) * TAMANIO_PAGINA)
                .Take(TAMANIO_PAGINA);

            foreach (var emp in paginados)
            {
                int rowIndex = dgvEmpleados.Rows.Add(
                    emp.IdEmpleadoPK,
                    emp.Nombre ?? "",
                    emp.Telefono ?? "",
                    ObtenerNombreCargo(emp.Cargo),
                    emp.IdUsuarioFK.HasValue ? $"User #{emp.IdUsuarioFK}" : "Sin Asignar",
                    emp.Estado == true ? "Activo" : "Inactivo",
                    "Editar   Eliminar"
                );

                dgvEmpleados.Rows[rowIndex].Tag = emp;

                var celdaEstado = dgvEmpleados.Rows[rowIndex].Cells["colEstado"];
                celdaEstado.Style.ForeColor = emp.Estado == true
                    ? Color.FromArgb(40, 167, 69)
                    : Color.FromArgb(220, 53, 69);
                celdaEstado.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                celdaEstado.Style.SelectionForeColor = celdaEstado.Style.ForeColor;
            }

            dgvEmpleados.ClearSelection();
            ActualizarBotonesPaginacion();
        }

        private string ObtenerNombreCargo(short? cargo)
        {
            return cargo switch
            {
                1 => "Administrador",
                2 => "Vendedor",
                3 => "Bodeguero",
                4 => "Gerente",
                _ => "Sin Cargo"
            };
        }

        private void DgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var celda = dgvEmpleados.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var pos = dgvEmpleados.PointToClient(Cursor.Position);
            int xRelativo = pos.X - celda.Left;

            if (xRelativo > celda.Width * 0.6)
                EliminarSeleccionado();
            else
                EditarSeleccionado();
        }

        private Empleado ObtenerEmpleadoSeleccionado()
        {
            if (dgvEmpleados.SelectedRows.Count == 0 && dgvEmpleados.CurrentRow == null)
                return null;
            var fila = dgvEmpleados.CurrentRow ?? dgvEmpleados.SelectedRows[0];
            return fila?.Tag as Empleado;
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            using (var modal = new frmEmpleadoModal())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    Empleado nuevo = modal.EmpleadoActual;
                    nuevo.IdEmpleadoPK = (short)(listaEmpleadosMemoria.Count + 1);

                    listaEmpleadosMemoria.Add(nuevo);
                    LlenarGrid();
                }
            }
        }

        private void EditarSeleccionado()
        {
            var empleado = ObtenerEmpleadoSeleccionado();
            if (empleado == null)
            {
                MessageBox.Show("Selecciona un empleado de la lista para editar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var modal = new frmEmpleadoModal(empleado))
            {
                if (modal.ShowDialog() == DialogResult.OK)
                    LlenarGrid();
            }
        }

        private void EliminarSeleccionado()
        {
            var empleado = ObtenerEmpleadoSeleccionado();
            if (empleado == null)
            {
                MessageBox.Show("Selecciona un empleado de la lista para eliminar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var conf = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar al empleado '{empleado.Nombre}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (conf == DialogResult.Yes)
            {
                listaEmpleadosMemoria.Remove(empleado);
                LlenarGrid();
            }
        }

        private void DgvEmpleados_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = dgvEmpleados.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0)
            {
                dgvEmpleados.ClearSelection();
                dgvEmpleados.Rows[hit.RowIndex].Selected = true;
                dgvEmpleados.CurrentCell = dgvEmpleados.Rows[hit.RowIndex].Cells[0];
                cmsOpciones.Show(dgvEmpleados, e.Location);
            }
        }


        private void ConfigurarPaginacion()
        {
            foreach (var lbl in new[] { lblPag1, lblPag2, lblPag3, lblPagFinal })
            {
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", 9F);
                lbl.Padding = new Padding(8, 6, 8, 6);
                lbl.Margin = new Padding(3);
                lbl.Cursor = Cursors.Hand;
                lbl.ForeColor = Color.FromArgb(80, 90, 100);
                lbl.Click += LblPagina_Click;
            }
            lblPuntos.AutoSize = true;
            lblPuntos.Font = new Font("Segoe UI", 10F);
            lblPuntos.ForeColor = Color.FromArgb(120, 130, 145);
            lblPuntos.Padding = new Padding(6, 6, 6, 6);
            lblPuntos.Text = "...";
        }

        private void LblPagina_Click(object sender, EventArgs e)
        {
            if (sender is Label lbl && int.TryParse(lbl.Text, out int pag))
            {
                paginaActual = pag;
                LlenarGrid();
            }
        }

        private void ActualizarBotonesPaginacion()
        {
            // Ocultar todos
            lblPag1.Visible = lblPag2.Visible = lblPag3.Visible = false;
            lblPuntos.Visible = lblPagFinal.Visible = false;

            if (totalPaginas <= 0) return;

            // Caso simple: ≤4 páginas, mostrar 1..N
            if (totalPaginas <= 4)
            {
                var labels = new[] { lblPag1, lblPag2, lblPag3, lblPagFinal };
                for (int i = 0; i < totalPaginas; i++)
                {
                    labels[i].Text = (i + 1).ToString();
                    labels[i].Visible = true;
                    EstilizarLabelPagina(labels[i], (i + 1) == paginaActual);
                }
                return;
            }

            // Caso: mostrar 1 2 3 ... N
            lblPag1.Text = "1"; lblPag1.Visible = true; EstilizarLabelPagina(lblPag1, paginaActual == 1);

            if (paginaActual <= 3)
            {
                lblPag2.Text = "2"; lblPag2.Visible = true; EstilizarLabelPagina(lblPag2, paginaActual == 2);
                lblPag3.Text = "3"; lblPag3.Visible = true; EstilizarLabelPagina(lblPag3, paginaActual == 3);
            }
            else if (paginaActual >= totalPaginas - 2)
            {
                lblPag2.Text = (totalPaginas - 2).ToString(); lblPag2.Visible = true; EstilizarLabelPagina(lblPag2, paginaActual == totalPaginas - 2);
                lblPag3.Text = (totalPaginas - 1).ToString(); lblPag3.Visible = true; EstilizarLabelPagina(lblPag3, paginaActual == totalPaginas - 1);
            }
            else
            {
                lblPag2.Text = (paginaActual - 1).ToString(); lblPag2.Visible = true; EstilizarLabelPagina(lblPag2, false);
                lblPag3.Text = paginaActual.ToString(); lblPag3.Visible = true; EstilizarLabelPagina(lblPag3, true);
            }

            lblPuntos.Visible = true;
            lblPagFinal.Text = totalPaginas.ToString();
            lblPagFinal.Visible = true;
            EstilizarLabelPagina(lblPagFinal, paginaActual == totalPaginas);
        }

        private void EstilizarLabelPagina(Label lbl, bool activo)
        {
            if (activo)
            {
                lbl.BackColor = Color.FromArgb(90, 70, 180);
                lbl.ForeColor = Color.White;
                lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
            else
            {
                lbl.BackColor = Color.Transparent;
                lbl.ForeColor = Color.FromArgb(80, 90, 100);
                lbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
        }



    }
}