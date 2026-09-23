// Este DTO sirve para transportar el id y nombre de una categoría, usado en listas desplegables y filtros.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class CategoriaFiltroDTO
    {
        public short IdCategoriaPK { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
