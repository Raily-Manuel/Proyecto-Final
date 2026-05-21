using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NCuadre
    {
        DCuadre datosCuadre = new DCuadre(); // Métodos de cuadre antiguos
        DReporte datosReporte = new DReporte(); // Métodos de reporte antiguos

        // -----------------------------
        // MÉTODOS DE CUADRE
        // -----------------------------
        public DataTable CalculoCuadre(int idIsla, string turno, DateTime fecha)
        {
            return datosCuadre.CalculoCuadre(idIsla, turno, fecha);
        }

        public DataTable TotalGalones(int idIsla, string turno, DateTime fecha)
        {
            return datosCuadre.TotalGalones(idIsla, turno, fecha);
        }

        public DataTable TotalVentas(int idIsla, string turno, DateTime fecha)
        {
            return datosCuadre.TotalVentas(idIsla, turno, fecha);
        }

        public DataTable CantidadTransacciones(int idIsla, string turno, DateTime fecha)
        {
            return datosCuadre.CantidadTransacciones(idIsla, turno, fecha);
        }

        public DataTable PromedioVenta(int idIsla, string turno, DateTime fecha)
        {
            return datosCuadre.PromedioVenta(idIsla, turno, fecha);
        }

        public DataSet RegistrarHojaDetalle(DateTime fecha, string turno)
        {
            return datosCuadre.RegistrarHojaDetalle(fecha, turno);
        }

        public DataTable MostrarCuadre(ECuadre obj)
        {
            return datosCuadre.MostrarCuadre(obj);
        }

        public DataTable MostrarTurnos()
        {
            return datosCuadre.MostrarTurnos();
        }

        public DataTable MostrarDatosIsla()
        {
            return datosCuadre.MostrarDatosIsla();
        }

        // -----------------------------
        // MÉTODOS DE REPORTE
        // -----------------------------
        public DataTable MostrarCuadre(int isla, string turno, DateTime fecha)
        {
            return datosReporte.MostrarCuadre(isla, turno, fecha);
        }

        public DataTable ObtenerParametros(int isla, string turno, DateTime fecha)
        {
            return datosReporte.ObtenerParametros(isla, turno, fecha);
        }

        public void GuardarReporte(int isla, string turno, DateTime fecha, decimal total)
        {
            datosReporte.GuardarReporte(isla, turno, fecha, total);
        }
    }
}
