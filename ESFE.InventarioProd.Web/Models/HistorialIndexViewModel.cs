// Este ViewModel sirve para enviar a la vista del historial de movimientos el rango de fechas inicial para los filtros.
namespace ESFE.InventarioProd.Web.Models
{
    public class HistorialIndexViewModel
    {
        public string DesdeIso { get; set; } = string.Empty; // yyyy-MM-dd
        public string HastaIso { get; set; } = string.Empty; // yyyy-MM-dd
    }
}
