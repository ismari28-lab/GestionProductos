using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.UI
{
    public partial class ucEmpleado : UserControl
    {
        private List<Empleado> listaEmpleadosMemoria = new List<Empleado>();

        public ucEmpleado()
        {
            InitializeComponent();

            // Fuente a 20pt para la lista
            materialListView1.Font = new Font("Roboto", 20F, FontStyle.Regular, GraphicsUnit.Point);

            // Incremento de fuente de los botones para llenar su nuevo tamaño vertical
            Font fuenteBotones = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnCrear.Font = fuenteBotones;
            btnBuscar.Font = fuenteBotones;
            btnEditar.Font = fuenteBotones;
            btnEliminar.Font = fuenteBotones;

            // Cursors de tipo Hand
            btnCrear.Cursor = Cursors.Hand;
            btnBuscar.Cursor = Cursors.Hand;
            btnEditar.Cursor = Cursors.Hand;
            btnEliminar.Cursor = Cursors.Hand;

            // Eventos
            btnCrear.Click += BtnCrear_Click;
            btnEditar.Click += (s, e) => EditarSeleccionado();
            btnEliminar.Click += (s, e) => EliminarSeleccionado();

            materialListView1.DoubleClick += (s, e) => EditarSeleccionado();
            materialListView1.MouseDown += MaterialListView1_MouseDown;

            itemEditar.Click += (s, e) => EditarSeleccionado();
            itemEliminar.Click += (s, e) => EliminarSeleccionado();

            CargarDatosPrueba();
            LlenarListView();
        }

        private void CargarDatosPrueba()
        {
            listaEmpleadosMemoria = new List<Empleado>
            {
                new Empleado { IdEmpleadoPK = 1, Nombre = "Juan Pérez", Telefono = "7777-8888", Cargo = 1, IdUsuarioFK = 1, Estado = true },
                new Empleado { IdEmpleadoPK = 2, Nombre = "María López", Telefono = "2222-3333", Cargo = 2, IdUsuarioFK = null, Estado = true },
                new Empleado { IdEmpleadoPK = 3, Nombre = "Carlos Gómez", Telefono = "7123-4567", Cargo = 3, IdUsuarioFK = 2, Estado = false }
            };
        }

        private void LlenarListView()
        {
            materialListView1.Items.Clear();

            foreach (var emp in listaEmpleadosMemoria)
            {
                var item = new ListViewItem(emp.IdEmpleadoPK.ToString());
                item.SubItems.Add(emp.Nombre ?? "");
                item.SubItems.Add(emp.Telefono ?? "");
                item.SubItems.Add(ObtenerNombreCargo(emp.Cargo));
                item.SubItems.Add(emp.IdUsuarioFK.HasValue ? $"User #{emp.IdUsuarioFK}" : "Sin Asignar");
                item.SubItems.Add(emp.Estado == true ? "Activo" : "Inactivo");

                item.Tag = emp;
                materialListView1.Items.Add(item);
            }
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

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            using (var modal = new frmEmpleadoModal())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    Empleado nuevo = modal.EmpleadoActual;
                    nuevo.IdEmpleadoPK = (short)(listaEmpleadosMemoria.Count + 1);

                    listaEmpleadosMemoria.Add(nuevo);
                    LlenarListView();
                }
            }
        }

        private void EditarSeleccionado()
        {
            if (materialListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un empleado de la lista para editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var itemSeleccionado = materialListView1.SelectedItems[0];
            var empleadoEditar = (Empleado)itemSeleccionado.Tag;

            using (var modal = new frmEmpleadoModal(empleadoEditar))
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    LlenarListView();
                }
            }
        }

        private void EliminarSeleccionado()
        {
            if (materialListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un empleado de la lista para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var itemSeleccionado = materialListView1.SelectedItems[0];
            var empleado = (Empleado)itemSeleccionado.Tag;

            var confirmacion = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar al empleado '{empleado.Nombre}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                listaEmpleadosMemoria.Remove(empleado);
                LlenarListView();
            }
        }

        private void MaterialListView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = materialListView1.HitTest(e.Location);
                if (hitTest.Item != null)
                {
                    hitTest.Item.Selected = true;
                    cmsOpciones.Show(materialListView1, e.Location);
                }
            }
        }
    }
}