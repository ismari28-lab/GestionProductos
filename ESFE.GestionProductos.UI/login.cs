using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.UI
{
    public partial class login : MaterialForm
    {
        public login()
{
    InitializeComponent();

    var materialSkinManager = MaterialSkinManager.Instance;
    materialSkinManager.AddFormToManage(this);
    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
    materialSkinManager.ColorScheme = new ColorScheme(
        Primary.Blue600, Primary.Blue400,
        Primary.Blue100, Accent.LightBlue200,
        TextShade.WHITE
    );
}

        private void login_Resize(object? sender, EventArgs e)
        {
            cardLogin.Left = (ClientSize.Width - cardLogin.Width) / 2;
            cardLogin.Top = (ClientSize.Height - cardLogin.Height) / 2;
        }

        private void login_Load(object? sender, EventArgs e)
        {
            cardLogin.Left = (ClientSize.Width - cardLogin.Width) / 2;
            cardLogin.Top = (ClientSize.Height - cardLogin.Height) / 2;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtUsuario.Text.Trim();
                string password = txtContraseña.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Usuario? usuarioAutenticado = UsuarioDAL.ValidarLogin(nombre, password);

                if (usuarioAutenticado != null)
                {
                    MessageBox.Show($"¡Bienvenido, {usuarioAutenticado.Nombre}!", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    frmMain pantallaPrincipal = new frmMain();
                    pantallaPrincipal.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar iniciar sesión: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecuperarContraseña_Click(object sender, EventArgs e)
        {
            var frmRecuperar = new MaterialForm
            {
                Text = "Recuperar contraseña",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(frmRecuperar);

            var uc = new uCRecuperarContraseña
            {
                Dock = DockStyle.Fill
            };

            frmRecuperar.ClientSize = uc.Size;
            frmRecuperar.Controls.Add(uc);

            frmRecuperar.ShowDialog(this);
        }
    }
}