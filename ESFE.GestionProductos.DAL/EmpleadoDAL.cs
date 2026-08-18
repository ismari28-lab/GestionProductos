using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL; // Asegúrate de importar el namespace de tu DBComun
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.DAL
{
    public class EmpleadoDAL
    {
        // 1. Listar Empleados (Para GridView)
        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_ListarEmpleado", conexion as SqlConnection))
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

        // 2. Buscar Empleados (Filtros opcionales)
        public List<Empleado> Buscar(string nombre = null, short? idEmpleado = null)
        {
            List<Empleado> lista = new List<Empleado>();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_BuscarEmpleado", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdEmpleadoPK", (object)idEmpleado ?? DBNull.Value);

                    using (SqlDataReader lector = comando.ExecuteReader() as SqlDataReader)
                    {
                        while (lector.Read())
                        {
                            int ordId = lector.GetOrdinal("IdEmpleadoPK");
                            int ordNombre = lector.GetOrdinal("Nombre");
                            int ordTelefono = lector.GetOrdinal("Telefono");
                            int ordCargo = lector.GetOrdinal("Cargo");
                            int ordUsuario = lector.GetOrdinal("IdUsuarioFK");
                            int ordEstado = lector.GetOrdinal("Estado");

                            Empleado emp = new Empleado
                            {
                                IdEmpleadoPK = Convert.ToInt16(lector[ordId]),
                                Nombre = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre),
                                Telefono = lector.IsDBNull(ordTelefono) ? string.Empty : lector.GetString(ordTelefono),
                                Cargo = lector.IsDBNull(ordCargo) ? (short?)null : Convert.ToInt16(lector[ordCargo]),
                                IdUsuarioFK = lector.IsDBNull(ordUsuario) ? (short?)null : Convert.ToInt16(lector[ordUsuario]),
                                Estado = lector.IsDBNull(ordEstado) ? (bool?)null : Convert.ToBoolean(lector[ordEstado])
                            };

                            lista.Add(emp);
                        }
                    }
                }
            }
            return lista;
        }

        // 3. Insertar Empleado
        public int Insertar(Empleado empleado)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("SP_InsertarEmpleado", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    comando.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                    comando.Parameters.AddWithValue("@Cargo", (object)empleado.Cargo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdUsuarioFK", (object)empleado.IdUsuarioFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)empleado.Estado ?? true);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // 4. Actualizar Empleado
        public int Actualizar(Empleado empleado)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_ActualizarEmpleado", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdEmpleadoPK", empleado.IdEmpleadoPK);
                    comando.Parameters.AddWithValue("@Nombre", (object)empleado.Nombre ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Telefono", (object)empleado.Telefono ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Cargo", (object)empleado.Cargo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdUsuarioFK", (object)empleado.IdUsuarioFK ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Estado", (object)empleado.Estado ?? DBNull.Value);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // 5. Eliminación Lógica
        public int EliminarLogico(short idEmpleado)
        {
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_EliminarLogicoEmpleado", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                    return comando.ExecuteNonQuery();
                }
            }
        }
    }
}