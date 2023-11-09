using DataLayer.Repositories.Contracts;
using DataLayer.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DataLayer.Tests.Services;

public class BookmarkServiceTests
{
    private readonly IBookmarkPersonalitiesRepository _personalityRepository;
    private readonly IBookmarkMoviesRepository _moviesRepository;
    private readonly BookmarkService _service;

    public BookmarkServiceTests()
    {
        _personalityRepository = Substitute.For<IBookmarkPersonalitiesRepository>();
        _moviesRepository = Substitute.For<IBookmarkMoviesRepository>();
        _service = new BookmarkService(_personalityRepository, _moviesRepository);
    }

    [Fact]
    public async Task AddBookmarkMovies_ReturnsTrueOnSuccess()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _moviesRepository.AddBookmarkMovies(userId, aliasId).Returns(Task.CompletedTask);

        // Act
        var result = await _service.AddBookmarkMovies(userId, aliasId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddBookmarkMovies_ReturnsFalseOnAddError()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _moviesRepository.AddBookmarkMovies(userId, aliasId).Throws(new Exception("Test exception"));

        // Act
        var result = await _service.AddBookmarkMovies(userId, aliasId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddBookmarkPersonality_ReturnsTrueOnSuccess()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _personalityRepository.AddBookmarkPersonality(userId, aliasId).Returns(Task.CompletedTask);

        // Act
        var result = await _service.AddBookmarkPersonality(userId, aliasId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddBookmarkPersonality_ReturnsFalseOnAddError()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _personalityRepository.AddBookmarkPersonality(userId, aliasId).Throws(new Exception("Test exception"));

        // Act
        var result = await _service.AddBookmarkPersonality(userId, aliasId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddNoteMovie_ReturnsTrueOnSuccess()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        var note = "Note for the movie";
        _moviesRepository.AddNote(userId, aliasId, note).Returns(Task.CompletedTask);

        // Act
        var result = await _service.AddNoteMovie(userId, aliasId, note);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddNoteMovie_ReturnsFalseOnAddNoteError()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        var note = "Note for the movie";
        _moviesRepository.AddNote(userId, aliasId, note).Throws(new Exception("Test exception"));

        // Act
        var result = await _service.AddNoteMovie(userId, aliasId, note);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveBookmarkMovie_ReturnsTrueOnSuccess()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _moviesRepository.DeleteBookmarkMovie(userId, aliasId).Returns(true);

        // Act
        var result = await _service.RemoveBookmarkMovie(userId, aliasId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoveBookmarkMovie_ReturnsFalseOnDeleteError()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _moviesRepository.DeleteBookmarkMovie(userId, aliasId).Throws(new Exception("Test exception"));

        // Act
        var result = await _service.RemoveBookmarkMovie(userId, aliasId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveBookmarkPersonality_ReturnsTrueOnSuccess()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _personalityRepository.DeleteBookmarkPersonality(userId, aliasId).Returns(true);

        // Act
        var result = await _service.RemoveBookmarkPersonality(userId, aliasId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoveBookmarkPersonality_ReturnsFalseOnDeleteError()
    {
        // Arrange
        var userId = "user1";
        var aliasId = "alias1";
        _personalityRepository.DeleteBookmarkPersonality(userId, aliasId).Throws(new Exception("Test exception"));

        // Act
        var result = await _service.RemoveBookmarkPersonality(userId, aliasId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetBookmarkMovies_ReturnsListOfMovies()
    {
        // Arrange
        var userId = "user1";
        var movies = new List<string> { "movie1", "movie2" };
        _moviesRepository.GetBookmarksMovies(userId).Returns(movies);

        // Act
        var result = await _service.GetBookmarkMovies(userId);

        // Assert
        Assert.Equal(movies.Count, result.Count);
    }

    [Fact]
    public async Task GetBookmarkPersons_ReturnsListOfPersons()
    {
        // Arrange
        var userId = "user1";
        var persons = new List<string> { "person1", "person2" };
        _personalityRepository.GetBookmarksPersonality(userId).Returns(persons);

        // Act
        var result = await _service.GetBookmarkPersons(userId);

        // Assert
        Assert.Equal(persons.Count, result.Count);
    }
}