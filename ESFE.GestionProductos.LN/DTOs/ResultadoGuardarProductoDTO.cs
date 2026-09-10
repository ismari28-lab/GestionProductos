using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoGuardarProductoDTO
    {
        public ResultadoGuardarProducto Resultado { get; set; }
        public string? Mensaje { get; set; }
        public int? IdGenerado { get; set; }
    }
}
