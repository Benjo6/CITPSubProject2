using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class SearchHistoriesRepository : GenericRepository<SearchHistory>, ISearchHistoriesRepository
{
    public SearchHistoriesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddSearchHistory(int userId, string searchString)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SearchResult>> StringSearch(int userId, string searchString)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SearchResult>> StructuredStringSearch(int userId, string title, string personName)
    {
        throw new NotImplementedException();
    }

    public async Task<float> CalculateTermFrequency(string tconst, string word)
    {
        throw new NotImplementedException();
    }

    public async Task<float> CalculateInverseDocumentFrequency(string word)
    {
        throw new NotImplementedException();
    }
}