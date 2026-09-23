// Este archivo es el punto de entrada de la aplicación de escritorio: inicializa la configuración de Windows Forms y abre el formulario de login.
namespace ESFE.GestionProductos.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Asegúrate de que apunte a tu formulario de login
            Application.Run(new login()); 
        }
    }
}