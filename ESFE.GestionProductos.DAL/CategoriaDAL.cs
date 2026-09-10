using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.DAL
{
    public class CategoriaDAL
    {
        // Listar (DataTable con alias — útil para grillas)
        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_ListarCategoria", conexion as SqlConnection))
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

        // Buscar (List<Categoria> tipada — útil para combos y edición)
        public List<Categoria> Buscar(
            string nombre = null,
            string descripcion = null,
            bool? estado = null)
        {
            List<Categoria> lista = new List<Categoria>();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_BuscarCategoria", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader() as SqlDataReader)
                    {
                        while (lector.Read())
                        {
                            int ordId = lector.GetOrdinal("IdCategoriaPK");
                            int ordNombre = lector.GetOrdinal("Nombre");
                            int ordDescripcion = lector.GetOrdinal("Descripcion");
                            int ordEstado = lector.GetOrdinal("Estado");

                            lista.Add(new Categoria
                            {
                                IdCategoriaPK = Convert.ToInt16(lector[ordId]),
                                Nombre = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                Descripcion = lector.IsDBNull(ordDescripcion) ? string.Empty : lector.GetString(ordDescripcion),
                                Estado = lector.IsDBNull(ordEstado) ? (bool?)null : Convert.ToBoolean(lector[ordEstado])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Insertar
        public int Insertar(Categoria categoria)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_InsertarCategoria", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)categoria.Nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Descripcion", (object)categoria.Descripcion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)categoria.Estado ?? true);
                    return comando.ExecuteNonQuery();
                }
            }
        }

        // Actualizar
        public int Actualizar(Categoria categoria)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("usp_ActualizarCategoria", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdCategoriaPK", categoria.IdCategoriaPK);
                    comando.Parameters.AddWithValue("@Nombre", (object)categoria.Nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Descripcion", (object)categoria.Descripcion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)categoria.Estado ?? DBNull.Value);
                    return comando.ExecuteNonQuery();
                }
            }
        }

        // Eliminar Lógico
        public int EliminarLogico(short idCategoria)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EliminarLogicoCategoria", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    return comando.ExecuteNonQuery();
                }
            }
        }

        // --- Módulo Categorías (STOCKEO): grid con conteo de productos ---

        // Categorías activas ordenadas por nombre, con conteo de productos activos asociados
        public List<(short IdCategoriaPK, string Nombre, string Descripcion, int TotalProductos)> ListarConConteo()
        {
            var lista = new List<(short, string, string, int)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarCategoriasConConteo", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordId = lector.GetOrdinal("IdCategoriaPK");
                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordDescripcion = lector.GetOrdinal("Descripcion");
                        int ordTotal = lector.GetOrdinal("TotalProductos");

                        while (lector.Read())
                        {
                            lista.Add((
                                Convert.ToInt16(lector[ordId]),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                lector.IsDBNull(ordDescripcion) ? string.Empty : lector.GetString(ordDescripcion),
                                lector.IsDBNull(ordTotal) ? 0 : Convert.ToInt32(lector[ordTotal])
                            ));
                        }
                    }
                }
            }

            return lista;
        }

        // Crea una categoría y retorna el nuevo Id
        public short Crear(string nombre, string? descripcion)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_CrearCategoria", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Descripcion", (object?)descripcion ?? DBNull.Value);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt16(resultado);
                }
            }
        }

        // Actualiza nombre/descripción de una categoría activa; retorna filas afectadas
        public int Actualizar(short idCategoriaPK, string nombre, string? descripcion)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ActualizarCategoria", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdCategoriaPK", idCategoriaPK);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Descripcion", (object?)descripcion ?? DBNull.Value);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        // Elimina (soft-delete) una categoría; retorna -1 (tiene productos activos), 0 (no encontrada) o 1 (eliminada)
        public int Eliminar(short idCategoriaPK)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_EliminarCategoria", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdCategoriaPK", idCategoriaPK);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }
    }
}