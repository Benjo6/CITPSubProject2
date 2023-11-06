using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class BookmarkMoviesRepository : IBookmarkMoviesRepository
{
    public async Task<List<int>> GetBookmarkMovies(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddBookmarkMovies(int userId, int personId)
    {
        throw new NotImplementedException();
    }

    public async Task AddNote(int userId, int aliasId, string note)
    {
        throw new NotImplementedException();
    }
}