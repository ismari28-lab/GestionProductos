// Este DTO sirve para devolver el resultado de crear o editar un proveedor junto con un mensaje para el usuario.
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoGuardarProveedorDTO
    {
        public ResultadoGuardarProveedor Resultado { get; set; }
        public string? Mensaje { get; set; }
        public int? IdGenerado { get; set; }
    }
}
