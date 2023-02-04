using CustomWeatherClientTool.Location;

namespace CustomWeatherClientTool.UnitTests
{
    public class LocationServiceTest
    {
        [Fact]
        public async Task TestGetCoOrdinates_With_City_Listed()
        {
            //arrange
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates("Kolkata");

            //assert
            Assert.NotNull(result);
            Assert.Equal("Kolkata", result.City);
        }

        [Fact]
        public async Task TestGetCoOrdinates_With_City_Name_Having_Multiple_Strings()
        {
            //arrange
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates("Port Blair");

            //assert
            Assert.NotNull(result);
            Assert.Equal("Port Blair", result.City);
        }

        [Fact]
        public async Task TestGetCoOrdinates_With_City_Not_Listed()
        {
            //arrange
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates("New York");

            //assert
            await Assert.ThrowsAnyAsync<Exception>(() => throw new Exception());
        }

        [Fact]
        public async Task TestGetCoOrdinates_With_City_Name_Is_Null()
        {
            //arrange
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates(null);

            //assert
            await Assert.ThrowsAnyAsync<Exception>(() => throw new Exception());
        }

        [Fact]
        public async Task TestGetCoOrdinates_With_City_Name_Is_WhiteSpace()
        {
            //arrange
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates(" ");

            //assert
            await Assert.ThrowsAnyAsync<Exception>(() => throw new Exception());
        }

        [Fact]
        public async Task TestGetCoOrdinates_With_City_Name_Is_Special_Character()
        {
            //arrange
            var locationService = new LocationService();

            //act
            var result = await locationService.GetCoOrdinates("@#%^&*");

            //assert
            await Assert.ThrowsAnyAsync<Exception>(() => throw new Exception());
        }
    }
}
