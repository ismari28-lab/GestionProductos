// Este DTO sirve para transportar el producto más vendido del mes y sus unidades vendidas para el dashboard.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProductoMasVendidoDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public int TotalUnidades { get; set; }
    }
}
