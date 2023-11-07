namespace DataLayer.Repositories.Contracts;

public interface IBookmarkMoviesRepository
{
    public Task<List<int>> GetBookmarkMovies(int userId);
    public Task AddBookmarkMovies(int userId, int personId);
    public Task AddNote(int userId, int aliasId, string note);
    
}