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

        // Método que reemplaza al SP sp_MostrarCuadre
        public DataTable MostrarCuadre(int idIsla, string turno, DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_MostrarCuadre", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros exactos del SP
                cmd.Parameters.AddWithValue("@id_Isla", idIsla);
                cmd.Parameters.AddWithValue("@Turno", turno);
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
        }

        // Método para guardar reporte (reemplaza sp_GuardarReporte)
        public void GuardarReporte(int idIsla, string turno, DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_GuardarReporte", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Isla", idIsla);
                cmd.Parameters.AddWithValue("@Turno", turno);
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Método que llama a sp_ObtenerParametros
        public DataTable ObtenerParametros(int idIsla, string turno, DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ObtenerParametros", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Isla", idIsla);
                cmd.Parameters.AddWithValue("@Turno", turno);
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable ListarReportes()
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ListarReportes", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

                return dt;
            }
        }

        // BUSCAR REPORTES POR FECHA
        public DataTable BuscarReportesPorFecha(DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_BuscarReportesPorFecha", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fecha", fecha);

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

                return dt;
            }
        }

        public DataTable MostrarHojaDetalle()
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_MostrarHojaDetalle", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable BuscarHojaDetallePorFecha(DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_BuscarHojaDetallePorFecha", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable BuscarReportePorParametros(int idIsla, string turno, DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_BuscarReportePorParametros", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Isla", idIsla);
                cmd.Parameters.AddWithValue("@Turno", turno);
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
        }

        public void RegistrarHojaDetalle(int idDetalle, string turno, DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_RegistrarHojaDetalle", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Detalle", idDetalle);
                cmd.Parameters.AddWithValue("@Turno", turno);
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
