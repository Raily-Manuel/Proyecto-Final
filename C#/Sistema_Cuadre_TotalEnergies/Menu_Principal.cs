using Entidades;
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
        private readonly Empleado _empleado;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        public Menu_Principal(Empleado empleado)
        {
            InitializeComponent();

            _empleado = empleado ?? throw new ArgumentNullException(nameof(empleado));
        }

        private void Menu_Principal_Load(object sender, EventArgs e)
        {
            // REDONDEAR BOTONES
            btncuadre.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btncuadre.Width, btncuadre.Height, 10, 10));

            btnhojdetalle.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btnhojdetalle.Width, btnhojdetalle.Height, 10, 10));

            btnreport.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btnreport.Width, btnreport.Height, 10, 10));

            btnsalida.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btnsalida.Width, btnsalida.Height, 7, 7));

            pictureBox2.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pictureBox2.Width, pictureBox2.Height, 6, 6));

            pbusu.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pbusu.Width, pbusu.Height, pbusu.Width, pbusu.Height));

            // EVENTOS PAINT
            btncuadre.Paint += btn_paint;
            btnhojdetalle.Paint += btn_paint;
            btnreport.Paint += btn_paint;

            // MOSTRAR DATOS
            label13.Text = _empleado.Nombre;
            label14.Text = _empleado.Cargo;
            label12.Text = _empleado.Turno;
        }

        private void btn_paint(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                e.Graphics.SmoothingMode =
                    System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (SolidBrush brillo =
                    new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
                {
                    e.Graphics.FillEllipse(brillo, btn.Width - 60, -30, 80, 80);
                }
            }
        }

        // CERRAR SESION
        private void btnsalida_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 login = new Form1();
            login.Show();
        }

        // CUADRE
        private void btncuadre_Click_1(object sender, EventArgs e)
        {
            Cuadre frm = new Cuadre(_empleado);
            frm.ShowDialog();
        }

        // HOJAS DETALLE
        private void btnhojdetalle_Click_1(object sender, EventArgs e)
        {
            Menu_HojasDetalle frm = new Menu_HojasDetalle(_empleado);
            frm.ShowDialog();
        }

        // REPORTES
        private void btnreport_Click_1(object sender, EventArgs e)
        {
            Menuparareportes frm = new Menuparareportes(_empleado);
            frm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Menu_Principal frm = new Menu_Principal(_empleado);
            frm.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}