using Common.DataTransferObjects;
using Common.Domain;
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

    public async Task<AddAndUpdateMovieDTO> AddMovie(CreateMovieDTO movie)
    {
        var addedMovie = await _repository.Add(_mapper.CreateMovieDTOToMovie(movie));
        return _mapper.MovieToAddAndUpdateMovieDTO(addedMovie);
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

    public async Task<AddAndUpdateMovieDTO> UpdateMovie(Movie movie)
    {
        _ = _repository.GetById(movie.Id);
        await _repository.Update(movie);
        var updatedMovie = await _repository.GetById(movie.Id);
        return _mapper.MovieToAddAndUpdateMovieDTO(updatedMovie);
    }

    public Task<List<WordFrequency>> WordToWordsQuery(string[] keywords)
    {
        return _repository.WordToWordsQuery(keywords);
    }
}