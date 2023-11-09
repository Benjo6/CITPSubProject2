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
        _service.GetAllSearchHistory().Returns(expectedSearchHistories);

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
        _service.GetAllSearchHistory().Throws(new Exception("Test exception"));

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
}