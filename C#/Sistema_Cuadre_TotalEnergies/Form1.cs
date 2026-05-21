using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        private Empleado _empleado;

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

        private void btnini_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDAR CAMPOS VACIOS
                if (string.IsNullOrWhiteSpace(txtnombre.Text) ||
                    string.IsNullOrWhiteSpace(txtcontra.Text))
                {
                    MessageBox.Show(
                        "Todos los campos son obligatorios.",
                        "Campos vacíos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // VALIDAR LIMITE DE CARACTERES
                if (txtnombre.Text.Length > 50 ||
                    txtcontra.Text.Length > 30)
                {
                    MessageBox.Show(
                        "Límite de caracteres excedido.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // CONEXION
                string conexion = ConfigurationManager
                    .ConnectionStrings["conexion"]
                    .ConnectionString;

                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("sp_Iniciar_Sesion", cn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", txtnombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@Clave", txtcontra.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();

                    // SI EXISTE
                    if (dr.Read())
                    {
                        string nombre = dr["Nombre"].ToString();
                        string cargo = dr["Cargo"].ToString();

                        MessageBox.Show(
                            "Bienvenido " + nombre +
                            "\nCargo: " + cargo,
                            "Inicio de sesión exitoso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // CREAR EMPLEADO
                        _empleado = new Empleado();

                        _empleado.Nombre = dr["Nombre"].ToString();
                        _empleado.Cargo = dr["Cargo"].ToString();
                        _empleado.Turno = dr["Turno"].ToString();

                        // OCULTAR LOGIN
                        pnldata.Visible = false;

                        // ABRIR MENU
                        Menu_Principal menu = new Menu_Principal(_empleado);

                        menu.TopLevel = false;
                        menu.Dock = DockStyle.Fill;

                        this.Controls.Add(menu);

                        menu.BringToFront();
                        menu.Show();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Acceso denegado.\nUsuario o contraseña incorrectos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error SQL:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
