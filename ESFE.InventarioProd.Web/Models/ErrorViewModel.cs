// Este ViewModel sirve para enviar a la vista de error el identificador de la petición que falló.
namespace ESFE.InventarioProd.Web.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
