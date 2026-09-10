using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoGuardarMovimientoDTO
    {
        public ResultadoGuardarMovimiento Resultado { get; set; }
        public string? Mensaje { get; set; }
        public short? IdMovimientoGenerado { get; set; }

        // Lista de productos problemáticos para mensaje detallado
        public List<string> ProductosProblema { get; set; } = new();
    }
}
