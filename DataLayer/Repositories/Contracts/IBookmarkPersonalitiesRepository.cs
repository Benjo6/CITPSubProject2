namespace DataLayer.Repositories.Contracts;

public interface IBookmarkPersonalitiesRepository
{
    public Task AddBookmarkPersonality(string userId, string personId);
    public Task<List<string>> GetBookmarksPersonality(string userId);
    
}