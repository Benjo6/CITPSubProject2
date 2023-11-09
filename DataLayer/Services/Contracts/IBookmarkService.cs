using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services.Contracts
{
    public interface IBookmarkService
    {
        public Task<List<string>> GetBookmarkMovies(string userId);
        public Task<List<string>> GetBookmarkPersons(string userId);
        public Task<bool> AddNoteMovie(string userId, string aliasId, string note);
        public Task<bool> AddBookmarkMovies(string userId, string aliasId);
        public Task<bool> AddBookmarkPersonality(string userId, string aliasId);
        public Task<bool> RemoveBookmarkPersonality(string userId, string personId);
        public Task<bool> RemoveBookmarkMovie(string userId, string aliasId);
    }
}
