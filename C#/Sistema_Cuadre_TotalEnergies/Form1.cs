using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Cuadre_TotalEnergies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr Redondeo(
        int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

        private void Form1_Load(object sender, EventArgs e)
        {
            pnldata.Region = Region.FromHrgn(Redondeo(0, 0, pnldata.Width, pnldata.Height, 10, 10));
            pnlredline2.Region = Region.FromHrgn(Redondeo(0, 0, pnlredline2.Width, pnlredline2.Height, 10, 10));
            txtnombre.Region = Region.FromHrgn(Redondeo(0, 0, txtnombre.Width, txtnombre.Height, 6, 6));
            txtcontra.Region = Region.FromHrgn(Redondeo(0, 0, txtcontra.Width, txtcontra.Height, 6, 6));
            btnini.Region = Region.FromHrgn(Redondeo(0, 0, btnini.Width, btnini.Height, 15, 15));

            txtcontra.PasswordChar = '•';
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
