using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using WeatherService;
using WeatherService.Controllers;

namespace TestsWithMocks;

public class WeatherForecastControllerSociableTests
{
    [Fact]
    public void Controller_Should_ReturnData()
    {
        var forecastDataStore = CreateMockWeatherForecastDataStore();
        var controller = new WeatherForecastController(new NullLogger<WeatherForecastController>(), new WeatherForecastService(forecastDataStore));

        var response = controller.Get();

        Assert.NotNull(response);
    }

    [Fact]
    public void Controller_Should_ReturnExpectedData()
    {
        var forecastDataStore = CreateMockWeatherForecastDataStore();
        var controller = new WeatherForecastController(new NullLogger<WeatherForecastController>(), new WeatherForecastService(forecastDataStore));

        var response = controller.Get();

        Assert.NotEmpty(response);
        Assert.Equal("Testy", response.First().Summary);
    }

    private static IWeatherForecastDataStore CreateMockWeatherForecastDataStore()
    {
        var forecastDataStore = Substitute.For<IWeatherForecastDataStore>();
        forecastDataStore.GetForecast(new DateOnly(1, 1, 1), 5).ReturnsForAnyArgs(new List<WeatherForecast>
        {
            new()
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Summary = "Testy",
                TemperatureC = 20
            }
        });
        return forecastDataStore;
    }
}