using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient; 

namespace ESFE.SysDesarrollo.DL
{
    public class DBComun
    {
        // Agregamos 'TrustServerCertificate=True' para evitar errores de certificado en LocalDB
        public const string stringCnn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDDesarrollo;Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>
        /// Crea y retorna una conexión abierta a la base de datos SQL Server.
        /// </summary>
        public static IDbConnection ObtenerConexion()
        {
            SqlConnection cnn = new SqlConnection(stringCnn);
            cnn.Open();
            return cnn;
        }
    }
}