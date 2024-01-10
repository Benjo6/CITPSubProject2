using Common.Domain;

namespace DataLayer.Repositories.Contracts;

public interface IBookmarkMoviesRepository
{
    public Task<List<BookmarkMovie>> GetBookmarksMovies(string userId, int? page = 1, int? perPage = 10);
    public Task AddBookmarkMovies(string userId, string personId);
    public Task AddNote(string userId, string aliasId, string note);
    public Task<bool> DeleteBookmarkMovie(string userId, string personId);
    public Task<bool> IsMovieBookmarked(string userId, string movieId);
}