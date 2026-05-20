using Negocio;
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
        // CAPA NEGOCIO
        NCuadre negocio = new NCuadre();

        public Cuadre()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr Redondeo(
        int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

        private Timer _reloj;

        private void Cuadre_Load(object sender, EventArgs e)
        {
            Mensaje_combo(cmbisla, "Seleccionar isla");
            Mensaje_combo(cmbturno, "Seleccionar turno");

            cmbisla.Font = new Font("Segoe UI", 9f);
            cmbturno.Font = new Font("Segoe UI", 9f);
            dtpfecha.Font = new Font("Segoe UI", 9f);

            // CARGAR DATOS COMBOBOX
            CargarIslas();
            CargarTurnos();

            // Mostrar inmediatamente
            ActualizarReloj();

            // Iniciar timer cada segundo
            _reloj = new Timer { Interval = 1000 };

            _reloj.Tick += (s, ev) => ActualizarReloj();

            _reloj.Start();

            btnhojadetalle.Region = Region.FromHrgn(
                Redondeo(0, 0,
                btnhojadetalle.Width,
                btnhojadetalle.Height,
                10, 10));

            btngenreport.Region = Region.FromHrgn(
                Redondeo(0, 0,
                btngenreport.Width,
                btngenreport.Height,
                10, 10));
        }

        // =========================
        // MENSAJE COMBOBOX
        // =========================
        private void Mensaje_combo(
            ComboBox cmb,
            string mensaje)
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

        // =========================
        // RELOJ
        // =========================
        private void ActualizarReloj()
        {
            var cultura =
                new System.Globalization.CultureInfo("es-DO");

            lblfecha.Text =
                DateTime.Now.ToString(
                    "dddd, dd 'de' MMMM yyyy",
                    cultura);

            lblhora.Text =
                DateTime.Now.ToString(
                    "hh:mm:ss tt",
                    cultura);
        }

        // =========================
        // DETENER TIMER
        // =========================
        private void Cuadre_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            _reloj?.Stop();

            _reloj?.Dispose();
        }

        // =========================
        // CARGAR ISLAS
        // =========================
        private void CargarIslas()
        {
            cmbisla.DataSource =
                negocio.MostrarDatosIsla();

            cmbisla.DisplayMember = "NomIsla";

            cmbisla.ValueMember = "id_Isla";
        }

        // =========================
        // CARGAR TURNOS
        // =========================
        private void CargarTurnos()
        {
            cmbturno.DataSource =
                negocio.MostrarTurnos();

            cmbturno.DisplayMember = "Turno";

            cmbturno.ValueMember = "Turno";
        }

        // =========================
        // MOSTRAR DATOS GRID
        // =========================
        private void btnbuscar_Click(
            object sender,
            EventArgs e)
        {
            int isla =
                Convert.ToInt32(cmbisla.SelectedValue);

            string turno =
                cmbturno.Text;

            DateTime fecha =
                dtpfecha.Value.Date;

            dataGridView1.DataSource =
                negocio.MostrarCuadre(
                    isla,
                    turno,
                    fecha);

            // MOSTRAR PARAMETROS
            MostrarParametros();
        }

        // =========================
        // PARAMETROS
        // =========================
        private void MostrarParametros()
        {
            int isla =
                Convert.ToInt32(cmbisla.SelectedValue);

            string turno =
                cmbturno.Text;

            DateTime fecha =
                dtpfecha.Value.Date;

            DataTable dt =
                negocio.ObtenerParametros(
                    isla,
                    turno,
                    fecha);

            if (dt.Rows.Count > 0)
            {
                label11.Text =
                    dt.Rows[0]["Ventas"].ToString();

                label9.Text =
                    dt.Rows[0]["Total"].ToString();

                label8.Text =
                    dt.Rows[0]["Galones"].ToString();

                label10.Text =
                    dt.Rows[0]["Balance"].ToString();
            }
        }

        // =========================
        // HOJA DETALLE
        // =========================
        private void btnhojadetalle_Click(
            object sender,
            EventArgs e)
        {
            // REGISTRAR AUTOMATICAMENTE
            negocio.RegistrarHojaDetalle(
                dtpfecha.Value.Date,
                cmbturno.Text);

            // ABRIR FORM EMERGENTE
            Menu_HojasDetalle frm =
                new Menu_HojasDetalle();

            frm.ShowDialog();
        }

        // =========================
        // GENERAR REPORTE
        // =========================
        private void btngenreport_Click(
            object sender,
            EventArgs e)
        {
            int isla =
                Convert.ToInt32(cmbisla.SelectedValue);

            string turno =
                cmbturno.Text;

            DateTime fecha =
                dtpfecha.Value.Date;

            // GUARDAR REPORTE
            negocio.GuardarReporte(
                isla,
                turno,
                fecha);

            // MOSTRAR REPORTE
            Menu_Reportes frm =
                new Menu_Reportes();

            frm.ShowDialog();
        }
    }
}