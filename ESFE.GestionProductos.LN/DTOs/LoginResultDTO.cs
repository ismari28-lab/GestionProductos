using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.LN.DTOs
{
    public class LoginResultDTO
    {
        public Usuario Usuario { get; set; } = null!;

        public string NombreRol { get; set; } = string.Empty;
    }
}
