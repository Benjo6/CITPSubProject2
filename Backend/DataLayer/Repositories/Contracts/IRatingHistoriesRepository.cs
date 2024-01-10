using Common.DataTransferObjects;

namespace DataLayer.Repositories.Contracts;

public interface IRatingHistoriesRepository 
{
    public Task RateMovie(string userId, string movieId, int rating);
    public Task<List<SimpleRatingHistory>> GetRatingHistory(string userId);

}