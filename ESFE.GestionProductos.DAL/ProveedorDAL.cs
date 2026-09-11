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

        // --- Módulo Proveedores (STOCKEO): listado y modal Crear/Editar/Eliminar ---

        // Reutiliza sp_BuscarProveedor sin filtros: lista todos (activos e inactivos).
        public List<(int IdProveedorPK, string Nombre, string Empresa, string Telefono, string Correo, string Direccion, bool Estado)> ListarTodos()
        {
            var lista = new List<(int, string, string, string, string, string, bool)>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("sp_BuscarProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", DBNull.Value);
                    comando.Parameters.AddWithValue("@Empresa", DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", DBNull.Value);
                    comando.Parameters.AddWithValue("@Correo", DBNull.Value);
                    comando.Parameters.AddWithValue("@Direccion", DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        int ordId = lector.GetOrdinal("IdProveedorPK");
                        int ordNombre = lector.GetOrdinal("Nombre");
                        int ordEmpresa = lector.GetOrdinal("Empresa");
                        int ordTelefono = lector.GetOrdinal("Telefono");
                        int ordCorreo = lector.GetOrdinal("Correo");
                        int ordDireccion = lector.GetOrdinal("Direccion");
                        int ordEstado = lector.GetOrdinal("Estado");

                        while (lector.Read())
                        {
                            lista.Add((
                                Convert.ToInt32(lector[ordId]),
                                lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                lector.IsDBNull(ordEmpresa) ? string.Empty : lector.GetString(ordEmpresa),
                                lector.IsDBNull(ordTelefono) ? string.Empty : lector.GetString(ordTelefono),
                                lector.IsDBNull(ordCorreo) ? string.Empty : lector.GetString(ordCorreo),
                                lector.IsDBNull(ordDireccion) ? string.Empty : lector.GetString(ordDireccion),
                                !lector.IsDBNull(ordEstado) && Convert.ToBoolean(lector[ordEstado])
                            ));
                        }
                    }
                }
            }

            return lista;
        }

        public int CrearProveedor(string nombre, string? empresa, string? telefono, string? correo, string? direccion)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_CrearProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Empresa", (object?)empresa ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", (object?)telefono ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Correo", (object?)correo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Direccion", (object?)direccion ?? DBNull.Value);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        public int ActualizarProveedor(int idProveedorPK, string nombre, string? empresa, string? telefono, string? correo, string? direccion, bool estado)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ActualizarProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProveedorPK", idProveedorPK);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Empresa", (object?)empresa ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", (object?)telefono ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Correo", (object?)correo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Direccion", (object?)direccion ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", estado);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }

        // Retorna -1 (tiene productos activos), 0 (no encontrado / ya inactivo) o 1 (eliminado)
        public int EliminarProveedor(int id)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_EliminarProveedor", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProveedorPK", id);

                    object resultado = comando.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
            }
        }
    }
}