using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NBackup
    {
        DBackup datos = new DBackup();

        public void RealizarBackup(string ruta)
        {
            datos.RealizarBackup(ruta);
        }
    }
}
