using Entidades;
using Negocio;

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
    public partial class Menu_HojasDetalle : Form
    {
        private Empleado empleadoActual;
        NReporte negocio = new NReporte();

        public int IdIslaSeleccionada { get; set; }
        public DateTime FechaSeleccionada { get; set; }
        public string TurnoSeleccionado { get; set; }

        public Menu_HojasDetalle(Empleado empleado)
        {
            InitializeComponent();
            empleadoActual = empleado;
        }

        private void Menu_HojasDetalle_Load(object sender, EventArgs e)
        {
            MostrarHojasDetalle();
        }

        private void MostrarHojasDetalle()
        {
            try
            {
                dataGridView1.DataSource = negocio.MostrarHojaDetalle();

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar hojas de detalle: " + ex.Message);
            }

            finally
            {
                dataGridView1.ClearSelection();
            }
        }

        private void btnbuscarhojadedetalle_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource =
                    negocio.BuscarHojaDetallePorFecha(dateTimePicker2.Value.Date);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar hoja de detalle: " + ex.Message);
            }
            finally
            {
                dataGridView1.ClearSelection();
            }
        }

        private void btn2xp_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource =
                    negocio.MostrarHojaDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al mostrar hojas de detalle: "
                    + ex.Message);
            }
            finally
            {
                dataGridView1.ClearSelection();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    IdIslaSeleccionada = Convert.ToInt32(
                        dataGridView1.Rows[e.RowIndex].Cells["id_Isla"].Value);

                    TurnoSeleccionado =
                        dataGridView1.Rows[e.RowIndex].Cells["Turno"].Value.ToString();

                    FechaSeleccionada = Convert.ToDateTime(
                        dataGridView1.Rows[e.RowIndex].Cells["Fecha"].Value);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar hoja de detalle: " + ex.Message);
            }
            finally
            {
                dataGridView1.ClearSelection();
            }
        }

        private void pictureBox88_Click(object sender, EventArgs e)
        {
            Menu_Principal frm = new Menu_Principal(empleadoActual);
            frm.ShowDialog();
        }
    }
}