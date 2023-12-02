using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Repositories.Contracts;

public interface IRatingHistoriesRepository 
{
    public Task RateMovie(string userId, string movieId, int rating);
    public Task<List<SimpleRatingHistory>> GetRatingHistory(string userId);

}