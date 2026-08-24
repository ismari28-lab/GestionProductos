using System;
using System.Windows.Forms;

namespace ESFE.GestionProductos.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Ejecuta directamente el formulario FrmCodigoAcceso
            Application.Run(new FrmNuevaContraseña());
        }
    }
}