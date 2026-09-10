namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProductoFormDTO
    {
        public int? IdProductoPK { get; set; }        // null en Crear
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public short IdCategoriaFK { get; set; }
        public int? IdProveedorFK { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool AplicaIVA { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public short StockMinimo { get; set; }
        public bool Estado { get; set; } = true;      // ignorado en Crear (siempre true)
    }
}
