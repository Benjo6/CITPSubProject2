using Common;
using Common.DataTransferObjects;
using Common.Mapper;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class SearchService : ISearchService
{
    private readonly ISearchHistoriesRepository _searchHistoriesRepository;
    private readonly IMoviesRepository _movieRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly ObjectMapper _mapper;

    public SearchService(ISearchHistoriesRepository searchHistoriesRepository, IMoviesRepository movieRepository, IPeopleRepository peopleRepository)
    {
        _movieRepository = movieRepository;
        _peopleRepository = peopleRepository;
        _searchHistoriesRepository = searchHistoriesRepository;
        _mapper = new ObjectMapper();
    }

    public async Task<(List<SearchHistoryDTO>, Metadata)> GetAllSearchHistory(Filter filter)
    {
        var (getAll, metadata) = await _searchHistoriesRepository.GetAll(filter);
        return (_mapper.ListSearchToListSearchDTO(getAll), metadata);
    }

    public async Task<SearchHistoryDTO> GetOneSearchHistory(string id)
    {
        var getOne = await _searchHistoriesRepository.GetById(id);
        return _mapper.SearchHistoryToSearchHistoryDTO(getOne);
    }

    public async Task<List<string>> ExactMatchQuery(string[] keywords, int? page = 1, int? perPage = 10)
    {
        return await _movieRepository.ExactMatchQuery(keywords, page, perPage);
    }

    public async Task<List<BestMatch>> BestMatchQuery(string[] keywords, int? page = 1, int? perPage = 10)
    {
        return await _movieRepository.BestMatchQuery(keywords, page, perPage);
    }

    public async Task<List<WordFrequency>> WordToWordsQuery(string[] keywords)
    {
        return await _movieRepository.WordToWordsQuery(keywords);
    }
    
    public async Task<List<PersonWord>> PersonWords(string word, int frequency)
    {
        return await _peopleRepository.PersonWords(word, frequency);
    }

    public async Task<List<SearchResult>> StringSearch(string searchString, int? resultCount = 10)
    {
        return await _movieRepository.StringSearch(searchString,resultCount);
    }
    
    public async Task<List<SearchResult>> LoggedInStringSearch(string userId, string searchString, int? resultCount = 10)
    {
        return await _movieRepository.LoggedInStringSearch(userId, searchString,resultCount);
    }


    public async Task<List<StructuredSearchResult>> StructuredStringSearch(string userId, string title,
        string personName, int? resultCount)
    {
        return await _movieRepository.StructuredStringSearch(userId, title, personName, resultCount);
    }
}