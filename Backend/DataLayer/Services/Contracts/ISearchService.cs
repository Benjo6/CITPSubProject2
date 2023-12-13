using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface ISearchService
{
    public Task<(List<SearchHistoryDTO>, Metadata)> GetAllSearchHistory(Filter filter);
    public Task<SearchHistoryDTO> GetOneSearchHistory(string id);
    public Task<List<string>> ExactMatchQuery(string[] keywords, int? page = 1, int? perPage = 10);
    public Task<List<BestMatch>> BestMatchQuery(string[] keywords, int? page = 1, int? perPage = 10);
    public Task<List<WordFrequency>> WordToWordsQuery(string[] keywords);
    public Task<List<PersonWord>> PersonWords(string word, int frequency);
    public Task<List<SearchResult>> StringSearch(string searchString, int? resultCount = 10);
    public Task<List<SearchResult>> LoggedInStringSearch(string userId, string searchString, int? resultCount = 10);

    public Task<List<StructuredSearchResult>> StructuredStringSearch(string userId, string title, string personName,
        int? resultCount);
}