using System.Data;
using Microsoft.Data.SqlClient;
using ESFE.SysDesarrollo.DAL;

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

                using (SqlCommand comando = new SqlCommand(
                    "SP_ListarUsuarios",
                    conexion as SqlConnection))
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
    }
}