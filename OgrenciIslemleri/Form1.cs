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
        string cinsiyet;

        public void BaglantiKontrol()
        {
            if (baglanti.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            else
            {
                baglanti.Close();
            }
        }

        void VerileriGoster()
        {

            BaglantiKontrol();
            SqlCommand komut = new SqlCommand("select * from OgrenciIslemleri", baglanti);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(komut);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            BaglantiKontrol();
        }
        void Temizle()
        {
            txt_No.Clear();
            txt_Adi.Clear();
            txt_Soyad.Clear();
            txt_DogumTarihi.Clear();
            txt_Sinif.Clear();
            txt_Puan.Clear();
            rbtn_Kız.Checked = true;
            rbtn_Erkek.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VerileriGoster();
        }

        private void btn_Ekle_Click(object sender, EventArgs e)
        {

            BaglantiKontrol();
            SqlCommand komut = new SqlCommand("insert into OgrenciIslemleri (Adi,Soyad,Cinsiyet,DogumTarihi,Sinif,Puan) values(@adi,@soyadi,@cinsiyet,@dogumTarihi,@sinif,@puan)", baglanti);
            komut.Parameters.AddWithValue("@adi", txt_Adi.Text);
            komut.Parameters.AddWithValue("@soyadi", txt_Soyad.Text);
            komut.Parameters.AddWithValue("@cinsiyet",cinsiyet);
            komut.Parameters.AddWithValue("@dogumTarihi", Convert.ToDateTime(txt_DogumTarihi.Text));
            komut.Parameters.AddWithValue("@sinif", txt_Sinif.Text);
            komut.Parameters.AddWithValue("@puan", Convert.ToInt32(txt_Puan.Text));
            komut.ExecuteNonQuery();
            BaglantiKontrol();
            VerileriGoster();
        }

        private void rbtn_Kız_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "K";
        }

        private void rbtn_Erkek_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "E";
        }

        private void btn_Sil_Click(object sender, EventArgs e)
        {
            BaglantiKontrol();
            SqlCommand komut = new SqlCommand("delete from OgrenciIslemleri where OgrNo=@id ", baglanti);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(txt_No.Text));
            komut.ExecuteNonQuery();
            BaglantiKontrol();
            VerileriGoster();
            Temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_No.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_Adi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_Soyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_DogumTarihi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txt_Sinif.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_Puan.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            cinsiyet = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (cinsiyet=="K")
            {
                rbtn_Kız.Checked = true;
                rbtn_Erkek.Checked = false;
            }
            if (cinsiyet=="E")
            {
                rbtn_Erkek.Checked = true;
                rbtn_Kız.Checked = false;
            }
        }

        private void btn_Guncelle_Click(object sender, EventArgs e)
        {
            BaglantiKontrol();
            SqlCommand komut = new SqlCommand("update OgrenciIslemleri  set Adi=@adi,Soyad=@soyadi,Cinsiyet=@cinsiyet,DogumTarihi=@dogumTarihi,Sinif=@sinif,Puan=@puan where OgrNo=@no", baglanti);
            komut.Parameters.AddWithValue("@no", txt_No.Text);
            komut.Parameters.AddWithValue("@adi", txt_Adi.Text);
            komut.Parameters.AddWithValue("@soyadi", txt_Soyad.Text);
            komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
            komut.Parameters.AddWithValue("@dogumTarihi", Convert.ToDateTime(txt_DogumTarihi.Text));
            komut.Parameters.AddWithValue("@sinif", txt_Sinif.Text);
            komut.Parameters.AddWithValue("@puan", Convert.ToInt32(txt_Puan.Text));
            if (rbtn_Kız.Checked == true)
            {
                cinsiyet = "K";
            }
            if (rbtn_Erkek.Checked == true )
            {
                cinsiyet = "E";
            }
            
            komut.ExecuteNonQuery();
            BaglantiKontrol();
            VerileriGoster();

        }
    }
}
