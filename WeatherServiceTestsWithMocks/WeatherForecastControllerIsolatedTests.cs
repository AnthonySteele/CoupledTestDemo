using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using WeatherService;
using WeatherService.Controllers;

namespace TestsWithMocks;

public class WeatherForecastControllerIsolatedTests
{
    [Fact]
    public void Controller_Should_ReturnData()
    {
        var service = Substitute.For<IWeatherForecastService>();
        service.Get().Returns(new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Summary = "Testy",
                TemperatureC = 20
            }
        });

        var controller = new WeatherForecastController(new NullLogger<WeatherForecastController>(), service);

        var response = controller.Get();

        Assert.NotNull(response);
        Assert.NotEmpty(response);

        Assert.Equal("Testy", response.First().Summary);
    }

    [Fact]
    public void Service_Should_ReturnData()
    {
        var repo = Substitute.For<IWeatherForecastDataStore>();
        repo.GetForecast(Arg.Any<DateOnly>(), Arg.Any<int>())
            .Returns(new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Summary = "Testy",
                TemperatureC = 20
            }
        });

        var service = new WeatherForecastService(repo);

        var response = service.Get();

        Assert.NotNull(response);
        Assert.NotEmpty(response);

        Assert.Equal("Testy", response.First().Summary);
    }
}