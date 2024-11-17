using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Weather_App;

namespace Weather_app
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient Client = new HttpClient();
        private WeatherService _weatherService = new WeatherService();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGetWeather_Click(object sender, EventArgs e)
        {
            string city = txtCity.Text.Trim();
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Please enter a city name.");
                return;
            }

            try
            {
                btnGetWeather.Enabled = false;
                WeatherResponse weather = await _weatherService.GetWeatherAsync(city);

                lblTemperature.Text = $"{weather.Main.Temp} °C";
                lblHumidity.Text = $"{weather.Main.Humidity} %";
                lblConditions.Text = weather.Weather[0].Description;

                // Optional: Load weather icon
                string iconUrl = $"http://openweathermap.org/img/wn/{weather.Weather[0].Icon}@2x.png";
                GeneralIcon.Load(iconUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                btnGetWeather.Enabled = true;
            }
        }
    }
}