using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface ISearchService
{
    Task<List<SearchHistoryDTO>> GetAllSearchHistory();
    Task<SearchHistoryDTO> GetOneSearchHistory(string id);
}