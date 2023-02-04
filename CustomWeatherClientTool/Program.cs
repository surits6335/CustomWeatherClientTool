using CustomWeatherClientTool.Location;
using CustomWeatherClientTool.Weather;

public class Program
{
    static async Task Main(string[] args)
    {
        var cityName = string.Join(" ", args);
        var locationService = new LocationService();
        var weatherService = new WeatherService(new HttpClient());
        var coordinate = await locationService.GetCoOrdinates(cityName);
        var weather = await weatherService.GetWeatherDetails(coordinate);

        if (coordinate != null && weather.IsAccurate)
        {
            Console.WriteLine($"Current Weather in {coordinate?.City} is");
            Console.WriteLine($"Temprature: {weather.Temperature}");
            Console.WriteLine($"Wind Speed: {weather.WindSpeed}");
            Console.WriteLine($"Wind Direction: {weather.WindDirection}");
            Console.WriteLine($"Weather Code: {weather.WeatherCode}");
            Console.WriteLine($"Time: {weather.Time}");
        }
    }
}
