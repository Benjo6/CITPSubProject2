using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Common.Domain;

namespace DataLayer.Repositories;

public class BookmarkMoviesRepository : IBookmarkMoviesRepository
{
    private readonly AppDbContext _context;

    public BookmarkMoviesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddBookmarkMovies(string userId, string movieId)
    {
        const string commandText = "SELECT add_bookmark_movie(@userId, @movieId)";
        var userIdParam = new NpgsqlParameter("@userId", userId);
        var movieIdParam = new NpgsqlParameter("@movieId", movieId);

        await _context.Database.ExecuteSqlRawAsync(commandText, userIdParam, movieIdParam);
    }

    public async Task<List<BookmarkMovie>> GetBookmarksMovies(string userId, int? page = 1, int? perPage = 10)
    {
        var skip = (page - 1) * perPage.Value ?? 1;

        var bookmarkedMovies = await _context.BookmarkMovies
            .Where(b => b.UserId == userId)
            .Skip(skip)
            .Take(perPage.Value)
            .ToListAsync();

        return bookmarkedMovies;
    }
        
    public async Task AddNote(string userId, string movieId, string note)
    {
        const string commandText = "SELECT add_note(@userId, @movieId, @note)";
        var userIdParam = new NpgsqlParameter("@userId", userId);
        var movieIdParam = new NpgsqlParameter("@movieId", movieId);
        var noteParam = new NpgsqlParameter("@note", note);

        await _context.Database.ExecuteSqlRawAsync(commandText, userIdParam, movieIdParam, noteParam);
    }

    public async Task<bool> DeleteBookmarkMovie(string userId, string movieId)
    {
        var bookmarkToRemove =
            _context.BookmarkMovies.FirstOrDefault(x => x.MovieId == movieId && x.UserId == userId);

        if (bookmarkToRemove == null) return false;
        _context.BookmarkMovies.Remove(bookmarkToRemove);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsMovieBookmarked(string userId, string movieId)
    {
        // Check if the bookmark exists in the database
        var bookmark = await _context.BookmarkMovies
            .FirstOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId);

        // If the bookmark exists, return true, else return false
        return bookmark != null;
    }

}