using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

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
                if (txtnombre.Text.Length > 30 ||
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

                // CONEXION DESDE APP.CONFIG
                string conexion = ConfigurationManager
                    .ConnectionStrings["conexion"]
                    .ConnectionString;

                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    cn.Open();

                    // PROCEDIMIENTO ALMACENADO
                    SqlCommand cmd = new SqlCommand("sp_Iniciar_Sesion", cn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Usuario", txtnombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contrasena", txtcontra.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();

                    // SI EXISTE EL USUARIO
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

                        // OCULTAR LOGIN
                        pnldata.Visible = false;

                        // ABRIR MENU PRINCIPAL EN EL MISMO FORM
                        Menu_Principal menu = new Menu_Principal();

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
                    "Error de base de datos:\n" + ex.Message,
                    "SQL Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Error de formato en los datos ingresados.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (OverflowException)
            {
                MessageBox.Show(
                    "Los datos ingresados son demasiado largos.",
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
