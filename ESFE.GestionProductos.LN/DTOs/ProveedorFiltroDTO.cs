// Este DTO sirve para transportar el id y nombre de un proveedor, usado en listas desplegables y filtros.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProveedorFiltroDTO
    {
        public int IdProveedorPK { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
