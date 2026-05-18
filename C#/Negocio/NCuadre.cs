using Datos;
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
        DCuadre datos = new DCuadre();

        // CALCULO CUADRE
        public DataTable CalculoCuadre(int idIsla, string turno, DateTime fecha)
        {
            return datos.CalculoCuadre(idIsla, turno, fecha);
        }

        // TOTAL GALONES
        public DataTable TotalGalones(int idIsla, string turno, DateTime fecha)
        {
            return datos.TotalGalones(idIsla, turno, fecha);
        }

        // TOTAL VENTAS
        public DataTable TotalVentas(int idIsla, string turno, DateTime fecha)
        {
            return datos.TotalVentas(idIsla, turno, fecha);
        }

        // CANTIDAD TRANSACCIONES
        public DataTable CantidadTransacciones(int idIsla, string turno, DateTime fecha)
        {
            return datos.CantidadTransacciones(idIsla, turno, fecha);
        }

        // PROMEDIO VENTA
        public DataTable PromedioVenta(int idIsla, string turno, DateTime fecha)
        {
            return datos.PromedioVenta(idIsla, turno, fecha);
        }
    }
}
