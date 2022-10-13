using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _202503051_rentacar_otomasyonu
{
    public partial class sifre : Form
    {
        public sifre()
        {
            InitializeComponent();
        }

        public string kullanıcı;
        private void button1_Click(object sender, EventArgs e)
        {
            sqlcon a = new sqlcon();
            if (a.login_kontrol(kullanıcı,textBox1.Text))
            {
                if (textBox2.Text == textBox3.Text && a.sifre_kontrol(textBox2.Text))
                {
                    //şifre değiştirilebilir
                    string sorgu = "update tbl_kullanici set k_pass = @pass where k_ad = @kad";
                    string[] d = { "@pass", "@kad"};               
                    string[] veri = {a.Md5pass(textBox2.Text),kullanıcı};     
                    a.parametrelisorgu(veri, d, sorgu);
                    MessageBox.Show("şifre değiştirildi");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("yeni şifre ve tekrarı aynı olmalıdır");
                }
            }
            else
            {
                MessageBox.Show("eski şifre hatalı");
            }
           
        }
        private void sifre_Load(object sender, EventArgs e)
        {
            label5.Text = kullanıcı;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            sqlcon sqlcon = new sqlcon();
            if (sqlcon.sifre_kontrol(textBox2.Text))
            {
                label6.Text = "şifre geçerli";
            }
            else
            {
                label6.Text = "lütfen şifreinizde büyük küçük harf sembol ve sayı kullanınız";
            }
            if (textBox2.Text == "")
            {
                label6.Text = "lütfen şifre giriniz";
            }
        }
    }
}
