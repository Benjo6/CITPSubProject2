﻿using Common.DataTransferObjects;
using Common.Mapper;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkPersonalitiesRepository _personalityRepository;
        private readonly IBookmarkMoviesRepository _moviesRepository;
        private readonly ObjectMapper _mapper;

        public BookmarkService(IBookmarkPersonalitiesRepository repositoryP , IBookmarkMoviesRepository repositoryM)
        {
            _mapper = new ObjectMapper();
            _personalityRepository = repositoryP;
            _moviesRepository = repositoryM;
        }

        public async Task<bool> AddBookmarkMovies(string userId, string movieId)
        {
            try
            {
                await _moviesRepository.AddBookmarkMovies(userId, movieId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddBookmarkPersonality(string userId, string movieId)
        {
            try
            {
                await _personalityRepository.AddBookmarkPersonality(userId, movieId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddNoteMovie(string userId, string movieId, string note)
        {
            try
            {
                await _moviesRepository.AddNote(userId, movieId, note);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<List<BookmarkMovieDTO>> GetBookmarkMovies(string userId, int? page = 1, int? perPage = 10)
        {
            var movies = await _moviesRepository.GetBookmarksMovies(userId, page, perPage);
            return _mapper.ListBookmarkMovieToListBookmarkMovieDTO(movies);
        }
        
        public async Task<bool> RemoveBookmarkMovies(string userId, string movieId)
        {
            try
            {
                await _moviesRepository.DeleteBookmarkMovie(userId, movieId);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RemoveBookmarkPersonality(string userId, string personId)
        {
            try
            {
                await _personalityRepository.DeleteBookmarkPersonality(userId, personId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsMovieBookmarked(string userId, string movieId)
        {
            return await _moviesRepository.IsMovieBookmarked(userId, movieId);
        }
        
        public async Task<bool> IsPersonalityBookmarked(string userId, string personId)
        {
            return await _personalityRepository.IsPersonalityBookmarked(userId, personId);
        }

        public async Task<List<BookmarkPersonalityDTO>> GetBookmarkPersons(string userId, int? page = 1, int? perPage = 10)
        {
            var persons = await _personalityRepository.GetBookmarksPersonality(userId, page, perPage);
            return _mapper.ListBookmarkPersonalityToListBookmarkPersonalityDTO(persons);
        }
    }
}
