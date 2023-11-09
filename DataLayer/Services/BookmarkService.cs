using Common.Domain;
using Common.Mapper;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkPersonalitiesRepository _personalityRepository;
        private readonly IBookmarkMoviesRepository _moviesRepository;
        private readonly ObjectMapper _mapper;

        public BookmarkService(IBookmarkPersonalitiesRepository repositoryP , IBookmarkMoviesRepository repositoryM)
        {
            _personalityRepository = repositoryP;
            _moviesRepository = repositoryM;
            _mapper = new ObjectMapper();
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


        public async Task<List<string>> GetBookmarkMovies(string userId)
        {
            return await _moviesRepository.GetBookmarksMovies(userId);
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

        public async Task<List<string>> GetBookmarkPersons(string userId)
        {
            return await _personalityRepository.GetBookmarksPersonality(userId);
        }
    }
}
