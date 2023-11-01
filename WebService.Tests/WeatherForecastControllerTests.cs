using DataLayer;
using System.Net;
using System.Net.Http.Json;

namespace WebService.Tests;

// Example for Controller tests
public class WeatherForecastControllerTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly WebAppFactoryFixture _fixture;
    private readonly HttpClient _client;

    public WeatherForecastControllerTests(WebAppFactoryFixture fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient();
    }

    [Fact]
    public async Task GetWeatherForecastReturnsCorrectCount()
    {
        var response = await _client.GetAsync("/WeatherForecast");
        var weatherForecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
        Assert.Equal(5, weatherForecasts.Count);
    }

    [Fact]
    public async Task GetWeatherForecastReturnsCorrectStatusCode()
    {
        var response = await _client.GetAsync("/WeatherForecast");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWeatherForecastReturnsCorrectContentType()
    {
        var response = await _client.GetAsync("/WeatherForecast");
        Assert.Equal("application/json; charset=utf-8",
                     response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetWeatherForecastReturnsNotFoundForInvalidRoute()
    {
        var response = await _client.GetAsync("/InvalidRoute");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}