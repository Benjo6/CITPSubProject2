using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Repositories.Contracts;

public interface IRatingHistoriesRepository : IGenericRepository<RatingHistory>
{
    public Task RateMovie(int userId, int movieId, int rating);
    public Task<List<SimpleRatingHistory>> GetRatingHistory(int userId);

}