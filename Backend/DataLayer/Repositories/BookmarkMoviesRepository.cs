using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace DataLayer.Repositories;

public class BookmarkMoviesRepository : IBookmarkMoviesRepository
{
    private readonly AppDbContext _context;

    public BookmarkMoviesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddBookmarkMovies(string userId, string aliasId)
    {
        const string commandText = "SELECT add_bookmark_movie(@userId, @aliasId)";
        var userIdParam = new NpgsqlParameter("@userId", userId);
        var aliasIdParam = new NpgsqlParameter("@aliasId", aliasId);

        await _context.Database.ExecuteSqlRawAsync(commandText, userIdParam, aliasIdParam);
    }

    public async Task<List<string>> GetBookmarksMovies(string userId, int? page = 1, int? perPage = 10)
    {
        var bookmarkedMovies = new List<string>();
        const string commandText = "SELECT * FROM get_bookmarks_movie(@userId);";
        var userIdParam = new NpgsqlParameter("@userId", userId);

        await using (var connection = _context.Database.GetDbConnection())
        {
            await connection.OpenAsync(); // Ensure the connection is open
            await using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text; // Use CommandType.Text for functions
                command.CommandText = commandText;
                command.Parameters.Add(userIdParam);

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    var count = 0;
                    var skip = (page - 1) * perPage.Value; // Calculate the number of records to skip
                    var take = page * perPage.Value; // Calculate the maximum number of records to take

                    while (await reader.ReadAsync())
                    {
                        if (count >= skip && count < take)
                        {
                            bookmarkedMovies.Add(reader.GetString(0));
                        }

                        count++;
                    }
                }
            }
        }

        return bookmarkedMovies;
    }
        
    public async Task AddNote(string userId, string aliasId, string note)
    {
        const string commandText = "SELECT add_note(@userId, @aliasId, @note)";
        var userIdParam = new NpgsqlParameter("@userId", userId);
        var aliasIdParam = new NpgsqlParameter("@aliasId", aliasId);
        var noteParam = new NpgsqlParameter("@note", note);

        await _context.Database.ExecuteSqlRawAsync(commandText, userIdParam, aliasIdParam, noteParam);
    }

    public async Task<bool> DeleteBookmarkMovie(string userId, string aliasId)
    {
        var bookmarkToRemove =
            _context.BookmarkMovies.FirstOrDefault(x => x.AliasId == aliasId && x.UserId == userId);

        if (bookmarkToRemove == null) return false;
        _context.BookmarkMovies.Remove(bookmarkToRemove);
        await _context.SaveChangesAsync();
        return true;

    }
}