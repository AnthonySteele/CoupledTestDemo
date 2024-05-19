using WeatherService;
using WeatherService.Controllers;

namespace TestsWithHost;

public class FakeWeatherForecastDataStore : IWeatherForecastDataStore
{
    public List<WeatherForecast> GetForecast(DateOnly startDate, int days)
    {
        CallCount++;


        return Enumerable.Range(1, days)
            .Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                Summary = "Testy",
                TemperatureC = 20
            }).ToList();
    }

    public int CallCount { get; private set; }
    public void ResetCallCount() => CallCount = 0;
}