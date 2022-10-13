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
    public partial class üye_kayıt : Form
    {
        public üye_kayıt()
        {
            InitializeComponent();
        }

        public string ad,soyad,mail,tel,adres,kulad,tc,id;
        private bool güncel;
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
            textBox2.Focus();
        }
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.Focus();
        }
        private bool kontrol()
        {
            sqlcon sqlcon = new sqlcon();
            if (sqlcon.sifre_kontrol(textBox2.Text) && !sqlcon.kullanıcıad_kontrol(textBox1.Text) && textBox1.TextLength >= 4 && textBox3.TextLength == 11 && maskedTextBox1.Text != "(   )    -" && textBox5.Text !="" && textBox6.Text != "" && comboBox1.SelectedIndex !=-1)
            {
                return true;
            }
            else
            {            
                return false;
            }
            
        }
        private bool kontrol1()
        {
            sqlcon sqlcon = new sqlcon();
            if (sqlcon.sifre_kontrol(textBox2.Text) && (!sqlcon.kullanıcıad_kontrol(textBox1.Text) || kulad == textBox1.Text) && textBox1.TextLength >= 4 && textBox3.TextLength == 11 && maskedTextBox1.Text != "(   )    -" && textBox5.Text != "" && textBox6.Text != "" && comboBox1.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.SelectedText = "";
            comboBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            maskedTextBox1.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
          
            if (sqlcon.durum == "ekle")
            {
                if(kontrol())
                {
                    sqlcon a = new sqlcon();
                    string[] d = { "@ad", "@soyad", "@mail", "@tel", "@adres", "@kulad","@tc" };
                    if (comboBox1.SelectedIndex==0)//müşteri ise
                    {
                         
                         string[] veri = { textBox4.Text ,textBox7.Text ,textBox5.Text ,maskedTextBox1.Text.Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", "") ,textBox6.Text ,textBox1.Text,textBox3.Text };
                         string eklemus = "INSERT INTO tbl_musteri (m_ad,m_soyad,m_mail,m_tel,m_adres,m_kulad,m_tc) values (@ad,@soyad,@mail,@tel,@adres,@kulad,@tc)";
                         a.parametrelisorgu(veri,d,eklemus);
                    }                 
                    else //admin ise
                    {
                        string[] veri = { textBox4.Text, textBox7.Text, textBox5.Text, maskedTextBox1.Text.Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", ""), textBox6.Text, textBox1.Text, textBox3.Text };
                        string eklead = "INSERT INTO tbl_admin (a_ad,a_soyad,a_mail,a_tel,a_adres,a_kulad,a_tc) values (@ad,@soyad,@mail,@tel,@adres,@kulad,@tc)"; 
                        a.parametrelisorgu(veri,d, eklead);
                    }
                    //tbl_kullanici eklenmesi lazım
                        string[] veri1 = { textBox1.Text, a.Md5pass(textBox2.Text), comboBox1.SelectedIndex.ToString()};
                        string eklekul = "INSERT INTO tbl_kullanici (k_ad,k_pass,k_tür) values (@ad,@pass,@tür)";
                        string[] d1 = { "@ad", "@pass", "@tür" };
                        a.parametrelisorgu(veri1,d1, eklekul);

                          MessageBox.Show("başarı ile eklendi");
                    temizle();

                }
                else
                {
                    MessageBox.Show("ekleme hatası");
                }
                
               
            }

            else if (sqlcon.durum =="güncelle")
            {
                if(kontrol1())
                {
                    sqlcon a = new sqlcon();
                    //string[] veri = { textBox1.Text, "11" ,maskedTextBox1.Text.Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", ""), textBox3.Text, textBox5.Text, textBox6.Text };
                    //a.güncelle(veri);

                    string[] d = { "@ad", "@soyad", "@mail", "@tel", "@adres", "@kulad", "@tc","@id" };
                    if (comboBox1.SelectedIndex == 0)//müşteri ise
                    {       
                        string[] veri = { textBox4.Text, textBox7.Text, textBox5.Text, maskedTextBox1.Text.Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", ""), textBox6.Text, textBox1.Text, textBox3.Text,id };
                        string günmus = "update tbl_musteri set m_ad = @ad ,m_soyad = @soyad , m_mail = @mail , m_tel = @tel , m_adres = @adres ,m_kulad = @kulad , m_tc = @tc where m_id = @id";
                        a.parametrelisorgu(veri, d, günmus);
                    }
                    else //admin ise
                    {
                        string[] veri = { textBox4.Text, textBox7.Text, textBox5.Text, maskedTextBox1.Text.Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", ""), textBox6.Text, textBox1.Text, textBox3.Text,id };
                        string günad = "update tbl_admin set a_ad = @ad ,a_soyad = @aoyad , a_mail = @mail , a_tel = @tel , a_adres = @adres ,a_kulad = @kulad , a_tc = @tc where a_id = @id";
                        a.parametrelisorgu(veri, d, günad);
                    }
                    //tbl_kullanici eklenmesi lazım
                    string[] veri1 = { textBox1.Text, a.Md5pass(textBox2.Text), comboBox1.SelectedIndex.ToString() };
                    string eklekul = "update tbl_kullanici set k_ad = @ad,k_pass = @pass,k_tür = @tür where k_ad = @ad";
                    string[] d1 = { "@ad", "@pass", "@tür" };
                    a.parametrelisorgu(veri1, d1, eklekul);
                    ;
                    MessageBox.Show("başarı ile güncellendi");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("güncelleme hatası");
                }
            }


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(güncel)
            {
            sqlcon sqlcon = new sqlcon();
            if (sqlcon.kullanıcıad_kontrol(textBox1.Text) &&güncel)
            {
                label3.Text = "kullanıcı adı kullanılmış başka bir ad seçin";
            }
            else
            {
                label3.Text = "kullanılabilir";  

            }
            if(textBox1.Text =="")
            {
                label3.Text = "Kullanıcı adı giriniz";
            }
            else if(textBox1.TextLength < 4)
            {
                label3.Text = "Kullanıcı adınız en az 4 karekter olmalıdır";
            }
            }

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            sqlcon sqlcon = new sqlcon();
            if (sqlcon.sifre_kontrol(textBox2.Text))
            {
                label4.Text = "şifre geçerli";
            }
            else
            {
                label4.Text = "lütfen şifreinizde büyük küçük harf sembol ve sayı kullanınız";
            }
            if (textBox2.Text == "")
            {
                label4.Text = "lütfen şifre giriniz";
            }
            
     
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void üye_kayıt_Load(object sender, EventArgs e)
        {
             
            if (sqlcon.durum =="ekle")
            {
                label9.Text = "MÜŞTERİ EKLE";
                button2.Text = "EKLE";
                güncel = true;
            }
            else if(sqlcon.durum =="güncelle")
            {
                label9.Text = "MÜŞRETİ GÜNCELLE";
                button2.Text = "GÜNCELLE";
                //verileri çek ve textboxlara yaz
                güncel=false;
                textBox1.Text = kulad;
                textBox4.Text = ad;
                textBox7.Text = soyad;
                textBox3.Text = tc;
                maskedTextBox1.Text = tel;
                textBox5.Text = mail;
                textBox6.Text = adres;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

       
    }
}
