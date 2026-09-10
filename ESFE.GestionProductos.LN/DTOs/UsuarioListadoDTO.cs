namespace ESFE.GestionProductos.LN.DTOs
{
    public class UsuarioListadoDTO
    {
        public int IdUsuarioPK { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public short? IdRolFK { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
