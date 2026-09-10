using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;

namespace ESFE.GestionProductos.DAL
{
    public class MovimientoDAL
    {
        // Autocomplete: top 10 productos activos que matchean código o nombre, con stock actual
        public List<(int IdProductoPK, string Codigo, string Nombre, int StockActual)> BuscarProductos(string termino)
        {
            var lista = new List<(int, string, string, int)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_BuscarProductosParaMovimiento", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Termino", termino);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordId = lector.GetOrdinal("IdProductoPK");
                        int ordCodigo = lector.GetOrdinal("Codigo");
                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordStock = lector.GetOrdinal("StockActual");

                        while (lector.Read())
                        {
                            lista.Add((
                                Convert.ToInt32(lector[ordId]),
                                lector.IsDBNull(ordCodigo) ? string.Empty : lector.GetString(ordCodigo),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                lector.IsDBNull(ordStock) ? 0 : Convert.ToInt32(lector[ordStock])
                            ));
                        }
                    }
                }
            }

            return lista;
        }

        // Stock actual de un producto activo; null si no existe registro en Stock_Producto
        // TODO: alinear a int cuando se resuelva deuda técnica (Producto.IdProductoPK es int, FKs son short)
        public (short IdStockProductoPK, int Stock)? ObtenerStock(short idProductoFK)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ObtenerStockProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                            return null;

                        int ordId = lector.GetOrdinal("IdStock_ProductoPK");
                        int ordStock = lector.GetOrdinal("Stock");

                        return (
                            Convert.ToInt16(lector[ordId]),
                            lector.IsDBNull(ordStock) ? 0 : Convert.ToInt32(lector[ordStock])
                        );
                    }
                }
            }
        }

        public short InsertarCabecera(string tipo, string? referencia, DateTime fecha, short idUsuarioFK)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_InsertarMovimientoCabecera", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@TipoMovimiento", tipo);
                    comando.Parameters.AddWithValue("@Referencia", (object?)referencia ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Fecha", fecha);
                    comando.Parameters.AddWithValue("@IdUsuarioFK", idUsuarioFK);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt16(resultado);
                }
            }
        }

        // TODO: alinear a int cuando se resuelva deuda técnica (idProductoFK castea Producto.IdProductoPK de int a short)
        public void InsertarDetalle(short idMovimiento, short idProductoFK, short cantidad)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_InsertarDetalleMovimiento", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdMovimientoFK", idMovimiento);
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);
                    comando.ExecuteNonQuery();
                }
            }
        }

        // Suma (Entrada) o resta (Salida, delta negativo) al stock existente; retorna filas afectadas
        public int ActualizarStock(short idProductoFK, short delta)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ActualizarStockProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);
                    comando.Parameters.AddWithValue("@Delta", delta);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        // Crea el registro de stock para un producto que aún no lo tiene (solo Entrada)
        public void CrearStock(short idProductoFK, short stock)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_CrearStockProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);
                    comando.Parameters.AddWithValue("@Stock", stock);
                    comando.ExecuteNonQuery();
                }
            }
        }

        // --- Historial de movimientos (Sprint B) ---

        // Página filtrada del historial; retorna el total de registros (sin paginar) y la página solicitada
        public (int TotalRegistros, List<(short IdMovimientoPK, string Referencia, DateTime Fecha, string TipoMovimiento, string NombreUsuario, int TotalItems)> Items) ListarHistorial(
            DateTime desde, DateTime hasta, string? tipo, string? termino, int pagina, int tamanioPagina)
        {
            int total = 0;
            var items = new List<(short, string, DateTime, string, string, int)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarHistorialMovimientos", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Desde", desde);
                    comando.Parameters.AddWithValue("@Hasta", hasta);
                    comando.Parameters.AddWithValue("@Tipo", (object?)tipo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Termino", (object?)termino ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Pagina", pagina);
                    comando.Parameters.AddWithValue("@TamanioPagina", tamanioPagina);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                            total = Convert.ToInt32(lector["TotalRegistros"]);

                        lector.NextResult();

                        int ordId = lector.GetOrdinal("IdMovimientoPK");
                        int ordReferencia = lector.GetOrdinal("Referencia");
                        int ordFecha = lector.GetOrdinal("Fecha");
                        int ordTipo = lector.GetOrdinal("TipoMovimiento");
                        int ordUsuario = lector.GetOrdinal("NombreUsuario");
                        int ordTotalItems = lector.GetOrdinal("TotalItems");

                        while (lector.Read())
                        {
                            items.Add((
                                Convert.ToInt16(lector[ordId]),
                                lector.IsDBNull(ordReferencia) ? string.Empty : lector.GetString(ordReferencia),
                                Convert.ToDateTime(lector[ordFecha]),
                                lector.IsDBNull(ordTipo) ? string.Empty : lector.GetString(ordTipo),
                                lector.IsDBNull(ordUsuario) ? string.Empty : lector.GetString(ordUsuario),
                                lector.IsDBNull(ordTotalItems) ? 0 : Convert.ToInt32(lector[ordTotalItems])
                            ));
                        }
                    }
                }
            }

            return (total, items);
        }

        // Cabecera + líneas de un movimiento; null si no existe o está inactivo
        public (short IdMovimientoPK, string Referencia, string TipoMovimiento, DateTime Fecha, string NombreUsuario, List<(string Codigo, string ProductoNombre, int Cantidad)> Lineas)? ObtenerDetalle(short idMovimientoPK)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ObtenerDetalleMovimiento", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdMovimientoPK", idMovimientoPK);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                            return null;

                        short id = Convert.ToInt16(lector["IdMovimientoPK"]);
                        string referencia = lector["Referencia"] as string ?? string.Empty;
                        string tipoMovimiento = lector["TipoMovimiento"] as string ?? string.Empty;
                        DateTime fecha = Convert.ToDateTime(lector["Fecha"]);
                        string nombreUsuario = lector["NombreUsuario"] as string ?? string.Empty;

                        lector.NextResult();

                        var lineas = new List<(string, string, int)>();
                        int ordCodigo = lector.GetOrdinal("Codigo");
                        int ordNombre = lector.GetOrdinal("ProductoNombre");
                        int ordCantidad = lector.GetOrdinal("Cantidad");

                        while (lector.Read())
                        {
                            lineas.Add((
                                lector.IsDBNull(ordCodigo) ? string.Empty : lector.GetString(ordCodigo),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                lector.IsDBNull(ordCantidad) ? 0 : Convert.ToInt32(lector[ordCantidad])
                            ));
                        }

                        return (id, referencia, tipoMovimiento, fecha, nombreUsuario, lineas);
                    }
                }
            }
        }
    }
}
