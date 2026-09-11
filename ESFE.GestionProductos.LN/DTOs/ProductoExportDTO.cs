namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProductoExportDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;
        public string ProveedorNombre { get; set; } = string.Empty;
        public int Existencias { get; set; }
        public int StockMinimo { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool AplicaIVA { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public bool Estado { get; set; }
    }
}
