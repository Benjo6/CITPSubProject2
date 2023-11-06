using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class MoviesRepository : GenericRepository<Movie>, IMoviesRepository
{
    public MoviesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<SimilarMovie>> FindSimilarMovies(int movieId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> ExactMatchQuery(string[] keywords)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BestMatch>> BestMatchQuery(string[] keywords)
    {
        throw new NotImplementedException();
    }

    public async Task<List<WordFrequency>> WordToWordsQuery(string[] keywords)
    {
        throw new NotImplementedException();
    }
}