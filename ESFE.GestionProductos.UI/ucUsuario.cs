using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.UI
{
    public partial class ucUsuario : UserControl
    {
        private List<Usuario> listaUsuariosMemoria = new List<Usuario>();

        // Paginación
        private const int TAMANIO_PAGINA = 8;
        private int paginaActual = 1;
        private int totalPaginas = 1;

        public ucUsuario()
        {
            InitializeComponent();

            EstilizarGrid();
            ConfigurarPaginacion();

            btnCrear.Click += BtnCrear_Click;
            btnBuscar.Click += (s, e) => { paginaActual = 1; LlenarGrid(); };

            dgvUsuarios.CellClick += DgvUsuarios_CellClick;
            dgvUsuarios.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditarSeleccionado(); };
            dgvUsuarios.MouseDown += DgvUsuarios_MouseDown;

            itemEditar.Click += (s, e) => EditarSeleccionado();
            itemEliminar.Click += (s, e) => EliminarSeleccionado();

            cboFiltro.SelectedIndex = 0;

            CargarDatosPrueba();
            LlenarGrid();
        }

        private void EstilizarGrid()
        {
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.FromArgb(60, 70, 85);
            dgvUsuarios.DefaultCellStyle.BackColor = Color.White;
            dgvUsuarios.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.FromArgb(60, 70, 85);

            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(120, 130, 145);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(120, 130, 145);

            foreach (DataGridViewColumn col in dgvUsuarios.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            colActions.DefaultCellStyle.ForeColor = Color.FromArgb(90, 70, 180);
            colActions.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colActions.DefaultCellStyle.SelectionForeColor = Color.FromArgb(90, 70, 180);
            colActions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);

            int filaHoverActual = -1;

            dgvUsuarios.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                if (e.ColumnIndex == colActions.Index)
                {
                    dgvUsuarios.Cursor = Cursors.Hand;
                    dgvUsuarios.Rows[e.RowIndex].Cells[colActions.Index].Style.BackColor =
                        Color.FromArgb(240, 240, 250);
                }

                if (filaHoverActual != e.RowIndex)
                {
                    filaHoverActual = e.RowIndex;
                    dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        Color.FromArgb(250, 251, 253);
                }
            };

            dgvUsuarios.CellMouseLeave += (s, e) =>
            {
                dgvUsuarios.Cursor = Cursors.Default;
                if (e.RowIndex >= 0)
                    dgvUsuarios.Rows[e.RowIndex].Cells[colActions.Index].Style.BackColor = Color.White;
            };

            dgvUsuarios.RowLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                filaHoverActual = -1;
            };
        }

        private void CargarDatosPrueba()
        {
            listaUsuariosMemoria = new List<Usuario>
            {
                new Usuario { IdUsuarioPK = 1, Nombre = "admin",  Password = "xxx", Id_RolFK = 1, Estado = true  },
                new Usuario { IdUsuarioPK = 2, Nombre = "jperez", Password = "xxx", Id_RolFK = 2, Estado = true  },
                new Usuario { IdUsuarioPK = 3, Nombre = "mlopez", Password = "xxx", Id_RolFK = 3, Estado = false },
            };
        }

        private string ObtenerNombreRol(short? rol)
        {
            return rol switch
            {
                1 => "Administrador",
                2 => "Vendedor",
                3 => "Bodeguero",
                4 => "Gerente",
                _ => "Sin Rol"
            };
        }

        private void LlenarGrid()
        {
            dgvUsuarios.Rows.Clear();

            string filtro = txtBuscar.Text?.Trim().ToLower() ?? "";
            string estado = cboFiltro.SelectedItem?.ToString() ?? "Todos";

            var filtrados = new List<Usuario>();
            foreach (var user in listaUsuariosMemoria)
            {
                if (!string.IsNullOrEmpty(filtro))
                {
                    bool coincide = (user.Nombre ?? "").ToLower().Contains(filtro);
                    if (!coincide) continue;
                }
                if (estado == "Activos" && user.Estado != true) continue;
                if (estado == "Inactivos" && user.Estado == true) continue;

                filtrados.Add(user);
            }

            totalPaginas = (int)Math.Ceiling((double)filtrados.Count / TAMANIO_PAGINA);
            if (totalPaginas == 0) totalPaginas = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;

            var paginados = filtrados
                .Skip((paginaActual - 1) * TAMANIO_PAGINA)
                .Take(TAMANIO_PAGINA);

            foreach (var user in paginados)
            {
                int rowIndex = dgvUsuarios.Rows.Add(
                    user.IdUsuarioPK,
                    user.Nombre ?? "",
                    ObtenerNombreRol(user.Id_RolFK),
                    user.Estado == true ? "Activo" : "Inactivo",
                    "Editar   Eliminar"
                );

                dgvUsuarios.Rows[rowIndex].Tag = user;

                var celdaEstado = dgvUsuarios.Rows[rowIndex].Cells["colEstado"];
                celdaEstado.Style.ForeColor = user.Estado == true
                    ? Color.FromArgb(40, 167, 69)
                    : Color.FromArgb(220, 53, 69);
                celdaEstado.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                celdaEstado.Style.SelectionForeColor = celdaEstado.Style.ForeColor;
            }

            dgvUsuarios.ClearSelection();
            ActualizarBotonesPaginacion();
        }

        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colActions.Index) return;

            var celda = dgvUsuarios.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var pos = dgvUsuarios.PointToClient(Cursor.Position);
            int xRelativo = pos.X - celda.Left;

            if (xRelativo > celda.Width * 0.6)
                EliminarSeleccionado();
            else
                EditarSeleccionado();
        }

        private Usuario ObtenerUsuarioSeleccionado()
        {
            if (dgvUsuarios.SelectedRows.Count == 0 && dgvUsuarios.CurrentRow == null)
                return null;
            var fila = dgvUsuarios.CurrentRow ?? dgvUsuarios.SelectedRows[0];
            return fila?.Tag as Usuario;
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            // TODO: reemplazar por tu frmUsuarioModal cuando lo tengas
            MessageBox.Show("Aquí se abriría el modal para crear un nuevo usuario.",
                "Crear Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Ejemplo cuando tengas el modal:
            // using (var modal = new frmUsuarioModal())
            // {
            //     if (modal.ShowDialog() == DialogResult.OK)
            //     {
            //         Usuario nuevo = modal.UsuarioActual;
            //         nuevo.IdUsuarioPK = listaUsuariosMemoria.Count + 1;
            //         listaUsuariosMemoria.Add(nuevo);
            //         LlenarGrid();
            //     }
            // }
        }

        private void EditarSeleccionado()
        {
            var user = ObtenerUsuarioSeleccionado();
            if (user == null)
            {
                MessageBox.Show("Selecciona un usuario de la lista para editar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show($"Aquí se abriría el modal para editar a: {user.Nombre}",
                "Editar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EliminarSeleccionado()
        {
            var user = ObtenerUsuarioSeleccionado();
            if (user == null)
            {
                MessageBox.Show("Selecciona un usuario de la lista para eliminar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var conf = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar al usuario '{user.Nombre}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (conf == DialogResult.Yes)
            {
                listaUsuariosMemoria.Remove(user);
                LlenarGrid();
            }
        }

        private void DgvUsuarios_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = dgvUsuarios.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0)
            {
                dgvUsuarios.ClearSelection();
                dgvUsuarios.Rows[hit.RowIndex].Selected = true;
                dgvUsuarios.CurrentCell = dgvUsuarios.Rows[hit.RowIndex].Cells[0];
                cmsOpciones.Show(dgvUsuarios, e.Location);
            }
        }

        // ==================== PAGINACIÓN ====================

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
            lblPag1.Visible = lblPag2.Visible = lblPag3.Visible = false;
            lblPuntos.Visible = lblPagFinal.Visible = false;

            if (totalPaginas <= 0) return;

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