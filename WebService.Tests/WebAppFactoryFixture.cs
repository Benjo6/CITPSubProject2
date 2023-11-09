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
        var connectionString =
            _dbContainer.GetConnectionString(); // Getting the connection string from the PostgreSQL container instance
        base.ConfigureWebHost(builder);
        builder.ConfigureLogging(x =>
        {
            x.ClearProviders(); // Clearing all existing logging providers
            x.SetMinimumLevel(LogLevel.Warning); // Setting the minimum log level to Warning
            x.Services.AddSingleton<ILoggerProvider>(
                new XUnitLoggerProvider(
                    _testOutputHelper)); // Adding a singleton logger provider with an XUnitLoggerProvider instance
        });
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(
                typeof(DbContextOptions<AppDbContext>)); // Removing all existing DbContextOptions for AppDbContext from the service collection
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString); // Using Npgsql as the database provider with the connection string from the PostgreSQL container instance
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync(); // Starting the PostgreSQL container instance

        using (var scope = Services.CreateScope()) // Creating a new service scope
        {
            var scopedServices = scope.ServiceProvider; // Getting the service provider from the service scope
            var cntx = scopedServices
                .GetRequiredService<AppDbContext>(); // Getting an AppDbContext instance from the service provider

            await cntx.Database.EnsureCreatedAsync(); // Ensuring that the database for this context is created

            AddDummyData(cntx);

            cntx.Movies.AddRange();
            await cntx.SaveChangesAsync();
        }
    }

    public async Task DisposeAsync() => await _dbContainer.StopAsync(); // Stopping the PostgreSQL container instance.

    private static void AddDummyData(AppDbContext context)
    {
        // Add predefined movies
        var movies = new List<Movie>
        {
            new Movie
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
            },
            new Movie
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
            },
            new Movie
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
            }
        };

        // Add dummy data for Alias class
        var aliases = new List<Alias>
        {
            new Alias
            {
                Id = "1",
                MovieId = "1",
                Ordering = 1,
                Title = "Alias 1",
                Region = "Region 1",
                Language = "English",
                Types = "Type 1",
                Attributes = "Attribute 1",
                IsOriginal = true
            },
            new Alias
            {
                Id = "2",
                MovieId = "2",
                Ordering = 2,
                Title = "Alias 2",
                Region = "Region 2",
                Language = "Spanish",
                Types = "Type 2",
                Attributes = "Attribute 2",
                IsOriginal = false
            },
            new Alias
            {
                Id = "3",
                MovieId = "3",
                Ordering = 3,
                Title = "Alias 3",
                Region = "Region 3",
                Language = "French",
                Types = "Type 3",
                Attributes = "Attribute 3",
                IsOriginal = true
            }
        };

        // Add dummy data for BookmarkMovie class
        var bookmarkMovies = new List<BookmarkMovie>
        {
            new BookmarkMovie
            {
                UserId = "User1",
                AliasId = "1",
                BookmarkDate = DateOnly.FromDateTime(DateTime.Now),
                Note = "Bookmark Note 1"
            },
            new BookmarkMovie
            {
                UserId = "User2",
                AliasId = "2",
                BookmarkDate = DateOnly.FromDateTime(DateTime.Now),
                Note = "Bookmark Note 2"
            },
            new BookmarkMovie
            {
                UserId = "User3",
                AliasId = "3",
                BookmarkDate = DateOnly.FromDateTime(DateTime.Now),
                Note = "Bookmark Note 3"
            }
        };

        // Add dummy data for BookmarkPersonality class
        var bookmarkPersonalities = new List<BookmarkPersonality>
        {
            new BookmarkPersonality
            {
                UserId = "User1",
                PersonId = "Person1",
                BookmarkDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new BookmarkPersonality
            {
                UserId = "User2",
                PersonId = "Person2",
                BookmarkDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new BookmarkPersonality
            {
                UserId = "User3",
                PersonId = "Person3",
                BookmarkDate = DateOnly.FromDateTime(DateTime.Now)
            }
        };

        // Add dummy data for Episode class
        var episodes = new List<Episode>
        {
            new Episode
            {
                Id = "Episode1",
                SeriesId = "Series1",
                Season = 1,
                Episode1 = 1
            },
            new Episode
            {
                Id = "Episode2",
                SeriesId = "Series1",
                Season = 1,
                Episode1 = 2
            },
            new Episode
            {
                Id = "Episode3",
                SeriesId = "Series2",
                Season = 2,
                Episode1 = 1
            }
        };

        // Add dummy data for Person class
        var persons = new List<Person>
        {
            new Person
            {
                Id = "Person1",
                Name = "John Doe",
                BirthYear = "1975",
                DeathYear = "2021",
                Professions = "Actor, Director",
                KnownFor = "The Shawshank Redemption, The Godfather"
            },
            new Person
            {
                Id = "Person2",
                Name = "Jane Smith",
                BirthYear = "1980",
                DeathYear = null,
                Professions = "Actor",
                KnownFor = "The Dark Knight"
            },
            new Person
            {
                Id = "Person3",
                Name = "Alice Johnson",
                BirthYear = "1990",
                DeathYear = "2022",
                Professions = "Director",
                KnownFor = "Inception"
            }
        };

        // Add dummy data for RatingHistory class
        var ratingHistories = new List<RatingHistory>
        {
            new RatingHistory
            {
                UserId = "User1",
                MovieId = "1",
                RatingValue = 5,
                RatingDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new RatingHistory
            {
                UserId = "User2",
                MovieId = "2",
                RatingValue = 4,
                RatingDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new RatingHistory
            {
                UserId = "User3",
                MovieId = "3",
                RatingValue = 4,
                RatingDate = DateOnly.FromDateTime(DateTime.Now)
            }
        };

        // Add dummy data for Role class
        var roles = new List<Role>
        {
            new Role
            {
                MovieId = "1",
                PersonId = "Person1",
                Ordering = 1,
                Category = "Actor",
                Job = "Lead",
                Characters = "Character 1"
            },
            new Role
            {
                MovieId = "2",
                PersonId = "Person2",
                Ordering = 1,
                Category = "Actor",
                Job = "Supporting",
                Characters = "Character 2"
            },
            new Role
            {
                MovieId = "3",
                PersonId = "Person3",
                Ordering = 1,
                Category = "Director",
                Job = "Director",
                Characters = null
            }
        };

        // Add dummy data for SearchHistory class
        var searchHistories = new List<SearchHistory>
        {
            new SearchHistory
            {
                Id = "Search1",
                UserId = "User1",
                SearchQuery = "Action movies",
                SearchDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new SearchHistory
            {
                Id = "Search2",
                UserId = "User2",
                SearchQuery = "Drama movies",
                SearchDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new SearchHistory
            {
                Id = "Search3",
                UserId = "User3",
                SearchQuery = "Comedy movies",
                SearchDate = DateOnly.FromDateTime(DateTime.Now)
            }
        };

// Add dummy data for User class
        var users = new List<User>
        {
            new User
            {
                Id = "User1",
                Username = "user1",
                Email = "user1@example.com",
                Password = "password1",
                RegistrationDate = DateTime.Now.AddYears(-2),
                IsAdmin = false
            },
            new User
            {
                Id = "User2",
                Username = "user2",
                Email = "user2@example.com",
                Password = "password2",
                RegistrationDate = DateTime.Now.AddYears(-1),
                IsAdmin = false
            },
            new User
            {
                Id = "User3",
                Username = "admin",
                Email = "admin@example.com",
                Password = "adminpassword",
                RegistrationDate = DateTime.Now.AddYears(-3),
                IsAdmin = true
            }
        };
        context.Movies.AddRange(movies);
        context.Aliases.AddRange(aliases);
        context.BookmarkMovies.AddRange(bookmarkMovies);
        context.BookmarkPersonalities.AddRange(bookmarkPersonalities);
        context.Episodes.AddRange(episodes);
        context.People.AddRange(persons);
        context.Roles.AddRange(roles);
        context.Users.AddRange(users);
    }
}