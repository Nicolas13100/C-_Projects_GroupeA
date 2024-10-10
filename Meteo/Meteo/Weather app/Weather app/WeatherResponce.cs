namespace Weather_App
{
    public class WeatherResponse
    {
        public Main? Main { get; set; }  // Nullable to avoid the warning
        public Weather[]? Weather { get; set; }  // Nullable array
        public string? Name { get; set; }  // Nullable string
    }

    public class Main
    {
        public float Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class Weather
    {
        public string? Description { get; set; }  // Nullable string
        public string? Icon { get; set; }  // Nullable string
    }
}