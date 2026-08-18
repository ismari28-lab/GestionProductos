using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL; // Asegúrate de importar el namespace de tu DBComun
using System.Text;

namespace ESFE.GestionProductos.DAL
{
    public class UserDAL
    {
        public DataTable ListarUsuarios()
        {
            DataTable dt = new DataTable();
            using (IDbConnection conexion = DBComun.ObtenerConexion())
            {
                conexion.Open();
                using (IDbCommand comando = conexion.CreateCommand())
                {
                    comando.CommandText = "SP_ListarUsuarios";
                    comando.CommandType = CommandType.StoredProcedure;
                    using (IDataAdapter adaptador = new System.Data.SqlClient.SqlDataAdapter(comando as System.Data.SqlClient.SqlCommand))
                    {
                        adaptador.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
