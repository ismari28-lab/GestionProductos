namespace ESFE.GestionProductos.LN.DTOs
{
    public class FiltrosHistorialDTO
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string? Tipo { get; set; }
        public string? Termino { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanioPagina { get; set; } = 25;
    }
}
