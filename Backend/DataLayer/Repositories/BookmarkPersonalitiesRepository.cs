using Common.Domain;
using Microsoft.EntityFrameworkCore;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Npgsql;

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

        public async Task<List<BookmarkPersonality>> GetBookmarksPersonality(string userId, int? page = 1, int? perPage = 10)
        {
            var skip = (page - 1) * perPage.Value ?? 1;

            var bookmarkedPersons = await _context.BookmarkPersonalities
                .Where(b => b.UserId == userId)
                .Skip(skip)
                .Take(perPage.Value)
                .ToListAsync();

            return bookmarkedPersons;
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
