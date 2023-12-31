using Common;
using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using NSubstitute;

namespace DataLayer.Tests.Services;

public class MoviesServiceTests
{
    private readonly IMoviesRepository _repository;
    private readonly MoviesService _service;
    private Filter _filter;

    public MoviesServiceTests()
    {
        _repository = Substitute.For<IMoviesRepository>();
        _service = new MoviesService(_repository);
        _filter = new Filter();
    }

    [Fact]
    public async Task GetAllMovies_ReturnsAllMovies()
    {
        // Arrange
        var movies = new List<Movie> { new(), new() };
        _repository.GetAll(_filter).Returns(movies);

        // Act
        var result = await _service.GetAllMovies(_filter);

        // Assert
        Assert.Equal(movies.Count, result.Count);
    }

    [Fact]
    public async Task GetOneMovie_ReturnsMovieById()
    {
        // Arrange
        var movieId = "1";
        var movie = new Movie();
        _repository.GetById(movieId).Returns(movie);

        // Act
        var result = await _service.GetOneMovie(movieId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateMovie_ReturnsUpdatedMovie()
    {
        // Arrange
        var movieId = "1";
        var movie = new Movie();
        var alterMovie = new AlterMovieDTO();
        _repository.Update(Arg.Any<Movie>()).Returns(true);
        _repository.GetById(movieId).Returns(movie);

        // Act
        var result = await _service.UpdateMovie(movieId, alterMovie);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateMovie_ReturnsEmptyMovieOnUpdateFailure()
    {
        // Arrange
        var movieId = "1";
        var alterMovie = new AlterMovieDTO();
        _repository.Update(Arg.Any<Movie>()).Returns(false);
        _repository.GetById(Arg.Any<string>()).Returns(new Movie());

        // Act
        var result = await _service.UpdateMovie(movieId, alterMovie);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(default, result.Id);
    }

    [Fact]
    public async Task AddMovie_ReturnsAddedMovie()
    {
        // Arrange
        var alterMovie = new AlterMovieDTO();
        var movie = new Movie();
        _repository.Add(Arg.Any<Movie>()).Returns(movie);

        // Act
        var result = await _service.AddMovie(alterMovie);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteMovie_ReturnsTrueOnSuccess()
    {
        // Arrange
        var movieId = "1";
        _repository.GetById(Arg.Any<string>()).Returns(new Movie());
        _repository.Delete(Arg.Any<Movie>()).Returns(true);

        // Act
        var result = await _service.DeleteMovie(movieId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetPopularActorsInMovie_ReturnsPopularActorsInMovie()
    {
        // Arrange
        var movieId = "MovieId";
        var actors = new List<PopularActor> { new(), new() };
        _repository.GetPopularActorsInMovie(movieId).Returns(actors);

        // Act
        var result = await _service.GetPopularActorsInMovie(movieId);

        // Assert
        Assert.Equal(actors.Count, result.Count);
    }

    [Fact]
    public async Task FindSimilarMovies_ReturnsSimilarMovies()
    {
        // Arrange
        var movieId = "1";
        var similarMovies = new List<SimilarMovie> { new(), new() };
        _repository.FindSimilarMovies(movieId).Returns(similarMovies);

        // Act
        var result = await _service.FindSimilarMovies(movieId);

        // Assert
        Assert.Equal(similarMovies.Count, result.Count);
    }
}