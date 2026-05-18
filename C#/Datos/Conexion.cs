using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Datos
{
    public class Conexion
    {
        private static string conexion =
        ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        public SqlConnection ObtenerConexion()
        {
            SqlConnection cn = new SqlConnection(conexion);
            return cn;
        }
    }
}
