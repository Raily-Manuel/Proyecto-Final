using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Cuadre_TotalEnergies
{
    public partial class pantallaemergente : Form
    {
        private Empleado _empleado;
        int idIsla;
        string turno;
        DateTime fecha;

        public pantallaemergente(
            int idIsla,
            string turno,
            DateTime fecha)
        {
            InitializeComponent();

            this.idIsla = idIsla;
            this.turno = turno;
            this.fecha = fecha;
        }

        private void pantallaemergente_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox200_Click(object sender, EventArgs e)
        {
            Menu_Principal frm = new Menu_Principal(_empleado);
            frm.ShowDialog();
        }
    }
}
