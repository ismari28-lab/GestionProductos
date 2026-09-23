// Este archivo es el punto de entrada de la aplicación de escritorio (WinForms): inicializa la configuración de la aplicación y abre el formulario de login.
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