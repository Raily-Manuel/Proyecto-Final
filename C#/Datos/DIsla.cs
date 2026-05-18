using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Datos
{
    public class DIsla
    {
        Conexion conexion = new Conexion();

        // MOSTRAR DATOS ISLA
        public DataTable MostrarDatosIsla(int idIsla, string turno, DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Mostrar_Datos_Isla", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        // HOJA DETALLE
        public DataSet HojaDetalle(DateTime fecha, string turno)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Hoja_Detalle", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Fecha", fecha);
            cmd.Parameters.AddWithValue("@Turno", turno);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }
    }
}
