using System.Net;

namespace TestsWithHost;

public class WeatherForecastControllerHostTests
{
    [Fact]
    public async Task GetForecast_Should_ReturnData()
    {
        var factory = new TestApplicationFactory();

        var client = factory.CreateClient();

        var response = await client.GetAsync("/weatherforecast");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Testy", content);
    }
}