using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Crear el formulario principal que contendrá el UserControl
            MaterialForm formContenedor = new MaterialForm
            {
                Text = "Sistema de Gestión de Productos - Recuperar Contraseña",
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                Sizable = false
            };

            // 2. Configurar el tema visual de MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(formContenedor);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE
            );

            // 3. Instanciar tu UserControl y agregarlo al formulario
            uCRecuperarContraseña ucRecuperar = new uCRecuperarContraseña
            {
                Location = new Point(10, 70) // Posiciona el control debajo de la barra de título de MaterialSkin
            };

            formContenedor.Controls.Add(ucRecuperar);

            // 4. Ejecutar la aplicación
            Application.Run(formContenedor);
        }
    }
}