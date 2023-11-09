﻿using Common.DataTransferObjects;
using Common.Mapper;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class MoviesService : IMoviesService
{
    private readonly IMoviesRepository _repository;
    private readonly IBookmarkMoviesRepository _bookmarkMoviesRepository;
    private readonly ObjectMapper _mapper;

    public MoviesService(IMoviesRepository repository)
    {
        _repository = repository;
        _mapper = new ObjectMapper();
    }

    public async Task<AlterResponseMovieDTO> AddMovie(AlterMovieDTO movie)
    {
        var addedMovie = await _repository.Add(_mapper.AlterMovieDTOToMovie(movie));
        return _mapper.MovieToAlterResponseMovieDTO(addedMovie);
    }

    public async Task<BookmarkMovieDTO> AddMovieToBookmark(BookmarkMovieDTO bookmarkMovie)
    {
        try
        {
            await _bookmarkMoviesRepository.AddBookmarkMovies(bookmarkMovie.UserId, bookmarkMovie.AliasName);
            var bookmarks = await _bookmarkMoviesRepository.GetBookmarkMovies(bookmarkMovie.UserId);
            return new BookmarkMovieDTO { AliasName = bookmarkMovie.AliasName, BookmarkDate = bookmarkMovie.BookmarkDate, UserId = bookmarkMovie.UserId };
        }
        catch { 
            return new BookmarkMovieDTO();
        }
    }

    public Task<List<BestMatch>> BestMatchQuery(string[] keywords)
    {
        return _repository.BestMatchQuery(keywords);
    }

    public async Task<bool> DeleteMovie(string id)
    {
        var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }

    public Task<List<string>> ExactMatchQuery(string[] keywords)
    {
        return _repository.ExactMatchQuery(keywords);
    }

    public Task<List<SimilarMovie>> FindSimilarMovies(string movieId)
    {
        return _repository.FindSimilarMovies(movieId) ?? throw new NullReferenceException("No similar movies");
    }

    public async Task<List<GetAllMovieDTO>> GetAllMovies()
    {
        var getAll = await _repository.GetAll();
        return _mapper.MovieToGetAllMoviesDTO(getAll) ?? new List<GetAllMovieDTO>();
    }

    public async Task<GetOneMovieDTO> GetOneMovie(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.MovieToGetOneMovieDTO(getOne);
    }

    public async Task<AlterResponseMovieDTO> UpdateMovie(string id, AlterMovieDTO movie)
    {
        var theMovie  = _mapper.AlterMovieDTOToMovie(movie);
        await _repository.Update(theMovie);
        var updatedMovie = await _repository.GetById(theMovie.Id);
        return _mapper.MovieToAlterResponseMovieDTO(updatedMovie);
    }

    public Task<List<WordFrequency>> WordToWordsQuery(string[] keywords)
    {
        return _repository.WordToWordsQuery(keywords);
    }
}