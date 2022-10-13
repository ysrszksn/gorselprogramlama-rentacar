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
namespace _202503051_rentacar_otomasyonu
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        int list = 1;
        int list1 = 1;

        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;


        string sql = "server=.; Initial Catalog=202503051;Integrated Security=SSPI";

        private void button1_Click(object sender, EventArgs e)
        {
            //ekleme
            dataGridView1.Visible = false;
            sqlcon.durum = "ekle";
            panel1.Controls.Clear();
            üye_kayıt a = new üye_kayıt();
            a.TopLevel = false;
            panel1.Controls.Add(a);
            a.Show();
            a.Dock = DockStyle.Fill;
            a.BringToFront();
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            list = 1; //güncelleme
            listelemüs();           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            list = 3; //silme
            listelemüs();           
        }
        private void button4_Click(object sender, EventArgs e)
        {
            list = 0; //listeleme
            listelemüs();        
        }
        public void listelemüs()
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView1);
            dataGridView1.Visible = true;
            dataGridView1.Dock = DockStyle.Fill;
            sqlcon sqlcon = new sqlcon();
            sqlcon.griddoldur(dataGridView1,"tbl_musteri");

        }      
        public void listelearac()
        {     
            panel2.Controls.Clear();
            panel2.Controls.Add(dataGridView2);
            dataGridView2.Visible = true;
            dataGridView2.Dock = DockStyle.Fill;
            sqlcon asd = new sqlcon();
            asd.griddoldur(dataGridView2,"tbl_arac");
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(list==1)//güncelleme
            {
             //label1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            sqlcon.durum = "güncelle";
            panel1.Controls.Clear();
            üye_kayıt a = new üye_kayıt()
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                ad = dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                soyad = dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                mail = dataGridView1.CurrentRow.Cells[3].Value.ToString(),
                tel = dataGridView1.CurrentRow.Cells[4].Value.ToString(),
                adres = dataGridView1.CurrentRow.Cells[5].Value.ToString(),
                kulad = dataGridView1.CurrentRow.Cells[6].Value.ToString(),
                tc = dataGridView1.CurrentRow.Cells[7].Value.ToString(),
                

            };    
            a.TopLevel = false;
            panel1.Controls.Add(a);
            a.Show();
            a.Dock = DockStyle.Fill;
            a.BringToFront();
            }
            else if (list == 2) // şifre değiştirme
            {
                panel1.Controls.Clear();
                sifre a = new sifre()
                {
                    kullanıcı = dataGridView1.CurrentRow.Cells[6].Value.ToString(),
                };
                a.TopLevel = false;
                panel1.Controls.Add(a);
                a.Show();
                a.Dock = DockStyle.Fill;
                a.BringToFront();
            }
            else if (list == 3) //SİLME
            {
                sqlcon a = new sqlcon();
                string sorgu = "DELETE FROM tbl_kullanici where k_ad = '"+ dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim()+ "' " ;
                string sorgu1 = "DELETE FROM tbl_musteri where m_id =" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                a.sorguyolla(sorgu);
                a.sorguyolla(sorgu1);         
                MessageBox.Show("başarı ile silindi");
            }
            else
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            list = 2;
            listelemüs();
        }
        private void button6_Click(object sender, EventArgs e)
        {       
            sqlcon.durum1 = "ekle";
            panel2.Controls.Clear();
            araçekle a = new araçekle();
            a.TopLevel = false;
            panel2.Controls.Add(a);
            a.Show();
            a.Dock = DockStyle.Fill;
            a.BringToFront();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            sqlcon.durum1 = "güncelle";
            list1 = 1;
            listelearac();
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {  
            if(list1 == 1)//güncelleme
            {
            panel2.Controls.Clear();
            araçekle a = new araçekle()
            {
                id = dataGridView2.CurrentRow.Cells[0].Value.ToString(),
                plaka = dataGridView2.CurrentRow.Cells[1].Value.ToString(),
                marka = dataGridView2.CurrentRow.Cells[2].Value.ToString(),
                model = dataGridView2.CurrentRow.Cells[3].Value.ToString(),          
                yakıt = dataGridView2.CurrentRow.Cells[4].Value.ToString(),
                vites = dataGridView2.CurrentRow.Cells[5].Value.ToString(),
                fiyat = dataGridView2.CurrentRow.Cells[6].Value.ToString()

            };
            a.TopLevel = false;
            panel2.Controls.Add(a);
            a.Show();
            a.Dock = DockStyle.Fill;
            a.BringToFront();
            }
            else if (list1==0)//silme
            { 
                sqlcon a = new sqlcon();
                string sorgu = "DELETE FROM tbl_arac where a_id =" + dataGridView2.CurrentRow.Cells[0].Value.ToString();
                a.sorguyolla(sorgu);
                MessageBox.Show("başarı ile silindi");
                listelearac();
            }
            else
            {
    
            }

        }
        private void button8_Click(object sender, EventArgs e)
        {
            list1 = 0;
            listelearac();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            list1 = 2;
            listelearac();
        }
        void stpgriddoldur(string deger)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(dataGridView3);
            dataGridView3.Visible = true;
            dataGridView3.Dock = DockStyle.Fill;
            con = new SqlConnection(sql);
            cmd = new SqlCommand("stp_kiralananAraclar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ateslim", SqlDbType.NVarChar).Value = deger;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            con.Open();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            con.Close();
            dataGridView3.Columns[0].HeaderText = "Plaka";
            dataGridView3.Columns[1].HeaderText = "Marka";
            dataGridView3.Columns[2].HeaderText = "Model";
            dataGridView3.Columns[3].HeaderText = "Müşteri ad";
            dataGridView3.Columns[4].HeaderText = "Müşteri soyad";
            dataGridView3.Columns[5].HeaderText = "Müşteri telefon";
            dataGridView3.Columns[6].HeaderText = "Teslim alma tarihi";
            dataGridView3.Columns[7].HeaderText = "Teslim edilme tarihi";
            dataGridView3.Columns[8].HeaderText = "Kiralanan gün sayısı";
            dataGridView3.Columns[9].HeaderText = "Fiyat";
        }
        void bosarac()
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(dataGridView3);
            dataGridView3.Visible = true;
            dataGridView3.Dock = DockStyle.Fill;
            
            con = new SqlConnection(sql);
            cmd = new SqlCommand("select * from tbl_arac where a_teslim = 'N' ", con);         
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            con.Open();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            con.Close();
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[1].HeaderText = "Plaka";
            dataGridView3.Columns[2].HeaderText = "Marka";
            dataGridView3.Columns[3].HeaderText = "Model";
            
            dataGridView3.Columns[4].HeaderText = "Yakıt türü";
            dataGridView3.Columns[5].HeaderText = "Vites türü";
            dataGridView3.Columns[6].HeaderText = "günlük kira bedeli";
            dataGridView3.Columns[7].Visible = false;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            stpgriddoldur("A");
        }
        private void button11_Click(object sender, EventArgs e)
        {
            bosarac();
        }

        void Notifyicon()
        {
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
        }

        int a = 0;

        private void admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (a == 0)
            {
                e.Cancel = true;
                Notifyicon();
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            a = 1;
            Application.Exit();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            a = 1;
            Application.Exit();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            a = 1;
            Application.Exit();
        }
    }
}
