// Este DTO sirve para devolver el resultado de guardar un movimiento de inventario junto con un mensaje para el usuario.
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
