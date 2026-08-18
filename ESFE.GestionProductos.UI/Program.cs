using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using ESFE.GestionProductos.UI;

namespace ESFE.GestionProductos.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmCatalogoProductos());
        }
    }

    /// <summary>
    /// Formulario mínimo solo para probar el UserControl UcCatalogoProductos.
    /// Sustitúyelo por tu formulario principal real cuando lo integres.
    /// </summary>
    public class FrmCatalogoProductos : MaterialForm
    {
        private readonly MaterialSkinManager _skinManager;

        public FrmCatalogoProductos()
        {
            Text = "Catálogo de Productos";
            Width = 700;
            Height = 700;

            _skinManager = MaterialSkinManager.Instance;
            _skinManager.AddFormToManage(this);
            _skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            _skinManager.ColorScheme = new ColorScheme(
                Primary.Blue700, Primary.Blue800, Primary.Blue500,
                Accent.LightBlue200, TextShade.WHITE);

            var ucCatalogo = new UcCatalogoProductos { Dock = DockStyle.Fill };
            Controls.Add(ucCatalogo);
            
            Application.Run(new login()); 
        }
    }
}