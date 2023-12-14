using Common;
using Common.DataTransferObjects;
using Common.Mapper;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class MoviesService : IMoviesService
{
    private readonly IMoviesRepository _repository;
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

    public async Task<bool> DeleteMovie(string id)
    {
        var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }

    public Task<List<SimilarMovie>> FindSimilarMovies(string movieId, int? page = 1, int? perPage = 10)
    {
        return _repository.FindSimilarMovies(movieId, page, perPage) ?? throw new NullReferenceException("No similar movies");
    }

    public async Task<List<GetAllMovieDTO>>GetAllMovies(Filter filter)
    {
        var getAll = await _repository.GetAll(filter);
        return _mapper.ListMovieToListGetAllMoviesDTO(getAll);
    }

    public async Task<GetOneMovieDTO> GetOneMovie(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.MovieToGetOneMovieDTO(getOne);
    }

    public async Task<AlterResponseMovieDTO> UpdateMovie(string id, AlterMovieDTO movie)
    {
        var theMovie  = _mapper.AlterMovieDTOToMovie(movie);
        theMovie.Id = id;
        await _repository.Update(theMovie);
        var updatedMovie = await _repository.GetById(theMovie.Id);
        return _mapper.MovieToAlterResponseMovieDTO(updatedMovie);
    }
    
    public Task<List<PopularActor>> GetPopularActorsInMovie(string movieId)
    {
        return _repository.GetPopularActorsInMovie(movieId);
    }
    
    public async Task<bool> RateMovie(string userId, string movieId, decimal rating)
    {
        try
        {
            await _repository.RateMovie(userId, movieId, rating);
            return true;
        }
        catch
        {
            return false;
        }
    }
}