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
    public partial class FormPuestos : Form
    {
        SqlConnection cn;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private SqlDataReader dr;
        public FormPuestos()
        {
            InitializeComponent();
            cn = new SqlConnection(@"Data Source=LAPTOP-46K57IRP\SQLEXPRESS;Initial Catalog=Kamil;Integrated Security=True");
            cn.Open();
            ObtenerCatalogo_Puesto();
        }

        public void ObtenerCatalogo_Puesto()
        {
            cmd = new SqlCommand("Catalogo_PuestoCrudOperation", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Num_Puesto", 0);
            cmd.Parameters.AddWithValue("@Nombre_Puesto", "");
            cmd.Parameters.AddWithValue("@Descripcion_Puesto", "");
            cmd.Parameters.AddWithValue("@OperationType", "5");
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvPuestos.DataSource = dt;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtbNombrePuesto.Text != string.Empty && txtbDescripcionPuesto.Text != string.Empty)
            {
                cmd = new SqlCommand("Catalogo_PuestoCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Num_Puesto", 0);
                cmd.Parameters.AddWithValue("@Nombre_Puesto", txtbNombrePuesto.Text);
                cmd.Parameters.AddWithValue("@Descripcion_Puesto", txtbDescripcionPuesto.Text);
                cmd.Parameters.AddWithValue("@OperationType", "1");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record inserted successfully.", "Record Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerCatalogo_Puesto();
                txtbNumeroPuesto.Text = "";
                txtbNombrePuesto.Text = "";
                txtbDescripcionPuesto.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtbNumeroPuesto.Text != string.Empty)
            {
                cmd = new SqlCommand("Catalogo_PuestoCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Num_Puesto", txtbNumeroPuesto.Text);
                cmd.Parameters.AddWithValue("@Nombre_Puesto", "");
                cmd.Parameters.AddWithValue("@Descripcion_Puesto", "");
                cmd.Parameters.AddWithValue("@OperationType", "4");
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtbNombrePuesto.Text = dr["Nombre_Puesto"].ToString();
                    txtbDescripcionPuesto.Text = dr["Descripcion_Puesto"].ToString();
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
            if (txtbNumeroPuesto.Text != string.Empty && txtbNombrePuesto.Text != string.Empty && txtbDescripcionPuesto.Text != string.Empty)
            {
                cmd = new SqlCommand("Catalogo_PuestoCrudOperation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Num_Puesto", txtbNumeroPuesto.Text);
                cmd.Parameters.AddWithValue("@Nombre_Puesto", txtbNombrePuesto.Text);
                cmd.Parameters.AddWithValue("@Descripcion_Puesto", txtbDescripcionPuesto.Text);
                cmd.Parameters.AddWithValue("@OperationType", "2");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record update successfully.", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerCatalogo_Puesto();
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
            if (txtbNumeroPuesto.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this centro ? ", "Delete Centro", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Catalogo_PuestoCrudOperation", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Num_Puesto", txtbNumeroPuesto.Text);
                    cmd.Parameters.AddWithValue("@Nombre_Puesto", "");
                    cmd.Parameters.AddWithValue("@Descripcion_Puesto", "");
                    cmd.Parameters.AddWithValue("@OperationType", "3");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerCatalogo_Puesto();
                    txtbNumeroPuesto.Text = "";
                    txtbNombrePuesto.Text = "";
                    txtbDescripcionPuesto.Text = "";
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
