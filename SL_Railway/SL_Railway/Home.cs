using Newtonsoft.Json;
using SL_Railway.Trains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SL_Railway
{
    public partial class Home : Form
    {
        private HttpClient httpClient;
        private const string apiUrl = "https://localhost:7239/api/Trains";

        public Home()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            dataGridView1.AutoGenerateColumns = true;
            RefreshData();
        }

        private async void RefreshData()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var trains = JsonConvert.DeserializeObject<List<TrainsDTO>>(jsonResponse);
                    dataGridView1.DataSource = trains;
                }
                else
                {
                    labelError.Text = "Failed to read data from the API. Status code: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                labelError.Text = "An error occurred: " + ex.Message;
            }
        }

        private async Task<List<TrainsDTO>> SearchTrains(string searchCriteria)
        {
            try
            {
                string url = $"{apiUrl}?trainName={searchCriteria}";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var allTrains = JsonConvert.DeserializeObject<List<TrainsDTO>>(jsonResponse);

                    var matchingTrains = allTrains.Where(train => train.trainName.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (matchingTrains.Count == 0)
                    {
                        labelError.Text = "No trains found for the given search criteria.";
                    }

                    return matchingTrains;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    labelError.Text = $"Failed to search trains. Status code: {response.StatusCode}. Error message: {errorMessage}";
                    return null;
                }
            }
            catch (Exception ex)
            {
                labelError.Text = $"An error occurred during search: {ex.Message}";
                return null;
            }
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            string searchCriteria = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(searchCriteria))
            {
                try
                {
                    var searchResults = await SearchTrains(searchCriteria);
                    if (searchResults != null && searchResults.Count > 0)
                    {
                        dataGridView1.DataSource = searchResults;
                        labelError.Text = "Results found.";
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        labelError.Text = "No results found.";
                    }
                }
                catch (Exception ex)
                {
                    labelError.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                labelError.Text = "Please enter search criteria.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private string connectionString = "Data Source=VISITHANR;Initial Catalog=RailwayUsers;Integrated Security=True;Encrypt=False";
        private void button1_Click(object sender, EventArgs e)
        {
            int trainId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["trainId"].Value);
            string trainName = dataGridView1.SelectedRows[0].Cells["trainName"].Value.ToString();

            string departureLocation = textBox2.Text;
            string destination = textBox3.Text;
            DateTime date = Convert.ToDateTime(textBox5.Text);
            TimeSpan time = TimeSpan.Parse(textBox6.Text);
            int numberOfSeats = Convert.ToInt32(textBox4.Text);

            if (numberOfSeats <= 0 || numberOfSeats > 5)
            {
                labelError.Text = "Number of seats must be between 1 and 5.";
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO SeatReservations (TrainId, TrainName, DepartureLocation, Destination, Date, Time, NumberOfSeats) " +
                                 "VALUES (@TrainId, @TrainName, @DepartureLocation, @Destination, @Date, @Time, @NumberOfSeats)";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@TrainId", trainId);
                    command.Parameters.AddWithValue("@TrainName", trainName);
                    command.Parameters.AddWithValue("@DepartureLocation", departureLocation);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Time", time);
                    command.Parameters.AddWithValue("@NumberOfSeats", numberOfSeats);

                    command.ExecuteNonQuery();
                }

                labelError.Text = "Seat reservation successful.";
            }
            catch (Exception ex)
            {
                labelError.Text = "Error: " + ex.Message;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reserved reserved = new Reserved();
            reserved.Show();
        }
    }
}