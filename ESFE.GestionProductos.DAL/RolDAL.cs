using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.DAL
{
    public class RolDAL
    {
        // Listar (List<Rol> tipada — útil para combos)
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();

            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand("SP_ListarRol", conexion as SqlConnection))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader lector = comando.ExecuteReader() as SqlDataReader)
                    {
                        int ordId = lector.GetOrdinal("ID Rol");
                        int ordNombre = lector.GetOrdinal("Nombre del Rol");

                        while (lector.Read())
                        {
                            lista.Add(new Rol
                            {
                                IdRolPK = Convert.ToInt16(lector[ordId]),
                                NombreRol = lector.IsDBNull(ordNombre) ? string.Empty : lector.GetString(ordNombre)
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
