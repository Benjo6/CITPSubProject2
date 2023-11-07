using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class BookmarkPersonalitiesRepository : GenericRepository<BookmarkPersonality>, IBookmarkPersonalitiesRepository
{
    public BookmarkPersonalitiesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddBookmarkPersonality(int userId, int personId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<int>> GetBookmarksPersonality(int userId)
    {
        throw new NotImplementedException();
    }
}