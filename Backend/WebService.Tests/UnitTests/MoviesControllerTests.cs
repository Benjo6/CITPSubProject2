using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
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

        var urlHelper = Substitute.For<IUrlHelper>();
        urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("callbackUrl");
        _controller.Url = urlHelper;
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<MoviesResult>(json);
        List<GetAllMovieDTO> returnedMoviesDTOs = data.Movies.Select(a => a.Movie).ToList();
        Assert.Equal(expectedMovies, returnedMoviesDTOs);
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<GetOneMovieWithUri>(json);
        var model = data.Movie;
        Assert.Equal(expectedMovie.Id, model.Id);
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<SimilarMovieResult>(json);
        var model = data.SimilarMovies;
        Assert.Equal(expectedSimilarMovies, model);
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<AlterMovieWithUri>(json);
        var model = data.Movie;

        Assert.Equal(updatedMovie.Id, model.Id);
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<AlterMovieWithUri>(json);
        var model = data.Movie;
        Assert.Equal(createdMovie.Id, model.Id);
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
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<MovieBooleanResult>(json);
        Assert.True(data.Result);
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
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
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task GetPopularActorsInMovie_ReturnsOkResult()
    {
        // Arrange
        var movieId = "123";
        var expectedActors = new List<PopularActor>(); // Adjust as needed
        _service.GetPopularActorsInMovie(movieId).Returns(expectedActors);

        // Act
        var result = await _controller.GetPopularActorsInMovie(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var json = JsonConvert.SerializeObject(okResult.Value);
        var data = JsonConvert.DeserializeObject<PopularMoviesResult>(json);
        Assert.Equal(expectedActors, data.Actors);
    }

    [Fact]
    public async Task GetPopularActorsInMovie_ReturnsBadRequestOnError()
    {
        // Arrange
        var movieId = "123";
        _service.GetPopularActorsInMovie(movieId).Throws(new Exception("Test exception"));

        // Act
        var result = await _controller.GetPopularActorsInMovie(movieId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Test exception", badRequestResult.Value.ToString());
    }
}

public class AlterMovieWithUri
{
    public AlterResponseMovieDTO Movie { get; set; }
    public string Uri { get; set; }
}
public class MovieWithUri
{
    public GetAllMovieDTO Movie { get; set; }
    public string Uri { get; set; }
}
public class GetOneMovieWithUri
{
    public GetOneMovieDTO Movie { get; set; }
    public string Uri { get; set; }
}


public class MoviesResult
{
    public IEnumerable<MovieWithUri> Movies { get; set; }
    public string Uri { get; set; }
}

public class SimilarMovieResult
{
    public IEnumerable<SimilarMovie> SimilarMovies { get; set; }
    public string Uri { get; set; }
}

public class PopularMoviesResult
{
    public IEnumerable<PopularActor> Actors { get; set; }
    public string Uri { get; set; }
}

public class MovieBooleanResult
{
    public bool Result { get; set; }
    public string Uri { get; set; }
}