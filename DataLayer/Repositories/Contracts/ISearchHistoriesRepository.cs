using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Repositories.Contracts;

public interface ISearchHistoriesRepository : IGenericRepository<SearchHistory>
{
    public Task AddSearchHistory(int userId, string searchString);

    public Task<List<SearchResult>> StringSearch(int userId, string searchString);

    public Task<List<SearchResult>> StructuredStringSearch(int userId, string title, string
        personName);
    
    Task<float> CalculateTermFrequency(string tconst, string word);

    Task<float> CalculateInverseDocumentFrequency(string word);
    
}