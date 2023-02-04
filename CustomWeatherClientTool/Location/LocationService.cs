using Newtonsoft.Json;

namespace CustomWeatherClientTool.Location
{
    public class LocationService
    {
        public async Task<CoOrdinate> GetCoOrdinates(string cityName)
        {
            var result = new CoOrdinate();
            try
            {
                if (string.IsNullOrWhiteSpace(cityName))
                    throw new Exception("Cityname needs to be provided");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\coordinates.json");
                var json = await File.ReadAllTextAsync(path);

                var coordinates = JsonConvert.DeserializeObject<List<CoOrdinate>>(json);

                result = coordinates.FirstOrDefault(c => c.City == cityName);

                if (result == null)
                    throw new Exception("City is not listed");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
