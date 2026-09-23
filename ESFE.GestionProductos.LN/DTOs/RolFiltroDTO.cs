// Este DTO sirve para transportar el id y nombre de un rol, usado en listas desplegables y filtros.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class RolFiltroDTO
    {
        public short IdRolPK { get; set; }
        public string NombreRol { get; set; } = string.Empty;
    }
}
