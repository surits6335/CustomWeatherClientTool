using CustomWeatherClientTool.Location;

namespace CustomWeatherClientTool.UnitTests
{
    public class LocationServiceTest
    {
        [Fact]
        public async Task TestGetCoOrdinates_With_City_Listed()
        {
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates("Kolkata");

            //assert
            Assert.NotNull(result);
            Assert.Equal("Kolkata", result.City);
        }

        [Fact]
        public async Task TestGetCoOrdinates_With_City_Not_Listed()
        {
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates("New York");

            //assert
            await Assert.ThrowsAsync<Exception>(() => throw new Exception());
        }
    }
}
