using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Mangueras
    {
        public int id_Manguera { get; set; }
        public int id_Producto { get; set; }
        public decimal LecturaINL { get; set; }
        public decimal LecturaFNL { get; set; }
        public string Estado { get; set; }
    }
}
