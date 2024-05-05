using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AUD_AppRailWayVNR
{
    public partial class Delete : Form
    {
        private const string apiUrl = "https://localhost:7239/api/Trains";

        public Delete()
        {
            InitializeComponent();
        }

        private async Task DeleteTrainById(int trainId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}/{trainId}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Train deleted successfully.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete train. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int trainId))
            {
                await DeleteTrainById(trainId);
            }
            else
            {
                MessageBox.Show("Please enter a valid train ID.");
            }
        }
    }
}
