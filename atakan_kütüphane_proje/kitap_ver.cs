using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System .Data .SqlClient ;

namespace atakan_kütüphane_proje
{
    public partial class kitap_ver : Form
    {
        private const int MaxKitapSayisi = 2; // Öğrencinin ödünç alabileceği maksimum kitap sayısı

        public kitap_ver()
        {
            InitializeComponent();
        }

        private void kitap_ver_Load(object sender, EventArgs e)
        {
            // Kitap adlarını ComboBox'a yükle
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("select kitap_adi from kitaplar", con);
            SqlDataReader Sdr = cmd.ExecuteReader();

            while (Sdr.Read())
            {
                kitap_adi.Items.Add(Sdr.GetString(0));
            }

            Sdr.Close();
            con.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "")
            {
                string eposta = guna2TextBox1.Text;
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;");
                SqlCommand cmd = new SqlCommand("select * from öğrenciler where eposta = @eposta", con);
                cmd.Parameters.AddWithValue("@eposta", eposta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    guna2TextBox2.Text = ds.Tables[0].Rows[0]["isim"].ToString();
                    guna2TextBox3.Text = ds.Tables[0].Rows[0]["eposta"].ToString();
                }
                else
                {
                    guna2TextBox2.Clear();
                    guna2TextBox3.Clear();
                    MessageBox.Show("Böyle bir öğrenci bulunamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                if (kitap_adi.SelectedIndex != -1)
                {
                    int kitapSayisi = CountKitapSayisi(guna2TextBox1.Text);

                    if (kitapSayisi < MaxKitapSayisi)
                    {
                        KitapOduncVer(guna2TextBox1.Text, kitap_adi.SelectedItem.ToString());
                        MessageBox.Show("Kitap başarıyla ödünç verildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Öğrenci en fazla " + MaxKitapSayisi + " kitap ödünç alabilir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen ödünç verilecek kitabı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Öğrenci bilgileri eksik veya hatalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int CountKitapSayisi(string eposta)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("select count(*) from trBook where e_posta = @eposta and kitap_adi is not null", con))
                {
                    cmd.Parameters.AddWithValue("@eposta", eposta);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private void KitapOduncVer(string eposta, string kitapAdi)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("insert into trBook (öğrenci_adi, e_posta, kitap_adi) values (@ogrenciAdi, @eposta, @kitapAdi)", con))
                {
                    cmd.Parameters.AddWithValue("@ogrenciAdi", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@eposta", eposta);
                    cmd.Parameters.AddWithValue("@kitapAdi", kitapAdi);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            kitap_ver obj = new kitap_ver();
            obj.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            anasayfa obj = new anasayfa();
            obj.Show();
            this.Hide();
        }
    }
}










