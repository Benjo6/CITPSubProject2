using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
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

        var urlHelper = Substitute.For<IUrlHelper>();
        urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("callbackUrl");
        _controller.Url = urlHelper;
    }

    [Fact]
    public async Task GetMovies_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var expectedMovies = new List<string>();
        _service.GetBookmarkMovies(userId, Arg.Any<int>(), Arg.Any<int>()).Returns(expectedMovies);

        // Act
        var result = await _controller.GetMovies(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<BookmarksResult>(json);
        List<string> returnedMovies = data.Result.Cast<string>().ToList();
        Assert.Equal(expectedMovies, returnedMovies);
    }

    [Fact]
    public async Task GetPerson_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var expectedPersons = new List<string>();
        _service.GetBookmarkPersons(userId, Arg.Any<int>(), Arg.Any<int>()).Returns(expectedPersons);

        // Act
        var result = await _controller.GetPerson(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<BookmarksResult>(json);
        List<string> returnedPersons = data.Result.Cast<string>().ToList();
        Assert.Equal(expectedPersons, returnedPersons);
    }

    [Fact]
    public async Task CreateBMMovie_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var movieId = "movie1";
        _service.AddBookmarkMovies(userId, movieId).Returns(true);

        // Act
        var result = await _controller.CreateBMMovie(userId, movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<BookmarksBoolResult>(json);
        Assert.True(data.Result);
    }

    [Fact]
    public async Task CreateBMPerson_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var personId = "person1";
        _service.AddBookmarkPersonality(userId, personId).Returns(true);

        // Act
        var result = await _controller.CreateBMPerson(userId, personId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<BookmarksBoolResult>(json);
        Assert.True(data.Result);
    }

    [Fact]
    public async Task AddNote_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var movieId = "movie1";
        var note = "Note for the movie";
        _service.AddNoteMovie(userId, movieId, note).Returns(true);

        // Act
        var result = await _controller.AddNote(userId, movieId, note);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<BookmarksBoolResult>(json);
        Assert.True(data.Result);
    }

    [Fact]
    public async Task DeleteBookmarkPersonality_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var personId = "person1";
        _service.RemoveBookmarkPersonality(userId, personId).Returns(true);

        // Act
        var result = await _controller.DeleteBookmarkPersonality(userId, personId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<BookmarksBoolResult>(json);
        Assert.True(data.Result);
    }

    [Fact]
    public async Task DeleteBookmarkMovie_ReturnsOkResult()
    {
        // Arrange
        var userId = "user1";
        var movieId = "movie1";
        _service.RemoveBookmarkMovies(userId, movieId).Returns(true);

        // Act
        var result = await _controller.DeleteBookmarkMovie(userId, movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<BookmarksBoolResult>(json);
        Assert.True(data.Result);
    }

    [Fact]
    public async Task CreateBMMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.AddBookmarkMovies(Arg.Any<string>(), Arg.Any<string>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.CreateBMMovie("userId", "movieId");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task CreateBMPerson_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.AddBookmarkPersonality(Arg.Any<string>(), Arg.Any<string>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.CreateBMPerson("userId", "personId");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task AddNote_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "user1";
        var movieId = "movie1";
        var note = "Note for the movie";
        _service.AddNoteMovie(userId, movieId, note).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.AddNote(userId, movieId, note);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task DeleteBookmarkPersonality_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "user1";
        var personId = "person1";
        _service.RemoveBookmarkPersonality(userId, personId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteBookmarkPersonality(userId, personId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task DeleteBookmarkMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var userId = "user1";
        var movieId = "movie1";
        _service.RemoveBookmarkMovies(userId, movieId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteBookmarkMovie(userId, movieId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }
}

public class BookmarksResult
{
    public IEnumerable<string> Result { get; set; }
    public string Uri { get; set; }
}


public class BookmarksBoolResult
{
    public bool Result { get; set; }
    public string Uri { get; set; }
}