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
        // NOTA: comparte el mismo objeto de base de datos que SP_ActualizarProducto (Sprint B) —
        // los nombres de SP en SQL Server no distinguen mayúsculas/minúsculas por collation, y este
        // método ya no es invocado por ningún controlador (scaffolding legado de la sección "Listar Productos").
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

        // --- Módulo Productos (STOCKEO): listado con filtros, sort y paginación (Sprint A) ---

        public List<(short IdCategoriaPK, string Nombre)> ListarCategoriasActivas()
        {
            var lista = new List<(short, string)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarCategoriasActivas", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordId = lector.GetOrdinal("IdCategoriaPK");
                        int ordNombre = lector.GetOrdinal("Nombre");

                        while (lector.Read())
                        {
                            lista.Add((
                                Convert.ToInt16(lector[ordId]),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre)
                            ));
                        }
                    }
                }
            }

            return lista;
        }

        public (int TotalRegistros, List<(int IdProductoPK, string Codigo, string Nombre, string CategoriaNombre, int Existencias, int StockMinimo, decimal PrecioCompra, decimal PrecioVenta, bool Estado)> Items) ListarProductos(
            string? termino, short? idCategoria, bool incluirInactivos, string ordenarPor, string direccion, int pagina, int tamanioPagina)
        {
            int total = 0;
            var items = new List<(int, string, string, string, int, int, decimal, decimal, bool)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarProductos", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Termino", (object?)termino ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdCategoria", (object?)idCategoria ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IncluirInactivos", incluirInactivos);
                    comando.Parameters.AddWithValue("@OrdenarPor", ordenarPor);
                    comando.Parameters.AddWithValue("@Direccion", direccion);
                    comando.Parameters.AddWithValue("@Pagina", pagina);
                    comando.Parameters.AddWithValue("@TamanioPagina", tamanioPagina);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                            total = Convert.ToInt32(lector["TotalRegistros"]);

                        lector.NextResult();

                        int ordId = lector.GetOrdinal("IdProductoPK");
                        int ordCodigo = lector.GetOrdinal("Codigo");
                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordCategoria = lector.GetOrdinal("CategoriaNombre");
                        int ordExistencias = lector.GetOrdinal("Existencias");
                        int ordStockMinimo = lector.GetOrdinal("StockMinimo");
                        int ordPrecioCompra = lector.GetOrdinal("PrecioCompra");
                        int ordPrecioVenta = lector.GetOrdinal("PrecioVenta");
                        int ordEstado = lector.GetOrdinal("Estado");

                        while (lector.Read())
                        {
                            items.Add((
                                Convert.ToInt32(lector[ordId]),
                                lector.IsDBNull(ordCodigo) ? string.Empty : lector.GetString(ordCodigo),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                lector.IsDBNull(ordCategoria) ? string.Empty : lector.GetString(ordCategoria),
                                lector.IsDBNull(ordExistencias) ? 0 : Convert.ToInt32(lector[ordExistencias]),
                                lector.IsDBNull(ordStockMinimo) ? 0 : Convert.ToInt32(lector[ordStockMinimo]),
                                lector.IsDBNull(ordPrecioCompra) ? 0m : Convert.ToDecimal(lector[ordPrecioCompra]),
                                lector.IsDBNull(ordPrecioVenta) ? 0m : Convert.ToDecimal(lector[ordPrecioVenta]),
                                !lector.IsDBNull(ordEstado) && Convert.ToBoolean(lector[ordEstado])
                            ));
                        }
                    }
                }
            }

            return (total, items);
        }

        // --- Módulo Productos (STOCKEO): modal Crear/Editar/Eliminar (Sprint B) ---

        public List<(int IdProveedorPK, string Nombre)> ListarProveedoresActivos()
        {
            var lista = new List<(int, string)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarProveedoresActivos", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordId = lector.GetOrdinal("IdProveedorPK");
                        int ordNombre = lector.GetOrdinal("Nombre");

                        while (lector.Read())
                        {
                            lista.Add((
                                Convert.ToInt32(lector[ordId]),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre)
                            ));
                        }
                    }
                }
            }

            return lista;
        }

        public string ObtenerCodigoSugerido()
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ObtenerSiguienteCodigoProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    object? resultado = comando.ExecuteScalar();
                    return resultado?.ToString() ?? string.Empty;
                }
            }
        }

        public bool ValidarCodigoUnico(string codigo, int idExcluir)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ValidarCodigoUnicoProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Codigo", codigo);
                    comando.Parameters.AddWithValue("@IdExcluir", idExcluir);

                    object? resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado) == 1;
                }
            }
        }

        public (int IdProductoPK, string Codigo, string Nombre, short? IdCategoriaFK, int? IdProveedorFK, decimal PrecioCompra, decimal PrecioVenta, bool AplicaIVA, decimal PorcentajeIVA, bool Estado, int StockActual, int StockMinimo)? ObtenerParaEdicion(int id)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ObtenerProductoParaEdicion", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoPK", id);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                            return null;

                        int ordId = lector.GetOrdinal("IdProductoPK");
                        int ordCodigo = lector.GetOrdinal("Codigo");
                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordCategoria = lector.GetOrdinal("IdCategoriaFK");
                        int ordProveedor = lector.GetOrdinal("IdProveedorFK");
                        int ordPrecioCompra = lector.GetOrdinal("PrecioCompra");
                        int ordPrecioVenta = lector.GetOrdinal("PrecioVenta");
                        int ordAplicaIVA = lector.GetOrdinal("AplicaIVA");
                        int ordPorcentajeIVA = lector.GetOrdinal("PorcentajeIVA");
                        int ordEstado = lector.GetOrdinal("Estado");
                        int ordStockActual = lector.GetOrdinal("StockActual");
                        int ordStockMinimo = lector.GetOrdinal("StockMinimo");

                        return (
                            Convert.ToInt32(lector[ordId]),
                            lector.IsDBNull(ordCodigo) ? string.Empty : lector.GetString(ordCodigo),
                            lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                            lector.IsDBNull(ordCategoria) ? (short?)null : Convert.ToInt16(lector[ordCategoria]),
                            lector.IsDBNull(ordProveedor) ? (int?)null : Convert.ToInt32(lector[ordProveedor]),
                            lector.IsDBNull(ordPrecioCompra) ? 0m : Convert.ToDecimal(lector[ordPrecioCompra]),
                            lector.IsDBNull(ordPrecioVenta) ? 0m : Convert.ToDecimal(lector[ordPrecioVenta]),
                            !lector.IsDBNull(ordAplicaIVA) && Convert.ToBoolean(lector[ordAplicaIVA]),
                            lector.IsDBNull(ordPorcentajeIVA) ? 0m : Convert.ToDecimal(lector[ordPorcentajeIVA]),
                            !lector.IsDBNull(ordEstado) && Convert.ToBoolean(lector[ordEstado]),
                            lector.IsDBNull(ordStockActual) ? 0 : Convert.ToInt32(lector[ordStockActual]),
                            lector.IsDBNull(ordStockMinimo) ? 0 : Convert.ToInt32(lector[ordStockMinimo])
                        );
                    }
                }
            }
        }

        public int CrearProducto(
            string codigo, string nombre, short idCategoriaFK, int? idProveedorFK,
            decimal precioCompra, decimal precioVenta, bool aplicaIVA, decimal porcentajeIVA)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_CrearProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Codigo", codigo);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@IdCategoriaFK", idCategoriaFK);
                    comando.Parameters.AddWithValue("@IdProveedorFK", (object?)idProveedorFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                    comando.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    comando.Parameters.AddWithValue("@AplicaIVA", aplicaIVA);
                    comando.Parameters.AddWithValue("@PorcentajeIVA", porcentajeIVA);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        public void CrearStockInicial(short idProductoFK, short stockMinimo)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_CrearStockInicialProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);
                    comando.Parameters.AddWithValue("@StockMinimo", stockMinimo);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public int ActualizarProducto(
            int idProductoPK, string nombre, short idCategoriaFK, int? idProveedorFK,
            decimal precioCompra, decimal precioVenta, bool aplicaIVA, decimal porcentajeIVA, bool estado)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ActualizarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoPK", idProductoPK);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@IdCategoriaFK", idCategoriaFK);
                    comando.Parameters.AddWithValue("@IdProveedorFK", (object?)idProveedorFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                    comando.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    comando.Parameters.AddWithValue("@AplicaIVA", aplicaIVA);
                    comando.Parameters.AddWithValue("@PorcentajeIVA", porcentajeIVA);
                    comando.Parameters.AddWithValue("@Estado", estado);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        public int ActualizarStockMinimo(short idProductoFK, short stockMinimo)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ActualizarStockMinimoProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);
                    comando.Parameters.AddWithValue("@StockMinimo", stockMinimo);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        // Retorna -1 (tiene movimientos), 0 (no encontrado / ya inactivo) o 1 (eliminado)
        public int EliminarProducto(int id)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_EliminarProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoPK", id);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        // --- Módulo Productos (STOCKEO): exportar a Excel (Sprint C) ---

        public List<(string Codigo, string Nombre, string CategoriaNombre, string ProveedorNombre, int Existencias, int StockMinimo, decimal PrecioCompra, decimal PrecioVenta, bool AplicaIVA, decimal PorcentajeIVA, bool Estado)> ListarParaExportar(
            string? termino, short? idCategoria, bool incluirInactivos, string ordenarPor, string direccion)
        {
            var items = new List<(string, string, string, string, int, int, decimal, decimal, bool, decimal, bool)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarProductosParaExportar", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Termino", (object?)termino ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdCategoria", (object?)idCategoria ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IncluirInactivos", incluirInactivos);
                    comando.Parameters.AddWithValue("@OrdenarPor", ordenarPor);
                    comando.Parameters.AddWithValue("@Direccion", direccion);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordCodigo = lector.GetOrdinal("Codigo");
                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordCategoria = lector.GetOrdinal("CategoriaNombre");
                        int ordProveedor = lector.GetOrdinal("ProveedorNombre");
                        int ordExistencias = lector.GetOrdinal("Existencias");
                        int ordStockMinimo = lector.GetOrdinal("StockMinimo");
                        int ordPrecioCompra = lector.GetOrdinal("PrecioCompra");
                        int ordPrecioVenta = lector.GetOrdinal("PrecioVenta");
                        int ordAplicaIVA = lector.GetOrdinal("AplicaIVA");
                        int ordPorcentajeIVA = lector.GetOrdinal("PorcentajeIVA");
                        int ordEstado = lector.GetOrdinal("Estado");

                        while (lector.Read())
                        {
                            items.Add((
                                lector.IsDBNull(ordCodigo) ? string.Empty : lector.GetString(ordCodigo),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                lector.IsDBNull(ordCategoria) ? string.Empty : lector.GetString(ordCategoria),
                                lector.IsDBNull(ordProveedor) ? string.Empty : lector.GetString(ordProveedor),
                                lector.IsDBNull(ordExistencias) ? 0 : Convert.ToInt32(lector[ordExistencias]),
                                lector.IsDBNull(ordStockMinimo) ? 0 : Convert.ToInt32(lector[ordStockMinimo]),
                                lector.IsDBNull(ordPrecioCompra) ? 0m : Convert.ToDecimal(lector[ordPrecioCompra]),
                                lector.IsDBNull(ordPrecioVenta) ? 0m : Convert.ToDecimal(lector[ordPrecioVenta]),
                                !lector.IsDBNull(ordAplicaIVA) && Convert.ToBoolean(lector[ordAplicaIVA]),
                                lector.IsDBNull(ordPorcentajeIVA) ? 0m : Convert.ToDecimal(lector[ordPorcentajeIVA]),
                                !lector.IsDBNull(ordEstado) && Convert.ToBoolean(lector[ordEstado])
                            ));
                        }
                    }
                }
            }

            return items;
        }
    }
}
