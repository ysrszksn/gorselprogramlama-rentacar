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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 5;
            if(progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                Application.EnableVisualStyles();
                login login = new login();
                login.Show();
                this.Hide();
                
            }
        }
 
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
           
            progressBar1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#cf1f46"); ;
        }
    }
}
