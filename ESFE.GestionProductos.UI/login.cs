using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.UI
{
    public partial class login : MaterialForm
    {
        // Indica si la contraseña está visible
        private bool contraseñaVisible = false;

        // Imagen que funcionará como botón del ojo
        private PictureBox picMostrarContraseña;

        public login()
        {
            InitializeComponent();

            // Crear el ojo
            CrearOjo();

            // Configuración de MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;

            materialSkinManager.AddFormToManage(this);

            materialSkinManager.Theme =
                MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600,
                Primary.Blue700,
                Primary.Blue200,
                Accent.Orange700,
                TextShade.WHITE
            );
        }

        // =========================================================
        // CREAR OJO
        // =========================================================
        private void CrearOjo()
        {
            picMostrarContraseña = new PictureBox();

            picMostrarContraseña.Name = "picMostrarContraseña";

            // Tamaño del ojo
            picMostrarContraseña.Size = new Size(30, 30);

            // Posición dentro del campo de contraseña
            picMostrarContraseña.Location = new Point(
                txtContraseña.Right - 35,
                txtContraseña.Top + 9
            );

            // Hacer que la imagen se adapte al tamaño
            picMostrarContraseña.SizeMode =
                PictureBoxSizeMode.Zoom;

            // Fondo transparente
            picMostrarContraseña.BackColor =
                Color.Transparent;

            // Mostrar inicialmente el ojo cerrado
            picMostrarContraseña.Image =
                CargarImagen("ojo_cerrado");

            // Cursor de mano
            picMostrarContraseña.Cursor =
                Cursors.Hand;

            // Agregar a la card
            cardLogin.Controls.Add(picMostrarContraseña);

            // Colocar encima del campo
            picMostrarContraseña.BringToFront();

            // Evento al hacer clic
            picMostrarContraseña.Click +=
                picMostrarContraseña_Click;
        }

        // =========================================================
        // CARGAR IMAGEN
        // =========================================================
        private Image CargarImagen(string nombre)
        {
            string ruta = Path.Combine(
                AppContext.BaseDirectory,
                "img",
                nombre + ".png"
            );

            if (!File.Exists(ruta))
            {
                MessageBox.Show(
                    "No se encontró la imagen:\n" + ruta,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return new Bitmap(1, 1);
            }

            return Image.FromFile(ruta);
        }

        // =========================================================
        // MOSTRAR / OCULTAR CONTRASEÑA
        // =========================================================
        private void picMostrarContraseña_Click(
            object? sender,
            EventArgs e)
        {
            contraseñaVisible = !contraseñaVisible;

            if (contraseñaVisible)
            {
                // Mostrar contraseña
                txtContraseña.PasswordChar = '\0';

                // Ojo abierto
                picMostrarContraseña.Image =
                    CargarImagen("ojo_abierto");
            }
            else
            {
                // Ocultar contraseña
                txtContraseña.PasswordChar = '*';

                // Ojo cerrado
                picMostrarContraseña.Image =
                    CargarImagen("ojo_cerrado");
            }
        }

        // =========================================================
        // CENTRAR CARD
        // =========================================================
        private void CentrarCard()
        {
            cardLogin.Left =
                (ClientSize.Width - cardLogin.Width) / 2;

            cardLogin.Top =
                (ClientSize.Height - cardLogin.Height) / 2;

            // Mantener el ojo en su posición
            if (picMostrarContraseña != null)
            {
                picMostrarContraseña.Location = new Point(
                    txtContraseña.Right - 35,
                    txtContraseña.Top + 9
                );

                picMostrarContraseña.BringToFront();
            }
        }

        // =========================================================
        // AL CAMBIAR EL TAMAÑO
        // =========================================================
        private void login_Resize(
            object? sender,
            EventArgs e)
        {
            CentrarCard();
        }

        // =========================================================
        // AL CARGAR EL LOGIN
        // =========================================================
        private void login_Load(
            object? sender,
            EventArgs e)
        {
            CentrarCard();
        }

        // =========================================================
        // INICIAR SESIÓN
        // =========================================================
        private void btnIniciarSesion_Click(
            object sender,
            EventArgs e)
        {

            try
            {
                string nombre =
                    txtUsuario.Text.Trim();

                string password =
                    txtContraseña.Text.Trim();

                if (string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(password))
                {
                    MessageBox.Show(
                        "Por favor, complete todos los campos.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                Usuario? usuarioAutenticado =
                    UsuarioDAL.ValidarLogin(
                        nombre,
                        password
                    );

                if (usuarioAutenticado != null)
                {
                    MessageBox.Show(
                        $"¡Bienvenido, {usuarioAutenticado.Nombre}!",
                        "Acceso Concedido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.Hide();

                    frmMain pantallaPrincipal =
                        new frmMain();

                    pantallaPrincipal.Show();
                }
                else
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Error de Autenticación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocurrió un error al intentar iniciar sesión: {ex.Message}",
                    "Error Crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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