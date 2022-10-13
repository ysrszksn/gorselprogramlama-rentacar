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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

    
        string sql = "server=.; Initial Catalog=202503051;Integrated Security=SSPI";
        private int durum1 = 1;




        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            araçkirala a = new araçkirala();
            a.TopLevel = false;
            panel2.Controls.Add(a);
            a.Show();
            a.Dock = DockStyle.Fill;
            a.BringToFront();
        }   
        void stpgriddoldur()
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(dataGridView1);
            dataGridView1.Visible = true;
            dataGridView1.Dock = DockStyle.Fill;
            con = new SqlConnection(sql);
            cmd = new SqlCommand("stp_kiraArac", con);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlcon a = new sqlcon();
            cmd.Parameters.Add("musteri_id", SqlDbType.Int).Value = a.id(login.usersession) ;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            con.Open();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
            dataGridView1.Columns[0].HeaderText = "Plaka";
            dataGridView1.Columns[1].HeaderText = "Marka";
            dataGridView1.Columns[2].HeaderText = "Model";
            dataGridView1.Columns[3].HeaderText = "Sipariş tarihi";
            dataGridView1.Columns[4].HeaderText = "Teslim Tarihi";
            dataGridView1.Columns[5].HeaderText = "Kiralanan gün sayısı";
            dataGridView1.Columns[6].HeaderText = "Ödenecek tutar";

        }
        private void button2_Click(object sender, EventArgs e)
        {
            durum1 = 1;
            stpgriddoldur();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            sifre a = new sifre()
            {
                kullanıcı = login.usersession,
            };
            a.TopLevel = false;
            panel2.Controls.Add(a);
            a.Show();
            a.Dock = DockStyle.Fill;
            a.BringToFront();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            durum1 = 2;
            stpgriddoldur();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(durum1 == 2)
            {
            sqlcon a = new sqlcon();    
            string sorgu = "update tbl_arac set a_teslim = 'N' where a_plaka = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString()+"'";
            string sorgu1 = "delete from tbl_siparis where s_aracid = "+a.arid(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            a.sorguyolla(sorgu);
            a.sorguyolla(sorgu1);
            MessageBox.Show("aracı teslim ettiğiniz için teşekkür ederiz");
            stpgriddoldur();
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            raporlama x = new raporlama();
            x.Show();
        }
       
        void Notifyicon()
        {
            this.Hide();
            notifyIcon1.Visible = true;
           // notifyIcon1.Text = "RENT - A - CAR";
          //  notifyIcon1.BalloonTipTitle = "arka planda çalışıyor";        
           // notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
           // notifyIcon1.ShowBalloonTip(2000);

            // notifyIcon için event ataması yaptık
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        int a = 0;
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(a== 0)
            {
                 e.Cancel = true;
                 Notifyicon();
            }
            else
            {
                e.Cancel = false;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            a = 1;
            Application.Exit();
        }
    }
}
