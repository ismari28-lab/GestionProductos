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

<<<<<<< HEAD
            // Ejecuta directamente el formulario Login
            Application.Run(new Login());
=======
            // Ejecuta directamente el formulario FrmCodigoAcceso

            Application.Run(new login());

>>>>>>> a598363946559c193b4282da516b8b04c3b1754d
        }
    }
}