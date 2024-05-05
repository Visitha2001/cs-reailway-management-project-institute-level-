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

namespace SL_Railway
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=VISITHANR;Initial Catalog=RailwayUsers;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO [dbo].[Users_RWSL]
                        ([Username],[Email],[Password])
                        VALUES
                        (@Username, @Email, @Password)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox3.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Registration Successfull !");

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";

                    LoginForm lForm = new LoginForm();
                    lForm.Show();
                    this.Hide();
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            LoginForm lForm = new LoginForm();
            lForm.Show();
            this.Hide();
        }
    }
}
