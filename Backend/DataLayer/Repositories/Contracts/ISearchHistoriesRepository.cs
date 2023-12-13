using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Repositories.Contracts;

public interface ISearchHistoriesRepository : IGenericRepository<SearchHistory>
{
    public Task AddSearchHistory(string userId, string searchString);

}