using Common.Domain;
using DataLayer;
using DataLayer.Infrastructure;
using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WebService.Tests;

// Setup for the test container database
public class WebAppFactoryFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private PostgreSqlContainer _dbContainer; // The PostgreSQL container instance
    private readonly ITestOutputHelper _testOutputHelper; // The test output helper instance

    public WebAppFactoryFixture()
    {
        _dbContainer = new PostgreSqlBuilder().Build(); // Building a new PostgreSQL container instance
        _testOutputHelper = new TestOutputHelper(); // Initializing a new test output helper instance

    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = _dbContainer.GetConnectionString(); // Getting the connection string from the PostgreSQL container instance
        base.ConfigureWebHost(builder);
        builder.ConfigureLogging(x =>
        {
            x.ClearProviders(); // Clearing all existing logging providers
            x.SetMinimumLevel(LogLevel.Warning); // Setting the minimum log level to Warning
            x.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(_testOutputHelper)); // Adding a singleton logger provider with an XUnitLoggerProvider instance
        });
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>)); // Removing all existing DbContextOptions for AppDbContext from the service collection
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString); // Using Npgsql as the database provider with the connection string from the PostgreSQL container instance
            });
        });
    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync(); // Starting the PostgreSQL container instance

        using (var scope = Services.CreateScope()) // Creating a new service scope
        {
            var scopedServices = scope.ServiceProvider; // Getting the service provider from the service scope
            var cntx = scopedServices.GetRequiredService<AppDbContext>(); // Getting an AppDbContext instance from the service provider

            await cntx.Database.EnsureCreatedAsync(); // Ensuring that the database for this context is created

            // Add predefined movies

            #region Movie
            var movie1 = new Movie
            {
                Id = "1",
                Type = "Feature Film",
                Title = "The Shawshank Redemption",
                OriginalTitle = "The Shawshank Redemption",
                IsAdult = false,
                StartYear = "1994",
                Runtime = 142,
                Genres = "Drama",
                Rating = 9.3m,
                Votes = 2400000
            };

            var movie2 = new Movie
            {
                Id = "2",
                Type = "Feature Film",
                Title = "The Godfather",
                OriginalTitle = "The Godfather",
                IsAdult = false,
                StartYear = "1972",
                Runtime = 175,
                Genres = "Crime, Drama",
                Rating = 9.2m,
                Votes = 1700000
            };

            var movie3 = new Movie
            {
                Id = "3",
                Type = "Feature Film",
                Title = "The Dark Knight",
                OriginalTitle = "The Dark Knight",
                IsAdult = false,
                StartYear = "2008",
                Runtime = 152,
                Genres = "Action, Crime, Drama",
                Rating = 9.0m,
                Votes = 2400000
            };
            #endregion

            cntx.Movies.AddRange(movie1, movie2, movie3);
            await cntx.SaveChangesAsync();
        }
    }
    public async Task DisposeAsync() => await _dbContainer.StopAsync(); // Stopping the PostgreSQL container instance.
}