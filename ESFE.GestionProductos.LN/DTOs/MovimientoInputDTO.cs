namespace ESFE.GestionProductos.LN.DTOs
{
    public class MovimientoInputDTO
    {
        public string TipoMovimiento { get; set; } = string.Empty; // "Entrada" | "Salida"
        public string? Referencia { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleMovimientoInputDTO> Detalles { get; set; } = new();
    }
}
