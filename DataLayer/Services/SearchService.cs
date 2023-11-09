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
       var all = new List<SearchHistoryDTO>();
       foreach(var x in getAll)
       {
           all.Add(_mapper.SearchHistoryToSearchHistoryDTO(x));
       }
       return all;
    }

    public async Task<SearchHistoryDTO> GetOneSearchHistory(string id)
    {
        var getOne = await _searchHistoriesRepository.GetById(id);
        return _mapper.SearchHistoryToSearchHistoryDTO(getOne);
    }
}