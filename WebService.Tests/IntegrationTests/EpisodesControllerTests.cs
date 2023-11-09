using System.Net;
using System.Net.Http.Json;
using Common.DataTransferObjects;

namespace WebService.Tests.IntegrationTests;

public class EpisodesControllerTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly WebAppFactoryFixture _fixture;
    private readonly HttpClient _client;

    public EpisodesControllerTests(WebAppFactoryFixture fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient();
    }

    [Fact]
    public async Task TestEpisodesController()
    {
        await GetEpisodesReturnsCorrectStatusCodeAndContent();
        await GetEpisodeReturnsCorrectStatusCodeAndContent();
        await GetEpisodeReturnsNotFoundForInvalidId();
        await PostEpisodeReturnsCorrectStatusCodeAndContent();
        await PutEpisodeReturnsCorrectStatusCodeAndContent();
        await DeleteEpisodeReturnsCorrectStatusCodeAndContent();
    }

    private async Task GetEpisodesReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.GetAsync("/Episodes");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var episodes = await response.Content.ReadFromJsonAsync<List<EpisodeDTO>>();
        Assert.True(episodes!.Count == 3); // Adjust the count as per your dummy data.
    }

    private async Task GetEpisodeReturnsCorrectStatusCodeAndContent()
    {
        // Create expected episode DTO based on your dummy data.
        var expectedEpisode = new EpisodeDTO
        {
            Id = "Episode1",
            SeriesId = "1",
            Season = 1,
            Episode1 = 1
        };

        var response = await _client.GetAsync("/Episodes/Episode1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var episode = await response.Content.ReadFromJsonAsync<EpisodeDTO>();
        Assert.Equal(expectedEpisode.Episode1, episode.Episode1);
    }

    private async Task GetEpisodeReturnsNotFoundForInvalidId()
    {
        var response = await _client.GetAsync("/Episodes/invalid_id");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task PostEpisodeReturnsCorrectStatusCodeAndContent()
    {
        // Create an episode DTO to post to the controller.
        var episode = new AlterEpisodeDTO()
        {
            SeriesId = "1",
            Season = 1,
            Episode1 = 2
        };

        var response = await _client.PostAsJsonAsync("/Episodes", episode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var addedEpisode = await response.Content.ReadFromJsonAsync<EpisodeDTO>();
        Assert.Equal(addedEpisode!.Episode1, episode.Episode1);
    }

    private async Task PutEpisodeReturnsCorrectStatusCodeAndContent()
    {
        // Create an episode DTO to update the existing episode with ID 1.
        var episode = new AlterEpisodeDTO()
        {
            SeriesId = "1",
            Season = 1,
            Episode1 = 3
        };

        var response = await _client.PutAsJsonAsync("/Episodes/Episode1", episode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var updatedEpisode = await response.Content.ReadFromJsonAsync<EpisodeDTO>();
            Assert.Equal(episode.Episode1, updatedEpisode?.Episode1); // Check other properties as well.
        }
    }

    private async Task DeleteEpisodeReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.DeleteAsync("/Episodes/Episode2");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}