using Common.DataTransferObjects;
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

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookmarkMovie>()
            .HasKey(b => new { b.UserId, b.AliasId });
        modelBuilder.Entity<BookmarkPersonality>()
            .HasKey(b => new { b.UserId, b.PersonId });
        modelBuilder.Entity<RatingHistory>()
            .HasKey(r => new { r.UserId, r.MovieId });
        modelBuilder.Entity<Role>()
            .HasKey(r => new { r.MovieId, r.PersonId });
        
        // User to BookmarkMovie
        modelBuilder.Entity<BookmarkMovie>()
            .HasOne(b => b.User)
            .WithMany(u => u.BookmarkMovies)
            .HasForeignKey(b => b.UserId);

        // User to BookmarkPersonality
        modelBuilder.Entity<BookmarkPersonality>()
            .HasOne(b => b.User)
            .WithMany(u => u.BookmarkPersonalities)
            .HasForeignKey(b => b.UserId);

        // User to RatingHistory
        modelBuilder.Entity<RatingHistory>()
            .HasOne(r => r.User)
            .WithMany(u => u.RatingHistories)
            .HasForeignKey(r => r.UserId);

        // Movie to RatingHistory
        modelBuilder.Entity<RatingHistory>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.RatingHistories)
            .HasForeignKey(r => r.MovieId);

        // Movie to Role
        modelBuilder.Entity<Role>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.Roles)
            .HasForeignKey(r => r.MovieId);

        // Person to Role
        modelBuilder.Entity<Role>()
            .HasOne(r => r.Person)
            .WithMany(p => p.Roles)
            .HasForeignKey(r => r.PersonId);

        // Alias to BookmarkMovie
        modelBuilder.Entity<BookmarkMovie>()
            .HasOne(b => b.Alias)
            .WithMany(a => a.BookmarkMovies)
            .HasForeignKey(b => b.AliasId);

        // Person to BookmarkPersonality
        modelBuilder.Entity<BookmarkPersonality>()
            .HasOne(b => b.Person)
            .WithMany(p => p.BookmarkPersonalities)
            .HasForeignKey(b => b.PersonId);
        
        // Movie to Alias
        modelBuilder.Entity<Alias>()
            .HasOne(a => a.Movie)
            .WithMany(m => m.Aliases)
            .HasForeignKey(a => a.MovieId);

        // Movie to Episode
        modelBuilder.Entity<Episode>()
            .HasOne(e => e.Series)
            .WithMany(m => m.Episodes)
            .HasForeignKey(e => e.SeriesId);
    }

}