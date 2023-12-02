using Common;
using Common.Domain;
using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class SearchServiceTests
{
    private readonly ISearchHistoriesRepository _searchHistoriesRepository;
    private readonly SearchService _service;
    private Filter _filter;

    public SearchServiceTests()
    {
        _searchHistoriesRepository = Substitute.For<ISearchHistoriesRepository>();
        _service = new SearchService(_searchHistoriesRepository);
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
}