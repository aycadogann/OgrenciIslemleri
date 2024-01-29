using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OgrenciIslemleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Denemeler;Integrated Security=True");

        void VerileriGoster()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from OgrenciIslemleri", baglanti);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komut);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VerileriGoster();
        }

        private void btn_Ekle_Click(object sender, EventArgs e)
        {

        }
    }
}
