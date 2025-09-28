using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaDAL
{
    public class ComunBD
    {
        public enum TipoBD
        {
            SqlServer, Oracle, DB2
        }
        public const string Sqlconn = @"Data Source=.\;Initial Catalog=Restaurante;Integrated Security=True;TrustServerCertificate=True;";
        public static IDbConnection ObtenerConexion(TipoBD pTipoBD)
        {
            IDbConnection _conn;
            if (pTipoBD == TipoBD.SqlServer)
            {
                _conn = new SqlConnection(Sqlconn);
                return _conn;
            }
            return null;
        }
    }
}
