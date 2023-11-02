using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SubProject2.Models;

public partial class Cit02Context : DbContext
{
    public Cit02Context()
    {
    }

    public Cit02Context(DbContextOptions<Cit02Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Alias> Aliases { get; set; }

    public virtual DbSet<Bookmarkmovie> Bookmarkmovies { get; set; }

    public virtual DbSet<Bookmarkpersonality> Bookmarkpersonalities { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Ratinghistory> Ratinghistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Searchhistory> Searchhistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wi> Wis { get; set; }

    public virtual DbSet<WiWeighted> WiWeighteds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=cit.ruc.dk,5432;Database=cit02;User Id=cit02;Password=Ns81PswxAHdy");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alias>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("alias_pkey");

            entity.ToTable("alias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attributes)
                .HasMaxLength(255)
                .HasColumnName("attributes");
            entity.Property(e => e.IsOriginal).HasColumnName("is_original");
            entity.Property(e => e.Language)
                .HasMaxLength(255)
                .HasColumnName("language");
            entity.Property(e => e.MovieId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
            entity.Property(e => e.Region)
                .HasMaxLength(255)
                .HasColumnName("region");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Types)
                .HasMaxLength(255)
                .HasColumnName("types");

            entity.HasOne(d => d.Movie).WithMany(p => p.Aliases)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("alias_movie_id_fkey");
        });

        modelBuilder.Entity<Bookmarkmovie>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AliasId }).HasName("bookmarkmovie_pkey");

            entity.ToTable("bookmarkmovie");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AliasId).HasColumnName("alias_id");
            entity.Property(e => e.BookmarkDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("bookmark_date");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");

            entity.HasOne(d => d.Alias).WithMany(p => p.Bookmarkmovies)
                .HasForeignKey(d => d.AliasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookmarkmovie_alias_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bookmarkmovies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookmarkmovie_user_id_fkey");
        });

        modelBuilder.Entity<Bookmarkpersonality>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PersonId }).HasName("bookmarkpersonality_pkey");

            entity.ToTable("bookmarkpersonality");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PersonId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("person_id");
            entity.Property(e => e.BookmarkDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("bookmark_date");

            entity.HasOne(d => d.Person).WithMany(p => p.Bookmarkpersonalities)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookmarkpersonality_person_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bookmarkpersonalities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookmarkpersonality_user_id_fkey");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("episode_pkey");

            entity.ToTable("episode");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Episode1).HasColumnName("episode");
            entity.Property(e => e.Season).HasColumnName("season");
            entity.Property(e => e.SeriesId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("series_id");

            entity.HasOne(d => d.Series).WithMany(p => p.Episodes)
                .HasForeignKey(d => d.SeriesId)
                .HasConstraintName("episode_series_id_fkey");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movie_pkey");

            entity.ToTable("movie");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.EndYear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("end_year");
            entity.Property(e => e.Genres)
                .HasMaxLength(255)
                .HasColumnName("genres");
            entity.Property(e => e.IsAdult).HasColumnName("is_adult");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(255)
                .HasColumnName("original_title");
            entity.Property(e => e.Rating)
                .HasPrecision(3, 1)
                .HasColumnName("rating");
            entity.Property(e => e.Runtime).HasColumnName("runtime");
            entity.Property(e => e.StartYear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("start_year");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.Votes).HasColumnName("votes");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pkey");

            entity.ToTable("person");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.BirthYear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("birth_year");
            entity.Property(e => e.DeathYear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("death_year");
            entity.Property(e => e.KnownFor)
                .HasMaxLength(255)
                .HasColumnName("known_for");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Professions)
                .HasMaxLength(255)
                .HasColumnName("professions");
        });

        modelBuilder.Entity<Ratinghistory>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MovieId }).HasName("ratinghistory_pkey");

            entity.ToTable("ratinghistory");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.MovieId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.RatingDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("rating_date");
            entity.Property(e => e.RatingValue).HasColumnName("rating_value");

            entity.HasOne(d => d.Movie).WithMany(p => p.Ratinghistories)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ratinghistory_movie_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Ratinghistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ratinghistory_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("role");

            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.Characters)
                .HasMaxLength(255)
                .HasColumnName("characters");
            entity.Property(e => e.Job)
                .HasMaxLength(255)
                .HasColumnName("job");
            entity.Property(e => e.MovieId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("movie_id");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
            entity.Property(e => e.PersonId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("person_id");

            entity.HasOne(d => d.Movie).WithMany()
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("role_movie_id_fkey");

            entity.HasOne(d => d.Person).WithMany()
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("role_person_id_fkey");
            entity.HasKey(e => new { e.MovieId, e.PersonId });
        });

        modelBuilder.Entity<Searchhistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("searchhistory_pkey");

            entity.ToTable("searchhistory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SearchDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("search_date");
            entity.Property(e => e.SearchQuery)
                .HasMaxLength(255)
                .HasColumnName("search_query");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Searchhistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("searchhistory_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "unique_email").IsUnique();

            entity.HasIndex(e => e.Username, "unique_username").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"User_id_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Isadmin)
                .HasDefaultValueSql("false")
                .HasColumnName("isadmin");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("registration_date");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Wi>(entity =>
        {
            entity.HasKey(e => new { e.Tconst, e.Word, e.Field }).HasName("wi_pkey");

            entity.ToTable("wi");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Word)
                .HasColumnType("character varying")
                .HasColumnName("word");
            entity.Property(e => e.Field)
                .HasMaxLength(1)
                .HasColumnName("field");
            entity.Property(e => e.Lexeme).HasColumnName("lexeme");
        });

        modelBuilder.Entity<WiWeighted>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("wi_weighted");

            entity.Property(e => e.Field)
                .HasMaxLength(1)
                .HasColumnName("field");
            entity.Property(e => e.Lexeme)
                .HasColumnType("character varying")
                .HasColumnName("lexeme");
            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.Word)
                .HasColumnType("character varying")
                .HasColumnName("word");

            entity.HasOne(d => d.TconstNavigation).WithMany()
                .HasForeignKey(d => d.Tconst)
                .HasConstraintName("wi_weighted_tconst_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
