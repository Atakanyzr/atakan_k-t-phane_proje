using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atakan_kütüphane_proje
{
    public partial class hatamsj : Form
    {
        public hatamsj()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            giriş obj = new giriş();
            obj.Show();
        }
    }
}
