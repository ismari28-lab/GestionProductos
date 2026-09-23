// Esta clase de acceso a datos sirve para el inicio de sesión: valida credenciales y obtiene el usuario activo por nombre para verificar su contraseña hasheada.
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.DAL
{
    public class UsuarioDAL
    {
        /// <summary>
        /// Valida las credenciales del usuario contra la BD usando SP_LoginUsuario de forma automática.
        /// </summary>
        /// <returns>El usuario si las credenciales son válidas, null en caso contrario.</returns>
        [Obsolete("Usar UsuarioDAL.ObtenerPorNombre + PasswordHasherHelper.Verificar. SP_LoginUsuario compara password en SQL con '=', incompatible con hashing.", error: false)]
        public static Usuario? ValidarLogin(string pNombre, string pPassword)
        {
            Usuario? _usuario = null;

            using (IDbConnection _conexion = DBComun.ObtenerConexion())
            {
                _conexion.Open();

                using (SqlCommand _command = new SqlCommand("SP_LoginUsuario", _conexion as SqlConnection))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    // Solo enviamos Nombre y Password; el rol se obtiene directo de la BD
                    _command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = pNombre });
                    _command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 256) { Value = pPassword });

                    using (IDataReader _reader = _command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (_reader.Read())
                        {
                            _usuario = new Usuario
                            {
                                IdUsuarioPK = Convert.ToInt32(_reader["IdUsuarioPK"]),
                                Nombre = _reader["Nombre"].ToString(),
                                Password = _reader["Password"].ToString(),
                                Id_RolFK = Convert.ToInt16(_reader["Id_RolFK"]),
                                Estado = Convert.ToBoolean(_reader["Estado"])
                            };
                        }
                    }
                }
            }

            return _usuario;
        }

        /// <summary>
        /// Obtiene el usuario activo por nombre, sin filtrar por password (la verificación ocurre en C#).
        /// Usado por el flujo de login hasheado del sistema web (SP_ObtenerUsuarioPorNombre).
        /// </summary>
        /// <returns>El usuario si existe y está activo, null en caso contrario.</returns>
        public static Usuario? ObtenerPorNombre(string pNombre)
        {
            Usuario? _usuario = null;

            using (IDbConnection _conexion = DBComun.ObtenerConexion())
            {
                _conexion.Open();

                using (SqlCommand _command = new SqlCommand("SP_ObtenerUsuarioPorNombre", _conexion as SqlConnection))
                {
                    _command.CommandType = CommandType.StoredProcedure;

                    _command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = pNombre });

                    using (IDataReader _reader = _command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (_reader.Read())
                        {
                            _usuario = new Usuario
                            {
                                IdUsuarioPK = Convert.ToInt32(_reader["IdUsuarioPK"]),
                                Nombre = _reader["Nombre"].ToString(),
                                Password = _reader["Password"].ToString(),
                                Id_RolFK = Convert.ToInt16(_reader["Id_RolFK"]),
                                NombreRol = _reader["Rol"].ToString(),
                                Estado = Convert.ToBoolean(_reader["Estado"])
                            };
                        }
                    }
                }
            }

            return _usuario;
        }
    }
}