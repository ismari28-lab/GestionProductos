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

            Application.Run(new login());
        }
    }
}