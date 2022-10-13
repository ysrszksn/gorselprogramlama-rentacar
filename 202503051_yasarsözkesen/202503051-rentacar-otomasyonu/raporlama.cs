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
    public partial class raporlama : Form
    {
        public raporlama()
        {
            InitializeComponent();
        }

        static SqlConnection con;
        static SqlDataAdapter da;
        static DataSet ds;

        public static string sql = @"Data Source=.;Initial Catalog=202503051;Integrated Security=True";

        public void faturaraporu(string sorgu)
        {
            con = new SqlConnection(sql);
            da = new SqlDataAdapter(sorgu, con);
            ds = new DataSet();

            con.Open();
            da.Fill(ds);
            crystalReport11.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = crystalReport11;
        }
        private void raporlama_Load(object sender, EventArgs e)
        {
            sqlcon a = new sqlcon();
            faturaraporu("select a.a_plaka,a.a_marka,a.a_model,s_gün,s.s_fiyat from tbl_arac a join tbl_siparis s on s.s_aracid = a.a_id where s.s_musteriid='"+a.id(login.usersession)+"'");
        }
    }
}
