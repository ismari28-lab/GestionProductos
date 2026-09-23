// Este DTO sirve para devolver el resultado de crear o editar un producto junto con un mensaje para el usuario.
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
