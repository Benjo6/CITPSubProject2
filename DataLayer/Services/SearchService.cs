using Common.DataTransferObjects;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class SearchService : ISearchService
{
    private ISearchHistoriesRepository _searchHistoriesRepository;

    public SearchService(ISearchHistoriesRepository searchHistoriesRepository)
    {
        _searchHistoriesRepository = searchHistoriesRepository;
    }

    public async Task<List<SearchHistoryDTO>> GetAllSearchHistory()
    {
        throw new NotImplementedException();
    }

    public async Task<SearchHistoryDTO> GetOneSearchHistory(string id)
    {
        throw new NotImplementedException();
    }
}