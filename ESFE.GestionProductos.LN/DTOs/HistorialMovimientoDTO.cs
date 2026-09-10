namespace ESFE.GestionProductos.LN.DTOs
{
    public class HistorialMovimientoDTO
    {
        public short IdMovimientoPK { get; set; }
        public string Referencia { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public int TotalItems { get; set; }
    }
}
