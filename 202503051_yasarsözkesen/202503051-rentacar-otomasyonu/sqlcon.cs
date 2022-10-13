using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace _202503051_rentacar_otomasyonu
{
    internal class sqlcon
    {
		SqlConnection con;
		SqlDataAdapter da;
		SqlCommand cmd;
		SqlDataReader dr;
		DataSet ds;

		public static string durum = "ekle";
		public static string durum1 = "ekle";
		string sql = "server=.; Initial Catalog=202503051;Integrated Security=SSPI";


		public string Md5pass(string sifre)
        {
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte [] dizi =Encoding.UTF8.GetBytes(sifre);
			dizi = md5.ComputeHash(dizi);
			StringBuilder sb = new StringBuilder();
            foreach (byte item in dizi)
            {
				sb.Append(item.ToString("x2").ToLower());
            }
			return sb.ToString();
        }
		public bool login_kontrol (string kullanıcı,string sifre)
        {
			string pass = Md5pass(sifre);
			string sorgu = "SELECT * FROM tbl_kullanici where [k_ad]=@user AND [k_pass]=@pass";
			con = new SqlConnection(sql);
			cmd = new SqlCommand(sorgu, con);
			cmd.Parameters.AddWithValue("@user", kullanıcı);
			cmd.Parameters.AddWithValue("@pass", pass);
			con.Open();
			dr = cmd.ExecuteReader();
			if (dr.Read())
			{
				con.Close();
				return true;
				
			}
			else
			{
				con.Close();
				return false;
			}
			
			
        }
		public int yetki (string kullanıcı, string sifre)
        {
			string pass = Md5pass(sifre);
			string sorgu = "Select k_tür FROM tbl_kullanici where [k_ad]=@user AND [k_pass]=@pass";
			int cevap;
			con = new SqlConnection(sql);
			cmd = new SqlCommand(sorgu, con);
			cmd.Parameters.AddWithValue("@user", kullanıcı);
			cmd.Parameters.AddWithValue("@pass", pass);
			con.Open();
			cevap = (int) cmd.ExecuteScalar();
			con.Close();

			return cevap;
        }
		public bool kullanıcıad_kontrol(string kullanıcı)
        {
			string sorgu = "SELECT * FROM tbl_kullanici where [k_ad]=@user";
			con = new SqlConnection(sql);
			cmd = new SqlCommand(sorgu, con);
			cmd.Parameters.AddWithValue("@user", kullanıcı);
			con.Open();
			dr = cmd.ExecuteReader();
			if (dr.Read())
			{
				con.Close();
				return true;

			}
			else
			{
				con.Close();
				return false;
			}
		}
		public bool sifre_kontrol(string sifre)
        {		
			//en az 1 büyük harf 1 küçük harf 1 sayı 1 sembol
			int sayı=0;
			int büyük=0;
			int küçük=0;
			int sembol=0;       
            foreach (char item in sifre)
            {
				if(char.IsDigit(item))
                {
					sayı++;
                }
				else if(char.IsUpper(item))
                {
					büyük++;
                }
				if(char.IsLower(item))
                {
					küçük++;
                }
				else if(char.IsPunctuation(item))
                {
					sembol++;
                }	
            }
            if (sayı >0 && büyük>0 && küçük>0 && sembol>0)
            {
				return true;
            }

			return false;

		}
		public void parametrelisorgu(string[] veri,string[] values,string sorgu)
        {
			/*
			 string[] d = { "@plaka", "@marka", "@model", "@yakıt", "@vites", "@fiyat","@teslim"};
              string[] veri = {textBox1.Text , textBox2.Text , textBox3.Text , comboBox2.Text, comboBox1.Text , textBox4.Text ,"N"};
              string eklearac = "INSERT INTO tbl_arac (a_plaka,a_marka,a_model,a_yakıt,a_vites,a_fiyat,a_teslim) values (@plaka,@marka, @model, @yakıt, @vites, @fiyat,@teslim)";
			 */
			con = new SqlConnection(sql);
			cmd = new SqlCommand(sorgu, con);
			int a = 0;
            foreach (string item in veri)
            {
				cmd.Parameters.AddWithValue(values[a], item);
					a++;
            }
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
		}	
		public int id(string kul)
        {
			string sorgu = "Select m_id FROM tbl_musteri where [m_kulad]=@user";
			int cevap;
			con = new SqlConnection(sql);
			cmd = new SqlCommand(sorgu, con);
			cmd.Parameters.AddWithValue("@user", kul);

			con.Open();
			cevap = (int)cmd.ExecuteScalar();
			con.Close();

			return cevap;
			
        }
		public int arid(string plaka)
		{
			string sorgu = "Select a_id FROM tbl_arac where [a_plaka]=@plaka";
			int cevap;
			con = new SqlConnection(sql);
			cmd = new SqlCommand(sorgu, con);
			cmd.Parameters.AddWithValue("@plaka", plaka);

			con.Open();
			cevap = (int)cmd.ExecuteScalar();
			con.Close();

			return cevap;

		}
		public void sorguyolla(string sorgu)
        {
			con = new SqlConnection(sql);
			cmd = new SqlCommand(sorgu, con);
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
		}
		public DataGridView griddoldur(DataGridView dgw1,string tablo)
        {
			con = new SqlConnection(sql);
			da = new SqlDataAdapter("select * from "+tablo,con);
			ds = new DataSet();
			con.Open();
			da.Fill(ds,tablo);
			dgw1.DataSource = ds.Tables[tablo];
			con.Close();
			return dgw1;

		}
		public DataGridView griddoldur1(DataGridView dgw1, string sorgu)
		{
			con = new SqlConnection(sql);
			da = new SqlDataAdapter(sorgu , con);
			ds = new DataSet();
			con.Open();
			da.Fill(ds, "tbl_siparis");
			dgw1.DataSource = ds.Tables["tbl_siparis"];
			con.Close();
			return dgw1;

		}
	}
}
