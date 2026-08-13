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