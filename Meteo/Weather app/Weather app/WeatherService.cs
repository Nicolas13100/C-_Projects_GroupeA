using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather_App
{
    public class WeatherService
    {
        private static readonly HttpClient client = new HttpClient();
        
        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            string apiKey = "YOUR_API_KEY";  // Replace with your actual API key
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(json);
                return weather;
            }
            else
            {
                throw new Exception("Unable to retrieve weather data.");
            }
        }
    }
}