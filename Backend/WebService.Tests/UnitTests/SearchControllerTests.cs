using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebService.Controllers;

namespace WebService.Tests.UnitTests;

public class SearchControllerTests
{
    private readonly ISearchService _service;
    private readonly SearchController _controller;

    public SearchControllerTests()
    {
        _service = Substitute.For<ISearchService>();
        _controller = new SearchController(_service);
    }

    [Fact]
    public async Task GetSearchHistories_ReturnsOkResult()
    {
        // Arrange
        var expectedSearchHistories = new List<SearchHistoryDTO>();
        _service.GetAllSearchHistory(Arg.Any<Filter>()).Returns(expectedSearchHistories);

        // Act
        var result = await _controller.GetSearchHistories();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<SearchHistoryDTO>>(okResult.Value);
        Assert.Equal(expectedSearchHistories, model);
    }

    [Fact]
    public async Task GetOneSearchHistory_ReturnsOkResult()
    {
        // Arrange
        var searchHistoryId = "1";
        var expectedSearchHistory = new SearchHistoryDTO();
        _service.GetOneSearchHistory(searchHistoryId).Returns(expectedSearchHistory);

        // Act
        var result = await _controller.GetOneSearchHistory(searchHistoryId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<SearchHistoryDTO>(okResult.Value);
        Assert.Equal(expectedSearchHistory, model);
    }

    [Fact]
    public async Task GetSearchHistories_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.GetAllSearchHistory(Arg.Any<Filter>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetSearchHistories();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task GetOneSearchHistory_ReturnsBadRequestOnError()
    {
        // Arrange
        var searchHistoryId = "1";
        _service.GetOneSearchHistory(searchHistoryId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetOneSearchHistory(searchHistoryId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task BestMatchQuery_ReturnsOkResult()
    {
        // Arrange
        var keywords = new string[] { "Action", "Comedy" };
        var expectedBestMatchQuery = new List<BestMatch>(); // Adjust as needed
        _service.BestMatchQuery(keywords).Returns(expectedBestMatchQuery);

        // Act
        var result = await _controller.BestMatchQuery(keywords);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<BestMatch>>(okResult.Value);
        Assert.Equal(expectedBestMatchQuery, model);
    }


    [Fact]
    public async Task ExactMatchQuery_ReturnsOkResult()
    {
        // Arrange
        var keywords = new string[] { "Action", "Comedy" };
        var expectedExactMatchQuery = new List<string>(); // Adjust as needed
        _service.ExactMatchQuery(keywords).Returns(expectedExactMatchQuery);

        // Act
        var result = await _controller.ExactMatchQuery(keywords);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
        Assert.Equal(expectedExactMatchQuery, model);
    }

    [Fact]
    public async Task WordToWordsQuery_ReturnsOkResult()
    {
        // Arrange
        var keywords = new string[] { "Action", "Comedy" };
        var expectedWordToWord = new List<WordFrequency>(); // Adjust as needed
        _service.WordToWordsQuery(keywords).Returns(expectedWordToWord);

        // Act
        var result = await _controller.WordToWordsQuery(keywords);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<WordFrequency>>(okResult.Value);
        Assert.Equal(expectedWordToWord, model);
    }


    [Fact]
    public async Task BestMatchQuery_ReturnsBadRequestOnError()
    {
        // Arrange
        var keywords = new string[] { "Action", "Comedy" };
        _service.BestMatchQuery(keywords).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.BestMatchQuery(keywords);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task ExactMatchQuery_ReturnsBadRequestOnError()
    {
        // Arrange
        var keywords = new string[] { "Action", "Comedy" };
        _service.ExactMatchQuery(keywords).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.ExactMatchQuery(keywords);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task WordToWordsQuery_ReturnsBadRequestOnError()
    {
        // Arrange
        var keywords = new string[] { "Action", "Comedy" };
        _service.WordToWordsQuery(keywords).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.WordToWordsQuery(keywords);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task PersonWords_ReturnsOkResult()
    {
        // Arrange
        var word = "test";
        var frequency = 5;
        var expectedPersonWords = new List<PersonWord>(); // Adjust as needed
        _service.PersonWords(word, frequency).Returns(expectedPersonWords);

        // Act
        var result = await _controller.PersonWords(word, frequency);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<PersonWord>>(okResult.Value);
        Assert.Equal(expectedPersonWords, model);
    }

    [Fact]
    public async Task PersonWords_ReturnsBadRequestOnError()
    {
        // Arrange
        var word = "test";
        var frequency = 5;
        _service.PersonWords(word, frequency).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PersonWords(word, frequency);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
}