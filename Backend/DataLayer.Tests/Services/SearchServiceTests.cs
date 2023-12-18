using Common;
using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class SearchServiceTests
{
    private readonly ISearchHistoriesRepository _searchHistoriesRepository;
    private readonly IMoviesRepository _moviesRepository;
    private readonly IPeopleRepository _peopleRepository;

    private readonly SearchService _service;
    private Filter _filter;

    public SearchServiceTests()
    {
        _searchHistoriesRepository = Substitute.For<ISearchHistoriesRepository>();
        _moviesRepository = Substitute.For<IMoviesRepository>();
        _peopleRepository = Substitute.For<IPeopleRepository>();
        _service = new SearchService(_searchHistoriesRepository, _moviesRepository, _peopleRepository);
        _filter = new Filter();
    }

    [Fact]
    public async Task GetAllSearchHistory_ReturnsAllSearchHistory()
    {
        // Arrange
        var searchHistoryList = new List<SearchHistory>();
        _searchHistoriesRepository.GetAll(_filter).Returns(searchHistoryList);

        // Act
        var result = await _service.GetAllSearchHistory(_filter);

        // Assert
        Assert.Equal(searchHistoryList.Count, result.Count);
    }

    [Fact]
    public async Task GetOneSearchHistory_ReturnsSearchHistoryById()
    {
        // Arrange
        var searchHistoryId = "1";
        var searchHistory = new SearchHistory();
        _searchHistoriesRepository.GetById(searchHistoryId).Returns(searchHistory);

        // Act
        var result = await _service.GetOneSearchHistory(searchHistoryId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task BestMatchQuery_ReturnsBestMatchResults()
    {
        // Arrange
        var keywords = new string[] { "keyword1", "keyword2" };
        var bestMatches = new List<BestMatch> { new(), new() };
        _moviesRepository.BestMatchQuery(keywords).Returns(bestMatches);

        // Act
        var result = await _service.BestMatchQuery(keywords);

        // Assert
        Assert.Equal(bestMatches.Count, result.Count);
    }

    [Fact]
    public async Task ExactMatchQuery_ReturnsExactMatchResults()
    {
        // Arrange
        var keywords = new string[] { "keyword1", "keyword2" };
        var exactMatches = new List<string> { "result1", "result2" };
        _moviesRepository.ExactMatchQuery(keywords).Returns(exactMatches);

        // Act
        var result = await _service.ExactMatchQuery(keywords);

        // Assert
        Assert.Equal(exactMatches.Count, result.Count);
    }

    [Fact]
    public async Task PersonWords_ReturnsPersonWords()
    {
        // Arrange
        var word = "Word";
        var frequency = 10;
        var actors = new List<PersonWord> { new(), new() };
        _peopleRepository.PersonWords(word, frequency).Returns(actors);

        // Act
        var result = await _service.PersonWords(word, frequency);

        // Assert
        Assert.Equal(actors.Count, result.Count);
    }

    [Fact]
    public async Task WordToWordsQuery_ReturnsWordToWordsResults()
    {
        // Arrange
        var keywords = new string[] { "keyword1", "keyword2" };
        var wordToWords = new List<WordFrequency> { new(), new() };
        _moviesRepository.WordToWordsQuery(keywords).Returns(wordToWords);

        // Act
        var result = await _service.WordToWordsQuery(keywords);

        // Assert
        Assert.Equal(wordToWords.Count, result.Count);
    }
}