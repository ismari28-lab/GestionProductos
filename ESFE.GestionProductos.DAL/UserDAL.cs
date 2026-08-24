using ESFE.GestionProductos.EN;
using ESFE.SysDesarrollo.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ESFE.GestionProductos.DAL
{
    public class UserDAL
    {
        // 1. Listar Usuarios
        public DataTable Listar()
        {
            DataTable dt = new DataTable();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "SP_ListarUsuarios",
                    conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // 2. Buscar Usuarios
        public List<Usuario> Buscar(string nombre = null, short? idUsuario = null)
        {
            List<Usuario> lista = new List<Usuario>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "sp_BuscarUsuario",
                    conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue(
                        "@Nombre",
                        (object)nombre ?? DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@IdUsuarioPK",
                        (object)idUsuario ?? DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            int ordId = lector.GetOrdinal("IdUsuarioPK");
                            int ordNombre = lector.GetOrdinal("Nombre");
                            int ordIdRolFK = lector.GetOrdinal("Id_RolFK");
                            int ordNombreRol = lector.GetOrdinal("NombreRol");
                            int ordEstado = lector.GetOrdinal("Estado");

                            Usuario usuario = new Usuario
                            {
                                IdUsuarioPK = Convert.ToInt16(lector[ordId]),

                                Nombre = lector.IsDBNull(ordNombre)
                                    ? string.Empty
                                    : lector.GetString(ordNombre),

                                Id_RolFK = lector.IsDBNull(ordIdRolFK)
                                    ? (short?)null
                                    : Convert.ToInt16(lector[ordIdRolFK]),

                                NombreRol = lector.IsDBNull(ordNombreRol) ? string.Empty : lector.GetString(ordNombreRol),

                                Estado = lector.IsDBNull(ordEstado)
                                    ? (bool?)null
                                    : Convert.ToBoolean(lector[ordEstado])
                            };

                            lista.Add(usuario);
                        }
                    }
                }
            }

            return lista;
        }

        // 3. Insertar Usuario
        public int Insertar(Usuario usuario)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "SP_InsertarUsuario",
                    conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue(
                        "@Nombre",
                        usuario.Nombre);

                    comando.Parameters.AddWithValue(
                        "@Password",
                        (object)usuario.Password ?? DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@Id_RolFK",
                        (object)usuario.Id_RolFK ?? DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@Estado",
                        (object)usuario.Estado ?? true);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // 4. Obtener la contraseña actual (para conservarla si al editar se deja en blanco)
        public string ObtenerPasswordActual(int idUsuario)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "SELECT Password FROM Usuario WHERE IdUsuarioPK = @IdUsuarioPK",
                    conexion as SqlConnection))
                {
                    comando.Parameters.AddWithValue("@IdUsuarioPK", idUsuario);

                    object resultado = comando.ExecuteScalar();
                    return resultado == null || resultado == DBNull.Value
                        ? null
                        : resultado.ToString();
                }
            }
        }

        // 5. Actualizar Usuario
        public int Actualizar(Usuario usuario)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "sp_ActualizarUsuario",
                    conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue(
                        "@IdUsuarioPK",
                        usuario.IdUsuarioPK);

                    comando.Parameters.AddWithValue(
                        "@Nombre",
                        (object)usuario.Nombre ?? DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@Password",
                        (object)usuario.Password ?? DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@Id_RolFK",
                        (object)usuario.Id_RolFK ?? DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@Estado",
                        (object)usuario.Estado ?? DBNull.Value);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // 6. Eliminación lógica
        public int EliminarLogico(short idUsuario)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "sp_EliminarLogicoUsuario",
                    conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue(
                        "@IdUsuario",
                        idUsuario);

                    return comando.ExecuteNonQuery();
                }
            }
        }
    }
}