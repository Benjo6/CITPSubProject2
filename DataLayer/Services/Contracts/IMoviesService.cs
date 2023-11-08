using Common.DataTransferObjects;
using Common.Domain;

namespace DataLayer.Services.Contracts;

public interface IMoviesService
{
    public Task<List<GetAllMovieDTO>> GetAllMovies();
    public Task<GetOneMovieDTO> GetOneMovie(string id);
    public Task<AddAndUpdateMovieDTO> UpdateMovie(Movie movie);
    public Task<bool> DeleteMovie (string id);
    public Task<AddAndUpdateMovieDTO> AddMovie(CreateMovieDTO movie);
    public Task<List<SimilarMovie>> FindSimilarMovies(string movieId);
    public Task<List<string>> ExactMatchQuery(string[] keywords);
    public Task<List<BestMatch>> BestMatchQuery(string[] keywords);
    public Task<List<WordFrequency>> WordToWordsQuery(string[] keywords);
}
