// Este DTO sirve para devolver el resultado de crear o editar un usuario junto con un mensaje para el usuario.
using ESFE.GestionProductos.LN.Enums;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoGuardarUsuarioDTO
    {
        public ResultadoGuardarUsuario Resultado { get; set; }
        public string? Mensaje { get; set; }
    }
}
