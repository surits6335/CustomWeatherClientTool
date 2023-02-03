namespace CustomWeatherClientTool.Weather
{
    public class Weather
    {
        public decimal Temperature { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal WindDirection { get; set; }
        public int WeatherCode { get; set; }
        public DateTime Time { get; set; }
        public bool IsAccurate { get; set; }
    }
}
