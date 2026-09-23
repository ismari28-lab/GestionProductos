// Este ViewModel sirve para enviar a la vista de registrar movimiento el usuario actual y la fecha de hoy.
namespace ESFE.InventarioProd.Web.Models
{
    public class MovimientoIndexViewModel
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string FechaHoyIso { get; set; } = string.Empty; // yyyy-MM-dd para input date
    }
}
