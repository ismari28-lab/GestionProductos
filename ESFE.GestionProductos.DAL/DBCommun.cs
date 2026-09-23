// Esta clase de acceso a datos sirve para centralizar la conexión a la base de datos SQL Server y crear los comandos que usan el resto de clases DAL.
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
        // Conexion a la base de datos de produccion en Somee
        public const string _stringCnn =
            @"Server=GestionProd.mssql.somee.com;
            Database=GestionProd;
            User Id=JosiasRamirez_SQLLogin_1;
            Password=pzvsf81i7v;
            TrustServerCertificate=True;";

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
            SqlCommand _command = new SqlCommand(
                pSql,
                pConexion as SqlConnection
            );

            return _command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}