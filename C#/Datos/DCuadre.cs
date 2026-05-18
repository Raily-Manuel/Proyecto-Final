using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DCuadre
    {
        Conexion conexion = new Conexion();

        // CALCULO CUADRE
        public DataTable CalculoCuadre(int idIsla, string turno, DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Calculo_Cuadre", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        // TOTAL GALONES
        public DataTable TotalGalones(int idIsla, string turno, DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Total_Galones", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        // TOTAL VENTAS
        public DataTable TotalVentas(int idIsla, string turno, DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Total_Ventas", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        // CANTIDAD TRANSACCIONES
        public DataTable CantidadTransacciones(int idIsla, string turno, DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Cantidad_Transacciones", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        // PROMEDIO VENTA
        public DataTable PromedioVenta(int idIsla, string turno, DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_Promedio_Venta", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
    }
}
