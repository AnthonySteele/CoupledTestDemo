using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WeatherService;
using WeatherService.Controllers;

namespace TestsWithHost;

public class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // this is where services that have concrete dependencies
            // are replaced by fakes/mocks

            RemoveService<IWeatherForecastDataStore>(services);
            services.AddSingleton<IWeatherForecastDataStore>(new FakeWeatherForecastDataStore());
        });
    }

    private void RemoveService<T>(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }
}