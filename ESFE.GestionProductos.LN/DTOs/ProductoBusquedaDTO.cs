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
