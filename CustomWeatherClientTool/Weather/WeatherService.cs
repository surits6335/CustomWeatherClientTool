using CustomWeatherClientTool.Location;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomWeatherClientTool.Weather
{
    public class WeatherService
    {
        public async Task<Weather> GetWeatherDetails(CoOrdinate coOrdinate)
        {
            var currentWeather = new Weather();
            try
            {
                if (coOrdinate != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        var url = $"https://api.open-meteo.com/v1/forecast?latitude={coOrdinate.Lat}&longitude={coOrdinate.Lng}&current_weather=true";
                        using (var response = await httpClient.GetAsync(url))
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            var parsedObject = JObject.Parse(apiResponse);
                            var currentWeatherJson = parsedObject["current_weather"].ToString();
                            currentWeather = JsonConvert.DeserializeObject<Weather>(currentWeatherJson);
                            currentWeather.IsAccurate = true;
                        }
                    }
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
