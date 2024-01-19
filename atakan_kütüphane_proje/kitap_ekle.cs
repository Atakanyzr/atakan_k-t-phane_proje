using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data .SqlClient ;
using System.Windows.Forms;

namespace atakan_kütüphane_proje
{
    public partial class kitap_ekle : Form
    {
        private string connectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";

        public kitap_ekle()
        {
            InitializeComponent();
        }

        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKitapAdi.Text) || string.IsNullOrWhiteSpace(txtYazarAdi.Text) ||
        string.IsNullOrWhiteSpace(GetSelectedRadioButtonText()) || string.IsNullOrWhiteSpace(GetSelectedToggleSwitchText(tsIcerikTuru)) ||
        cmbDil.SelectedItem == null)
            {
                MessageBox.Show("Lütfen tüm gerekli bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL sorgusu
                    string sql = "INSERT INTO Kitaplar (kitap_turu, icerik_turu, dil, kitap_adi, yazar_adi) VALUES (@kitapTuru, @icerikTuru, @dil, @kitapAdi, @yazarAdi)";

                    // Sorgu ve bağlantıyı birleştirme
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        // Parametreleri ekleme
                        cmd.Parameters.AddWithValue("@kitapAdi", txtKitapAdi.Text);
                        cmd.Parameters.AddWithValue("@kitapTuru", GetSelectedRadioButtonText());
                        cmd.Parameters.AddWithValue("@icerikTuru", GetSelectedToggleSwitchText(tsIcerikTuru));
                        cmd.Parameters.AddWithValue("@dil", cmbDil.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@yazarAdi", txtYazarAdi.Text);

                        // Sorguyu çalıştırma
                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kitap başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Kitap eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIptal_Click_Click(object sender, EventArgs e)
        {
            anasayfa obj = new anasayfa();
            obj.Show();
            this.Hide();
        }
        private string GetSelectedRadioButtonText()
        {
            if (rbDunyaKlasikleri.Checked)
                return rbDunyaKlasikleri.Text;
            else if (rbPsikoloji.Checked)
                return rbPsikoloji.Text;
            else if (rbDiger.Checked)
                return rbDiger.Text;
            else
                return "";
        }
        private string GetSelectedToggleSwitchText(Guna.UI2.WinForms.Guna2ToggleSwitch toggleSwitch)
        {
            return toggleSwitch.Checked ? "kurgu" : "kurgu değil";
        }
                }
            }

            
        
    
        