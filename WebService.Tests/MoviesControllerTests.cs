using System.Net;
using System.Net.Http.Json;
using Common.DataTransferObjects;
using Common.Domain;
using Xunit.Abstractions;

namespace WebService.Tests;

public class MoviesControllerTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly WebAppFactoryFixture _fixture;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _outputHelper;

    
    public MoviesControllerTests(WebAppFactoryFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
        _outputHelper = outputHelper;
        _client = _fixture.CreateClient();
    }

    [Fact]
    public async Task GetMoviesReturnsCorrectStatusCode()
    {
        var response = await _client.GetAsync("/Movies");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMovieReturnsCorrectStatusCode()
    {
        var response = await _client.GetAsync("/Movies/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetMovieReturnsNotFoundForInvalidId()
    {
        var response = await _client.GetAsync("/Movies/invalid_id");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostMovieReturnsCorrectStatusCode()
    {
        var movie = new Movie
        {
            Id = "4",
            Type = "Feature Film",
            Title = "Test Movie",
            OriginalTitle = "Test Movie",
            IsAdult = false,
            StartYear = "2023",
            Runtime = 120,
            Genres = "Test",
            Rating = 7.0m,
            Votes = 1000
        };

        var response = await _client.PostAsJsonAsync("/Movies", movie);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PutMovieReturnsCorrectStatusCode()
    {
        var movie = new Movie()
        {
            Type = "Feature Film",
            Title = "Updated Movie",
            OriginalTitle = "Updated Movie",
            IsAdult = false,
            StartYear = "1994",
            Runtime = 142,
            Genres = "Drama",
            Rating = 9.3m,
            Votes = 2400000
        };
        var response = await _client.PutAsJsonAsync("/Movies/1", movie);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var exceptionMessage = await response.Content.ReadAsStringAsync();
            _outputHelper.WriteLine(exceptionMessage);  // make sure your test class has ITestOutputHelper _outputHelper
        }

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteMovieReturnsCorrectStatusCode()
    {
        var response = await _client.DeleteAsync("/Movies/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}