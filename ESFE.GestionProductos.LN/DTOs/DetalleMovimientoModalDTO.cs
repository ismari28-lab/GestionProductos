namespace ESFE.GestionProductos.LN.DTOs
{
    public class DetalleMovimientoModalDTO
    {
        public short IdMovimientoPK { get; set; }
        public string Referencia { get; set; } = string.Empty;
        public string TipoMovimiento { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public List<LineaDetalleDTO> Lineas { get; set; } = new();
    }

    public class LineaDetalleDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string ProductoNombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }
}
