using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DReporte
    {
        Conexion conexion = new Conexion();

        // REPORTE GENERAL
        public DataSet Reporte(int idIsla, string turno, DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Reporte", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }
    }
}
