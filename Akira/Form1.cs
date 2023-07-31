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
    public partial class Form1 : Form
    {
        //Cadena de Conexion
        string cadena = "Data Source=LAPTOP-46K57IRP\\SQLEXPRESS;Initial Catalog=Kamil;Integrated Security=True";
        public SqlConnection Conectarbd = new SqlConnection();
        public Form1()
        {
            InitializeComponent();
            Conectarbd.ConnectionString = cadena;
            abrir();
            cerrar();
        }
        //Metodo para abrir la conexion
        public void abrir()
        {
            try
            {
                Conectarbd.Open();
                MessageBox.Show("Conexión abierta");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al abrir BD " + ex.Message);
            }
        }
        //Metodo para cerrar la conexion
        public void cerrar()
        {
            Conectarbd.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnEmpleados_Click (object sender, EventArgs e)
        {
            FormEmpleados formEmpleados = new FormEmpleados();
            formEmpleados.Show();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCentros_Click(object sender, EventArgs e)
        {
            FormCentros formCentros = new FormCentros();
            formCentros.Show();
        }

        private void btnDirectivos_Click(object sender, EventArgs e)
        {
            FormDirectivos formDirectivos = new FormDirectivos();
            formDirectivos.Show();
        }

        private void btnPuestos_Click(object sender, EventArgs e)
        {
            FormPuestos formPuestos = new FormPuestos();
            formPuestos.Show();
        }
    }
}
