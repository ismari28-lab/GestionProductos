namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProveedorEdicionDTO
    {
        public int IdProveedorPK { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
