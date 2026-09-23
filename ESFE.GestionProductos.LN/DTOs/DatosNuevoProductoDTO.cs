// Este DTO sirve para transportar los datos necesarios para crear un producto: código sugerido, categorías y proveedores.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class DatosNuevoProductoDTO
    {
        public string CodigoSugerido { get; set; } = string.Empty;
        public List<CategoriaFiltroDTO> Categorias { get; set; } = new();
        public List<ProveedorFiltroDTO> Proveedores { get; set; } = new();
    }
}
