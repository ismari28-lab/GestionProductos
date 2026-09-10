using ESFE.GestionProductos.LN.DTOs;

namespace ESFE.InventarioProd.Web.Models
{
    public class ProductoIndexViewModel
    {
        public List<CategoriaFiltroDTO> Categorias { get; set; } = new();
    }
}
