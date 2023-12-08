using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IMoviesService
{
    public Task<(List<GetAllMovieDTO>, Metadata)> GetAllMovies(Filter filter);
    public Task<GetOneMovieDTO> GetOneMovie(string id);
    public Task<AlterResponseMovieDTO> UpdateMovie(string id, AlterMovieDTO movie);
    public Task<bool> DeleteMovie (string id);
    public Task<AlterResponseMovieDTO> AddMovie(AlterMovieDTO movie);
    public Task<List<SimilarMovie>> FindSimilarMovies(string movieId, int? page = 1, int? perPage = 10);
    public Task<List<string>> ExactMatchQuery(string[] keywords, int? page = 1, int? perPage = 10);
    public Task<List<BestMatch>> BestMatchQuery(string[] keywords, int? page = 1, int? perPage = 10);
    public Task<List<WordFrequency>> WordToWordsQuery(string[] keywords);
}
