namespace DataLayer.Repositories.Contracts;

public interface IBookmarkPersonalitiesRepository
{
    public Task AddBookmarkPersonality(string userId, string personId);
    public Task<List<string>> GetBookmarksPersonality(string userId, int? page = 1, int? perPage = 10);
    public Task<bool> DeleteBookmarkPersonality(string userId, string personId);


}