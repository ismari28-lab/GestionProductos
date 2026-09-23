// Este DTO sirve para transportar los resultados de la búsqueda de productos al registrar un movimiento.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProductoBusquedaDTO
    {
        public int IdProductoPK { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int StockActual { get; set; }
    }
}
