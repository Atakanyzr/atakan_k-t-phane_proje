using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace atakan_kütüphane_proje
{
    public partial class kitap_görüntüle : Form
    {
        public kitap_görüntüle()
        {
            InitializeComponent();
        }

        private void kitap_görüntüle_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from Kitaplar";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            guna2DataGridView1.DataSource = ds.Tables[0];

        }
        int id;
        Int64 rowId;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from Kitaplar where id= " + id + "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            rowId = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

            kitapadı.Text = ds.Tables[0].Rows[0][4].ToString(); 
            yazaradı.Text = ds.Tables[0].Rows[0][5].ToString();
            kitapturu.Text = ds.Tables[0].Rows[0][1].ToString();
            icerikturu.Text = ds.Tables[0].Rows[0][2].ToString();
            dıl.Text = ds.Tables[0].Rows[0][3].ToString();





        }

        private void btnıptal_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            if (txtBookName.Text != "")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from Kitaplar where kitap_adi like '" + txtBookName.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from Kitaplar";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                guna2DataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void BtnYenile_Click(object sender, EventArgs e)
        {
            kitap_görüntüle obj = new kitap_görüntüle();
            obj.Show();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu kaydı güncellemek istediğinize emin misiniz?", "Güncelleme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               
            }
            string kitapTuru = kitapturu.Text;
            string icerikTuru = icerikturu.Text;
                string dil = dıl.Text;
            string kitapAdi = kitapadı.Text;
            string yazarAdi = yazaradı.Text;
           

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "update Kitaplar set kitap_turu='" + kitapTuru + "',icerik_turu='" + icerikTuru + "',dil='" + dil + "',kitap_adi='" + kitapAdi + "',yazar_adi='" + yazarAdi + "' where id=" + rowId + "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


        }

        private void btnsıl_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show("Bu kaydı silmek istediğinize emin misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "delete from Kitaplar where id=" + rowId + "";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
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

