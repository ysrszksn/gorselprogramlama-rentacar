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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        int yanlıs = 0;
        int a, b;

        public static string usersession = "";
        public static int userpermission = -1;

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            if(textBox2.Text !="ŞİFRE")
            textBox2.PasswordChar = '*';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
  
        private void giris()
        {
           if(textBox3.Text =="")
            {
                güvenlik();
                MessageBox.Show("güvenlik kodu boş geçilemez");
                
            }
           else if(a+b == Convert.ToInt32(textBox3.Text))
            {
                sqlcon deneme = new sqlcon();
                if (deneme.login_kontrol(textBox1.Text, textBox2.Text))
                {
                    usersession =  textBox1.Text;
                    int yetki = deneme.yetki(textBox1.Text, textBox2.Text);

                    if (yetki == 1)//admin
                    {
                        admin a = new admin();
                        this.Hide();
                        a.Show();
                        userpermission = yetki;
                    }                  
                    else//kullanıcı
                    {
                        main a = new main();
                        this.Hide();
                        a.Show();
                    }


                }
                else
                {
                    güvenlik();
                    MessageBox.Show("giriş başarısız");
                    yanlıs++;
                    
                }
            }
           else
            {
                güvenlik();
                MessageBox.Show("güvenlik kodu hatalı!!");
            }
            
        }

        private void güvenlik()
        {
            Random random = new Random();
            a = random.Next() % 100;
            b = random.Next() % 100;
            label1.Text = a.ToString();
            label2.Text = b.ToString();
            
        }

        private void login_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#191919");
            textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#191919");
            textBox3.BackColor = System.Drawing.ColorTranslator.FromHtml("#191919");
            textBox1.Text = "KULLANICI ADI";
            textBox2.Text = "ŞİFRE";
            textBox2.PasswordChar = '\0';
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox3.TextAlign = HorizontalAlignment.Center;
            güvenlik();
            label1.Focus();
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text =="")
            {
                textBox1.Text = "KULLANICI ADI";
                textBox1.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "ŞİFRE";
                textBox2.PasswordChar = '\0';
                textBox2.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            //KULLANICI ŞİFRESİNİ "ŞİFRE" YAPARSA BOZULUYOR :)
            if (textBox2.Text == "ŞİFRE")
            {
                textBox2.Text = "";
                textBox2.PasswordChar = '*';
                textBox2.TextAlign = HorizontalAlignment.Left;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "KULLANICI ADI")
            {
                textBox1.Text = "";
                textBox1.TextAlign = HorizontalAlignment.Left;
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ÇOK YAKINDA HİZMETTE");//şifremi unuttum
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ÜYE OLMA EKRANINA GİDER");//yeni üye
        }

        private void button1_Click(object sender, EventArgs e)
        {
            giris();//enter ile giriş için
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            giris();
        }


    }
}
