using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Infrastructure;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Remove this in the future, it's just for testing
    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;

    public virtual DbSet<Alias> Aliases { get; set; } = null!;

    public virtual DbSet<BookmarkMovie> BookmarkMovies { get; set; } = null!;

    public virtual DbSet<BookmarkPersonality> BookmarkPersonalities { get; set; } = null!;

    public virtual DbSet<Episode> Episodes { get; set; } = null!;

    public virtual DbSet<Movie> Movies { get; set; } = null!;

    public virtual DbSet<Person> People { get; set; } = null!;

    public virtual DbSet<RatingHistory> RatingHistories { get; set; } = null!;

    public virtual DbSet<Role> Roles { get; set; } = null!;

    public virtual DbSet<SearchHistory> SearchHistories { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    public virtual DbSet<Wi> Wis { get; set; } = null!;

    public virtual DbSet<WiWeighted> WiWeighteds { get; set; } = null!;
}