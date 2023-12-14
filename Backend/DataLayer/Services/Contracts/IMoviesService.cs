using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IMoviesService
{
    public Task<List<GetAllMovieDTO>> GetAllMovies(Filter filter);
    public Task<GetOneMovieDTO> GetOneMovie(string id);
    public Task<AlterResponseMovieDTO> UpdateMovie(string id, AlterMovieDTO movie);
    public Task<bool> DeleteMovie (string id);
    public Task<AlterResponseMovieDTO> AddMovie(AlterMovieDTO movie);
    public Task<List<SimilarMovie>> FindSimilarMovies(string movieId, int? page = 1, int? perPage = 10);
    public Task<bool> RateMovie(string userId, string movieId, decimal rating);
    public Task<List<PopularActor>> GetPopularActorsInMovie(string movieId);
}
