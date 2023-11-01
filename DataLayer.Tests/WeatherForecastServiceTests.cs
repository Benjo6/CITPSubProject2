namespace DataLayer.Tests;

// Example of Service tests
public class WeatherForecastServiceTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly WebAppFactoryFixture _fixture; // The WebAppFactoryFixture instance
    private WeatherForecastService _service; // The WeatherForecastService instance

    // Constructor that takes a WebAppFactoryFixture instance
    public WeatherForecastServiceTests(WebAppFactoryFixture fixture)
    {
        _fixture = fixture; // Assigning the passed in WebAppFactoryFixture instance to the private field
        _service = new WeatherForecastService(_fixture.Context); // Initializing the WeatherForecastService instance with the AppDbContext instance from the WebAppFactoryFixture
    }

    //Test method to check if the GetWeatherForecasts method of the WeatherForecastService returns the correct count
    [Fact]
    public void GetWeatherForecastsReturnsCorrectCount()
    {
        var weatherForecasts = _service.GetWeatherForecasts();
        Assert.Equal(5, weatherForecasts.Count());
    }
}
