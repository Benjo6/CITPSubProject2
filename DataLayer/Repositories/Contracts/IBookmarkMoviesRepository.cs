namespace DataLayer.Repositories.Contracts;

public interface IBookmarkMoviesRepository
{
    public Task<List<string>> GetBookmarkMovies(string userId);
    public Task AddBookmarkMovies(string userId, string personId);
    public Task AddNote(string userId, string aliasId, string note);
    
}