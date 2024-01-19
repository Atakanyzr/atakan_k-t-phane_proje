using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace atakan_kütüphane_proje
{
    public partial class öğrenci_ekle : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
        public öğrenci_ekle()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(isimTextBox.Text) ||
                string.IsNullOrWhiteSpace(soyisimTextBox.Text) ||
                !erkekRadioButton.Checked && !kadinRadioButton.Checked ||
                dogumTarihiDateTimePicker.Value == DateTime.MinValue ||
                string.IsNullOrWhiteSpace(epostaTextBox.Text) ||
                string.IsNullOrWhiteSpace(telefonTextBox.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // E-posta format kontrolü
            if (!IsValidEmail(epostaTextBox.Text))
            {
                MessageBox.Show("Geçersiz e-posta formatı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Telefon numarası format ve uzunluk kontrolü
            if (!IsValidPhoneNumber(telefonTextBox.Text))
            {
                MessageBox.Show("Geçersiz telefon numarası 10 karekterden oluşmalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Seçilen cinsiyeti radio butonlardan al
                    string selectedGender = erkekRadioButton.Checked ? "Erkek" : "Kadin";

                    // 'öğrenci' tablosuna veri eklemek için SQL komutu ve parametreleri
                    string insertDataSql = @"
                        INSERT INTO öğrenciler (isim, soyisim, cinsiyet, dogum_tarihi, eposta, telefon)
                        VALUES (@isim, @soyisim, @cinsiyet, @dogum_tarihi, @eposta, @telefon)";

                    using (SqlCommand insertDataCmd = new SqlCommand(insertDataSql, connection))
                    {
                        // Form kontrollerinden parametreleri ayarla
                        insertDataCmd.Parameters.AddWithValue("@isim", isimTextBox.Text);
                        insertDataCmd.Parameters.AddWithValue("@soyisim", soyisimTextBox.Text);
                        insertDataCmd.Parameters.AddWithValue("@cinsiyet", selectedGender);
                        insertDataCmd.Parameters.AddWithValue("@dogum_tarihi", dogumTarihiDateTimePicker.Value);
                        insertDataCmd.Parameters.AddWithValue("@eposta", epostaTextBox.Text);
                        insertDataCmd.Parameters.AddWithValue("@telefon", telefonTextBox.Text);

                        insertDataCmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarılı.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            anasayfa obj = new anasayfa();
            obj.Show();
            this.Hide();
        }

        // E-posta format kontrolü
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Telefon numarası format ve uzunluk kontrolü
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        }
    }
}
