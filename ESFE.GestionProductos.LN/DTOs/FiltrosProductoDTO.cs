namespace ESFE.GestionProductos.LN.DTOs
{
    public class FiltrosProductoDTO
    {
        public string? Termino { get; set; }
        public short? IdCategoria { get; set; }
        public bool IncluirInactivos { get; set; } = false;
        public string OrdenarPor { get; set; } = "nombre";
        public string Direccion { get; set; } = "ASC";
        public int Pagina { get; set; } = 1;
        public int TamanioPagina { get; set; } = 25;
    }
}
