// Este helper de seguridad sirve para hashear y verificar contraseñas, detectando contraseñas antiguas en texto plano para rehashearlas al iniciar sesión.
using System;
using Microsoft.AspNetCore.Identity;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.LN.Security
{
    /// <summary>
    /// Resultado de verificar un password ingresado contra el valor almacenado en BD.
    /// </summary>
    public enum VerificacionPasswordResultado
    {
        /// <summary>El password no coincide.</summary>
        Fallido,
        /// <summary>El password coincide y ya está hasheado con el formato/costo actual.</summary>
        Exitoso,
        /// <summary>El password coincide pero el valor almacenado debe rehasheárse (era plano, o el hash usa un costo/formato viejo).</summary>
        ExitosoRequiereMigracion
    }

    /// <summary>
    /// Helper de hashing de passwords para el sistema web.
    /// Encapsula PasswordHasher&lt;Usuario&gt; de Microsoft.Extensions.Identity.Core y agrega detección
    /// de valores en texto plano (legado) para soportar la estrategia de re-hash on login.
    /// </summary>
    public static class PasswordHasherHelper
    {
        // Usuario como TUser dummy — el hasher no lee campos de la entidad, solo la usa como marcador de tipo.
        private static readonly PasswordHasher<Usuario> _hasher = new PasswordHasher<Usuario>();
        private static readonly Usuario _dummyUser = new Usuario();

        /// <summary>
        /// Hashea un password en texto plano. Nunca devuelve null si la entrada es válida.
        /// </summary>
        public static string Hash(string plano)
        {
            if (string.IsNullOrEmpty(plano))
                throw new ArgumentException("El password no puede estar vacío.", nameof(plano));

            return _hasher.HashPassword(_dummyUser, plano);
        }

        /// <summary>
        /// Verifica si un valor almacenado tiene formato de hash Identity (v2 o v3).
        /// Los hashes Identity son Base64 cuyo primer byte es 0x00 (v2) o 0x01 (v3).
        /// </summary>
        public static bool EsHash(string almacenado)
        {
            if (string.IsNullOrEmpty(almacenado))
                return false;

            // Longitud mínima razonable de un hash Identity (~84 chars); si es más corto casi seguro es plano.
            if (almacenado.Length < 60)
                return false;

            try
            {
                byte[] bytes = Convert.FromBase64String(almacenado);
                if (bytes.Length < 1)
                    return false;

                return bytes[0] == 0x00 || bytes[0] == 0x01;
            }
            catch (FormatException)
            {
                // No es Base64 válido -> no es hash Identity -> asumimos plano.
                return false;
            }
        }

        /// <summary>
        /// Verifica un password ingresado contra el valor almacenado.
        /// - Si <paramref name="almacenado"/> es hash Identity: usa PasswordHasher.VerifyHashedPassword.
        ///   Devuelve <see cref="VerificacionPasswordResultado.ExitosoRequiereMigracion"/> si el hasher indica
        ///   que hay que rehash (rotación futura de formato/costo).
        /// - Si <paramref name="almacenado"/> es texto plano: compara con igualdad exacta. Match =>
        ///   <see cref="VerificacionPasswordResultado.ExitosoRequiereMigracion"/> para que el caller haga el rehash.
        /// </summary>
        public static VerificacionPasswordResultado Verificar(string ingresado, string almacenado)
        {
            if (string.IsNullOrEmpty(ingresado) || string.IsNullOrEmpty(almacenado))
                return VerificacionPasswordResultado.Fallido;

            if (EsHash(almacenado))
            {
                var resultado = _hasher.VerifyHashedPassword(_dummyUser, almacenado, ingresado);
                return resultado switch
                {
                    PasswordVerificationResult.Success => VerificacionPasswordResultado.Exitoso,
                    PasswordVerificationResult.SuccessRehashNeeded => VerificacionPasswordResultado.ExitosoRequiereMigracion,
                    _ => VerificacionPasswordResultado.Fallido
                };
            }

            // Legado: password en texto plano.
            return string.Equals(ingresado, almacenado, StringComparison.Ordinal)
                ? VerificacionPasswordResultado.ExitosoRequiereMigracion
                : VerificacionPasswordResultado.Fallido;
        }
    }
}
