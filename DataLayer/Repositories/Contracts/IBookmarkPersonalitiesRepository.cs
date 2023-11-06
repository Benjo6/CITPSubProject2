namespace DataLayer.Repositories.Contracts;

public interface IBookmarkPersonalitiesRepository
{
    public Task AddBookmarkPersonality(int userId, int personId);
    public Task<List<int>> GetBookmarksPersonality(int userId);
    
}