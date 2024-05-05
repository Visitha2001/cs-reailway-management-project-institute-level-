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

namespace AUD_AppRailWayVNR
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
            string password = textBox3.Text;

            string connectionString = "Data Source=VISITHANR;Initial Catalog=RailwayUsers;Integrated Security=True;Encrypt=False";
            string query = "SELECT COUNT(*) FROM Admin WHERE (username = @username) AND password = @password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", userInput);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    conn.Open();

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Form1 home_page = new Form1();
                        home_page.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username/email or password!");
                        textBox1.Clear();
                        textBox3.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
