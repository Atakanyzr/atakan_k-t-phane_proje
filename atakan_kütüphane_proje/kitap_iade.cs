using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace atakan_kütüphane_proje
{
    public partial class kitap_iade : Form
    {
        public kitap_iade()
        {
            InitializeComponent();
        }

        private void btnSerchStudent_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from trBook where e_posta = '" + txtEnterEnroll.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Öğrenci bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kitap_iade_Load(object sender, EventArgs e)
        {
            txtEnterEnroll.Clear();

        }

        string öğrenci_adi;
        string e_posta;
        Int64 rowid;


        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowid = Int64.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                öğrenci_adi = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                e_posta = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            txtBookName.Text = öğrenci_adi;
            txtBooklssudate.Text = e_posta;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();

                        cmd.CommandText = "UPDATE trBook SET kitap_adi = @kitapAdi WHERE id = @rowId";
                        cmd.Parameters.AddWithValue("@kitapAdi", kitap_adi.Text);
                        cmd.Parameters.AddWithValue("@rowId", rowid);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kitap iade edildi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme başarısız. Belirtilen ID bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            kitap_iade_Load(this, null);
        }

    

    private void btnRefresh_Click(object sender, EventArgs e)
        {
            kitap_iade obj = new kitap_iade();
            obj.Show();
            this.Hide();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            anasayfa obj = new anasayfa();
            obj.Show();
            this.Hide();
        }
    }
}







