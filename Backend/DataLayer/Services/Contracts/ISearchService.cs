using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface ISearchService
{
    Task<List<SearchHistoryDTO>> GetAllSearchHistory(Filter filter);
    Task<SearchHistoryDTO> GetOneSearchHistory(string id);
}