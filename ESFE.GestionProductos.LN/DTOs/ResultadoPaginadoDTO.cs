// Este DTO genérico sirve para devolver una página de resultados junto con el total de registros y datos de paginación.
namespace ESFE.GestionProductos.LN.DTOs
{
    public class ResultadoPaginadoDTO<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanioPagina { get; set; }
        public int TotalPaginas => TamanioPagina > 0
            ? (int)Math.Ceiling((double)TotalRegistros / TamanioPagina)
            : 0;
    }
}
