// Este DTO sirve para devolver el resultado de eliminar un proveedor junto con un mensaje para el usuario.
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoEliminarProveedorDTO
    {
        public ResultadoEliminarProveedor Resultado { get; set; }
        public string? Mensaje { get; set; }
    }
}
