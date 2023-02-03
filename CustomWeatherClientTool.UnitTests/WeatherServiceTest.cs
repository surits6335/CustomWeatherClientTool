using CustomWeatherClientTool.Location;
using CustomWeatherClientTool.Weather;
using Moq.Protected;
using System.Net;

namespace CustomWeatherClientTool.UnitTests
{
    public class WeatherServiceTest
    {
        [Fact]
        public async Task TestGetWeather_With_Accurate_Data()
        {
            var coOrdinate = new CoOrdinate
            {
                City = "Kolkata",
                Lat = "22.5411",
                Lng = "88.3378"
            };

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""latitude"":22.5,""longitude"":88.375,""generationtime_ms"":0.21004676818847656,""utc_offset_seconds"":0,""timezone"":""GMT"",""timezone_abbreviation"":""GMT"",""elevation"":5.0,""current_weather"":{""temperature"":16.6,""windspeed"":4.3,""winddirection"":355.0,""weathercode"":0,""time"":""2023-02-03T22:00""}}"),
            };

            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);

            //act
            var weatherService = new WeatherService(httpClient);

            var result = await weatherService.GetWeatherDetails(coOrdinate);

            //assert
            Assert.True(result.IsAccurate);

            handlerMock.Protected().Verify(
              "SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
              ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task TestGetWeather_With_Errorneous_Data()
        {
            var coOrdinate = new CoOrdinate
            {
                City = "Kolkata",
                Lat = "22.5411",
                Lng = "88.3378"
            };

            var handlerMock = new Mock<HttpMessageHandler>();

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);

            //act
            var weatherService = new WeatherService(httpClient);

            var result = await weatherService.GetWeatherDetails(coOrdinate);

            //assert
            Assert.False(result.IsAccurate);

            handlerMock.Protected().Verify(
              "SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
              ItExpr.IsAny<CancellationToken>());
        }
    }
}
