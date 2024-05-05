using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using AUD_AppRailWayVNR.Trains;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AUD_AppRailWayVNR
{
    public partial class Form1 : Form
    {
        private HttpClient httpClient;
        private const string apiUrl = "https://localhost:7239/api/Trains";

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            dataGridView1.AutoGenerateColumns = true;
            RefreshData();
        }

        public async void RefreshData()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("JSON Response: " + jsonResponse);

                    var trains = JsonConvert.DeserializeObject<List<TrainsDTO>>(jsonResponse);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = trains;
                }
                else
                {
                    labelError.Text = "Failed to read data from the API. Status code: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                labelError.Text = "A error occurred: " + ex.Message;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var trainId = int.Parse(textBox1.Text);
            var trainName = textBox2.Text;
            var startLocation = textBox3.Text;
            var endLocation = textBox4.Text;
            var startTime = textBox5.Text;
            var endTime = textBox6.Text;
            var capacity = int.Parse(textBox7.Text);
            var distance = double.Parse(textBox8.Text);
            var price = double.Parse(textBox9.Text);

            var trainData = new
            {
                trainId,
                trainName,
                startLocation,
                endLocation,
                startTime,
                endTime,
                capacity,
                distance,
                price
            };

            var jsonData = JsonConvert.SerializeObject(trainData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();

                    labelError.Text = "Data added successfully!";
                    ClearTextBoxes();
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                labelError.Text = "Error: " + ex.Message;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshData();
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

        private void labelError_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Delete delete = new Delete();
            delete.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Update update = new Update();
            update.ShowDialog();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();

                Login loginForm = new Login();
                loginForm.Show();
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this train record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int selectedTrainId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["trainId"].Value);
                        HttpResponseMessage response = await httpClient.DeleteAsync($"{apiUrl}/{selectedTrainId}");

                        if (response.IsSuccessStatusCode)
                        {
                            labelError.Text = "Train data deleted successfully!";
                            RefreshData();
                        }
                        else
                        {
                            labelError.Text = "Failed to delete train data. Status code: " + response.StatusCode;
                        }
                    }
                    else
                    {}
                }
                else
                {
                    labelError.Text = "Please select a row to delete.";
                }
            }
            catch (Exception ex)
            {
                labelError.Text = "Error: " + ex.Message;
            }
        }
    }
}