using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using WeatherService;
using WeatherService.Controllers;

namespace TestsWithHost;

public class TestContext
{
    public TestApplicationFactory Factory { get; } = new TestApplicationFactory();
    public HttpClient CreateClient() => Factory.CreateClient();

    public FakeWeatherForecastDataStore? FakeDataStore => Factory.Services.GetRequiredService<IWeatherForecastDataStore>() as FakeWeatherForecastDataStore;

    public async Task<HttpResponseMessage> GetForecast() => await CreateClient().GetAsync("/weatherforecast");

    public async Task<List<WeatherForecast>?> GetForecastTyped()
        => await CreateClient().GetFromJsonAsync<List<WeatherForecast>>("/weatherforecast");
}