// Esta clase de acceso a datos sirve para gestionar las imágenes de un producto: listarlas, contarlas, registrarlas, eliminarlas y marcar cuál es la principal.
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.DAL
{
    public class ResultadoEliminarImagenDb
    {
        public string NombreArchivo { get; set; } = string.Empty;
        public int IdProductoFK { get; set; }
        public bool EraPrincipal { get; set; }
        public int? IdImagenPromovida { get; set; }
        public string? NombreArchivoPromovido { get; set; }
    }

    public class ProductoImagenDAL
    {
        // Listar imágenes activas de un producto
        public List<ProductoImagen> Listar(int idProductoFK)
        {
            var lista = new List<ProductoImagen>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarImagenesProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordId = lector.GetOrdinal("IdProductoImagenPK");
                        int ordNombreArchivo = lector.GetOrdinal("NombreArchivo");
                        int ordEsPrincipal = lector.GetOrdinal("EsPrincipal");
                        int ordOrden = lector.GetOrdinal("Orden");

                        while (lector.Read())
                        {
                            lista.Add(new ProductoImagen
                            {
                                IdProductoImagenPK = Convert.ToInt32(lector[ordId]),
                                IdProductoFK = idProductoFK,
                                NombreArchivo = lector.IsDBNull(ordNombreArchivo) ? string.Empty : lector.GetString(ordNombreArchivo),
                                EsPrincipal = Convert.ToBoolean(lector[ordEsPrincipal]),
                                Orden = Convert.ToInt16(lector[ordOrden]),
                                Estado = true
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // Contar imágenes activas de un producto (validación de límite)
        public int ContarActivas(int idProductoFK)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ContarImagenesActivas", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);

                    object? resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        // Crear registro de imagen. Retorna el nuevo IdProductoImagenPK.
        public int Crear(int idProductoFK, string nombreArchivo, bool esPrincipal)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_CrearImagenProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoFK", idProductoFK);
                    comando.Parameters.AddWithValue("@NombreArchivo", nombreArchivo);
                    comando.Parameters.AddWithValue("@EsPrincipal", esPrincipal);

                    object? resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        // Soft-delete de una imagen. Retorna los datos necesarios para borrar del disco
        // y para saber si hubo promoción automática de nueva principal.
        public ResultadoEliminarImagenDb Eliminar(int idProductoImagenPK)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_EliminarImagenProducto", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoImagenPK", idProductoImagenPK);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                            return new ResultadoEliminarImagenDb();

                        int ordNombreArchivo = lector.GetOrdinal("NombreArchivo");
                        int ordIdProductoFK = lector.GetOrdinal("IdProductoFK");
                        int ordEraPrincipal = lector.GetOrdinal("EraPrincipal");
                        int ordIdImagenPromovida = lector.GetOrdinal("IdImagenPromovida");
                        int ordNombreArchivoPromovido = lector.GetOrdinal("NombreArchivoPromovido");

                        return new ResultadoEliminarImagenDb
                        {
                            NombreArchivo = lector.IsDBNull(ordNombreArchivo) ? string.Empty : lector.GetString(ordNombreArchivo),
                            IdProductoFK = lector.IsDBNull(ordIdProductoFK) ? 0 : Convert.ToInt32(lector[ordIdProductoFK]),
                            EraPrincipal = !lector.IsDBNull(ordEraPrincipal) && Convert.ToBoolean(lector[ordEraPrincipal]),
                            IdImagenPromovida = lector.IsDBNull(ordIdImagenPromovida) ? (int?)null : Convert.ToInt32(lector[ordIdImagenPromovida]),
                            NombreArchivoPromovido = lector.IsDBNull(ordNombreArchivoPromovido) ? null : lector.GetString(ordNombreArchivoPromovido)
                        };
                    }
                }
            }
        }

        // Marca una imagen como principal (desmarca la anterior del mismo producto)
        public bool MarcarPrincipal(int idProductoImagenPK)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_MarcarImagenPrincipal", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProductoImagenPK", idProductoImagenPK);

                    object? resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado) == 1;
                }
            }
        }
    }
}
