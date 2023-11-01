namespace DataLayer;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly AppDbContext _context;

    public WeatherForecastService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<WeatherForecast> GetWeatherForecasts()
    {
        return _context.WeatherForecasts.ToArray();
    }
}
