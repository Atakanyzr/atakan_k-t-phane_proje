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
using System.Data.Sql;


namespace atakan_kütüphane_proje
{
    public partial class giriş : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand cmd;
        public giriş()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-DOI94OU;Initial Catalog=library;Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT * FROM loginTable WHERE username='" + txtUsername.Text + "' AND pass ='" + txtPassword.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                this.Hide();
                anasayfa obj = new anasayfa();
                obj.Show();

            }
            else
            {
                this.Hide();
                hatamsj obj = new hatamsj();
                obj.Show();

            }

        }

      
        }
    }
    

