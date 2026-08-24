using System;
using System.Windows.Forms;

namespace ESFE.GestionProductos.UI
{
    public partial class uCRecuperarContraseña : UserControl
    {
        public uCRecuperarContraseña()
        {
            InitializeComponent();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show(
                    "Ingrese su correo electrónico.",
                    "Recuperar contraseña",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtCorreo.Focus();
                return;
            }

            MessageBox.Show(
                "Correo ingresado correctamente.",
                "Recuperar contraseña",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}