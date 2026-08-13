using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

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
    }
}