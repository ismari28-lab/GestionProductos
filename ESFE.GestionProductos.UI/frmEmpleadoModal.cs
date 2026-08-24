using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN;
using System.Linq;
using System.Collections.Generic;

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
            // Cargo (hardcodeado por ahora)
            cmbCargo.Items.Clear();
            cmbCargo.Items.Add("1 - Gerente");
            cmbCargo.Items.Add("2 - Vendedor");
            cmbCargo.Items.Add("3 - Bodeguero");

            // Usuarios
            try
            {
                var userLN = new UserLN();
                var usuarios = userLN.ObtenerActivos();

                // Si estamos editando y el usuario asignado NO está en los activos,
                // lo agregamos para que se pueda seleccionar (aunque esté inactivo)
                if (EmpleadoActual?.IdUsuarioFK.HasValue == true)
                {
                    short idAsignado = EmpleadoActual.IdUsuarioFK.Value;

                    if (!usuarios.Any(u => u.IdUsuarioPK == idAsignado))
                    {
                        var asignado = userLN.Buscar(null, idAsignado).FirstOrDefault();
                        if (asignado != null)
                            usuarios.Insert(0, asignado);
                    }
                }

                cmbUsuario.DataSource = null;
                cmbUsuario.DisplayMember = "NombreConRol";
                cmbUsuario.ValueMember = "IdUsuarioPK";
                cmbUsuario.DataSource = usuarios;
                cmbUsuario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los usuarios: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                    var usuarios = cmbUsuario.DataSource as List<Usuario>;
                    if (usuarios != null)
                    {
                        int index = usuarios.FindIndex(u => u.IdUsuarioPK == EmpleadoActual.IdUsuarioFK.Value);
                        if (index >= 0)
                            cmbUsuario.SelectedIndex = index;
                    }
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