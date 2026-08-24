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
<<<<<<< HEAD
            Application.Run(new FrmNuevaContraseña());
=======
            Application.Run(new login());
>>>>>>> 1801d2f98993a54ab58bab27172750ce92847ee0
        }
    }
}