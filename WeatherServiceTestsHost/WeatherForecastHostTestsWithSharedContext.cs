using System.Net;

namespace TestsWithHost;

public class WeatherForecastHostTestsWithSharedContext : IClassFixture<TestContext>
{
    private readonly TestContext _testContext;

    public WeatherForecastHostTestsWithSharedContext(TestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task GetForecast_Should_ReturnHttpSuccess()
    {
        var response = await _testContext.GetForecast();

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetForecast_Should_ReturnData()
    {
        var response = await _testContext.GetForecastTyped();

        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Assert.Equal("Testy", response.First().Summary);
    }

    [Fact]
    public async Task GetForecast_Should_CallFakeDatStore()
    {
        var fakeDataStore = _testContext.FakeDataStore;
        fakeDataStore!.ResetCallCount();

        await _testContext.GetForecastTyped();

        Assert.Equal(1, fakeDataStore.CallCount);
    }
}