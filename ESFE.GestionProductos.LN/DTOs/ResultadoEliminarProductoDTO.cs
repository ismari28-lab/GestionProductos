// Este DTO sirve para devolver el resultado de eliminar un producto junto con un mensaje para el usuario.
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoEliminarProductoDTO
    {
        public ResultadoEliminarProducto Resultado { get; set; }
        public string? Mensaje { get; set; }
    }
}
