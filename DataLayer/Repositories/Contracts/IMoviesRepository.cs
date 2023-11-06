using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Repositories.Contracts;

public interface IMoviesRepository : IGenericRepository<Movie>
{
    public Task<List<SimilarMovie>> FindSimilarMovies(int movieId);
    public Task<List<string>> ExactMatchQuery(string[] keywords);
    public Task<List<BestMatch>> BestMatchQuery(string[] keywords);
    public Task<List<WordFrequency>> WordToWordsQuery(string[] keywords);

}

