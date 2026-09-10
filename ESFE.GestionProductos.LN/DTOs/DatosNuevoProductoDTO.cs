namespace ESFE.GestionProductos.LN.DTOs
{
    public class DatosNuevoProductoDTO
    {
        public string CodigoSugerido { get; set; } = string.Empty;
        public List<CategoriaFiltroDTO> Categorias { get; set; } = new();
        public List<ProveedorFiltroDTO> Proveedores { get; set; } = new();
    }
}
