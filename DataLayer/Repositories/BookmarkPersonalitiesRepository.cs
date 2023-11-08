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

namespace DataLayer.Repositories
{
    public class BookmarkPersonalitiesRepository : IBookmarkPersonalitiesRepository
    {
        private readonly AppDbContext _context;

        public BookmarkPersonalitiesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddBookmarkPersonality(string userId, string personId)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"SELECT add_bookmark_personality({userId}, {personId})");
        }

        public async Task<List<string>> GetBookmarksPersonality(string userId)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_bookmarks_personality(@userId)";
                command.Parameters.Add(new NpgsqlParameter("userId", userId));
                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var similarMovies = new List<string>();

                    while (await result.ReadAsync())
                    {
                        var similarMovie = result.GetString(result.GetOrdinal("person_id"));

                        similarMovies.Add(similarMovie);
                    }

                    return similarMovies;
                }
            }
        }
    }
}
