// Esta clase de lógica de negocio sirve para validar el inicio de sesión: verifica la contraseña (y la rehashea si es necesario) y devuelve los datos del usuario autenticado.
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Security;

namespace ESFE.GestionProductos.LN
{
    public class UsuarioLN
    {
        public LoginResultDTO? ValidarLogin(string nombre, string password)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(password))
                return null;

            // 1. Traer usuario por nombre (SP nuevo, sin filtro de password).
            Usuario? usuario = UsuarioDAL.ObtenerPorNombre(nombre);
            if (usuario == null || usuario.Estado != true)
                return null;

            // 2. Verificar password en C# (nunca en SQL).
            var resultado = PasswordHasherHelper.Verificar(password, usuario.Password ?? string.Empty);
            if (resultado == VerificacionPasswordResultado.Fallido)
                return null;

            // 3. Migración transparente: si el password estaba plano o el hash quedó viejo, rehashear en BD.
            //    Fallo del UPDATE = swallow silencioso (loguear a Debug). El login procede — no penalizamos al
            //    usuario por un problema de migración.
            if (resultado == VerificacionPasswordResultado.ExitosoRequiereMigracion)
            {
                try
                {
                    usuario.Password = PasswordHasherHelper.Hash(password);
                    // usuario ya trae Nombre/Id_RolFK/Estado del SP_ObtenerUsuarioPorNombre, así que el UPDATE
                    // (sp_ActualizarUsuario_v2, que escribe todas las columnas) preserva esos valores tal como
                    // estaban en BD.
                    var userDAL = new UserDAL();
                    userDAL.Actualizar(usuario);
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[PasswordMigration] Falló re-hash on login para usuario '{nombre}': {ex.Message}");
                    // Login sigue.
                }
            }

            // NombreRol viene del join del SP (columna "Rol" del SELECT), mapeado en UsuarioDAL.ObtenerPorNombre.
            return new LoginResultDTO
            {
                Usuario = usuario,
                NombreRol = usuario.NombreRol ?? string.Empty
            };
        }
    }
}
