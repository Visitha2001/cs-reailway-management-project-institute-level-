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
    public partial class Reserved : Form
    {
        string connectionString = "Data Source=VISITHANR;Initial Catalog=RailwayUsers;Integrated Security=True;Encrypt=False";
        public Reserved()
        {
            InitializeComponent();
        }

        private void Reserved_Load(object sender, EventArgs e)
        {
            PopulateBookingsGrid();
        }

        private void PopulateBookingsGrid()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM SeatReservations";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this reservation?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                        int reservationId = Convert.ToInt32(selectedRow.Cells["ReservationID"].Value);
                        DeleteReservation(reservationId);
                        PopulateBookingsGrid();

                        MessageBox.Show("Reservation cancelled successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to cancel.");
            }
        }
        private void DeleteReservation(int reservationId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM SeatReservations WHERE ReservationID = @ReservationID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ReservationID", reservationId);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
