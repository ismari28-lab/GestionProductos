namespace ESFE.GestionProductos.LN.DTOs
{
    public class UsuarioFormDTO
    {
        public int? IdUsuarioPK { get; set; }      // null en Crear
        public string Nombre { get; set; } = string.Empty;
        public string? Password { get; set; }      // obligatoria en Crear; en blanco en Editar = conservar la actual
        public short? IdRolFK { get; set; }
        public bool Estado { get; set; } = true;   // ignorado en Crear (siempre true)
    }
}
