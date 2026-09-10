namespace ESFE.GestionProductos.LN.DTOs
{
    public class CategoriaConConteoDTO
    {
        public short IdCategoriaPK { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int TotalProductos { get; set; }
    }
}
