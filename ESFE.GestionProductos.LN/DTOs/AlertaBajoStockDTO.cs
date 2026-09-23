// Este DTO sirve para transportar la información de un producto con stock bajo (nombre, existencias y mínimo) que se muestra en el dashboard.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class AlertaBajoStockDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public int Existencias { get; set; }

        public int Minimo { get; set; }
    }
}
