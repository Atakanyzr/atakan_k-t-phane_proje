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
using System.Globalization;

namespace atakan_kütüphane_proje
{
    public partial class öğrenci_görüntüle : Form
    {
        

        public öğrenci_görüntüle()
        {
            InitializeComponent();
        }

        private void öğrenci_görüntüle_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from öğrenciler";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            guna2DataGridView2.DataSource = ds.Tables[0];
        }
        int id;
        Int64 rowId;
        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                id = int.Parse(guna2DataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                panel2.Visible = true;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from öğrenciler where id=" + id + "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                rowId = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

                isimTextBox.Text = ds.Tables[0].Rows[0][1].ToString();
                soyisimTextBox.Text = ds.Tables[0].Rows[0][2].ToString();
                cinsiyet.Text = ds.Tables[0].Rows[0][3].ToString();
                txtTarih.Text = DateTime.Parse(ds.Tables[0].Rows[0][4].ToString()).ToString("dd.MM.yyyy");
                mailTextBox.Text = ds.Tables[0].Rows[0][5].ToString();
                telefonTextBox.Text = ds.Tables[0].Rows[0][6].ToString();
            }
        }

        private void btnıptal_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            
        }

        private void txtöğrenciadı_TextChanged(object sender, EventArgs e)
        {
            if (txtöğrenciadı.Text != "")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from öğrenciler where ad like '" + txtöğrenciadı.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from öğrenciler";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                guna2DataGridView2.DataSource = ds.Tables[0];
            }
        }

        private void Yenile_Click(object sender, EventArgs e)
        {
            öğrenci_görüntüle obj = new öğrenci_görüntüle();
            obj.Show();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Öğrenci bilgilerini güncellemek istediğinize emin misiniz?", "Güncelleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String isim = isimTextBox.Text;
                String soyisim = soyisimTextBox.Text;
                String cinsiyet = this.cinsiyet.Text;
                String tarih = txtTarih.Text;
                String mail = mailTextBox.Text;
                String telefon = telefonTextBox.Text;

                DateTime parsedDate;
                if (DateTime.TryParseExact(tarih, "dd.MM.yyyy", null, DateTimeStyles.None, out parsedDate))
                {
                    String sqlCommand = "update öğrenciler set isim='" + isim + "', soyisim='" + soyisim + "', cinsiyet='" + cinsiyet + "', dogum_tarihi='" + parsedDate.ToString("yyyy-MM-dd") + "', eposta='" + mail + "', telefon='" + telefon + "' where id=" + this.rowId + "";

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Öğrenci bilgileri güncellendi.", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panel2.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz tarih formatı. Lütfen 'dd.MM.yyyy' formatında bir tarih girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnsıl_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Öğrenci kaydını silmek istediğinize emin misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String sqlCommand = "delete from öğrenciler where id=" + this.rowId + "";

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand(sqlCommand, con);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Öğrenci kaydı silindi.", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panel2.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            anasayfa obj = new anasayfa();
            obj.Show();
            this.Hide();
        }
    }
}




