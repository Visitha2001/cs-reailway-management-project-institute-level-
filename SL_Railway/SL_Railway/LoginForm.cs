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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
            string password = textBox3.Text;

            string connectionString = "Data Source=VISITHANR;Initial Catalog=RailwayUsers;Integrated Security=True;Encrypt=False";
            string query = "SELECT COUNT(*) FROM Users_RWSL WHERE (Username = @UserInput OR Email = @UserInput) AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserInput", userInput);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Home home_page = new Home();
                        home_page.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username/email or password!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form1 sForm = new Form1();
            sForm.Show();
            this.Hide();
        }
    }
}
