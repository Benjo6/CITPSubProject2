using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebService.Controllers;

namespace WebService.Tests.UnitTests;

public class BookmarkControllerTests
{
    private readonly IBookmarkService _service;
    private readonly BookmarksController _controller;

    public BookmarkControllerTests()
    {
        _service = Substitute.For<IBookmarkService>();
        _controller = new BookmarksController(_service);
    }

    [Fact]
    public async Task CreateMovieBookmark_ReturnsOkResult()
    {
        // Arrange
        var bookmarkMovie = new BookmarkMovieDTO();
        _service.AddBookmarkMovies(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

        // Act
        var result = await _controller.Create(bookmarkMovie);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public async Task CreatePersonalityBookmark_ReturnsOkResult()
    {
        // Arrange
        var bookmarkPersonality = new BookmarkPersonalityDTO();
        _service.AddBookmarkPersonality(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

        // Act
        var result = await _controller.Create(bookmarkPersonality);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public async Task AddNoteMovie_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        var note = "Note for the movie";
        _service.AddNoteMovie(userId, aliasId, note).Returns(true);

        // Act
        var result = await _controller.AddNote(userId, aliasId, note);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public async Task DeletePersonalityBookmark_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var personId = "person1";
        _service.RemoveBookmarkPersonality(userId, personId).Returns(true);

        // Act
        var result = await _controller.DeleteBookmarkPersonality(userId, personId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public async Task DeleteMovieBookmark_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _service.RemoveBookmarkMovies(userId, aliasId).Returns(true);

        // Act
        var result = await _controller.DeleteBookmarkMovie(userId, aliasId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public async Task CreateMovieBookmark_ReturnsBadRequestOnError()
    {
        // Arrange
        var bookmarkMovie = new BookmarkMovieDTO();
        _service.AddBookmarkMovies(Arg.Any<string>(), Arg.Any<string>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.Create(bookmarkMovie);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task CreatePersonalityBookmark_ReturnsBadRequestOnError()
    {
        // Arrange
        var bookmarkPersonality = new BookmarkPersonalityDTO();
        _service.AddBookmarkPersonality(Arg.Any<string>(), Arg.Any<string>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.Create(bookmarkPersonality);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task AddNoteMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        var note = "Note for the movie";
        _service.AddNoteMovie(userId, aliasId, note).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.AddNote(userId, aliasId, note);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task DeletePersonalityBookmark_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "user1";
        var personId = "person1";
        _service.RemoveBookmarkPersonality(userId, personId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteBookmarkPersonality(userId, personId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteMovieBookmark_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _service.RemoveBookmarkMovies(userId, aliasId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteBookmarkMovie(userId, aliasId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
}