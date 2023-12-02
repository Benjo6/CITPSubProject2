using Common.Mapper;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkPersonalitiesRepository _personalityRepository;
        private readonly IBookmarkMoviesRepository _moviesRepository;

        public BookmarkService(IBookmarkPersonalitiesRepository repositoryP , IBookmarkMoviesRepository repositoryM)
        {
            _personalityRepository = repositoryP;
            _moviesRepository = repositoryM;
        }

        public async Task<bool> AddBookmarkMovies(string userId, string aliasId)
        {
            try
            {
                await _moviesRepository.AddBookmarkMovies(userId, aliasId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddBookmarkPersonality(string userId, string aliasId)
        {
            try
            {
                await _personalityRepository.AddBookmarkPersonality(userId, aliasId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddNoteMovie(string userId, string aliasId, string note)
        {
            try
            {
                await _moviesRepository.AddNote(userId, aliasId, note);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<string>> GetBookmarkMovies(string userId, int? page = 1, int? perPage = 10)
        {
            return await _moviesRepository.GetBookmarksMovies(userId, page, perPage);
        }
        public async Task<bool> RemoveBookmarkMovies(string userId, string aliasId)
        {
            try
            {
                await _moviesRepository.DeleteBookmarkMovie(userId, aliasId);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RemoveBookmarkPersonality(string userId, string aliasId)
        {
            try
            {
                await _personalityRepository.DeleteBookmarkPersonality(userId, aliasId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<string>> GetBookmarkPersons(string userId, int? page = 1, int? perPage = 10)
        {
            return await _personalityRepository.GetBookmarksPersonality(userId, page, perPage);
        }
    }
}
