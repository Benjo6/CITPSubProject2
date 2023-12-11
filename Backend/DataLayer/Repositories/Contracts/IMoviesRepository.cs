using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Repositories.Contracts;

public interface IMoviesRepository : IGenericRepository<Movie>
{
    public Task<List<SimilarMovie>> FindSimilarMovies(string movieId, int? page = 1, int? perPage = 10);
    public Task<List<string>> ExactMatchQuery(string[] keywords, int? page = 1, int? perPage = 10);
    public Task<List<BestMatch>> BestMatchQuery(string[] keywords, int? page = 1, int? perPage = 10);
    public Task<List<WordFrequency>> WordToWordsQuery(string[] keywords);
    public Task<List<PopularActor>> GetPopularActorsInMovie(string movieId);
    public Task RateMovie(string userId, string movieId, decimal rating);
    public Task<List<SearchResults>> StringSearch(string userId, string searchString);
    public Task<List<SearchResults>> StructuredStringSearch(string userId, string title, string personName);
}

