namespace WeatherService.Controllers;

public interface IWeatherForecastDataStore
{
    public List<WeatherForecast> GetForecast(DateOnly startDate, int days);

}