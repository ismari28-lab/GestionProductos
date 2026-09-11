namespace ESFE.GestionProductos.LN.DTOs
{
    public class ProveedorFormDTO
    {
        public int? IdProveedorPK { get; set; }    // null en Crear
        public string Nombre { get; set; } = string.Empty;
        public string? Empresa { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public bool Estado { get; set; } = true;   // ignorado en Crear (siempre true)
    }
}
