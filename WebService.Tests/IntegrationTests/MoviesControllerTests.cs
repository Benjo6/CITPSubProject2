using System.Net;
using System.Net.Http.Json;
using Common.DataTransferObjects;

namespace WebService.Tests.IntegrationTests;

public class MoviesControllerTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly WebAppFactoryFixture _fixture;
    private readonly HttpClient _client;

    public MoviesControllerTests(WebAppFactoryFixture fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient();
    }
    
    [Fact]
    public async Task TestMoviesController()
    {
        await GetMoviesReturnsCorrectStatusCodeAndContent();
        await GetMovieReturnsCorrectStatusCodeAndContent();
        await GetMovieReturnsNotFoundForInvalidId();
        await PostMovieReturnsCorrectStatusCodeAndContent();
        await PutMovieReturnsCorrectStatusCodeAndContent();
        await DeleteMovieReturnsCorrectStatusCodeAndContent();
    }
    private async Task GetMoviesReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.GetAsync("/Movies");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var movies = await response.Content.ReadFromJsonAsync<System.Collections.Generic.List<GetAllMovieDTO>>();
        Assert.True(movies!.Count == 3);
    }
    private async Task GetMovieReturnsCorrectStatusCodeAndContent()
    {
        var exceptedMovie = new GetOneMovieDTO()
        {
            Type = "Feature Film",
            OriginalTitle = "The Shawshank Redemption",
            IsAdult = false,
            StartYear = "1994",
            Runtime = 142,
            Genres = "Drama",
            Rating = 9.3m,
            Votes = 2400000
        };
        var response = await _client.GetAsync("/Movies/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var movie = await response.Content.ReadFromJsonAsync<GetOneMovieDTO>();
        Assert.Equivalent(exceptedMovie,movie);
    }
    private async Task GetMovieReturnsNotFoundForInvalidId()
    {
        var response = await _client.GetAsync("/Movies/invalid_id");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    private async Task PostMovieReturnsCorrectStatusCodeAndContent()
    {
        var movie = new AlterMovieDTO
        {
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
        var addedMovie = await response.Content.ReadFromJsonAsync<AlterResponseMovieDTO>();
        Assert.Equal(addedMovie!.Title, movie.Title);
    }
    private async Task PutMovieReturnsCorrectStatusCodeAndContent()
    {
        var movie = new AlterMovieDTO()
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
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var updatedMovie = await response.Content.ReadFromJsonAsync<AlterResponseMovieDTO>();
            Assert.Equal(movie.OriginalTitle, updatedMovie?.OriginalTitle);
        }
    }
    private async Task DeleteMovieReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.DeleteAsync("/Movies/2");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}