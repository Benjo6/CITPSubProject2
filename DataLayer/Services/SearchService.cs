using Common.DataTransferObjects;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class SearchService : ISearchService
{
    private ISearchHistoriesRepository _searchHistoriesRepository;
    private ObjectMapper _mapper;

    public SearchService(ISearchHistoriesRepository searchHistoriesRepository)
    {
        _searchHistoriesRepository = searchHistoriesRepository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<SearchHistoryDTO>> GetAllSearchHistory()
    {
        var getAll = await _repository.GetAll();
        return _mapper.SearchHistoryDTO(getAll) ?? new List<SearchHistoryDTO>();
    }

    public async Task<SearchHistoryDTO> GetOneSearchHistory(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.SearchHistoryDTO(getOne);
    }
}