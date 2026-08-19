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
        } // Se removió la llave de cierre que cortaba la clase aquí

        // Buscar Productos
        public List<Producto> Buscar(string nombre = null, short? idProducto = null)
        {
            List<Producto> lista = new List<Producto>();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_BuscarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdProductoPK", (object)idProducto ?? DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader() as SqlDataReader)
                    {
                        while (lector.Read())
                        {
                            int ordId = lector.GetOrdinal("IdProductoPK");
                            int ordNombre = lector.GetOrdinal("Nombre");
                            int ordDescripcion = lector.GetOrdinal("Descripcion");
                            int ordPrecioCompra = lector.GetOrdinal("PrecioCompra");
                            int ordPrecioVenta = lector.GetOrdinal("PrecioVenta");
                            int ordPorcentajeIVA = lector.GetOrdinal("PorcentajeIVA");
                            int ordAplicaIVA = lector.GetOrdinal("AplicaIVA");
                            int ordIdProveedorFK = lector.GetOrdinal("IdProveedorFK");
                            int ordIdCategoriaFK = lector.GetOrdinal("IdCategoriaFK");
                            int ordEstado = lector.GetOrdinal("Estado");

                            Producto producto = new Producto
                            {
                                IdProductoPK = Convert.ToInt16(lector[ordId]),
                                Nombre = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                Descripcion = lector.IsDBNull(ordDescripcion) ? string.Empty : lector.GetString(ordDescripcion),
                                PrecioCompra = lector.IsDBNull(ordPrecioCompra) ? (decimal?)null : Convert.ToDecimal(lector[ordPrecioCompra]),
                                PrecioVenta = lector.IsDBNull(ordPrecioVenta) ? (decimal?)null : Convert.ToDecimal(lector[ordPrecioVenta]),
                                PorcentajeIVA = lector.IsDBNull(ordPorcentajeIVA) ? (decimal?)null : Convert.ToDecimal(lector[ordPorcentajeIVA]),
                                AplicaIVA = lector.IsDBNull(ordAplicaIVA) ? (bool?)null : Convert.ToBoolean(lector[ordAplicaIVA]),
                                IdProveedorFK = lector.IsDBNull(ordIdProveedorFK) ? (short?)null : Convert.ToInt16(lector[ordIdProveedorFK]),
                                IdCategoriaFK = lector.IsDBNull(ordIdCategoriaFK) ? (short?)null : Convert.ToInt16(lector[ordIdCategoriaFK]),
                                Estado = lector.IsDBNull(ordEstado) ? (bool?)null : Convert.ToBoolean(lector[ordEstado])
                            };

                            lista.Add(producto);
                        }
                    }
                }
            }
            return lista;
        }

        // Insertar Productos
        public int Insertar(Producto producto)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                // Corregido: Se cambió "SP_InsertarEmpleado" por "SP_InsertarProducto"
                using (SqlCommand comando = new SqlCommand("SP_InsertarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
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

        // Actualización
        public int Actualizar(Producto producto)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ActualizarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    // Agregado: Parámetro para identificador único del producto a actualizar
                    comando.Parameters.AddWithValue("@IdProductoPK", producto.IdProductoPK);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
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
    } // Cierre correcto de la clase ProductoDAL
} // Cierre correcto del namespace