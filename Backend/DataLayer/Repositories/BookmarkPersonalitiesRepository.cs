using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Npgsql;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Common.DataTransferObjects;
using System.Reflection.PortableExecutable;

namespace DataLayer.Repositories
{
    public class BookmarkPersonalitiesRepository : IBookmarkPersonalitiesRepository
    {
        private readonly AppDbContext _context;

        public BookmarkPersonalitiesRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> IsPersonalityBookmarked(string userId, string personId)
        {
            // Check if the bookmark exists in the database
            var bookmark = await _context.BookmarkPersonalities
                .FirstOrDefaultAsync(x => x.PersonId == personId && x.UserId == userId);

            // If the bookmark exists, return true, else return false
            return bookmark != null;
        }

        public async Task AddBookmarkPersonality(string userId, string personId)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"SELECT add_bookmark_personality({userId}, {personId})");
        }

        public async Task<List<string>> GetBookmarksPersonality(string userId, int? page = 1, int? perPage = 10)
        {
            await using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_bookmarks_personality(@userId)";
                command.Parameters.Add(new NpgsqlParameter("userId", userId));
                _context.Database.OpenConnection();

                await using (var result = await command.ExecuteReaderAsync())
                {
                    var similarMovies = new List<string>();

                    int count = 0;
                    while (await result.ReadAsync() && count <= page * perPage)
                    {
                        if (count < (page - 1) * perPage)
                        {
                            count++;
                            continue;
                        }
                        count++;
                        var similarMovie = result.GetString(result.GetOrdinal("person_id"));

                        similarMovies.Add(similarMovie);
                    }

                    return similarMovies;
                }
            }
        }

        public async Task<bool> DeleteBookmarkPersonality(string userId, string personId)
        { 
            // Attempt to find the bookmark to remove
            var bookmarkToRemove = _context.BookmarkPersonalities.FirstOrDefault(x => x.PersonId == personId && x.UserId == userId);
            if (bookmarkToRemove == null) return false;
            _context.BookmarkPersonalities.Remove(bookmarkToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
