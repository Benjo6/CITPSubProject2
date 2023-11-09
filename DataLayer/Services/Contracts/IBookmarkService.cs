using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services.Contracts
{
    public interface IBookmarkService
    {
        public Task<List<string>> GetBookmarkMovies(string userId, int? page = 1, int? perPage = 10);
        public Task<List<string>> GetBookmarkPersons(string userId, int? page = 1, int? perPage = 10);
        public Task<bool> AddNoteMovie(string userId, string aliasId, string note);
        public Task<bool> AddBookmarkMovies(string userId, string aliasId);
        public Task<bool> AddBookmarkPersonality(string userId, string aliasId);
        public Task<bool> RemoveBookmarkPersonality(string userId, string personId);
        public Task<bool> RemoveBookmarkMovies(string userId, string aliasId);
    }
}
