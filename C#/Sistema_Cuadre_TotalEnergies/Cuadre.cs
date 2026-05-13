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
    public partial class Cuadre : Form
    {
        public Cuadre()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr Redondeo(
        int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

        private void Cuadre_Load(object sender, EventArgs e)
        {
            Mensaje_combo(cmbisla, "Seleccionar isla");
            Mensaje_combo(cmbturno, "Seleccionar turno");

            cmbisla.Font = new Font("Segoe UI", 9f);
            cmbturno.Font = new Font("Segoe UI", 9f);
            dtpfecha.Font = new Font("Segoe UI", 9f);

            // Mostrar inmediatamente
            ActualizarReloj();

            // Iniciar timer cada segundo
            _reloj = new Timer { Interval = 1000 };
            _reloj.Tick += (s, ev) => ActualizarReloj();
            _reloj.Start();

            btnhojadetalle.Region = Region.FromHrgn(Redondeo(0, 0, btnhojadetalle.Width, btnhojadetalle.Height, 10, 10));
            btngenreport.Region = Region.FromHrgn(Redondeo(0, 0, btngenreport.Width, btngenreport.Height, 10, 10));

        }

        private void Mensaje_combo(ComboBox cmb, string mensaje)
        {
            cmb.Items.Insert(0, mensaje);
            cmb.SelectedIndex = 0;
            cmb.ForeColor = Color.Gray;

            cmb.SelectedIndexChanged += (s, e) =>
            {
                if (cmb.SelectedIndex == 0)
                    cmb.ForeColor = Color.Gray;
                else
                    cmb.ForeColor = Color.FromArgb(28, 28, 46);
            };
        }

        private Timer _reloj;
        private void ActualizarReloj()
        {
            var cultura = new System.Globalization.CultureInfo("es-DO");
            lblfecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM yyyy", cultura);
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss tt", cultura);
        }

        // Detener el timer al cerrar el form
        private void Cuadre_FormClosing(object sender, FormClosingEventArgs e)
        {
            _reloj?.Stop();
            _reloj?.Dispose();
        }
    }
}
