    using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NIsla
    {
        DIsla datos = new DIsla();

        // MOSTRAR DATOS ISLA
        public DataTable MostrarDatosIsla(int idIsla, string turno, DateTime fecha)
        {
            return datos.MostrarDatosIsla(idIsla, turno, fecha);
        }

        // HOJA DETALLE
        public DataSet HojaDetalle(DateTime fecha, string turno)
        {
            return datos.HojaDetalle(fecha, turno);
        }
    }
}
