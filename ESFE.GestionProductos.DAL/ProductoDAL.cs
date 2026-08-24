using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.DAL
{
    public class ProductoDAL
    {
        // Listar Productos
        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_ListarProducto", conexion as SqlConnection))
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

        // Buscar Productos
        public List<Producto> Buscar(
    string nombre = null,
    short? idProducto = null,
    string codigo = null)
        {
            List<Producto> lista = new List<Producto>();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_BuscarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Codigo", (object)codigo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdProductoPK", (object)idProducto ?? DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader() as SqlDataReader)
                    {
                        while (lector.Read())
                        {
                            int ordId = lector.GetOrdinal("IdProductoPK");
                            int ordCodigo = lector.GetOrdinal("Codigo");
                            int ordNombre = lector.GetOrdinal("Nombre");
                            int ordDescripcion = lector.GetOrdinal("Descripcion");
                            int ordPrecioCompra = lector.GetOrdinal("PrecioCompra");
                            int ordPrecioVenta = lector.GetOrdinal("PrecioVenta");
                            int ordPorcentajeIVA = lector.GetOrdinal("PorcentajeIVA");
                            int ordAplicaIVA = lector.GetOrdinal("AplicaIVA");
                            int ordIdProveedorFK = lector.GetOrdinal("IdProveedorFK");
                            int ordIdCategoriaFK = lector.GetOrdinal("IdCategoriaFK");
                            int ordEstado = lector.GetOrdinal("Estado");

                            lista.Add(new Producto
                            {
                                IdProductoPK = Convert.ToInt16(lector[ordId]),
                                Codigo = lector.IsDBNull(ordCodigo) ? string.Empty : lector.GetString(ordCodigo),
                                Nombre = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                Descripcion = lector.IsDBNull(ordDescripcion) ? string.Empty : lector.GetString(ordDescripcion),
                                PrecioCompra = lector.IsDBNull(ordPrecioCompra) ? (decimal?)null : Convert.ToDecimal(lector[ordPrecioCompra]),
                                PrecioVenta = lector.IsDBNull(ordPrecioVenta) ? (decimal?)null : Convert.ToDecimal(lector[ordPrecioVenta]),
                                PorcentajeIVA = lector.IsDBNull(ordPorcentajeIVA) ? (decimal?)null : Convert.ToDecimal(lector[ordPorcentajeIVA]),
                                AplicaIVA = lector.IsDBNull(ordAplicaIVA) ? (bool?)null : Convert.ToBoolean(lector[ordAplicaIVA]),
                                IdProveedorFK = lector.IsDBNull(ordIdProveedorFK) ? (short?)null : Convert.ToInt16(lector[ordIdProveedorFK]),
                                IdCategoriaFK = lector.IsDBNull(ordIdCategoriaFK) ? (short?)null : Convert.ToInt16(lector[ordIdCategoriaFK]),
                                Estado = lector.IsDBNull(ordEstado) ? (bool?)null : Convert.ToBoolean(lector[ordEstado])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Insertar Producto
        public int Insertar(Producto producto)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_InsertarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Codigo", (object)producto.Codigo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Nombre", (object)producto.Nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Descripcion", (object)producto.Descripcion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PrecioCompra", (object)producto.PrecioCompra ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PrecioVenta", (object)producto.PrecioVenta ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PorcentajeIVA", (object)producto.PorcentajeIVA ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@AplicaIVA", (object)producto.AplicaIVA ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdProveedorFK", (object)producto.IdProveedorFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdCategoriaFK", (object)producto.IdCategoriaFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)producto.Estado ?? true);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // Actualizar
        public int Actualizar(Producto producto)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ActualizarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoPK", producto.IdProductoPK);
                    comando.Parameters.AddWithValue("@Codigo", (object)producto.Codigo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Nombre", (object)producto.Nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Descripcion", (object)producto.Descripcion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PrecioCompra", (object)producto.PrecioCompra ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PrecioVenta", (object)producto.PrecioVenta ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PorcentajeIVA", (object)producto.PorcentajeIVA ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@AplicaIVA", (object)producto.AplicaIVA ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdProveedorFK", (object)producto.IdProveedorFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdCategoriaFK", (object)producto.IdCategoriaFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)producto.Estado ?? true);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // Eliminar Lógico
        public int EliminarLogico(short idProducto)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EliminarLogicoProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);
                    return comando.ExecuteNonQuery();
                }
            }
        }
    }
}