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

        // REPORTE GENERAL
        public DataSet Reporte(int idIsla, string turno, DateTime fecha)
        {
            return datos.Reporte(idIsla, turno, fecha);
        }
    }
}
