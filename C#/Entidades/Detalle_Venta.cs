using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Detalle_Venta
    {
        public int id_Detalle { get; set; }
        public int id_Venta { get; set; }
        public int id_Empleado { get; set; }
        public int id_Manguera { get; set; }
        public int id_Producto { get; set; }

        public decimal Precio { get; set; }
        public decimal LecturaINL { get; set; }
        public decimal LecturaFNL { get; set; }
        public decimal Galones { get; set; }
        public decimal Total { get; set; }

        public string Estado { get; set; }
    }
}
