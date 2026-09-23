// Este DTO sirve para transportar una fila del listado de usuarios que se muestra en la vista.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class UsuarioListadoDTO
    {
        public int IdUsuarioPK { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public short? IdRolFK { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
