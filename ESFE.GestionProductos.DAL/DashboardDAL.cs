using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;

namespace ESFE.GestionProductos.DAL
{
    public class DashboardDAL
    {
        // Valor total del inventario (Stock * PrecioVenta) de productos activos
        public decimal ObtenerValorTotalInventario()
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ValorTotalInventario", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    object resultado = comando.ExecuteScalar();
                    return resultado == null || resultado == DBNull.Value
                        ? 0m
                        : Convert.ToDecimal(resultado);
                }
            }
        }

        // Cantidad de productos con Stock < Stock_Minimo
        public int ContarProductosBajoStock()
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ContarProductosBajoStock", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    object resultado = comando.ExecuteScalar();
                    return resultado == null || resultado == DBNull.Value
                        ? 0
                        : Convert.ToInt32(resultado);
                }
            }
        }

        // Producto mas vendido del mes calendario actual (null si no hay ventas)
        public (string Nombre, int TotalUnidades)? ObtenerProductoMasVendidoDelMes()
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ProductoMasVendidoMes", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                            return null;

                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordTotal = lector.GetOrdinal("TotalUnidades");

                        string nombre = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre);
                        int total = lector.IsDBNull(ordTotal) ? 0 : Convert.ToInt32(lector[ordTotal]);

                        return (nombre, total);
                    }
                }
            }
        }

        // Alertas de bajo stock, ordenadas por criticidad (mas criticos primero)
        public List<(string Nombre, int Existencias, int Minimo)> ObtenerAlertasBajoStock(int top)
        {
            List<(string Nombre, int Existencias, int Minimo)> lista = new List<(string, int, int)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_AlertasBajoStock", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Top", top);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordExistencias = lector.GetOrdinal("Existencias");
                        int ordMinimo = lector.GetOrdinal("Minimo");

                        while (lector.Read())
                        {
                            lista.Add((
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                lector.IsDBNull(ordExistencias) ? 0 : Convert.ToInt32(lector[ordExistencias]),
                                lector.IsDBNull(ordMinimo) ? 0 : Convert.ToInt32(lector[ordMinimo])
                            ));
                        }
                    }
                }
            }

            return lista;
        }
    }
}
