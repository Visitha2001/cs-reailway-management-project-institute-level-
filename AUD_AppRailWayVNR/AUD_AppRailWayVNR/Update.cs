using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AUD_AppRailWayVNR.Trains;
using Newtonsoft.Json;

namespace AUD_AppRailWayVNR
{
    public partial class Update : Form
    {
        private const string apiUrl = "https://localhost:7239/api/Trains";

        public Update()
        {
            InitializeComponent();
        }

        private async void buttonLoad_Click(object sender, EventArgs e)
        {
            int trainId;
            if (int.TryParse(textBox1.Text, out trainId))
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{trainId}");
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            var train = JsonConvert.DeserializeObject<TrainsDTO>(jsonResponse);

                            textBox2.Text = train.TrainName;
                            textBox3.Text = train.StartLocation;
                            textBox4.Text = train.EndLocation;
                            textBox5.Text = train.StartTime;
                            textBox6.Text = train.EndTime;
                            textBox7.Text = train.Capacity.ToString();
                            textBox8.Text = train.Distance.ToString();
                            textBox9.Text = train.Price.ToString();
                        }
                        else
                        {
                            MessageBox.Show($"Failed to load train details. Status code: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Train ID.");
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            int trainId = int.Parse(textBox1.Text);
            string trainName = textBox2.Text;
            string startLocation = textBox3.Text;
            string endLocation = textBox4.Text;
            string startTime = textBox5.Text;
            string endTime = textBox6.Text;
            int capacity = int.Parse(textBox7.Text);
            double distance = double.Parse(textBox8.Text);
            double price = double.Parse(textBox9.Text);

            var updatedTrain = new
            {
                TrainId = trainId,
                TrainName = trainName,
                StartLocation = startLocation,
                EndLocation = endLocation,
                StartTime = startTime,
                EndTime = endTime,
                Capacity = capacity,
                Distance = distance,
                Price = price
            };

            var jsonData = JsonConvert.SerializeObject(updatedTrain);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync($"{apiUrl}/{trainId}", content);
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show("Train data updated successfully!");
                    ClearTextBoxes();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating train data: {ex.Message}");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearTextBoxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

    }
}
