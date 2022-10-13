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
    public partial class araçekle : Form
    {
        public araçekle()
        {
            InitializeComponent();
        }

        public string id,plaka,marka,model,yıl,yakıt,vites,fiyat;
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.Text);
        }
        private void araçekle_Load(object sender, EventArgs e)
        {
            textBox5.Enabled = false;
            if (sqlcon.durum1 == "güncelle")
            {
                label8.Text = "GÜNCELLE";
                button1.Text = "GÜNCELLE";
                textBox5.Text = id;
                textBox1.Text = plaka;
                textBox2.Text = marka;
                textBox3.Text = model;
                textBox4.Text = fiyat;
                comboBox1.SelectedText = vites;
                comboBox2.SelectedText = yakıt;
            }
            else
            {
                label8.Text = "EKLE";
                button1.Text = "EKLE";
               
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool kontrol()
        {
            if(textBox1.Text !="" && textBox2.Text !="" && textBox3.Text!="" && textBox4.Text !="" && comboBox1.Text != "" && comboBox2.Text != "")
            {
                return true;
            }
            else
            {
                return false;   
            }         
        }
        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(sqlcon.durum1 =="ekle")
            {
                if(kontrol())
                {
                    sqlcon a = new sqlcon();
                    string[] d = { "@plaka", "@marka", "@model", "@yakıt", "@vites", "@fiyat","@teslim"};
                    string[] veri = {textBox1.Text , textBox2.Text , textBox3.Text , comboBox2.Text, comboBox1.Text , textBox4.Text ,"N"};
                    string eklearac = "INSERT INTO tbl_arac (a_plaka,a_marka,a_model,a_yakıt,a_vites,a_fiyat,a_teslim) values (@plaka,@marka, @model, @yakıt, @vites, @fiyat,@teslim)";
                    a.parametrelisorgu(veri, d, eklearac);
                    MessageBox.Show("işlem tamlandı");
                    temizle();
                
                }
                else
                {
                    MessageBox.Show("bilgileri eksiksiz giriniz");
                }
            }
            else
            {
                //araç güncelleme
                if(kontrol())
                {
                sqlcon a = new sqlcon();
                string[] d = { "@plaka", "@marka", "@model", "@yakıt", "@vites", "@fiyat", "@id" };
                string[] veri = { textBox1.Text, textBox2.Text, textBox3.Text, comboBox2.Text, comboBox1.Text, textBox4.Text, textBox5.Text };
                string günarac = "update tbl_arac set a_plaka = @plaka , a_marka = @marka ,a_model = @model,a_yakıt = @yakıt , a_vites=@vites,a_fiyat=@fiyat where a_id=@id "; 
                a.parametrelisorgu(veri, d, günarac);
                MessageBox.Show("işlem tamlandı");
                this.Close();
                }
                else
                {
                    MessageBox.Show("bilgileri eksiksiz giriniz");
                }
            }
               
        }

      
    }
}
