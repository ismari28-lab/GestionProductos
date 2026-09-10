namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProductoListadoDTO
    {
        public int IdProductoPK { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;
        public int Existencias { get; set; }
        public int StockMinimo { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Estado { get; set; }
    }
}
