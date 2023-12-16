using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLayer.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

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
        modelBuilder.Entity<Alias>(entity =>
        {
            entity.HasOne(a => a.Movie)
                .WithMany(m => m.Aliases)
                .HasForeignKey(a => a.MovieId)
                .IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BookmarkMovie>(entity =>
        {
            entity.HasKey(b => new { b.UserId, b.MovieId });

            entity.HasOne(b => b.User)
                .WithMany(u => u.BookmarkMovies)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.Movie)
                .WithMany(a => a.BookmarkMovies)
                .HasForeignKey(b => b.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BookmarkPersonality>(entity =>
        {
            entity.HasKey(b => new { b.UserId, b.PersonId });

            entity.HasOne(b => b.User)
                .WithMany(u => u.BookmarkPersonalities)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.Person)
                .WithMany(p => p.BookmarkPersonalities)
                .HasForeignKey(b => b.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Episode>()
            .HasOne(e => e.Series)
            .WithMany(m => m.Episodes)
            .HasForeignKey(e => e.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RatingHistory>(entity =>
        {
            entity.HasKey(r => new { r.UserId, r.MovieId });
            entity.HasOne(r => r.User)
                .WithMany(u => u.RatingHistories)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
    
            entity.HasOne(r => r.Movie)
                .WithMany(m => m.RatingHistories)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade); // This ensures related RatingHistory records are deleted
        });


        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => new { r.MovieId, r.PersonId });
            
            entity.HasOne(r => r.Movie)
                .WithMany(m => m.Roles)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(r => r.Person)
                .WithMany(p => p.Roles)
                .HasForeignKey(r => r.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SearchHistory>()
            .HasOne(d => d.User)
            .WithMany(p => p.SearchHistories)
            .HasForeignKey(d => d.UserId)        
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email)
                .IsUnique();
            entity.HasIndex(u => u.Password)
                .IsUnique();
        });
    }
}