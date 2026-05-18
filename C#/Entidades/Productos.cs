using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Productos
    {
        public int id_Producto { get; set; }
        public string NomProducto { get; set; }
        public int id_Categoria { get; set; }
        public decimal Precio_Litro { get; set; }
        public decimal Galones { get; set; }
        public string Estado { get; set; }
    }
}
