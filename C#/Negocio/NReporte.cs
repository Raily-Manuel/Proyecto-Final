using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NReporte
    {
        DReporte datos = new DReporte();

        public DataTable MostrarReportes()
        {
            return datos.ListarReportes();
        }

        public DataTable BuscarPorFecha(DateTime fecha)
        {
            return datos.BuscarReportesPorFecha(fecha);
        }

        public DataTable MostrarHojaDetalle()
        {
            return datos.MostrarHojaDetalle();
        }

        public DataTable BuscarHojaDetallePorFecha(DateTime fecha)
        {
            return datos.BuscarHojaDetallePorFecha(fecha);
        }

        public DataTable BuscarReportePorParametros(int idIsla, string turno, DateTime fecha)
        {
            return datos.BuscarReportePorParametros(idIsla, turno, fecha);
        }

        public void RegistrarHojaDetalle(int idDetalle, string turno, DateTime fecha)
        {
           datos.RegistrarHojaDetalle(idDetalle, turno, fecha);
        }
        
    }
}
