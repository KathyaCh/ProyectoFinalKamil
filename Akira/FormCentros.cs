using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akira
{
    public partial class FormCentros : Form
    {
        SqlConnection cn;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader dr;

        public FormCentros()
        {
            InitializeComponent();
            cn = new SqlConnection(@"Data Source=LAPTOP-46K57IRP\SQLEXPRESS;Initial Catalog=Kamil;Integrated Security=True");
            cn.Open();
            ObtenerCentro_Trabajo();
        }

        public void ObtenerCentro_Trabajo ()
        {
            cmd = new SqlCommand("Centro_TrabajoCrudOperation", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Num_Centro", 0);
            cmd.Parameters.AddWithValue("@Ciudad", "");
            cmd.Parameters.AddWithValue("@Nombre_Centro", "");
            cmd.Parameters.AddWithValue("@OperationType", "5");
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvCentros.DataSource = dt;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtbNombreCentro.Text != string.Empty && txtbCiudad.Text != string.Empty)
            {
                cmd = new SqlCommand("Centro_TrabajoCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Num_Centro", 0);
                cmd.Parameters.AddWithValue("@Ciudad", txtbCiudad.Text);
                cmd.Parameters.AddWithValue("@Nombre_Centro", txtbNombreCentro.Text);
                cmd.Parameters.AddWithValue("@OperationType", "1");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record inserted successfully.", "Record Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerCentro_Trabajo();
                txtbNumeroCentro.Text = "";
                txtbCiudad.Text = "";
                txtbNombreCentro.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtbNumeroCentro.Text != string.Empty)
            {
                cmd = new SqlCommand("Centro_TrabajoCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Num_Centro", txtbNumeroCentro.Text);
                cmd.Parameters.AddWithValue("@Ciudad", "");
                cmd.Parameters.AddWithValue("@Nombre_Centro", "");
                cmd.Parameters.AddWithValue("@OperationType", "4");
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtbCiudad.Text = dr["Ciudad"].ToString();
                    txtbNombreCentro.Text = dr["Nombre_Centro"].ToString();
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No record found with this id", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dr.Close();
            }
            else
            {
                MessageBox.Show("Please enter employee id ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtbNumeroCentro.Text != string.Empty && txtbCiudad.Text != string.Empty && txtbNombreCentro.Text != string.Empty)
            {
                cmd = new SqlCommand("Centro_TrabajoCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Num_Centro", txtbNumeroCentro.Text);
                cmd.Parameters.AddWithValue("@Ciudad", txtbCiudad.Text);
                cmd.Parameters.AddWithValue("@Nombre_Centro", txtbNombreCentro.Text);
                cmd.Parameters.AddWithValue("@OperationType", "2");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record update successfully.", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerCentro_Trabajo();
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtbNumeroCentro.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this centro ? ", "Delete Centro", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Centro_TrabajoCrudOperation", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Num_Centro", txtbNumeroCentro.Text);
                    cmd.Parameters.AddWithValue("@Ciudad", "");
                    cmd.Parameters.AddWithValue("@Nombre_Centro", "");
                    cmd.Parameters.AddWithValue("@OperationType", "3");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerCentro_Trabajo();
                    txtbNumeroCentro.Text = "";
                    txtbCiudad.Text = "";
                    txtbNombreCentro.Text = "";
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please enter employee id", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
