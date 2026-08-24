using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN;

namespace ESFE.GestionProductos.UI
{
    public partial class frmUsuarioModal : MaterialForm
    {
        // Oculta la propiedad del diseñador y previene la advertencia WFO1000
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Usuario UsuarioActual { get; set; }

        public frmUsuarioModal() : this("Crear Usuario", new Usuario())
        {
        }

        public frmUsuarioModal(string titulo) : this(titulo, new Usuario())
        {
        }

        public frmUsuarioModal(Usuario usuario) : this("Editar Usuario", usuario)
        {
        }

        public frmUsuarioModal(string titulo, Usuario usuario)
        {
            InitializeComponent();
            this.Text = titulo;
            UsuarioActual = usuario ?? new Usuario();

            AplicarEstilos();

            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            btnGuardar.Click += BtnGuardar_Click;

            this.Load += (s, e) =>
            {
                CargarCombos();
                CargarDatosEnControles();
            };
        }

        private void CargarCombos()
        {
            try
            {
                var roles = new RolLN().Listar();

                cmbRol.DataSource = null;
                cmbRol.DisplayMember = "NombreRol";
                cmbRol.ValueMember = "IdRolPK";
                cmbRol.DataSource = roles;
                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los roles: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AplicarEstilos()
        {
            Font fuenteBotones = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancelar.Font = fuenteBotones;
            btnGuardar.Font = fuenteBotones;

            Font fuenteInputs = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point);
            txtNombre.Font = fuenteInputs;
            txtPassword.Font = fuenteInputs;
            chkEstado.Font = fuenteInputs;

            btnCancelar.Cursor = Cursors.Hand;
            btnGuardar.Cursor = Cursors.Hand;
        }

        private void CargarDatosEnControles()
        {
            if (UsuarioActual != null)
            {
                txtNombre.Text = UsuarioActual.Nombre ?? string.Empty;
                txtPassword.Text = UsuarioActual.Password ?? string.Empty;

                // Carga de rol si aplica
                if (UsuarioActual.Id_RolFK.HasValue)
                {
                    cmbRol.SelectedValue = UsuarioActual.Id_RolFK.Value;
                }

                // Asignación segura del estado
                chkEstado.Checked = UsuarioActual.Estado.GetValueOrDefault(true);
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (UsuarioActual == null)
            {
                UsuarioActual = new Usuario();
            }

            // Validaciones básicas de entrada
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de usuario es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            UsuarioActual.Nombre = txtNombre.Text.Trim();
            UsuarioActual.Password = txtPassword.Text.Trim();

            // Mapeo seguro del ComboBox de Rol (short?)
            if (cmbRol.SelectedValue != null && short.TryParse(cmbRol.SelectedValue.ToString(), out short idRol))
            {
                UsuarioActual.Id_RolFK = idRol;
            }
            else
            {
                UsuarioActual.Id_RolFK = null;
            }

            UsuarioActual.Estado = chkEstado.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}