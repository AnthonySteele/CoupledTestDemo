namespace WeatherService.Controllers;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastDataStore _forecastDataStore;

    public WeatherForecastService(IWeatherForecastDataStore forecastDataStore)
    {
        _forecastDataStore = forecastDataStore;
    }

    public IEnumerable<WeatherForecast> Get()
    {
        return _forecastDataStore.GetForecast(DateOnly.FromDateTime(DateTime.Today), 5);
    }
}