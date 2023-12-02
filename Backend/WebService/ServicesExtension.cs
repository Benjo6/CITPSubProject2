using Common.Domain;
using DataLayer;
using DataLayer.Generics;
using DataLayer.Repositories;
using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using DataLayer.Services.Contracts;

namespace WebService;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAliasesService, AliasesService>();
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IEpisodesService, EpisodesService>();
        serviceCollection.AddScoped<IMoviesService, MoviesService>();
        serviceCollection.AddScoped<IPeopleService, PeopleService>();
        serviceCollection.AddScoped<ISearchService, SearchService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IBookmarkService, BookmarkService>();

        
        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        serviceCollection.AddScoped<IBookmarkMoviesRepository, BookmarkMoviesRepository>();
        serviceCollection.AddScoped<IBookmarkPersonalitiesRepository, BookmarkPersonalitiesRepository>();
        serviceCollection.AddScoped<IMoviesRepository, MoviesRepository>();
        serviceCollection.AddScoped<IPeopleRepository, PeopleRepository>();
        serviceCollection.AddScoped<IRolesRepository, RolesRepository>();
        serviceCollection.AddScoped<ISearchHistoriesRepository, SearchHistoriesRepository>();
        serviceCollection.AddScoped<IGenericRepository<Alias>, GenericRepository<Alias>>();
        serviceCollection.AddScoped<IGenericRepository<Episode>, GenericRepository<Episode>>();
        serviceCollection.AddScoped<IGenericRepository<User>, GenericRepository<User>>();   
        return serviceCollection;
    }
}