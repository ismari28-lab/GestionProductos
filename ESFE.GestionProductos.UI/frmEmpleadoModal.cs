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
        // Oculta la propiedad del diseñador y previene la advertencia WFO1000
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

            this.Load += (s, e) => CargarDatosEnControles();
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

        private void CargarDatosEnControles()
        {
            if (EmpleadoActual != null)
            {
                txtNombre.Text = EmpleadoActual.Nombre ?? string.Empty;
                txtTelefono.Text = EmpleadoActual.Telefono ?? string.Empty;

                // Solución: Convierte el bool? a bool de forma segura (si es null, por defecto será true o false)
                chkActivo.Checked = EmpleadoActual.Estado.GetValueOrDefault(true);
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (EmpleadoActual == null)
            {
                EmpleadoActual = new Empleado();
            }

            EmpleadoActual.Nombre = txtNombre.Text.Trim();
            EmpleadoActual.Telefono = txtTelefono.Text.Trim();

            // Asignación directa del bool del checkbox al bool? de la entidad
            EmpleadoActual.Estado = chkActivo.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}