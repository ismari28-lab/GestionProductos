namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProductoEdicionDTO
    {
        public int IdProductoPK { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public short? IdCategoriaFK { get; set; }
        public int? IdProveedorFK { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool AplicaIVA { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public bool Estado { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
    }
}
