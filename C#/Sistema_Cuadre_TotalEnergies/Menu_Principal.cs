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
    public partial class Menu_Principal : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse
    );
        public Menu_Principal()
        {
            InitializeComponent();
        }

        private void btnini_Click(object sender, EventArgs e)
        {

        }

        private void pbusu_Click(object sender, EventArgs e)
        {

        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr Redondeo(
        int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

        private void Menu_Principal_Load(object sender, EventArgs e)
        {
            btncuadre.Region = Region.FromHrgn(Redondeo(0, 0, btncuadre.Width, btncuadre.Height, 10, 10));
            btnhojdetalle.Region = Region.FromHrgn(Redondeo(0, 0, btnhojdetalle.Width, btnhojdetalle.Height, 10, 10));
            btnreport.Region = Region.FromHrgn(Redondeo(0, 0, btnreport.Width, btnreport.Height, 10, 10));
            btnsalida.Region = Region.FromHrgn(Redondeo(0, 0, btnsalida.Width, btnsalida.Height, 7, 7));
            pictureBox2.Region = Region.FromHrgn(Redondeo(0, 0, pictureBox2.Width, pictureBox2.Height, 6, 6));

            pbusu.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0,
                pictureBox1.Width,
                pictureBox1.Height,
                pictureBox1.Width,
                pictureBox1.Height)
            );

            btncuadre.Paint += btn_paint;
            btnhojdetalle.Paint += btn_paint;
            btnreport.Paint += btn_paint;
        }

        private void Btnhojdetalle_Paint(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btn_paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using(SolidBrush brillo = new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
            {
                e.Graphics.FillEllipse(brillo, btn.Width - 60, -30, 80, 80);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
