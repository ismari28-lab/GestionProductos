// Este ViewModel sirve para enviar a la vista de usuarios la lista de roles disponibles.
using ESFE.GestionProductos.LN.DTOs;

namespace ESFE.InventarioProd.Web.Models
{
    public class UsuariosIndexViewModel
    {
        public List<RolFiltroDTO> Roles { get; set; } = new();
    }
}
