using Common.DataTransferObjects;
using Common.Mapper;
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
        var getAll = await _searchHistoriesRepository.GetAll();
        return _mapper.ListSearchToListSearchDTO(getAll) ?? new List<SearchHistoryDTO>();
    }

    public async Task<SearchHistoryDTO> GetOneSearchHistory(string id)
    {
        var getOne = await _searchHistoriesRepository.GetById(id);
        return _mapper.SearchHistoryToSearchHistoryDTO(getOne);
    }
}