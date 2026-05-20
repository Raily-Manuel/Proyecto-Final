using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public void GuardarReporte(EReporte obj)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_GuardarReporte", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", obj.IdIsla);
            cmd.Parameters.AddWithValue("@Turno", obj.Turno);
            cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public DataSet RegistrarHojaDetalle(DateTime fecha, string turno)
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

        public DataTable ObtenerParametros(
            int idIsla,
            string turno,
            DateTime fecha)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_ObtenerParametros", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", idIsla);
            cmd.Parameters.AddWithValue("@Turno", turno);
            cmd.Parameters.AddWithValue("@Fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        public DataTable MostrarCuadre(ECuadre obj)
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_MostrarCuadre", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_Isla", obj.IdIsla);
            cmd.Parameters.AddWithValue("@Turno", obj.Turno);
            cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        public DataTable MostrarTurnos()
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_MostrarTurnos", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }


        public DataTable MostrarDatosIsla()
        {
            SqlConnection cn = conexion.ObtenerConexion();

            SqlCommand cmd = new SqlCommand("sp_MostrarDatosIsla", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
    }
}
