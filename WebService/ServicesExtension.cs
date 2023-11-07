using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using DataLayer.Services.Contracts;

namespace WebService;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IWeatherForecastService, WeatherForecastService>();
        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        
        return serviceCollection;
    }
}