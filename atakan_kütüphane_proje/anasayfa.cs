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
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }

        private void eXİTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kitapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kitap_ekle obj = new kitap_ekle();
            obj.Show();
            this.Hide();
        }

        private void kitapGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kitap_görüntüle obj = new kitap_görüntüle();
            obj.Show();
            this.Hide();
        }

        private void öğrenciEkleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            öğrenci_ekle obj = new öğrenci_ekle();
            obj.Show();
            this.Hide();
        }

        private void öğrenciGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            öğrenci_görüntüle obj = new öğrenci_görüntüle();
            obj.Show();
            this.Hide();
        }

        private void kitaplarıİadeEtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            kitap_iade obj = new kitap_iade();
            obj.Show();
            this.Hide();
        }

        private void kitapÖdünçVerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kitap_ver obj = new kitap_ver();
            obj.Show();
            this.Hide();
        }


    } 
}

        
    
       

