using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.UI
{
    // El nombre debe ser 'login' en minúsculas para coincidir con login.Designer.cs
    public partial class login : MaterialForm
    {
        public login() // El constructor debe ser igual: login()
        {
            InitializeComponent(); // ¡Ahora sí lo reconoce perfectamente!

            // Configuración del tema de MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700,
                Primary.Blue200, Accent.Orange700,
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

                // Llamada limpia sin el rol
                Usuario? usuarioAutenticado = UsuarioDAL.ValidarLogin(nombre, password);

                if (usuarioAutenticado != null)
                {
                    MessageBox.Show($"¡Bienvenido, {usuarioAutenticado.Nombre}!", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Aquí puedes usar usuarioAutenticado.Id_RolFK para saber qué permisos o qué menú mostrar.

                    this.Hide();
                    frmMain pantallaPrincipal = new frmMain();
                    pantallaPrincipal.Show();
                    // FormPrincipal principal = new FormPrincipal(usuarioAutenticado);
                    // principal.Show();
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
    }
}