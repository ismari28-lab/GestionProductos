using System.Linq;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN.DTOs;

namespace ESFE.GestionProductos.LN
{
    public class UsuarioLN
    {
        public LoginResultDTO? ValidarLogin(string nombre, string password)
        {
            Usuario? usuario = UsuarioDAL.ValidarLogin(nombre, password);

            if (usuario == null)
                return null;

            // Protección extra por si el SP alguna vez deja de filtrar por Estado
            if (usuario.Estado != true)
                return null;

            var roles = new RolLN().Listar();
            var rol = roles.FirstOrDefault(r => r.IdRolPK == usuario.Id_RolFK);

            // Si no se encuentra el rol (inconsistencia de datos), no se falla el login;
            // simplemente queda sin nombre de rol.
            string nombreRol = rol?.NombreRol ?? string.Empty;

            return new LoginResultDTO
            {
                Usuario = usuario,
                NombreRol = nombreRol
            };
        }
    }
}
