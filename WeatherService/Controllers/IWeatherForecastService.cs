namespace WeatherService.Controllers;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get();
}