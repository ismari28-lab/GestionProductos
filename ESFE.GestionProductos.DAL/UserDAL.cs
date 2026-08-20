<<<<<<< HEAD
﻿using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;
=======
﻿using ESFE.GestionProductos.EN;
using ESFE.SysDesarrollo.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
>>>>>>> 82e6f63e471d02c7255e92da592bd35cc90785c6

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
<<<<<<< HEAD
                    "SP_ListarUsuarios",
=======
                    "SP_ListarUsuario",
>>>>>>> 82e6f63e471d02c7255e92da592bd35cc90785c6
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
                            int ordTelefono = lector.GetOrdinal("Telefono");
                            int ordCargo = lector.GetOrdinal("Cargo");
                            int ordUsuario = lector.GetOrdinal("IdUsuarioFK");
                            int ordEstado = lector.GetOrdinal("Estado");

                            Usuario usuario = new Usuario
                            {
                                IdUsuarioPK = Convert.ToInt16(lector[ordId]),

                                Nombre = lector.IsDBNull(ordNombre)
                                    ? string.Empty
                                    : lector.GetString(ordNombre),

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
                        "@Estado",
                        (object)usuario.Estado ?? true);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // 4. Actualizar Usuario
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
                        "@Estado",
                        (object)usuario.Estado ?? DBNull.Value);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // 5. Eliminación lógica
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