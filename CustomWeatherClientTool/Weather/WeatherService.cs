using CustomWeatherClientTool.Location;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace CustomWeatherClientTool.Weather
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Weather> GetWeatherDetails(CoOrdinate coOrdinate)
        {
            var currentWeather = new Weather();
            try
            {
                if (coOrdinate != null)
                {
                    var url = $"https://api.open-meteo.com/v1/forecast?latitude={coOrdinate.Lat}&longitude={coOrdinate.Lng}&current_weather=true";

                    var response = await _httpClient.GetAsync(url);

                    var apiResponse = await response.Content.ReadAsStringAsync();
                    var parsedObject = JObject.Parse(apiResponse);
                    var currentWeatherJson = parsedObject["current_weather"].ToString();
                    currentWeather = JsonConvert.DeserializeObject<Weather>(currentWeatherJson);
                    currentWeather.IsAccurate = true;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            return currentWeather;
        }
    }
}
