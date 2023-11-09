using System.Net;
using System.Net.Http.Json;
using Common.DataTransferObjects;

namespace WebService.Tests.IntegrationTests;

public class SearchControllerTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly HttpClient _client;

    public SearchControllerTests(WebAppFactoryFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task TestSearchController()
    {
        await GetSearchHistoriesReturnsCorrectStatusCodeAndContent();
        await GetOneSearchHistoryReturnsCorrectStatusCodeAndContent();
    }

    private async Task GetSearchHistoriesReturnsCorrectStatusCodeAndContent()
    {
        var response = await _client.GetAsync("/Search/History");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var searchHistories = await response.Content.ReadFromJsonAsync<List<SearchHistoryDTO>>();
        Assert.True(searchHistories != null && searchHistories.Count > 0);
    }

    private async Task GetOneSearchHistoryReturnsCorrectStatusCodeAndContent()
    {
        // Create expected search history DTO based on your dummy data.
        var expectedSearchHistory = new SearchHistoryDTO
        {
            Id = "Search1",
            UserId = "User1",
            SearchQuery = "Action movies",
        };

        var response = await _client.GetAsync("/Search/History/Search1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var searchHistory = await response.Content.ReadFromJsonAsync<SearchHistoryDTO>();
        Assert.Equal(expectedSearchHistory.Id, searchHistory.Id);
    }
}