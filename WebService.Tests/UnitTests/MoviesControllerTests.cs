using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebService.Controllers;

namespace WebService.Tests.UnitTests;

public class MoviesControllerTests
{
    private readonly IMoviesService _service;
    private readonly MoviesController _controller;

    public MoviesControllerTests()
    {
        _service = Substitute.For<IMoviesService>();
        _controller = new MoviesController(_service);
    }

    [Fact]
    public async Task GetMovies_ReturnsOkResult()
    {
        // Arrange
        var expectedMovies = new List<GetAllMovieDTO>();
        _service.GetAllMovies(Arg.Any<Filter>()).Returns(expectedMovies);

        // Act
        var result = await _controller.GetMovies();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<GetAllMovieDTO>>(okResult.Value);
        Assert.Equal(expectedMovies, model);
    }

    [Fact]
    public async Task GetMovie_ReturnsOkResult()
    {
        // Arrange
        var movieId = "1";
        var expectedMovie = new GetOneMovieDTO();
        _service.GetOneMovie(movieId).Returns(expectedMovie);

        // Act
        var result = await _controller.GetMovie(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<GetOneMovieDTO>(okResult.Value);
        Assert.Equal(expectedMovie, model);
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
    public async Task FindSimilarMovies_ReturnsOkResult()
    {
        // Arrange
        var movieId = "13";
        var expectedSimilarMovies = new List<SimilarMovie>(); // Adjust as needed
        _service.FindSimilarMovies(movieId).Returns(expectedSimilarMovies);

        // Act
        var result = await _controller.FindSimilarMovies(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<SimilarMovie>>(okResult.Value);
        Assert.Equal(expectedSimilarMovies, model);
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
    public async Task PutMovie_ReturnsOkResult()
    {
        // Arrange
        var movieId = "1";
        var movie = new AlterMovieDTO();
        var updatedMovie = new AlterResponseMovieDTO();
        _service.UpdateMovie(movieId, movie).Returns(updatedMovie);

        // Act
        var result = await _controller.PutMovie(movieId, movie);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<AlterResponseMovieDTO>(okResult.Value);
        Assert.Equal(updatedMovie, model);
    }

    [Fact]
    public async Task PostMovie_ReturnsOkResult()
    {
        // Arrange
        var movie = new AlterMovieDTO();
        var createdMovie = new AlterResponseMovieDTO();
        _service.AddMovie(movie).Returns(createdMovie);

        // Act
        var result = await _controller.PostMovie(movie);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<AlterResponseMovieDTO>(okResult.Value);
        Assert.Equal(createdMovie, model);
    }

    [Fact]
    public async Task DeleteMovie_ReturnsOkResult()
    {
        // Arrange
        var movieId = "1";
        var deletionResult = true; // Adjust as needed
        _service.DeleteMovie(movieId).Returns(deletionResult);

        // Act
        var result = await _controller.DeleteMovie(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(deletionResult, okResult.Value);
    }

    [Fact]
    public async Task GetMovies_ReturnsBadRequestOnError()
    {
        // Arrange
        _service.GetAllMovies(Arg.Any<Filter>()).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetMovies();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task GetMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var movieId = "1";
        _service.GetOneMovie(movieId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetMovie(movieId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
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
    public async Task FindSimilarMovies_ReturnsBadRequestOnError()
    {
        // Arrange
        var movieId = "13";
        _service.FindSimilarMovies(movieId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.FindSimilarMovies(movieId);

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
    public async Task PutMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var movieId = "1";
        var movie = new AlterMovieDTO();
        _service.UpdateMovie(movieId, movie).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PutMovie(movieId, movie);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task PostMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var movie = new AlterMovieDTO();
        _service.AddMovie(movie).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.PostMovie(movie);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var movieId = "1";
        _service.DeleteMovie(movieId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.DeleteMovie(movieId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
}