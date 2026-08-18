using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ESFE.SysDesarrollo.DAL
{
    public class DBComun
    {
        // Actualizado a tu instancia de SQLEXPRESS y con TrustServerCertificate habilitado
        public const string _stringCnn = @"Data Source=DESKTOP-TF2SLSI\SQLEXPRESS;Initial Catalog=GestionProductoBD;Integrated Security=True;TrustServerCertificate=True";

        /// <summary>
        /// Metodo para obtener base de datos.
        /// </summary>
        /// <returns>Devuelve la conexion</returns>
        public static IDbConnection ObtenerConexion()
        {
            return new SqlConnection(_stringCnn);
        }

        public static IDataReader ObtenerCommando(IDbConnection pConexion, string pSql)
        {
            SqlCommand _command = new SqlCommand(pSql, pConexion as SqlConnection);
            return _command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}