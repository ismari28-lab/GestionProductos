namespace ESFE.GestionProductos.LN.DTOs
{
    // No incluye Password: nunca se devuelve la contraseña al cliente.
    public class UsuarioEdicionDTO
    {
        public int IdUsuarioPK { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public short? IdRolFK { get; set; }
        public bool Estado { get; set; }
    }
}
