// Este ViewModel sirve para enviar a la vista de productos las categorías usadas en los filtros.
using ESFE.GestionProductos.LN.DTOs;

namespace ESFE.InventarioProd.Web.Models
{
    public class ProductoIndexViewModel
    {
        public List<CategoriaFiltroDTO> Categorias { get; set; } = new();
    }
}
