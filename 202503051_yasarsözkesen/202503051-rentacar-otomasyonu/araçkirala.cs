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
    public partial class araçkirala : Form
    {

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        string sql = "server=.; Initial Catalog=202503051;Integrated Security=SSPI";
        public araçkirala()
        {
            InitializeComponent();
        }

        private void araçkirala_Load(object sender, EventArgs e)
        {
            ara();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;      
        }
        private void ara()
        {
            con = new SqlConnection(sql);
            da = new SqlDataAdapter("select * from tbl_arac where a_teslim = 'n'",con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds);
            con.Close();
            bindingSource1.DataSource = ds.Tables[0];
            bindingNavigator1.BindingSource = bindingSource1;

            textBox1.DataBindings.Add(new Binding("text", bindingSource1, "a_plaka"));
            textBox2.DataBindings.Add(new Binding("text", bindingSource1, "a_marka"));
            textBox3.DataBindings.Add(new Binding("text", bindingSource1, "a_model"));
            textBox4.DataBindings.Add(new Binding("text", bindingSource1, "a_yakıt"));
            textBox5.DataBindings.Add(new Binding("text", bindingSource1, "a_vites"));
            textBox6.DataBindings.Add(new Binding("text", bindingSource1, "a_fiyat"));
            textBox7.DataBindings.Add(new Binding("text", bindingSource1, "a_id"));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox8.Text !="0")
            {
                sqlcon a = new sqlcon();
                string[] d = { "@t1", "@t2", "@fiyat", "@musid", "@arid", "@gün", "@tekf" };

                string[] veri = { dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), tutar().ToString(), a.id(login.usersession).ToString(), textBox7.Text, textBox8.Text, textBox6.Text };
                string eklesip = "INSERT INTO tbl_siparis (s_siparistarih,s_teslimtarih,s_fiyat,s_musteriid,s_aracid,s_gün,s_tekf) values (@t1,@t2,@fiyat,@musid,@arid,@gün,@tekf)";
                a.parametrelisorgu(veri, d, eklesip);
                string sorgu = "update tbl_arac set a_teslim = 'A' where a_id =  " + textBox7.Text;
                a.sorguyolla(sorgu);
                MessageBox.Show("başarı ile kiraladın");
                this.Close();

                raporlama x = new raporlama();
                x.Show();
            }
            else
            {
                MessageBox.Show("kiralanacak gün giriniz");
            }


         }
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public int tutar()
        {
            if (textBox8.Text != "")
            {
                return Convert.ToInt32(textBox8.Text) * Convert.ToInt32(textBox6.Text);
            }
            else
            {
                return 0;

            }
            
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if(textBox8.Text != string.Empty)
            {
            label12.Text = tutar().ToString();
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(Convert.ToInt32(textBox8.Text));
            }
            else
            {
                label12.Text = "0";
                dateTimePicker2.Value = dateTimePicker1.Value;
            }




        }
        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {
            label12.Text = tutar().ToString();
        }
    }
}
