using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.UI
{
    public partial class frmEmpleadoModal : MaterialForm
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Empleado EmpleadoActual { get; set; }

        public frmEmpleadoModal() : this("Crear Empleado", new Empleado())
        {
        }

        public frmEmpleadoModal(string titulo) : this(titulo, new Empleado())
        {
        }

        public frmEmpleadoModal(Empleado empleado) : this("Editar Empleado", empleado)
        {
        }

        public frmEmpleadoModal(string titulo, Empleado empleado)
        {
            InitializeComponent();
            this.Text = titulo;
            EmpleadoActual = empleado ?? new Empleado();

            AplicarEstilos();

            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            btnGuardar.Click += BtnGuardar_Click;

            this.Load += (s, e) =>
            {
                CargarCombos();
                CargarDatosEnControles();
            };
        }

        private void AplicarEstilos()
        {
            Font fuenteBotones = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancelar.Font = fuenteBotones;
            btnGuardar.Font = fuenteBotones;

            Font fuenteInputs = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point);
            txtNombre.Font = fuenteInputs;
            txtTelefono.Font = fuenteInputs;
            chkActivo.Font = fuenteInputs;

            btnCancelar.Cursor = Cursors.Hand;
            btnGuardar.Cursor = Cursors.Hand;
        }

        private void CargarCombos()
        {
            // TODO: Ajusta según cómo manejes la lista/catálogo de Cargos
            // Ejemplo con lista de prueba o enum/DAL de cargos:
            cmbCargo.Items.Clear();
            cmbCargo.Items.Add("1 - Gerente");
            cmbCargo.Items.Add("2 - Vendedor");
            cmbCargo.Items.Add("3 - Bodeguero");

            // TODO: Cargar catálogo de Usuarios disponibles (ej. desde UsuarioDAL)
            // cmbUsuario.DataSource = usuarioDAL.ObtenerUsuarios();
            // cmbUsuario.DisplayMember = "Nombre";
            // cmbUsuario.ValueMember = "IdUsuarioPK";
        }

        private void CargarDatosEnControles()
        {
            if (EmpleadoActual != null)
            {
                txtNombre.Text = EmpleadoActual.Nombre ?? string.Empty;
                txtTelefono.Text = EmpleadoActual.Telefono ?? string.Empty;
                chkActivo.Checked = EmpleadoActual.Estado.GetValueOrDefault(true);

                // Cargar Cargo seleccionado
                if (EmpleadoActual.Cargo.HasValue)
                {
                    // Si usas SelectedValue: cmbCargo.SelectedValue = EmpleadoActual.Cargo.Value;
                    // O selección de índice si coinciden los IDs:
                    if (EmpleadoActual.Cargo.Value > 0 && EmpleadoActual.Cargo.Value <= cmbCargo.Items.Count)
                    {
                        cmbCargo.SelectedIndex = EmpleadoActual.Cargo.Value - 1;
                    }
                }

                // Cargar Usuario seleccionado (si no es nulo)
                if (EmpleadoActual.IdUsuarioFK.HasValue)
                {
                    // cmbUsuario.SelectedValue = EmpleadoActual.IdUsuarioFK.Value;
                }
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (EmpleadoActual == null)
            {
                EmpleadoActual = new Empleado();
            }

            EmpleadoActual.Nombre = txtNombre.Text.Trim();
            EmpleadoActual.Telefono = txtTelefono.Text.Trim();
            EmpleadoActual.Estado = chkActivo.Checked;

            // Mapeo de Cargo seleccionado (short?)
            if (cmbCargo.SelectedIndex != -1)
            {
                EmpleadoActual.Cargo = (short)(cmbCargo.SelectedIndex + 1); // O parsear SelectedValue
            }
            else
            {
                EmpleadoActual.Cargo = null;
            }

            // Mapeo de Usuario seleccionado (short?)
            if (cmbUsuario.SelectedValue != null)
            {
                EmpleadoActual.IdUsuarioFK = Convert.ToInt16(cmbUsuario.SelectedValue);
            }
            else
            {
                EmpleadoActual.IdUsuarioFK = null;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}