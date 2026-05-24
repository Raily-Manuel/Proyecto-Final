using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DBackup
    {
        Conexion conexion = new Conexion();

        public void RealizarBackup(string ruta)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_BackupBaseDatos", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Ruta", ruta);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
