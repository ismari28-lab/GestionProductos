using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.DAL
{
    public class ProveedorDAL
    {
        // Listar (DataTable con alias)
        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_ListarProveedor", conexion as SqlConnection))
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

        // Buscar (List<Proveedor> tipada)
        public List<Proveedor> Buscar(
            string nombre = null,
            string empresa = null,
            string telefono = null,
            string correo = null,
            string direccion = null,
            bool? estado = null)
        {
            List<Proveedor> lista = new List<Proveedor>();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_BuscarProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Empresa", (object)empresa ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Correo", (object)correo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Direccion", (object)direccion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader() as SqlDataReader)
                    {
                        while (lector.Read())
                        {
                            int ordId = lector.GetOrdinal("IdProveedorPK");
                            int ordNombre = lector.GetOrdinal("Nombre");
                            int ordEmpresa = lector.GetOrdinal("Empresa");
                            int ordTelefono = lector.GetOrdinal("Telefono");
                            int ordCorreo = lector.GetOrdinal("Correo");
                            int ordDireccion = lector.GetOrdinal("Direccion");
                            int ordEstado = lector.GetOrdinal("Estado");

                            lista.Add(new Proveedor
                            {
                                IdProveedorPK = Convert.ToInt32(lector[ordId]),
                                Nombre = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                Empresa = lector.IsDBNull(ordEmpresa) ? string.Empty : lector.GetString(ordEmpresa),
                                Telefono = lector.IsDBNull(ordTelefono) ? string.Empty : lector.GetString(ordTelefono),
                                Correo = lector.IsDBNull(ordCorreo) ? string.Empty : lector.GetString(ordCorreo),
                                Direccion = lector.IsDBNull(ordDireccion) ? string.Empty : lector.GetString(ordDireccion),
                                Estado = lector.IsDBNull(ordEstado) ? (bool?)null : Convert.ToBoolean(lector[ordEstado])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Insertar
        public int Insertar(Proveedor proveedor)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_InsertarProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)proveedor.Nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Empresa", (object)proveedor.Empresa ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", (object)proveedor.Telefono ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Correo", (object)proveedor.Correo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Direccion", (object)proveedor.Direccion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)proveedor.Estado ?? true);
                    return comando.ExecuteNonQuery();
                }
            }
        }

        // Actualizar
        public int Actualizar(Proveedor proveedor)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("usp_ActualizarProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProveedorPK", proveedor.IdProveedorPK);
                    comando.Parameters.AddWithValue("@Nombre", (object)proveedor.Nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Empresa", (object)proveedor.Empresa ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", (object)proveedor.Telefono ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Correo", (object)proveedor.Correo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Direccion", (object)proveedor.Direccion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)proveedor.Estado ?? DBNull.Value);
                    return comando.ExecuteNonQuery();
                }
            }
        }

        // Eliminar Lógico
        public int EliminarLogico(short idProveedor)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EliminarLogicoProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    return comando.ExecuteNonQuery();
                }
            }
        }
    }
}