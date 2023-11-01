﻿using Meziantou.Extensions.Logging.Xunit;
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

namespace DataLayer.Tests;

// Setup for the test container database for Service/Repository tests
public class WebAppFactoryFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private PostgreSqlContainer _dbContainer; // The PostgreSQL container instance
    private readonly ITestOutputHelper _testOutputHelper; // The test output helper instance

    public IServiceScope Scope { get; private set; } // The service scope instance
    public AppDbContext Context => Scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Getting an AppDbContext instance from the service provider of the service scope

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

        // Create a new scope
        Scope = Services.CreateScope();
        var context = Scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Getting an AppDbContext instance from the service provider of the service scope

        await context.Database.EnsureCreatedAsync(); // Ensuring that the database for this context is created

        // Add 5 WeatherForecasts, Replace with all the fixed data we want for the testing
        for (int i = 0; i < 5; i++)
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i)), // Adjusting the date as needed
                TemperatureC = 20 + i, // Adjusting the temperature as needed
                Summary = $"Summary {i}" // Adjusting the summary as needed
            };

            context.WeatherForecasts.Add(weatherForecast); // Adding a new WeatherForecast instance to AppDbContext's WeatherForecasts DbSet 
        }

        await context.SaveChangesAsync(); // Saving all changes made in this context to the database.
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync(); // Stopping the PostgreSQL container instance.
        Scope.Dispose(); // Disposing of the service scope.
    }
}

