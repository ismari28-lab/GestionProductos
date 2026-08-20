using ESFE.GestionProductos.EN;
using ESFE.SysDesarrollo.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ESFE.GestionProductos.DAL
{
    public class UserDAL
    {
        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_ListarUsuario", conexion as SqlConnection))
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
            }
        }
        // 2. Buscar Empleados (Filtros opcionales)
        public List<Usuario> Buscar(string nombre = null, short? usuario = null)
        {
            List<Usuarioo> lista = new List<usuario>();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_BuscarUsuario", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdUsuarioPK", (object)idUsuario ?? DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader() as SqlDataReader)
                    {
                        while (lector.Read())
                        {
                            int ordId = lector.GetOrdinal("IdUsuarioPK");
                            int ordNombre = lector.GetOrdinal("Nombre");
                            int ordTelefono = lector.GetOrdinal("Telefono");
                            int ordCargo = lector.GetOrdinal("Cargo");
                            int ordUsuario = lector.GetOrdinal("IdUsuarioFK");
                            int ordEstado = lector.GetOrdinal("Estado");

                            Usuario emp = new Usuario
                            {
                                IdUsuarioPK = Convert.ToInt16(lector[ordId]),
                                Nombre = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                Telefono = lector.IsDBNull(ordTelefono) ? string.Empty : lector.GetString(ordTelefono),
                                Cargo = lector.IsDBNull(ordCargo) ? (short?)null : Convert.ToInt16(lector[ordCargo]),
                                IdUsuarioFK = lector.IsDBNull(ordUsuario) ? (short?)null : Convert.ToInt16(lector[ordUsuario]),
                                Estado = lector.IsDBNull(ordEstado) ? (bool?)null : Convert.ToBoolean(lector[ordEstado])
                            };

                            lista.Add(emp);
                        }
                    }
                }
            }
            return lista;
        }

// 3. Insertar Empleado
public int Insertar(Usuario usuario)
{
    using (IDbConnection conexion = DBComun.ObtenerConexion())
    {
        conexion.Open();
        using (SqlCommand comando = new SqlCommand("SP_InsertarUsuario", conexion as SqlConnection))
        {
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
            comando.Parameters.AddWithValue("@Cargo", (object)usuario.Cargo ?? DBNull.Value);
            comando.Parameters.AddWithValue("@IdUsuarioFK", (object)usuario.IdUsuarioFK ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Estado", (object)usuario.Estado ?? true);

            return comando.ExecuteNonQuery();
        }
    }
}
// 4. Actualizar Empleado
public int Actualizar(Usuario usuario)
{
    using (IDbConnection conexion = DBComun.ObtenerConexion())
    {
        conexion.Open();
        using (SqlCommand comando = new SqlCommand("sp_ActualizarUsuario", conexion as SqlConnection))
        {
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdUsuarioPK", usuario.IdUsuarioPK);
            comando.Parameters.AddWithValue("@Nombre", (object)usuario.Nombre ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Telefono", (object)usuario.Telefono ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Cargo", (object)usuario.Cargo ?? DBNull.Value);
            comando.Parameters.AddWithValue("@IdUsuarioFK", (object)usuario.IdUsuarioFK ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Estado", (object)usuario.Estado ?? DBNull.Value);

            return comando.ExecuteNonQuery();
        }
    }
     // 5. Eliminación Lógica
        public int EliminarLogico(short idUsuario)
{
    using (IDbConnection conexion = DBComun.ObtenerConexion())
    {
        conexion.Open();
        using (SqlCommand comando = new SqlCommand("sp_EliminarLogicoUsuario", conexion as SqlConnection))
        {
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdUsuario", idUsuario);

            return comando.ExecuteNonQuery();
        }
    }
}
    }





    // No salirse de aca
}


