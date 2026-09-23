// Este DTO sirve para devolver el resultado de eliminar un usuario junto con un mensaje para el usuario.
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoEliminarUsuarioDTO
    {
        public ResultadoEliminarUsuario Resultado { get; set; }
        public string? Mensaje { get; set; }
    }
}
