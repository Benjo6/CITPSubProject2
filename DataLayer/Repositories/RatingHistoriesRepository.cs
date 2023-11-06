using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class RatingHistoriesRepository : GenericRepository<RatingHistory>, IRatingHistoriesRepository
{
    public RatingHistoriesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task RateMovie(int userId, int movieId, int rating)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SimpleRatingHistory>> GetRatingHistory(int userId)
    {
        throw new NotImplementedException();
    }
}