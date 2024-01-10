using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts
{
    public interface IBookmarkService
    {
        public Task<List<BookmarkMovieDTO>> GetBookmarkMovies(string userId, int? page = 1, int? perPage = 10);
        public Task<List<BookmarkPersonalityDTO>> GetBookmarkPersons(string userId, int? page = 1, int? perPage = 10);
        public Task<bool> AddNoteMovie(string userId, string aliasId, string note);
        public Task<bool> AddBookmarkMovies(string userId, string aliasId);
        public Task<bool> AddBookmarkPersonality(string userId, string personId);
        public Task<bool> RemoveBookmarkPersonality(string userId, string personId);
        public Task<bool> RemoveBookmarkMovies(string userId, string aliasId);
        public Task<bool> IsMovieBookmarked(string userId, string movieId);
        public Task<bool> IsPersonalityBookmarked(string userId, string movieId);
    }
}
